using Business.Abstractions.Persistence;
using Business.Authentication;
using Business.Organizations.Specifications;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using Domain.Users;
using Domain.Users.ValueObjects;
using MediatR;

namespace Business.Organizations.Invitations.AcceptInvitation
{
    public sealed class AcceptInvitationCommandHandler
        : IRequestHandler<AcceptInvitationCommand, Result>
    {
        private readonly IOrganizationInvitationRepository invitationRepository;
        private readonly IOrganizationRepository organizationRepository;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly ICurrentUser currentUser;
        private readonly IUnitOfWork unitOfWork;

        public AcceptInvitationCommandHandler(
            IOrganizationInvitationRepository invitationRepository,
            IOrganizationRepository organizationRepository,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ICurrentUser currentUser,
            IUnitOfWork unitOfWork)
        {
            this.invitationRepository = invitationRepository;
            this.organizationRepository = organizationRepository;
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.currentUser = currentUser;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            if (!currentUser.IsAuthenticated || string.IsNullOrWhiteSpace(currentUser.Email))
            {
                return Result.Failure(new Error("Auth.Required", "Authentication required to accept invitation."));
            }

            var pending = await invitationRepository.ListPendingAsync(cancellationToken);

            OrganizationInvitation? invitation = null;
            foreach (var inv in pending)
            {
                if (passwordHasher.Verify(request.Token, inv.Token))
                {
                    invitation = inv;
                    break;
                }
            }

            if (invitation is null)
            {
                return Result.Failure(InvitationErrors.NotFound);
            }

            if (!string.Equals(invitation.Email.Value, currentUser.Email, StringComparison.OrdinalIgnoreCase))
            {
                return Result.Failure(new Error("Invitation.EmailMismatch", "Invitation email does not match the current user."));
            }

            var userEmail = Email.Create(currentUser.Email!);
            if (userEmail.IsFailure)
            {
                return Result.Failure(userEmail.Error);
            }

            var user = await userRepository.GetByEmailAsync(userEmail.Value!);
            if (user is null)
            {
                return Result.Failure(Domain.Users.Errors.UserErrors.EmailNotFound);
            }

            if (invitation.ExpiresAt < DateTime.UtcNow)
            {
                return Result.Failure(InvitationErrors.Expired);
            }

            if (invitation.Status != InvitationStatus.Pending)
            {
                return Result.Failure(InvitationErrors.AlreadyProcessed);
            }

            var orgSpec = new OrganizationByIdSpecification(invitation.OrganizationId);
            var organization = await organizationRepository.FirstOrDefaultAsync(orgSpec, cancellationToken);
            if (organization is null)
            {
                return Result.Failure(OrganizationErrors.NotFound);
            }

            var addResult = organization.AddMember(user.Id);
            if (addResult.IsFailure)
            {
                return Result.Failure(addResult.Error);
            }

            invitation.Accept();
            await invitationRepository.UpdateAsync(invitation, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

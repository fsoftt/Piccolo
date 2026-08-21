using Business.Abstractions.Persistence;
using Business.Authentication;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using Domain.Users.ValueObjects;
using MediatR;
using Business.Organizations.Specifications;

namespace Business.Organizations.Invitations.CreateInvitation
{
    public sealed class CreateOrganizationInvitationCommandHandler
        : IRequestHandler<CreateOrganizationInvitationCommand, Result<CreateOrganizationInvitationResponse>>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IOrganizationInvitationRepository invitationRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IInvitationTokenGenerator tokenGenerator;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrganizationInvitationCommandHandler(
            IOrganizationRepository organizationRepository,
            IOrganizationInvitationRepository invitationRepository,
            IPasswordHasher passwordHasher,
            IInvitationTokenGenerator tokenGenerator,
            IUnitOfWork unitOfWork)
        {
            this.organizationRepository = organizationRepository;
            this.invitationRepository = invitationRepository;
            this.passwordHasher = passwordHasher;
            this.tokenGenerator = tokenGenerator;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateOrganizationInvitationResponse>> Handle(
            CreateOrganizationInvitationCommand request,
            CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
            {
                return Result<CreateOrganizationInvitationResponse>.Failure(emailResult.Error);
            }

            var specification = new OrganizationByIdSpecification(request.OrganizationId);

            var organization = await organizationRepository.FirstOrDefaultAsync(
                specification,
                cancellationToken);
            if (organization is null)
            {
                return Result<CreateOrganizationInvitationResponse>.Failure(OrganizationErrors.NotFound);
            }

            var token = tokenGenerator.GenerateToken();
            var hashed = passwordHasher.Hash(token);
            var expiresAt = DateTime.UtcNow.AddDays(7);

            var invitation = OrganizationInvitation.Create(
                request.OrganizationId,
                emailResult.Value!,
                hashed,
                expiresAt);

            invitation.AddCreatedEvent(token);

            await invitationRepository.AddAsync(invitation, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new CreateOrganizationInvitationResponse(
                invitation.Id,
                invitation.OrganizationId,
                invitation.Email.Value,
                invitation.ExpiresAt,
                token);

            return Result<CreateOrganizationInvitationResponse>.Success(response);
        }
    }
}

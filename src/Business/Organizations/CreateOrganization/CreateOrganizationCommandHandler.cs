using Business.Abstractions.Persistence;
using Business.Authentication;
using Business.Organizations.Policies;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.ValueObjects;
using Domain.Users.Errors;

namespace Business.Organizations.CreateOrganization
{
    public sealed class CreateOrganizationCommandHandler
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;
        private readonly ICreateOrganizationPolicy createOrganizationPolicy;

        public CreateOrganizationCommandHandler(
            IOrganizationRepository organizationRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ICreateOrganizationPolicy createOrganizationPolicy)
        {
            this.organizationRepository = organizationRepository;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
            this.createOrganizationPolicy = createOrganizationPolicy;
        }

        public async Task<Result<CreateOrganizationResponse>> Handle(
            CreateOrganizationCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserId = currentUser.UserId;
            if (currentUserId is null)
            {
                return Result<CreateOrganizationResponse>.Failure(
                    AuthenticationErrors.Unauthorized);
            }

            var policyResult = await createOrganizationPolicy.CanCreateAsync(
                currentUserId.Value,
                cancellationToken);
            if (policyResult.IsFailure)
            {
                return Result<CreateOrganizationResponse>.Failure(
                    policyResult.Error);
            }

            var organizationNameResult = OrganizationName.Create(request.Name);
            if (organizationNameResult.IsFailure)
            {
                return Result<CreateOrganizationResponse>.Failure(
                    organizationNameResult.Error);
            }

            var organizationResult = Organization.Create(
                organizationNameResult.Value!,
                currentUserId.Value);
            if (organizationResult.IsFailure)
            {
                return Result<CreateOrganizationResponse>.Failure(
                    organizationResult.Error);
            }

            await organizationRepository.AddAsync(
                organizationResult.Value!,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<CreateOrganizationResponse>.Success(
                new CreateOrganizationResponse(
                    organizationResult.Value!.Id));
        }
    }
}

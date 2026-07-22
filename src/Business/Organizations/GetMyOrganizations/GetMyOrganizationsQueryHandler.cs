using Business.Authentication;
using Domain.Common;
using Domain.Organizations;
using Domain.Users.Errors;
using MediatR;

namespace Business.Organizations.GetMyOrganizations
{
    public sealed class GetMyOrganizationsQueryHandler
        : IRequestHandler<
            GetMyOrganizationsQuery,
            Result<IReadOnlyList<OrganizationSummaryResponse>>>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly ICurrentUser currentUser;

        public GetMyOrganizationsQueryHandler(
            IOrganizationRepository organizationRepository,
            ICurrentUser currentUser)
        {
            this.organizationRepository = organizationRepository;
            this.currentUser = currentUser;
        }

        public async Task<Result<IReadOnlyList<OrganizationSummaryResponse>>> Handle(
            GetMyOrganizationsQuery request,
            CancellationToken cancellationToken)
        {
            if (currentUser.UserId is null)
            {
                return Result<IReadOnlyList<OrganizationSummaryResponse>>.Failure(
                    AuthenticationErrors.Unauthorized);
            }

            var specification =
                new OrganizationsByMemberSpecification(
                    currentUser.UserId.Value);

            var organizations =
                await organizationRepository.ListAsync(
                    specification,
                    cancellationToken);

            var response =
                organizations
                    .Select(x =>
                        new OrganizationSummaryResponse(
                            x.Id,
                            x.Name.Value))
                    .ToList();

            return Result<IReadOnlyList<OrganizationSummaryResponse>>.Success(response);
        }
    }
}

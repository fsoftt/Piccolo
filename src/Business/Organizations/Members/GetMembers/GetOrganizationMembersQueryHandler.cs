using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using MediatR;

namespace Business.Organizations.Members.GetMembers
{
    public sealed class GetOrganizationMembersQueryHandler
        : IRequestHandler<
            GetOrganizationMembersQuery,
            Result<IReadOnlyCollection<OrganizationMemberResponse>>>
    {
        private readonly IOrganizationRepository organizationRepository;

        public GetOrganizationMembersQueryHandler(
            IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        public async Task<Result<IReadOnlyCollection<OrganizationMemberResponse>>> Handle(
            GetOrganizationMembersQuery request,
            CancellationToken cancellationToken)
        {
            var specification = new OrganizationMembersSpecification(
                request.OrganizationId);

            var organization = await organizationRepository.FirstOrDefaultAsync(
                specification,
                cancellationToken);
            if (organization is null)
            {
                return Result<IReadOnlyCollection<OrganizationMemberResponse>>.Failure(
                    OrganizationErrors.NotFound);
            }

            var response = organization.Members
                .OrderBy(x => x.JoinedAt)
                .Select(x => new OrganizationMemberResponse(
                    x.UserId,
                    x.Role,
                    x.Status,
                    x.JoinedAt))
                .ToList();

            return Result<IReadOnlyCollection<OrganizationMemberResponse>>.Success(
                response);
        }
    }
}

using Ardalis.Specification;
using Domain.Organizations;

namespace Business.Organizations.GetMyOrganizations
{
    public sealed class OrganizationsByMemberSpecification
        : Specification<Organization>
    {
        public OrganizationsByMemberSpecification(Guid userId)
        {
            Query
                .Where(x =>
                    x.Members.Any(m => m.UserId == userId))
                .OrderBy(x => x.Name);
        }
    }
}

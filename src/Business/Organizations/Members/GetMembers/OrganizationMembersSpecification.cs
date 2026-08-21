using Ardalis.Specification;
using Domain.Organizations;

namespace Business.Organizations.Members.GetMembers
{
    public sealed class OrganizationMembersSpecification
        : Specification<Organization>
    {
        public OrganizationMembersSpecification(Guid organizationId)
        {
            Query
                .Where(x => x.Id == organizationId)
                .Include(x => x.Members);
        }
    }
}

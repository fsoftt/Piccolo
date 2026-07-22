using Ardalis.Specification;
using Domain.Organizations;

namespace Business.Organizations.Specifications
{
    public sealed class OrganizationByIdSpecification
        : Specification<Organization>
    {
        public OrganizationByIdSpecification(Guid organizationId)
        {
            Query
                .Where(x => x.Id == organizationId)
                .Include(x => x.Instruments);
        }
    }
}

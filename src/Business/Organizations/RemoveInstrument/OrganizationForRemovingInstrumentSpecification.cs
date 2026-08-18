using Ardalis.Specification;
using Domain.Organizations;

namespace Business.Organizations.RemoveInstrument
{
    public sealed class OrganizationForRemovingInstrumentSpecification
        : Specification<Organization>
    {
        public OrganizationForRemovingInstrumentSpecification(
            Guid organizationId)
        {
            Query
                .Where(x => x.Id == organizationId)
                .Include(x => x.Instruments);
        }
    }
}

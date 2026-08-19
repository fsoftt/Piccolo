using Ardalis.Specification;
using Domain.Organizations;

namespace Business.Organizations.UpdateInstrument
{
    public sealed class OrganizationForUpdatingInstrumentSpecification
        : Specification<Organization>
    {
        public OrganizationForUpdatingInstrumentSpecification(
            Guid organizationId)
        {
            Query
                .Where(x => x.Id == organizationId)
                .Include(x => x.Instruments);
        }
    }
}

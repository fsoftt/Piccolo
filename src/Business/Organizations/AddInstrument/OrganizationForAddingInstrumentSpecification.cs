using Ardalis.Specification;
using Domain.Organizations;

namespace Business.Organizations.AddInstrument
{
    public sealed class OrganizationForAddingInstrumentSpecification
        : Specification<Organization>
    {
        public OrganizationForAddingInstrumentSpecification(
            Guid organizationId)
        {
            Query
                .Where(x => x.Id == organizationId)
                .Include(x => x.Instruments);
        }
    }
}

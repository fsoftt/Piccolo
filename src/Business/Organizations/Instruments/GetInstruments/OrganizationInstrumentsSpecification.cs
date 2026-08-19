using Ardalis.Specification;
using Domain.Organizations;

namespace Business.Organizations.Instruments.GetInstruments
{
    public sealed class OrganizationInstrumentsSpecification
        : Specification<Organization>
    {
        public OrganizationInstrumentsSpecification(Guid organizationId)
        {
            Query
                .Where(x => x.Id == organizationId)
                .Include(x => x.Instruments)
                .OrderBy(x => x.Name);
        }
    }
}

using Domain.Common;
using Domain.Instruments;

namespace Domain.Organizations
{
    public sealed class OrganizationInstrument : Entity
    {
        private OrganizationInstrument()
        {
        }

        internal OrganizationInstrument(
            Guid id,
            Guid organizationId,
            string name,
            InstrumentFamily family,
            Guid? instrumentDefinitionId)
        {
            Name = name;
            Family = family;
            OrganizationId = organizationId;
            InstrumentDefinitionId = instrumentDefinitionId;
        }

        public string Name { get; private set; }

        public InstrumentFamily Family { get; private set; }

        public Guid? InstrumentDefinitionId { get; private set; }

        public Guid OrganizationId { get; private set; }

        internal void Rename(string name)
        {
            Name = name;
        }
    }
}

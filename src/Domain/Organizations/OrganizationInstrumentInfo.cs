using Domain.Instruments;

namespace Domain.Organizations
{
    public sealed record OrganizationInstrumentInfo(
        string Name,
        InstrumentFamily Family,
        Guid? InstrumentDefinitionId);
}

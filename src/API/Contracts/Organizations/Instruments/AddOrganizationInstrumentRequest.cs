using Domain.Instruments;

namespace API.Contracts.Organizations.Instruments
{
    public sealed record AddOrganizationInstrumentRequest(
        string Name,
        InstrumentFamily Family,
        Guid? InstrumentDefinitionId);
}

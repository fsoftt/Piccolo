using Domain.Instruments;

namespace API.Contracts.Organizations.Instruments
{
    public sealed record UpdateOrganizationInstrumentRequest(
        string Name,
        InstrumentFamily Family,
        Guid? InstrumentDefinitionId);
}

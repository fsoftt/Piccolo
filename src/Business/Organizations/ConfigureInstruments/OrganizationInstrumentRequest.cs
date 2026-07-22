using Domain.Instruments;

namespace Business.Organizations.ConfigureInstruments
{
    public sealed record OrganizationInstrumentRequest(
        Guid? InstrumentDefinitionId,
        string Name,
        InstrumentFamily Family);
}

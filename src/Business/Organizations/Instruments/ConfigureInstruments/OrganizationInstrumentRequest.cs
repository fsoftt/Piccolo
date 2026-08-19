using Domain.Instruments;

namespace Business.Organizations.Instruments.ConfigureInstruments
{
    public sealed record OrganizationInstrumentRequest(
        Guid? InstrumentDefinitionId,
        string Name,
        InstrumentFamily Family);
}

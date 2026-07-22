using Domain.Instruments;

namespace Business.InstrumentDefinitions.GetInstrumentDefinitions
{
    public sealed record InstrumentDefinitionResponse(
        Guid Id,
        string Name,
        InstrumentFamily Family);
}

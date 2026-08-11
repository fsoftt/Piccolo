using Domain.Instruments;

namespace Business.Organizations.GetInstruments
{
    public sealed record OrganizationInstrumentResponse(
        Guid Id,
        string Name,
        InstrumentFamily Family,
        Guid? InstrumentDefinitionId);
}

using Domain.Instruments;

namespace API.Contracts.Organizations
{
    public sealed record AddOrganizationInstrumentRequest(
        string Name,
        InstrumentFamily Family,
        Guid? InstrumentDefinitionId);
}

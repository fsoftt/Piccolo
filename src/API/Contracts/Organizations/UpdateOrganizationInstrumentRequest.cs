using Domain.Instruments;

namespace API.Contracts.Organizations
{
    public sealed record UpdateOrganizationInstrumentRequest(
        string Name,
        InstrumentFamily Family,
        Guid? InstrumentDefinitionId);
}

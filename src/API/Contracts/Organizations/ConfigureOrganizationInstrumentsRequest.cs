using Business.Organizations.ConfigureInstruments;

namespace API.Contracts.Organizations
{
    public sealed record ConfigureOrganizationInstrumentsRequest(
        IReadOnlyCollection<OrganizationInstrumentRequest> Instruments);
}

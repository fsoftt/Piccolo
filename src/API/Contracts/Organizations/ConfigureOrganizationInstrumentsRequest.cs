using Business.Organizations.Instruments.ConfigureInstruments;

namespace API.Contracts.Organizations
{
    public sealed record ConfigureOrganizationInstrumentsRequest(
        IReadOnlyCollection<OrganizationInstrumentRequest> Instruments);
}

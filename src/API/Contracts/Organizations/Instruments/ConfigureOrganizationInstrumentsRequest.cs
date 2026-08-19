using Business.Organizations.Instruments.ConfigureInstruments;

namespace API.Contracts.Organizations.Instruments
{
    public sealed record ConfigureOrganizationInstrumentsRequest(
        IReadOnlyCollection<OrganizationInstrumentRequest> Instruments);
}

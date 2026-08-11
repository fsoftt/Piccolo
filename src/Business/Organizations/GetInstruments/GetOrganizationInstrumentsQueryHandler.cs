using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using MediatR;

namespace Business.Organizations.GetInstruments
{
    public sealed class GetOrganizationInstrumentsQueryHandler
        : IRequestHandler<
            GetOrganizationInstrumentsQuery,
            Result<IReadOnlyCollection<OrganizationInstrumentResponse>>>
    {
        private readonly IOrganizationRepository organizationRepository;

        public GetOrganizationInstrumentsQueryHandler(
            IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        public async Task<Result<IReadOnlyCollection<OrganizationInstrumentResponse>>> Handle(
            GetOrganizationInstrumentsQuery request,
            CancellationToken cancellationToken)
        {
            var specification = new OrganizationInstrumentsSpecification(
                request.OrganizationId);

            var organization = await organizationRepository.FirstOrDefaultAsync(
                specification,
                cancellationToken);

            if (organization is null)
            {
                return Result<IReadOnlyCollection<OrganizationInstrumentResponse>>.Failure(
                    OrganizationErrors.NotFound);
            }

            var response = organization.Instruments
                .OrderBy(x => x.Name)
                .Select(x => new OrganizationInstrumentResponse(
                    x.Id,
                    x.Name,
                    x.Family,
                    x.InstrumentDefinitionId))
                .ToList();

            return Result<IReadOnlyCollection<OrganizationInstrumentResponse>>.Success(
                response);
        }
    }
}

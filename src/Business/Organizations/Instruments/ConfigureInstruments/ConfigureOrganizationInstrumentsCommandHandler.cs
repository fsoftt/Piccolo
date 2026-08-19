using Business.Abstractions.Persistence;
using Business.Organizations.Specifications;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using MediatR;

namespace Business.Organizations.Instruments.ConfigureInstruments
{
    public sealed class ConfigureOrganizationInstrumentsCommandHandler
        : IRequestHandler<ConfigureOrganizationInstrumentsCommand, Result>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IUnitOfWork unitOfWork;

        public ConfigureOrganizationInstrumentsCommandHandler(
            IOrganizationRepository organizationRepository,
            IUnitOfWork unitOfWork)
        {
            this.organizationRepository = organizationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            ConfigureOrganizationInstrumentsCommand request,
            CancellationToken cancellationToken)
        {
            var specification = new OrganizationByIdSpecification(
                request.OrganizationId);

            var organization = await organizationRepository.FirstOrDefaultAsync(
                specification,
                cancellationToken);
            if (organization is null)
            {
                return Result.Failure(OrganizationErrors.NotFound);
            }

            var instruments = request.Instruments
                .Select(x => new OrganizationInstrumentInfo(
                    x.Name,
                    x.Family,
                    x.InstrumentDefinitionId))
                .ToList();

            var result = organization.ConfigureInstruments(instruments);
            if (result.IsFailure)
            {
                return result;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

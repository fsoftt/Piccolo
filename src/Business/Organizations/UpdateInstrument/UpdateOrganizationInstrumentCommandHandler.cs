using Business.Abstractions.Persistence;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using MediatR;

namespace Business.Organizations.UpdateInstrument
{
    public sealed class UpdateOrganizationInstrumentCommandHandler
        : IRequestHandler<UpdateOrganizationInstrumentCommand, Result>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateOrganizationInstrumentCommandHandler(
            IOrganizationRepository organizationRepository,
            IUnitOfWork unitOfWork)
        {
            this.organizationRepository = organizationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateOrganizationInstrumentCommand request,
            CancellationToken cancellationToken)
        {
            var specification =
                new OrganizationForUpdatingInstrumentSpecification(
                    request.OrganizationId);

            var organization = await organizationRepository.FirstOrDefaultAsync(
                specification,
                cancellationToken);
            if (organization is null)
            {
                return Result.Failure(
                    OrganizationErrors.NotFound);
            }

            var result = organization.UpdateInstrument(
                request.InstrumentId,
                request.Name,
                request.Family,
                request.InstrumentDefinitionId);
            if (result.IsFailure)
            {
                return result;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

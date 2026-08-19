using Business.Abstractions.Persistence;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using MediatR;

namespace Business.Organizations.Instruments.RemoveInstrument
{
    public sealed class RemoveOrganizationInstrumentCommandHandler
        : IRequestHandler<RemoveOrganizationInstrumentCommand, Result>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IUnitOfWork unitOfWork;

        public RemoveOrganizationInstrumentCommandHandler(
            IOrganizationRepository organizationRepository,
            IUnitOfWork unitOfWork)
        {
            this.organizationRepository = organizationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            RemoveOrganizationInstrumentCommand request,
            CancellationToken cancellationToken)
        {
            var specification =
                new OrganizationForRemovingInstrumentSpecification(
                    request.OrganizationId);

            var organization = await organizationRepository.FirstOrDefaultAsync(
                specification,
                cancellationToken);
            if (organization is null)
            {
                return Result.Failure(OrganizationErrors.NotFound);
            }

            var result = organization.RemoveInstrument(
                request.InstrumentId);
            if (result.IsFailure)
            {
                return result;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

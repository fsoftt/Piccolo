using Business.Abstractions.Persistence;
using Business.Organizations.Instruments.AddInstrument;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using MediatR;

namespace Business.Organizations.Instruments.AddInstrument
{
    public sealed class AddOrganizationInstrumentCommandHandler
        : IRequestHandler<AddOrganizationInstrumentCommand, Result<Guid>>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IUnitOfWork unitOfWork;

        public AddOrganizationInstrumentCommandHandler(
            IOrganizationRepository organizationRepository,
            IUnitOfWork unitOfWork)
        {
            this.organizationRepository = organizationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            AddOrganizationInstrumentCommand request,
            CancellationToken cancellationToken)
        {
            var specification =
                new OrganizationForAddingInstrumentSpecification(
                    request.OrganizationId);

            var organization = await organizationRepository.FirstOrDefaultAsync(
                specification,
                cancellationToken);
            if (organization is null)
            {
                return Result<Guid>.Failure(
                    OrganizationErrors.NotFound);
            }

            var result = organization.AddInstrument(
                request.Name,
                request.Family,
                request.InstrumentDefinitionId);
            if (result.IsFailure)
            {
                return Result<Guid>.Failure(result.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result< Guid>.Success(result.Value!.Id);
        }
    }
}

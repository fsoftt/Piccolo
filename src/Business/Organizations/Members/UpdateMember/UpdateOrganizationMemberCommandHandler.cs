using Business.Abstractions.Persistence;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using MediatR;
using Business.Organizations.Specifications;

namespace Business.Organizations.Members.UpdateMember
{
    public sealed class UpdateOrganizationMemberCommandHandler
        : IRequestHandler<UpdateOrganizationMemberCommand, Result>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateOrganizationMemberCommandHandler(
            IOrganizationRepository organizationRepository,
            IUnitOfWork unitOfWork)
        {
            this.organizationRepository = organizationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateOrganizationMemberCommand request,
            CancellationToken cancellationToken)
        {
            var specification = new OrganizationByIdSpecification(request.OrganizationId);

            var organization = await organizationRepository.FirstOrDefaultAsync(
                specification,
                cancellationToken);
            if (organization is null)
            {
                return Result.Failure(OrganizationErrors.NotFound);
            }

            var result = organization.UpdateMemberStatus(request.UserId, request.Status);
            if (result.IsFailure)
            {
                return result;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

using Business.Abstractions.Persistence;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using MediatR;
using Business.Organizations.Specifications;

namespace Business.Organizations.Members.UpdateRole
{
    public sealed class UpdateOrganizationMemberRoleCommandHandler
        : IRequestHandler<UpdateOrganizationMemberRoleCommand, Result>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateOrganizationMemberRoleCommandHandler(
            IOrganizationRepository organizationRepository,
            IUnitOfWork unitOfWork)
        {
            this.organizationRepository = organizationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateOrganizationMemberRoleCommand request,
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

            var result = organization.UpdateMemberRole(request.UserId, request.Role);
            if (result.IsFailure)
            {
                return result;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

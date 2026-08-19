using Business.Abstractions.Persistence;
using Business.Organizations.Specifications;
using Domain.Common;
using Domain.Organizations;
using Domain.Organizations.Errors;
using MediatR;

namespace Business.Organizations.Members.AddMember
{
    public sealed class AddMemberCommandHandler
        : IRequestHandler<AddMemberCommand, Result<AddMemberResponse>>
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly IUnitOfWork unitOfWork;

        public AddMemberCommandHandler(
            IOrganizationRepository organizationRepository,
            IUnitOfWork unitOfWork)
        {
            this.organizationRepository = organizationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<AddMemberResponse>> Handle(
            AddMemberCommand request,
            CancellationToken cancellationToken)
        {
            var organization = await organizationRepository.FirstOrDefaultAsync(
                new OrganizationByIdSpecification(request.OrganizationId),
                cancellationToken);
            if (organization is null)
            {
                return Result<AddMemberResponse>.Failure(
                    OrganizationErrors.NotFound);
            }

            var result = organization.AddMember(request.UserId);
            if (result.IsFailure)
            {
                return Result<AddMemberResponse>.Failure(result.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var member = result.Value;

            var response = new AddMemberResponse(
                organization.Id,
                member!.UserId,
                member.Role,
                member.Status,
                member.JoinedAt);

            return Result<AddMemberResponse>.Success(response);
        }
    }
}

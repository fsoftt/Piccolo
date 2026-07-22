using Domain.Common;

namespace Business.Organizations.Policies
{
    public sealed class CreateOrganizationPolicy : ICreateOrganizationPolicy
    {
        public Task<Result> CanCreateAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success());
        }
    }
}

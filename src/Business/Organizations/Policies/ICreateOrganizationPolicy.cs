using Domain.Common;

namespace Business.Organizations.Policies
{
    public interface ICreateOrganizationPolicy
    {
        Task<Result> CanCreateAsync(
            Guid userId,
            CancellationToken cancellationToken);
    }
}

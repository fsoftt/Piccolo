using Domain.Organizations.ValueObjects;

namespace Domain.Organizations
{
    public interface IOrganizationRepository
    {
        Task AddAsync(
            Organization organization,
            CancellationToken cancellationToken);
    }
}

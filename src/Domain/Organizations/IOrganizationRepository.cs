using Ardalis.Specification;

namespace Domain.Organizations
{
    public interface IOrganizationRepository
    {
        Task AddAsync(
            Organization organization,
            CancellationToken cancellationToken);

        Task<Organization?> FirstOrDefaultAsync(
            ISpecification<Organization> specification, 
            CancellationToken cancellationToken);

        Task<IReadOnlyList<Organization>> ListAsync(
            ISpecification<Organization> specification,
            CancellationToken cancellationToken);
    }
}

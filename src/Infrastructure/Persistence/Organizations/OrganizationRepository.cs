using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Organizations
{
    public sealed class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(
            Organization organization,
            CancellationToken cancellationToken)
        {
            await context.Organizations.AddAsync(
                organization,
                cancellationToken);
        }

        public async Task<IReadOnlyList<Organization>> ListAsync(
            ISpecification<Organization> specification,
            CancellationToken cancellationToken)
        {
            return await context.Organizations
                .WithSpecification(specification)
                .ToListAsync(cancellationToken);
        }
    }
}

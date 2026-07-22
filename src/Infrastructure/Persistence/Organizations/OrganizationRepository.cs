using Domain.Organizations;

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
    }
}

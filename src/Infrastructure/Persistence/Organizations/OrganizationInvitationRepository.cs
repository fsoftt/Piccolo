using Domain.Organizations;

namespace Infrastructure.Persistence.Organizations
{
    public sealed class OrganizationInvitationRepository : IOrganizationInvitationRepository
    {
        private readonly ApplicationDbContext context;

        public OrganizationInvitationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(OrganizationInvitation invitation, CancellationToken cancellationToken)
        {
            await context.AddAsync(invitation, cancellationToken);
        }
    }
}

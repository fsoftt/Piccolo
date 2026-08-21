using Domain.Organizations;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IReadOnlyList<OrganizationInvitation>> ListPendingAsync(CancellationToken cancellationToken)
        {
            return await context.OrganizationInvitations
                .Where(x => x.Status == InvitationStatus.Pending && x.ExpiresAt >= DateTime.UtcNow)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(OrganizationInvitation invitation, CancellationToken cancellationToken)
        {
            context.OrganizationInvitations.Update(invitation);
            await Task.CompletedTask;
        }
    }
}

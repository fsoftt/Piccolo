using Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public sealed class OutboxRepository : Business.Abstractions.Persistence.IOutboxRepository
    {
        private readonly ApplicationDbContext context;

        public OutboxRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(string type, string payload, CancellationToken cancellationToken)
        {
            var message = new OutboxMessage(type, payload);
            await context.Set<OutboxMessage>().AddAsync(message, cancellationToken);
        }
    }
}

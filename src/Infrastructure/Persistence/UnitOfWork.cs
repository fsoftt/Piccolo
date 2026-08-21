using Business.Abstractions.Persistence;
using Domain.Common;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private readonly IOutboxRepository outboxRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public UnitOfWork(ApplicationDbContext context, IOutboxRepository outboxRepository)
        {
            this.context = context;
            this.outboxRepository = outboxRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var aggregates = context.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(a => a.DomainEvents != null && a.DomainEvents.Any())
                .ToList();

            foreach (var aggregate in aggregates)
            {
                foreach (var evt in aggregate.DomainEvents)
                {
                    var type = evt.GetType().AssemblyQualifiedName!;
                    var data = System.Text.Json.JsonSerializer.Serialize(evt, evt.GetType());
                    await outboxRepository.AddAsync(type, data, cancellationToken);
                }
            }

            var result = await context.SaveChangesAsync(cancellationToken);

            foreach (var aggregate in aggregates)
            {
                aggregate.ClearDomainEvents();
            }

            return result;
        }
    }
}

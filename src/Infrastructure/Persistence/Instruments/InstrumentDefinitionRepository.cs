using Domain.Instruments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Instruments
{
    public sealed class InstrumentDefinitionRepository
    : IInstrumentDefinitionRepository
    {
        private readonly ApplicationDbContext context;

        public InstrumentDefinitionRepository(
            ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> ExistsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            return await context.InstrumentDefinitions
                .AsNoTracking()
                .AnyAsync(x => ids.Contains(x.Id), cancellationToken);
        }

        public async Task<IReadOnlyList<InstrumentDefinition>> ListAsync(
            CancellationToken cancellationToken)
        {
            return await context.InstrumentDefinitions
                .AsNoTracking()
                .OrderBy(x => x.Family)
                .ThenBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
    }
}

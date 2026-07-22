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

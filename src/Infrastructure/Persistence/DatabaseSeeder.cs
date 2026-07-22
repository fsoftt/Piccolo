using Domain.Instruments;
using Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public sealed class DatabaseSeeder
    {
        private readonly ApplicationDbContext context;

        public DatabaseSeeder(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            await SeedInstrumentDefinitions(cancellationToken);
        }

        private async Task SeedInstrumentDefinitions(
            CancellationToken cancellationToken)
        {
            if (await context.InstrumentDefinitions.AnyAsync(cancellationToken))
            {
                return;
            }

            await context.InstrumentDefinitions.AddRangeAsync(
                InstrumentDefinitionSeed.Create(),
                cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}

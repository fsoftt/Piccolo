using Domain.Instruments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Instruments
{
    public sealed class InstrumentDefinitionConfiguration
        : IEntityTypeConfiguration<InstrumentDefinition>
    {
        public void Configure(EntityTypeBuilder<InstrumentDefinition> builder)
        {
            builder.ToTable("InstrumentDefinitions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Family)
                .HasConversion<int>();
        }
    }
}

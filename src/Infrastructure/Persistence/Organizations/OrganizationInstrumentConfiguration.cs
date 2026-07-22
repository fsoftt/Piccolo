using Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Organizations
{
    public sealed class OrganizationInstrumentConfiguration
        : IEntityTypeConfiguration<OrganizationInstrument>
    {
        public void Configure(
            EntityTypeBuilder<OrganizationInstrument> builder)
        {
            builder.ToTable("OrganizationInstruments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Family)
                .HasConversion<int>();

            builder.Property(x => x.InstrumentDefinitionId);

            builder.HasIndex(x => x.OrganizationId);
        }
    }
}

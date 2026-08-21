using Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OccurredAt)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Payload)
                .IsRequired();

            builder.Property(x => x.Error)
                .HasMaxLength(2000);

            builder.Property(x => x.Attempts)
                .IsRequired();

            builder.Property(x => x.Processed)
                .IsRequired();

            builder.Property(x => x.ProcessedAt);
        }
    }
}

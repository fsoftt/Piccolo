using Domain.Organizations;
using Domain.Organizations.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Persistence.Organizations
{
    public sealed class OrganizationConfiguration
    : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organizations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasConversion(
                    x => x.Value,
                    value => OrganizationName.Create(value).Value!)
                .HasMaxLength(OrganizationName.MaxLength)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasMany(x => x.Members)
                .WithOne()
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Instruments)
                .WithOne()
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Instruments)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

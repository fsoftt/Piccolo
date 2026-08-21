using Domain.Organizations;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Organizations
{
    public sealed class OrganizationInvitationConfiguration : IEntityTypeConfiguration<OrganizationInvitation>
    {
        public void Configure(EntityTypeBuilder<OrganizationInvitation> builder)
        {
            builder.ToTable("OrganizationInvitations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrganizationId)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value).Value!)
                .IsRequired()
                .HasMaxLength(320);

            builder.Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.ExpiresAt)
                .IsRequired();
        }
    }
}

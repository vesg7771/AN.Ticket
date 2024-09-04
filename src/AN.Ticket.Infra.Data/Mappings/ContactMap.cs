using AN.Ticket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Ticket.Infra.Data.Mappings;
internal class ContactMap : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .HasMaxLength(100);

        builder.Property(c => c.PrimaryEmail)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.SecondaryEmail)
            .HasMaxLength(100);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.Property(c => c.Mobile)
            .HasMaxLength(20);

        builder.Property(c => c.Department)
            .HasMaxLength(100);

        builder.Property(c => c.Title)
            .HasMaxLength(250);

        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        builder.ToTable("Contacts");
    }
}

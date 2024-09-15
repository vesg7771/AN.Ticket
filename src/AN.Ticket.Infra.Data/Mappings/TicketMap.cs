using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Infra.Data.Mappings;
public class TicketMap : IEntityTypeConfiguration<DomainEntity.Ticket>
{
    public void Configure(EntityTypeBuilder<DomainEntity.Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.ContactName)
            .HasMaxLength(150);

        builder.Property(t => t.AccountName)
            .HasMaxLength(150);

        builder.Property(t => t.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.Phone)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(t => t.Subject)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.EmailMessageId)
            .IsRequired(false);

        builder.Property(t => t.Status)
            .IsRequired();

        builder.Property(t => t.UserId)
            .IsRequired(false);

        builder.Property(t => t.DueDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(t => t.Priority)
            .IsRequired();

        builder.Property(t => t.Classification)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(t => t.Resolution)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(t => t.Activities)
            .WithOne()
            .HasForeignKey(x => x.TicketId);

        builder.HasMany(t => t.InteractionHistories)
            .WithOne()
            .HasForeignKey(x => x.TicketId);

        builder.HasMany(t => t.Attachments)
            .WithOne(a => a.Ticket)
            .HasForeignKey(a => a.TicketId);

        builder.OwnsOne(t => t.SatisfactionRating);

        builder.Property(t => t.FirstResponseAt);

        builder.Property(t => t.ClosedAt);

        builder.Property(t => t.TicketCode)
            .IsRequired();

        builder.ToTable("Tickets");
    }
}

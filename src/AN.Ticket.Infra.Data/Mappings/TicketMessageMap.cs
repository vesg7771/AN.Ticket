using AN.Ticket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Ticket.Infra.Data.Mappings;

public class TicketMessageMap
    : IEntityTypeConfiguration<TicketMessage>
{
    public void Configure(EntityTypeBuilder<TicketMessage> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Message)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.UserId)
            .IsRequired(false);

        builder.Property(t => t.SentAt)
            .IsRequired();

        builder.HasOne(t => t.Ticket)
            .WithMany(t => t.Messages)
            .HasForeignKey(t => t.TicketId);

        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);
    }
}

using AN.Ticket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Ticket.Infra.Data.Mappings;
internal class InteractionHistoryMap : IEntityTypeConfiguration<InteractionHistory>
{
    public void Configure(EntityTypeBuilder<InteractionHistory> builder)
    {
        builder.HasKey(ih => ih.Id);

        builder.Property(ih => ih.TicketId)
            .IsRequired();

        builder.Property(ih => ih.InteractionDate)
            .IsRequired();

        builder.Property(ih => ih.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(ih => ih.Ticket)
            .WithMany(t => t.InteractionHistories)
            .HasForeignKey(ih => ih.TicketId)
            .IsRequired();

        builder.ToTable("InteractionHistories");
    }
}

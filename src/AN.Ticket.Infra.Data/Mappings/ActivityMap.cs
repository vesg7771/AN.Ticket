using AN.Ticket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Ticket.Infra.Data.Mappings;
public class ActivityMap : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Type)
            .IsRequired();

        builder.Property(a => a.Description)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(a => a.ScheduledDate)
            .IsRequired();

        builder.Property(a => a.TicketId)
            .IsRequired();

        builder.Property(a => a.Subject)
            .HasMaxLength(200);

        builder.Property(a => a.Duration)
            .IsRequired(false);

        builder.Property(a => a.Priority)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired();

        builder.Property(a => a.ContactId)
            .IsRequired(false);

        builder.HasOne(a => a.Ticket)
            .WithMany(t => t.Activities)
            .HasForeignKey(a => a.TicketId)
            .IsRequired();

        builder.HasOne(a => a.Contact)
            .WithMany()
            .HasForeignKey(a => a.ContactId);

        builder.ToTable("Activities");
    }
}

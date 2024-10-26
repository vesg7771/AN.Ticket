using AN.Ticket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Ticket.Infra.Data.Mappings;

public class SatisfactionRatingMap : IEntityTypeConfiguration<SatisfactionRating>
{
    public void Configure(EntityTypeBuilder<SatisfactionRating> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rating);

        builder.Property(x => x.Comment)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Ticket)
            .WithOne(x => x.SatisfactionRating)
            .HasForeignKey<SatisfactionRating>(x => x.TicketId);

        builder.ToTable("SatisfactionRatings");
    }
}

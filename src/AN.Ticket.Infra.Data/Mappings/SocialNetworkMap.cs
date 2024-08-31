using AN.Ticket.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Ticket.Infra.Data.Mappings;
internal class SocialNetworkMap : IEntityTypeConfiguration<SocialNetwork>
{
    public void Configure(EntityTypeBuilder<SocialNetwork> builder)
    {
        builder.HasKey(sn => sn.Id);

        builder.Property(sn => sn.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(sn => sn.Url)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(sn => sn.Contact)
            .WithMany(c => c.SocialNetworks)
            .HasForeignKey(sn => sn.ContactId)
            .IsRequired();

        builder.ToTable("SocialNetworks");
    }
}

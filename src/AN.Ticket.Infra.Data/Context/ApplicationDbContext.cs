using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.Extensions;
using AN.Ticket.Domain.ValueObjects;
using AN.Ticket.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Infra.Data.Context;
public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    DbSet<Contact> Contacts { get; set; }
    DbSet<SocialNetwork> SocialNetworks { get; set; }
    DbSet<DomainEntity.Ticket> Tickets { get; set; }
    DbSet<SatisfactionRating> SatisfactionRatings { get; set; }
    DbSet<Activity> Activities { get; set; }
    DbSet<InteractionHistory> InteractionHistories { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<Team> Teams { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<PaymentPlan> PaymentPlans { get; set; }
    public DbSet<Attachment> Attachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
            {
                var createdAtProperty = entityType.FindProperty(nameof(EntityBase.CreatedAt));
                var updatedAtProperty = entityType.FindProperty(nameof(EntityBase.UpdatedAt));

                if (createdAtProperty != null && createdAtProperty.ClrType == typeof(DateTime))
                {
                    createdAtProperty.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }

                if (updatedAtProperty != null && updatedAtProperty.ClrType == typeof(DateTime))
                {
                    updatedAtProperty.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is EntityBase && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((EntityBase)entityEntry.Entity).UpdatedAt = DateTime.UtcNow.ToLocal();

            if (entityEntry.State == EntityState.Added)
            {
                ((EntityBase)entityEntry.Entity).CreatedAt = DateTime.UtcNow.ToLocal();
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

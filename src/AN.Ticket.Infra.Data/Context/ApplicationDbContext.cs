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

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<SocialNetwork> SocialNetworks { get; set; }
    public DbSet<DomainEntity.Ticket> Tickets { get; set; }
    public DbSet<SatisfactionRating> SatisfactionRatings { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<InteractionHistory> InteractionHistories { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PaymentPlan> PaymentPlans { get; set; }
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

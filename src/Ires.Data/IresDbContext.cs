using Microsoft.EntityFrameworkCore;

namespace Ires.Data;

public class IresDbContext : DbContext
{
    public DbSet<Person> People { get; set; }

    public DbSet<PersonNote> PeopleNotes { get; set; }

    public IresDbContext(DbContextOptions<IresDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(e =>
        {
            e.Property(p => p.GivenName).IsRequired().HasMaxLength(50);
            e.Property(p => p.FamilyName).IsRequired().HasMaxLength(100);
            e.Property(p => p.Nickname).HasMaxLength(50);
            e.Property(p => p.Gender).IsRequired().HasDefaultValue(Gender.NotSpecified);
            e.Property(p => p.DateOfBirth).IsRequired().HasDefaultValue(DateOnly.MinValue);
            e.HasMany(p => p.Notes)
                .WithOne(p => p.Person)
                .HasForeignKey(p => p.PersonId);
            e.HasKey(p => p.Id);
        });

        modelBuilder.Entity<PersonNote>(e =>
        {
            e.HasKey(n => n.Id);
            e.Property(n => n.Content).IsRequired().HasMaxLength(1000);
        });
    }
}

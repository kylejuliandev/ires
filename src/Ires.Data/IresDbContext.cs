using Microsoft.EntityFrameworkCore;

namespace Ires.Data;

public class IresDbContext : DbContext
{
    public DbSet<Person> People { get; set; }

    public DbSet<PersonNote> PeopleNotes { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<ContactDetail> ContactDetails { get; set; }

    public IresDbContext(DbContextOptions<IresDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.GivenName).IsRequired().HasMaxLength(50);
            e.Property(p => p.FamilyName).IsRequired().HasMaxLength(100);
            e.Property(p => p.Nickname).HasMaxLength(50);
            e.Property(p => p.DateOfBirth).IsRequired().HasDefaultValue(DateOnly.MinValue);
        });

        modelBuilder.Entity<PersonNote>(e =>
        {
            e.HasKey(n => n.Id);
            e.Property(n => n.Content).IsRequired().HasMaxLength(1000);

            e.HasOne(p => p.Person)
                .WithMany(p => p.Notes)
                .HasForeignKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Address>(e =>
        {
            e.HasKey(a => a.Id);

            e.Property(a => a.Type).IsRequired().HasDefaultValue(AddressType.Unspecified);
            e.Property(a => a.Street).IsRequired().HasMaxLength(250);
            e.Property(a => a.City).IsRequired().HasMaxLength(100);
            e.Property(a => a.State).IsRequired().HasMaxLength(100);
            e.Property(a => a.PostalCode).IsRequired().HasMaxLength(20);
            e.Property(a => a.Country).IsRequired().HasMaxLength(100);

            e.HasMany(a => a.People)
                .WithMany(p => p.Addresses)
                .UsingEntity<PersonAddresses>();
        });

        modelBuilder.Entity<ContactDetail>(e =>
        {
            e.HasKey(c => c.Id);

            e.Property(c => c.Type).IsRequired().HasDefaultValue(ContactType.Unspecified);
            e.Property(c => c.Value).IsRequired().HasMaxLength(320);

            e.HasIndex(c => new { c.PersonId, c.Type });

            e.HasOne(c => c.Person)
                .WithMany(p => p.Contacts)
                .HasForeignKey(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class CompanyPortalDbContext : DbContext
{
    public CompanyPortalDbContext(DbContextOptions<CompanyPortalDbContext> options)
        : base(options)
    {
    }

    // Define DbSets for your entities here
    public DbSet<Employee> Employees => Set<Employee>();

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var genderConverter = new ValueConverter<Gender, string>(
            toProvider => toProvider == Gender.M ? "M" : "F",
            fromProvider => fromProvider == "M" ? Gender.M : Gender.F
        );

        modelBuilder.Entity<Employee>()
         .Property(e => e.Gender)
         .HasConversion(genderConverter);

        // Configure entity properties and relationships here if needed
    }
}
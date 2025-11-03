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
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<EmployeeDepartment> EmployeeDepartments => Set<EmployeeDepartment>();

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

        modelBuilder.Entity<Department>()
         .HasKey(d => d.Code);

        modelBuilder.Entity<EmployeeDepartment>(ed =>
        {
            ed.HasKey(e => new { e.EmployeeId, e.DepartmentCode });

            ed.HasOne(e => e.Employee)
              .WithMany(e => e.EmployeeDepartments)
              .HasForeignKey(e => e.EmployeeId)
              .OnDelete(DeleteBehavior.Cascade); // When an Employee is deleted, delete related EmployeeDepartment records

            ed.HasOne(e => e.Department)
              .WithMany(d => d.EmployeeDepartments)
              .HasForeignKey(e => e.DepartmentCode)
              .OnDelete(DeleteBehavior.Cascade); // When a Department is deleted, delete related EmployeeDepartment records
        });
    }
}
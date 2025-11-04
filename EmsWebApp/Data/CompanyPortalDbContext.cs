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
    public DbSet<DepartmentManager> DepartmentManagers => Set<DepartmentManager>();
    public DbSet<EmployeeTitle> EmployeeTitles => Set<EmployeeTitle>();
    public DbSet<EmployeeSalary> EmployeeSalaries => Set<EmployeeSalary>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
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

    modelBuilder.Entity<DepartmentManager>(ed =>
    {
      ed.HasKey(e => new { e.EmployeeId, e.DepartmentCode });

      ed.HasOne(e => e.Employee)
        .WithMany(e => e.DepartmentManager)
        .HasForeignKey(e => e.EmployeeId)
        .OnDelete(DeleteBehavior.Cascade); // When an Employee is deleted, delete related DepartmentManager records

      ed.HasOne(e => e.Department)
        .WithMany(d => d.DepartmentManager)
        .HasForeignKey(e => e.DepartmentCode)
        .OnDelete(DeleteBehavior.Cascade); // When a Department is deleted, delete related DepartmentManager records
    });

    modelBuilder.Entity<EmployeeTitle>(et =>
    { 
      et.HasKey(x => new { x.EmployeeId, x.Title, x.FromDate });
      
      et.HasOne(e => e.Employee)
        .WithMany(e => e.Titles)
        .HasForeignKey(e => e.EmployeeId)
        .OnDelete(DeleteBehavior.Cascade);
    });


    modelBuilder.Entity<EmployeeSalary>(es =>
    {
      es.HasKey(x => new { x.EmployeeId, x.FromDate });
        
      es.HasOne(e => e.Employee)
        .WithMany(e => e.Salaries)
        .HasForeignKey(e => e.EmployeeId)
        .OnDelete(DeleteBehavior.Cascade);
    });
      
  }
}
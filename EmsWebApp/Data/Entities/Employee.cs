public class Employee
{
    public int Id { get; set; }
    public DateTime BirthDate { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime HireDate { get; set; }

    public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; } = new List<EmployeeDepartment>();
    public ICollection<DepartmentManager> DepartmentManager { get; set; } = new List<DepartmentManager>();
    public ICollection<EmployeeTitle> Titles { get; set; } = new List<EmployeeTitle>();
    public ICollection<EmployeeSalary> Salaries { get; set; } = new List<EmployeeSalary>();
}

public enum Gender
{
    M,
    F
}
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
}

public enum Gender
{
    M,
    F
}
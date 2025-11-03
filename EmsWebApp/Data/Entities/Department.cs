public class Department
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; } = new List<EmployeeDepartment>();
    public ICollection<DepartmentManager> DepartmentManager { get; set; } = new List<DepartmentManager>();
}
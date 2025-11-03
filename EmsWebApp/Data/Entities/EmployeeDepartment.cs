public class EmployeeDepartment
{
    public int EmployeeId { get; set; }
    public required Employee Employee { get; set; }
    public required string DepartmentCode { get; set; }
    public required Department Department { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
using Microsoft.EntityFrameworkCore;

[Keyless]
public class EmployeeProfile
{
    public int EmployeeId { get; set; }
    public required string EmployeeName { get; set; }
    public required string Title { get; set; }
    public decimal Salary { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
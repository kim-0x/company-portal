public class EmployeeSalary
{
    public int EmployeeId { get; set; }
    public required Employee Employee { get; set; }
    public decimal Amount { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
public class EmployeeTitle
{
    public int EmployeeId { get; set; }
    public required Employee Employee { get; set; }
    public required string Title { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
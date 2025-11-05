namespace EmsWebApp.Models;
public class EmployeeProfileViewModel
{
    public IEnumerable<EmployeeProfile> Items { get; set; } = Enumerable.Empty<EmployeeProfile>();
}
public class Employee
{
    public int Id { get; set; }
    public DateTime BirthDate { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime HireDate { get; set; }
}

public enum Gender
{
    M,
    F
}
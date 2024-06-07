namespace Models;

public class History
{
    // Setting FK lewat Fluent
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid JobId { get; set; }

    //Cardinality
    public Department? Department { get; set; }
    public Job? Job { get; set; }

    public Employee? Employee { get; set; }
}
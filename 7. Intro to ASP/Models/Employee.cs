using System.ComponentModel.DataAnnotations;

namespace Models;

public class Employee : BaseEntity
{
    [MaxLength(6)]
    public string Nik { get; set; } = string.Empty;
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(50)]
    public string? LastName { get; set; }
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public int Salary { get; set; }
    public float? CommisionPct { get; set; }
    public Guid? ManagerId {get; set;}
    public Guid JobId {get; set;}
    public Guid DepartmentId {get; set;}

    // Cardinality

    // Untuk relasi one to many antara manager dan karyawan biasa
    public ICollection<Employee>? Employees{ get; set; }
    public Employee? Manager { get; set; }

    // Department
    public Department? Department { get; set; }
    public Department? DepartmentManager {get; set;}

    public ICollection<History>? Histories { get; set; }
    public Job? Job { get; set; }

    public User? User {get; set;}
}
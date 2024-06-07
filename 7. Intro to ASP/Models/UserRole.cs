namespace Models;

public class UserRole : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid RoleId { get; set; }

    //Cardinality
    public User? User {get; set;} 
    public Role? Role {get; set;}   
}
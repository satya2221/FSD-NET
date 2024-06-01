namespace ZooStaffNamespace;

public class ZooStaff{
    public int Id { get; set;}
    public string Name { get; set;}
    public string Role { get; set;}

    public ZooStaff (int id, string name, string role)
    {
        Id = id;
        Name = name;
        Role = role;
    }
    public bool IsEligbleAsCaretaker()
    {
        return Role == "AnimalSpecialist";
    }
}
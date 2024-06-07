using System.ComponentModel.DataAnnotations;

namespace Models;

public class Role : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<UserRole>? UserRoles{ get; set; }
}
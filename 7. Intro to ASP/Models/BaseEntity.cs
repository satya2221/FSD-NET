using System.ComponentModel.DataAnnotations;

namespace Models;

public abstract class BaseEntity{

    [Key]
    public Guid Id {get; set;}
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Region : BaseEntity
{
    //[MaxLength(25)]
    [Column("Name", TypeName = "varchar(25)")]
    public string Name {get; set;}  = string.Empty;


    // Cardinality
    public ICollection<Country>? Countries {get; set;}
}
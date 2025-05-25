using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD9_S30916.Models;

[Table("Medicament")]
public class Medicament
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)] 
    public string Name { get; set; }
    [MaxLength(100)] 
    public string Description { get; set; }
    [MaxLength(100)]
    public string Type { get; set; }
    
    public virtual ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; } = new List<Prescription_Medicament>();

}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD9_S30916.Models;
[Table("Patient")]
public class Patient
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)] 
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    
    public DateOnly BirthDate { get; set; }
    
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    
}
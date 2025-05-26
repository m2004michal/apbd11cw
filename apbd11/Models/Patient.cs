using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd11.Models;

public class Patient {
    [Key] 
    public int IdPatient { get; set; }

    [Required] 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Column(TypeName = "date")]
    public DateTime BirthDate { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
}
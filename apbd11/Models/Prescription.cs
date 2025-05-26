using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd11.Models;

public class Prescription {
    [Key]
    public int IdPrescription { get; set; }
    [Column(TypeName = "date")]
    public DateTime Date { get; set; }
    [Column(TypeName = "date")]
    public DateTime DueDate { get; set; }
    [ForeignKey(nameof(Doctor))]
    public int IdDoctor { get; set; }
    public Doctor Doctor { get; set; }
    
    [ForeignKey(nameof(Patient))]
    public int IdPatient { get; set; }
    public Patient Patient { get; set; }
    public ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; }

}
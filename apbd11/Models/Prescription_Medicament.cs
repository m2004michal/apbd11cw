using System.ComponentModel.DataAnnotations.Schema;

namespace apbd11.Models;

public class Prescription_Medicament {
    [ForeignKey(nameof(Medicament))]
    public int IdMedicament { get; set; }
    [ForeignKey(nameof(Prescription))]
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
    
    public Prescription Prescription { get; set; }
    public Medicament Medicament { get; set; }
}
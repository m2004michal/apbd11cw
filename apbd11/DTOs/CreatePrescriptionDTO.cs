using apbd11.Models;

namespace apbd11.DTOs;

public class CreatePrescriptionDTO {
    public PatientDto Patient { get; set; }
    public int IdDoctor { get; set; }
    public List<MedicamentPrescriptionDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}


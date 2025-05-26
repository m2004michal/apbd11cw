namespace apbd11.DTOs;

public class PrescriptionResponse {
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public DoctorDto Doctor { get; set; }
    public List<MedicamentResponse> Medicaments { get; set; }
}
namespace apbd11.DTOs;

public class PatientDetailsDTO {
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<PrescriptionResponse> Prescriptions { get; set; }
}
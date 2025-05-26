using apbd11.DTOs;

namespace Tutorial5.Services;

public interface IDbService
{
    Task createPrescription(CreatePrescriptionDTO createPrescriptionDto);
    Task<PatientDetailsDTO> getPatient(int id);
}
using apbd11.Data;
using apbd11.DTOs;
using apbd11.Models;
using Microsoft.EntityFrameworkCore;

namespace Tutorial5.Services;

public class DbService : IDbService {
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context) {
        _context = context;
    }

    public async Task createPrescription(CreatePrescriptionDTO createPrescriptionDto) {
        if (createPrescriptionDto.Medicaments.Count > 10)
            throw new ArgumentException("Prescription can't contain more than 10 medicaments.");
        if (createPrescriptionDto.DueDate < createPrescriptionDto.Date)
            throw new ArgumentException("DueDate must be later than or equal to Date.");

        var patient = _context.Patients
                          .FirstOrDefault(p =>
                              p.FirstName == createPrescriptionDto.Patient.FirstName &&
                              p.LastName == createPrescriptionDto.Patient.LastName)
                      ?? new Patient {
                          FirstName = createPrescriptionDto.Patient.FirstName,
                          LastName = createPrescriptionDto.Patient.LastName
                      };

        var doctor = _context.Doctors.Find(createPrescriptionDto.IdDoctor) ??
                     throw new ArgumentException("Doctor not found");

        var medicamentIds = createPrescriptionDto.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedIds = _context.Medicaments.Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament).ToList();
        if (existingMedIds.Count != medicamentIds.Count)
            throw new ArgumentException("One or more medicaments do not exist.");

        var prescription = new Prescription {
            Date = createPrescriptionDto.Date,
            DueDate = createPrescriptionDto.DueDate,
            Patient = patient,
            Doctor = doctor,
            PrescriptionMedicaments = createPrescriptionDto.Medicaments.Select(m => new Prescription_Medicament {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

    }
    
    public async Task<PatientDetailsDTO> getPatient(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null) return null;

        return new PatientDetailsDTO
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionResponse
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorDto
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentResponse
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Details
                    }).ToList()
                }).ToList()
        };
    }
    
    
}




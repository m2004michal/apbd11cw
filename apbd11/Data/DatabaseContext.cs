using apbd11.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd11.Data;

public class DatabaseContext : DbContext{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription_Medicament>()
            .HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "fn1", LastName = "ln1", Email = "test@gmail.com"},
            new Doctor { IdDoctor = 2, FirstName = "fn2", LastName = "ln2", Email = "test2@gmail.com" }
        );

        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "med1", Description = "desc1", Type = "t1"},
            new Medicament { IdMedicament = 2, Name = "med2", Description = "desc2", Type = "t2" },
            new Medicament { IdMedicament = 3, Name = "med3", Description = "desc3", Type = "t3" }
        );

        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "n1", LastName = "ln1" }
        );

        modelBuilder.Entity<Prescription>().HasData(
            new Prescription {
                IdPrescription = 1,
                Date = new DateTime(2024, 1, 1),
                DueDate = new DateTime(2024, 12, 31),
                IdPatient = 1,
                IdDoctor = 1
            }
        );

        modelBuilder.Entity<Prescription_Medicament>().HasData(
            new Prescription_Medicament {
                IdPrescription = 1,
                IdMedicament = 1,
                Dose = 2,
                Details = "Po jedzeniu"
            }
        );
    }
}

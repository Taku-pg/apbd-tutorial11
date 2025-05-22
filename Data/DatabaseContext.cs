using apbd_tutorial11.Model;
using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial11.Data;

public class DatabaseContext: DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
        {
            new Doctor { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com" },
            new Doctor { IdDoctor = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@gmail.com" },
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>
        {
            new Patient{IdPatient = 1,FirstName = "Tom", LastName = "Cat",BirthDate = new DateTime(1999,1,1)},
            new Patient{IdPatient = 2,FirstName = "Jann", LastName = "Dog",BirthDate = new DateTime(2008,1,23)},
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
        {
            new Medicament{IdMedicament = 1,Name = "Drag1",Description = "Normal drag",Type = "Drag1"},
            new Medicament{IdMedicament = 2,Name = "Drag2",Description = "Super drag",Type = "Drag2"},
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>
        {
            new Prescription
            {
                IdPrescription = 1, Date = new DateTime(2025, 5, 6),
                DueDate = new DateTime(2025, 5, 18), IdDoctor = 1, IdPatient = 1
            },
            new Prescription
            {
            IdPrescription = 2, Date = new DateTime(2025, 4, 6),
            DueDate = new DateTime(2025, 6, 18), IdDoctor = 2, IdPatient = 2
        }
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament() {IdMedicament = 1,IdPrescription = 1, Does = 10,Details = "For cold"},
            new PrescriptionMedicament() {IdMedicament = 2,IdPrescription = 2, Details = "For ful"},
        });

    }
}
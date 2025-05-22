using System.ComponentModel;
using apbd_tutorial11.Data;
using apbd_tutorial11.DTO;
using apbd_tutorial11.Model;
using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial11.Service;

public class PatientService : IPatientService
{
    private readonly DatabaseContext _context;

    public PatientService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PatientDetailDTO?> GetPatient(int id)
    {
        var patient = await _context.Patients
                        .Where(p=>p.IdPatient==id)
                        .Select(p=>new PatientDetailDTO
                        {
                            IdPatient = p.IdPatient,
                            FirstName = p.FirstName, 
                            LastName = p.LastName, 
                            BirthDate = p.BirthDate,
                            
                            Prescription =p.Prescriptions.Select(pre=> new PrescriptionDetailDTO
                            {
                                IdPrescription = pre.IdPrescription,
                                Date = pre.Date,
                                DueDate = pre.DueDate,
                                Medicament = pre.Medicaments
                                                .Join(_context.Medicaments,
                                                        pm=>pm.IdMedicament,
                                                        m=>m.IdMedicament,
                                                        (pm,m) => new MedicamentDetailDTO
                                                {
                                                    IdMedicament = pm.IdMedicament,
                                                    Does = pm.Does ?? 0,
                                                    Name = m.Name,
                                                    Description = m.Description,    
                                                }).ToList(),
                                Doctor = _context.Doctors
                                        .Where(d=>d.IdDoctor==pre.IdDoctor)
                                        .Select(d=>new DoctorDTO
                                        { IdDoctor = pre.IdDoctor, FirstName = d.FirstName}).First() 
                            }).OrderBy(pre=>pre.Date).ToList()
                        })
                        .FirstOrDefaultAsync();

        return patient;
    }
}
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
                                                .Select(pm=>new MedicamentDetailDTO
                                                {
                                                    IdMedicament = pm.IdMedicament,
                                                    Does = pm.Does,
                                                    Name = _context.Medicaments
                                                            .Where(m=>m.IdMedicament==pm.IdMedicament)
                                                            .Select(m=>m.Name)
                                                            .First(),
                                                    Description = _context.Medicaments
                                                        .Where(m=>m.IdMedicament==pm.IdMedicament)
                                                        .Select(m=>m.Name)
                                                        .First(),    
                                                }).ToList(),
                            }).OrderBy(pre=>pre.Date).ToList()
                        })
                        .FirstOrDefaultAsync();

        return patient;
    }
}
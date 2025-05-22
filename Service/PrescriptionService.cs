using apbd_tutorial11.Data;
using apbd_tutorial11.DTO;
using apbd_tutorial11.Exceptions;
using apbd_tutorial11.Model;
using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial11.Service;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _context;

    public PrescriptionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddPrescription(PrescriptionDTO prescription)
    {
        
        foreach (var medic in prescription.Medicament)
        {
            var med= await _context.Medicaments
                                    .Where(m => m.IdMedicament == medic.IdMedicament)
                                    .FirstOrDefaultAsync();
            if (med == null)
                throw new BadRequestException("Medicament doesn't exist");
        }

        if (prescription.Medicament.Count > 10)
        {
            throw new BadRequestException("Number of different medicament is less than 10");
        }

        if (prescription.DueDate >= prescription.Date)
        {
            throw new BadRequestException("Due date cannot be greater than prescription date");
        }
        
        var patient = await _context.Patients
                                        .Where(p=>p.IdPatient==prescription.Patient.IdPatient)
                                        .FirstOrDefaultAsync();
        if (patient == null) 
        { 
            _context.Patients.Add(new Patient 
            { 
                IdPatient = prescription.Patient.IdPatient, 
                FirstName = prescription.Patient.FirstName, 
                LastName = prescription.Patient.LastName, 
                BirthDate = prescription.Patient.BirthDate,
            });
        }
        
        var newPrescription = new Prescription
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = prescription.Patient.IdPatient,
            IdDoctor = prescription.Doctor.IdDoctor
        };
        
        await _context.Prescriptions.AddAsync(newPrescription);

        

        foreach (var medic in prescription.Medicament)
        {
            await _context.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament
            {
                IdMedicament = medic.IdMedicament,
                IdPrescription = newPrescription.IdPrescription,
                Does = medic.Does,
                Details = medic.Description
            });
        }
        
        await _context.SaveChangesAsync();
    }
}
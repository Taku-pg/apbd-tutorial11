namespace apbd_tutorial11.DTO;

public class PatientDetailDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<PrescriptionDetailDTO> Prescription { get; set; }
}

public class PrescriptionDetailDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentDetailDTO> Medicament { get; set; }
    public DoctorDTO Doctor { get; set; }
}

public class MedicamentDetailDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Does { get; set; }
    public string Description { get; set; }
}
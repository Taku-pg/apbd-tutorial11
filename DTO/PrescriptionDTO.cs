namespace apbd_tutorial11.DTO;

public class PrescriptionDTO
{
    public PatientDTO Patient { get; set; }
    public ICollection<MedicamentDTO> Medicament { get; set; }
    public DoctorDTO Doctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}

public class PatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    public int Does { get; set; }
    public string Description { get; set; }
}

public class DoctorDTO
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}

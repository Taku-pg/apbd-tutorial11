using apbd_tutorial11.DTO;

namespace apbd_tutorial11.Service;

public interface IPatientService
{
    Task<PatientDetailDTO?> GetPatient(int id);
}
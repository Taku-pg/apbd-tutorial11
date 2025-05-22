using apbd_tutorial11.DTO;
using apbd_tutorial11.Model;

namespace apbd_tutorial11.Service;

public interface IPrescriptionService
{
    Task AddPrescription(PrescriptionDTO prescription);
}
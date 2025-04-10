using Hea.Models;

namespace Hea.Service
{
    public interface IConsultationService
    {
        Task<IEnumerable<Consultation>> GetAllConsultations();
        Task<Consultation> GetConsultationById(int id);
        Task<Consultation> AddConsultation(Consultation consultation);
        Task<int> AddConsultationUsingSP(Consultation consultation);
        Task<bool> DeleteConsultation(int id);
        Task SendConsultationNotificationAsync(int appointmentId, int doctorId, string notes, string prescription/*, DateOnly consultationDate*/);
        Task<IEnumerable<Consultation>> GetDoctorConsultationsAsync(int doctorId);
    }
}

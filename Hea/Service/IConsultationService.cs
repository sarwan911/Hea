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
    }
}

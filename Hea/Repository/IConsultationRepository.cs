using Hea.Models;
namespace Hea.Repository
{
    public interface IConsultationRepository
    {

        Task<IEnumerable<Consultation>> GetAllConsultations();
        Task<Consultation> GetConsultationById(int id);
        Task<Consultation> AddConsultation(Consultation consultation);
        Task<bool> DeleteConsultation(int id);
        Task<int> AddConsultationUsingSP(Consultation consultation);
    }
}
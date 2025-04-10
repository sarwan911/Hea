using Hea.Models;
using Hea.Repository;

namespace Hea.Repository
{
    public interface IDocAvailabilityRepository
    {
        Task<IEnumerable<DocAvailability>> GetAllAvailabilities();
        Task<DocAvailability> GetAvailabilityById(int id);
        Task<DocAvailability> AddAvailability(DocAvailability availability);
        Task<bool> DeleteAvailability(int id);
        Task<bool> IsSessionAvailable(int sessionId);

        Task GenerateDoctorAvailabilityAsync(int doctorId, string location, DateOnly availableDate);
        Task DeletePastAvailabilityAsync();
        Task<IEnumerable<DocAvailability>> GetDoctorSessionsAsync(int doctorId);
        Task DeletePastAvailabilityAsync(int doctorId);
    }
}

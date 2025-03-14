using Hea.Models;

namespace Hea.Service
{
    public interface IDocAvailabilityService
    {
        Task<IEnumerable<DocAvailability>> GetAllAvailabilities();
        Task<DocAvailability> GetAvailabilityById(int id);
        Task<DocAvailability> AddAvailability(DocAvailability availability);
        Task<bool> IsSessionAvailable(int sessionId);
        Task<bool> DeleteAvailability(int id);
    }
}

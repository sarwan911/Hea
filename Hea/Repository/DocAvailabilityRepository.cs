using Microsoft.EntityFrameworkCore;
using Hea.Repository;
using Hea;
using Hea.Models;
using Hea.Data;

namespace Hea.Repository
{
    public class DocAvailabilityRepository : IDocAvailabilityRepository
    {
        private readonly Context _context;
        public DocAvailabilityRepository(Context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DocAvailability>> GetAllAvailabilities()
        {
            return await _context.DocAvailabilities.ToListAsync();
        }
        public async Task<DocAvailability> GetAvailabilityById(int id)
        {
            return await _context.DocAvailabilities.FindAsync(id);
        }
        public async Task<DocAvailability> AddAvailability(DocAvailability availability)
        {
            _context.DocAvailabilities.Add(availability);
            await _context.SaveChangesAsync();
            return availability;
        }
        public async Task<bool> IsSessionAvailable(int sessionId)
        {
            var result = await _context.Database.ExecuteSqlRawAsync("EXEC CheckAvailability @p0", sessionId);
            return result == 1;
        }
        public async Task<bool> DeleteAvailability(int id)
        {
            var availability = await _context.DocAvailabilities.FindAsync(id);
            if (availability == null)
                return false;
            _context.DocAvailabilities.Remove(availability);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
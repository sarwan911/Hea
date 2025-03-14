﻿using Hea.Models;
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
    }
}

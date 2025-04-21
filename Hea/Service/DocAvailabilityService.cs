using Hea.Models;
using Hea.Repository;
using NuGet.Protocol.Core.Types;

namespace Hea.Service
{
    public class DocAvailabilityService : IDocAvailabilityService
    {
        private readonly IDocAvailabilityRepository _docAvailabilityRepository;

        public DocAvailabilityService(IDocAvailabilityRepository docAvailabilityRepository)
        {
            _docAvailabilityRepository = docAvailabilityRepository;
        }

        public async Task<IEnumerable<DocAvailability>> GetAllAvailabilities()
        {
            try
            {
                return await _docAvailabilityRepository.GetAllAvailabilities();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while retrieving availabilities.", ex);
            }
        }

        public async Task<DocAvailability> GetAvailabilityById(int id)
        {
            try
            {
                return await _docAvailabilityRepository.GetAvailabilityById(id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while retrieving the availability with ID {id}.", ex);
            }
        }

        public async Task<DocAvailability> AddAvailability(DocAvailability availability)
        {
            try
            {
                return await _docAvailabilityRepository.AddAvailability(availability);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while adding the availability.", ex);
            }
        }

        public async Task<bool> IsSessionAvailable(int sessionId)
        {
            try
            {
                return await _docAvailabilityRepository.IsSessionAvailable(sessionId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while checking session availability.", ex);
            }
        }

        public async Task<bool> DeleteAvailability(int id)
        {
            try
            {
                return await _docAvailabilityRepository.DeleteAvailability(id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while deleting the availability with ID {id}.", ex);
            }
        }
        public async Task GenerateDoctorAvailabilityAsync(int doctorId, string location, DateOnly availableDate)
        {
            try
            {
                await _docAvailabilityRepository.GenerateDoctorAvailabilityAsync(doctorId, location, availableDate);
            }
            catch (Exception)
            {
                throw new Exception($"An error occurred while Generating doctor sessions.");
            }
        }



        public async Task DeletePastAvailabilityAsync()
        {
            try
            {
                await _docAvailabilityRepository.DeletePastAvailabilityAsync();
            }
            catch (Exception)
            {
                throw new Exception($"An error occurred while deleting past doctor sessions.");
            }
        }

        public async Task<IEnumerable<DocAvailability>> GetDoctorSessionsAsync(int doctorId)
        {
            try
            {
                var sessions = await _docAvailabilityRepository.GetDoctorSessionsAsync(doctorId);
                if (!sessions.Any())
                {
                    throw new Exception($"No sessions found for doctor with ID {doctorId}.");
                }
                return sessions;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving sessions for doctor with ID {doctorId}.", ex);
            }
        }
        public async Task DeletePastAvailabilityAsync(int doctorId)
        {
            try
            {
                await _docAvailabilityRepository.DeletePastAvailabilityAsync(doctorId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred in the service layer.", ex);
            }
        }
    }
}
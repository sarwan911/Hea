using Hea.Models;
using Hea.Repository;
using NuGet.Protocol.Core.Types;

namespace Hea.Service
{
    public class ConsultationService : IConsultationService
    {
        private readonly IConsultationRepository _consultationRepository;

        public ConsultationService(IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }

        public async Task<IEnumerable<Consultation>> GetAllConsultations()
        {
            try
            {
                return await _consultationRepository.GetAllConsultations();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while retrieving consultations.", ex);
            }
        }

        public async Task<Consultation> GetConsultationById(int id)
        {
            try
            {
                return await _consultationRepository.GetConsultationById(id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while retrieving the consultation with ID {id}.", ex);
            }
        }

        public async Task<Consultation> AddConsultation(Consultation consultation)
        {
            try
            {
                return await _consultationRepository.AddConsultation(consultation);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while adding the consultation.", ex);
            }
        }

        public async Task<int> AddConsultationUsingSP(Consultation consultation)
        {
            try
            {
                return await _consultationRepository.AddConsultationUsingSP(consultation);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while adding the consultation using stored procedure.", ex);
            }
        }
        public async Task SendConsultationNotificationAsync(int appointmentId, int doctorId, string notes, string prescription, DateOnly consultationDate)
        {
            await _consultationRepository.SendConsultationNotificationAsync(appointmentId, doctorId, notes, prescription, consultationDate);
        }

        public async Task<bool> DeleteConsultation(int id)
        {
            try
            {
                return await _consultationRepository.DeleteConsultation(id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while deleting the consultation with ID {id}.", ex);
            }
        }
    }
}
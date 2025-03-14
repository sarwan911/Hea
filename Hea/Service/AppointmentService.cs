using Hea.Models;
using Hea.Repository;

namespace Hea.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> BookAppointmentAsync(int sessionId, int patientId, string status)
        {
            try
            {
                return await _appointmentRepository.BookAppointmentAsync(sessionId, patientId, status);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while booking the appointment.", ex);
            }
        }

        public async Task<Appointment> RescheduleAppointmentAsync(int appointmentId, int newSessionId)
        {
            try
            {
                return await _appointmentRepository.RescheduleAppointmentAsync(appointmentId, newSessionId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while rescheduling the appointment.", ex);
            }
        }

        public async Task<Appointment> CancelAppointmentAsync(int appointmentId)
        {
            try
            {
                return await _appointmentRepository.CancelAppointmentAsync(appointmentId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while canceling the appointment.", ex);
            }
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            try
            {
                return await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while retrieving the appointment with ID {appointmentId}.", ex);
            }
        }

        public async Task<Appointment> GetByIdAsync(int appointmentId)
        {
            try
            {
                return await _appointmentRepository.GetByIdAsync(appointmentId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while retrieving the appointment with ID {appointmentId}.", ex);
            }
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            try
            {
                return await _appointmentRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while retrieving all appointments.", ex);
            }
        }

        public async Task AddAsync(Appointment appointment)
        {
            try
            {
                await _appointmentRepository.AddAsync(appointment);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while adding the appointment.", ex);
            }
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            try
            {
                await _appointmentRepository.UpdateAsync(appointment);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while updating the appointment.", ex);
            }
        }

        public async Task DeleteAsync(int appointmentId)
        {
            try
            {
                await _appointmentRepository.DeleteAsync(appointmentId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while deleting the appointment with ID {appointmentId}.", ex);
            }
        }

        public async Task<List<Appointment>> GetAppointmentsForPatient(int patientId)
        {
            try
            {
                return await _appointmentRepository.GetAppointmentsForPatient(patientId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while retrieving appointments for patient with ID {patientId}.", ex);
            }
        }

        public async Task<List<Appointment>> GetAppointmentsForDoctor(int doctorId)
        {
            try
            {
                return await _appointmentRepository.GetAppointmentsForDoctor(doctorId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while retrieving appointments for doctor with ID {doctorId}.", ex);
            }
        }
    }
}
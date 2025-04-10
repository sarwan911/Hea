using Hea.Models;
using Hea.Repository;
using NuGet.Protocol.Core.Types;

namespace Hea.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
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
        public async Task UpdateAppointmentAsync(int sessionId, int patientId, string status)
        {
            await _appointmentRepository.UpdateAppointmentAsync(sessionId, patientId, status);
        }
        public async Task<Appointment> RescheduleAppointmentAsync(int appointmentId, int newSessionId)
        {
            await _appointmentRepository.RescheduleAppointmentAsync(appointmentId, newSessionId);
            return await _appointmentRepository.GetByIdAsync(appointmentId);
        }

        public async Task<Appointment> CancelAppointmentAsync(int appointmentId)
        {
            await _appointmentRepository.CancelAppointmentAsync(appointmentId);
            return await _appointmentRepository.GetByIdAsync(appointmentId);
        }
        public async Task<IEnumerable<Appointment>> GetDoctorAppointmentsAsync(int doctorId)
        {
            var appointments = await _appointmentRepository.GetDoctorAppointmentsAsync(doctorId);

            if (!appointments.Any())
            {
                throw new Exception("No appointments booked under this doctor.");
            }

            return appointments;
        }
    }
}

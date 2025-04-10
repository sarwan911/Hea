using Hea.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace Hea.Repository
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetByIdAsync(int appointmentId);
        Task<List<Appointment>> GetAllAsync();
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int appointmentId);
        Task RescheduleAppointmentAsync(int appointmentId, int newSessionId);
        Task CancelAppointmentAsync(int appointmentId);
        Task UpdateAppointmentAsync(int sessionId, int patientId, string status);
        Task<IEnumerable<Appointment>> GetDoctorAppointmentsAsync(int doctorId);
    }
}
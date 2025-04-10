using Hea.Data;
using Microsoft.EntityFrameworkCore;
using Hea.Models;
using Hea.Repository;
using System.Reflection.Metadata;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Hea.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly Context _context;
        public AppointmentRepository(Context context)
        {
            _context = context;
        }

        public async Task<Appointment> GetByIdAsync(int appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAppointmentAsync(int sessionId, int patientId, string status)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateAppointment @p0, @p1, @p2",
                sessionId, patientId, status);
        }
        public async Task RescheduleAppointmentAsync(int appointmentId, int newSessionId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC RescheduleAppointment @p0, @p1",
                appointmentId, newSessionId);
        }

        public async Task CancelAppointmentAsync(int appointmentId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC CancelAppointment @p0",
                appointmentId);
        }
        public async Task<IEnumerable<Appointment>> GetDoctorAppointmentsAsync(int doctorId)
        {
            var appointments = await _context.Appointments
                .Join(_context.DocAvailabilities,
                      a => a.SessionId,
                      d => d.SessionId,
                      (a, d) => new { a, d })
                .Where(ad => ad.d.DoctorId == doctorId && ad.a.Status == "Booked")
                .Select(ad => ad.a)
                .ToListAsync();

            return appointments;
        }
    }
}

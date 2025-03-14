using Hea.Data;
using Microsoft.EntityFrameworkCore;
using Hea.Models;
using Hea.Repository;
using System.Reflection.Metadata;
using System;

namespace Hea.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly Context _context;
        public AppointmentRepository(Context context)
        {
            _context = context;
        }

        public async Task<Appointment> BookAppointmentAsync(int sessionId, int patientId, string status)
        {
            return await _context.Appointments
                .FromSqlRaw("EXEC sp_BookAppointment @p0, @p1, @p2",
                    sessionId, patientId, "Booked")
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Appointment> RescheduleAppointmentAsync(int appointmentId, int newSessionId)
        {
            return await _context.Appointments
                .FromSqlRaw("EXEC sp_RescheduleAppointment @p0, @p1",
                    appointmentId, newSessionId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Appointment> CancelAppointmentAsync(int appointmentId)
        {
            return await _context.Appointments
                .FromSqlRaw("EXEC sp_CancelAppointment @p0",
                    appointmentId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments
                .FromSqlRaw("EXEC sp_GetAppointmentById @p0",
                    appointmentId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
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

        public async Task<List<Appointment>> GetAppointmentsForPatient(int patientId)
        {
            return await _context.Appointments.Where(a => a.PatientId == patientId).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsForDoctor(int doctorId)
        {
            return await _context.Appointments.Where(a => a.SessionId == doctorId).ToListAsync();
        }//an error here
    }
}
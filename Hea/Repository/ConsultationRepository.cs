using Microsoft.EntityFrameworkCore;
using Hea.Data;
using Hea.Repository;
using Hea.Models;

namespace Hea.Repository
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly Context _context;
        public ConsultationRepository(Context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Consultation>> GetAllConsultations()
        {
            return await _context.Consultations.ToListAsync();
        }
        public async Task<Consultation> GetConsultationById(int id)
        {
            return await _context.Consultations.FindAsync(id);
        }
        public async Task<Consultation> AddConsultation(Consultation consultation)
        {
            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync();
            return consultation;
        }
        public async Task<int> AddConsultationUsingSP(Consultation consultation)
        {
            var consultationId = await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddConsultation @p0, @p1, @p2, @p3",
                consultation.ConsultationId, consultation.DoctorId, consultation.Notes, consultation.Prescription);
            return consultationId;
        }
        public async Task<bool> DeleteConsultation(int id)
        {
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null)
                return false;
            _context.Consultations.Remove(consultation);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task SendConsultationNotificationAsync(int appointmentId, int doctorId, string notes, string prescription/*, DateOnly consultationDate*/)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SendConsultationNotification @p0, @p1, @p2, @p3", //,@p4",
                appointmentId, doctorId, notes, prescription/*, consultationDate*/);
        }
        public async Task<IEnumerable<Consultation>> GetDoctorConsultationsAsync(int doctorId)
        {
            return await _context.Consultations
                .FromSqlRaw("EXEC GetDoctorConsultations @p0", doctorId)
                .ToListAsync();
        }
    }
}
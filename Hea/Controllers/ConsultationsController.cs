using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hea.Data;
using Hea.Models;
using Microsoft.AspNetCore.Authorization;
using Hea.Service;
using Microsoft.AspNetCore.Cors;
using static System.Collections.Specialized.BitVector32;

namespace Hea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class ConsultationsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IConsultationService _service;

        public ConsultationsController(Context context, IConsultationService service)
        {
            _context = context;
            _service = service;
        }
        [HttpGet("AppointmentId")]
        public async Task<ActionResult<IEnumerable<Consultation>>> GetConsultations(int appointmentId)
        {
            try
            {
                var consultations = await _context.Consultations
                    .Where(n => n.AppointmentId == appointmentId)
                    .ToListAsync();

                if (consultations == null || !consultations.Any())
                {
                    return NotFound();
                }

                return Ok(consultations);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("patient/{userId}")]
        public async Task<ActionResult<IEnumerable<Consultation>>> GetConsultationsByUserId(int userId)
        {
            var consultations = await _context.Consultations
                .Join(_context.Appointments,
                      consultation => consultation.AppointmentId,
                      appointment => appointment.AppointmentId,
                      (consultation, appointment) => new { consultation, appointment })
                .Where(ca => ca.appointment.PatientId == userId)
                .Select(ca => ca.consultation)
                .ToListAsync();

            if (consultations == null || !consultations.Any())
            {
                return NotFound();
            }

            return Ok(consultations);
        }


        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<Consultation>>> GetDoctorConsultations(int doctorId)
        {
            var consultations = await _service.GetDoctorConsultationsAsync(doctorId);

            if (consultations == null || !consultations.Any())
            {
                return NotFound($"No consultations found for doctor with ID {doctorId}");
            }

            return Ok(consultations);
        }


        // GET: api/Consultations
        [HttpGet]
        //[Authorize(Roles = "Doctor, Patient")]
        public async Task<ActionResult<IEnumerable<Consultation>>> GetConsultations()
        {
            return await _context.Consultations.ToListAsync();
        }

        // GET: api/Consultations/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Doctor, Patient")]
        public async Task<ActionResult<Consultation>> GetConsultation(int id)
        {
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
            {
                return NotFound();
            }

            return consultation;
        }

        // PUT: api/Consultations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "Doctor")]
        public async Task<IActionResult> PutConsultation(int id, Consultation consultation)
        {
            if (id != consultation.ConsultationId)
            {
                return BadRequest();
            }

            _context.Entry(consultation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Consultations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Doctor")]
        public async Task<ActionResult<Consultation>> PostConsultation(Consultation consultation)
        {
            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsultation", new { id = consultation.ConsultationId }, consultation);
        }

        // DELETE: api/Consultations/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Doctor")]

        public async Task<IActionResult> DeleteConsultation(int id)
        {
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            _context.Consultations.Remove(consultation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("Consultation")]
        //[Authorize(Roles = "Doctor")]
        public async Task<IActionResult> SendConsultationNotification(int appointmentId, int doctorId, string notes, string prescription/*, DateOnly consultationDate*/)
        {
            await _service.SendConsultationNotificationAsync(appointmentId, doctorId, notes, prescription/*, consultationDate*/);
            return Ok();
        }

        private bool ConsultationExists(int id)
        {
            return _context.Consultations.Any(e => e.ConsultationId == id);
        }
    }
}

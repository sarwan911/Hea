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
using static System.Collections.Specialized.BitVector32;
using Microsoft.AspNetCore.Cors;

namespace Hea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class AppointmentsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IAppointmentService _service;

        public AppointmentsController(Context context, IAppointmentService service)
        {
            _context = context;
            _service = service;
        }

        //GET: api/Appointments
        [HttpGet]
       [Authorize]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        [HttpGet("patient/{userId}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByUserId(int userId)
        {
            var appointments = await _context.Appointments
            .Where(a => a.PatientId == userId)
            .ToListAsync();

            if (appointments == null || !appointments.Any())
            {
                return NotFound();
            }

            return Ok(appointments);
        }
        // GET: api/Appointments/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor, Patient")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.AppointmentId }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor, Patient")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }

        [HttpPost("Book_Appointment")]
        [Authorize(Roles = "Doctor, Patient")]
        public async Task<IActionResult> UpdateAppointment(int sessionId, int patientId, string status)
        {
            await _service.UpdateAppointmentAsync(sessionId, patientId, status);
            return Ok();
        }
        [HttpPost("Reschedule_Appointment")]
        [Authorize(Roles = "Doctor, Patient")]
        public async Task<IActionResult> RescheduleAppointment(int appointmentId, int newSessionId)
        {
            var appointment = await _service.RescheduleAppointmentAsync(appointmentId, newSessionId);
            return Ok(appointment);
        }

        [HttpPost("Cancel_Appointment")]
        [Authorize(Roles= "Doctor, Patient")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            var appointment = await _service.CancelAppointmentAsync(appointmentId);
            return Ok(appointment);
        }
        [HttpGet("Appointments_for_Doctor")]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentss()
        {
            // Get the logged-in doctor's ID from the claims
            var doctorId = User.Identity.Name;

            // Convert doctorId to integer
            int docId = int.Parse(doctorId);

            try
            {
                var appointments = await _service.GetDoctorAppointmentsAsync(docId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
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

namespace Hea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocAvailabilitiesController : ControllerBase
    {
        private readonly Context _context;
        private readonly IDocAvailabilityService _service;

        public DocAvailabilitiesController(Context context, IDocAvailabilityService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/DocAvailabilities
        [HttpGet]
        [Authorize(Roles ="Doctor, Patient")]
        public async Task<ActionResult<IEnumerable<DocAvailability>>> GetDocAvailabilities()
        {
            return await _context.DocAvailabilities.ToListAsync();
        }
        [HttpPost("generate")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GenerateDoctorAvailability(int doctorId, string location, DateOnly availableDate)
        {
            await _service.GenerateDoctorAvailabilityAsync(doctorId, location, availableDate);
            return Ok();
        }

        [HttpDelete("delete-past")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeletePastAvailability()
        {
            await _service.DeletePastAvailabilityAsync();
            return Ok();
        }
        // GET: api/DocAvailabilities/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Doctor, Patient")]
        public async Task<ActionResult<DocAvailability>> GetDocAvailability(int id)
        {
            var docAvailability = await _context.DocAvailabilities.FindAsync(id);

            if (docAvailability == null)
            {
                return NotFound();
            }

            return docAvailability;
        }

        // PUT: api/DocAvailabilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> PutDocAvailability(int id, DocAvailability docAvailability)
        {
            if (id != docAvailability.SessionId)
            {
                return BadRequest();
            }

            _context.Entry(docAvailability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocAvailabilityExists(id))
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

        // POST: api/DocAvailabilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<DocAvailability>> PostDocAvailability(DocAvailability docAvailability)
        {
            _context.DocAvailabilities.Add(docAvailability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocAvailability", new { id = docAvailability.SessionId }, docAvailability);
        }

        // DELETE: api/DocAvailabilities/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteDocAvailability(int id)
        {
            var docAvailability = await _context.DocAvailabilities.FindAsync(id);
            if (docAvailability == null)
            {
                return NotFound();
            }

            _context.DocAvailabilities.Remove(docAvailability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocAvailabilityExists(int id)
        {
            return _context.DocAvailabilities.Any(e => e.SessionId == id);
        }
    }
}

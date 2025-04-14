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
using Microsoft.AspNetCore.Cors;

namespace Hea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class NotificationsController : ControllerBase
    {
        private readonly Context _context;

        public NotificationsController(Context context)
        {
            _context = context;
        }

        [HttpGet("UserId")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications(int userId)
        {
            try
            {
                var notifications = await _context.Notifications
                    .Where(n => n.UserId == userId)
                    .ToListAsync();

                if (notifications == null || !notifications.Any())
                {
                    return NotFound();
                }

                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        //// GET: api/Notifications
        //[HttpGet]
        ////[Authorize]
        //public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        //{
        //    return await _context.Notifications.ToListAsync();
        //}

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> PutNotification(int id, Notification notification)
        {
            if (id != notification.NotificationId)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotification", new { id = notification.NotificationId }, notification);
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.NotificationId == id);
        }
    }
}

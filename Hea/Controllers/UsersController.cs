using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hea.Models;
using Hea.Data;
using Microsoft.AspNetCore.Authorization;
using Hea.Repository;
using Hea.Service;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;

namespace Hea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IAuth _Auth;

        public UsersController(Context context, IUserRepository userRepository, IUserService _userService, IAuth Auth)
        {
            this._context = context;
            this._userRepository = userRepository;
            this._userService = _userService;
            this._Auth = Auth;
        }

        // GET: api/Users
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        // GET: api/Users/5
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("Doctors")]
        public IActionResult GetDoctors()
        {
            var doctors = _userRepository.GetAllDoctors();
            return Ok(doctors);
        }

        // GET: api/User/Patients
        [HttpGet("Patients")]
        public IActionResult GetPatients()
        {
            var patients = _userRepository.GetAllPatients();
            return Ok(patients);
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        [AllowAnonymous]
        // POST api/<UsersController>/authentication
        [HttpPost("authentication")]
        public IActionResult Authentication(int user_id, string password)
        {
            var token = _Auth.Authentication(user_id.ToString(), password);
            if (token == null)
                return Unauthorized();
            return Ok(new { Token = token });
        }
    }
}


using Hea.Controllers;
using Hea.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsPolicy")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        //public IActionResult Login([FromBody] LoginModel login)
        //{
        //    var token = _authService.Authentication(login.Username.ToString(), login.Password);
        //    if (token == null)
        //    {
        //        return Unauthorized(new { message = "Invalid credentials" });
        //    }
        //    return Ok(new { token });
        //}
        public IActionResult Login([FromBody] LoginModel login)
        {
            try
            {
                Console.WriteLine($"Login attempt for user {login.Username}");
                var token = _authService.Authentication(login.Username.ToString(), login.Password);
                if (token == null)
                {
                    Console.WriteLine("Invalid credentials");
                    return Unauthorized(new { message = "Invalid credentials" });
                }
                var user = _authService.GetUser(login.Username.ToString());
                Console.WriteLine($"User found: {user?.UserId}, Role: {user?.Role}");
                return Ok(new
                {
                    token,
                    userId = login.Username,
                    role = user?.Role
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex}");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }

    public class LoginModel
    {
        public int Username { get; set; }
        public string Password { get; set; }
    }
}
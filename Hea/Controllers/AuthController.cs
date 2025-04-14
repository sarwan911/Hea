using Hea.Service;
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var token = _authService.Authentication(login.Username.ToString(), login.Password);
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            return Ok(new { token });
        }
    }

    public class LoginModel
    {
        public int Username { get; set; }
        public string Password { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using AnimalRecognizer;
using AnimalRecognizer.Model;
using AnimalRecognizer.Data;

namespace AnimalRecognizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserContext _context;

        public RegisterController(UserContext context)
        {
            _context = context;
        }

        // POST api/<RegisterController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            // Check if user already exists
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return Conflict("User already exists");
            }

            // Save user to database
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully", name = user.Name });
        }
    }
}

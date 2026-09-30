using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;



namespace ArrayLabApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetupController : ControllerBase
    {
        // GET: /api/setup/hello
        // Task 1: Confirms the environment is running
        [HttpGet("hello")]
        public IActionResult Hello()
        {
            var info = new
            {
                environment = ".NET 9 Web API",
                status = "OK",
                timestamp = DateTime.UtcNow,
                studentName = "<Your Name>",
                registrationNumber = "<Your Registration Number>"
            };
            return Ok(ApiResponse<object>.Ok(info, "Environment is ready."));
        }

        // POST: /api/setup/register
        // Task 1: Accept name + reg number and echo them back
        [HttpPost("register")]
        public IActionResult Register([FromBody] StudentInfoRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Name) ||
                string.IsNullOrWhiteSpace(req.RegistrationNumber))
                return BadRequest(ApiResponse<object>.Fail("Name and registration number are required."));

            return Ok(ApiResponse<object>.Ok(
                new { req.Name, req.RegistrationNumber },
                $"Hello {req.Name} ({req.RegistrationNumber}) — environment verified."));
        }


       
         }
}
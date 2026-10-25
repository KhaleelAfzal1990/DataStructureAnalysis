using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursionController : ControllerBase
    {
        private readonly RecursionService _svc;
        public RecursionController(RecursionService svc) => _svc = svc;

        // GET: /api/recursion/factorial/6
        [HttpGet("factorial/{n}")]
        public IActionResult Factorial(int n)
        {
            try { return Ok(ApiResponse<long>.Ok(_svc.Factorial(n))); }
            catch (ArgumentException ex) { return BadRequest(ApiResponse<long>.Fail(ex.Message)); }
        }

        // GET: /api/recursion/fibonacci/8
        [HttpGet("fibonacci/{n}")]
        public IActionResult Fibonacci(int n)
        {
            try { return Ok(ApiResponse<int>.Ok(_svc.Fibonacci(n))); }
            catch (ArgumentException ex) { return BadRequest(ApiResponse<int>.Fail(ex.Message)); }
        }

        // GET: /api/recursion/sum-of-digits/1234
        [HttpGet("sum-of-digits/{n}")]
        public IActionResult SumOfDigits(int n)
        {
            return Ok(ApiResponse<int>.Ok(_svc.SumOfDigits(n)));
        }

        // POST: /api/recursion/palindrome
        [HttpPost("palindrome")]
        public IActionResult Palindrome([FromBody] StringRequest req)
        {
            if (string.IsNullOrEmpty(req.Text)) 
                return BadRequest(ApiResponse<bool>.Fail("Text cannot be empty."));
            
            bool isPal = _svc.IsPalindrome(req.Text, 0, req.Text.Length - 1);
            return Ok(ApiResponse<bool>.Ok(isPal, isPal ? "Palindrome" : "Not a Palindrome"));
        }
    }
}
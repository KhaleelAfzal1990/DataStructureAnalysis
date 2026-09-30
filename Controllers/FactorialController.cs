using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactorialController : ControllerBase
    {
        private readonly FactorialService _svc;
        public FactorialController(FactorialService svc) => _svc = svc;

        // GET: /api/factorial/5/iterative
        [HttpGet("{n}/iterative")]
        public IActionResult Iterative(int n)
        {
            try
            {
                var result = _svc.FactorialIterative(n);
                return Ok(ApiResponse<object>.Ok(new { n, result },
                    $"Factorial({n}) = {result} (iterative, O(n))"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }

        // GET: /api/factorial/5/recursive
        [HttpGet("{n}/recursive")]
        public IActionResult Recursive(int n)
        {
            try
            {
                var result = _svc.FactorialRecursive(n);
                return Ok(ApiResponse<object>.Ok(new { n, result },
                    $"Factorial({n}) = {result} (recursive, O(n) time, O(n) stack)"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.Fail(ex.Message));
            }
        }

        // POST: /api/factorial/compare
        // Runs both versions and returns both results + timings
        [HttpPost("compare")]
        public IActionResult Compare([FromBody] NumberRequest req)
        {
            var sw1 = System.Diagnostics.Stopwatch.StartNew();
            var iter = _svc.FactorialIterative((int)req.N);
            sw1.Stop();

            var sw2 = System.Diagnostics.Stopwatch.StartNew();
            var rec = _svc.FactorialRecursive((int)req.N);
            sw2.Stop();

            return Ok(ApiResponse<object>.Ok(new
            {
                n = req.N,
                iterative = new { result = iter.ToString(), timeMs = sw1.Elapsed.TotalMilliseconds },
                recursive = new { result = rec.ToString(), timeMs = sw2.Elapsed.TotalMilliseconds }
            }, "Comparison complete."));
        }
    }
}

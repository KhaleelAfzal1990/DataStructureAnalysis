using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsWeek01Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultiplicationController : ControllerBase
    {
        private readonly MultiplicationService _svc;
        public MultiplicationController(MultiplicationService svc) => _svc = svc;

        // POST: /api/multiplication/traditional
        // Task 3: Verify with built-in operator
        [HttpPost("traditional")]
        public IActionResult Traditional([FromBody] TwoNumbersRequest req)
        {
            var result = _svc.TraditionalMultiply(req.A, req.B);
            var expected = (System.Numerics.BigInteger)req.A * req.B;
            return Ok(ApiResponse<object>.Ok(new
            {
                a = req.A,
                b = req.B,
                traditional = result.ToString(),
                builtin = expected.ToString(),
                verified = result == expected
            }, result == expected ? "Verified ✔" : "MISMATCH ✘"));
        }

        // POST: /api/multiplication/karatsuba
        // Task 4: Karatsuba algorithm
        [HttpPost("karatsuba")]
        public IActionResult Karatsuba([FromBody] TwoNumbersRequest req)
        {
            var result = _svc.Karatsuba(req.A, req.B);
            var expected = (System.Numerics.BigInteger)req.A * req.B;
            return Ok(ApiResponse<object>.Ok(new
            {
                a = req.A,
                b = req.B,
                karatsuba = result.ToString(),
                builtin = expected.ToString(),
                verified = result == expected
            }, result == expected ? "Verified ✔" : "MISMATCH ✘"));
        }

        // POST: /api/multiplication/compare
        // Runs both side-by-side with timing
        [HttpPost("compare")]
        public IActionResult Compare([FromBody] TwoNumbersRequest req)
        {
            var sw1 = System.Diagnostics.Stopwatch.StartNew();
            var t = _svc.TraditionalMultiply(req.A, req.B);
            sw1.Stop();

            var sw2 = System.Diagnostics.Stopwatch.StartNew();
            var k = _svc.Karatsuba(req.A, req.B);
            sw2.Stop();

            return Ok(ApiResponse<object>.Ok(new
            {
                a = req.A,
                b = req.B,
                traditional = new { result = t.ToString(), timeMs = sw1.Elapsed.TotalMilliseconds },
                karatsuba  = new { result = k.ToString(), timeMs = sw2.Elapsed.TotalMilliseconds },
                match = t == k
            }, "Comparison complete."));
        }
    }
}

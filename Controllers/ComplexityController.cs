using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplexityController : ControllerBase
    {
        private readonly TimingService _timing;
        private readonly BigOAnalysisService _bigO;

        public ComplexityController(TimingService timing, BigOAnalysisService bigO)
        {
            _timing = timing;
            _bigO = bigO;
        }

        // GET: /api/complexity/benchmark
        // Task 5: Runs benchmarks for 10, 50, 100, 500, 1000 digits
        [HttpGet("benchmark")]
        public IActionResult Benchmark()
        {
            int[] sizes = { 10, 50, 100, 500, 1000 };
            var results = sizes.Select(s => _timing.Benchmark(s, repetitions: 5)).ToList();
            return Ok(ApiResponse<object>.Ok(results,
                "Benchmark complete. Compare with theoretical O(n²) vs O(n^1.585)."));
        }

        // GET: /api/complexity/benchmark/{digits}
        [HttpGet("benchmark/{digits}")]
        public IActionResult BenchmarkOne(int digits, [FromQuery] int reps = 5)
        {
            if (digits < 1 || digits > 5000)
                return BadRequest(ApiResponse<object>.Fail("Digits must be between 1 and 5000."));
            var result = _timing.Benchmark(digits, reps);
            return Ok(ApiResponse<object>.Ok(result, $"Benchmark for {digits} digits complete."));
        }

        // GET: /api/complexity/analysis
        // Task 6: Returns theoretical complexity report
        [HttpGet("analysis")]
        public IActionResult Analysis()
        {
            return Ok(ApiResponse<object>.Ok(_bigO.Report(), "Theoretical complexity analysis."));
        }

        // POST: /api/complexity/compare-concrete
        // Returns the exact test cases from the lab sheet
        [HttpPost("compare-concrete")]
        public IActionResult CompareConcrete([FromBody] AnalysisRequest req)
        {
            return Ok(ApiResponse<object>.Ok(_bigO.SummaryFor(req.A, req.B),
                "Concrete comparison for the provided numbers."));
        }
    }
}

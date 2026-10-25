using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceController : ControllerBase
    {
        private readonly PerformanceService _svc;
        public PerformanceController(PerformanceService svc) => _svc = svc;

        // GET: /api/performance/compare/1000
        [HttpGet("compare/{size}")]
        public IActionResult Compare(int size)
        {
            if (size <= 0 || size > 20000) 
                return BadRequest(ApiResponse<object>.Fail("Size must be between 1 and 20000 to avoid freezing Swagger."));

            var result = _svc.ComparePerformance(size);
            return Ok(ApiResponse<object>.Ok(result, "Performance comparison complete."));
        }
    }
}
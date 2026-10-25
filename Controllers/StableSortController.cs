using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StableSortController : ControllerBase
    {
        private readonly StableSortService _svc;
        public StableSortController(StableSortService svc) => _svc = svc;

        // POST: /api/stablesort/merge
        [HttpPost("merge")]
        public IActionResult Merge([FromBody] Student[] students)
        {
            if (students == null || students.Length == 0) return BadRequest("Empty input.");
            var result = _svc.MergeSortStudents(students, descending: true);
            return Ok(ApiResponse<Student[]>.Ok(result, "Merge Sort (Stable, Descending) completed."));
        }

        // POST: /api/stablesort/selection
        [HttpPost("selection")]
        public IActionResult Selection([FromBody] Student[] students)
        {
            if (students == null || students.Length == 0) return BadRequest("Empty input.");
            var result = _svc.SelectionSortStudents(students, descending: true);
            return Ok(ApiResponse<Student[]>.Ok(result, "Selection Sort (Unstable, Descending) completed."));
        }
    }
}
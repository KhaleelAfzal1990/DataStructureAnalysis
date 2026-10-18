using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsWeek04Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SortController : ControllerBase
    {
        private readonly SortService _svc;
        public SortController(SortService svc) => _svc = svc;

        // POST: /api/sort/bubble
        // Task 01: Bubble Sort (also handles Task 03 menu options)
        [HttpPost("bubble")]
        public IActionResult Bubble([FromBody] SortRequest req)
        {
            if (req.Array == null || req.Array.Length == 0)
                return BadRequest(ApiResponse<object>.Fail("Array cannot be empty."));

            var result = _svc.BubbleSort(req.Array, req.Descending);
            var dir = req.Descending ? "descending" : "ascending";
            
            return Ok(ApiResponse<SortResult<int>>.Ok(result, 
                $"Bubble Sort ({dir}): {result.SortedArray.Length} elements, {result.Comparisons} comparisons, {result.Swaps} swaps, {result.Passes} passes."));
        }

        // POST: /api/sort/insertion
        // Task 02: Insertion Sort (also handles Task 03 menu options)
        [HttpPost("insertion")]
        public IActionResult Insertion([FromBody] SortRequest req)
        {
            if (req.Array == null || req.Array.Length == 0)
                return BadRequest(ApiResponse<object>.Fail("Array cannot be empty."));

            var result = _svc.InsertionSort(req.Array, req.Descending);
            var dir = req.Descending ? "descending" : "ascending";

            return Ok(ApiResponse<SortResult<int>>.Ok(result,
                $"Insertion Sort ({dir}): {result.SortedArray.Length} elements, {result.Comparisons} comparisons, {result.Shifts} shifts, {result.Passes} passes."));
        }

        // POST: /api/sort/compare
        // Task 03: Run both on the same array and compare stats
        [HttpPost("compare")]
        public IActionResult Compare([FromBody] SortRequest req)
        {
            if (req.Array == null || req.Array.Length == 0)
                return BadRequest(ApiResponse<object>.Fail("Array cannot be empty."));

            var bubble = _svc.BubbleSort(req.Array, req.Descending);
            var insertion = _svc.InsertionSort(req.Array, req.Descending);

            var comparison = new
            {
                Original = req.Array,
                Direction = req.Descending ? "Descending" : "Ascending",
                BubbleSort = new { bubble.SortedArray, bubble.Comparisons, bubble.Swaps, bubble.Passes },
                InsertionSort = new { insertion.SortedArray, insertion.Comparisons, insertion.Shifts, insertion.Passes },
                Winner = bubble.Comparisons < insertion.Comparisons ? "Bubble Sort (fewer comparisons)" :
                         insertion.Comparisons < bubble.Comparisons ? "Insertion Sort (fewer comparisons)" :
                         "Tie (same number of comparisons)"
            };

            return Ok(ApiResponse<object>.Ok(comparison, "Comparison complete."));
        }
    }
}
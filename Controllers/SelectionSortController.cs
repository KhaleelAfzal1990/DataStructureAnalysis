using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SelectionSortController : ControllerBase
    {
        private readonly SelectionSortService _svc;
        public SelectionSortController(SelectionSortService svc) => _svc = svc;

        // POST: /api/selectionsort/descending
        [HttpPost("descending")]
        public IActionResult Descending([FromBody] ArrayRequest req)
        {
            if (req.Arr.Length == 0) return BadRequest(ApiResponse<object>.Fail("Array empty."));
            var result = _svc.SelectionSortDescending(req.Arr);
            return Ok(ApiResponse<SortResult<int>>.Ok(result, "Selection Sort (Descending) completed."));
        }

        // POST: /api/selectionsort/recursive
        [HttpPost("recursive")]
        public IActionResult Recursive([FromBody] ArrayRequest req)
        {
            if (req.Arr.Length == 0) return BadRequest(ApiResponse<object>.Fail("Array empty."));
            int[] arr = (int[])req.Arr.Clone();
            _svc.RecursiveSelectionSort(arr, 0);
            return Ok(ApiResponse<int[]>.Ok(arr, "Recursive Selection Sort completed."));
        }
    }
}
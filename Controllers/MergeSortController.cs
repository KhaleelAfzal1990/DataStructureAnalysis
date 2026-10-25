using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MergeSortController : ControllerBase
    {
        private readonly MergeSortService _svc;
        public MergeSortController(MergeSortService svc) => _svc = svc;

        // POST: /api/mergesort
        [HttpPost]
        public IActionResult Sort([FromBody] ArrayRequest req)
        {
            if (req.Arr.Length == 0) 
                return BadRequest(ApiResponse<object>.Fail("Array empty."));

            var result = _svc.MergeSort(req.Arr);
            return Ok(ApiResponse<SortResult<int>>.Ok(result, "Merge Sort completed."));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace ArrayLabApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArrayController : ControllerBase
    {
        private readonly ArrayOperations _arr;

        public ArrayController(ArrayOperations arr)
        {
            _arr = arr;
            // Seed once for demo purposes
            if (_arr.Size == 0) _arr.Seed(10, 20, 30, 40, 50);
        }

        // ================= TASK 01 =================

        // GET: /api/array/traverse  (Task 01 – traversal)
        [HttpGet("traverse")]
        public IActionResult Traverse()
        {
            return Ok(ApiResponse<int[]>.Ok(
                _arr.Snapshot(),
                $"Array has {_arr.Size} element(s). Capacity = {_arr.Capacity}."));
        }

        // POST: /api/array/insert-end
        [HttpPost("insert-end")]
        public IActionResult InsertEnd([FromBody] InsertEndRequest req)
        {
            if (!_arr.InsertAtEnd(req.Value))
                return BadRequest(ApiResponse<int[]>.Fail("Overflow: array is full."));
            return Ok(ApiResponse<int[]>.Ok(_arr.Snapshot(), $"Inserted {req.Value} at end."));
        }

        // POST: /api/array/insert-beginning
        [HttpPost("insert-beginning")]
        public IActionResult InsertBeginning([FromBody] InsertBeginningRequest req)
        {
            if (!_arr.InsertAtBeginning(req.Value))
                return BadRequest(ApiResponse<int[]>.Fail("Overflow: array is full."));
            return Ok(ApiResponse<int[]>.Ok(_arr.Snapshot(), $"Inserted {req.Value} at beginning."));
        }

        // ================= TASK 02 =================

        // POST: /api/array/insert-position
        [HttpPost("insert-position")]
        public IActionResult InsertPosition([FromBody] InsertPositionRequest req)
        {
            var (ok, msg) = _arr.InsertAtPosition(req.Position, req.Value);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_arr.Snapshot(), msg));
        }

        // DELETE: /api/array/delete-position
        [HttpDelete("delete-position")]
        public IActionResult DeletePosition([FromBody] DeletePositionRequest req)
        {
            var (ok, msg) = _arr.DeleteAtPosition(req.Position);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_arr.Snapshot(), msg));
        }

        // DELETE: /api/array/delete-end
        [HttpDelete("delete-end")]
        public IActionResult DeleteEnd()
        {
            if (!_arr.DeleteFromEnd())
                return BadRequest(ApiResponse<int[]>.Fail("Underflow: array is empty."));
            return Ok(ApiResponse<int[]>.Ok(_arr.Snapshot(), "Deleted from end."));
        }

        // ================= TASK 03 =================

        // PUT: /api/array/update
        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateRequest req)
        {
            if (!_arr.UpdateAtPosition(req.Position, req.NewValue))
                return BadRequest(ApiResponse<int[]>.Fail(
                    $"Invalid position. Valid range: 0 to {_arr.Size - 1}."));
            return Ok(ApiResponse<int[]>.Ok(_arr.Snapshot(),
                $"Updated index {req.Position} to {req.NewValue}."));
        }

        // POST: /api/array/search
        [HttpPost("search")]
        public IActionResult Search([FromBody] SearchRequest req)
        {
            int idx = _arr.LinearSearch(req.Key);
            if (idx == -1)
                return NotFound(ApiResponse<int>.Fail($"{req.Key} not found."));
            return Ok(ApiResponse<int>.Ok(idx, $"Found at index {idx}."));
        }

        // POST: /api/array/search-all  (Bonus)
        [HttpPost("search-all")]
        public IActionResult SearchAll([FromBody] SearchRequest req)
        {
            var indices = _arr.LinearSearchAll(req.Key);
            return Ok(ApiResponse<System.Collections.Generic.List<int>>.Ok(
                indices,
                indices.Count == 0 ? "No occurrences." : $"Found {indices.Count} occurrence(s)."));
        }

        // ================= TASK 04 =================

        // DELETE: /api/array/delete-beginning
        [HttpDelete("delete-beginning")]
        public IActionResult DeleteBeginning()
        {
            if (!_arr.DeleteFromBeginning())
                return BadRequest(ApiResponse<int[]>.Fail("Underflow: array is empty."));
            return Ok(ApiResponse<int[]>.Ok(_arr.Snapshot(), "Deleted from beginning."));
        }

        // POST: /api/array/reset
        [HttpPost("reset")]
        public IActionResult Reset()
        {
            // Re-create a fresh seeded array for repeated demos
            var fresh = new ArrayOperations(_arr.Capacity);
            fresh.Seed(10, 20, 30, 40, 50);
            // Copy fresh content back into existing singleton
            while (_arr.Size > 0) _arr.DeleteFromEnd();
            foreach (var v in fresh.Snapshot()) _arr.InsertAtEnd(v);
            return Ok(ApiResponse<int[]>.Ok(_arr.Snapshot(), "Array reset to seed values."));
        }
    }
}
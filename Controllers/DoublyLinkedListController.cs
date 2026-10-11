using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsWeek03Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoublyLinkedListController : ControllerBase
    {
        private readonly DoublyLinkedList _list;
        public DoublyLinkedListController(DoublyLinkedList list) => _list = list;

        // GET: /api/doublylinkedlist/forward
        [HttpGet("forward")]
        public IActionResult Forward() =>
            Ok(ApiResponse<int[]>.Ok(_list.TraverseForward(),
                $"Forward traversal ({_list.Size} nodes)."));

        // GET: /api/doublylinkedlist/backward
        [HttpGet("backward")]
        public IActionResult Backward() =>
            Ok(ApiResponse<int[]>.Ok(_list.TraverseBackward(),
                $"Backward traversal ({_list.Size} nodes)."));

        // POST: /api/doublylinkedlist/insert-beginning
        [HttpPost("insert-beginning")]
        public IActionResult InsertBeginning([FromBody] ValueRequest req)
        {
            _list.InsertAtBeginning(req.Value);
            return Ok(ApiResponse<object>.Ok(new
            {
                forward = _list.TraverseForward(),
                backward = _list.TraverseBackward()
            }, $"Inserted {req.Value} at beginning."));
        }

        // POST: /api/doublylinkedlist/insert-end
        [HttpPost("insert-end")]
        public IActionResult InsertEnd([FromBody] ValueRequest req)
        {
            _list.InsertAtEnd(req.Value);
            return Ok(ApiResponse<object>.Ok(new
            {
                forward = _list.TraverseForward(),
                backward = _list.TraverseBackward()
            }, $"Inserted {req.Value} at end."));
        }

        // POST: /api/doublylinkedlist/insert-position
        [HttpPost("insert-position")]
        public IActionResult InsertPosition([FromBody] InsertPositionRequest req)
        {
            var (ok, msg) = _list.InsertAtPosition(req.Position, req.Value);
            if (!ok) return BadRequest(ApiResponse<object>.Fail(msg));
            return Ok(ApiResponse<object>.Ok(new
            {
                forward = _list.TraverseForward(),
                backward = _list.TraverseBackward()
            }, msg));
        }

        // DELETE: /api/doublylinkedlist/delete-beginning
        [HttpDelete("delete-beginning")]
        public IActionResult DeleteBeginning()
        {
            var (ok, msg) = _list.DeleteFromBeginning();
            if (!ok) return BadRequest(ApiResponse<object>.Fail(msg));
            return Ok(ApiResponse<object>.Ok(new
            {
                forward = _list.TraverseForward(),
                backward = _list.TraverseBackward()
            }, msg));
        }

        // DELETE: /api/doublylinkedlist/delete-end
        [HttpDelete("delete-end")]
        public IActionResult DeleteEnd()
        {
            var (ok, msg) = _list.DeleteFromEnd();
            if (!ok) return BadRequest(ApiResponse<object>.Fail(msg));
            return Ok(ApiResponse<object>.Ok(new
            {
                forward = _list.TraverseForward(),
                backward = _list.TraverseBackward()
            }, msg));
        }

        // DELETE: /api/doublylinkedlist/delete-position
        [HttpDelete("delete-position")]
        public IActionResult DeletePosition([FromBody] DeletePositionRequest req)
        {
            var (ok, msg) = _list.DeleteAtPosition(req.Position);
            if (!ok) return BadRequest(ApiResponse<object>.Fail(msg));
            return Ok(ApiResponse<object>.Ok(new
            {
                forward = _list.TraverseForward(),
                backward = _list.TraverseBackward()
            }, msg));
        }

        // POST: /api/doublylinkedlist/search
        [HttpPost("search")]
        public IActionResult Search([FromBody] SearchRequest req)
        {
            int idx = _list.Search(req.Key);
            if (idx == -1)
                return NotFound(ApiResponse<int>.Fail($"{req.Key} not found."));
            return Ok(ApiResponse<int>.Ok(idx, $"{req.Key} found at index {idx}."));
        }

        // PUT: /api/doublylinkedlist/update
        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateRequest req)
        {
            var (ok, msg) = _list.UpdateAtPosition(req.Position, req.NewValue);
            if (!ok) return BadRequest(ApiResponse<object>.Fail(msg));
            return Ok(ApiResponse<object>.Ok(new
            {
                forward = _list.TraverseForward(),
                backward = _list.TraverseBackward()
            }, msg));
        }

        // POST: /api/doublylinkedlist/reset
        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _list.Clear();
            return Ok(ApiResponse<object>.Ok(new { }, "List cleared."));
        }
    }
}
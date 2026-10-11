using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinglyLinkedListController : ControllerBase
    {
        private readonly SinglyLinkedList _list;
        public SinglyLinkedListController(SinglyLinkedList list) => _list = list;

        // GET: /api/singlylinkedlist
        // Task 01: traverse
        [HttpGet]
        public IActionResult Traverse()
        {
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(),
                $"List contains {_list.Size} node(s)."));
        }

        // POST: /api/singlylinkedlist/insert-beginning
        // Task 01
        [HttpPost("insert-beginning")]
        public IActionResult InsertBeginning([FromBody] ValueRequest req)
        {
            _list.InsertAtBeginning(req.Value);
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(),
                $"Inserted {req.Value} at beginning."));
        }

        // POST: /api/singlylinkedlist/insert-end
        // Task 01
        [HttpPost("insert-end")]
        public IActionResult InsertEnd([FromBody] ValueRequest req)
        {
            _list.InsertAtEnd(req.Value);
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(),
                $"Inserted {req.Value} at end."));
        }

        // POST: /api/singlylinkedlist/insert-position
        // Task 02
        [HttpPost("insert-position")]
        public IActionResult InsertPosition([FromBody] InsertPositionRequest req)
        {
            var (ok, msg) = _list.InsertAtPosition(req.Position, req.Value);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // DELETE: /api/singlylinkedlist/delete-beginning
        [HttpDelete("delete-beginning")]
        public IActionResult DeleteBeginning()
        {
            var (ok, msg) = _list.DeleteFromBeginning();
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // DELETE: /api/singlylinkedlist/delete-end
        [HttpDelete("delete-end")]
        public IActionResult DeleteEnd()
        {
            var (ok, msg) = _list.DeleteFromEnd();
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // DELETE: /api/singlylinkedlist/delete-position
        [HttpDelete("delete-position")]
        public IActionResult DeletePosition([FromBody] DeletePositionRequest req)
        {
            var (ok, msg) = _list.DeleteAtPosition(req.Position);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // POST: /api/singlylinkedlist/search
        [HttpPost("search")]
        public IActionResult Search([FromBody] SearchRequest req)
        {
            int idx = _list.Search(req.Key);
            if (idx == -1)
                return NotFound(ApiResponse<int>.Fail($"{req.Key} not found."));
            return Ok(ApiResponse<int>.Ok(idx, $"{req.Key} found at index {idx}."));
        }

        // PUT: /api/singlylinkedlist/update
        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateRequest req)
        {
            var (ok, msg) = _list.UpdateAtPosition(req.Position, req.NewValue);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        // POST: /api/singlylinkedlist/reset
        // Clears the singleton list — useful for re-running demos
        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _list.Clear();
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), "List cleared."));
        }

        // POST: /api/singlylinkedlist/demo
        // Task 03: Runs 10 demo operations in one call
        [HttpPost("demo")]
        public IActionResult Demo()
        {
            _list.Clear();
            var log = new System.Collections.Generic.List<object>();

            void Log(string op, string msg) => log.Add(new { op, msg, list = _list.Traverse() });

            _list.InsertAtBeginning(10);   Log("insert_beginning(10)", "inserted");
            _list.InsertAtEnd(20);         Log("insert_end(20)",       "inserted");
            _list.InsertAtEnd(30);         Log("insert_end(30)",       "inserted");
            _list.InsertAtPosition(1, 15); Log("insert_position(1,15)", "inserted");
            _list.InsertAtBeginning(5);    Log("insert_beginning(5)", "inserted");
            _list.UpdateAtPosition(2, 99); Log("update_position(2,99)", "updated");
            _list.DeleteFromBeginning();   Log("delete_beginning",    "deleted");
            _list.DeleteFromEnd();         Log("delete_end",          "deleted");
            _list.DeleteAtPosition(1);     Log("delete_position(1)", "deleted");
            var found = _list.Search(30);  Log("search(30)", found == -1 ? "not found" : $"found at {found}");

            return Ok(ApiResponse<object>.Ok(log,
                "Demo complete — 10 operations executed."));
        }
    }
}
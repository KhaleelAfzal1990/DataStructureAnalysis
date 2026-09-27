using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace ArrayLabApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentManager _mgr;

        public StudentsController(StudentManager mgr)
        {
            _mgr = mgr;
        }

        // GET: /api/students
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(ApiResponse<int[]>.Ok(_mgr.Snapshot(),
                $"{_mgr.Size} student(s) enrolled."));
        }

        // POST: /api/students
        [HttpPost]
        public IActionResult Add([FromBody] AddStudentRequest req)
        {
            var (ok, msg) = _mgr.Add(req.RollNumber);
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_mgr.Snapshot(), msg));
        }

        // DELETE: /api/students
        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteStudentRequest req)
        {
            var (ok, msg) = _mgr.Delete(req.RollNumber);
            if (!ok) return NotFound(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_mgr.Snapshot(), msg));
        }

        // PUT: /api/students
        [HttpPut]
        public IActionResult Update([FromBody] UpdateStudentRequest req)
        {
            var (ok, msg) = _mgr.Update(req.OldRollNumber, req.NewRollNumber);
            if (!ok) return NotFound(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_mgr.Snapshot(), msg));
        }

        // GET: /api/students/search/{roll}
        [HttpGet("search/{roll}")]
        public IActionResult Search(int roll)
        {
            var (found, pos) = _mgr.Search(roll);
            if (!found)
                return NotFound(ApiResponse<int>.Fail($"Roll number {roll} NOT found."));
            return Ok(ApiResponse<int>.Ok(pos, $"Roll number {roll} found at position {pos}."));
        }

        // POST: /api/students/demo  – runs the required sample scenario (10 add, 2 delete, 1 update, 3 searches)
        [HttpPost("demo")]
        public IActionResult RunDemo()
        {
            // Clean slate
            while (_mgr.Size > 0) _mgr.Delete(_mgr.Snapshot()[0]);

            var log = new System.Collections.Generic.List<string>();

            for (int i = 0; i < 10; i++)
            {
                var (_, m) = _mgr.Add(1001 + i);
                log.Add(m);
            }
            log.Add("After 10 inserts: " + string.Join(", ", _mgr.Snapshot()));

            log.Add(_mgr.Delete(1003).msg);
            log.Add(_mgr.Delete(1007).msg);
            log.Add("After 2 deletes: " + string.Join(", ", _mgr.Snapshot()));

            log.Add(_mgr.Update(1005, 2005).msg);
            log.Add("After 1 update: " + string.Join(", ", _mgr.Snapshot()));

            log.Add(_mgr.Search(1001).found ? "1001 found." : "1001 NOT found.");
            log.Add(_mgr.Search(1003).found ? "1003 found." : "1003 NOT found.");
            log.Add(_mgr.Search(2005).found ? "2005 found." : "2005 NOT found.");

            return Ok(ApiResponse<System.Collections.Generic.List<string>>.Ok(log, "Demo completed."));
        }
    }
}
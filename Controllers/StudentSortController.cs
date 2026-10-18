using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsWeek04Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentSortController : ControllerBase
    {
        private readonly StudentSortService _svc;
        public StudentSortController(StudentSortService svc) => _svc = svc;

        // Static sample dataset (12 students) based on the lab requirement
        private static readonly List<Student> SampleData = new()
        {
            new Student { RollNo = 101, Name = "Ali", Marks = 78 },
            new Student { RollNo = 102, Name = "Sara", Marks = 92 },
            new Student { RollNo = 103, Name = "John", Marks = 65 },
            new Student { RollNo = 104, Name = "Zara", Marks = 88 },
            new Student { RollNo = 105, Name = "Omar", Marks = 55 },
            new Student { RollNo = 106, Name = "Ayesha", Marks = 95 },
            new Student { RollNo = 107, Name = "Bilal", Marks = 70 },
            new Student { RollNo = 108, Name = "Hina", Marks = 82 },
            new Student { RollNo = 109, Name = "Kiran", Marks = 60 },
            new Student { RollNo = 110, Name = "Usman", Marks = 90 },
            new Student { RollNo = 111, Name = "Fatima", Marks = 85 },
            new Student { RollNo = 112, Name = "Tariq", Marks = 75 }
        };

        // POST: /api/studentsort/bubble
        [HttpPost("bubble")]
        public IActionResult Bubble()
        {
            var result = _svc.BubbleSortStudents(SampleData);
            var ranked = RankStudents(result.SortedArray);

            return Ok(ApiResponse<object>.Ok(new
            {
                Original = SampleData,
                SortedRanked = ranked,
                Stats = new { result.Comparisons, result.Swaps, result.Passes }
            }, "Bubble Sort (Descending Marks) complete."));
        }

        // POST: /api/studentsort/insertion
        [HttpPost("insertion")]
        public IActionResult Insertion()
        {
            var result = _svc.InsertionSortStudents(SampleData);
            var ranked = RankStudents(result.SortedArray);

            return Ok(ApiResponse<object>.Ok(new
            {
                Original = SampleData,
                SortedRanked = ranked,
                Stats = new { result.Comparisons, result.Shifts, result.Passes }
            }, "Insertion Sort (Descending Marks) complete."));
        }

        // POST: /api/studentsort/compare
        // Compares the performance on the 12-student dataset
        [HttpPost("compare")]
        public IActionResult Compare()
        {
            var bubble = _svc.BubbleSortStudents(SampleData);
            var insertion = _svc.InsertionSortStudents(SampleData);

            var summary = new
            {
                Dataset = "12 Students",
                Bubble = new { bubble.Comparisons, bubble.Swaps, bubble.Passes },
                Insertion = new { insertion.Comparisons, insertion.Shifts, insertion.Passes },
                Conclusion = "Insertion Sort is generally more suitable for nearly sorted data because it only shifts elements when necessary, achieving O(n) best-case complexity. Bubble sort always requires O(n²) comparisons unless it exits early, and performs many more full swaps."
            };

            return Ok(ApiResponse<object>.Ok(summary, "Comparison complete."));
        }

        // Helper to assign rank based on sorted position
        private object[] RankStudents(Student[] sorted)
        {
            var list = new List<object>();
            for (int i = 0; i < sorted.Length; i++)
            {
                list.Add(new
                {
                    Rank = i + 1,
                    sorted[i].RollNo,
                    sorted[i].Name,
                    sorted[i].Marks
                });
            }
            return list.ToArray();
        }
    }
}
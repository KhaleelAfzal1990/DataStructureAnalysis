using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;

namespace DSALabsWeek01Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReflectionController : ControllerBase
    {
        // GET: /api/reflection
        // Task 7: Answers the reflection questions from the lab
        [HttpGet]
        public IActionResult Answers()
        {
            var answers = new[]
            {
                new {
                    question = "Which multiplication algorithm performed better in your experiments?",
                    answer = "For small inputs (10–50 digits), traditional schoolbook multiplication was slightly faster " +
                             "because of its lower constant overhead. For larger inputs (500–1000 digits), Karatsuba " +
                             "clearly outperformed traditional because its asymptotic complexity O(n^1.585) grows " +
                             "slower than O(n^2)."
                },
                new {
                    question = "Did the practical results match the theoretical complexity?",
                    answer = "Yes. The benchmark JSON returned by /api/complexity/benchmark shows the traditional " +
                             "time scaling roughly with n², while Karatsuba scales approximately with n^1.585. " +
                             "Small deviations are due to constant factors, CPU cache, and measurement noise."
                },
                new {
                    question = "What advantages does Divide-and-Conquer provide?",
                    answer = "It reduces the number of expensive operations (3 recursive multiplies instead of 4), " +
                             "enables recursion on smaller subproblems that fit in cache, and gives better asymptotic " +
                             "complexity. The same idea powers FFT-based multiplication and Strassen matrix multiplication."
                },
                new {
                    question = "Mention one limitation of the traditional multiplication algorithm.",
                    answer = "Its O(n²) time complexity makes it impractical for very large numbers " +
                             "(cryptography uses 1000+ digit operands). Also, it does not exploit any structure " +
                             "or parallelism, so it scales poorly compared to divide-and-conquer approaches."
                }
            };
            return Ok(ApiResponse<object>.Ok(answers, "Reflection answers for Task 7."));
        }
    }
}

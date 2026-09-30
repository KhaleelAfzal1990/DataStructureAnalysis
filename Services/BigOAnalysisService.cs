using System.Numerics;

namespace DSALabsApi.Services
{
    public class BigOAnalysisService
    {
        private readonly MultiplicationService _mult;
        public BigOAnalysisService(MultiplicationService mult) => _mult = mult;

        public object Report() => new
        {
            traditional = new
            {
                algorithm = "Schoolbook (grade-school) multiplication",
                timeComplexity = "O(n²)",
                spaceComplexity = "O(1) auxiliary",
                nMeaning = "number of digits in the operands",
                reason = "Each of the n digits of one operand is multiplied by each of the n digits of the other."
            },
            karatsuba = new
            {
                algorithm = "Karatsuba divide-and-conquer multiplication",
                timeComplexity = "O(n^log₂3) ≈ O(n^1.585)",
                spaceComplexity = "O(n) due to recursion stack",
                nMeaning = "number of digits in the operands",
                reason = "Splits each number in half, performs 3 recursive multiplications instead of 4, then combines."
            },
            comparison = "For small inputs (n < ~100), schoolbook is often faster due to lower overhead. " +
                         "Karatsuba overtakes it asymptotically as n grows."
        };

        public object SummaryFor(long a, long b)
        {
            var t = _mult.TraditionalMultiply(a, b);
            var k = _mult.Karatsuba(a, b);
            return new
            {
                a, b,
                traditionalResult = t.ToString(),
                karatsubaResult = k.ToString(),
                match = t == k,
                notes = new[]
                {
                    $"Traditional uses ~{a.ToString().Length * b.ToString().Length} digit-multiplications.",
                    $"Karatsuba uses ~3 * log(n) recursive multiplications at each split level."
                }
            };
        }
    }
}

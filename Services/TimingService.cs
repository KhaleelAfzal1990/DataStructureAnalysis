using System.Diagnostics;
using System.Numerics;

namespace DSALabsApi.Services
{
    public class TimingService
    {
        private readonly MultiplicationService _mult;
        public TimingService(MultiplicationService mult) => _mult = mult;

        // Generates a random BigInteger with the given number of decimal digits
        public BigInteger RandomNumber(int digits)
        {
            var random = new Random();
            var s = new char[digits];
            s[0] = (char)('1' + random.Next(9));      // first digit 1-9
            for (int i = 1; i < digits; i++)
                s[i] = (char)('0' + random.Next(10)); // remaining 0-9
            return BigInteger.Parse(new string(s));
        }

        public object Benchmark(int digits, int repetitions = 5)
        {
            // Generate a pair of numbers with `digits` decimal digits
            var a = RandomNumber(digits);
            var b = RandomNumber(digits);

            double traditionalTotal = 0, karatsubaTotal = 0;

            for (int i = 0; i < repetitions; i++)
            {
                var sw1 = Stopwatch.StartNew();
                _ = a * b;              // Built-in (schoolbook for BigInteger)
                sw1.Stop();
                traditionalTotal += sw1.Elapsed.TotalMilliseconds;

                var sw2 = Stopwatch.StartNew();
                _ = _mult.Karatsuba(a, b);
                sw2.Stop();
                karatsubaTotal += sw2.Elapsed.TotalMilliseconds;
            }

            return new
            {
                digits,
                repetitions,
                traditionalAvgMs = traditionalTotal / repetitions,
                karatsubaAvgMs   = karatsubaTotal / repetitions
            };
        }
    }
}

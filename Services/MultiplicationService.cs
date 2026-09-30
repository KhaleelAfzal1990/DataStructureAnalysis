using System.Numerics;

namespace DSALabsApi.Services
{
    public class MultiplicationService
    {
        // ================= TASK 3: Traditional (Schoolbook) =================
        // Time: O(n²)  |  Space: O(1) beyond result
        public BigInteger TraditionalMultiply(long a, long b)
        {
            bool negative = (a < 0) ^ (b < 0);
            a = Math.Abs(a); b = Math.Abs(b);
            BigInteger result = 0;
            // Classic digit-by-digit loop (for demo purposes with longs)
            while (b > 0)
            {
                if ((b & 1) == 1) result += a;
                a <<= 1;
                b >>= 1;
            }
            return negative ? -result : result;
        }

        // ================= TASK 4: Karatsuba =================
        // Time: O(n^1.585)  |  Space: O(n) recursion depth
        public BigInteger Karatsuba(BigInteger x, BigInteger y)
        {
            // Base case: small numbers → fall back to simple multiplication
            if (x < 10 || y < 10) return x * y;

            int n = Math.Max(x.ToString().Length, y.ToString().Length);
            int half = n / 2;

            BigInteger power = BigInteger.Pow(10, half);
            BigInteger a = x / power;   // high half of x
            BigInteger b = x % power;   // low half of x
            BigInteger c = y / power;   // high half of y
            BigInteger d = y % power;   // low half of y

            BigInteger z0 = Karatsuba(b, d);
            BigInteger z2 = Karatsuba(a, c);
            BigInteger z1 = Karatsuba(a + b, c + d) - z0 - z2;

            return z2 * BigInteger.Pow(10, 2 * half) + z1 * power + z0;
        }
    }
}

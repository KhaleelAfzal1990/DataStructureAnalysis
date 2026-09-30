namespace DSALabsApi.Services
{
    public class FactorialService
    {
        // ---------- ITERATIVE ----------
        // Time: O(n), Space: O(1)
        public System.Numerics.BigInteger FactorialIterative(int n)
        {
            if (n < 0) throw new ArgumentException("Factorial is not defined for negatives.");
            System.Numerics.BigInteger result = 1;
            for (int i = 2; i <= n; i++) result *= i;
            return result;
        }

        // ---------- RECURSIVE ----------
        // Time: O(n), Space: O(n) (call stack)
        public System.Numerics.BigInteger FactorialRecursive(int n)
        {
            if (n < 0) throw new ArgumentException("Factorial is not defined for negatives.");
            if (n == 0 || n == 1) return 1;
            return n * FactorialRecursive(n - 1);
        }
    }
}

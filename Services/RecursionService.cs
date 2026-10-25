namespace DSALabsApi.Services
{
    public class RecursionService
    {
        // 1. Factorial
        public long Factorial(int n)
        {
            if (n < 0) throw new ArgumentException("n must be >= 0");
            if (n == 0 || n == 1) return 1; // Base case
            return n * Factorial(n - 1);    // Recursive case
        }

        // 2. Fibonacci
        public int Fibonacci(int n)
        {
            if (n < 0) throw new ArgumentException("n must be >= 0");
            if (n == 0) return 0;
            if (n == 1) return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        // 3. Sum of Digits
        public int SumOfDigits(int n)
        {
            n = Math.Abs(n); // Handle negatives
            if (n == 0) return 0;
            return (n % 10) + SumOfDigits(n / 10);
        }

        // 4. Is Palindrome (Recursive)
        public bool IsPalindrome(string s, int start, int end)
        {
            if (start >= end) return true; // Base case: crossed pointers
            if (char.ToLower(s[start]) != char.ToLower(s[end])) return false;
            return IsPalindrome(s, start + 1, end - 1);
        }
    }
}
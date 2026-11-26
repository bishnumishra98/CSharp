// Leetcode: 50. Pow(x, n)   --->   Implement pow(x, n), which calculates x raised to the power n (i.e., x^n).

// Example 1:
// Input: x = 2.00000, n = 10
// Output: 1024.00000

// Example 2:
// Input: x = 2.10000, n = 3
// Output: 9.26100

// Example 3:
// Input: x = 2.00000, n = -2
// Output: 0.25000
// Explanation: 2^-2 = 1/2^2 = 1/4 = 0.25

// Constraints: -2^31 <= n <= 2^31 - 1

// Extension: Modular exponentiation   --->   Find (a^b) % M. e.g., a = 2, b = 30, M = 10^9 + 7  ->  73741817
// (See '01_ModularArithmetic.cs' for why we use % M.)

// Negative power: x^(-n) = (1/x)^n. So if n < 0, change x to 1/x and n to -n.
// Why 'long N = n' ? If n = int.MinValue (-2147483648), then -n = 2147483648 doesn't fit in int.


using System;

public class Solution
{
	// Brute force: Multiply x, n times.
	// Problem: n can be 2 * 10^9, so 2 billion multiplications. Too slow (Time Limit Exceeded).
	// T.C: O(n)
	// S.C: O(1)
	public double MyPow_BruteForce(double x, int n)
	{
		long N = n;
		if (N < 0)
		{
			x = 1 / x;
			N = -N;
		}

		double ans = 1;
		for (long i = 0; i < N; i++) ans = ans * x;
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Fast exponentiation (Binary exponentiation).
	// Idea: Halve the power at every step.
	//       • If n is even: x^n = (x * x)^(n / 2).          e.g., 2^10 = 4^5
	//       • If n is odd:  x^n = x * (x * x)^(n / 2).      e.g., 4^5 = 4 * 16^2
	//       So an odd step puts one 'x' aside into 'ans', and then both cases square x and halve n.
	// 1. ans = 1.
	// 2. While n > 0:
	//    -> if n is odd (n & 1 == 1): ans = ans * x
	//    -> x = x * x,  n = n / 2  (n >> 1)
	// e.g., 2^11:   n = 11 (odd): ans = 2,      x = 4,    n = 5
	//               n = 5  (odd): ans = 2*4 = 8, x = 16,  n = 2
	//               n = 2 (even):                x = 256, n = 1
	//               n = 1  (odd): ans = 8*256 = 2048,     n = 0     ->  2^11 = 2048
	// (It reads the bits of n: 11 = 1011 in binary = 8 + 2 + 1, so 2^11 = 2^8 * 2^2 * 2^1.)
	// T.C: O(logn)
	// S.C: O(1)
	public double MyPow(double x, int n)
	{
		long N = n;
		if (N < 0)
		{
			x = 1 / x;
			N = -N;
		}

		double ans = 1;
		while (N > 0)
		{
			if ((N & 1) == 1) ans = ans * x;   // odd power: put one x aside
			x = x * x;
			N = N >> 1;                        // N = N / 2
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Extension: Modular exponentiation (a^b) % M. Same as MyPow(), but take % M after every multiplication.
	// a and ans always stay below M (10^9 + 7), so a * a and ans * a always fit in a long.
	// T.C: O(logb)
	// S.C: O(1)
	public long ModPow(long a, long b, long M)
	{
		long ans = 1;
		a = a % M;

		while (b > 0)
		{
			if ((b & 1) == 1) ans = (ans * a) % M;
			a = (a * a) % M;
			b = b >> 1;
		}
		return ans;
	}
}

// Built-in in C#:
// • Math.Pow(x, n): power for double. (Fine for real code, but Leetcode wants us to write it ourselves.)
// • System.Numerics.BigInteger.ModPow(a, b, M): modular power for very big numbers.


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().MyPow_BruteForce(2.0, 10));          // o/p: 1024
		Console.WriteLine(new Solution().MyPow(2.0, 10));                     // o/p: 1024
		Console.WriteLine(new Solution().ModPow(2, 30, 1_000_000_007));       // o/p: 73741817
	}
}

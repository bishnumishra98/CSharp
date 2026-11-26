// Prime Number   --->   Given an integer n, check whether it is a prime number.
// A prime number is a number greater than 1 that has only 2 factors: 1 and itself. e.g., 2, 3, 5, 7, 11, 13, ...

// Example 1:
// Input: n = 31
// Output: Prime number

// Example 2:
// Input: n = 12
// Output: Not a prime number
// Explanation: 12 has factors 1, 2, 3, 4, 6, 12.


using System;

public class Solution
{
	// Brute force: Check every number from 2 to n - 1. If any of them divides n, n is not prime.
	// T.C: O(n)
	// S.C: O(1)
	public bool IsPrime_BruteForce(int n)
	{
		if (n <= 1) return false;   // 0, 1 and negative numbers are not prime

		for (int i = 2; i < n; i++)
		{
			if (n % i == 0) return false;   // found a factor
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Check only up to sqrt(n).
	// Why ? Factors always come in pairs: if n = a * b, then one of a or b is <= sqrt(n).
	// e.g., n = 36: (1, 36), (2, 18), (3, 12), (4, 9), (6, 6). After 6 (= sqrt(36)), the pairs just repeat in reverse.
	// So if no number from 2 to sqrt(n) divides n, nothing bigger will either.
	// Loop condition: We want i <= sqrt(n), i.e., i * i <= n. But i * i can go out of int range for big n.
	//                 So write it as 'i <= n / i', which means the same thing and never overflows.
	// T.C: O(sqrt(n))
	// S.C: O(1)
	public bool IsPrime(int n)
	{
		if (n <= 1) return false;

		for (int i = 2; i <= n / i; i++)
		{
			if (n % i == 0) return false;
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		int n = 31;

		Console.WriteLine(new Solution().IsPrime_BruteForce(n) ? "Prime number" : "Not a prime number");   // o/p: Prime number
		Console.WriteLine(new Solution().IsPrime(n) ? "Prime number" : "Not a prime number");              // o/p: Prime number
	}
}

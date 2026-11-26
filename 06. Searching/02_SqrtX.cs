// Leetcode: 69. Sqrt(x)   --->   Given a non-negative integer x, return the square root of x rounded down to the
// nearest integer. The returned integer should be non-negative as well.
// You must not use any built-in exponent function or operator.
// For example, do not use pow(x, 0.5) in c++ or x ** 0.5 in python.

// Example 1:
// Input: x = 4
// Output: 2
// Explanation: The square root of 4 is 2, so we return 2.

// Example 2:
// Input: x = 8
// Output: 2
// Explanation: The square root of 8 is 2.82842..., and since we round it down to the nearest integer, 2 is returned.

// Extension: Return the square root of x up to a given number of decimal places (precision).
// Input: x = 68, precision = 3
// Output: 8.246


using System;

public class Solution
{
	// Brute force: Try every number i = 0, 1, 2, ... while i * i <= x. The last such i is the answer.
	// T.C: O(sqrt(x))
	// S.C: O(1)
	public int MySqrt_BruteForce(int x)
	{
		long i = 0;   // long, because i * i can go out of int range
		while ((i + 1) * (i + 1) <= x) i++;
		return (int)i;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search on answer (search space).
	// The square root of x lies somewhere between 0 and x. This range is sorted, so we can apply binary search on it.
	// 1. Initialize start = 0, end = x. Find mid, and think of 'mid' as the square root:
	//    Case 1: mid * mid == x  ->  Exact square root found. Return mid.
	//    Case 2: mid * mid > x   ->  mid is too big. Go left: end = mid - 1.
	//    Case 3: mid * mid < x   ->  mid can be the answer (rounded down), so store it in 'ans'.
	//                                But a bigger answer may exist, so go right: start = mid + 1.
	// 2. When the loop ends, 'ans' has the largest number whose square is <= x. Return it.
	//    e.g., x = 8: 2 * 2 = 4 < 8 (ans = 2), 3 * 3 = 9 > 8. So the answer is 2.
	// T.C: O(logx)
	// S.C: O(1)
	public int MySqrt(int x)
	{
		long start = 0, end = x;   // long, because mid * mid can go out of int range
		long ans = 0;

		while (start <= end)
		{
			long mid = start + (end - start) / 2;

			if (mid * mid == x) return (int)mid;
			else if (mid * mid > x) end = mid - 1;
			else
			{
				ans = mid;
				start = mid + 1;
			}
		}

		return (int)ans;
	}

// ------------------------------------------------------------------------------

	// Extension: Square root up to 'precision' decimal places.
	// 1. First find the integer part using MySqrt(). e.g., x = 68 -> 8.
	// 2. Then find each decimal digit one by one, using a step of 0.1, then 0.01, then 0.001, and so on.
	//    For each step, keep adding the step while (ans * ans <= x).
	//    e.g., x = 68:  8.1, 8.2 are fine, but 8.3 * 8.3 = 68.89 > 68. So ans = 8.2.
	//                   8.21, 8.22, 8.23, 8.24 are fine, but 8.25 * 8.25 > 68. So ans = 8.24, and so on.
	// T.C: O(logx + 10 * precision);   each decimal place takes at most 10 tries
	// S.C: O(1)
	public double MySqrt_Decimal(int x, int precision)
	{
		double ans = MySqrt(x);
		double step = 1;

		for (int i = 0; i < precision; i++)
		{
			step /= 10;   // 0.1, 0.01, 0.001, ...
			while ((ans + step) * (ans + step) <= x) ans += step;
		}

		return Math.Round(ans, precision);   // rounding only removes tiny floating point errors like 8.24599999
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().MySqrt_BruteForce(8));     // o/p: 2
		Console.WriteLine(new Solution().MySqrt(8));                // o/p: 2
		Console.WriteLine(new Solution().MySqrt_Decimal(68, 3));    // o/p: 8.246
	}
}

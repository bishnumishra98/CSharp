// Leetcode: 29. Divide Two Integers   --->   Given two integers dividend and divisor, divide two integers without using
// multiplication, division, and mod operator.
// The integer division should truncate toward zero, which means losing its fractional part. For example, 8.345 would
// be truncated to 8, and -2.7335 would be truncated to -2.
// Return the quotient after dividing dividend by divisor.
// Note: Assume we are dealing with an environment that could only store integers within the 32-bit signed integer
// range: [-2^31, 2^31 - 1]. For this problem, if the quotient is strictly greater than 2^31 - 1, then return 2^31 - 1,
// and if the quotient is strictly less than -2^31, then return -2^31.

// Example 1:
// Input: dividend = 10, divisor = 3
// Output: 3
// Explanation: 10/3 = 3.33333.. which is truncated to 3.

// Example 2:
// Input: dividend = 7, divisor = -3
// Output: -2
// Explanation: 7/-3 = -2.33333.. which is truncated to -2.

// Extension: Return the quotient up to a given number of decimal places (precision).
// Input: dividend = 22, divisor = 3, precision = 3
// Output: 7.333

// Common steps for all approaches:
// 1. Overflow case: -2147483648 / -1 = 2147483648, which is bigger than int.MaxValue. So return int.MaxValue.
// 2. Find the sign of the answer: negative if exactly one of them is negative.
// 3. Work with absolute values as 'long'. Why long? Math.Abs(int.MinValue) = 2147483648 doesn't fit in int.
// 4. Find the quotient of the absolute values, and put the sign back.


using System;

public class Solution
{
	// Brute force: Keep subtracting divisor from dividend, and count how many times we could subtract.
	// e.g., 10 / 3: 10 -> 7 -> 4 -> 1, subtracted 3 times. Quotient = 3.
	// Problem: For 2147483647 / 1, it subtracts 2 billion times. Too slow (Time Limit Exceeded).
	// T.C: O(dividend / divisor)
	// S.C: O(1)
	public int Divide_BruteForce(int dividend, int divisor)
	{
		if (dividend == int.MinValue && divisor == -1) return int.MaxValue;

		int sign = (dividend < 0) ^ (divisor < 0) ? -1 : 1;
		long a = Math.Abs((long)dividend), b = Math.Abs((long)divisor);

		long quotient = 0;
		while (a >= b)
		{
			a -= b;
			quotient++;
		}

		return (int)(quotient * sign);
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Binary search on answer (but it uses multiplication, so it breaks the problem's rule).
	// The quotient lies between 0 and the dividend. Think of 'mid' as the quotient:
	// Case 1: mid * divisor == dividend  ->  Exact quotient. Return mid.
	// Case 2: mid * divisor > dividend   ->  mid is too big. Go left: end = mid - 1.
	// Case 3: mid * divisor < dividend   ->  mid can be the answer (rounded down), so store it in 'ans'.
	//                                        Try a bigger one: start = mid + 1.
	// (Same idea as '02_SqrtX.cs', where we checked mid * mid.)
	// T.C: O(log(dividend))
	// S.C: O(1)
	public int Divide_BinarySearch(int dividend, int divisor)
	{
		if (dividend == int.MinValue && divisor == -1) return int.MaxValue;

		int sign = (dividend < 0) ^ (divisor < 0) ? -1 : 1;
		long a = Math.Abs((long)dividend), b = Math.Abs((long)divisor);

		long start = 0, end = a;
		long ans = 0;

		while (start <= end)
		{
			long mid = start + (end - start) / 2;

			if (mid * b == a) return (int)(mid * sign);
			else if (mid * b > a) end = mid - 1;
			else
			{
				ans = mid;
				start = mid + 1;
			}
		}

		return (int)(ans * sign);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Bit manipulation (no multiplication, no division).
	// Problem with brute force: It subtracts only 1 divisor at a time.
	// Solution: Subtract BIG multiples of divisor at once: divisor * 2^i. And divisor * 2^i is just (divisor << i).
	// Any quotient can be written in binary, e.g., 10 / 3 = 3 = 2^1 + 2^0. So we try every power of 2 from big to small:
	// 1. For i = 31 down to 0: if (a >> i) >= b, it means a >= b * 2^i. So:
	//    -> subtract (b << i) from a
	//    -> add 2^i, i.e., (1 << i), to the quotient
	// 2. Why check (a >> i) >= b and not a >= (b << i) ? Because (b << i) can become a very big number and overflow.
	// e.g., a = 10, b = 3:
	//       i = 1: (10 >> 1) = 5 >= 3  ->  a = 10 - (3 << 1) = 10 - 6 = 4,  quotient = 2
	//       i = 0: (4 >> 0) = 4 >= 3   ->  a = 4 - 3 = 1,                    quotient = 2 + 1 = 3
	// T.C: O(32) = O(1)
	// S.C: O(1)
	public int Divide(int dividend, int divisor)
	{
		if (dividend == int.MinValue && divisor == -1) return int.MaxValue;

		int sign = (dividend < 0) ^ (divisor < 0) ? -1 : 1;
		long a = Math.Abs((long)dividend), b = Math.Abs((long)divisor);

		long quotient = 0;
		for (int i = 31; i >= 0; i--)
		{
			if ((a >> i) >= b)
			{
				a -= b << i;
				quotient += 1L << i;
			}
		}

		return (int)(sign == -1 ? -quotient : quotient);
	}

// ------------------------------------------------------------------------------

	// Extension: Divide up to 'precision' decimal places. e.g., 22 / 3 with precision 3 = 7.333
	// (This part uses multiplication, so it is not for the Leetcode problem. Same idea as MySqrt_Decimal() in '02_SqrtX.cs'.)
	// 1. Work with positive values only. With a negative divisor, the condition '(ans + step) * b <= a' would stay true
	//    forever (infinite loop). So use absolute values, and put the sign back at the end.
	// 2. Integer part: Divide() from above. e.g., 22 / 3 -> 7.
	// 3. Then find each decimal digit one by one, using a step of 0.1, then 0.01, and so on.
	//    For each step, keep adding the step while (ans + step) * b <= a.
	//    e.g., 22 / 3:  7.1, 7.2, 7.3 are fine (7.3 * 3 = 21.9), but 7.4 * 3 = 22.2 > 22. So ans = 7.3, and so on.
	// T.C: O(32 + 10 * precision);   each decimal place takes at most 10 tries
	// S.C: O(1)
	public double Divide_Decimal(int dividend, int divisor, int precision)
	{
		int sign = (dividend < 0) ^ (divisor < 0) ? -1 : 1;
		long a = Math.Abs((long)dividend), b = Math.Abs((long)divisor);

		double ans = Math.Abs((long)Divide(dividend, divisor));
		double step = 1;

		for (int i = 0; i < precision; i++)
		{
			step /= 10;   // 0.1, 0.01, 0.001, ...
			while ((ans + step) * b <= a) ans += step;
		}

		return Math.Round(ans * sign, precision);   // rounding only removes tiny floating point errors like 7.3329999
	}
}


class Program
{
	static void Main()
	{
		int dividend = 7, divisor = -3;

		Console.WriteLine(new Solution().Divide_BruteForce(dividend, divisor));     // o/p: -2
		Console.WriteLine(new Solution().Divide_BinarySearch(dividend, divisor));   // o/p: -2
		Console.WriteLine(new Solution().Divide(dividend, divisor));                // o/p: -2
		Console.WriteLine(new Solution().Divide_Decimal(22, 3, 3));                 // o/p: 7.333
	}
}

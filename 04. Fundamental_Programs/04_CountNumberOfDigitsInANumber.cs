// Count Number of Digits in a Number   --->   Given an integer n, count how many digits it has.

// Example 1:
// Input: n = 12345
// Output: 5

// Example 2:
// Input: n = -125
// Output: 3

// Example 3:
// Input: n = 0
// Output: 1


using System;

public class Solution
{
	// Loop approach: Keep removing the last digit (n = n / 10) and count how many times we can do it.
	// e.g., 125 -> 12 -> 1 -> 0, removed 3 times. So 3 digits.
	// Negative numbers work too, as -125 / 10 = -12 (C# division cuts towards 0).
	// Edge case: 0 has 1 digit, but the loop would run 0 times. So handle it separately.
	// Note: C# needs a real bool in conditions, so C++'s 'while(n)' becomes 'while (n != 0)'.
	// T.C: O(logn);   base 10, i.e., number of digits
	// S.C: O(1)
	public int CountDigits_Loop(int n)
	{
		if (n == 0) return 1;

		int digits = 0;
		while (n != 0)
		{
			digits++;
			n = n / 10;
		}

		return digits;
	}

// ------------------------------------------------------------------------------

	// Math approach: Number of digits = floor(log10(n)) + 1.
	// e.g., log10(125) = 2.09..., floor = 2, so digits = 3.   log10(1000) = 3, so digits = 4.
	// log10 doesn't work on 0 or negative numbers. So handle 0 separately, and take the absolute value first.
	// Why 'Math.Abs((long)n)' ? -int.MinValue doesn't fit in int, but it fits in long.
	// T.C: O(1)
	// S.C: O(1)
	public int CountDigits(int n)
	{
		if (n == 0) return 1;
		return (int)Math.Log10(Math.Abs((long)n)) + 1;
	}
}

// Shortcut in C#: Math.Abs((long)n).ToString().Length   (without Abs, the '-' sign is also counted)


class Program
{
	static void Main()
	{
		int n = -125;

		Console.WriteLine(new Solution().CountDigits_Loop(n));   // o/p: 3
		Console.WriteLine(new Solution().CountDigits(n));        // o/p: 3
	}
}

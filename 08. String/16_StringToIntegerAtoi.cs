// Leetcode: 8. String to Integer (atoi)   --->   Implement the myAtoi(string s) function, which converts a string to a
// 32-bit signed integer.
// The algorithm for myAtoi(string s) is as follows:
// 1. Whitespace: Ignore any leading whitespace (" ").
// 2. Signedness: Determine the sign by checking if the next character is '-' or '+', assuming positivity if neither
//    present.
// 3. Conversion: Read the integer by skipping leading zeros until a non-digit character is encountered or the end of
//    the string is reached. If no digits were read, then the result is 0.
// 4. Rounding: If the integer is out of the 32-bit signed integer range [-2^31, 2^31 - 1], then round the integer to
//    remain in the range. Specifically, integers less than -2^31 should be rounded to -2^31, and integers greater than
//    2^31 - 1 should be rounded to 2^31 - 1.
// Return the integer as the final result.

// Example 1:
// Input: s = "42"
// Output: 42

// Example 2:
// Input: s = " -042"
// Output: -42

// Example 3:
// Input: s = "1337c0d3"
// Output: 1337

// Example 4:
// Input: s = "0-1"
// Output: 0

// Example 5:
// Input: s = "words and 987"
// Output: 0

// Why not int.Parse() / int.TryParse() ? They need the WHOLE string to be a number, so "1337c0d3" fails. This problem
// wants us to read digits only until the first non-digit, so we write it by hand.
// Note: Check digits with (c >= '0' && c <= '9'), not char.IsDigit(c). char.IsDigit() is also true for digits of other
//       languages (like '٣', Arabic 3), and (c - '0') would give a wrong number for them.


using System;

public class Solution
{
	// Easier version: Build the number in a long, and clamp as soon as it leaves the int range.
	// Why stop early, and not just clamp at the end ? The string can have 200 digits, which doesn't fit even in a long.
	// T.C: O(n)
	// S.C: O(1)
	public int MyAtoi_Long(string s)
	{
		int i = 0, n = s.Length;

		while (i < n && s[i] == ' ') i++;                // 1. skip leading spaces

		int sign = 1;                                    // 2. read the sign
		if (i < n && (s[i] == '+' || s[i] == '-'))
		{
			if (s[i] == '-') sign = -1;
			i++;
		}

		long result = 0;                                 // 3. read digits
		while (i < n && s[i] >= '0' && s[i] <= '9')
		{
			result = result * 10 + (s[i] - '0');
			if (sign * result > int.MaxValue) return int.MaxValue;   // 4. clamp
			if (sign * result < int.MinValue) return int.MinValue;
			i++;
		}

		return (int)(sign * result);
	}

// ------------------------------------------------------------------------------

	// Optimal version: Use only int, and check for overflow BEFORE it happens (same idea as
	// '04. Fundamental Programs\18_ReverseAnInteger.cs').
	// 'result * 10 + digit' goes above int.MaxValue exactly when result > (int.MaxValue - digit) / 10.
	// If that happens, return int.MaxValue for a positive number, and int.MinValue for a negative one.
	// (For a negative number, -2147483648 also lands here, and returning int.MinValue is exactly right.)
	// T.C: O(n)
	// S.C: O(1)
	public int MyAtoi(string s)
	{
		int i = 0, n = s.Length;

		while (i < n && s[i] == ' ') i++;

		int sign = 1;
		if (i < n && (s[i] == '+' || s[i] == '-'))
		{
			if (s[i] == '-') sign = -1;
			i++;
		}

		int result = 0;
		while (i < n && s[i] >= '0' && s[i] <= '9')
		{
			int digit = s[i] - '0';
			if (result > (int.MaxValue - digit) / 10) return sign == 1 ? int.MaxValue : int.MinValue;

			result = result * 10 + digit;
			i++;
		}

		return result * sign;
	}
}


class Program
{
	static void Main()
	{
		string s = "1337c0d3";

		Console.WriteLine(new Solution().MyAtoi_Long(s));   // o/p: 1337
		Console.WriteLine(new Solution().MyAtoi(s));        // o/p: 1337
	}
}

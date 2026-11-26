// Binary to Decimal   --->   Given a binary number, convert it into its decimal value.

// Example 1:
// Input: binary = 1001
// Output: 9
// Explanation: (1 * 2^3) + (0 * 2^2) + (0 * 2^1) + (1 * 2^0) = 8 + 0 + 0 + 1 = 9

// Example 2:
// Input: binary = 1101
// Output: 13


using System;

public class Solution
{
	// Approach 1: The binary is given as a NUMBER made of 0s and 1s (e.g., 1001). Go from right to left.
	// 1. Take the last digit (n % 10). Multiply it with its place value: 1, 2, 4, 8, ... (powers of 2), and add it.
	// 2. Remove the last digit (n / 10), and move to the next place value (place * 2).
	// Note: We don't use Math.Pow(2, i) here. It works with double, and can give values like 7.9999 instead of 8.
	//       Multiplying 'place' by 2 every time is simpler and exact.
	// T.C: O(d);   where d = number of digits in the binary
	// S.C: O(1)
	public int BinaryToDecimal_Number(long n)
	{
		int ans = 0, place = 1;

		while (n != 0)
		{
			int bit = (int)(n % 10);
			ans = ans + bit * place;
			place = place * 2;
			n = n / 10;
		}

		return ans;
	}

// ------------------------------------------------------------------------------

	// Approach 2: The binary is given as a STRING (e.g., "1001"). Go from left to right.
	// For every new bit: ans = ans * 2 + bit. (Multiplying by 2 shifts all old bits one place to the left.)
	// e.g., "1101":  0 * 2 + 1 = 1  ->  1 * 2 + 1 = 3  ->  3 * 2 + 0 = 6  ->  6 * 2 + 1 = 13
	// (c - '0') turns the char '0' or '1' into the number 0 or 1.
	// T.C: O(d);   where d = length of the string
	// S.C: O(1)
	public int BinaryToDecimal(string s)
	{
		int ans = 0;
		foreach (char c in s)
		{
			ans = ans * 2 + (c - '0');
		}
		return ans;
	}
}

// Built-in in C#: Convert.ToInt32("1001", 2) gives 9. (2 is the base.)


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().BinaryToDecimal_Number(1001));   // o/p: 9
		Console.WriteLine(new Solution().BinaryToDecimal("1001"));        // o/p: 9
	}
}

// Decimal to Binary   --->   Given a non-negative integer n, convert it into its binary representation.

// Example 1:
// Input: n = 8
// Output: 1000

// Example 2:
// Input: n = 13
// Output: 1101
// Explanation: 13 = 8 + 4 + 1 = (1 * 2^3) + (1 * 2^2) + (0 * 2^1) + (1 * 2^0)

// How to convert on paper: Keep dividing by 2 and note the remainders. Read the remainders from bottom to top.
//   13 / 2 = 6, remainder 1
//    6 / 2 = 3, remainder 0
//    3 / 2 = 1, remainder 1
//    1 / 2 = 0, remainder 1    ->  read upwards: 1101
// In code: (n & 1) gives the remainder of n / 2 (the last bit), and (n >> 1) is n / 2.


using System;
using System.Text;

public class Solution
{
	// Approach 1: Build the binary as a NUMBER made of 0s and 1s, e.g., 13 -> 1101 (one thousand one hundred one).
	// 1. Take the last bit (n & 1), and put it at the correct place: ones, tens, hundreds, ... (place = 1, 10, 100, ...).
	// 2. Remove the last bit (n >> 1), and move to the next place (place * 10).
	// Problem: A long can hold only 19 digits, so this works only for n < 2^19 (= 524288). For bigger n, use a string.
	// T.C: O(logn);   base 2, i.e., number of bits
	// S.C: O(1)
	public long DecimalToBinary_Number(int n)
	{
		long ans = 0, place = 1;

		while (n != 0)
		{
			int bit = n & 1;
			ans = ans + bit * place;
			place = place * 10;
			n = n >> 1;
		}

		return ans;
	}

// ------------------------------------------------------------------------------

	// Approach 2: Build the binary as a STRING (works for any non-negative int).
	// We get the bits from right to left (last bit first). So append them, and reverse the string at the end.
	// T.C: O(logn)
	// S.C: O(logn);   for the string
	public string DecimalToBinary(int n)
	{
		if (n == 0) return "0";

		StringBuilder sb = new StringBuilder();
		while (n != 0)
		{
			sb.Append(n & 1);   // last bit
			n = n >> 1;         // remove the last bit
		}

		char[] arr = sb.ToString().ToCharArray();
		Array.Reverse(arr);
		return new string(arr);
	}
}

// Built-in in C#: Convert.ToString(13, 2) gives "1101". (2 is the base. Use 8 for octal, 16 for hexadecimal.)


class Program
{
	static void Main()
	{
		int n = 13;

		Console.WriteLine(new Solution().DecimalToBinary_Number(n));   // o/p: 1101
		Console.WriteLine(new Solution().DecimalToBinary(n));          // o/p: 1101
	}
}

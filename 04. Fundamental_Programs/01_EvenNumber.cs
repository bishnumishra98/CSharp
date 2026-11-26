// Even or Odd   --->   Given an integer n, check whether it is even or odd.

// Example 1:
// Input: n = 4
// Output: Even number

// Example 2:
// Input: n = 7
// Output: Odd number


using System;

public class Solution
{
	// Modulo approach: An even number leaves remainder 0 when divided by 2.
	// Careful: For negative numbers, % keeps the sign, e.g., -3 % 2 = -1 (not 1).
	//          So to check odd, write 'n % 2 != 0', never 'n % 2 == 1'.
	// T.C: O(1)
	// S.C: O(1)
	public bool IsEven_Modulo(int n)
	{
		return n % 2 == 0;
	}

// ------------------------------------------------------------------------------

	// Bitwise approach: In binary, the last (rightmost) bit of every even number is 0, and of every odd number is 1.
	// e.g., 4 = 100, 6 = 110  (last bit 0  ->  even)
	//       5 = 101, 7 = 111  (last bit 1  ->  odd)
	// (n & 1) keeps only the last bit. So if (n & 1) == 0, n is even. It works for negative numbers too.
	// Note: The brackets in '(n & 1) == 0' are needed. Without them, C# reads it as 'n & (1 == 0)', i.e., int & bool,
	//       which is a compile error.
	// T.C: O(1)
	// S.C: O(1)
	public bool IsEven(int n)
	{
		return (n & 1) == 0;
	}
}


class Program
{
	static void Main()
	{
		int n = 4;

		Console.WriteLine(new Solution().IsEven_Modulo(n) ? "Even number" : "Odd number");   // o/p: Even number
		Console.WriteLine(new Solution().IsEven(n) ? "Even number" : "Odd number");          // o/p: Even number
	}
}

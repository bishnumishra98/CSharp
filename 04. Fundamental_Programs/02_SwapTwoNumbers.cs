// Swap Two Numbers   --->   Given two integers a and b, swap their values.

// Example:
// Input: a = 2, b = 3
// Output: a = 3, b = 2

// Note: Each method below returns the swapped pair as a tuple '(int, int)', i.e., 2 values packed together.
//       If a method should swap the caller's own variables, pass them by reference: Swap(ref a, ref b).
//       Without 'ref', the method gets copies of a and b, and the caller's variables don't change.


using System;

public class Solution
{
	// Using a temporary variable: Keep a copy of 'a' in 'temp', so it's not lost when 'a' is overwritten.
	// T.C: O(1)
	// S.C: O(1)
	public (int, int) Swap_Temp(int a, int b)
	{
		int temp = a;   // temp = 2
		a = b;          // a = 3
		b = temp;       // b = 2
		return (a, b);
	}

// ------------------------------------------------------------------------------

	// Using + and - (no extra variable):
	// Problem: For big numbers, 'a + b' can go out of int range. (C# wraps around by default, so the swap still
	//          ends correct, but it's confusing. In C++, this overflow is undefined behaviour.)
	// T.C: O(1)
	// S.C: O(1)
	public (int, int) Swap_Arithmetic(int a, int b)
	{
		a = a + b;   // a = 2 + 3 = 5
		b = a - b;   // b = 5 - 3 = 2
		a = a - b;   // a = 5 - 2 = 3
		return (a, b);
	}

// ------------------------------------------------------------------------------

	// Using XOR (no extra variable, no overflow): x ^ x = 0 and x ^ 0 = x.
	// T.C: O(1)
	// S.C: O(1)
	public (int, int) Swap_XOR(int a, int b)
	{
		a = a ^ b;   // a = 2 ^ 3
		b = a ^ b;   // b = 2 ^ 3 ^ 3 = 2
		a = a ^ b;   // a = 2 ^ 3 ^ 2 = 3
		return (a, b);
	}

// ------------------------------------------------------------------------------

	// Modern C# way: Tuple swap. Write it in 1 line, and the compiler does the temp-variable work for us.
	// Use this one in real code. It is clear, and as fast as the others. (The XOR trick is NOT faster on modern
	// computers; the compiler already makes the temp swap very fast.)
	// T.C: O(1)
	// S.C: O(1)
	public (int, int) Swap(int a, int b)
	{
		(a, b) = (b, a);
		return (a, b);
	}
}


class Program
{
	static void Main()
	{
		int a = 2, b = 3;

		Console.WriteLine(new Solution().Swap_Temp(a, b));         // o/p: (3, 2)
		Console.WriteLine(new Solution().Swap_Arithmetic(a, b));   // o/p: (3, 2)
		Console.WriteLine(new Solution().Swap_XOR(a, b));          // o/p: (3, 2)
		Console.WriteLine(new Solution().Swap(a, b));              // o/p: (3, 2)
	}
}

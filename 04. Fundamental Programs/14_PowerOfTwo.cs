// Leetcode 231. Power of Two   --->   Given an integer n, return true if it is a power of two. Otherwise, return false.
// An integer n is a power of two, if there exists an integer x such that n == 2^x.

// Example 1:
// Input: n = 1
// Output: true
// Explanation: 2^0 = 1

// Example 2:
// Input: n = 16
// Output: true
// Explanation: 2^4 = 16

// Example 3:
// Input: n = 3
// Output: false


using System;

public class Solution
{
	// Brute force: Keep dividing n by 2 while it is even. If we reach 1, n was a power of 2.
	// e.g., 16 -> 8 -> 4 -> 2 -> 1  (true),   12 -> 6 -> 3 (odd, stop, not 1)  (false)
	// T.C: O(logn)
	// S.C: O(1)
	public bool IsPowerOfTwo_Loop(int n)
	{
		if (n <= 0) return false;

		while (n % 2 == 0) n = n / 2;
		return n == 1;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Bit trick.
	// A power of 2 has exactly ONE set bit:  1 = 1,  2 = 10,  4 = 100,  8 = 1000,  16 = 10000
	// n - 1 flips that bit to 0, and all the bits after it to 1:  8 = 1000,  7 = 0111
	// So n & (n - 1) == 0 only for powers of 2.   e.g., 8 & 7 = 1000 & 0111 = 0000
	// For a non-power, some other 1 survives:   e.g., 12 & 11 = 1100 & 1011 = 1000 (not 0)
	// n > 0 is needed, because 0 & -1 = 0 too, but 0 is not a power of 2.
	// (This is the same n & (n - 1) trick from '13_CountAllSetBits.cs': if removing one set bit makes it 0,
	// there was only one set bit.)
	// T.C: O(1)
	// S.C: O(1)
	public bool IsPowerOfTwo(int n)
	{
		return n > 0 && (n & (n - 1)) == 0;
	}
}

// Built-in in C#: System.Numerics.BitOperations.IsPow2(n)


class Program
{
	static void Main()
	{
		int n = 16;

		Console.WriteLine(new Solution().IsPowerOfTwo_Loop(n));   // o/p: True
		Console.WriteLine(new Solution().IsPowerOfTwo(n));        // o/p: True
	}
}

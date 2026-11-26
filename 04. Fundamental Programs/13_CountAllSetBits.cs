// Leetcode: 191. Number of 1 Bits   --->   Given a positive integer n, write a function that returns the number of
// set bits in its binary representation (also known as the Hamming weight).
// (A "set bit" is a bit with value 1.)

// Example 1:
// Input: n = 11
// Output: 3
// Explanation: The input binary string 1011 has a total of three set bits.

// Example 2:
// Input: n = 128
// Output: 1
// Explanation: The input binary string 10000000 has a total of one set bit.


using System;

public class Solution
{
	// Brute force: Check all 32 bits of the int, one by one.
	// (1 << i) is a number with only the i-th bit set. e.g., 1 << 3 = 1000.
	// So (n & (1 << i)) is non-zero only if the i-th bit of n is 1.
	// Note: C# needs a bool in 'if', so C++'s 'if(n & (1 << i))' becomes 'if ((n & (1 << i)) != 0)'.
	// T.C: O(32) = O(1)
	// S.C: O(1)
	public int CountSetBits_Loop(int n)
	{
		int count = 0;
		for (int i = 0; i < 32; i++)
		{
			if ((n & (1 << i)) != 0) count++;
		}
		return count;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Brian Kernighan's algorithm.
	// Problem with brute force: It always checks all 32 bits, even if n has only 1 or 2 set bits.
	// Solution: n & (n - 1) removes the RIGHTMOST set bit of n. So count how many times we can do it until n becomes 0.
	// Why does it work ? n - 1 flips the rightmost set bit to 0, and all the 0s after it to 1. The & then clears all of them.
	// e.g., n = 12 (1100):  n - 1 = 1011,  n & (n - 1) = 1000   (rightmost 1 is gone)
	//       n = 8  (1000):  n - 1 = 0111,  n & (n - 1) = 0000   (done, count = 2)
	// T.C: O(k);   where k = number of set bits (at most 32)
	// S.C: O(1)
	public int CountSetBits(int n)
	{
		int count = 0;
		while (n != 0)
		{
			n = n & (n - 1);
			count++;
		}
		return count;
	}
}

// Built-in in C#: System.Numerics.BitOperations.PopCount((uint)n). ("Pop count" = population count = number of 1s.)


class Program
{
	static void Main()
	{
		int n = 11;

		Console.WriteLine(new Solution().CountSetBits_Loop(n));   // o/p: 3
		Console.WriteLine(new Solution().CountSetBits(n));        // o/p: 3
	}
}

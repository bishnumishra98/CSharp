// Leetcode: 342. Power of Four   --->   Given an integer n, return true if it is a power of four. Otherwise, return
// false.
// An integer n is a power of four, if there exists an integer x such that n == 4^x.

// Example 1:
// Input: n = 16
// Output: true

// Example 2:
// Input: n = 5
// Output: false

// Example 3:
// Input: n = 1
// Output: true


using System;

public class Solution
{
	// Brute force: Keep dividing n by 4 while it is divisible by 4. If we reach 1, n was a power of 4.
	// T.C: O(logn)
	// S.C: O(1)
	public bool IsPowerOfFour_Loop(int n)
	{
		if (n <= 0) return false;

		while (n % 4 == 0) n = n / 4;
		return n == 1;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Every power of 4 is also a power of 2. So:
	// 1. First check that n is a power of 2: n > 0 && (n & (n - 1)) == 0.   (see '14_PowerOfTwo.cs')
	// 2. Then remove the powers of 2 that are NOT powers of 4 (2, 8, 32, 128, ...). Trick: (n - 1) % 3 == 0.
	//    Powers of 4:  1 - 1 = 0,  4 - 1 = 3,  16 - 1 = 15,  64 - 1 = 63     ->  all divisible by 3
	//    Others:       2 - 1 = 1,  8 - 1 = 7,  32 - 1 = 31,  128 - 1 = 127   ->  none divisible by 3
	//    (Why ? 4 % 3 = 1, so 4^x % 3 = 1, i.e., 4^x - 1 is divisible by 3. But 2 * 4^x % 3 = 2, so it never is.)
	// Another way for step 2: The only set bit must be at an EVEN position (0, 2, 4, ...): 1 = 1, 4 = 100, 16 = 10000.
	//    0x55555555 = 0101 0101 ... 0101 has 1s only at even positions. So check (n & 0x55555555) != 0.
	// T.C: O(1)
	// S.C: O(1)
	public bool IsPowerOfFour(int n)
	{
		return n > 0 && (n & (n - 1)) == 0 && (n - 1) % 3 == 0;
	}
}


class Program
{
	static void Main()
	{
		int n = 16;

		Console.WriteLine(new Solution().IsPowerOfFour_Loop(n));   // o/p: True
		Console.WriteLine(new Solution().IsPowerOfFour(n));        // o/p: True
	}
}

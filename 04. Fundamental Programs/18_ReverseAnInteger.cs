// Leetcode: 7. Reverse Integer   --->   Given a signed 32-bit integer x, return x with its digits reversed. If reversing
// x causes the value to go outside the signed 32-bit integer range [-2^31, 2^31 - 1], then return 0.
// Assume the environment does not allow you to store 64-bit integers (signed or unsigned).

// Example 1:
// Input: x = 123
// Output: 321

// Example 2:
// Input: x = -123
// Output: -321

// Example 3:
// Input: x = 120
// Output: 21

// Reverse logic: Take the last digit (x % 10), add it to the end of 'ans' (ans * 10 + digit), remove it (x / 10).
// e.g., 123:  ans = 3  ->  32  ->  321
// Negative numbers: In C#, % and / keep the sign. e.g., -123 % 10 = -3, -123 / 10 = -12.
//                   So the same logic gives -3 -> -32 -> -321 directly. No need to remove and add back the sign.


using System;

public class Solution
{
	// Using long: Build the answer in a long, so it can't overflow. At the end, if it doesn't fit in int, return 0.
	// e.g., 1534236469 reversed = 9646324351, which is bigger than int.MaxValue (2147483647). So return 0.
	// Problem: The problem says we can't use 64-bit numbers (long).
	// T.C: O(logx);   base 10, i.e., number of digits
	// S.C: O(1)
	public int Reverse_Long(int x)
	{
		long ans = 0;
		while (x != 0)
		{
			ans = ans * 10 + x % 10;
			x = x / 10;
		}

		if (ans > int.MaxValue || ans < int.MinValue) return 0;
		return (int)ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Use only int, and check for overflow BEFORE it happens.
	// 'ans * 10' overflows if ans > int.MaxValue / 10 (= 214748364), or ans < int.MinValue / 10 (= -214748364).
	// So check this before every 'ans * 10'. If it would overflow, return 0.
	// What if ans == 214748364 exactly ? Then ans * 10 + digit overflows only if digit > 7. But this happens only at the
	// 10th digit, and the 10th (last) digit we add is the FIRST digit of x. A 10 digit int starts with 1 or 2, so it
	// is never > 7. That's why the check above is enough.
	// T.C: O(logx);   base 10, i.e., number of digits
	// S.C: O(1)
	public int Reverse(int x)
	{
		int ans = 0;
		while (x != 0)
		{
			if (ans > int.MaxValue / 10 || ans < int.MinValue / 10) return 0;

			ans = ans * 10 + x % 10;
			x = x / 10;
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int x = -123;

		Console.WriteLine(new Solution().Reverse_Long(x));   // o/p: -321
		Console.WriteLine(new Solution().Reverse(x));        // o/p: -321
	}
}

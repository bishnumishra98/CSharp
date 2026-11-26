// Modular Arithmetic   --->   Many problems say "the answer can be very large, so return it modulo 10^9 + 7".
// This file explains what that means, and how to do it without overflow.

// Example:
// Input: n = 25
// Output: 440732388
// Explanation: 25! = 15511210043330985984000000, which doesn't fit even in a long. 25! % (10^9 + 7) = 440732388.

// ● What is a % m ?  The remainder when a is divided by m. For a >= 0, the answer is always from 0 to m - 1.
//   e.g., 17 % 5 = 2,  10 % 5 = 0

// ● Why 10^9 + 7 ?
//   I.  It is big, so answers stay different from each other, but small enough that (a % M) * (b % M) < 10^18 fits in a
//       long (max ~9.2 * 10^18).
//   II. It is a prime number, which some advanced formulas need.

// ● Rules (M = the modulo number). We can take % at every step instead of only at the end, and the answer stays the same:
//   I.   (a + b) % M = ((a % M) + (b % M)) % M
//   II.  (a * b) % M = ((a % M) * (b % M)) % M
//   III. (a - b) % M = ((a % M) - (b % M) + M) % M     (+ M keeps it from going negative)
//   IV.  (a % M) % M = a % M                           (taking % again changes nothing)
//   Note: Division does NOT work this way. (a / b) % M is not ((a % M) / (b % M)) % M. It needs "modular inverse"
//         (an advanced topic).

// ● C# gotcha with negative numbers: % keeps the sign of the left number. e.g., -7 % 3 = -1 (not 2).
//   To always get an answer from 0 to m - 1, write: ((a % m) + m) % m.  e.g., ((-7 % 3) + 3) % 3 = (-1 + 3) % 3 = 2


using System;

public class Solution
{
	private const long MOD = 1_000_000_007;   // 10^9 + 7. The _ is just a separator, like a comma in 1,000,000,007.

	// Without modulo: Multiply everything, and take % only at the end.
	// Problem: long can hold only up to 20!. From 21! onwards, the multiplication overflows silently, so the final % is
	//          taken on a garbage number, and the answer is wrong.
	// T.C: O(n)
	// S.C: O(1)
	public long FactorialMod_Wrong(int n)
	{
		long fact = 1;
		for (int i = 2; i <= n; i++) fact = fact * i;   // overflows for n > 20
		return fact % MOD;
	}

// ------------------------------------------------------------------------------

	// With modulo at every step: Use rule II, (a * b) % M = ((a % M) * (b % M)) % M.
	// 'fact' is always < 10^9 + 7 after the %, and i <= n, so fact * i always fits in a long.
	// T.C: O(n)
	// S.C: O(1)
	public long FactorialMod(int n)
	{
		long fact = 1;
		for (int i = 2; i <= n; i++) fact = (fact * i) % MOD;
		return fact;
	}

// ------------------------------------------------------------------------------

	// Always-positive modulo (for negative numbers too).
	// T.C: O(1)
	// S.C: O(1)
	public long PositiveMod(long a, long m)
	{
		return ((a % m) + m) % m;
	}
}


class Program
{
	static void Main()
	{
		int n = 25;

		Console.WriteLine(new Solution().FactorialMod_Wrong(n));   // o/p: 332217180   (wrong, because 25! overflowed)
		Console.WriteLine(new Solution().FactorialMod(n));         // o/p: 440732388   (correct)
		Console.WriteLine(new Solution().PositiveMod(-7, 3));      // o/p: 2
	}
}

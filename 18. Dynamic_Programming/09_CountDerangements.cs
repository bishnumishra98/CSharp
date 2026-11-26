// GFG: Dearrangement of Balls (Count Derangements)   --->   There are n balls numbered 1..n and n baskets numbered 1..n.
// Basket i is meant for ball i. Count the ways to put the balls in the baskets (one ball per basket) so that NO ball is
// in its own basket.

// Example 1:
// Input: n = 2
// Output: 1   (ball 1 in basket 2, ball 2 in basket 1)

// Example 2:
// Input: n = 3
// Output: 2   (baskets get balls [3, 1, 2] or [2, 3, 1])

// Problem link: https://www.geeksforgeeks.org/problems/dearrangement-of-balls0918/1

// The recursion: D(n) = the number of derangements of n balls.
// Ball 1 can go into any basket except its own: n - 1 choices. Say it goes into basket i. Now look at ball i:
//   I.  Ball i goes into basket 1 (they swap). Balls 1 and i are both done, and the other n - 2 balls must still avoid
//       their own baskets:                                                             D(n - 2) ways
//   II. Ball i does NOT go into basket 1. Then basket 1 is "forbidden" for ball i, exactly like its own basket would
//       be. So the n - 1 balls left (all but ball 1) each have one forbidden basket:   D(n - 1) ways
//   D(n) = (n - 1) * (D(n - 1) + D(n - 2))
//   D(1) = 0 (ball 1 has only its own basket),  D(2) = 1
// e.g., D(3) = 2 * (D(2) + D(1)) = 2 * (1 + 0) = 2.   D(4) = 3 * (2 + 1) = 9.

// The answer grows like n! / e, so 'long' is used (it fits up to n = 20).


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n);   recursion stack
	public long CountDer_Recursion(int n)
	{
		if (n == 1) return 0;
		if (n == 2) return 1;
		return (n - 1) * (CountDer_Recursion(n - 1) + CountDer_Recursion(n - 2));
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public long CountDer_Memoization(int n)
	{
		long[] dp = new long[n + 1];
		Array.Fill(dp, -1);
		return Der(n, dp);
	}

	private long Der(int n, long[] dp)
	{
		if (n == 1) return 0;
		if (n == 2) return 1;
		if (dp[n] != -1) return dp[n];
		return dp[n] = (n - 1) * (Der(n - 1, dp) + Der(n - 2, dp));
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public long CountDer_Tabulation(int n)
	{
		if (n == 1) return 0;

		long[] dp = new long[n + 1];
		dp[1] = 0;
		dp[2] = 1;
		for (int i = 3; i <= n; i++) dp[i] = (i - 1) * (dp[i - 1] + dp[i - 2]);
		return dp[n];
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(1)
	public long CountDer_SpaceOptimized(int n)
	{
		if (n == 1) return 0;

		long prev2 = 0, prev = 1;   // D(1), D(2)
		for (int i = 3; i <= n; i++)
		{
			long curr = (i - 1) * (prev + prev2);
			prev2 = prev;
			prev = curr;
		}
		return prev;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().CountDer_Recursion(4));        // o/p: 9
		Console.WriteLine(new Solution().CountDer_Memoization(4));      // o/p: 9
		Console.WriteLine(new Solution().CountDer_Tabulation(4));       // o/p: 9
		Console.WriteLine(new Solution().CountDer_SpaceOptimized(4));   // o/p: 9
	}
}

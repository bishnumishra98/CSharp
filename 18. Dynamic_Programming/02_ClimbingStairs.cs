// Leetcode: 70. Climbing Stairs   --->   You are climbing a staircase. It takes n steps to reach the top. Each time you can
// climb 1 or 2 steps. In how many distinct ways can you climb to the top ?

// Example 1:
// Input: n = 2
// Output: 2   (1 + 1, or 2)

// Example 2:
// Input: n = 3
// Output: 3   (1 + 1 + 1, 1 + 2, 2 + 1)

// First understand '01_DPIntroduction.cs'.

// The recursion: To stand on step n, the LAST move was either 1 step (from n - 1) or 2 steps (from n - 2). Every way
// to reach n - 1 plus one 1-step, and every way to reach n - 2 plus one 2-step, are all different ways. So:
//   ways(n) = ways(n - 1) + ways(n - 2),   ways(1) = 1, ways(2) = 2.
// That is Fibonacci with different starting values.


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n);   recursion stack (the C++ comment said O(2^n))
	public int ClimbStairs_Recursion(int n)
	{
		if (n <= 2) return n;
		return ClimbStairs_Recursion(n - 1) + ClimbStairs_Recursion(n - 2);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n);   dp + recursion stack
	public int ClimbStairs_Memoization(int n)
	{
		int[] dp = new int[n + 1];
		Array.Fill(dp, -1);
		return Ways(n, dp);
	}

	private int Ways(int n, int[] dp)
	{
		if (n <= 2) return n;
		if (dp[n] != -1) return dp[n];
		return dp[n] = Ways(n - 1, dp) + Ways(n - 2, dp);   // store and return in one line
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public int ClimbStairs_Tabulation(int n)
	{
		if (n <= 2) return n;

		int[] dp = new int[n + 1];
		dp[1] = 1;
		dp[2] = 2;
		for (int i = 3; i <= n; i++) dp[i] = dp[i - 1] + dp[i - 2];
		return dp[n];
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(1)
	public int ClimbStairs_SpaceOptimized(int n)
	{
		if (n <= 2) return n;

		int prev2 = 1, prev = 2;   // ways(1), ways(2)
		for (int i = 3; i <= n; i++)
		{
			int curr = prev + prev2;
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
		Console.WriteLine(new Solution().ClimbStairs_Recursion(4));        // o/p: 5
		Console.WriteLine(new Solution().ClimbStairs_Memoization(4));      // o/p: 5
		Console.WriteLine(new Solution().ClimbStairs_Tabulation(4));       // o/p: 5
		Console.WriteLine(new Solution().ClimbStairs_SpaceOptimized(4));   // o/p: 5
	}
}

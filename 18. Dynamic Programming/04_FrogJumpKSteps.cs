// Frog Jump with K Steps   --->   Same as '03_FrogJump.cs', but from stair i the frog can jump to ANY of i + 1, i + 2,
// ..., i + k. A jump from i to j costs |heights[i] - heights[j]|. Return the minimum total energy to reach stair n - 1.

// Example 1:
// Input: heights = [10, 50, 30, 40, 10, 40, 60, 50, 20, 60], k = 3
// Output: 50   (stairs 0 -> 3 -> 5 -> 7 -> 9: |40 - 10| + |40 - 40| + |50 - 40| + |60 - 50| = 30 + 0 + 10 + 10)

// Example 2:
// Input: heights = [10, 40, 30, 10], k = 2   (k = 2 is exactly the previous problem)
// Output: 40   (0 -> 2 -> 3: |30 - 10| + |10 - 30| = 20 + 20)

// The recursion: the last jump into stair i came from i - 1, i - 2, ..., or i - k (only those >= 0). Try them all:
//   f(i) = min over j = 1..k (with i - j >= 0) of:  f(i - j) + |h[i] - h[i - j]|
//   f(0) = 0


// Dry run image (in this folder): 04_FrogJumpKSteps.jpg

using System;

public class Solution
{
	// T.C: O(k^n);   every call tries up to k smaller calls
	// S.C: O(n);     recursion stack
	// Note: The C++ comments said T.C O(2^n) and S.C O(2^n). With k choices per call the tree grows like k^n, and the
	//       stack is at most n deep.
	public int FrogJump_Recursion(int[] heights, int k)
	{
		return MinEnergy(heights.Length - 1, k, heights);
	}

	private int MinEnergy(int i, int k, int[] h)
	{
		if (i == 0) return 0;

		int best = int.MaxValue;
		for (int j = 1; j <= k && i - j >= 0; j++)
		{
			best = Math.Min(best, MinEnergy(i - j, k, h) + Math.Abs(h[i] - h[i - j]));
		}
		return best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * k);   n states, each tries k jumps (the C++ comment said O(n))
	// S.C: O(n)
	public int FrogJump_Memoization(int[] heights, int k)
	{
		int[] dp = new int[heights.Length];
		Array.Fill(dp, -1);
		return MinEnergy(heights.Length - 1, k, heights, dp);
	}

	private int MinEnergy(int i, int k, int[] h, int[] dp)
	{
		if (i == 0) return 0;
		if (dp[i] != -1) return dp[i];

		int best = int.MaxValue;
		for (int j = 1; j <= k && i - j >= 0; j++)
		{
			best = Math.Min(best, MinEnergy(i - j, k, h, dp) + Math.Abs(h[i] - h[i - j]));
		}
		return dp[i] = best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * k)
	// S.C: O(n)
	// Space optimization would keep only the last k values: O(k) space. That helps when k is much smaller than n, but
	// when k is close to n it is O(n) anyway, so the full dp array is kept here.
	public int FrogJump_Tabulation(int[] heights, int k)
	{
		int n = heights.Length;
		int[] dp = new int[n];
		dp[0] = 0;

		for (int i = 1; i < n; i++)
		{
			dp[i] = int.MaxValue;
			for (int j = 1; j <= k && i - j >= 0; j++)
			{
				dp[i] = Math.Min(dp[i], dp[i - j] + Math.Abs(heights[i] - heights[i - j]));
			}
		}
		return dp[n - 1];
	}
}


class Program
{
	static void Main()
	{
		int[] heights = { 10, 50, 30, 40, 10, 40, 60, 50, 20, 60 };

		Console.WriteLine(new Solution().FrogJump_Recursion(heights, 3));     // o/p: 50
		Console.WriteLine(new Solution().FrogJump_Memoization(heights, 3));   // o/p: 50
		Console.WriteLine(new Solution().FrogJump_Tabulation(heights, 3));    // o/p: 50
	}
}

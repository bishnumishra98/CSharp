// Frog Jump   --->   A frog is on stair 0 of a staircase with n stairs (0 to n - 1). heights[i] is the height of stair i.
// From stair i, the frog can jump to stair i + 1 or i + 2. A jump from i to j costs |heights[i] - heights[j]| energy.
// Return the MINIMUM total energy for the frog to reach stair n - 1.

// Example 1:
// Input: heights = [10, 20, 30, 10]
// Output: 20   (0 -> 1 costs |20 - 10| = 10, then 1 -> 3 costs |10 - 20| = 10)

// Example 2:
// Input: heights = [10, 50, 10]
// Output: 0    (0 -> 2 directly costs |10 - 10| = 0)

// Example 3:
// Input: heights = [10, 20, 30, 40, 10, 40, 60, 50]
// Output: 40

// Problem link: https://www.naukri.com/code360/problems/frog-jump_3621012

// Like '02_ClimbingStairs.cs', but instead of COUNTING ways, we want the CHEAPEST way. So + becomes min.

// The recursion: f(i) = the minimum energy to reach stair i. The frog came to i either from i - 1 or from i - 2:
//   f(i) = min( f(i - 1) + |h[i] - h[i - 1]|,     (last jump was 1 step)
//               f(i - 2) + |h[i] - h[i - 2]| )    (last jump was 2 steps, only if i >= 2)
//   f(0) = 0   (the frog starts there)
// Why only ONE base case (f(0)), and the "if i >= 2" check, instead of 2 base cases (f(0) and f(1)) ? The next file
// allows jumps of up to k steps. With this style, the code works for any k; with base cases we would need k of them.


// Dry run image (in this folder): 03_FrogJump.jpg

using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n);   recursion stack
	public int FrogJump_Recursion(int[] heights)
	{
		return MinEnergy(heights.Length - 1, heights);
	}

	private int MinEnergy(int i, int[] h)
	{
		if (i == 0) return 0;

		int oneStep = MinEnergy(i - 1, h) + Math.Abs(h[i] - h[i - 1]);
		int twoSteps = int.MaxValue;   // no 2-step jump into stair 1
		if (i >= 2) twoSteps = MinEnergy(i - 2, h) + Math.Abs(h[i] - h[i - 2]);

		return Math.Min(oneStep, twoSteps);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n);   dp + recursion stack
	public int FrogJump_Memoization(int[] heights)
	{
		int[] dp = new int[heights.Length];
		Array.Fill(dp, -1);
		return MinEnergy(heights.Length - 1, heights, dp);
	}

	private int MinEnergy(int i, int[] h, int[] dp)
	{
		if (i == 0) return 0;
		if (dp[i] != -1) return dp[i];

		int oneStep = MinEnergy(i - 1, h, dp) + Math.Abs(h[i] - h[i - 1]);
		int twoSteps = int.MaxValue;
		if (i >= 2) twoSteps = MinEnergy(i - 2, h, dp) + Math.Abs(h[i] - h[i - 2]);

		return dp[i] = Math.Min(oneStep, twoSteps);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public int FrogJump_Tabulation(int[] heights)
	{
		int n = heights.Length;
		int[] dp = new int[n];
		dp[0] = 0;

		for (int i = 1; i < n; i++)
		{
			int oneStep = dp[i - 1] + Math.Abs(heights[i] - heights[i - 1]);
			int twoSteps = int.MaxValue;
			if (i >= 2) twoSteps = dp[i - 2] + Math.Abs(heights[i] - heights[i - 2]);
			dp[i] = Math.Min(oneStep, twoSteps);
		}
		return dp[n - 1];
	}

// ------------------------------------------------------------------------------

	// dp[i] only needs dp[i - 1] (prev) and dp[i - 2] (prev2).
	// T.C: O(n)
	// S.C: O(1)
	public int FrogJump_SpaceOptimized(int[] heights)
	{
		int prev = 0, prev2 = 0;

		for (int i = 1; i < heights.Length; i++)
		{
			int oneStep = prev + Math.Abs(heights[i] - heights[i - 1]);
			int twoSteps = int.MaxValue;
			if (i >= 2) twoSteps = prev2 + Math.Abs(heights[i] - heights[i - 2]);

			int curr = Math.Min(oneStep, twoSteps);
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
		int[] heights = { 10, 20, 30, 40, 10, 40, 60, 50 };

		Console.WriteLine(new Solution().FrogJump_Recursion(heights));        // o/p: 40
		Console.WriteLine(new Solution().FrogJump_Memoization(heights));      // o/p: 40
		Console.WriteLine(new Solution().FrogJump_Tabulation(heights));       // o/p: 40
		Console.WriteLine(new Solution().FrogJump_SpaceOptimized(heights));   // o/p: 40
	}
}

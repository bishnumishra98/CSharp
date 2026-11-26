// Leetcode: 746. Min Cost Climbing Stairs   --->   You are given an array cost, where cost[i] is the cost of step i. Once
// you pay a step's cost, you can climb 1 or 2 steps from it. You can start at step 0 or step 1.
// Return the minimum cost to reach the TOP (the spot just after the last step, index n).

// Example 1:
// Input: cost = [10,15,20]
// Output: 15   (start at step 1, pay 15, climb 2 steps to the top)

// Example 2:
// Input: cost = [1,100,1,1,1,100,1,1,100,1]
// Output: 6    (step on every 1, skip every 100)

// Close to '03_FrogJump.cs': here the cost is paid for the step you STAND on, not for the jump.

// The recursion: f(i) = the minimum cost to stand on step i AND pay for it.
//   f(i) = cost[i] + min(f(i - 1), f(i - 2))      (you came from 1 or 2 steps below)
//   f(0) = cost[0], f(1) = cost[1]                (you may start on either one)
// The top (index n) is reached from step n - 1 or step n - 2, so the answer is min(f(n - 1), f(n - 2)).


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n);   recursion stack (the C++ comment said O(2^(n + 1)))
	public int MinCostClimbingStairs_Recursion(int[] cost)
	{
		int n = cost.Length;
		return Math.Min(MinCost(n - 1, cost), MinCost(n - 2, cost));
	}

	private int MinCost(int i, int[] cost)
	{
		if (i <= 1) return cost[i];
		return cost[i] + Math.Min(MinCost(i - 1, cost), MinCost(i - 2, cost));
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public int MinCostClimbingStairs_Memoization(int[] cost)
	{
		int n = cost.Length;
		int[] dp = new int[n];
		Array.Fill(dp, -1);
		return Math.Min(MinCost(n - 1, cost, dp), MinCost(n - 2, cost, dp));
	}

	private int MinCost(int i, int[] cost, int[] dp)
	{
		if (i <= 1) return cost[i];
		if (dp[i] != -1) return dp[i];
		return dp[i] = cost[i] + Math.Min(MinCost(i - 1, cost, dp), MinCost(i - 2, cost, dp));
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public int MinCostClimbingStairs_Tabulation(int[] cost)
	{
		int n = cost.Length;
		int[] dp = new int[n];
		dp[0] = cost[0];
		dp[1] = cost[1];

		for (int i = 2; i < n; i++) dp[i] = cost[i] + Math.Min(dp[i - 1], dp[i - 2]);

		return Math.Min(dp[n - 1], dp[n - 2]);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(1)
	public int MinCostClimbingStairs_SpaceOptimized(int[] cost)
	{
		int prev2 = cost[0], prev = cost[1];

		for (int i = 2; i < cost.Length; i++)
		{
			int curr = cost[i] + Math.Min(prev, prev2);
			prev2 = prev;
			prev = curr;
		}
		return Math.Min(prev, prev2);
	}
}


class Program
{
	static void Main()
	{
		int[] cost = { 1, 100, 1, 1, 1, 100, 1, 1, 100, 1 };

		Console.WriteLine(new Solution().MinCostClimbingStairs_Recursion(cost));        // o/p: 6
		Console.WriteLine(new Solution().MinCostClimbingStairs_Memoization(cost));      // o/p: 6
		Console.WriteLine(new Solution().MinCostClimbingStairs_Tabulation(cost));       // o/p: 6
		Console.WriteLine(new Solution().MinCostClimbingStairs_SpaceOptimized(cost));   // o/p: 6
	}
}

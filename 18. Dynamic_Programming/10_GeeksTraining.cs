// GFG: Geek's Training (also called Ninja's Training)   --->   A training lasts n days. Each day the Geek does exactly
// ONE of 3 activities: running (0), fighting (1) or learning (2). points[day][activity] is what it earns.
// The Geek can NOT do the same activity on two days in a row. Return the maximum total points.

// Example 1:
// Input: points = [[1,2,5],[3,1,1],[3,3,3]]
// Output: 11   (day 0: learning 5, day 1: running 3, day 2: fighting 3)

// Example 2:
// Input: points = [[1,2,5],[3,1,1],[3,2,3]]
// Output: 11   (day 0: learning 5, day 1: running 3, day 2: learning 3)

// Problem link: https://www.geeksforgeeks.org/problems/geeks-training/1

// Why not just take the biggest value every day (greedy) ? Today's best choice may block a much better choice
// tomorrow. e.g., [[10, 50, 1], [5, 100, 11]]: greedy takes 50 then 11 = 61, but 10 + 100 = 110 is better.

// The recursion: the 1D index (day) is not enough, we also need to know what was done the day AFTER (it is not allowed
// today). So the state has 2 parts: f(day, last) = the max points for days 0..day, if activity 'last' is not allowed
// on 'day'. last = 3 means "nothing is blocked" (used for the very last day, where no next day exists).
//   f(day, last) = max over task != last of:  points[day][task] + f(day - 1, task)
//   f(0, last)   = max over task != last of:  points[0][task]
// Answer: f(n - 1, 3).


// Dry run image (in this folder): 10_GeeksTraining_tabulation_SO.jpg   (why one row of 4 values is enough)

using System;

public class Solution
{
	// T.C: O(3 * 2^(n - 1)) = O(2^n);   the last day tries 3 tasks, every other day 2 (one is blocked)
	// S.C: O(n);   recursion stack
	// Note: The C++ comment said O(3^n). Only 2 tasks are allowed per day (except the first call), so it is O(2^n).
	public int MaximumPoints_Recursion(int[][] points)
	{
		return MaxPoints(points.Length - 1, 3, points);
	}

	private int MaxPoints(int day, int last, int[][] points)
	{
		int best = 0;
		for (int task = 0; task < 3; task++)
		{
			if (task == last) continue;

			int earned = points[day][task];
			if (day > 0) earned += MaxPoints(day - 1, task, points);
			best = Math.Max(best, earned);
		}
		return best;
	}

// ------------------------------------------------------------------------------

	// dp[day][last]: 4 columns, for last = 0, 1, 2 and 3 ("nothing blocked").
	// T.C: O(n * 4 * 3) = O(n)
	// S.C: O(n * 4) = O(n);   dp + recursion stack
	public int MaximumPoints_Memoization(int[][] points)
	{
		int n = points.Length;
		int[][] dp = new int[n][];
		for (int i = 0; i < n; i++)
		{
			dp[i] = new int[4];
			Array.Fill(dp[i], -1);
		}
		return MaxPoints(n - 1, 3, points, dp);
	}

	private int MaxPoints(int day, int last, int[][] points, int[][] dp)
	{
		if (dp[day][last] != -1) return dp[day][last];

		int best = 0;
		for (int task = 0; task < 3; task++)
		{
			if (task == last) continue;

			int earned = points[day][task];
			if (day > 0) earned += MaxPoints(day - 1, task, points, dp);
			best = Math.Max(best, earned);
		}
		return dp[day][last] = best;
	}

// ------------------------------------------------------------------------------

	// Fill day 0 first (the base case), then every day from the previous day's row.
	// T.C: O(n * 4 * 3) = O(n)
	// S.C: O(n * 4) = O(n)
	public int MaximumPoints_Tabulation(int[][] points)
	{
		int n = points.Length;
		int[,] dp = new int[n, 4];   // a 2D array: dp[day, last]

		for (int last = 0; last < 4; last++)
		{
			for (int task = 0; task < 3; task++)
			{
				if (task != last) dp[0, last] = Math.Max(dp[0, last], points[0][task]);
			}
		}

		for (int day = 1; day < n; day++)
		{
			for (int last = 0; last < 4; last++)
			{
				for (int task = 0; task < 3; task++)
				{
					if (task != last) dp[day, last] = Math.Max(dp[day, last], points[day][task] + dp[day - 1, task]);
				}
			}
		}
		return dp[n - 1, 3];
	}

// ------------------------------------------------------------------------------

	// Every row only needs the row before it (see the image). Keep one row 'prev' of 4 values, and build the next row
	// in 'curr'.
	// T.C: O(n * 4 * 3) = O(n)
	// S.C: O(4) = O(1)
	public int MaximumPoints_SpaceOptimized(int[][] points)
	{
		int[] prev = new int[4];
		for (int last = 0; last < 4; last++)
		{
			for (int task = 0; task < 3; task++)
			{
				if (task != last) prev[last] = Math.Max(prev[last], points[0][task]);
			}
		}

		for (int day = 1; day < points.Length; day++)
		{
			int[] curr = new int[4];
			for (int last = 0; last < 4; last++)
			{
				for (int task = 0; task < 3; task++)
				{
					if (task != last) curr[last] = Math.Max(curr[last], points[day][task] + prev[task]);
				}
			}
			prev = curr;
		}
		return prev[3];
	}
}


class Program
{
	static void Main()
	{
		int[][] points = { new[] { 1, 2, 5 }, new[] { 3, 1, 1 }, new[] { 3, 3, 3 } };

		Console.WriteLine(new Solution().MaximumPoints_Recursion(points));        // o/p: 11
		Console.WriteLine(new Solution().MaximumPoints_Memoization(points));      // o/p: 11
		Console.WriteLine(new Solution().MaximumPoints_Tabulation(points));       // o/p: 11
		Console.WriteLine(new Solution().MaximumPoints_SpaceOptimized(points));   // o/p: 11
	}
}

// Leetcode: 64. Minimum Path Sum   --->   Given an m x n grid of non-negative numbers, find a path from the top-left to
// the bottom-right with the SMALLEST sum of the numbers on it. You can only move right or down.

// Example 1:
// Input: grid = [[1,3,1],[1,5,1],[4,2,1]]
// Output: 7   (1 -> 3 -> 1 -> 1 -> 1)

// Example 2:
// Input: grid = [[1,2,3],[4,5,6]]
// Output: 12  (1 -> 2 -> 3 -> 6)

// Like '11_UniquePaths.cs', but instead of COUNTING paths (sum of both ways), take the CHEAPEST one (min of both ways).

// The recursion: f(i, j) = the smallest path sum from (0, 0) to (i, j).
//   f(i, j) = grid[i][j] + min(f(i - 1, j), f(i, j - 1))
//   f(0, 0) = grid[0][0],   f(outside) = a huge number (so that way is never picked by min)
// Why 1e9 and not int.MaxValue for "huge" ? Adding grid[i][j] to int.MaxValue overflows to a negative number, which min
// would then pick. 1e9 + a small sum still fits in an int.


using System;

public class Solution
{
	private const int INF = 1_000_000_000;

	// T.C: O(2^(m + n))
	// S.C: O(m + n)
	public int MinPathSum_Recursion(int[][] grid)
	{
		return MinSum(grid.Length - 1, grid[0].Length - 1, grid);
	}

	private int MinSum(int i, int j, int[][] grid)
	{
		if (i < 0 || j < 0) return INF;
		if (i == 0 && j == 0) return grid[0][0];
		return grid[i][j] + Math.Min(MinSum(i - 1, j, grid), MinSum(i, j - 1, grid));
	}

// ------------------------------------------------------------------------------

	// T.C: O(m * n)
	// S.C: O(m * n)
	public int MinPathSum_Memoization(int[][] grid)
	{
		int m = grid.Length, n = grid[0].Length;
		int[,] dp = new int[m, n];
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++) dp[i, j] = -1;
		}
		return MinSum(m - 1, n - 1, grid, dp);
	}

	private int MinSum(int i, int j, int[][] grid, int[,] dp)
	{
		if (i < 0 || j < 0) return INF;
		if (i == 0 && j == 0) return grid[0][0];
		if (dp[i, j] != -1) return dp[i, j];
		return dp[i, j] = grid[i][j] + Math.Min(MinSum(i - 1, j, grid, dp), MinSum(i, j - 1, grid, dp));
	}

// ------------------------------------------------------------------------------

	// T.C: O(m * n)
	// S.C: O(m * n)
	public int MinPathSum_Tabulation(int[][] grid)
	{
		int m = grid.Length, n = grid[0].Length;
		int[,] dp = new int[m, n];

		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (i == 0 && j == 0)
				{
					dp[i, j] = grid[0][0];
					continue;
				}
				int up = i > 0 ? dp[i - 1, j] : INF;
				int left = j > 0 ? dp[i, j - 1] : INF;
				dp[i, j] = grid[i][j] + Math.Min(up, left);
			}
		}
		return dp[m - 1, n - 1];
	}

// ------------------------------------------------------------------------------

	// T.C: O(m * n)
	// S.C: O(n)
	public int MinPathSum_SpaceOptimized(int[][] grid)
	{
		int m = grid.Length, n = grid[0].Length;
		int[] prev = new int[n];

		for (int i = 0; i < m; i++)
		{
			int[] curr = new int[n];
			for (int j = 0; j < n; j++)
			{
				if (i == 0 && j == 0)
				{
					curr[j] = grid[0][0];
					continue;
				}
				int up = i > 0 ? prev[j] : INF;
				int left = j > 0 ? curr[j - 1] : INF;
				curr[j] = grid[i][j] + Math.Min(up, left);
			}
			prev = curr;
		}
		return prev[n - 1];
	}
}


class Program
{
	static void Main()
	{
		int[][] grid = { new[] { 1, 3, 1 }, new[] { 1, 5, 1 }, new[] { 4, 2, 1 } };

		Console.WriteLine(new Solution().MinPathSum_Recursion(grid));        // o/p: 7
		Console.WriteLine(new Solution().MinPathSum_Memoization(grid));      // o/p: 7
		Console.WriteLine(new Solution().MinPathSum_Tabulation(grid));       // o/p: 7
		Console.WriteLine(new Solution().MinPathSum_SpaceOptimized(grid));   // o/p: 7
	}
}

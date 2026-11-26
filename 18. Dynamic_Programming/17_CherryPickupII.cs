// Leetcode: 1463. Cherry Pickup II   --->   grid[i][j] = the cherries in cell (i, j). Two robots collect cherries:
// Robot 1 starts at the top-left (0, 0), Robot 2 at the top-right (0, cols - 1).
// ● From (i, j) a robot moves to (i + 1, j - 1), (i + 1, j) or (i + 1, j + 1) (down-left, down, down-right).
// ● A robot takes all cherries of every cell it passes. If BOTH robots are on the same cell, only one takes them.
// ● Robots can not leave the grid, and both must reach the bottom row.
// Return the maximum cherries both robots can collect together.

// Example 1:
// Input: grid = [[3,1,1],[2,5,1],[1,5,5],[2,1,1]]
// Output: 24   (Robot 1: 3 + 2 + 5 + 2 = 12, Robot 2: 1 + 5 + 5 + 1 = 12)

// Example 2:
// Input: grid = [[1,0,0,0,0,0,1],[2,0,0,0,0,3,0],[2,0,9,0,0,0,0],[0,3,0,5,4,0,0],[1,0,2,3,0,0,6]]
// Output: 28   (Robot 1: 1 + 9 + 5 + 2 = 17, Robot 2: 1 + 3 + 4 + 3 = 11)

// 3D DP: two things move at the same time.
// Why not find the best path for Robot 1, remove its cherries, then the best path for Robot 2 (greedy) ? Robot 1's
// best path can take cherries that Robot 2 needed more. They must be planned TOGETHER.

// The recursion: both robots go down one row at every step, so they are always on the SAME row i. The state is
// (i, j1, j2): the row and the 2 columns. f(i, j1, j2) = the most cherries from row i to the bottom.
//   cherries here = grid[i][j1] + grid[i][j2]   (only grid[i][j1] if j1 == j2)
//   f(i, j1, j2) = cherries here + max over the 3 x 3 = 9 moves (d1, d2 in {-1, 0, +1}) of f(i + 1, j1 + d1, j2 + d2)
//   f(last row, j1, j2) = cherries here
//   f(a column outside the grid) = a huge NEGATIVE number (that move is never picked by max)
// Fixed start (0, 0, cols - 1), free end, so the recursion goes from the top down.


using System;

public class Solution
{
	private const int NEG = -100_000_000;

	private int Here(int[][] grid, int i, int j1, int j2) => j1 == j2 ? grid[i][j1] : grid[i][j1] + grid[i][j2];

	// T.C: O(9^n);   n = rows, 9 moves per row
	// S.C: O(n)
	public int CherryPickup_Recursion(int[][] grid)
	{
		return MaxCherries(0, 0, grid[0].Length - 1, grid);
	}

	private int MaxCherries(int i, int j1, int j2, int[][] grid)
	{
		int cols = grid[0].Length;
		if (j1 < 0 || j2 < 0 || j1 >= cols || j2 >= cols) return NEG;
		if (i == grid.Length - 1) return Here(grid, i, j1, j2);

		int best = NEG;
		for (int d1 = -1; d1 <= 1; d1++)
		{
			for (int d2 = -1; d2 <= 1; d2++) best = Math.Max(best, MaxCherries(i + 1, j1 + d1, j2 + d2, grid));
		}
		return Here(grid, i, j1, j2) + best;
	}

// ------------------------------------------------------------------------------

	// dp[i, j1, j2]: a 3D array.
	// T.C: O(n * m * m * 9);   n = rows, m = columns
	// S.C: O(n * m * m)
	public int CherryPickup_Memoization(int[][] grid)
	{
		int n = grid.Length, m = grid[0].Length;
		int[,,] dp = new int[n, m, m];
		for (int i = 0; i < n; i++)
		{
			for (int a = 0; a < m; a++)
			{
				for (int b = 0; b < m; b++) dp[i, a, b] = -1;   // cherries are never negative, so -1 is a safe marker
			}
		}
		return MaxCherries(0, 0, m - 1, grid, dp);
	}

	private int MaxCherries(int i, int j1, int j2, int[][] grid, int[,,] dp)
	{
		int cols = grid[0].Length;
		if (j1 < 0 || j2 < 0 || j1 >= cols || j2 >= cols) return NEG;
		if (i == grid.Length - 1) return Here(grid, i, j1, j2);
		if (dp[i, j1, j2] != -1) return dp[i, j1, j2];

		int best = NEG;
		for (int d1 = -1; d1 <= 1; d1++)
		{
			for (int d2 = -1; d2 <= 1; d2++) best = Math.Max(best, MaxCherries(i + 1, j1 + d1, j2 + d2, grid, dp));
		}
		return dp[i, j1, j2] = Here(grid, i, j1, j2) + best;
	}

// ------------------------------------------------------------------------------

	// Fill the last row first (the base case), then go up row by row.
	// T.C: O(n * m * m * 9)
	// S.C: O(n * m * m)
	public int CherryPickup_Tabulation(int[][] grid)
	{
		int n = grid.Length, m = grid[0].Length;
		int[,,] dp = new int[n, m, m];

		for (int j1 = 0; j1 < m; j1++)
		{
			for (int j2 = 0; j2 < m; j2++) dp[n - 1, j1, j2] = Here(grid, n - 1, j1, j2);
		}

		for (int i = n - 2; i >= 0; i--)
		{
			for (int j1 = 0; j1 < m; j1++)
			{
				for (int j2 = 0; j2 < m; j2++)
				{
					int best = NEG;
					for (int d1 = -1; d1 <= 1; d1++)
					{
						for (int d2 = -1; d2 <= 1; d2++)
						{
							int a = j1 + d1, b = j2 + d2;
							if (a >= 0 && a < m && b >= 0 && b < m) best = Math.Max(best, dp[i + 1, a, b]);
						}
					}
					dp[i, j1, j2] = Here(grid, i, j1, j2) + best;
				}
			}
		}
		return dp[0, 0, m - 1];
	}

// ------------------------------------------------------------------------------

	// Row i only needs row i + 1: keep one 2D table 'below' (m x m) instead of the 3D one.
	// T.C: O(n * m * m * 9)
	// S.C: O(m * m)
	public int CherryPickup_SpaceOptimized(int[][] grid)
	{
		int n = grid.Length, m = grid[0].Length;
		int[,] below = new int[m, m];

		for (int j1 = 0; j1 < m; j1++)
		{
			for (int j2 = 0; j2 < m; j2++) below[j1, j2] = Here(grid, n - 1, j1, j2);
		}

		for (int i = n - 2; i >= 0; i--)
		{
			int[,] curr = new int[m, m];
			for (int j1 = 0; j1 < m; j1++)
			{
				for (int j2 = 0; j2 < m; j2++)
				{
					int best = NEG;
					for (int d1 = -1; d1 <= 1; d1++)
					{
						for (int d2 = -1; d2 <= 1; d2++)
						{
							int a = j1 + d1, b = j2 + d2;
							if (a >= 0 && a < m && b >= 0 && b < m) best = Math.Max(best, below[a, b]);
						}
					}
					curr[j1, j2] = Here(grid, i, j1, j2) + best;
				}
			}
			below = curr;
		}
		return below[0, m - 1];
	}
}


class Program
{
	static void Main()
	{
		int[][] grid = { new[] { 3, 1, 1 }, new[] { 2, 5, 1 }, new[] { 1, 5, 5 }, new[] { 2, 1, 1 } };

		Console.WriteLine(new Solution().CherryPickup_Recursion(grid));        // o/p: 24
		Console.WriteLine(new Solution().CherryPickup_Memoization(grid));      // o/p: 24
		Console.WriteLine(new Solution().CherryPickup_Tabulation(grid));       // o/p: 24
		Console.WriteLine(new Solution().CherryPickup_SpaceOptimized(grid));   // o/p: 24
	}
}

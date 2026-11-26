// Leetcode: 63. Unique Paths II   --->   Same as '11_UniquePaths.cs', but the grid has OBSTACLES: grid[i][j] = 1 is an
// obstacle, 0 is free. A path can not go through an obstacle. Return the number of paths from top-left to bottom-right.

// Example 1:
// Input: obstacleGrid = [[0,0,0],[0,1,0],[0,0,0]]
// Output: 2   (Right -> Right -> Down -> Down,  Down -> Down -> Right -> Right: both go around the middle)

// Example 2:
// Input: obstacleGrid = [[0,1],[0,0]]
// Output: 1

// The recursion: the same as Unique Paths, with ONE extra base case: an obstacle cell has 0 paths through it.
//   f(i, j) = 0                        if (i, j) is an obstacle
//   f(i, j) = f(i - 1, j) + f(i, j - 1)  otherwise
// Check the obstacle BEFORE the "(0, 0) = 1" case: if the start itself is blocked ([[1]]), the answer is 0.


using System;

public class Solution
{
	// T.C: O(m * n)
	// S.C: O(m * n);   dp + O(m + n) recursion stack
	public int UniquePathsWithObstacles_Memoization(int[][] grid)
	{
		int m = grid.Length, n = grid[0].Length;
		int[,] dp = new int[m, n];
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++) dp[i, j] = -1;
		}
		return Paths(m - 1, n - 1, grid, dp);
	}

	private int Paths(int i, int j, int[][] grid, int[,] dp)
	{
		if (i < 0 || j < 0) return 0;
		if (grid[i][j] == 1) return 0;   // obstacle
		if (i == 0 && j == 0) return 1;
		if (dp[i, j] != -1) return dp[i, j];
		return dp[i, j] = Paths(i - 1, j, grid, dp) + Paths(i, j - 1, grid, dp);
	}

// ------------------------------------------------------------------------------

	// Tabulation with one row (like Unique Paths), and obstacle cells set to 0.
	// T.C: O(m * n)
	// S.C: O(n)
	public int UniquePathsWithObstacles(int[][] grid)
	{
		int m = grid.Length, n = grid[0].Length;
		int[] prev = new int[n];

		for (int i = 0; i < m; i++)
		{
			int[] curr = new int[n];
			for (int j = 0; j < n; j++)
			{
				if (grid[i][j] == 1) curr[j] = 0;
				else if (i == 0 && j == 0) curr[j] = 1;
				else curr[j] = prev[j] + (j > 0 ? curr[j - 1] : 0);
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
		int[][] grid = { new[] { 0, 0, 0 }, new[] { 0, 1, 0 }, new[] { 0, 0, 0 } };

		Console.WriteLine(new Solution().UniquePathsWithObstacles_Memoization(grid));   // o/p: 2
		Console.WriteLine(new Solution().UniquePathsWithObstacles(grid));               // o/p: 2
	}
}

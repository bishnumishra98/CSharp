// Leetcode: 62. Unique Paths   --->   A robot is at the top-left cell (0, 0) of an m x n grid. It wants to reach the
// bottom-right cell (m - 1, n - 1). It can only move RIGHT or DOWN. Return the number of different paths.

// Example 1:
// Input: m = 3, n = 7
// Output: 28

// Example 2:
// Input: m = 3, n = 2
// Output: 3   (Right -> Down -> Down,  Down -> Down -> Right,  Down -> Right -> Down)

// DP on GRIDS: the state is a cell (i, j).

// The recursion: think BACKWARDS from the end. f(i, j) = the number of paths from (0, 0) to (i, j). The robot entered
// (i, j) either from the cell ABOVE (i - 1, j) or from the cell on the LEFT (i, j - 1). Every path to those cells,
// plus one move, is a path to (i, j):
//   f(i, j) = f(i - 1, j) + f(i, j - 1)
//   f(0, 0) = 1   (one path: stay there),   f(outside the grid) = 0
// Going backwards makes the base case simple: the destination of the recursion is the fixed cell (0, 0).


// Dry run images (in this folder): 11_UniquePaths.jpg, 11_UniquePaths_SO.jpg (how one row is enough)

using System;

public class Solution
{
	// T.C: O(2^(m + n));   every call makes 2 calls, and a path has m + n - 2 moves
	// S.C: O(m + n);       recursion stack (the longest path)
	public int UniquePaths_Recursion(int m, int n)
	{
		return Paths(m - 1, n - 1);
	}

	private int Paths(int i, int j)
	{
		if (i < 0 || j < 0) return 0;
		if (i == 0 && j == 0) return 1;
		return Paths(i - 1, j) + Paths(i, j - 1);
	}

// ------------------------------------------------------------------------------

	// T.C: O(m * n)
	// S.C: O(m * n);   dp + O(m + n) recursion stack
	public int UniquePaths_Memoization(int m, int n)
	{
		int[,] dp = new int[m, n];
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++) dp[i, j] = -1;
		}
		return Paths(m - 1, n - 1, dp);
	}

	private int Paths(int i, int j, int[,] dp)
	{
		if (i < 0 || j < 0) return 0;
		if (i == 0 && j == 0) return 1;
		if (dp[i, j] != -1) return dp[i, j];
		return dp[i, j] = Paths(i - 1, j, dp) + Paths(i, j - 1, dp);
	}

// ------------------------------------------------------------------------------

	// Fill row by row, left to right, so 'up' and 'left' are always ready.
	// T.C: O(m * n)
	// S.C: O(m * n)
	public int UniquePaths_Tabulation(int m, int n)
	{
		int[,] dp = new int[m, n];

		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (i == 0 && j == 0)
				{
					dp[i, j] = 1;
					continue;
				}
				int up = i > 0 ? dp[i - 1, j] : 0;
				int left = j > 0 ? dp[i, j - 1] : 0;
				dp[i, j] = up + left;
			}
		}
		return dp[m - 1, n - 1];
	}

// ------------------------------------------------------------------------------

	// A cell needs only the row ABOVE (prev) and the cell to its LEFT in the current row (curr). See the image.
	// T.C: O(m * n)
	// S.C: O(n)
	public int UniquePaths_SpaceOptimized(int m, int n)
	{
		int[] prev = new int[n];   // row i - 1 (all 0 above the grid)

		for (int i = 0; i < m; i++)
		{
			int[] curr = new int[n];
			for (int j = 0; j < n; j++)
			{
				if (i == 0 && j == 0)
				{
					curr[j] = 1;
					continue;
				}
				curr[j] = prev[j] + (j > 0 ? curr[j - 1] : 0);
			}
			prev = curr;
		}
		return prev[n - 1];
	}

// ------------------------------------------------------------------------------

	// Optimal (Math): Every path has exactly (m - 1) DOWN moves and (n - 1) RIGHT moves, m + n - 2 moves in total. A path
	// is decided only by WHICH of those moves are the downs. So the answer is "choose m - 1 out of m + n - 2":
	//   C(m + n - 2, m - 1)
	// Compute it step by step: C = 1, then for i = 1..r: C = C * (N - r + i) / i. Each step's result is itself a
	// binomial coefficient, so the division is always exact. Use r = min(m, n) - 1 for fewer steps.
	// e.g., m = 3, n = 7: C(8, 2) = 8 * 7 / 2 = 28.
	// T.C: O(min(m, n))
	// S.C: O(1)
	public int UniquePaths(int m, int n)
	{
		int N = m + n - 2, r = Math.Min(m, n) - 1;
		long result = 1;
		for (int i = 1; i <= r; i++) result = result * (N - r + i) / i;
		return (int)result;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().UniquePaths_Recursion(3, 7));        // o/p: 28
		Console.WriteLine(new Solution().UniquePaths_Memoization(3, 7));      // o/p: 28
		Console.WriteLine(new Solution().UniquePaths_Tabulation(3, 7));       // o/p: 28
		Console.WriteLine(new Solution().UniquePaths_SpaceOptimized(3, 7));   // o/p: 28
		Console.WriteLine(new Solution().UniquePaths(3, 7));                  // o/p: 28
	}
}

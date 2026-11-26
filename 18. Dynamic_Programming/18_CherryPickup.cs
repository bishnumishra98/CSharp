// Leetcode: 741. Cherry Pickup   --->   An n x n grid: 0 = empty cell, 1 = a cherry, -1 = a thorn (blocked).
// ● Go from (0, 0) to (n - 1, n - 1) moving only RIGHT or DOWN through non-thorn cells,
// ● then come BACK from (n - 1, n - 1) to (0, 0) moving only LEFT or UP.
// ● A cherry is picked up when you pass it, and the cell becomes empty (it can not be picked twice).
// Return the maximum cherries collected. If there is no valid path to (n - 1, n - 1), return 0.

// Example 1:
// Input: grid = [[0,1,-1],[1,0,-1],[1,1,1]]
// Output: 5   (down, down, right, right picks 4. Back: left, up, up, left picks the 1 at (0, 1).)

// Example 2:
// Input: grid = [[1,1,-1],[1,-1,1],[-1,1,1]]
// Output: 0   (the thorns block every path)

// First understand '17_CherryPickupII.cs'.

// Why not take the best path there, remove its cherries, then the best path back (greedy) ? The best first trip can
// spoil the second. Both trips must be planned TOGETHER.
// Trick 1: The trip back (left / up from the end to the start) is the same as a second trip FORWARD (right / down from
//          the start to the end). So: 2 people walk from (0, 0) to (n - 1, n - 1) at the SAME time, one step each.
//          Both take the cherries of their cells (only once if they stand on the same cell).
// Trick 2: After k steps, each person has made k moves, so i1 + j1 = k = i2 + j2. Knowing i1, j1 and i2 gives
//          j2 = i1 + j1 - i2. So 3 numbers are enough for the state, not 4.
//          (Also: they can only ever share a cell at the same moment, so "same cell" is a correct way to avoid
//           picking a cherry twice.)
// The recursion: f(i1, j1, i2) = the most cherries from these 2 positions to the end.
//   either one is outside the grid or on a thorn:  a huge negative number (that path is not allowed)
//   both at (n - 1, n - 1):                        grid[n - 1][n - 1]
//   otherwise: cherries here + max of the 4 move pairs: (down, down), (down, right), (right, down), (right, right)
// Answer: max(0, f(0, 0, 0))   (negative means no valid path).


using System;

public class Solution
{
	private const int NEG = -100_000_000;
	private static readonly int[] di1 = { 1, 1, 0, 0 }, dj1 = { 0, 0, 1, 1 }, di2 = { 1, 0, 1, 0 };

	// T.C: O(4^(2n));   4 move pairs per step, 2n - 2 steps
	// S.C: O(n);        recursion stack
	public int CherryPickup_Recursion(int[][] grid)
	{
		return Math.Max(0, MaxCherries(0, 0, 0, grid));
	}

	private int MaxCherries(int i1, int j1, int i2, int[][] grid)
	{
		int n = grid.Length, j2 = i1 + j1 - i2;
		if (i1 >= n || j1 >= n || i2 >= n || j2 >= n || grid[i1][j1] == -1 || grid[i2][j2] == -1) return NEG;
		if (i1 == n - 1 && j1 == n - 1) return grid[i1][j1];   // then person 2 is there too (same number of steps)

		int here = grid[i1][j1] + (i1 == i2 ? 0 : grid[i2][j2]);   // same row and same step -> same cell

		int best = NEG;
		for (int d = 0; d < 4; d++) best = Math.Max(best, MaxCherries(i1 + di1[d], j1 + dj1[d], i2 + di2[d], grid));
		return here + best;
	}

// ------------------------------------------------------------------------------

	// dp[i1, j1, i2]: a 3D array (thanks to trick 2; the C++ code also had a 4D version, dp[i1][j1][i2][j2]: O(n^4)).
	// T.C: O(n^3 * 4);   n^3 states, 4 move pairs each
	// S.C: O(n^3)
	// Note: The C++ comments said O(N * M * M) for both versions. The grid is n x n, so the 4D version is O(n^4) and the
	//       3D version is O(n^3).
	public int CherryPickup(int[][] grid)
	{
		int n = grid.Length;
		int[,,] dp = new int[n, n, n];
		for (int a = 0; a < n; a++)
		{
			for (int b = 0; b < n; b++)
			{
				for (int c = 0; c < n; c++) dp[a, b, c] = int.MinValue;   // not solved yet (NEG and 0 are real answers)
			}
		}
		return Math.Max(0, MaxCherries(0, 0, 0, grid, dp));
	}

	private int MaxCherries(int i1, int j1, int i2, int[][] grid, int[,,] dp)
	{
		int n = grid.Length, j2 = i1 + j1 - i2;
		if (i1 >= n || j1 >= n || i2 >= n || j2 >= n || grid[i1][j1] == -1 || grid[i2][j2] == -1) return NEG;
		if (i1 == n - 1 && j1 == n - 1) return grid[i1][j1];
		if (dp[i1, j1, i2] != int.MinValue) return dp[i1, j1, i2];

		int here = grid[i1][j1] + (i1 == i2 ? 0 : grid[i2][j2]);

		int best = NEG;
		for (int d = 0; d < 4; d++) best = Math.Max(best, MaxCherries(i1 + di1[d], j1 + dj1[d], i2 + di2[d], grid, dp));
		return dp[i1, j1, i2] = here + best;
	}
}


class Program
{
	static void Main()
	{
		int[][] grid = { new[] { 0, 1, -1 }, new[] { 1, 0, -1 }, new[] { 1, 1, 1 } };

		Console.WriteLine(new Solution().CherryPickup_Recursion(grid));   // o/p: 5
		Console.WriteLine(new Solution().CherryPickup(grid));             // o/p: 5
	}
}

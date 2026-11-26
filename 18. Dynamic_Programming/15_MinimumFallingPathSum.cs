// Leetcode: 931. Minimum Falling Path Sum   --->   Given an n x n matrix, return the minimum sum of any FALLING path.
// A falling path starts at ANY cell of the first row, and from (row, col) moves to (row + 1, col - 1), (row + 1, col)
// or (row + 1, col + 1) (down-left, down, down-right). It ends at any cell of the last row.

// Example 1:
// Input: matrix = [[2,1,3],[6,5,4],[7,8,9]]
// Output: 13   (1 -> 5 -> 7, or 1 -> 4 -> 8)

// Example 2:
// Input: matrix = [[-19,57],[-40,-5]]
// Output: -59  (-19 -> -40)

// Both the start AND the end are free here. Pick one end to recurse from: f(i, j) = the smallest falling sum from row 0
// down to cell (i, j). The answer is the min of f(n - 1, j) over all cells j of the last row.
//   f(i, j) = matrix[i][j] + min(f(i - 1, j - 1), f(i - 1, j), f(i - 1, j + 1))
//   f(0, j) = matrix[0][j],   f(i, j outside the columns) = a huge number

// Variation (GFG: Path in Matrix): the MAXIMUM falling path sum. Same code with max instead of min.
// Problem link: https://www.geeksforgeeks.org/problems/path-in-matrix3805/1


using System;

public class Solution
{
	private const int INF = 1_000_000_000;

	// T.C: O(n * 3^n);   n starting cells, each a tree with 3 branches per row
	// S.C: O(n)
	public int MinFallingPathSum_Recursion(int[][] matrix)
	{
		int n = matrix.Length, best = int.MaxValue;
		for (int j = 0; j < n; j++) best = Math.Min(best, MinSum(n - 1, j, matrix));
		return best;
	}

	private int MinSum(int i, int j, int[][] m)
	{
		if (j < 0 || j >= m.Length) return INF;   // check this first, before reading m[i][j]
		if (i == 0) return m[0][j];

		int best = Math.Min(MinSum(i - 1, j - 1, m), Math.Min(MinSum(i - 1, j, m), MinSum(i - 1, j + 1, m)));
		return m[i][j] + best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n^2)
	// S.C: O(n^2)
	public int MinFallingPathSum_Memoization(int[][] matrix)
	{
		int n = matrix.Length, best = int.MaxValue;
		int?[,] dp = new int?[n, n];   // null = not solved (sums can be negative, so -1 is not a safe marker)
		for (int j = 0; j < n; j++) best = Math.Min(best, MinSum(n - 1, j, matrix, dp));
		return best;
	}

	private int MinSum(int i, int j, int[][] m, int?[,] dp)
	{
		if (j < 0 || j >= m.Length) return INF;
		if (i == 0) return m[0][j];
		if (dp[i, j] != null) return dp[i, j].Value;

		int best = Math.Min(MinSum(i - 1, j - 1, m, dp), Math.Min(MinSum(i - 1, j, m, dp), MinSum(i - 1, j + 1, m, dp)));
		dp[i, j] = m[i][j] + best;
		return dp[i, j].Value;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n^2)
	// S.C: O(n^2)
	public int MinFallingPathSum_Tabulation(int[][] matrix)
	{
		int n = matrix.Length;
		int[,] dp = new int[n, n];
		for (int j = 0; j < n; j++) dp[0, j] = matrix[0][j];

		for (int i = 1; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				int upLeft = j > 0 ? dp[i - 1, j - 1] : INF;
				int upRight = j < n - 1 ? dp[i - 1, j + 1] : INF;
				dp[i, j] = matrix[i][j] + Math.Min(dp[i - 1, j], Math.Min(upLeft, upRight));
			}
		}

		int best = int.MaxValue;
		for (int j = 0; j < n; j++) best = Math.Min(best, dp[n - 1, j]);
		return best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n^2)
	// S.C: O(n)
	public int MinFallingPathSum_SpaceOptimized(int[][] matrix)
	{
		int n = matrix.Length;
		int[] prev = (int[])matrix[0].Clone();

		for (int i = 1; i < n; i++)
		{
			int[] curr = new int[n];
			for (int j = 0; j < n; j++)
			{
				int upLeft = j > 0 ? prev[j - 1] : INF;
				int upRight = j < n - 1 ? prev[j + 1] : INF;
				curr[j] = matrix[i][j] + Math.Min(prev[j], Math.Min(upLeft, upRight));
			}
			prev = curr;
		}

		int best = int.MaxValue;
		foreach (int x in prev) best = Math.Min(best, x);
		return best;
	}
}


class Program
{
	static void Main()
	{
		int[][] matrix = { new[] { 2, 1, 3 }, new[] { 6, 5, 4 }, new[] { 7, 8, 9 } };

		Console.WriteLine(new Solution().MinFallingPathSum_Recursion(matrix));        // o/p: 13
		Console.WriteLine(new Solution().MinFallingPathSum_Memoization(matrix));      // o/p: 13
		Console.WriteLine(new Solution().MinFallingPathSum_Tabulation(matrix));       // o/p: 13
		Console.WriteLine(new Solution().MinFallingPathSum_SpaceOptimized(matrix));   // o/p: 13
	}
}

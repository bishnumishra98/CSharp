// Leetcode: 120. Triangle   --->   Given a triangle (row i has i + 1 numbers), return the minimum path sum from the top to
// the bottom. From index j in a row, you can move to index j or j + 1 in the row below.

// Example 1:
// Input: triangle = [[2],[3,4],[6,5,7],[4,1,8,3]]
//        2
//       3 4
//      6 5 7
//     4 1 8 3
// Output: 11   (2 + 3 + 5 + 1)

// Example 2:
// Input: triangle = [[-10]]
// Output: -10

// New here: the START is fixed (the top), but the END is not (any cell of the last row). So go FORWARD from the fixed
// start: f(i, j) = the smallest sum from (i, j) down to the bottom row.
//   f(i, j) = triangle[i][j] + min(f(i + 1, j), f(i + 1, j + 1))
//   f(last row, j) = triangle[last][j]
// No "outside" check is needed: row i + 1 is one longer than row i, so j + 1 always exists below.


using System;
using System.Collections.Generic;

public class Solution
{
	// T.C: O(2^n);   n = number of rows
	// S.C: O(n)
	public int MinimumTotal_Recursion(IList<IList<int>> triangle)
	{
		return MinSum(0, 0, triangle);
	}

	private int MinSum(int i, int j, IList<IList<int>> t)
	{
		if (i == t.Count - 1) return t[i][j];
		return t[i][j] + Math.Min(MinSum(i + 1, j, t), MinSum(i + 1, j + 1, t));
	}

// ------------------------------------------------------------------------------

	// T.C: O(n^2);   about n^2 / 2 cells
	// S.C: O(n^2)
	public int MinimumTotal_Memoization(IList<IList<int>> triangle)
	{
		int n = triangle.Count;
		int?[,] dp = new int?[n, n];   // null = not solved yet (any int, even -1, can be a real answer here)
		return MinSum(0, 0, triangle, dp);
	}

	private int MinSum(int i, int j, IList<IList<int>> t, int?[,] dp)
	{
		if (i == t.Count - 1) return t[i][j];
		if (dp[i, j] != null) return dp[i, j].Value;

		int best = t[i][j] + Math.Min(MinSum(i + 1, j, t, dp), MinSum(i + 1, j + 1, t, dp));
		dp[i, j] = best;
		return best;
	}

// ------------------------------------------------------------------------------

	// The recursion went from top to bottom, so the table is filled the other way: from the bottom row UP.
	// T.C: O(n^2)
	// S.C: O(n^2)
	public int MinimumTotal_Tabulation(IList<IList<int>> triangle)
	{
		int n = triangle.Count;
		int[,] dp = new int[n, n];
		for (int j = 0; j < n; j++) dp[n - 1, j] = triangle[n - 1][j];

		for (int i = n - 2; i >= 0; i--)
		{
			for (int j = 0; j <= i; j++)
			{
				dp[i, j] = triangle[i][j] + Math.Min(dp[i + 1, j], dp[i + 1, j + 1]);
			}
		}
		return dp[0, 0];
	}

// ------------------------------------------------------------------------------

	// Each row only needs the row BELOW it.
	// T.C: O(n^2)
	// S.C: O(n)
	public int MinimumTotal_SpaceOptimized(IList<IList<int>> triangle)
	{
		int n = triangle.Count;
		int[] below = new int[n];
		for (int j = 0; j < n; j++) below[j] = triangle[n - 1][j];

		for (int i = n - 2; i >= 0; i--)
		{
			int[] curr = new int[n];
			for (int j = 0; j <= i; j++) curr[j] = triangle[i][j] + Math.Min(below[j], below[j + 1]);
			below = curr;
		}
		return below[0];
	}
}

// Note: The memoization uses int? (nullable int, see '01. CSharp_Program/15_NullableTypes.cs') to mean "not solved yet",
//       because the numbers can be negative, so -1 could be a real answer. (The C++ code used -1, which is wrong in
//       rare cases, e.g., when a real sub-answer is exactly -1: it would be recomputed every time, losing the speed-up.)


class Program
{
	static void Main()
	{
		IList<IList<int>> triangle = new List<IList<int>>
		{
			new List<int> { 2 },
			new List<int> { 3, 4 },
			new List<int> { 6, 5, 7 },
			new List<int> { 4, 1, 8, 3 }
		};

		Console.WriteLine(new Solution().MinimumTotal_Recursion(triangle));        // o/p: 11
		Console.WriteLine(new Solution().MinimumTotal_Memoization(triangle));      // o/p: 11
		Console.WriteLine(new Solution().MinimumTotal_Tabulation(triangle));       // o/p: 11
		Console.WriteLine(new Solution().MinimumTotal_SpaceOptimized(triangle));   // o/p: 11
	}
}

// Leetcode: 221. Maximal Square   --->   Given an m x n matrix of '0's and '1's, find the largest SQUARE that contains
// only '1's, and return its area.

// Example 1:
// Input: matrix = [["1","0","1","0","0"],
//                  ["1","0","1","1","1"],
//                  ["1","1","1","1","1"],
//                  ["1","0","0","1","0"]]
// Output: 4   (a 2 x 2 square of 1's: rows 1-2, columns 2-3 or 3-4)

// Example 2:
// Input: matrix = [["0","1"],["1","0"]]
// Output: 1

// Example 3:
// Input: matrix = [["0"]]
// Output: 0

// The recursion: f(i, j) = the side of the biggest square of 1's whose BOTTOM-RIGHT corner is (i, j).
//   matrix[i][j] == '0':  f(i, j) = 0 (no square can end on a 0)
//   matrix[i][j] == '1':  f(i, j) = 1 + min(f(i, j - 1), f(i - 1, j - 1), f(i - 1, j))     (left, diagonal, up)
//   f(outside) = 0
// Why the MIN of the 3 ? A square of side s ending at (i, j) needs squares of side s - 1 ending at its left, up and
// diagonal neighbours. The smallest of the 3 is the one that limits it.
// e.g.,      ...  1  1           left = 2, up = 2, diagonal = 1 (the top-left corner breaks the bigger square)
//            ...  1  1           -> 1 + min(2, 1, 2) = 2.
//            1  1  [1]
// The answer is the biggest f(i, j) of ALL cells, squared.


using System;

public class Solution
{
	// T.C: O(3^(m + n));   3 calls each time, and a chain of calls is at most m + n long
	// S.C: O(m + n);       recursion stack
	// Note: The C++ comments said T.C O(3^(m * n)) and S.C O(min(m, n)). The depth is m + n (going left only lowers j,
	//       going up only lowers i), which gives these values.
	public int MaximalSquare_Recursion(char[][] matrix)
	{
		int maxSide = 0;
		Side(matrix.Length - 1, matrix[0].Length - 1, matrix, ref maxSide);   // visits every cell, keeps the best in maxSide
		return maxSide * maxSide;
	}

	private int Side(int i, int j, char[][] matrix, ref int maxSide)
	{
		if (i < 0 || j < 0) return 0;

		int left = Side(i, j - 1, matrix, ref maxSide);
		int diag = Side(i - 1, j - 1, matrix, ref maxSide);
		int up = Side(i - 1, j, matrix, ref maxSide);

		if (matrix[i][j] == '0') return 0;

		int side = 1 + Math.Min(left, Math.Min(diag, up));
		maxSide = Math.Max(maxSide, side);
		return side;
	}

// ------------------------------------------------------------------------------

	// T.C: O(m * n)
	// S.C: O(m * n);   dp + O(m + n) recursion stack
	public int MaximalSquare_Memoization(char[][] matrix)
	{
		int m = matrix.Length, n = matrix[0].Length, maxSide = 0;
		int[,] dp = new int[m, n];
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++) dp[i, j] = -1;
		}

		Side(m - 1, n - 1, matrix, ref maxSide, dp);
		return maxSide * maxSide;
	}

	private int Side(int i, int j, char[][] matrix, ref int maxSide, int[,] dp)
	{
		if (i < 0 || j < 0) return 0;
		if (dp[i, j] != -1) return dp[i, j];

		int left = Side(i, j - 1, matrix, ref maxSide, dp);
		int diag = Side(i - 1, j - 1, matrix, ref maxSide, dp);
		int up = Side(i - 1, j, matrix, ref maxSide, dp);

		if (matrix[i][j] == '0') return dp[i, j] = 0;

		int side = 1 + Math.Min(left, Math.Min(diag, up));
		maxSide = Math.Max(maxSide, side);
		return dp[i, j] = side;
	}

// ------------------------------------------------------------------------------

	// SHIFTED indexes: dp has one extra row and column of 0's at the top and left, and dp[i, j] is for matrix[i - 1][j - 1].
	// Then "outside the grid" is just dp[0, x] or dp[x, 0] = 0, and no 'if (i > 0)' checks are needed.
	// (A common trick in DP tables, used a lot in the string problems later.)
	// T.C: O(m * n)
	// S.C: O(m * n)
	public int MaximalSquare_Tabulation(char[][] matrix)
	{
		int m = matrix.Length, n = matrix[0].Length, maxSide = 0;
		int[,] dp = new int[m + 1, n + 1];   // all 0

		for (int i = 1; i <= m; i++)
		{
			for (int j = 1; j <= n; j++)
			{
				if (matrix[i - 1][j - 1] == '0') continue;   // dp stays 0

				dp[i, j] = 1 + Math.Min(dp[i, j - 1], Math.Min(dp[i - 1, j - 1], dp[i - 1, j]));
				maxSide = Math.Max(maxSide, dp[i, j]);
			}
		}
		return maxSide * maxSide;
	}

// ------------------------------------------------------------------------------

	// Only the row above (prev) and the current row (curr) are needed.
	// T.C: O(m * n)
	// S.C: O(n)
	// C# trap: 'prev = curr' does NOT copy an array in C# (it does copy a vector in C++). It makes both names point to
	//          the SAME array, so the next row would overwrite the "previous" row while reading it. So a NEW curr
	//          array is made for every row.
	public int MaximalSquare_SpaceOptimized(char[][] matrix)
	{
		int m = matrix.Length, n = matrix[0].Length, maxSide = 0;
		int[] prev = new int[n + 1];

		for (int i = 1; i <= m; i++)
		{
			int[] curr = new int[n + 1];
			for (int j = 1; j <= n; j++)
			{
				if (matrix[i - 1][j - 1] == '0') continue;

				curr[j] = 1 + Math.Min(curr[j - 1], Math.Min(prev[j - 1], prev[j]));
				maxSide = Math.Max(maxSide, curr[j]);
			}
			prev = curr;
		}
		return maxSide * maxSide;
	}
}


class Program
{
	static void Main()
	{
		char[][] matrix =
		{
			"10100".ToCharArray(),
			"10111".ToCharArray(),
			"11111".ToCharArray(),
			"10010".ToCharArray()
		};

		Console.WriteLine(new Solution().MaximalSquare_Recursion(matrix));        // o/p: 4
		Console.WriteLine(new Solution().MaximalSquare_Memoization(matrix));      // o/p: 4
		Console.WriteLine(new Solution().MaximalSquare_Tabulation(matrix));       // o/p: 4
		Console.WriteLine(new Solution().MaximalSquare_SpaceOptimized(matrix));   // o/p: 4
	}
}

// Leetcode: 73. Set Matrix Zeroes   --->   Given an m x n integer matrix matrix, if an element is 0, set its entire
// row and column to 0's.
// You must do it in place.

// Example 1:
// Input: matrix = [[1,1,1],[1,0,1],[1,1,1]]
// Output: [[1,0,1],[0,0,0],[1,0,1]]
//    1 1 1        1 0 1
//    1 0 1  --->  0 0 0
//    1 1 1        1 0 1

// Example 2:
// Input: matrix = [[0,1,2,0],[3,4,5,2],[1,3,1,5]]
// Output: [[0,0,0,0],[0,4,5,0],[0,3,1,0]]

// The main trap: If we turn elements to 0 directly while scanning, those new 0s will also be treated as "original" 0s,
// and they will wrongly make their rows and columns 0 too. So we must first MARK what to change, and change it later.


using System;

public class Solution
{
	// Helper: mark all non-zero elements of row 'i' as -1
	private void MarkRow(int i, int[][] matrix)
	{
		int n = matrix[0].Length;   // no.of columns (a row has n elements)
		for (int j = 0; j < n; j++)
		{
			if (matrix[i][j] != 0) matrix[i][j] = -1;
		}
	}

	// Helper: mark all non-zero elements of column 'j' as -1
	private void MarkCol(int j, int[][] matrix)
	{
		int m = matrix.Length;   // no.of rows (a column has m elements)
		for (int i = 0; i < m; i++)
		{
			if (matrix[i][j] != 0) matrix[i][j] = -1;
		}
	}

	// Brute force algorithm:
	// 1. For every 0 found, mark all non-zero elements of its row and column as -1 (not 0, see "main trap" above).
	// 2. At the end, turn all -1s into 0s.
	// NOTE: This works only if the matrix does not already contain -1. It is only for understanding the idea.
	// T.C: O(m * n) for nested loops * O(m + n) for marking rows and columns + O(m * n) for turning -1s to 0s
	//      = O((m * n) * (m + n)) + O(m * n)
	// S.C: O(1)
	public void SetZeroes_BruteForce(int[][] matrix)
	{
		int m = matrix.Length;
		int n = matrix[0].Length;

		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (matrix[i][j] == 0)
				{
					MarkRow(i, matrix);   // mark all elements in 'i'th row as -1
					MarkCol(j, matrix);   // mark all elements in 'j'th column as -1
				}
			}
		}

		// Turn back all -1s to 0s
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (matrix[i][j] == -1) matrix[i][j] = 0;
			}
		}
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Mark the rows and columns which have 0s in 2 separate bool arrays, and then turn all elements in
	// those rows and columns to 0.
	// T.C: O(m * n) + O(m * n) = O(2 * m * n)
	// S.C: O(m + n)
	public void SetZeroes_Better(int[][] matrix)
	{
		int m = matrix.Length;
		int n = matrix[0].Length;
		bool[] row = new bool[m];   // all values are false by default
		bool[] col = new bool[n];

		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (matrix[i][j] == 0)
				{
					row[i] = true;   // marking this row true means all elements of this row have to be made 0
					col[j] = true;   // marking this column true means all elements of this column have to be made 0
				}
			}
		}

		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (row[i] || col[j]) matrix[i][j] = 0;
			}
		}
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Same marking idea as the better approach, but without the extra 'row' and 'col' arrays.
	// Instead, we use the first column and the first row of the matrix itself as the markers:
	// -> matrix[i][0] == 0 means "row i has to be zeroed" (row headings).
	// -> matrix[0][j] == 0 means "column j has to be zeroed" (column headings).
	// Problem: matrix[0][0] is in both the first row and the first column. So it can't mark both. Hence, matrix[0][0] is
	//          used only for row 0, and a separate variable 'col0' tells whether the first column has to be zeroed.
	// 1. Initialize 'col0' to 1, meaning that the first column does not have to be zeroed initially.
	// 2. Scan the matrix. If an element is 0, mark its row heading (matrix[i][0] = 0) and column heading (matrix[0][j] = 0).
	//    If any element of the first column is 0, set 'col0' = 0.
	// 3. Scan the matrix again in REVERSE order (from last row to first row, and from last column to 2nd column).
	//    If the row heading or the column heading of an element is 0, set that element to 0. After each row, if 'col0'
	//    is 0, set the first column element of that row to 0 as well.
	//    Why reverse? The headings are in row 0 and column 0. If we go forward, we would change the headings first, and
	//    then use the changed (wrong) headings for the rest of the matrix. Going in reverse changes the headings last.
	// T.C: O(m * n)
	// S.C: O(1)
	public void SetZeroes(int[][] matrix)
	{
		int m = matrix.Length;
		int n = matrix[0].Length;

		int col0 = 1;   // Initially let the first column not be zeroed

		// Step 1: Mark rows and cols to be zeroed using the matrix itself
		for (int i = 0; i < m; i++)
		{
			if (matrix[i][0] == 0) col0 = 0;   // if any element in the first column is 0, the first column has to be zeroed
			for (int j = 1; j < n; j++)
			{
				// If an element is 0, mark the corresponding row and column headings as 0
				if (matrix[i][j] == 0)
				{
					matrix[i][0] = 0;
					matrix[0][j] = 0;
				}
			}
		}

		// Step 2: Iterate in reverse and update the matrix
		for (int i = m - 1; i >= 0; i--)
		{
			for (int j = n - 1; j >= 1; j--)
			{
				// If the element in the row heading is 0, or the element in the column heading is 0, set the element to 0
				if (matrix[i][0] == 0 || matrix[0][j] == 0) matrix[i][j] = 0;
			}
			// If the 'col0' variable is 0, set the first column element of this row to 0
			if (col0 == 0) matrix[i][0] = 0;
		}
	}
}


class Program
{
	static void Print(int[][] matrix)
	{
		foreach (int[] row in matrix) Console.WriteLine(string.Join(" ", row));
		Console.WriteLine();
	}

	static void Main()
	{
		int[][] m1 = { new int[] { 0, 1, 2, 0 }, new int[] { 3, 4, 5, 2 }, new int[] { 1, 3, 1, 5 } };
		new Solution().SetZeroes_BruteForce(m1);
		Print(m1);

		int[][] m2 = { new int[] { 0, 1, 2, 0 }, new int[] { 3, 4, 5, 2 }, new int[] { 1, 3, 1, 5 } };
		new Solution().SetZeroes_Better(m2);
		Print(m2);

		int[][] m3 = { new int[] { 0, 1, 2, 0 }, new int[] { 3, 4, 5, 2 }, new int[] { 1, 3, 1, 5 } };
		new Solution().SetZeroes(m3);
		Print(m3);
		// o/p (same for all 3):
		// 0 0 0 0
		// 0 4 5 0
		// 0 3 1 0
	}
}

// Leetcode: 867. Transpose Matrix   --->   Given a 2D integer array matrix, return the transpose of matrix.
// The transpose of a matrix is the matrix flipped over its main diagonal, switching the matrix's row and column indices.

// Example 1:
// Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
// Output: [[1,4,7],[2,5,8],[3,6,9]]

// Example 2:
// Input: matrix = [[1,2,3],[4,5,6]]
// Output: [[1,4],[2,5],[3,6]]

// ● 2D array in C#: Leetcode uses int[][] (jagged array), i.e., an "array of arrays". Each row is its own int[].
//   - matrix.Length    : no.of rows
//   - matrix[0].Length : no.of columns
//   - matrix[i][j]     : element at row i, column j
//   - While creating, each row has to be created separately: new int[rows][] and then matrix[i] = new int[cols].


using System;

public class Solution
{
	// Algorithm (works for any m x n matrix): Element at (i, j) goes to (j, i) in the transposed matrix.
	// 1. A matrix with 'rows' rows and 'cols' columns becomes a matrix with 'cols' rows and 'rows' columns.
	//    So create a new matrix with swapped dimensions.
	// 2. For every element, put matrix[i][j] at tMatrix[j][i].
	// T.C: O(m * n)
	// S.C: O(m * n)
	public int[][] Transpose(int[][] matrix)
	{
		int rows = matrix.Length;
		int cols = matrix[0].Length;

		// Create a 2D array with the dimensions swapped
		int[][] tMatrix = new int[cols][];
		for (int i = 0; i < cols; i++) tMatrix[i] = new int[rows];

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				// NOTE: Writing tMatrix[i][j] = matrix[j][i] would give an IndexOutOfRangeException for a non-square matrix,
				//       because tMatrix does not have 'rows' no.of rows. Hence, always write tMatrix[j][i] = matrix[i][j].
				tMatrix[j][i] = matrix[i][j];
			}
		}

		return tMatrix;
	}

// ------------------------------------------------------------------------------

	// Follow up: Transpose a SQUARE matrix (n x n) in-place, i.e., without a new matrix.
	// Non-square matrices need extra space to transpose, but square matrices can be transposed in-place.
	// Algorithm: Swap matrix[i][j] with matrix[j][i], but only for the upper triangle (j starts from i).
	// NOTE: If j starts from 0, every pair gets swapped twice, and the matrix comes back to the original.
	//       So j must start from i, to swap each pair only once. Dry run for better understanding.
	// T.C: O(n^2);   where n = no.of rows or columns
	// S.C: O(1)
	public void TransposeSquare(int[][] matrix)
	{
		int n = matrix.Length;

		for (int i = 0; i < n; i++)
		{
			for (int j = i; j < n; j++)
			{
				(matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);   // swap
			}
		}
	}
}


class Program
{
	static void Main()
	{
		int[][] matrix = new int[][]
		{
			new int[] { 1, 2, 3 },
			new int[] { 4, 5, 6 }
		};

		int[][] ans = new Solution().Transpose(matrix);
		foreach (int[] row in ans) Console.WriteLine(string.Join(" ", row));
		// o/p:
		// 1 4
		// 2 5
		// 3 6

		Console.WriteLine();

		int[][] square = new int[][]
		{
			new int[] { 1, 2, 3 },
			new int[] { 4, 5, 6 },
			new int[] { 7, 8, 9 }
		};

		new Solution().TransposeSquare(square);
		foreach (int[] row in square) Console.WriteLine(string.Join(" ", row));
		// o/p:
		// 1 4 7
		// 2 5 8
		// 3 6 9
	}
}

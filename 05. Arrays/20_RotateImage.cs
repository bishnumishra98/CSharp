// Leetcode: 48. Rotate Image   --->   You are given an n x n 2D matrix representing an image, rotate the image
// by 90 degrees (clockwise).
// You have to rotate the image in-place, which means you have to modify the input 2D matrix directly. DO NOT
// allocate another 2D matrix and do the rotation.

// Example 1:
// Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
// Output: [[7,4,1],[8,5,2],[9,6,3]]
//    1 2 3        7 4 1
//    4 5 6  --->  8 5 2
//    7 8 9        9 6 3

// Example 2:
// Input: matrix = [[5,1,9,11],[2,4,8,10],[13,3,6,7],[15,14,12,16]]
// Output: [[15,13,2,5],[14,3,4,1],[12,6,8,9],[16,7,10,11]]


using System;

public class Solution
{
	// Brute force algorithm: Use a new matrix.
	// Observe: The 1st row of the answer is the 1st column read from bottom to top (7 4 1), the 2nd row is the
	// 2nd column read from bottom to top (8 5 2), and so on. So: arr[i][j] = matrix[n - j - 1][i].
	// T.C: O(n^2)
	// S.C: O(n^2)
	public void Rotate_BruteForce(int[][] matrix)
	{
		int n = matrix.Length;

		// Create a new n x n matrix
		int[][] arr = new int[n][];
		for (int i = 0; i < n; i++) arr[i] = new int[n];

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				arr[i][j] = matrix[n - j - 1][i];
			}
		}

		// Copy back. NOTE (C#): 'matrix = arr;' would only change the local variable, not the caller's matrix.
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				matrix[i][j] = arr[i][j];
			}
		}
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Rotate a matrix by 90° clockwise = Transpose the matrix + reverse each row
	//    1 2 3   transpose   1 4 7   reverse each row   7 4 1
	//    4 5 6  ---------->  2 5 8  ----------------->  8 5 2
	//    7 8 9               3 6 9                      9 6 3
	// T.C: O(n^2)
	// S.C: O(1)
	public void Rotate(int[][] matrix)
	{
		int n = matrix.Length;

		// Transpose (same as TransposeSquare() in 09_TransposeMatrix.cs)
		// Note: Always remember that in in-place transpose of a square matrix, j should start from i, not from 0. Because if
		//       we start j from 0, the same elements get swapped twice, resulting in no change in the original matrix.
		for (int i = 0; i < n; i++)
		{
			for (int j = i; j < n; j++)
			{
				(matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);   // swap
			}
		}

		// Reverse each row (each row is an int[], so Array.Reverse works on it)
		for (int i = 0; i < n; i++)
		{
			Array.Reverse(matrix[i]);
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
			new int[] { 4, 5, 6 },
			new int[] { 7, 8, 9 }
		};

		// new Solution().Rotate_BruteForce(matrix);
		new Solution().Rotate(matrix);

		foreach (int[] row in matrix) Console.WriteLine(string.Join(" ", row));
		// o/p:
		// 7 4 1
		// 8 5 2
		// 9 6 3
	}
}

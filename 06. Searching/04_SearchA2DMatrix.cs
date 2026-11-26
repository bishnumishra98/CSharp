// Leetcode: 74. Search a 2D Matrix   --->   You are given an m x n integer matrix matrix with the following two
// properties:
// • Each row is sorted in non-decreasing order.
// • The first integer of each row is greater than the last integer of the previous row.
// Given an integer target, return true if target is in matrix or false otherwise.
// You must write a solution in O(log(m * n)) time complexity.

// Example 1:
// Input: matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]], target = 3
// Output: true

// Example 2:
// Input: matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]], target = 13
// Output: false


using System;

public class Solution
{
	// Brute force: Check every cell of the matrix.
	// T.C: O(m * n)
	// S.C: O(1)
	public bool SearchMatrix_BruteForce(int[][] matrix, int target)
	{
		for (int i = 0; i < matrix.Length; i++)
		{
			for (int j = 0; j < matrix[i].Length; j++)
			{
				if (matrix[i][j] == target) return true;
			}
		}
		return false;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Treat the matrix as one long sorted 1D array, and apply binary search on it.
	// If we write all rows one after another, we get: [1, 3, 5, 7, 10, 11, 16, 20, 23, 30, 34, 60], which is sorted.
	// We don't need to really create this 1D array. We can convert any 1D index into (row, col) of the matrix:
	// ● 1D -> 2D:  row = index / cols,  col = index % cols;   where cols = number of columns
	// ● 2D -> 1D:  index = row * cols + col
	//   e.g., cols = 4, index 6  ->  row = 6 / 4 = 1, col = 6 % 4 = 2  ->  matrix[1][2] = 16
	// 1. start = 0, end = m * n - 1. Find mid, convert it to (row, col), and compare matrix[row][col] with target,
	//    just like normal binary search.
	// T.C: O(log(m * n))
	// S.C: O(1)
	public bool SearchMatrix(int[][] matrix, int target)
	{
		int rows = matrix.Length;
		int cols = matrix[0].Length;
		int start = 0, end = rows * cols - 1;

		while (start <= end)
		{
			int mid = start + (end - start) / 2;
			int curr = matrix[mid / cols][mid % cols];   // 'mid' is the index in the imaginary 1D array

			if (target == curr) return true;
			else if (target > curr) start = mid + 1;
			else end = mid - 1;
		}

		return false;
	}
}


class Program
{
	static void Main()
	{
		int[][] matrix =
		{
			new int[] { 1, 3, 5, 7 },
			new int[] { 10, 11, 16, 20 },
			new int[] { 23, 30, 34, 60 }
		};
		int target = 3;

		Console.WriteLine(new Solution().SearchMatrix_BruteForce(matrix, target));   // o/p: True
		Console.WriteLine(new Solution().SearchMatrix(matrix, target));              // o/p: True
	}
}

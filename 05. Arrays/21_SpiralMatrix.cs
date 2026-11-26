// Leetcode: 54. Spiral Matrix   --->   Given an m x n matrix, return all elements of the matrix in spiral order.

// Example 1:
// 1 2 3
// 4 5 6
// 7 8 9
// Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
// Output: [1,2,3,6,9,8,7,4,5]

// Example 2:
// 1  2  3  4
// 5  6  7  8
// 9 10 11 12
// Input: matrix = [[1,2,3,4],[5,6,7,8],[9,10,11,12]]
// Output: [1,2,3,4,8,12,11,10,9,5,6,7]

// Algorithm: If you observe carefully, the spiral path follows a pattern:
// Traverse the top row (left to right), then the right column (top to bottom), then the bottom row (right to left),
// then the left column (bottom to top). Then do the same for the inner part: 2nd row, 2nd last column, 2nd last row,
// 2nd column, and so on, until all elements have been added.
// 1. Keep 4 boundaries: startRow, endRow, startCol, endCol.
// 2. After traversing a boundary, shrink it (startRow++, endCol--, endRow--, startCol++), so it is not visited again.
// 3. Stop when all m * n elements are added. (The 'ans.Count < total' check inside every loop stops us from adding
//    extra elements when the last layer is a single row or a single column.)


using System;
using System.Collections.Generic;

public class Solution
{
	// T.C: O(m * n);   where m = no.of rows and n = no.of columns respectively
	// S.C: O(m * n);   for the answer list
	public IList<int> SpiralOrder(int[][] matrix)
	{
		int m = matrix.Length;      // no.of rows
		int n = matrix[0].Length;   // no.of columns
		int total = m * n;          // total no.of elements in matrix
		int startRow = 0;
		int endRow = m - 1;
		int startCol = 0;
		int endCol = n - 1;

		List<int> ans = new List<int>();
		while (ans.Count < total)
		{
			// Traverse starting row: fixed row, varying column
			for (int j = startCol; j <= endCol && ans.Count < total; j++)
			{
				ans.Add(matrix[startRow][j]);
			}
			startRow++;   // for next iteration, startRow should be incremented by 1

			// Traverse ending column: varying row, fixed column
			for (int i = startRow; i <= endRow && ans.Count < total; i++)
			{
				ans.Add(matrix[i][endCol]);
			}
			endCol--;   // for next iteration, endCol should be decremented by 1

			// Traverse ending row: fixed row, varying column
			for (int j = endCol; j >= startCol && ans.Count < total; j--)
			{
				ans.Add(matrix[endRow][j]);
			}
			endRow--;   // for next iteration, endRow should be decremented by 1

			// Traverse starting column: varying row, fixed column
			for (int i = endRow; i >= startRow && ans.Count < total; i--)
			{
				ans.Add(matrix[i][startCol]);
			}
			startCol++;   // for next iteration, startCol should be incremented by 1
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[][] matrix = new int[][]
		{
			new int[] { 1, 2, 3, 4 },
			new int[] { 5, 6, 7, 8 },
			new int[] { 9, 10, 11, 12 }
		};

		IList<int> ans = new Solution().SpiralOrder(matrix);
		Console.WriteLine(string.Join(" ", ans));   // o/p: 1 2 3 4 8 12 11 10 9 5 6 7
	}
}

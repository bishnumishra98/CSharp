// GFG: Number of Distinct Islands   --->   A grid of 0's and 1's. An island is a group of 1's connected up / down / left /
// right. Two islands are the SAME if one can be moved (shifted, NOT rotated or mirrored) to exactly cover the other.
// Count the DIFFERENT island shapes.

// Example 1:
// Input: grid = [[1,1,0,0,0],
//                [1,1,0,0,0],
//                [0,0,0,1,1],
//                [0,0,0,1,1]]
// Output: 1   (two 2x2 squares: the same shape)

// Example 2:
// Input: grid = [[1,1,0,1,1],
//                [1,0,0,0,0],
//                [0,0,0,0,1],
//                [1,1,0,1,1]]
// Output: 3   (top-left and bottom-right have the same "L" shape; the other two are different)

// Problem link: https://www.geeksforgeeks.org/problems/number-of-distinct-islands/1


using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };

	// Algorithm: Describe every island's SHAPE in a way that does not depend on WHERE it is, and put the descriptions in
	// a HashSet (which keeps only different ones). The answer is the set's size.
	// Shape = the list of its cells RELATIVE to the island's first cell (r0, c0): (r - r0, c - c0).
	// e.g., Example 2 has 4 islands:
	//       top-left    (0,0) (0,1) (1,0)   -> "0,0;0,1;1,0;"
	//       top-right   (0,3) (0,4)         -> "0,0;0,1;"
	//       bottom-left (3,0) (3,1)         -> "0,0;0,1;"        (the same as top-right)
	//       right       (2,4) (3,4) (3,3)   -> "0,0;1,0;1,-1;"
	//       3 different strings -> 3 distinct islands.
	// Why do equal shapes always give the SAME list ? The scan finds the island's top-left-most cell first (the first
	// cell in row-by-row order), and the DFS visits the neighbours in a fixed order (up, right, down, left). Two copies
	// of a shape start at the same relative cell and make the same moves, so they list their cells in the same order.
	// The list is turned into a string (e.g., "0,0;0,1;1,0;") so the HashSet can compare whole shapes.
	// T.C: O(n * m);   every cell is visited once, and all shape strings together have O(n * m) characters to hash
	// S.C: O(n * m)
	public int CountDistinctIslands(int[][] grid)
	{
		int n = grid.Length, m = grid[0].Length;
		bool[,] visited = new bool[n, m];
		HashSet<string> shapes = new HashSet<string>();

		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < m; c++)
			{
				if (grid[r][c] != 1 || visited[r, c]) continue;

				StringBuilder shape = new StringBuilder();
				Dfs(r, c, r, c, grid, visited, shape);
				shapes.Add(shape.ToString());
			}
		}
		return shapes.Count;
	}

	private void Dfs(int r, int c, int r0, int c0, int[][] grid, bool[,] visited, StringBuilder shape)
	{
		visited[r, c] = true;
		shape.Append(r - r0).Append(',').Append(c - c0).Append(';');

		for (int k = 0; k < 4; k++)
		{
			int nr = r + dr[k], nc = c + dc[k];
			if (nr >= 0 && nr < grid.Length && nc >= 0 && nc < grid[0].Length && grid[nr][nc] == 1 && !visited[nr, nc])
			{
				Dfs(nr, nc, r0, c0, grid, visited, shape);
			}
		}
	}
}


class Program
{
	static void Main()
	{
		int[][] grid = { new[] { 1, 1, 0, 1, 1 }, new[] { 1, 0, 0, 0, 0 }, new[] { 0, 0, 0, 0, 1 }, new[] { 1, 1, 0, 1, 1 } };

		Console.WriteLine(new Solution().CountDistinctIslands(grid));   // o/p: 3
	}
}

// Leetcode: 1020. Number of Enclaves   --->   A grid: 0 = sea, 1 = land. A move goes from a land cell to a neighbouring
// land cell (4 directions), or off the edge of the grid. Count the land cells from which you can NOT walk off the grid.

// Example 1:
// Input: grid = [[0,0,0,0],
//                [1,0,1,0],
//                [0,1,1,0],
//                [0,0,0,0]]
// Output: 3   (the 1 at (1,0) is on the border; the other three 1's are trapped)

// Example 2:
// Input: grid = [[0,1,1,0],
//                [0,0,1,0],
//                [0,0,1,0],
//                [0,0,0,0]]
// Output: 0   (all the land is connected to the border)

// First understand '11_SurroundedRegions.cs'. The same idea: the land connected to the border can escape; count the rest.


using System;
using System.Collections.Generic;

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };

	// Algorithm (multi-source BFS from the border):
	// 1. Put every LAND cell of the border into the queue and mark it visited.
	// 2. BFS: everything reached from them can walk off the grid.
	// 3. Count the land cells that were never visited.
	// T.C: O(n * m)
	// S.C: O(n * m)
	// Note: The C++ code (DFS) did the same; its last counting loop started at column 0 instead of 1 (harmless: a land
	//       cell in column 0 is on the border and always visited).
	public int NumEnclaves(int[][] grid)
	{
		int n = grid.Length, m = grid[0].Length;
		bool[,] visited = new bool[n, m];
		Queue<(int r, int c)> q = new Queue<(int r, int c)>();

		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < m; c++)
			{
				bool onBorder = r == 0 || c == 0 || r == n - 1 || c == m - 1;
				if (onBorder && grid[r][c] == 1)
				{
					visited[r, c] = true;
					q.Enqueue((r, c));
				}
			}
		}

		while (q.Count > 0)
		{
			var (r, c) = q.Dequeue();
			for (int k = 0; k < 4; k++)
			{
				int nr = r + dr[k], nc = c + dc[k];
				if (nr < 0 || nr >= n || nc < 0 || nc >= m || grid[nr][nc] != 1 || visited[nr, nc]) continue;
				visited[nr, nc] = true;
				q.Enqueue((nr, nc));
			}
		}

		int count = 0;
		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < m; c++)
			{
				if (grid[r][c] == 1 && !visited[r, c]) count++;
			}
		}
		return count;
	}
}


class Program
{
	static void Main()
	{
		int[][] grid = { new[] { 0, 0, 0, 0 }, new[] { 1, 0, 1, 0 }, new[] { 0, 1, 1, 0 }, new[] { 0, 0, 0, 0 } };

		Console.WriteLine(new Solution().NumEnclaves(grid));   // o/p: 3
	}
}

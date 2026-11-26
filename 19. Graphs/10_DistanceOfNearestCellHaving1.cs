// GFG: Distance of Nearest Cell Having 1   --->   Given a grid of 0's and 1's (at least one 1), return a grid where each
// cell holds the distance to its NEAREST 1. Distance = |row1 - row2| + |col1 - col2| (steps up / down / left / right).
// (Leetcode: 542. 01 Matrix is the same, with the nearest 0 instead of the nearest 1.)

// Example 1:
// Input:  grid = [[0,1,1,0],          Output: [[1,0,0,1],
//                 [1,1,0,0],                   [0,0,1,1],
//                 [0,0,1,1]]                   [1,1,0,0]]

// Example 2:
// Input:  grid = [[1,0,1],            Output: [[0,1,0],
//                 [1,1,0],                     [0,0,1],
//                 [1,0,0]]                     [0,1,2]]

// First understand '08_RottingOranges.cs' (multi-source BFS).


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };

	// Brute force (idea only): BFS from EVERY 0 separately until it finds a 1: O((n * m)^2).
	// Algorithm (multi-source BFS): Start the BFS from ALL the 1's at once, with distance 0. BFS spreads one step per
	// level, so the FIRST time a cell is reached, it is reached from its nearest 1. Write that distance, mark it visited,
	// and never touch it again.
	// T.C: O(n * m)
	// S.C: O(n * m);   visited + queue + answer
	public int[][] Nearest(int[][] grid)
	{
		int n = grid.Length, m = grid[0].Length;
		int[][] dist = new int[n][];
		bool[,] visited = new bool[n, m];
		Queue<(int r, int c, int d)> q = new Queue<(int r, int c, int d)>();

		for (int r = 0; r < n; r++)
		{
			dist[r] = new int[m];
			for (int c = 0; c < m; c++)
			{
				if (grid[r][c] == 1)
				{
					q.Enqueue((r, c, 0));
					visited[r, c] = true;
				}
			}
		}

		while (q.Count > 0)
		{
			var (r, c, d) = q.Dequeue();
			dist[r][c] = d;

			for (int k = 0; k < 4; k++)
			{
				int nr = r + dr[k], nc = c + dc[k];
				if (nr < 0 || nr >= n || nc < 0 || nc >= m || visited[nr, nc]) continue;

				visited[nr, nc] = true;
				q.Enqueue((nr, nc, d + 1));
			}
		}
		return dist;
	}
}


class Program
{
	static void Main()
	{
		int[][] grid = { new[] { 1, 0, 1 }, new[] { 1, 1, 0 }, new[] { 1, 0, 0 } };

		var rows = new Solution().Nearest(grid).Select(row => string.Join(" ", row));
		Console.WriteLine(string.Join(" | ", rows));   // o/p: 0 1 0 | 0 0 1 | 0 1 2
	}
}

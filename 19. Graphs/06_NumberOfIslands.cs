// GFG: Find the Number of Islands   --->   A grid of '0' (water) and '1' (land). An ISLAND is a group of land cells
// connected in any of the 8 directions (up, down, left, right AND the 4 diagonals). Count the islands.
// (Leetcode: 200. Number of Islands is the same, but with only 4 directions, no diagonals.)

// Example 1:
// Input: grid = [[0,1],
//                [1,0],
//                [1,1],
//                [1,0]]
// Output: 1   (the 1 at the top touches the one below-left diagonally)

// Example 2:
// Input: grid = [[0,1,1,1,0,0,0],
//                [0,0,1,1,0,1,0]]
// Output: 2

// Problem link: https://www.geeksforgeeks.org/problems/find-the-number-of-islands/1

// First understand '05_NumberOfProvinces.cs'. A GRID is a graph too: every cell is a node, and its neighbours are the
// cells around it. No adjacency list is needed: compute the neighbours on the fly.


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm: Same as counting provinces. Scan every cell. A land cell that is not visited yet starts a NEW island:
	// count it, and BFS from it to mark the whole island visited.
	// The 8 neighbours of (r, c) are (r + dr, c + dc) for dr, dc in {-1, 0, 1} (except dr = dc = 0, the cell itself).
	// Two nested loops over dr and dc try them all. For 4 directions use: up (-1,0), right (0,1), down (1,0), left (0,-1).
	// T.C: O(n * m);   every cell is visited once, and checks 8 neighbours
	// S.C: O(n * m);   visited + queue
	public int NumIslands(char[][] grid)
	{
		int n = grid.Length, m = grid[0].Length, islands = 0;
		bool[,] visited = new bool[n, m];

		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < m; c++)
			{
				if (grid[r][c] != '1' || visited[r, c]) continue;
				islands++;
				Bfs(r, c, grid, visited);
			}
		}
		return islands;
	}

	private void Bfs(int startR, int startC, char[][] grid, bool[,] visited)
	{
		int n = grid.Length, m = grid[0].Length;
		Queue<(int r, int c)> q = new Queue<(int r, int c)>();
		q.Enqueue((startR, startC));
		visited[startR, startC] = true;

		while (q.Count > 0)
		{
			var (r, c) = q.Dequeue();
			for (int dr = -1; dr <= 1; dr++)
			{
				for (int dc = -1; dc <= 1; dc++)
				{
					int nr = r + dr, nc = c + dc;
					if (nr < 0 || nr >= n || nc < 0 || nc >= m) continue;   // outside the grid
					if (grid[nr][nc] != '1' || visited[nr, nc]) continue;

					visited[nr, nc] = true;
					q.Enqueue((nr, nc));
				}
			}
		}
	}
}


class Program
{
	static void Main()
	{
		char[][] grid = { "0111000".ToCharArray(), "0011010".ToCharArray() };

		Console.WriteLine(new Solution().NumIslands(grid));   // o/p: 2
	}
}

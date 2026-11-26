// GFG: Shortest Distance in a Binary Maze   --->   A grid of 0's and 1's. You may only step on cells with 1, moving up /
// down / left / right. Return the fewest steps from 'source' to 'destination', or -1 if it is impossible.

// Example 1:
// Input: grid = [[1,1,1,1],
//                [1,1,0,1],
//                [1,1,1,1],
//                [1,1,0,0],
//                [1,0,0,1]], source = (0,1), destination = (2,2)
// Output: 3   ((0,1) -> (1,1) -> (2,1) -> (2,2))

// Example 2:
// Input: grid = [[1,1,1,1,1],
//                [1,1,1,1,1],
//                [1,1,1,1,0],
//                [1,0,1,0,1]], source = (0,0), destination = (3,4)
// Output: -1  ((3,4) has no open neighbour)

// Problem link: https://www.geeksforgeeks.org/problems/shortest-path-in-a-binary-maze-1655453161/1


using System;
using System.Collections.Generic;

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };

	// Algorithm: Every step costs 1, so this is the unit-weight shortest path ('21_...WithUnitWeights.cs'): a plain BFS.
	// The C++ code calls it "Dijkstra with a normal queue": with equal weights, Dijkstra's heap would pop the nodes in
	// exactly the BFS order anyway, so a queue is enough.
	// 1. dist[source] = 0, queue = [source].
	// 2. For each cell taken out, each open neighbour that gets a SHORTER distance (in BFS: that is not reached yet) gets
	//    dist + 1. If it is the destination, return at once: BFS reaches it first by the shortest way.
	// T.C: O(n * m)
	// S.C: O(n * m)
	public int ShortestPath(int[][] grid, int[] source, int[] destination)
	{
		int n = grid.Length, m = grid[0].Length;
		int sr = source[0], sc = source[1], er = destination[0], ec = destination[1];

		if (grid[sr][sc] == 0 || grid[er][ec] == 0) return -1;
		if (sr == er && sc == ec) return 0;

		int[,] dist = new int[n, m];
		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < m; c++) dist[r, c] = int.MaxValue;
		}
		dist[sr, sc] = 0;

		Queue<(int r, int c)> q = new Queue<(int r, int c)>();
		q.Enqueue((sr, sc));
		while (q.Count > 0)
		{
			var (r, c) = q.Dequeue();
			for (int k = 0; k < 4; k++)
			{
				int nr = r + dr[k], nc = c + dc[k];
				if (nr < 0 || nr >= n || nc < 0 || nc >= m || grid[nr][nc] == 0) continue;
				if (dist[r, c] + 1 >= dist[nr, nc]) continue;   // not shorter

				dist[nr, nc] = dist[r, c] + 1;
				if (nr == er && nc == ec) return dist[nr, nc];
				q.Enqueue((nr, nc));
			}
		}
		return -1;
	}
}


class Program
{
	static void Main()
	{
		int[][] grid =
		{
			new[] { 1, 1, 1, 1 }, new[] { 1, 1, 0, 1 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 0, 0 }, new[] { 1, 0, 0, 1 }
		};

		Console.WriteLine(new Solution().ShortestPath(grid, new[] { 0, 1 }, new[] { 2, 2 }));   // o/p: 3
	}
}

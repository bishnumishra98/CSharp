// Leetcode: 1631. Path With Minimum Effort   --->   heights[r][c] is the height of a cell. Walk from the top-left to the
// bottom-right cell (up / down / left / right). A route's EFFORT is the BIGGEST height difference between two
// neighbouring cells on it. Return the minimum effort.

// Example 1:
// Input: heights = [[1,2,2],
//                   [3,8,2],
//                   [5,3,5]]
// Output: 2   (1 -> 3 -> 5 -> 3 -> 5: the biggest step is 2. The route 1 -> 2 -> 2 -> 2 -> 5 has a step of 3.)

// Example 2:
// Input: heights = [[7,7],[7,7]]
// Output: 0

// First understand '24_DijkstraAlgorithm.cs'.


using System;
using System.Collections.Generic;

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };

	// Algorithm: Dijkstra, but a path's "length" is the MAXIMUM step on it instead of the SUM.
	//   effort[cell] = the smallest effort found so far to reach the cell.
	//   Going from a cell with effort e to a neighbour costs max(e, |height difference|).
	// Dijkstra still works because the cost of a path never DEcreases as it gets longer (a max can only grow, like a sum
	// of non-negative numbers). So the first time the target comes out of the heap, its effort is final: return it.
	// T.C: O(n * m * log(n * m))
	// S.C: O(n * m)
	public int MinimumEffortPath(int[][] heights)
	{
		int n = heights.Length, m = heights[0].Length;
		int[,] effort = new int[n, m];
		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < m; c++) effort[r, c] = int.MaxValue;
		}
		effort[0, 0] = 0;

		PriorityQueue<(int r, int c), int> pq = new PriorityQueue<(int r, int c), int>();
		pq.Enqueue((0, 0), 0);

		while (pq.TryDequeue(out var cell, out int e))
		{
			var (r, c) = cell;
			if (r == n - 1 && c == m - 1) return e;
			if (e > effort[r, c]) continue;   // outdated heap entry

			for (int k = 0; k < 4; k++)
			{
				int nr = r + dr[k], nc = c + dc[k];
				if (nr < 0 || nr >= n || nc < 0 || nc >= m) continue;

				int newEffort = Math.Max(e, Math.Abs(heights[nr][nc] - heights[r][c]));
				if (newEffort < effort[nr, nc])
				{
					effort[nr, nc] = newEffort;
					pq.Enqueue((nr, nc), newEffort);
				}
			}
		}
		return 0;   // never reached (every cell can be reached), but C# needs a return here
	}
}

// Another way: binary search on the answer E ("can we get there using only steps <= E ?", checked with BFS / DFS).
// T.C: O(n * m * log(max height)).


class Program
{
	static void Main()
	{
		int[][] heights = { new[] { 1, 2, 2 }, new[] { 3, 8, 2 }, new[] { 5, 3, 5 } };

		Console.WriteLine(new Solution().MinimumEffortPath(heights));   // o/p: 2
	}
}

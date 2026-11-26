// Leetcode: 994. Rotting Oranges   --->   A grid: 0 = empty, 1 = fresh orange, 2 = rotten orange. Every minute, each
// fresh orange next to (4 directions) a rotten one becomes rotten. Return the minimum minutes until no fresh orange is
// left, or -1 if some fresh orange can never rot.

// Example 1:
// Input: grid = [[2,1,1],
//                [1,1,0],
//                [0,1,1]]
// Output: 4

// Example 2:
// Input: grid = [[2,1,1],
//                [0,1,1],
//                [1,0,1]]
// Output: -1   (the bottom-left orange has no rotten neighbour path)

// Example 3:
// Input: grid = [[0,2]]
// Output: 0    (no fresh orange at the start)


// Dry run image (in this folder): 08_RottingOranges.jpg

using System;
using System.Collections.Generic;

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };

	// Algorithm (MULTI-SOURCE BFS): Rot spreads from ALL rotten oranges at the same time, one step per minute. BFS
	// spreads exactly like that, level by level, if we start it from ALL rotten oranges at once (put them all in the
	// queue at time 0).
	// 1. Put every rotten orange in the queue with time 0, and count the fresh ones.
	// 2. BFS: take out (r, c, t). Every fresh neighbour becomes rotten at time t + 1: mark it 2 (also works as "visited"),
	//    put it in the queue, and count it.
	// 3. The last time seen is the answer, but only if every fresh orange got rotten; else -1.
	// Why not DFS ? DFS follows one path deep before the others, so it does not give "minutes" (the time when each
	// orange is reached FIRST from the nearest rotten one).
	// T.C: O(n * m)
	// S.C: O(n * m);   the queue
	// Note: This changes 'grid'. Work on a copy if the caller still needs the original.
	public int OrangesRotting(int[][] grid)
	{
		int n = grid.Length, m = grid[0].Length, fresh = 0;
		Queue<(int r, int c, int t)> q = new Queue<(int r, int c, int t)>();

		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < m; c++)
			{
				if (grid[r][c] == 2) q.Enqueue((r, c, 0));
				else if (grid[r][c] == 1) fresh++;
			}
		}

		int minutes = 0, rotted = 0;
		while (q.Count > 0)
		{
			var (r, c, t) = q.Dequeue();
			minutes = Math.Max(minutes, t);

			for (int d = 0; d < 4; d++)
			{
				int nr = r + dr[d], nc = c + dc[d];
				if (nr < 0 || nr >= n || nc < 0 || nc >= m || grid[nr][nc] != 1) continue;

				grid[nr][nc] = 2;
				q.Enqueue((nr, nc, t + 1));
				rotted++;
			}
		}

		return rotted == fresh ? minutes : -1;
	}
}


class Program
{
	static void Main()
	{
		int[][] grid = { new[] { 2, 1, 1 }, new[] { 1, 1, 0 }, new[] { 0, 1, 1 } };

		Console.WriteLine(new Solution().OrangesRotting(grid));   // o/p: 4
	}
}

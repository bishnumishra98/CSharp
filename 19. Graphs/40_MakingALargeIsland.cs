// Leetcode: 827. Making A Large Island (GFG: Maximum Connected Group)   --->   An n x n grid of 0's and 1's. You may change
// AT MOST ONE 0 into a 1. Return the size of the biggest island (1's connected up / down / left / right) you can get.

// Example 1:
// Input: grid = [[1,1],
//                [0,1]]
// Output: 4   (turn the 0 into 1: all 4 cells connect)

// Example 2:
// Input: grid = [[1,0,1],
//                [1,0,1],
//                [1,0,1]]
// Output: 7   (turn the bottom-middle 0: the left column (3) + it (1) + the right column (3))

// Example 3:
// Input: grid = [[1,1],[1,1]]
// Output: 4   (no 0 to change)

// First understand '35_DisjointSet.cs'.


using System;
using System.Collections.Generic;

public class DisjointSet
{
	private int[] parent, size;

	public DisjointSet(int n)
	{
		parent = new int[n + 1];
		size = new int[n + 1];
		for (int i = 0; i <= n; i++)
		{
			parent[i] = i;
			size[i] = 1;
		}
	}

	public int Find(int x) => parent[x] == x ? x : (parent[x] = Find(parent[x]));

	public void Union(int u, int v)
	{
		int ru = Find(u), rv = Find(v);
		if (ru == rv) return;
		if (size[ru] < size[rv]) (ru, rv) = (rv, ru);
		parent[rv] = ru;
		size[ru] += size[rv];
	}

	public int SizeOf(int x) => size[Find(x)];
}

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };

	// Algorithm:
	// 1. Union every pair of neighbouring 1's. Now each island is a group, and the Disjoint Set knows its SIZE.
	// 2. Try every 0: turning it into a 1 joins it with the islands around it. New size = 1 + the sizes of its
	//    neighbouring islands, counting each island ONCE: two neighbours can belong to the same island, so collect their
	//    leaders in a HashSet first.
	// 3. The answer is the best of those. Also compare with the biggest existing island (the grid may have no 0 at all).
	// e.g., Example 2, the bottom-middle 0: left neighbour's island {left column} = 3, right neighbour's island = 3, its
	//       upper neighbour is a 0. 1 + 3 + 3 = 7.
	// T.C: O(n^2 * α(n^2))
	// S.C: O(n^2)
	public int LargestIsland(int[][] grid)
	{
		int n = grid.Length;
		DisjointSet ds = new DisjointSet(n * n);

		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < n; c++)
			{
				if (grid[r][c] == 0) continue;
				for (int k = 0; k < 4; k++)
				{
					int nr = r + dr[k], nc = c + dc[k];
					if (nr >= 0 && nr < n && nc >= 0 && nc < n && grid[nr][nc] == 1) ds.Union(r * n + c, nr * n + nc);
				}
			}
		}

		int best = 0;
		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < n; c++)
			{
				if (grid[r][c] == 1)
				{
					best = Math.Max(best, ds.SizeOf(r * n + c));   // an existing island
					continue;
				}

				HashSet<int> leaders = new HashSet<int>();
				for (int k = 0; k < 4; k++)
				{
					int nr = r + dr[k], nc = c + dc[k];
					if (nr >= 0 && nr < n && nc >= 0 && nc < n && grid[nr][nc] == 1) leaders.Add(ds.Find(nr * n + nc));
				}

				int size = 1;
				foreach (int leader in leaders) size += ds.SizeOf(leader);
				best = Math.Max(best, size);
			}
		}
		return best;
	}
}


class Program
{
	static void Main()
	{
		int[][] grid = { new[] { 1, 0, 1 }, new[] { 1, 0, 1 }, new[] { 1, 0, 1 } };

		Console.WriteLine(new Solution().LargestIsland(grid));   // o/p: 7
	}
}

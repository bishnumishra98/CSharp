// Leetcode: 947. Most Stones Removed with Same Row or Column (GFG: Maximum Stone Removal)   --->   stones[i] = [row, col]
// (at most one stone per point). A stone can be removed if ANOTHER stone that is still there shares its row or its
// column. Return the most stones that can be removed.

// Example 1:
// Input: stones = [[0,0],[0,1],[1,0],[1,2],[2,1],[2,2]]
// Output: 5   (all are linked through shared rows / columns; only one must stay)

// Example 2:
// Input: stones = [[0,0],[0,2],[1,1],[2,0],[2,2]]
// Output: 3   ({(0,0),(0,2),(2,0),(2,2)} are linked -> remove 3; (1,1) is alone -> remove 0)

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
}

public class Solution
{
	// Key fact: Stones that are linked (through shared rows or columns, step by step) form a GROUP. Inside a group, we can
	// always remove all stones but ONE (remove them in an order where the remaining ones stay linked, e.g., from the
	// leaves of a spanning tree inwards). Different groups can not help each other. So:
	//   answer = (number of stones) - (number of groups)
	// Finding the groups: instead of linking stones with each other (O(n^2) pairs), make every ROW and every COLUMN a node
	// of the Disjoint Set, and for each stone Union its row with its column. Stones on the same row or column then end up
	// in the same group automatically.
	// Numbering: rows are 0..maxRow, columns are maxRow + 1 .. maxRow + 1 + maxCol (shifted so they do not clash).
	// Count the groups: leaders among the rows / columns that actually HAVE a stone.
	// T.C: O(n + maxRow + maxCol)
	// S.C: O(maxRow + maxCol)
	public int RemoveStones(int[][] stones)
	{
		int maxRow = 0, maxCol = 0;
		foreach (int[] s in stones)
		{
			maxRow = Math.Max(maxRow, s[0]);
			maxCol = Math.Max(maxCol, s[1]);
		}

		DisjointSet ds = new DisjointSet(maxRow + maxCol + 1);
		HashSet<int> used = new HashSet<int>();   // the row / column nodes that have a stone
		foreach (int[] s in stones)
		{
			int rowNode = s[0], colNode = s[1] + maxRow + 1;
			ds.Union(rowNode, colNode);
			used.Add(rowNode);
			used.Add(colNode);
		}

		int groups = 0;
		foreach (int node in used)
		{
			if (ds.Find(node) == node) groups++;
		}
		return stones.Length - groups;
	}
}


class Program
{
	static void Main()
	{
		int[][] stones = { new[] { 0, 0 }, new[] { 0, 1 }, new[] { 1, 0 }, new[] { 1, 2 }, new[] { 2, 1 }, new[] { 2, 2 } };

		Console.WriteLine(new Solution().RemoveStones(stones));   // o/p: 5
	}
}

// GFG: Minimum Spanning Tree (with Kruskal's Algorithm)   --->   Same problem as '34_MinimumSpanningTree_Prims.cs': the
// total weight of the MST of a connected, undirected, weighted graph.

// Example 1:
// Input: V = 3, edges = [[0,1,5],[1,2,3],[0,2,1]]
// Output: 4

// Example 2:
// Input: V = 2, edges = [[0,1,5]]
// Output: 5

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

	public bool Union(int u, int v)   // false if they were already in the same group
	{
		int ru = Find(u), rv = Find(v);
		if (ru == rv) return false;
		if (size[ru] < size[rv]) (ru, rv) = (rv, ru);
		parent[rv] = ru;
		size[ru] += size[rv];
		return true;
	}
}

public class Solution
{
	// Kruskal's Algorithm (greedy): Look at ALL edges from the cheapest to the most expensive. Take an edge if it joins
	// two DIFFERENT groups (it does not close a cycle); skip it otherwise. The Disjoint Set answers "different groups ?"
	// in almost O(1). Stop is automatic: after V - 1 taken edges everything is one group and the rest are skipped.
	// e.g., sorted: 0-2 (1) take, 1-2 (3) take, 0-1 (5): 0 and 1 are already connected -> skip. Total 4.
	// Prim vs Kruskal: Prim grows one tree node by node (good for dense graphs); Kruskal joins forests edge by edge (good
	// for sparse graphs, and when the edges are already given as a list).
	// T.C: O(E log E);   the sort, plus E nearly-O(1) union-find steps
	// S.C: O(V + E)
	// Note: The C++ code built the edge list from an adjacency list, so every undirected edge appeared TWICE (u -> v and
	//       v -> u). That is harmless but sorts twice as many edges. Here each edge is listed once.
	public int SpanningTree(int V, int[][] edges)
	{
		int[][] sorted = (int[][])edges.Clone();
		Array.Sort(sorted, (a, b) => a[2].CompareTo(b[2]));   // by weight

		DisjointSet ds = new DisjointSet(V);
		int sum = 0;
		foreach (int[] e in sorted)
		{
			if (ds.Union(e[0], e[1])) sum += e[2];
		}
		return sum;
	}

// ------------------------------------------------------------------------------

	// Building the MST itself: keep the edges that were taken.
	// T.C: O(E log E)
	// S.C: O(V + E)
	public List<(int u, int v, int w)> BuildMST(int V, int[][] edges)
	{
		int[][] sorted = (int[][])edges.Clone();
		Array.Sort(sorted, (a, b) => a[2].CompareTo(b[2]));

		DisjointSet ds = new DisjointSet(V);
		List<(int u, int v, int w)> mst = new List<(int u, int v, int w)>();
		foreach (int[] e in sorted)
		{
			if (ds.Union(e[0], e[1])) mst.Add((e[0], e[1], e[2]));
		}
		return mst;
	}
}


class Program
{
	static void Main()
	{
		int[][] edges = { new[] { 0, 1, 5 }, new[] { 1, 2, 3 }, new[] { 0, 2, 1 } };

		Console.WriteLine(new Solution().SpanningTree(3, edges));                 // o/p: 4
		Console.WriteLine(string.Join(" ", new Solution().BuildMST(3, edges)));   // o/p: (0, 2, 1) (1, 2, 3)
	}
}

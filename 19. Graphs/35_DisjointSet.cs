// Disjoint Set (Union-Find, DSU)   --->   Keeps track of GROUPS of elements that do not overlap, and supports 2 fast
// operations:
// ● Find(x):      which group is x in ? (Returns the group's "leader", called the ultimate parent or root.)
// ● Union(x, y):  merge the groups of x and y into one.
// Two elements are in the same group exactly when Find gives the same leader.
// Used for: connected components that keep growing (edges arrive one by one), cycle detection in undirected graphs,
// Kruskal's MST ('36_MinimumSpanningTree_Kruskals.cs'), and many problems after it.
//
// ● How it is stored: every element points to a parent; a leader points to itself. Each group is a small tree.
//      parent: 1 -> 1, 2 -> 1, 3 -> 2      means {1, 2, 3} is one group, with leader 1.
//
// ● Two tricks make both operations almost O(1):
//   I.  Path compression (in Find): after finding the leader, point every node on the way DIRECTLY to it. The next Find
//       on them takes one step.
//   II. Union by rank or by size: attach the SMALLER tree under the BIGGER one, so the trees stay flat.
//       ● rank = an upper bound on the tree's height. Equal ranks: attach either way, and the new root's rank grows by 1.
//       ● size = the number of elements. Simpler to understand, and the size itself is often useful in problems.
//   With both: O(α(n)) per operation, where α is the inverse Ackermann function, < 5 for any real n. Practically O(1).
//   Without them: O(n) per operation in the worst case (a long chain).


using System;

public class DisjointSet
{
	private int[] parent, rank, size;

	// Elements 0..n (size n + 1, so it works for 0-based and 1-based nodes).
	public DisjointSet(int n)
	{
		parent = new int[n + 1];
		rank = new int[n + 1];
		size = new int[n + 1];
		for (int i = 0; i <= n; i++)
		{
			parent[i] = i;   // everyone starts as its own group
			size[i] = 1;
		}
	}

	// With path compression.
	public int Find(int x)
	{
		if (parent[x] == x) return x;
		return parent[x] = Find(parent[x]);   // point x straight to the leader
	}

	public void UnionByRank(int u, int v)
	{
		int ru = Find(u), rv = Find(v);
		if (ru == rv) return;   // already in the same group

		if (rank[ru] < rank[rv]) (ru, rv) = (rv, ru);   // make ru the one with the bigger (or equal) rank
		parent[rv] = ru;
		size[ru] += size[rv];                           // keep sizes right too, so SizeOf works with either union
		if (rank[ru] == rank[rv]) rank[ru]++;           // equal ranks: the tree gets one level taller
	}

	public void UnionBySize(int u, int v)
	{
		int ru = Find(u), rv = Find(v);
		if (ru == rv) return;

		if (size[ru] < size[rv]) (ru, rv) = (rv, ru);   // make ru the bigger one
		parent[rv] = ru;
		size[ru] += size[rv];
	}

	public int SizeOf(int x) => size[Find(x)];
}

public class Solution
{
	// Example use 1 - Leetcode 547. Number of Provinces (see '05_NumberOfProvinces.cs' for DFS / BFS):
	// Union every connected pair of cities. The number of provinces = the number of leaders (cities that are their own
	// leader).
	// T.C: O(n^2 * α(n))
	// S.C: O(n)
	public int FindCircleNum(int[][] isConnected)
	{
		int n = isConnected.Length;
		DisjointSet ds = new DisjointSet(n);

		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				if (isConnected[i][j] == 1) ds.UnionBySize(i, j);
			}
		}

		int provinces = 0;
		for (int i = 0; i < n; i++)
		{
			if (ds.Find(i) == i) provinces++;
		}
		return provinces;
	}

// ------------------------------------------------------------------------------

	// Example use 2 - Cycle in an undirected graph (see '09_DetectCycleInUndirectedGraph.cs' for BFS / DFS):
	// Add the edges one by one. If an edge joins two nodes that are ALREADY in the same group, there was already a path
	// between them, and this edge closes a cycle.
	// T.C: O(E * α(V))
	// S.C: O(V)
	public bool HasCycle(int V, int[][] edges)
	{
		DisjointSet ds = new DisjointSet(V);
		foreach (int[] e in edges)
		{
			if (ds.Find(e[0]) == ds.Find(e[1])) return true;
			ds.UnionBySize(e[0], e[1]);
		}
		return false;
	}
}


class Program
{
	static void Main()
	{
		DisjointSet ds = new DisjointSet(7);
		ds.UnionByRank(1, 2);
		ds.UnionByRank(2, 3);
		ds.UnionByRank(4, 5);
		ds.UnionByRank(6, 7);
		ds.UnionByRank(5, 6);
		Console.WriteLine(ds.Find(3) == ds.Find(7));   // o/p: False   ({1,2,3} and {4,5,6,7})
		ds.UnionBySize(3, 7);
		Console.WriteLine(ds.Find(3) == ds.Find(7));   // o/p: True
		Console.WriteLine(ds.SizeOf(1));               // o/p: 7

		int[][] isConnected = { new[] { 1, 1, 0 }, new[] { 1, 1, 0 }, new[] { 0, 0, 1 } };
		Console.WriteLine(new Solution().FindCircleNum(isConnected));   // o/p: 2

		int[][] edges = { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 2, 0 } };
		Console.WriteLine(new Solution().HasCycle(3, edges));           // o/p: True
	}
}

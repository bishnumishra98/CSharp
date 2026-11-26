// Articulation Point (Cut Vertex)   --->   In an undirected graph, a NODE whose removal (with all its edges) splits the
// graph into more pieces.

// GFG: Articulation Point - I   --->   Given an undirected graph with V nodes, return all articulation points in increasing
// order, or [-1] if there are none.

// Example 1:
//    1 --- 0
//    |
//    4 --- 3
//     \   /
//       2
// Input: V = 5, edges = [[0,1],[1,4],[4,2],[4,3],[2,3]]
// Output: 1 4
// Explanation: removing 1 cuts 0 off; removing 4 separates {0, 1} from {2, 3}.

// Problem link: https://www.geeksforgeeks.org/problems/articulation-point-1/1

// First understand '43_BridgesInGraph.cs' (disc / low).


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm (Tarjan, like bridges, with 2 differences):
	// I.  For a child v of u (u is not the DFS root):  if low[v] >= disc[u], u is an articulation point.
	//     ">=" instead of ">": if v's part can climb back only up to u itself (not above), then removing u (the NODE)
	//     still cuts it off. (For a bridge, reaching u was enough, because only the edge was removed.)
	// II. The DFS ROOT has no ancestors, so the rule above does not apply. The root is an articulation point only if it
	//     has 2 or more DFS CHILDREN (separate subtrees that are connected only through the root).
	// For visited neighbours, use disc[v] (not low[v]): low must mean "reachable with ONE back edge", otherwise it could
	// climb through another articulation point and hide it.
	// T.C: O(V + E)
	// S.C: O(V + E)
	// Note: The C++ main built the adjacency list inconsistently (node 1 listed 4 but not 0, and 4 did not list 1), which
	//       does not match its own picture. Here the graph is built from an edge list, so both directions always match.
	public List<int> ArticulationPoints(int V, int[][] edges)
	{
		List<int>[] adj = new List<int>[V];
		for (int i = 0; i < V; i++) adj[i] = new List<int>();
		foreach (int[] e in edges)
		{
			adj[e[0]].Add(e[1]);
			adj[e[1]].Add(e[0]);
		}

		int[] disc = new int[V], low = new int[V];
		Array.Fill(disc, -1);
		bool[] isCut = new bool[V];
		int timer = 0;

		for (int i = 0; i < V; i++)
		{
			if (disc[i] == -1) Dfs(i, -1, adj, disc, low, isCut, ref timer);
		}

		List<int> result = new List<int>();
		for (int i = 0; i < V; i++)
		{
			if (isCut[i]) result.Add(i);   // already in increasing order: no sort needed
		}
		return result.Count > 0 ? result : new List<int> { -1 };
	}

	private void Dfs(int u, int parent, List<int>[] adj, int[] disc, int[] low, bool[] isCut, ref int timer)
	{
		disc[u] = low[u] = timer++;
		int children = 0;

		foreach (int v in adj[u])
		{
			if (v == parent) continue;

			if (disc[v] == -1)
			{
				children++;
				Dfs(v, u, adj, disc, low, isCut, ref timer);
				low[u] = Math.Min(low[u], low[v]);
				if (parent != -1 && low[v] >= disc[u]) isCut[u] = true;
			}
			else
			{
				low[u] = Math.Min(low[u], disc[v]);
			}
		}

		if (parent == -1 && children > 1) isCut[u] = true;   // the root rule
	}
}


class Program
{
	static void Main()
	{
		int[][] edges = { new[] { 0, 1 }, new[] { 1, 4 }, new[] { 4, 2 }, new[] { 4, 3 }, new[] { 2, 3 } };

		Console.WriteLine(string.Join(" ", new Solution().ArticulationPoints(5, edges)));   // o/p: 1 4
	}
}

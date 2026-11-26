// Spanning Tree   --->   For a connected undirected graph with V nodes, a spanning tree is a set of edges that:
// ● reaches ALL V nodes,
// ● has exactly V - 1 edges,
// ● has no cycle (so it is a tree).
// A graph can have many spanning trees.
// Minimum Spanning Tree (MST)   --->   In a WEIGHTED graph, the spanning tree with the smallest total weight. (There can be
// more than one MST with the same total.) e.g., the cheapest way to connect all houses with cables.

// GFG: Minimum Spanning Tree   --->   Given a connected, undirected, weighted graph, return the total weight of its MST.

// Example 1:
// Input: V = 3, edges = [[0,1,5],[1,2,3],[0,2,1]]
//          0
//       5 / \ 1
//        1 - 2
//          3
// Output: 4   (edges 0-2 (1) and 1-2 (3))

// Example 2:
// Input: V = 2, edges = [[0,1,5]]
// Output: 5

// Problem link: https://www.geeksforgeeks.org/problems/minimum-spanning-tree/1


using System;
using System.Collections.Generic;

public class Solution
{
	private List<(int to, int w)>[] BuildGraph(int V, int[][] edges)
	{
		List<(int to, int w)>[] adj = new List<(int to, int w)>[V];
		for (int i = 0; i < V; i++) adj[i] = new List<(int to, int w)>();
		foreach (int[] e in edges)
		{
			adj[e[0]].Add((e[1], e[2]));
			adj[e[1]].Add((e[0], e[2]));
		}
		return adj;
	}

	// Prim's Algorithm (greedy): Grow ONE tree from node 0. At every step, add the CHEAPEST edge that connects a node
	// already in the tree to a node NOT in the tree yet.
	// 1. Min-heap of (node, edge weight to reach it). Start with (0, 0).
	// 2. Take out the cheapest. If the node is already in the tree, skip it (a cheaper edge got it first). Otherwise add
	//    it to the tree and add its edge weight to the sum.
	// 3. Push every edge from it to a node not in the tree yet.
	// Why is the cheapest crossing edge always safe ? The tree must connect to the rest somehow; swapping any more
	// expensive crossing edge for this one can only lower the total.
	// e.g., Example 1: take 0 (0). Push (1,5), (2,1). Take 2 (1): sum 1. Push (1,3). Take 1 (3): sum 4. (1,5): skip.
	// T.C: O(E log E)
	// S.C: O(V + E)
	public int SpanningTree(int V, int[][] edges)
	{
		var adj = BuildGraph(V, edges);
		bool[] inTree = new bool[V];
		int sum = 0;

		PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
		pq.Enqueue(0, 0);
		while (pq.TryDequeue(out int u, out int w))
		{
			if (inTree[u]) continue;
			inTree[u] = true;
			sum += w;

			foreach (var (v, wt) in adj[u])
			{
				if (!inTree[v]) pq.Enqueue(v, wt);
			}
		}
		return sum;
	}

// ------------------------------------------------------------------------------

	// Building the MST itself: the same, but the heap also remembers WHICH tree node the edge comes from (its parent),
	// and every node added (except the first) gives an MST edge (parent, node, weight).
	// T.C: O(E log E)
	// S.C: O(V + E)
	public List<(int u, int v, int w)> BuildMST(int V, int[][] edges)
	{
		var adj = BuildGraph(V, edges);
		bool[] inTree = new bool[V];
		List<(int u, int v, int w)> mst = new List<(int u, int v, int w)>();

		PriorityQueue<(int node, int parent), int> pq = new PriorityQueue<(int node, int parent), int>();
		pq.Enqueue((0, -1), 0);
		while (pq.TryDequeue(out var item, out int w))
		{
			var (u, parent) = item;
			if (inTree[u]) continue;
			inTree[u] = true;
			if (parent != -1) mst.Add((parent, u, w));

			foreach (var (v, wt) in adj[u])
			{
				if (!inTree[v]) pq.Enqueue((v, u), wt);
			}
		}
		return mst;
	}
}


class Program
{
	static void Main()
	{
		int[][] edges = { new[] { 0, 1, 5 }, new[] { 1, 2, 3 }, new[] { 0, 2, 1 } };

		Console.WriteLine(new Solution().SpanningTree(3, edges));                       // o/p: 4
		Console.WriteLine(string.Join(" ", new Solution().BuildMST(3, edges)));         // o/p: (0, 2, 1) (2, 1, 3)
	}
}

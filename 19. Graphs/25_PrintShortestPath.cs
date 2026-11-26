// GFG: Shortest Path in Weighted Undirected Graph   --->   n nodes (1..n), undirected edges [a, b, w]. Find the shortest
// path from node 1 to node n. Return a list: first the total weight, then the nodes on the path in order. If there is
// no path, return [-1].

// Example 1:
// Input: n = 5, edges = [[1,2,2],[2,5,5],[2,3,4],[1,4,1],[4,3,3],[3,5,1]]
// Output: 5 1 4 3 5   (weight 5, path 1 -> 4 -> 3 -> 5: 1 + 3 + 1)

// Example 2:
// Input: n = 2, edges = [[1,2,2]]
// Output: 2 1 2

// Example 3:
// Input: n = 2, edges = []
// Output: -1

// Problem link: https://www.geeksforgeeks.org/problems/shortest-path-in-weighted-undirected-graph/1

// First understand '24_DijkstraAlgorithm.cs'. Dijkstra gives the DISTANCES; here we also need the PATH.


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm: Dijkstra, plus a 'parent' array: whenever dist[v] improves through u, remember parent[v] = u (the node
	// we came from on the best path so far). At the end, follow the parents back from n to 1, and reverse.
	// e.g., parent[5] = 3, parent[3] = 4, parent[4] = 1 -> 5, 3, 4, 1 -> reversed 1 4 3 5.
	// T.C: O(E log V) + O(V) for the path
	// S.C: O(V + E)
	public List<int> ShortestPath(int n, int[][] edges)
	{
		List<(int to, int w)>[] adj = new List<(int to, int w)>[n + 1];
		for (int i = 0; i <= n; i++) adj[i] = new List<(int to, int w)>();
		foreach (int[] e in edges)
		{
			adj[e[0]].Add((e[1], e[2]));
			adj[e[1]].Add((e[0], e[2]));
		}

		int[] dist = new int[n + 1], parent = new int[n + 1];
		Array.Fill(dist, int.MaxValue);
		for (int i = 0; i <= n; i++) parent[i] = i;
		dist[1] = 0;

		PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
		pq.Enqueue(1, 0);
		while (pq.TryDequeue(out int u, out int d))
		{
			if (d > dist[u]) continue;
			foreach (var (v, w) in adj[u])
			{
				if (d + w < dist[v])
				{
					dist[v] = d + w;
					parent[v] = u;
					pq.Enqueue(v, dist[v]);
				}
			}
		}

		if (dist[n] == int.MaxValue) return new List<int> { -1 };

		List<int> path = new List<int>();
		for (int node = n; node != 1; node = parent[node]) path.Add(node);
		path.Add(1);
		path.Reverse();

		path.Insert(0, dist[n]);   // the weight goes first
		return path;
	}
}


class Program
{
	static void Main()
	{
		int[][] edges =
		{
			new[] { 1, 2, 2 }, new[] { 2, 5, 5 }, new[] { 2, 3, 4 }, new[] { 1, 4, 1 }, new[] { 4, 3, 3 }, new[] { 3, 5, 1 }
		};

		Console.WriteLine(string.Join(" ", new Solution().ShortestPath(5, edges)));   // o/p: 5 1 4 3 5
	}
}

// GFG: Shortest Path in a Directed Acyclic Graph   --->   A DAG with n nodes (0..n-1); edges[i] = [u, v, w] is a directed
// edge u -> v with weight w. Return the shortest distance from node 0 (the source) to every node, or -1 if a node can
// not be reached.

// Example 1:
// Input: n = 4, edges = [[0,1,2],[0,2,1]]
// Output: 0 2 1 -1

// Example 2:
// Input: n = 6, edges = [[0,1,2],[0,4,1],[4,5,4],[4,2,2],[1,2,3],[2,3,6],[5,3,1]]
// Output: 0 2 3 6 1 5
// Explanation: to 2: 0 -> 4 -> 2 (1 + 2 = 3).  to 3: 0 -> 4 -> 5 -> 3 (1 + 4 + 1 = 6).

// ● RELAXING an edge u -> v (weight w): if going through u is shorter than the best known way to v, use it:
//     if (dist[u] + w < dist[v]) dist[v] = dist[u] + w;
//   Every shortest path algorithm in this folder is built on this one line. They differ in the ORDER of relaxing.

// First understand '15_TopologicalSort.cs'.


// Dry run image (in this folder): 20_ShortestPathInDAG.jpg

using System;
using System.Collections.Generic;

public class Solution
{
	private const int INF = int.MaxValue;

	// Algorithm: In a DAG, relax the nodes in TOPOLOGICAL order. When a node u is processed, every edge INTO u comes from
	// a node that was processed earlier, so dist[u] is already final. Relaxing u's outgoing edges then gives correct
	// values to the nodes after it. One pass is enough, and negative weights are fine too.
	// 1. Topological sort (DFS + stack).
	// 2. dist[src] = 0, everything else = infinity.
	// 3. Take the nodes in topological order; for each reachable node, relax all its outgoing edges.
	// 4. Infinity left = unreachable -> -1.
	// T.C: O(n + m)
	// S.C: O(n + m)
	public int[] ShortestPath(int n, int[][] edges)
	{
		List<(int to, int w)>[] adj = new List<(int to, int w)>[n];
		for (int i = 0; i < n; i++) adj[i] = new List<(int to, int w)>();
		foreach (int[] e in edges) adj[e[0]].Add((e[1], e[2]));

		bool[] visited = new bool[n];
		Stack<int> topo = new Stack<int>();
		for (int i = 0; i < n; i++)
		{
			if (!visited[i]) Dfs(i, adj, visited, topo);
		}

		int[] dist = new int[n];
		Array.Fill(dist, INF);
		dist[0] = 0;

		while (topo.Count > 0)
		{
			int u = topo.Pop();
			if (dist[u] == INF) continue;   // not reachable from the source (also avoids INF + w overflowing)

			foreach (var (v, w) in adj[u])
			{
				if (dist[u] + w < dist[v]) dist[v] = dist[u] + w;   // relax
			}
		}

		for (int i = 0; i < n; i++)
		{
			if (dist[i] == INF) dist[i] = -1;
		}
		return dist;
	}

	private void Dfs(int node, List<(int to, int w)>[] adj, bool[] visited, Stack<int> topo)
	{
		visited[node] = true;
		foreach (var (next, _) in adj[node])
		{
			if (!visited[next]) Dfs(next, adj, visited, topo);
		}
		topo.Push(node);
	}
}

// Note: The C++ code also relaxed edges out of UNREACHABLE nodes (dist = 1e9). It still worked (1e9 + w is never
//       smaller than 1e9), but skipping them is clearer and is required when INF is int.MaxValue.


class Program
{
	static void Main()
	{
		int[][] edges =
		{
			new[] { 0, 1, 2 }, new[] { 0, 4, 1 }, new[] { 4, 5, 4 }, new[] { 4, 2, 2 },
			new[] { 1, 2, 3 }, new[] { 2, 3, 6 }, new[] { 5, 3, 1 }
		};

		Console.WriteLine(string.Join(" ", new Solution().ShortestPath(6, edges)));   // o/p: 0 2 3 6 1 5
	}
}

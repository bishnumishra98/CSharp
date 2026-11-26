// GFG: Shortest Path in Undirected Graph having Unit Distance   --->   An undirected graph with n nodes, where EVERY edge
// has weight 1. Return the shortest distance from src to every node, or -1 if unreachable.

// Example 1:
// Input: n = 9, edges = [[0,1],[0,3],[3,4],[4,5],[5,6],[1,2],[2,6],[6,7],[7,8],[6,8]], src = 0
// Output: 0 1 2 1 2 3 3 4 4

// Example 2:
// Input: n = 4, edges = [[1,3],[3,0]], src = 3
// Output: 1 1 -1 0


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm: When every edge costs 1, the shortest distance = the fewest edges = the BFS LEVEL. BFS reaches all nodes
	// at distance 1 before any node at distance 2, and so on. So the FIRST time BFS reaches a node, that distance is the
	// shortest; it never needs to be changed.
	// 1. dist[src] = 0, everything else = -1 (not reached yet). Put src in the queue.
	// 2. Take out a node u. Every neighbour v not reached yet gets dist[v] = dist[u] + 1 and goes into the queue.
	// T.C: O(n + m)
	// S.C: O(n + m)
	// Note: The C++ code checked "dist[u] + 1 < dist[v]" (the relaxing idea, see '20_ShortestPathInDAG.cs'). With BFS that
	//       check can only be true the first time, so "not reached yet" is the same thing, and simpler.
	public int[] ShortestPath(int n, int[][] edges, int src)
	{
		List<int>[] adj = new List<int>[n];
		for (int i = 0; i < n; i++) adj[i] = new List<int>();
		foreach (int[] e in edges)
		{
			adj[e[0]].Add(e[1]);
			adj[e[1]].Add(e[0]);
		}

		int[] dist = new int[n];
		Array.Fill(dist, -1);
		dist[src] = 0;

		Queue<int> q = new Queue<int>();
		q.Enqueue(src);
		while (q.Count > 0)
		{
			int u = q.Dequeue();
			foreach (int v in adj[u])
			{
				if (dist[v] != -1) continue;
				dist[v] = dist[u] + 1;
				q.Enqueue(v);
			}
		}
		return dist;
	}
}


class Program
{
	static void Main()
	{
		int[][] edges =
		{
			new[] { 0, 1 }, new[] { 0, 3 }, new[] { 3, 4 }, new[] { 4, 5 }, new[] { 5, 6 },
			new[] { 1, 2 }, new[] { 2, 6 }, new[] { 6, 7 }, new[] { 7, 8 }, new[] { 6, 8 }
		};

		Console.WriteLine(string.Join(" ", new Solution().ShortestPath(9, edges, 0)));   // o/p: 0 1 2 1 2 3 3 4 4
	}
}

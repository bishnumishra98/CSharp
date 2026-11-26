// Bellman-Ford Algorithm   --->   The shortest distance from a source to every node in a DIRECTED weighted graph, which may
// have NEGATIVE edge weights (Dijkstra can not handle those). It can also DETECT a negative cycle.
// ● For an undirected graph, add each edge in both directions (u -> v and v -> u). (Careful: an undirected edge with a
//   negative weight is itself a negative cycle u -> v -> u.)
// ● Slower than Dijkstra: O(V * E) instead of O(E log V). Used in network routing (RIP).

// GFG: Bellman-Ford   --->   V nodes, directed edges [u, v, w], source src. Return the shortest distance to every node
// (10^8 for unreachable ones). If a negative cycle can be reached from src, return [-1].

// Example 1:
// Input: V = 2, edges = [[0,1,9]], src = 0
// Output: 0 9

// Example 2:
// Input: V = 3, edges = [[0,1,5],[1,0,3],[1,2,-1],[2,0,1]], src = 2
// Output: 1 6 0   (to 0: 2 -> 0 = 1.  to 1: 2 -> 0 -> 1 = 1 + 5 = 6)

// Problem link: https://www.geeksforgeeks.org/problems/distance-from-the-source-bellman-ford-algorithm/1

// ● Negative cycle: a cycle whose weights add up to less than 0. Going round it again and again makes the distance
//   smaller forever, so "shortest distance" does not exist.


using System;

public class Solution
{
	private const int INF = 100_000_000;   // 10^8, as the problem asks

	// Algorithm: Relax EVERY edge, V - 1 times.
	// Why V - 1 rounds ? A shortest path (without a negative cycle) uses at most V - 1 edges (more would repeat a node).
	// After round 1, every shortest path of 1 edge is correct; after round 2, every one of 2 edges; ... after round V - 1,
	// all of them.
	// Negative cycle check: do ONE more round. If any distance still gets smaller, some path keeps improving forever:
	// a negative cycle.
	// T.C: O(V * E)
	// S.C: O(V)
	public int[] BellmanFord(int V, int[][] edges, int src)
	{
		int[] dist = new int[V];
		Array.Fill(dist, INF);
		dist[src] = 0;

		for (int round = 0; round < V - 1; round++)
		{
			foreach (int[] e in edges)
			{
				int u = e[0], v = e[1], w = e[2];
				if (dist[u] != INF && dist[u] + w < dist[v]) dist[v] = dist[u] + w;
			}
		}

		foreach (int[] e in edges)   // the V-th round
		{
			int u = e[0], v = e[1], w = e[2];
			if (dist[u] != INF && dist[u] + w < dist[v]) return new int[] { -1 };
		}
		return dist;
	}
}

// Small speed-up: if a whole round changes nothing, every later round would change nothing too: stop early.


class Program
{
	static void Main()
	{
		int[][] edges = { new[] { 0, 1, 5 }, new[] { 1, 0, 3 }, new[] { 1, 2, -1 }, new[] { 2, 0, 1 } };

		Console.WriteLine(string.Join(" ", new Solution().BellmanFord(3, edges, 2)));   // o/p: 1 6 0
	}
}

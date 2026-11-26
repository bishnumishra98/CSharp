// Dijkstra's Algorithm   --->   The shortest distance from a source node to EVERY node, in a graph whose edge weights are
// NON-NEGATIVE (weights may differ, unlike '21_...WithUnitWeights.cs').
// ● Works on directed and undirected graphs. It is GREEDY: always finish the closest unfinished node next.
// ● Does NOT work with negative edge weights (use Bellman-Ford, '31_BellmanFordAlgorithm.cs').
// ● Used in maps / GPS, network routing.

// GFG: Implementing Dijkstra   --->   V nodes, undirected weighted edges [u, v, w], and a source src. Return the shortest
// distance from src to every node.

// Example 1:
// Input: V = 2, edges = [[0,1,9]], src = 0
// Output: 0 9

// Example 2:
//         0
//      1 / \ 6
//       /   \
//      1 --- 2
//         3
// Input: V = 3, edges = [[0,1,1],[1,2,3],[0,2,6]], src = 2
// Output: 4 3 0   (to 0: 2 -> 1 -> 0 = 3 + 1 = 4, shorter than the direct edge 6)


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

	// I. With a min-heap (PriorityQueue, see '16. Heaps/05_PriorityQueue.cs'):
	// 1. dist[] = infinity, dist[src] = 0. Heap: (src, priority 0).
	// 2. Take out the node u with the SMALLEST distance d. Relax its edges: if d + w < dist[v], update dist[v] and push
	//    (v, new distance) into the heap.
	// 3. Repeat until the heap is empty.
	// Why is the smallest one FINAL when it comes out ? All weights are >= 0, so any other route to u would go through a
	// node that is at least as far away, and then add more: it can not be shorter.
	// ● Stale entries: a node can be pushed several times (every time its distance improves). C#'s PriorityQueue can not
	//   change an item's priority, so the old, larger entries stay in the heap. When one comes out, d > dist[u]: it is
	//   outdated, skip it. (The C++ code did not skip them. The answer was still right, but outdated entries re-scanned
	//   their whole neighbour list for nothing.)
	// e.g., Example 2 from 2: pop (2, 0): dist[1] = 3, dist[0] = 6. pop (1, 3): 3 + 1 = 4 < 6 -> dist[0] = 4, push (0, 4).
	//       pop (0, 4): no improvement. pop (0, 6): 6 > dist[0] = 4, outdated, skip.
	// T.C: O(E log V);   each edge can push one entry, and each heap operation costs O(log E) = O(log V)
	// S.C: O(V + E)
	public int[] Dijkstra(int V, int[][] edges, int src)
	{
		var adj = BuildGraph(V, edges);
		int[] dist = new int[V];
		Array.Fill(dist, int.MaxValue);
		dist[src] = 0;

		PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
		pq.Enqueue(src, 0);

		while (pq.TryDequeue(out int u, out int d))
		{
			if (d > dist[u]) continue;   // outdated entry

			foreach (var (v, w) in adj[u])
			{
				if (d + w < dist[v])
				{
					dist[v] = d + w;
					pq.Enqueue(v, dist[v]);
				}
			}
		}
		return dist;
	}

// ------------------------------------------------------------------------------

	// II. With a SortedSet of (distance, node) pairs (the C++ "set" version):
	//     A SortedSet can REMOVE any item, so when a node's distance improves, remove its old pair first and add the new
	//     one. Every node is in the set at most once: no outdated entries. Min = the closest unfinished node.
	// T.C: O(E log V)
	// S.C: O(V)
	public int[] Dijkstra_SortedSet(int V, int[][] edges, int src)
	{
		var adj = BuildGraph(V, edges);
		int[] dist = new int[V];
		Array.Fill(dist, int.MaxValue);
		dist[src] = 0;

		SortedSet<(int d, int node)> set = new SortedSet<(int d, int node)> { (0, src) };

		while (set.Count > 0)
		{
			var (d, u) = set.Min;
			set.Remove(set.Min);

			foreach (var (v, w) in adj[u])
			{
				if (d + w >= dist[v]) continue;

				if (dist[v] != int.MaxValue) set.Remove((dist[v], v));   // drop the old pair
				dist[v] = d + w;
				set.Add((dist[v], v));
			}
		}
		return dist;
	}
}

// Note: unreachable nodes keep int.MaxValue. Many problems want -1 or "1e9" instead; convert at the end.


class Program
{
	static void Main()
	{
		int[][] edges = { new[] { 0, 1, 1 }, new[] { 1, 2, 3 }, new[] { 0, 2, 6 } };

		Console.WriteLine(string.Join(" ", new Solution().Dijkstra(3, edges, 2)));             // o/p: 4 3 0
		Console.WriteLine(string.Join(" ", new Solution().Dijkstra_SortedSet(3, edges, 2)));   // o/p: 4 3 0
	}
}

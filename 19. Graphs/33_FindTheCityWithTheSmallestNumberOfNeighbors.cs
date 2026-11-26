// Leetcode: 1334. Find the City With the Smallest Number of Neighbors at a Threshold Distance   --->   n cities, two-way
// weighted roads [u, v, w], and a distanceThreshold. For every city, count the OTHER cities reachable with a shortest
// distance <= distanceThreshold. Return the city with the SMALLEST count; on a tie, the city with the BIGGEST number.

// Example 1:
// Input: n = 4, edges = [[0,1,3],[1,2,1],[1,3,4],[2,3,1]], distanceThreshold = 4
// Output: 3
// Explanation: reachable within 4: city 0 -> {1, 2}, 1 -> {0, 2, 3}, 2 -> {0, 1, 3}, 3 -> {1, 2}.
//              Cities 0 and 3 both have 2; the bigger number is 3.

// Example 2:
// Input: n = 5, edges = [[0,1,2],[0,4,8],[1,2,3],[1,4,2],[2,3,1],[3,4,1]], distanceThreshold = 2
// Output: 0   (city 0 reaches only city 1)

// We need the shortest distance between EVERY pair of cities: an all-pairs shortest path problem.


using System;
using System.Collections.Generic;

public class Solution
{
	private const int INF = int.MaxValue;

	// Count, for every city, how many others are within the threshold, and pick the answer (ties -> bigger city:
	// '<=' lets a later city with the same count win).
	private int PickCity(int n, int[,] dist, int threshold)
	{
		int bestCity = -1, bestCount = int.MaxValue;
		for (int city = 0; city < n; city++)
		{
			int count = 0;
			for (int other = 0; other < n; other++)
			{
				if (other != city && dist[city, other] <= threshold) count++;
			}
			if (count <= bestCount)
			{
				bestCount = count;
				bestCity = city;
			}
		}
		return bestCity;
	}

	// I. Floyd-Warshall (see '32_FloydWarshallAlgorithm.cs').
	// T.C: O(n^3)
	// S.C: O(n^2)
	public int FindTheCity_FloydWarshall(int n, int[][] edges, int distanceThreshold)
	{
		int[,] dist = new int[n, n];
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++) dist[i, j] = i == j ? 0 : INF;
		}
		foreach (int[] e in edges)
		{
			dist[e[0], e[1]] = e[2];
			dist[e[1], e[0]] = e[2];
		}

		for (int k = 0; k < n; k++)
		{
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					if (dist[i, k] == INF || dist[k, j] == INF) continue;
					dist[i, j] = Math.Min(dist[i, j], dist[i, k] + dist[k, j]);
				}
			}
		}
		return PickCity(n, dist, distanceThreshold);
	}

// ------------------------------------------------------------------------------

	// II. Dijkstra from EVERY city (see '24_DijkstraAlgorithm.cs'). All weights are positive, so Dijkstra works.
	// T.C: O(n * E log n);   better than O(n^3) when the graph has few roads
	// S.C: O(n^2 + E)
	public int FindTheCity(int n, int[][] edges, int distanceThreshold)
	{
		List<(int to, int w)>[] adj = new List<(int to, int w)>[n];
		for (int i = 0; i < n; i++) adj[i] = new List<(int to, int w)>();
		foreach (int[] e in edges)
		{
			adj[e[0]].Add((e[1], e[2]));
			adj[e[1]].Add((e[0], e[2]));
		}

		int[,] dist = new int[n, n];
		for (int src = 0; src < n; src++)
		{
			for (int j = 0; j < n; j++) dist[src, j] = INF;
			dist[src, src] = 0;

			PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
			pq.Enqueue(src, 0);
			while (pq.TryDequeue(out int u, out int d))
			{
				if (d > dist[src, u]) continue;
				foreach (var (v, w) in adj[u])
				{
					if (d + w < dist[src, v])
					{
						dist[src, v] = d + w;
						pq.Enqueue(v, dist[src, v]);
					}
				}
			}
		}
		return PickCity(n, dist, distanceThreshold);
	}
}


class Program
{
	static void Main()
	{
		int[][] edges = { new[] { 0, 1, 3 }, new[] { 1, 2, 1 }, new[] { 1, 3, 4 }, new[] { 2, 3, 1 } };

		Console.WriteLine(new Solution().FindTheCity_FloydWarshall(4, edges, 4));   // o/p: 3
		Console.WriteLine(new Solution().FindTheCity(4, edges, 4));                 // o/p: 3
	}
}

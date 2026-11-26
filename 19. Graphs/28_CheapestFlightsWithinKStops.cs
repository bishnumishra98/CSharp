// Leetcode: 787. Cheapest Flights Within K Stops   --->   n cities, flights[i] = [from, to, price] (one way). Return the
// cheapest price from src to dst using AT MOST k stops (k cities in between), or -1 if there is no such route.

// Example 1:
// Input: n = 4, flights = [[0,1,100],[1,2,100],[2,0,100],[1,3,600],[2,3,200]], src = 0, dst = 3, k = 1
// Output: 700   (0 -> 1 -> 3. The cheaper 0 -> 1 -> 2 -> 3 = 400 uses 2 stops.)

// Example 2:
// Input: n = 3, flights = [[0,1,100],[1,2,100],[0,2,500]], src = 0, dst = 2, k = 0
// Output: 500   (no stop allowed: only the direct flight)

// Why plain Dijkstra fails: Dijkstra keeps only the CHEAPEST way to each city. But a cheap way that used many stops may
// be useless later, while a more expensive way with fewer stops can still reach dst within k. The number of stops must
// be part of the decision.


using System;
using System.Collections.Generic;

public class Solution
{
	// I. BFS by number of STOPS (from the C++ code):
	//    The queue holds (stops, city, cost). BFS takes the entries in order of stops, level by level. From a city reached
	//    with s stops, each flight leads to a city reached with s + 1 flights. Keep dist[city] = the cheapest cost seen,
	//    and only push a neighbour if this way is CHEAPER (a more expensive way with more stops can never help).
	//    An entry with stops > k is not expanded: flying on from it would use too many stops.
	// T.C: O(k * E) in the worst case;   a city can be pushed again each time a cheaper way is found, once per level
	// S.C: O(n + E)
	// Note: The C++ comment said O(n + E). Cities can be pushed many times (once per level), which is where k comes in.
	public int FindCheapestPrice_BFS(int n, int[][] flights, int src, int dst, int k)
	{
		List<(int to, int price)>[] adj = new List<(int to, int price)>[n];
		for (int i = 0; i < n; i++) adj[i] = new List<(int to, int price)>();
		foreach (int[] f in flights) adj[f[0]].Add((f[1], f[2]));

		int[] dist = new int[n];
		Array.Fill(dist, int.MaxValue);
		dist[src] = 0;

		Queue<(int stops, int city, int cost)> q = new Queue<(int stops, int city, int cost)>();
		q.Enqueue((0, src, 0));

		while (q.Count > 0)
		{
			var (stops, city, cost) = q.Dequeue();
			if (stops > k) continue;

			foreach (var (next, price) in adj[city])
			{
				if (cost + price < dist[next])
				{
					dist[next] = cost + price;
					q.Enqueue((stops + 1, next, cost + price));
				}
			}
		}
		return dist[dst] == int.MaxValue ? -1 : dist[dst];
	}

// ------------------------------------------------------------------------------

	// II. Bellman-Ford with k + 1 rounds (see '31_BellmanFordAlgorithm.cs'):
	//     At most k stops = at most k + 1 FLIGHTS. After round r, dist[c] = the cheapest cost to c using at most r flights.
	//     Each round relaxes every flight once, reading from a COPY of the previous round (so one round can not chain
	//     two flights together).
	// T.C: O(k * E)
	// S.C: O(n)
	public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
	{
		int[] dist = new int[n];
		Array.Fill(dist, int.MaxValue);
		dist[src] = 0;

		for (int round = 0; round <= k; round++)
		{
			int[] prev = (int[])dist.Clone();
			foreach (int[] f in flights)
			{
				int u = f[0], v = f[1], price = f[2];
				if (prev[u] != int.MaxValue && prev[u] + price < dist[v]) dist[v] = prev[u] + price;
			}
		}
		return dist[dst] == int.MaxValue ? -1 : dist[dst];
	}
}


class Program
{
	static void Main()
	{
		int[][] flights =
		{
			new[] { 0, 1, 100 }, new[] { 1, 2, 100 }, new[] { 2, 0, 100 }, new[] { 1, 3, 600 }, new[] { 2, 3, 200 }
		};

		Console.WriteLine(new Solution().FindCheapestPrice_BFS(4, flights, 0, 3, 1));   // o/p: 700
		Console.WriteLine(new Solution().FindCheapestPrice(4, flights, 0, 3, 1));       // o/p: 700
	}
}

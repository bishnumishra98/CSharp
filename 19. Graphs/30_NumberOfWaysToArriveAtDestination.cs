// Leetcode: 1976. Number of Ways to Arrive at Destination   --->   n intersections, two-way roads [u, v, time]. Count the
// different routes from 0 to n - 1 that take the SHORTEST possible time. Return the count modulo 10^9 + 7.

// Example 1:
// Input: n = 7, roads = [[0,6,7],[0,1,2],[1,2,3],[1,3,3],[6,3,3],[3,5,1],[6,5,1],[2,5,1],[0,4,5],[4,6,2]]
// Output: 4
// Explanation: the shortest time is 7, and 4 routes take 7 minutes: 0-6, 0-4-6, 0-1-2-5-6, 0-1-3-5-6.

// Example 2:
// Input: n = 2, roads = [[1,0,10]]
// Output: 1

// First understand '24_DijkstraAlgorithm.cs'.


using System;
using System.Collections.Generic;

public class Solution
{
	private const int MOD = 1_000_000_007;

	// Algorithm: Dijkstra, plus ways[node] = the number of shortest routes to node. When relaxing u -> v:
	//   time[u] + t <  time[v]:  a NEW shortest time for v. All its old routes are worse now:  ways[v] = ways[u].
	//   time[u] + t == time[v]:  ANOTHER route with the same shortest time:  ways[v] += ways[u].
	// Why is ways[u] complete when u is used ? Dijkstra takes nodes in increasing time order, and all road times are
	// positive. So every node that can come right before u on a shortest route has a smaller time and was handled
	// earlier, adding its ways into ways[u] already.
	// Times can be as large as about 200 * 10^9, so 'long' is used.
	// T.C: O(E log V)
	// S.C: O(V + E)
	public int CountPaths(int n, int[][] roads)
	{
		List<(int to, int t)>[] adj = new List<(int to, int t)>[n];
		for (int i = 0; i < n; i++) adj[i] = new List<(int to, int t)>();
		foreach (int[] r in roads)
		{
			adj[r[0]].Add((r[1], r[2]));
			adj[r[1]].Add((r[0], r[2]));
		}

		long[] time = new long[n];
		long[] ways = new long[n];
		Array.Fill(time, long.MaxValue);
		time[0] = 0;
		ways[0] = 1;

		PriorityQueue<int, long> pq = new PriorityQueue<int, long>();
		pq.Enqueue(0, 0);

		while (pq.TryDequeue(out int u, out long d))
		{
			if (d > time[u]) continue;   // outdated heap entry

			foreach (var (v, t) in adj[u])
			{
				if (d + t < time[v])
				{
					time[v] = d + t;
					ways[v] = ways[u];
					pq.Enqueue(v, time[v]);
				}
				else if (d + t == time[v])
				{
					ways[v] = (ways[v] + ways[u]) % MOD;
				}
			}
		}
		return (int)ways[n - 1];
	}
}


class Program
{
	static void Main()
	{
		int[][] roads =
		{
			new[] { 0, 6, 7 }, new[] { 0, 1, 2 }, new[] { 1, 2, 3 }, new[] { 1, 3, 3 }, new[] { 6, 3, 3 },
			new[] { 3, 5, 1 }, new[] { 6, 5, 1 }, new[] { 2, 5, 1 }, new[] { 0, 4, 5 }, new[] { 4, 6, 2 }
		};

		Console.WriteLine(new Solution().CountPaths(7, roads));   // o/p: 4
	}
}

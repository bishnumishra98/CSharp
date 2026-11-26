// Floyd-Warshall Algorithm   --->   The shortest distance between EVERY PAIR of nodes (all sources at once), in a
// directed weighted graph. Negative edge weights are allowed, and it can detect negative cycles.
// ● For an undirected graph, put each edge in both directions.
// ● It works on an adjacency MATRIX, in place. It is a DP.

// GFG: Floyd Warshall   --->   mat[i][j] = the weight of the edge i -> j, or -1 if there is no edge (mat[i][i] = 0).
// Change mat in place so that mat[i][j] = the shortest distance from i to j (-1 if j can not be reached).

// Example 1:
// Input: mat = [[0,25],[-1,0]]
// Output: [[0,25],[-1,0]]

// Example 2:
// Input: mat = [[0,1,43],[1,0,6],[-1,-1,0]]
// Output: [[0,1,7],[1,0,6],[-1,-1,0]]   (0 -> 1 -> 2 = 1 + 6 = 7 < 43)

// Problem link: https://www.geeksforgeeks.org/problems/implementing-floyd-warshall2042/1


using System;
using System.Linq;

public class Solution
{
	private const int INF = 1_000_000_000;

	// Algorithm: Allow the nodes 0, 1, ..., V - 1 as "stop-overs" one at a time. After allowing node k, mat[i][j] = the
	// shortest i -> j path whose middle nodes are all among 0..k. Allowing k can only help as:
	//   i -> ... -> k -> ... -> j,  so  mat[i][j] = min(mat[i][j], mat[i][k] + mat[k][j])
	// k MUST be the outer loop: when k is allowed, mat[i][k] and mat[k][j] already use every stop-over before k.
	// Negative cycle: if some mat[i][i] ends up < 0, there is a way from i back to i that costs less than 0.
	// T.C: O(V^3)
	// S.C: O(1) extra;   works in the given matrix
	// Returns true if a negative cycle exists.
	public bool ShortestDistance(int[][] mat)
	{
		int V = mat.Length;
		for (int i = 0; i < V; i++)
		{
			for (int j = 0; j < V; j++)
			{
				if (mat[i][j] == -1) mat[i][j] = INF;   // -1 means "no edge" here; turn it into infinity
			}
			mat[i][i] = 0;
		}

		for (int k = 0; k < V; k++)
		{
			for (int i = 0; i < V; i++)
			{
				for (int j = 0; j < V; j++)
				{
					if (mat[i][k] == INF || mat[k][j] == INF) continue;   // no way through k
					mat[i][j] = Math.Min(mat[i][j], mat[i][k] + mat[k][j]);
				}
			}
		}

		bool negativeCycle = false;
		for (int i = 0; i < V; i++)
		{
			if (mat[i][i] < 0) negativeCycle = true;
			for (int j = 0; j < V; j++)
			{
				if (mat[i][j] == INF) mat[i][j] = -1;   // back to the problem's "unreachable"
			}
		}
		return negativeCycle;
	}
}

// Floyd-Warshall vs running Dijkstra from every node: Dijkstra V times costs O(V * E log V), faster on SPARSE graphs,
// but it needs non-negative weights. Floyd-Warshall is O(V^3) always, handles negative weights, and is 5 lines.
// Note: The C++ code added mat[i][k] + mat[k][j] even when one of them was "infinity" (1e9). With negative edges that
//       can produce a wrong "almost infinite" distance (e.g., 1e9 - 5). Skipping those pairs avoids it.


class Program
{
	static void Main()
	{
		int[][] mat = { new[] { 0, 1, 43 }, new[] { 1, 0, 6 }, new[] { -1, -1, 0 } };

		bool negativeCycle = new Solution().ShortestDistance(mat);
		Console.WriteLine(string.Join(" | ", mat.Select(row => string.Join(" ", row))));   // o/p: 0 1 7 | 1 0 6 | -1 -1 0
		Console.WriteLine(negativeCycle);                                                  // o/p: False
	}
}

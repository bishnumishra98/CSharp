// Bridge   --->   In an undirected graph, a BRIDGE is an edge whose removal splits the graph into more pieces (some nodes can
// no longer reach each other). An edge that lies on a CYCLE is never a bridge: the rest of the cycle still connects
// its two ends.

// Leetcode: 1192. Critical Connections in a Network   --->   n servers, connections[i] = [a, b] (undirected). Return all
// critical connections (= bridges), in any order.

// Example 1:
// Input: n = 4, connections = [[0,1],[1,2],[2,0],[1,3]]
//    0 --- 1 --- 3
//     \   /
//       2
// Output: [[1,3]]   (0-1, 1-2, 2-0 form a cycle; only 1-3 is a bridge)

// Example 2:
// Input: n = 2, connections = [[0,1]]
// Output: [[0,1]]


using System;
using System.Collections.Generic;

public class Solution
{
	// Tarjan's Bridge-Finding Algorithm (one DFS):
	// ● disc[u] = the DFS "time" when u is first visited (0, 1, 2, ...).
	// ● low[u]  = the smallest disc[] that u (or anything below u in the DFS tree) can reach using ONE edge that is not the
	//             edge to u's parent. It says: "how high up the tree can this part climb without its parent edge ?"
	// After the DFS of a child v returns:
	//   low[u] = min(low[u], low[v])        (u can climb as high as its child can)
	//   if low[v] > disc[u]: v's whole part can NOT reach u or anything above u without the edge u - v.
	//                        So u - v is a BRIDGE.
	// For an already visited neighbour v (not the parent): low[u] = min(low[u], disc[v])   (a back edge climbs up to v).
	// e.g., Example 1 from 0: disc 0:0, 1:1, 2:2 (via 1), 2 sees 0 (back edge) -> low[2] = 0, so low[1] = 0.
	//       1 -> 3: disc[3] = 3, low[3] = 3 > disc[1] = 1 -> 1-3 is a bridge. low[2] = 0 is not > disc[1]: 1-2 is not.
	// T.C: O(V + E)
	// S.C: O(V + E)
	// Note: Skipping the parent BY ITS NUMBER (as here and in the C++) breaks if the same pair is connected twice (two
	//       parallel edges are never bridges). Skipping by EDGE id fixes that; Leetcode has no parallel edges.
	//       (The C++ used low[v] instead of disc[v] for visited neighbours. For bridges both give the right answer;
	//        disc[v] is the textbook form, and the one that ALSO works for articulation points, see the next file.)
	public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
	{
		List<int>[] adj = new List<int>[n];
		for (int i = 0; i < n; i++) adj[i] = new List<int>();
		foreach (var c in connections)
		{
			adj[c[0]].Add(c[1]);
			adj[c[1]].Add(c[0]);
		}

		int[] disc = new int[n], low = new int[n];
		Array.Fill(disc, -1);
		int timer = 0;
		IList<IList<int>> bridges = new List<IList<int>>();

		for (int i = 0; i < n; i++)
		{
			if (disc[i] == -1) Dfs(i, -1, adj, disc, low, ref timer, bridges);
		}
		return bridges;
	}

	private void Dfs(int u, int parent, List<int>[] adj, int[] disc, int[] low, ref int timer, IList<IList<int>> bridges)
	{
		disc[u] = low[u] = timer++;

		foreach (int v in adj[u])
		{
			if (v == parent) continue;

			if (disc[v] == -1)
			{
				Dfs(v, u, adj, disc, low, ref timer, bridges);
				low[u] = Math.Min(low[u], low[v]);
				if (low[v] > disc[u]) bridges.Add(new List<int> { u, v });
			}
			else
			{
				low[u] = Math.Min(low[u], disc[v]);
			}
		}
	}
}


class Program
{
	static void Main()
	{
		IList<IList<int>> connections = new List<IList<int>>
		{
			new List<int> { 0, 1 }, new List<int> { 1, 2 }, new List<int> { 2, 0 }, new List<int> { 1, 3 }
		};

		foreach (var b in new Solution().CriticalConnections(4, connections)) Console.WriteLine(string.Join(" ", b));   // o/p: 1 3
	}
}

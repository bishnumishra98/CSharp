// Leetcode: 802. Find Eventual Safe States (GFG: Eventual Safe States)   --->   A directed graph with V nodes. A TERMINAL
// node has no outgoing edges. A node is SAFE if EVERY path starting from it ends at a terminal node. Return all safe
// nodes in increasing order.

// Example 1:
// Input: adj = [[1,2], [2,3], [5], [0], [5], [], []]
//    0 -> 1, 2      1 -> 2, 3      2 -> 5      3 -> 0      4 -> 5      5, 6: terminal
// Output: 2 4 5 6
// Explanation: 0, 1 and 3 are on the cycle 0 -> 1 -> 3 -> 0: a path from them can go round forever.

// Example 2:
// Input: adj = [[1], [2], [0,3], []]
// Output: 3

// The key idea: a node is UNSAFE exactly when some path from it can reach a CYCLE (a path that never ends). Every node
// on a cycle, and every node that can walk into one, is unsafe. All other nodes are safe.


using System;
using System.Collections.Generic;

public class Solution
{
	// I. DFS (the cycle detection of '16_DetectCycleInDirectedGraph.cs', with one change):
	//    When a cycle is found, we return at once WITHOUT clearing onPath. So every node on the way to the cycle stays
	//    marked onPath forever, which means "leads to a cycle". A node that finishes normally (no cycle below it) is safe.
	//    Later DFS calls that touch one of those marked nodes also see "onPath" and become unsafe too: correct, because
	//    they can walk into the cycle.
	// T.C: O(V + E)
	// S.C: O(V)
	public List<int> EventualSafeNodes_DFS(int V, List<List<int>> adj)
	{
		bool[] visited = new bool[V], onPath = new bool[V], safe = new bool[V];
		for (int i = 0; i < V; i++)
		{
			if (!visited[i]) Dfs(i, adj, visited, onPath, safe);
		}

		List<int> result = new List<int>();
		for (int i = 0; i < V; i++)
		{
			if (safe[i]) result.Add(i);
		}
		return result;
	}

	private bool Dfs(int node, List<List<int>> adj, bool[] visited, bool[] onPath, bool[] safe)
	{
		visited[node] = onPath[node] = true;

		foreach (int next in adj[node])
		{
			if (!visited[next])
			{
				if (Dfs(next, adj, visited, onPath, safe)) return true;   // onPath[node] stays true: unsafe
			}
			else if (onPath[next]) return true;
		}

		safe[node] = true;
		onPath[node] = false;
		return false;
	}

// ------------------------------------------------------------------------------

	// II. BFS (Kahn's algorithm on the REVERSED graph):
	//     Terminal nodes are safe. A node is safe once ALL its outgoing edges lead to safe nodes. Reverse every edge, so
	//     that a node's "out-degree" becomes an in-degree. Now terminal nodes have in-degree 0: start Kahn's algorithm
	//     from them. Every node that comes out of the queue is safe. Nodes that can reach a cycle never reach 0.
	// T.C: O(V + E)
	// S.C: O(V + E);   the reversed graph
	// Note: The C++ code collected the safe nodes in queue order and then SORTED them (O(V log V)). Marking them in a
	//       bool array and reading it from 0 to V - 1 gives them in increasing order in O(V).
	public List<int> EventualSafeNodes(int V, List<List<int>> adj)
	{
		List<int>[] reversed = new List<int>[V];
		for (int i = 0; i < V; i++) reversed[i] = new List<int>();

		int[] inDegree = new int[V];   // = the out-degree in the original graph
		for (int u = 0; u < V; u++)
		{
			foreach (int v in adj[u])
			{
				reversed[v].Add(u);   // u -> v becomes v -> u
				inDegree[u]++;
			}
		}

		Queue<int> q = new Queue<int>();
		for (int i = 0; i < V; i++)
		{
			if (inDegree[i] == 0) q.Enqueue(i);   // terminal nodes
		}

		bool[] safe = new bool[V];
		while (q.Count > 0)
		{
			int node = q.Dequeue();
			safe[node] = true;
			foreach (int prev in reversed[node])
			{
				if (--inDegree[prev] == 0) q.Enqueue(prev);
			}
		}

		List<int> result = new List<int>();
		for (int i = 0; i < V; i++)
		{
			if (safe[i]) result.Add(i);
		}
		return result;
	}
}


class Program
{
	static void Main()
	{
		List<List<int>> adj = new List<List<int>>
		{
			new List<int> { 1, 2 }, new List<int> { 2, 3 }, new List<int> { 5 }, new List<int> { 0 },
			new List<int> { 5 }, new List<int>(), new List<int>()
		};

		Console.WriteLine(string.Join(" ", new Solution().EventualSafeNodes_DFS(7, adj)));   // o/p: 2 4 5 6
		Console.WriteLine(string.Join(" ", new Solution().EventualSafeNodes(7, adj)));       // o/p: 2 4 5 6
	}
}

// GFG: Detect Cycle in a Directed Graph   --->   Given a directed graph with V nodes (adjacency list), return true if it
// contains a cycle.

// Example 1:
//    0 --> 1 --> 2 --> 3
//                |     ^
//                v     |
//                5 --> 4
// Input: adj = [[1], [2], [3,5], [], [3], [4]]
// Output: false   (2 -> 3 and 2 -> 5 -> 4 -> 3 both end at 3, but there is no loop)

// Example 2:
//    0 --> 1 --> 2 --> 3
//                ^     |
//                |     v
//                5 <-- 4
// Input: adj = [[1], [2], [3], [4], [5], [2]]
// Output: true    (2 -> 3 -> 4 -> 5 -> 2)

// Why the undirected trick ('09_DetectCycleInUndirectedGraph.cs') fails here: in Example 1, node 3 is reached twice
// (from 2 and from 4), but that is NOT a cycle, because the edges only go one way. Reaching a visited node is a cycle
// only if that node is still on the CURRENT path (we went from it, down, and came back to it).


// Dry run image (in this folder): 16_DetectCycleInDirectedGraph.jpg

using System;
using System.Collections.Generic;

public class Solution
{
	// I. DFS with a "path visited" array:
	//    visited[i]   = i was ever visited (so it is never explored twice).
	//    onPath[i]    = i is on the current DFS path (set when we enter it, cleared when we leave it).
	//    Meeting a neighbour that is onPath means we came back to a node of our own path: a cycle.
	//    Meeting a neighbour that is visited but NOT onPath is fine: it was fully explored earlier, with no cycle.
	// T.C: O(V + E)
	// S.C: O(V);   two arrays + recursion stack
	public bool IsCyclic_DFS(int V, List<List<int>> adj)
	{
		bool[] visited = new bool[V], onPath = new bool[V];
		for (int i = 0; i < V; i++)
		{
			if (!visited[i] && Dfs(i, adj, visited, onPath)) return true;
		}
		return false;
	}

	private bool Dfs(int node, List<List<int>> adj, bool[] visited, bool[] onPath)
	{
		visited[node] = onPath[node] = true;

		foreach (int next in adj[node])
		{
			if (!visited[next])
			{
				if (Dfs(next, adj, visited, onPath)) return true;
			}
			else if (onPath[next]) return true;   // back to a node of the current path
		}

		onPath[node] = false;   // leaving this node: it is no longer on the path
		return false;
	}

// ------------------------------------------------------------------------------

	// II. BFS (Kahn's algorithm, see '15_TopologicalSort.cs'): run the topological sort and COUNT the nodes it outputs.
	//     Nodes on a cycle never get in-degree 0 (each one waits for the one before it on the cycle), so they are never
	//     output. Fewer than V nodes output -> there is a cycle.
	// T.C: O(V + E)
	// S.C: O(V)
	public bool IsCyclic(int V, List<List<int>> adj)
	{
		int[] inDegree = new int[V];
		for (int u = 0; u < V; u++)
		{
			foreach (int v in adj[u]) inDegree[v]++;
		}

		Queue<int> q = new Queue<int>();
		for (int i = 0; i < V; i++)
		{
			if (inDegree[i] == 0) q.Enqueue(i);
		}

		int count = 0;
		while (q.Count > 0)
		{
			int node = q.Dequeue();
			count++;
			foreach (int next in adj[node])
			{
				if (--inDegree[next] == 0) q.Enqueue(next);
			}
		}
		return count < V;
	}
}


class Program
{
	static void Main()
	{
		List<List<int>> adj = new List<List<int>>
		{
			new List<int> { 1 }, new List<int> { 2 }, new List<int> { 3 },
			new List<int> { 4 }, new List<int> { 5 }, new List<int> { 2 }
		};

		Console.WriteLine(new Solution().IsCyclic_DFS(6, adj));   // o/p: True
		Console.WriteLine(new Solution().IsCyclic(6, adj));       // o/p: True
	}
}

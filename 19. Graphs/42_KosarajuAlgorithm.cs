// Strongly Connected Components (SCC)   --->   In a DIRECTED graph, an SCC is a biggest-possible group of nodes where
// every node can reach every other node of the group (following the edge directions).
//
// Example: adj = [[2,3], [0], [1], [4], []]   (0 -> 2, 0 -> 3, 1 -> 0, 2 -> 1, 3 -> 4)
//    0 -> 2 -> 1 -> 0   (a loop: 0, 1, 2 reach each other)      0 -> 3 -> 4   (one way only)
// SCCs: {0, 1, 2}, {3}, {4}   -> 3 SCCs
//
// GFG: Strongly Connected Components (Kosaraju's Algo)   --->   Return the number of SCCs (also listed below).

// Example 2:
// Input: adj = [[1], [2], [0]]
// Output: 1

// Example 3:
// Input: adj = [[1], []]
// Output: 2

// Problem link: https://www.geeksforgeeks.org/problems/strongly-connected-components-kosarajus-algo/1


using System;
using System.Collections.Generic;

public class Solution
{
	// Kosaraju's Algorithm (2 DFS passes):
	// 1. DFS on the graph, and push each node on a stack when it FINISHES (like the topological sort in
	//    '15_TopologicalSort.cs'). The node that finishes LAST (top of the stack) is in a "source" SCC: no edge enters it
	//    from another SCC.
	// 2. REVERSE every edge. The SCCs stay the same (a loop reversed is still a loop), but the edges BETWEEN SCCs flip.
	// 3. Pop nodes from the stack; each one not visited yet starts a DFS on the REVERSED graph. That DFS collects exactly
	//    one SCC: in the reversed graph, the edges that used to leave this SCC now point INTO it, so it can not leak out
	//    into another SCC that is not finished yet.
	// e.g., stack after step 1 (top first): 0, 3, 4, 2, 1 (one possible order).
	//       Reversed: 2 -> 0, 3 -> 0, 0 -> 1, 1 -> 2, 4 -> 3. Pop 0: DFS reaches 1, 2 -> SCC {0, 1, 2}.
	//       Pop 3: DFS reaches nothing new (0 is done) -> {3}. Pop 4 -> {4}.
	// T.C: O(V + E)
	// S.C: O(V + E);   the reversed graph
	public List<List<int>> FindSCCs(List<List<int>> adj)
	{
		int V = adj.Count;
		bool[] visited = new bool[V];
		Stack<int> finished = new Stack<int>();
		for (int i = 0; i < V; i++)
		{
			if (!visited[i]) FillOrder(i, adj, visited, finished);
		}

		List<int>[] reversed = new List<int>[V];
		for (int i = 0; i < V; i++) reversed[i] = new List<int>();
		for (int u = 0; u < V; u++)
		{
			foreach (int v in adj[u]) reversed[v].Add(u);
		}

		Array.Fill(visited, false);
		List<List<int>> sccs = new List<List<int>>();
		while (finished.Count > 0)
		{
			int node = finished.Pop();
			if (visited[node]) continue;

			List<int> component = new List<int>();
			Collect(node, reversed, visited, component);
			sccs.Add(component);
		}
		return sccs;
	}

	private void FillOrder(int node, List<List<int>> adj, bool[] visited, Stack<int> finished)
	{
		visited[node] = true;
		foreach (int next in adj[node])
		{
			if (!visited[next]) FillOrder(next, adj, visited, finished);
		}
		finished.Push(node);
	}

	private void Collect(int node, List<int>[] reversed, bool[] visited, List<int> component)
	{
		visited[node] = true;
		component.Add(node);
		foreach (int next in reversed[node])
		{
			if (!visited[next]) Collect(next, reversed, visited, component);
		}
	}

// ------------------------------------------------------------------------------

	// The GFG problem only asks for the COUNT.
	// T.C: O(V + E)
	// S.C: O(V + E)
	public int Kosaraju(List<List<int>> adj)
	{
		return FindSCCs(adj).Count;
	}
}

// (Tarjan's algorithm finds SCCs in ONE DFS pass using the disc / low idea of '43_BridgesInGraph.cs'. Kosaraju is easier
//  to understand and remember.)


class Program
{
	static void Main()
	{
		List<List<int>> adj = new List<List<int>>
		{
			new List<int> { 2, 3 }, new List<int> { 0 }, new List<int> { 1 }, new List<int> { 4 }, new List<int>()
		};

		foreach (var scc in new Solution().FindSCCs(adj)) Console.Write("{" + string.Join(",", scc) + "} ");
		Console.WriteLine();                                // o/p: {0,1,2} {3} {4}
		Console.WriteLine(new Solution().Kosaraju(adj));    // o/p: 3
	}
}

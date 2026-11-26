// GFG: Detect Cycle in an Undirected Graph   --->   Given an undirected graph with V nodes (0..V-1) as an adjacency list,
// return true if it has a CYCLE anywhere (the graph may have several separate components).

// Example 1:
// Input: V = 5, adj = [[1], [0,2,4], [1,3], [2,4], [1,3]]
//      1 --- 2
//     / \     \
//    0   4 --- 3
// Output: true   (1 -> 2 -> 3 -> 4 -> 1)

// Example 2:
// Input: V = 4, adj = [[], [2], [1,3], [2]]
//    0    1 --- 2 --- 3
// Output: false

// The key idea: walk the graph and remember, for every node, the node we came FROM (its parent). In an undirected graph
// every edge shows up in both directions, so from a node we always "see" its parent again: that is not a cycle.
// But if we see an ALREADY VISITED node that is NOT our parent, it was reached by another path: two paths meet, so
// there is a cycle.


// Dry run image (in this folder): 09_DetectCycleInUndirectedGraph_DFS.jpg

using System;
using System.Collections.Generic;

public class Solution
{
	// BFS: the queue holds (node, parent). For each neighbour:
	//   not visited:          mark it, enqueue (neighbour, node).
	//   visited, not parent:  someone else already reached it -> cycle.
	// e.g., cycle 0 - 1 - 2 - 3 - 4 - 0: BFS from 0 visits 1 and 4. From 1: 2. From 4: 3. From 2: its neighbour 3 is
	//       visited, and 2 came from 1, not from 3 -> cycle.
	// Run it from every node that is still not visited, to cover every component.
	// T.C: O(V + 2E)
	// S.C: O(V)
	public bool IsCycle_BFS(int V, List<List<int>> adj)
	{
		bool[] visited = new bool[V];
		for (int start = 0; start < V; start++)
		{
			if (visited[start]) continue;

			Queue<(int node, int parent)> q = new Queue<(int node, int parent)>();
			q.Enqueue((start, -1));
			visited[start] = true;

			while (q.Count > 0)
			{
				var (node, parent) = q.Dequeue();
				foreach (int next in adj[node])
				{
					if (!visited[next])
					{
						visited[next] = true;
						q.Enqueue((next, node));
					}
					else if (next != parent) return true;
				}
			}
		}
		return false;
	}

// ------------------------------------------------------------------------------

	// DFS: Dfs(node, parent). For each neighbour: not visited -> go deeper (and pass a found cycle up); visited and not
	// the parent -> cycle. (See the image.)
	// T.C: O(V + 2E)
	// S.C: O(V);   visited + recursion stack
	public bool IsCycle(int V, List<List<int>> adj)
	{
		bool[] visited = new bool[V];
		for (int start = 0; start < V; start++)
		{
			if (!visited[start] && HasCycle(start, -1, adj, visited)) return true;
		}
		return false;
	}

	private bool HasCycle(int node, int parent, List<List<int>> adj, bool[] visited)
	{
		visited[node] = true;
		foreach (int next in adj[node])
		{
			if (!visited[next])
			{
				if (HasCycle(next, node, adj, visited)) return true;
			}
			else if (next != parent) return true;
		}
		return false;
	}
}

// Note: The parent check assumes no duplicate edges (the same pair u - v given twice). With a duplicate, the second copy
//       would look like "visited, and it IS the parent", and be ignored, although two parallel edges do form a cycle.
//       (A Disjoint Set can also detect cycles in an undirected graph, see '35_DisjointSet.cs'.)


class Program
{
	static void Main()
	{
		List<List<int>> adj = new List<List<int>>
		{
			new List<int> { 1 }, new List<int> { 0, 2, 4 }, new List<int> { 1, 3 }, new List<int> { 2, 4 }, new List<int> { 1, 3 }
		};

		Console.WriteLine(new Solution().IsCycle_BFS(5, adj));   // o/p: True
		Console.WriteLine(new Solution().IsCycle(5, adj));       // o/p: True
	}
}

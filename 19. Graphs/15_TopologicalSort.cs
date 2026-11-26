// GFG: Topological Sort   --->   Given a DAG (Directed Acyclic Graph) with V nodes as an adjacency list, return any
// TOPOLOGICAL ORDER: a list of all nodes such that for every edge u -> v, u comes BEFORE v.
// (Think of tasks with prerequisites: an edge u -> v means "do u before v".)

// Example 1:
//    1 --> 0 <-- 3
//          ^
//          |
//          2
// Input: adj = [[], [0], [0], [0]]
// Output: any of 3 2 1 0, 1 2 3 0, 2 1 3 0 ... (0 must come last)

// Example 2:
//    5 --> 0 <-- 4
//    |           |
//    v           v
//    2 --> 3 <-- 1
// Input: adj = [[], [3], [3], [], [0,1], [0,2]]
// Output: e.g., 5 4 2 1 3 0   or   4 5 0 2 1 3

// ● It only exists for a DAG. With a cycle (a -> b -> a), a must come before b AND b before a: impossible.
// ● There can be many valid orders.


using System;
using System.Collections.Generic;

public class Solution
{
	// I. DFS: A node is FINISHED (all its neighbours explored) only after everything it points to is finished. So when a
	//    node finishes, push it on a stack. Every node is pushed AFTER all the nodes that must come after it, so popping
	//    the stack gives a valid order.
	// e.g., Example 2, from 0: 0 finishes -> stack [0]. From 1: visits 3, 3 finishes, 1 finishes -> [0, 3, 1].
	//       From 2: 3 is done, 2 finishes -> [0, 3, 1, 2]. From 4: [.., 4]. From 5: [.., 5]. Popping: 5 4 2 1 3 0.
	// T.C: O(V + E)
	// S.C: O(V);   visited + stack + recursion stack
	public List<int> TopoSort_DFS(int V, List<List<int>> adj)
	{
		bool[] visited = new bool[V];
		Stack<int> st = new Stack<int>();

		for (int i = 0; i < V; i++)
		{
			if (!visited[i]) Dfs(i, adj, visited, st);
		}
		return new List<int>(st);   // a Stack enumerates from the top: the reverse of the push order
	}

	private void Dfs(int node, List<List<int>> adj, bool[] visited, Stack<int> st)
	{
		visited[node] = true;
		foreach (int next in adj[node])
		{
			if (!visited[next]) Dfs(next, adj, visited, st);
		}
		st.Push(node);   // pushed only when everything after it is done
	}

// ------------------------------------------------------------------------------

	// II. BFS (Kahn's Algorithm): use IN-DEGREES (how many edges point INTO a node).
	//     A node with in-degree 0 has nothing that must come before it: it can go first.
	// 1. Count every node's in-degree. Put all nodes with in-degree 0 in a queue.
	// 2. Take a node out, add it to the order, and "remove" its outgoing edges: lower each neighbour's in-degree by 1.
	//    A neighbour that reaches 0 has nothing left before it: put it in the queue.
	// e.g., Example 2: in-degrees 0:2, 1:1, 2:1, 3:2, 4:0, 5:0. Queue [4, 5].
	//       4 -> order [4], 0 drops to 1, 1 drops to 0 (enqueue). 5 -> [4, 5], 0 drops to 0, 2 drops to 0. ...
	//       Final: 4 5 1 0 2 3.
	// T.C: O(V + E)
	// S.C: O(V)
	public List<int> TopoSort(int V, List<List<int>> adj)
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

		List<int> order = new List<int>();
		while (q.Count > 0)
		{
			int node = q.Dequeue();
			order.Add(node);
			foreach (int next in adj[node])
			{
				if (--inDegree[next] == 0) q.Enqueue(next);   // --x: lower first, then compare
			}
		}
		return order;
	}
}

// Kahn's algorithm has a bonus: if the graph HAS a cycle, the nodes on it never reach in-degree 0, so the order comes
// out SHORTER than V. That is the easiest way to detect a cycle in a directed graph (see the next file).


class Program
{
	static void Main()
	{
		List<List<int>> adj = new List<List<int>>
		{
			new List<int>(), new List<int> { 3 }, new List<int> { 3 },
			new List<int>(), new List<int> { 0, 1 }, new List<int> { 0, 2 }
		};

		Console.WriteLine(string.Join(" ", new Solution().TopoSort_DFS(6, adj)));   // o/p: 5 4 2 1 3 0
		Console.WriteLine(string.Join(" ", new Solution().TopoSort(6, adj)));       // o/p: 4 5 1 0 2 3
	}
}

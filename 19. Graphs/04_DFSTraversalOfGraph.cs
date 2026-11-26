// DFS (Depth First Search) of a Graph   --->   Go as DEEP as possible along one path first, then come back
// (backtrack) and try the next path. Like preorder of a tree, plus a visited array because graphs can have cycles.

// Example 1:
// Input: n = 5, adj = [[2,3,1], [0], [0,4], [0], [2]]   (undirected)
//        0
//      / | \
//     2  3  1
//     |
//     4
// Output: 0 2 4 3 1   (from 0 go to 2, then deep to 4, back up, then 3, then 1)

// Example 2:
// Input: n = 4, adj = [[1,3], [2,0], [1], [0]]
// Output: 0 1 2 3


// Dry run image (in this folder): 04_DFSTraversalOfGraph.jpg

using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm (recursive, start at node 0):
	// Dfs(node): mark node visited, add it to the answer, then call Dfs on each neighbour that is not visited yet.
	// The recursion stack remembers the way back.
	// T.C: O(n + m);   every node is visited once, every adjacency list walked once
	// S.C: O(n);   visited + recursion stack (a long chain can be n deep)
	public List<int> DfsOfGraph(int n, List<List<int>> adj)
	{
		bool[] visited = new bool[n];
		List<int> dfs = new List<int>();
		Dfs(0, adj, visited, dfs);
		return dfs;
	}

	private void Dfs(int node, List<List<int>> adj, bool[] visited, List<int> dfs)
	{
		visited[node] = true;
		dfs.Add(node);

		foreach (int next in adj[node])
		{
			if (!visited[next]) Dfs(next, adj, visited, dfs);
		}
	}

// ------------------------------------------------------------------------------

	// Iterative DFS with a Stack (no recursion, so no stack overflow on a very deep graph).
	// Push the neighbours in REVERSE order, so that the first neighbour is popped first (same order as the recursion).
	// A node may be pushed more than once (by different neighbours); skip it if it was already visited when popped.
	// T.C: O(n + m)
	// S.C: O(n + m);   in the worst case the stack holds many copies
	public List<int> DfsOfGraph_Iterative(int n, List<List<int>> adj)
	{
		bool[] visited = new bool[n];
		List<int> dfs = new List<int>();
		Stack<int> st = new Stack<int>();
		st.Push(0);

		while (st.Count > 0)
		{
			int node = st.Pop();
			if (visited[node]) continue;

			visited[node] = true;
			dfs.Add(node);

			for (int i = adj[node].Count - 1; i >= 0; i--)
			{
				if (!visited[adj[node][i]]) st.Push(adj[node][i]);
			}
		}
		return dfs;
	}
}


class Program
{
	static void Main()
	{
		List<List<int>> adj = new List<List<int>>
		{
			new List<int> { 2, 3, 1 }, new List<int> { 0 }, new List<int> { 0, 4 }, new List<int> { 0 }, new List<int> { 2 }
		};

		Console.WriteLine(string.Join(" ", new Solution().DfsOfGraph(5, adj)));             // o/p: 0 2 4 3 1
		Console.WriteLine(string.Join(" ", new Solution().DfsOfGraph_Iterative(5, adj)));   // o/p: 0 2 4 3 1
	}
}

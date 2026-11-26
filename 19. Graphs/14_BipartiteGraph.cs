// GFG / Leetcode 785: Is Graph Bipartite ?   --->   Given an undirected graph (adjacency list, nodes 0..V-1), return true
// if it is BIPARTITE: its nodes can be coloured with 2 colours so that every edge joins two DIFFERENT colours.
// (Equivalently: the nodes split into two groups, and every edge goes between the groups, never inside one.)

// Example 1:
//    0 --- 1
//    |     |
//    3 --- 2
//    |
//    4
// Input: adj = [[1,3], [0,2], [1,3], [0,4], [3]]
// Output: true   (colour 0, 2, 4 with A and 1, 3 with B)

// Example 2:
//    0 --- 1
//     \   /
//       2
// Input: adj = [[1,2], [0,2], [0,1]]
// Output: false  (a triangle: 3 nodes, 2 colours, two neighbours must share a colour)

// ● Good to know: A graph is bipartite exactly when it has NO cycle of ODD length. A graph with no cycles, or with only
//   even cycles, is bipartite.


using System;
using System.Collections.Generic;

public class Solution
{
	// BFS colouring: colour[i] = -1 (not coloured yet), 0 or 1.
	// Start a node with colour 0. For each neighbour:
	//   not coloured:        give it the OPPOSITE colour (1 - colour[node]) and enqueue it.
	//   already coloured:    if it has the SAME colour as this node, two neighbours share a colour: not bipartite.
	// Start from every node that is still uncoloured, to cover every component.
	// T.C: O(V + E)   (the C++ comment said O(n): every edge is also looked at)
	// S.C: O(V)
	public bool IsBipartite_BFS(int V, List<List<int>> adj)
	{
		int[] colour = new int[V];
		Array.Fill(colour, -1);

		for (int start = 0; start < V; start++)
		{
			if (colour[start] != -1) continue;

			Queue<int> q = new Queue<int>();
			q.Enqueue(start);
			colour[start] = 0;

			while (q.Count > 0)
			{
				int node = q.Dequeue();
				foreach (int next in adj[node])
				{
					if (colour[next] == -1)
					{
						colour[next] = 1 - colour[node];
						q.Enqueue(next);
					}
					else if (colour[next] == colour[node]) return false;
				}
			}
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// DFS colouring: the same rules, with recursion. Dfs(node, c) paints node with c and its uncoloured neighbours with
	// 1 - c.
	// T.C: O(V + E)
	// S.C: O(V);   colour + recursion stack
	public bool IsBipartite(int V, List<List<int>> adj)
	{
		int[] colour = new int[V];
		Array.Fill(colour, -1);

		for (int start = 0; start < V; start++)
		{
			if (colour[start] == -1 && !Dfs(start, 0, adj, colour)) return false;
		}
		return true;
	}

	private bool Dfs(int node, int c, List<List<int>> adj, int[] colour)
	{
		colour[node] = c;
		foreach (int next in adj[node])
		{
			if (colour[next] == -1)
			{
				if (!Dfs(next, 1 - c, adj, colour)) return false;
			}
			else if (colour[next] == c) return false;
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		List<List<int>> adj = new List<List<int>>
		{
			new List<int> { 1, 3 }, new List<int> { 0, 2 }, new List<int> { 1, 3 }, new List<int> { 0, 4 }, new List<int> { 3 }
		};

		Console.WriteLine(new Solution().IsBipartite_BFS(5, adj));   // o/p: True
		Console.WriteLine(new Solution().IsBipartite(5, adj));       // o/p: True
	}
}

// Graph   --->   A set of NODES (also called vertices) connected by EDGES. Unlike a tree, a graph has no root, can have
// cycles (loops), and some nodes may not be connected at all. Trees and linked lists are special kinds of graphs.
//
// Example: n = 5 nodes, m = 6 edges: (1,2) (1,3) (2,4) (3,4) (3,5) (4,5)
//
//        1 ----- 3
//        |       | \
//        |       |   5
//        |       | /
//        2 ----- 4
//
// ● Words you must know:
//   I.    Undirected graph: an edge goes BOTH ways (1 - 3 means 1 -> 3 and 3 -> 1). e.g., friendships.
//   II.   Directed graph:   an edge has a direction (1 -> 3 does not mean 3 -> 1). e.g., following on social media.
//   III.  Weighted graph:   every edge has a number (cost, distance, time). Unweighted = every edge counts as 1.
//   IV.   Degree: the number of edges at a node. Directed graphs have an in-degree (edges coming in) and an out-degree.
//         In an undirected graph, the sum of all degrees = 2 * edges (every edge touches 2 nodes).
//   V.    Path: a sequence of nodes where each one is connected to the next, with no node repeated.
//   VI.   Cycle: a path that starts and ends at the same node (1 -> 3 -> 4 -> 2 -> 1 above).
//   VII.  Connected components: groups of nodes that can reach each other. A graph can have several separate groups.
//   VIII. DAG: Directed Acyclic Graph, a directed graph with no cycle (e.g., course prerequisites).
//
// ● Storing a graph in memory, 2 ways (see the images):
//   I.  Adjacency MATRIX: an n x n table, mat[u][v] = 1 if there is an edge u - v (or the weight, for weighted graphs).
//       + Check "is there an edge u - v ?" in O(1).
//       - O(n^2) memory, even when there are few edges. Finding a node's neighbours costs O(n).
//   II. Adjacency LIST: for every node, a list of its neighbours. adj[u] = [all v with an edge u - v].
//       + O(n + m) memory. Walking a node's neighbours costs only its degree.
//       This is the one used in almost every problem.
//   For a weighted graph, the list stores pairs: adj[u] = [(v, weight), ...].
//
// ● Most problems number the nodes 0..n-1 (or 1..n). When nodes are 1..n, the arrays get size n + 1, and index 0 is not
//   used (that is why node 0 is empty in the images).


// Dry run images (in this folder): 01_GraphIntroduction_adjacencyMatrix.gif, 01_GraphIntroduction_adjacencyList.gif
// (animations: they add the edges one at a time)

using System;
using System.Collections.Generic;

public class Solution
{
	// Adjacency matrix of an undirected graph with nodes 1..n.
	// T.C: O(n^2 + m)
	// S.C: O(n^2)
	public int[,] BuildAdjacencyMatrix(int n, int[][] edges)
	{
		int[,] mat = new int[n + 1, n + 1];
		foreach (int[] e in edges)
		{
			mat[e[0], e[1]] = 1;
			mat[e[1], e[0]] = 1;   // remove this line for a DIRECTED graph
		}
		return mat;
	}

// ------------------------------------------------------------------------------

	// Adjacency list of an undirected graph with nodes 1..n.
	// T.C: O(n + m)
	// S.C: O(n + 2m);   every undirected edge is stored twice (once in each node's list)
	public List<List<int>> BuildAdjacencyList(int n, int[][] edges)
	{
		List<List<int>> adj = new List<List<int>>();
		for (int i = 0; i <= n; i++) adj.Add(new List<int>());

		foreach (int[] e in edges)
		{
			adj[e[0]].Add(e[1]);
			adj[e[1]].Add(e[0]);   // remove this line for a DIRECTED graph
		}
		return adj;
	}
}


class Program
{
	static void Main()
	{
		int n = 5;
		int[][] edges = { new[] { 1, 2 }, new[] { 1, 3 }, new[] { 2, 4 }, new[] { 3, 4 }, new[] { 3, 5 }, new[] { 4, 5 } };

		int[,] mat = new Solution().BuildAdjacencyMatrix(n, edges);
		for (int i = 1; i <= n; i++)
		{
			for (int j = 1; j <= n; j++) Console.Write(mat[i, j] + " ");
			Console.WriteLine();
		}
		// o/p: 0 1 1 0 0
		//      1 0 0 1 0
		//      1 0 0 1 1
		//      0 1 1 0 1
		//      0 0 1 1 0

		List<List<int>> adj = new Solution().BuildAdjacencyList(n, edges);
		for (int i = 1; i <= n; i++) Console.WriteLine($"{i} -> {string.Join(" ", adj[i])}");
		// o/p: 1 -> 2 3
		//      2 -> 1 4
		//      3 -> 1 4 5
		//      4 -> 2 3 5
		//      5 -> 3 4
	}
}

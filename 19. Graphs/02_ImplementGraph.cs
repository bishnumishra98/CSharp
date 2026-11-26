// Implement a Graph class   --->   A reusable graph whose nodes can be ANY type (int, char, string ...), not only 0..n-1.
// The adjacency list is a Dictionary {node -> list of its neighbours} (see '01. CSharp_Program/17_Dictionary.cs').
//
// Example (undirected):
//        a
//       / \
//      b - e
//     / \
//    d - c
// Edges: a-b, a-e, b-c, b-d, b-e, c-d


using System;
using System.Collections.Generic;

// <T> makes the class generic: Graph<int>, Graph<char>, Graph<string> all use the same code.
public class Graph<T>
{
	private Dictionary<T, List<T>> adj = new Dictionary<T, List<T>>();

	// directed = true: only u -> v.  directed = false: u -> v and v -> u.
	// T.C: O(1)
	public void AddEdge(T u, T v, bool directed)
	{
		if (!adj.ContainsKey(u)) adj[u] = new List<T>();
		if (!adj.ContainsKey(v)) adj[v] = new List<T>();   // so v is listed even if it has no outgoing edge

		adj[u].Add(v);
		if (!directed) adj[v].Add(u);
	}

	// T.C: O(n + m)
	public void Print()
	{
		foreach (var (node, neighbours) in adj) Console.WriteLine($"{node} -> {string.Join(" ", neighbours)}");
	}
}

// Note: The C++ code used unordered_map, which prints the nodes in an unpredictable order. A C# Dictionary, in practice,
//       keeps the order in which keys were added (as long as nothing is removed), but this is not promised by the
//       documentation. Use SortedDictionary if a sorted order is needed.


class Program
{
	static void Main()
	{
		Graph<char> g = new Graph<char>();
		g.AddEdge('a', 'b', false);
		g.AddEdge('a', 'e', false);
		g.AddEdge('b', 'c', false);
		g.AddEdge('b', 'd', false);
		g.AddEdge('b', 'e', false);
		g.AddEdge('c', 'd', false);

		g.Print();
		// o/p: a -> b e
		//      b -> a c d e
		//      e -> a b
		//      c -> b d
		//      d -> b c
	}
}

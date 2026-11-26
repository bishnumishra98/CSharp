// BFS (Breadth First Search) of a Graph   --->   Visit the nodes LEVEL by LEVEL: first the start node, then all its
// neighbours, then all THEIR new neighbours, and so on. Like the level order traversal of a tree
// ('14. Binary_Tree/02_BinaryTreeTraversals.cs'), but a graph can have cycles, so we must remember VISITED nodes.

// Example 1:
// Input: n = 5, adj = [[1,2,3], [], [4], [], []]   (directed: 0 -> 1, 2, 3 and 2 -> 4)
//        0
//      / | \
//     1  2  3
//        |
//        4
// Output: 0 1 2 3 4

// Example 2:
// Input: n = 3, adj = [[1,2], [], []]
// Output: 0 1 2


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm (start at node 0):
	// 1. Put 0 in a queue and mark it visited.
	// 2. While the queue is not empty: take out the front node, add it to the answer, and put every neighbour that is
	//    NOT visited yet into the queue, marking it visited RIGHT AWAY (when it enters the queue, not when it leaves).
	//    Why right away ? Otherwise the same node could be put into the queue several times by different neighbours.
	// T.C: O(n + m);   every node enters the queue once, and every adjacency list is walked once (2m entries in total
	//                  for an undirected graph)
	// S.C: O(n);   visited array + queue
	// Note: This only reaches the nodes connected to node 0. To cover a graph with several separate parts, run the same
	//       BFS from every node that is still not visited (see '05_NumberOfProvinces.cs').
	public List<int> BfsOfGraph(int n, List<List<int>> adj)
	{
		bool[] visited = new bool[n];
		List<int> bfs = new List<int>();
		Queue<int> q = new Queue<int>();

		q.Enqueue(0);
		visited[0] = true;

		while (q.Count > 0)
		{
			int node = q.Dequeue();
			bfs.Add(node);

			foreach (int next in adj[node])
			{
				if (visited[next]) continue;
				visited[next] = true;
				q.Enqueue(next);
			}
		}
		return bfs;
	}
}


class Program
{
	static void Main()
	{
		List<List<int>> adj = new List<List<int>>
		{
			new List<int> { 1, 2, 3 }, new List<int>(), new List<int> { 4 }, new List<int>(), new List<int>()
		};

		Console.WriteLine(string.Join(" ", new Solution().BfsOfGraph(5, adj)));   // o/p: 0 1 2 3 4
	}
}

// Leetcode: 547. Number of Provinces   --->   There are n cities. isConnected[i][j] = 1 if city i and city j are directly
// connected, else 0. A PROVINCE is a group of cities connected directly or through other cities. Return the number of
// provinces (= the number of CONNECTED COMPONENTS of the graph).

// Example 1:
// Input: isConnected = [[1,1,0],[1,1,0],[0,0,1]]
//    0 --- 1       2
// Output: 2

// Example 2:
// Input: isConnected = [[1,0,0],[0,1,0],[0,0,1]]
// Output: 3


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm (count the components): Go through the cities 0..n-1. Every time we find a city that is NOT visited yet,
	// it starts a NEW province: count it, and run a DFS (or BFS) from it, which marks its whole province visited. The
	// other cities of that province will then be skipped.
	// The graph is given as an adjacency MATRIX; the DFS can walk it directly: the neighbours of city u are every v with
	// isConnected[u][v] == 1.
	// T.C: O(n^2);   each city is visited once, and each visit scans its row of n cells
	// S.C: O(n);     visited + recursion stack
	// Note: The C++ code first converted the matrix into an adjacency list (adding every edge twice, from (i, j) and
	//       from (j, i)), and its DFS took the list BY VALUE (vector<vector<int>> adj, without &), so every recursive call
	//       COPIED the whole graph. That made it much slower and used much more memory than the O(n^2) it claimed.
	public int FindCircleNum(int[][] isConnected)
	{
		int n = isConnected.Length, provinces = 0;
		bool[] visited = new bool[n];

		for (int city = 0; city < n; city++)
		{
			if (visited[city]) continue;
			provinces++;
			Dfs(city, isConnected, visited);
		}
		return provinces;
	}

	private void Dfs(int u, int[][] isConnected, bool[] visited)
	{
		visited[u] = true;
		for (int v = 0; v < isConnected.Length; v++)
		{
			if (isConnected[u][v] == 1 && !visited[v]) Dfs(v, isConnected, visited);
		}
	}

// ------------------------------------------------------------------------------

	// The same with BFS instead of DFS (no recursion).
	// T.C: O(n^2)
	// S.C: O(n)
	public int FindCircleNum_BFS(int[][] isConnected)
	{
		int n = isConnected.Length, provinces = 0;
		bool[] visited = new bool[n];

		for (int city = 0; city < n; city++)
		{
			if (visited[city]) continue;
			provinces++;

			Queue<int> q = new Queue<int>();
			q.Enqueue(city);
			visited[city] = true;
			while (q.Count > 0)
			{
				int u = q.Dequeue();
				for (int v = 0; v < n; v++)
				{
					if (isConnected[u][v] == 1 && !visited[v])
					{
						visited[v] = true;
						q.Enqueue(v);
					}
				}
			}
		}
		return provinces;
	}
}

// A third way, with a Disjoint Set (Union-Find), is in '35_DisjointSet.cs'.


class Program
{
	static void Main()
	{
		int[][] isConnected = { new[] { 1, 1, 0 }, new[] { 1, 1, 0 }, new[] { 0, 0, 1 } };

		Console.WriteLine(new Solution().FindCircleNum(isConnected));       // o/p: 2
		Console.WriteLine(new Solution().FindCircleNum_BFS(isConnected));   // o/p: 2
	}
}

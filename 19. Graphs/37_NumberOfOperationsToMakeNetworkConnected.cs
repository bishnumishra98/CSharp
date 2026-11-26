// Leetcode: 1319. Number of Operations to Make Network Connected   --->   n computers, connections[i] = [a, b] is a cable.
// In one operation you can unplug any cable and plug it between any two computers. Return the fewest operations to
// connect ALL computers, or -1 if it is impossible.

// Example 1:
// Input: n = 4, connections = [[0,1],[0,2],[1,2]]
// Output: 1   (the cable 1-2 is extra: move it to connect computer 3)

// Example 2:
// Input: n = 6, connections = [[0,1],[0,2],[0,3],[1,2],[1,3]]
// Output: 2   (groups {0,1,2,3}, {4}, {5}: 2 extra cables connect the 3 groups)

// Example 3:
// Input: n = 6, connections = [[0,1],[0,2],[0,3],[1,2]]
// Output: -1  (only 4 cables, but 6 computers need at least 5)

// First understand '35_DisjointSet.cs'.


using System;

public class DisjointSet
{
	private int[] parent, size;

	public DisjointSet(int n)
	{
		parent = new int[n + 1];
		size = new int[n + 1];
		for (int i = 0; i <= n; i++)
		{
			parent[i] = i;
			size[i] = 1;
		}
	}

	public int Find(int x) => parent[x] == x ? x : (parent[x] = Find(parent[x]));

	public void Union(int u, int v)
	{
		int ru = Find(u), rv = Find(v);
		if (ru == rv) return;
		if (size[ru] < size[rv]) (ru, rv) = (rv, ru);
		parent[rv] = ru;
		size[ru] += size[rv];
	}
}

public class Solution
{
	// Algorithm:
	// 1. Connecting n computers needs at least n - 1 cables. Fewer -> -1. Otherwise there are always enough spare cables.
	// 2. Union every cable; count the groups (components) c.
	// 3. Joining c groups into one needs exactly c - 1 cables, and each operation moves one (spare) cable. Answer c - 1.
	// Why are there enough SPARE cables ? A group of k computers needs only k - 1 cables; anything more is spare. With at
	// least n - 1 cables in total and only n - c of them needed inside the groups, at least c - 1 are spare.
	// T.C: O(n + m * α(n));   m = number of cables
	// S.C: O(n)
	public int MakeConnected(int n, int[][] connections)
	{
		if (connections.Length < n - 1) return -1;

		DisjointSet ds = new DisjointSet(n);
		foreach (int[] c in connections) ds.Union(c[0], c[1]);

		int groups = 0;
		for (int i = 0; i < n; i++)
		{
			if (ds.Find(i) == i) groups++;
		}
		return groups - 1;
	}
}


class Program
{
	static void Main()
	{
		int[][] connections = { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 0, 3 }, new[] { 1, 2 }, new[] { 1, 3 } };

		Console.WriteLine(new Solution().MakeConnected(6, connections));   // o/p: 2
	}
}

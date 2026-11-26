// GFG: Number of Islands (online)   --->   An n x m grid starts as ALL water. operators[i] = [r, c] turns cell (r, c) into
// land. After EACH operation, report how many islands (groups of land connected up / down / left / right) there are.
// (Leetcode: 305. Number of Islands II is the same problem.)

// Example 1:
// Input: n = 4, m = 5, operators = [[1,1],[0,1],[3,3],[3,4]]
// Output: 1 1 2 2
// Explanation: (1,1) -> 1 island. (0,1) touches (1,1) -> still 1. (3,3) -> 2. (3,4) touches (3,3) -> still 2.

// Example 2:
// Input: n = 4, m = 5, operators = [[0,0],[1,1],[2,2],[3,3]]
// Output: 1 2 3 4   (diagonal cells do not touch)

// Problem link: https://www.geeksforgeeks.org/problems/number-of-islands/1

// Why not BFS after every operation ('06_NumberOfIslands.cs') ? That costs O(n * m) per operation. Islands only ever
// MERGE here (land is never removed): exactly what a Disjoint Set does in almost O(1). First understand '35_DisjointSet.cs'.


using System;
using System.Collections.Generic;

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

	public bool Union(int u, int v)   // false if they were already in the same group
	{
		int ru = Find(u), rv = Find(v);
		if (ru == rv) return false;
		if (size[ru] < size[rv]) (ru, rv) = (rv, ru);
		parent[rv] = ru;
		size[ru] += size[rv];
		return true;
	}
}

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };

	// Algorithm:
	// ● Give each cell one number for the Disjoint Set: id = r * m + c (row by row: 0, 1, ..., n * m - 1).
	// ● Keep a running count of islands. For each new land cell:
	//   1. If it is already land, nothing changes.
	//   2. Otherwise it is a NEW island by itself: count++.
	//   3. For each of its 4 neighbours that is land: if they are in DIFFERENT groups, Union them and count-- (two islands
	//      became one). If they are already in the same group, nothing changes.
	// T.C: O(n * m + k * α(n * m));   k = number of operations
	// S.C: O(n * m)
	public List<int> NumOfIslands(int n, int m, int[][] operators)
	{
		DisjointSet ds = new DisjointSet(n * m);
		bool[,] land = new bool[n, m];
		int islands = 0;
		List<int> result = new List<int>();

		foreach (int[] op in operators)
		{
			int r = op[0], c = op[1];
			if (!land[r, c])
			{
				land[r, c] = true;
				islands++;

				for (int k = 0; k < 4; k++)
				{
					int nr = r + dr[k], nc = c + dc[k];
					if (nr < 0 || nr >= n || nc < 0 || nc >= m || !land[nr, nc]) continue;
					if (ds.Union(r * m + c, nr * m + nc)) islands--;
				}
			}
			result.Add(islands);
		}
		return result;
	}
}


class Program
{
	static void Main()
	{
		int[][] operators = { new[] { 1, 1 }, new[] { 0, 1 }, new[] { 3, 3 }, new[] { 3, 4 } };

		Console.WriteLine(string.Join(" ", new Solution().NumOfIslands(4, 5, operators)));   // o/p: 1 1 2 2
	}
}

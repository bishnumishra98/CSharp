// Leetcode: 210. Course Schedule II (GFG: Course Schedule)   --->   There are n courses (0..n-1). prerequisites[i] =
// [a, b] means you must take course b BEFORE course a. Return an order in which you can take all courses, or an EMPTY
// array if it is impossible.
// (Leetcode: 207. Course Schedule asks only true / false: "can you finish all courses ?" -> the order is not empty.)

// Example 1:
// Input: n = 2, prerequisites = [[1,0]]
// Output: [0,1]

// Example 2:
// Input: n = 4, prerequisites = [[1,0],[2,0],[3,1],[3,2]]
// Output: [0,1,2,3] or [0,2,1,3]

// Example 3:
// Input: n = 2, prerequisites = [[1,0],[0,1]]
// Output: []   (0 needs 1 and 1 needs 0: a cycle)

// First understand '15_TopologicalSort.cs' and '16_DetectCycleInDirectedGraph.cs'.


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm:
	// 1. Build the graph: [a, b] means b must come before a, so the edge is b -> a.
	// 2. Kahn's algorithm gives a topological order.
	// 3. If it contains all n courses, it is a valid schedule. If not, there is a cycle (courses that wait for each
	//    other): return an empty array.
	// T.C: O(n + m);   m = number of prerequisites
	// S.C: O(n + m)
	public int[] FindOrder(int numCourses, int[][] prerequisites)
	{
		List<int>[] adj = new List<int>[numCourses];
		for (int i = 0; i < numCourses; i++) adj[i] = new List<int>();

		int[] inDegree = new int[numCourses];
		foreach (int[] p in prerequisites)
		{
			adj[p[1]].Add(p[0]);   // b -> a
			inDegree[p[0]]++;
		}

		Queue<int> q = new Queue<int>();
		for (int i = 0; i < numCourses; i++)
		{
			if (inDegree[i] == 0) q.Enqueue(i);
		}

		List<int> order = new List<int>();
		while (q.Count > 0)
		{
			int course = q.Dequeue();
			order.Add(course);
			foreach (int next in adj[course])
			{
				if (--inDegree[next] == 0) q.Enqueue(next);
			}
		}

		return order.Count == numCourses ? order.ToArray() : new int[0];
	}
}


class Program
{
	static void Main()
	{
		int[][] prerequisites = { new[] { 1, 0 }, new[] { 2, 0 }, new[] { 3, 1 }, new[] { 3, 2 } };

		Console.WriteLine(string.Join(" ", new Solution().FindOrder(4, prerequisites)));   // o/p: 0 1 2 3
	}
}

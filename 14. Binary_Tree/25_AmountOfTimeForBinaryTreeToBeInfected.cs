// Leetcode: 2385. Amount of Time for Binary Tree to Be Infected   --->   You are given the root of a binary tree with
// unique values, and an integer start. At minute 0, an infection starts from the node with value start.
// Each minute, a node becomes infected if it is not infected yet AND it is next to (connected by an edge to) an
// infected node. Return the number of minutes needed to infect the whole tree.

// Example 1:
// Input: root = [1,5,3,null,4,10,6,9,2], start = 3
//        _ 1 _
//       /     \
//      5       3
//       \     / \
//        4   10  6
//       / \
//      9   2
// Output: 4
// Explanation:
// Minute 0: 3
// Minute 1: 1, 10, 6
// Minute 2: 5
// Minute 3: 4
// Minute 4: 9, 2

// Example 2:
// Input: root = [1], start = 1
// Output: 0

// The problem: Infection spreads in 3 directions: left child, right child, AND the PARENT. A TreeNode only has links
// down to its children, so we can not go up. Fix: first remember every node's parent.


using System;
using System.Collections.Generic;

public class TreeNode
{
	public int val;
	public TreeNode left;
	public TreeNode right;
	public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
	{
		this.val = val; this.left = left; this.right = right;
	}
}

public class Solution
{
	// Algorithm: Treat the tree like a graph (every edge goes both ways), and spread out from 'start' level by level.
	// 1. BFS over the tree to fill a Dictionary {node -> parent}, and find the start node on the way.
	// 2. BFS again, from the start node. Each round of the BFS = one minute. From every node in the queue, infect its
	//    left child, right child and parent, if they are not infected yet (a HashSet 'infected' stops us from going back
	//    to a node, e.g., from 3 to 1 and back to 3).
	// 3. Count only the rounds that infected someone new. That count is the answer.
	// T.C: O(n);   2 BFS walks, Dictionary / HashSet work is O(1)
	// S.C: O(n);   the parent dictionary, the infected set and the queue
	// Note: The C++ code used map (a sorted tree map, O(log n) per lookup), so its real T.C was O(n log n), not O(n).
	//       unordered_map in C++, or Dictionary in C#, makes it O(n).
	public int AmountOfTime(TreeNode root, int start)
	{
		Dictionary<TreeNode, TreeNode> parent = new Dictionary<TreeNode, TreeNode>();
		TreeNode startNode = MapParents(root, start, parent);

		Queue<TreeNode> q = new Queue<TreeNode>();
		HashSet<TreeNode> infected = new HashSet<TreeNode>();
		q.Enqueue(startNode);
		infected.Add(startNode);
		int minutes = 0;

		while (q.Count > 0)
		{
			int size = q.Count;
			bool spread = false;

			for (int i = 0; i < size; i++)
			{
				TreeNode curr = q.Dequeue();
				parent.TryGetValue(curr, out TreeNode up);   // 'up' is null for the root

				foreach (TreeNode next in new[] { curr.left, curr.right, up })
				{
					// HashSet.Add returns false if 'next' is already infected
					if (next != null && infected.Add(next))
					{
						q.Enqueue(next);
						spread = true;
					}
				}
			}

			if (spread) minutes++;
		}

		return minutes;
	}

	// BFS: store each child's parent, and return the node whose value is 'start'.
	private TreeNode MapParents(TreeNode root, int start, Dictionary<TreeNode, TreeNode> parent)
	{
		TreeNode startNode = null;
		Queue<TreeNode> q = new Queue<TreeNode>();
		q.Enqueue(root);

		while (q.Count > 0)
		{
			TreeNode curr = q.Dequeue();
			if (curr.val == start) startNode = curr;

			if (curr.left != null)
			{
				parent[curr.left] = curr;
				q.Enqueue(curr.left);
			}
			if (curr.right != null)
			{
				parent[curr.right] = curr;
				q.Enqueue(curr.right);
			}
		}

		return startNode;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(5, null, new TreeNode(4, new TreeNode(9), new TreeNode(2)));
		root.right = new TreeNode(3, new TreeNode(10), new TreeNode(6));

		Console.WriteLine(new Solution().AmountOfTime(root, 3));   // o/p: 4
	}
}

// GFG: Top View of Binary Tree   --->   Given the root of a binary tree, return its top view: the nodes you can see when
// you look at the tree from the TOP. Return them from the leftmost to the rightmost.
// If several nodes are on the same vertical line, only the topmost one (closest to the root) is seen.

// Example 1:
// Input: root = [1, 2, 3]
//       1
//      / \
//     2   3
// Output: [2, 1, 3]

// Example 2:
// Input: root = [10, 20, 30, 40, 60, 90, 100]
//              10
//            /    \
//          20      30
//         /  \    /  \
//       40   60  90   100
// Output: [40, 20, 10, 30, 100]
// Explanation: 60 and 90 are on the same vertical line as 10, but below it, so they are hidden.

// Problem link: https://www.geeksforgeeks.org/problems/top-view-of-binary-tree/1

// ● Vertical lines (columns): Give the root column 0. A left child is 1 column to the left (col - 1), a right child is
//   1 column to the right (col + 1).
//   Example 2:  col -2: 40 |  col -1: 20 |  col 0: 10, 60, 90 |  col 1: 30 |  col 2: 100
//   The top view = the TOPMOST node of every column, from the leftmost column to the rightmost.


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
	// Algorithm: Level order traversal (BFS), carrying each node's column in the queue. Keep a Dictionary
	// {column -> value}. BFS visits nodes top to bottom, so the FIRST node we see in a column is its topmost one: store
	// it only if the column is not in the dictionary yet.
	// Why not DFS ? DFS goes deep first, so it can reach a deep node of a column before a higher one.
	// e.g., root 1, left 2, 2's right 4, 4's right 5, and root's right 3: DFS meets 5 (col 1) before 3 (col 1), but 3 is
	//       higher, so 3 is the one seen from the top.
	// How to print columns left to right ? Keep the smallest and largest column seen. Every column between them has a
	// node (to reach column -2 the tree must pass column -1), so just loop from minCol to maxCol.
	// T.C: O(n)
	// S.C: O(n);   the queue and the dictionary
	// Note: The C++ code used map<int, int>. A C++ map keeps keys sorted but every insert costs O(log n), so its real
	//       T.C was O(n log n), not O(n). The min/max column trick removes that sorting.
	public List<int> TopView(TreeNode root)
	{
		List<int> ans = new List<int>();
		if (root == null) return ans;

		Dictionary<int, int> top = new Dictionary<int, int>();   // {column -> topmost value}
		Queue<(TreeNode node, int col)> q = new Queue<(TreeNode node, int col)>();
		q.Enqueue((root, 0));
		int minCol = 0, maxCol = 0;

		while (q.Count > 0)
		{
			var (node, col) = q.Dequeue();

			top.TryAdd(col, node.val);   // adds only if 'col' is not there yet (first = topmost)
			minCol = Math.Min(minCol, col);
			maxCol = Math.Max(maxCol, col);

			if (node.left != null) q.Enqueue((node.left, col - 1));
			if (node.right != null) q.Enqueue((node.right, col + 1));
		}

		for (int c = minCol; c <= maxCol; c++) ans.Add(top[c]);
		return ans;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(10);
		root.left = new TreeNode(20, new TreeNode(40), new TreeNode(60));
		root.right = new TreeNode(30, new TreeNode(90), new TreeNode(100));

		Console.WriteLine(string.Join(" ", new Solution().TopView(root)));   // o/p: 40 20 10 30 100
	}
}

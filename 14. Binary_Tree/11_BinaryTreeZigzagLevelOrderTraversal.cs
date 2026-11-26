// Leetcode: 103. Binary Tree Zigzag Level Order Traversal   --->   Given the root of a binary tree, return the zigzag
// level order traversal of its nodes' values (left to right, then right to left for the next level, and so on).

// Example 1:
// Input: root = [3,9,20,null,null,15,7]
//        3
//       / \
//      9  20
//        /  \
//       15   7
// Output: [[3],[20,9],[15,7]]

// Example 2:
// Input: root = [1]
// Output: [[1]]

// Example 3:
// Input: root = []
// Output: []

// First understand the level order traversal in '02_BinaryTreeTraversals.cs'.


using System;
using System.Collections.Generic;
using System.Linq;

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
	// Algorithm: A normal level order traversal (the queue always gives each level left to right). Only the place where
	// we WRITE each value changes. Keep a flag 'leftToRight', and flip it after every level.
	// For a level of 'size' nodes, the i-th node taken from the queue goes to:
	//   index i              when leftToRight  (normal order)
	//   index size - 1 - i   otherwise         (filled from the back, so the level comes out reversed)
	// e.g., level [9, 20] going right to left: 9 -> index 1, 20 -> index 0. Row = [20, 9].
	// (Reversing the row at the end also works, but writing to the right index saves that extra pass.)
	// T.C: O(n)
	// S.C: O(w);   w = max width of the tree, for the queue
	public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
	{
		IList<IList<int>> ans = new List<IList<int>>();
		if (root == null) return ans;

		Queue<TreeNode> q = new Queue<TreeNode>();
		q.Enqueue(root);
		bool leftToRight = true;

		while (q.Count > 0)
		{
			int size = q.Count;
			int[] row = new int[size];

			for (int i = 0; i < size; i++)
			{
				TreeNode node = q.Dequeue();

				int index = leftToRight ? i : size - 1 - i;
				row[index] = node.val;

				if (node.left != null) q.Enqueue(node.left);
				if (node.right != null) q.Enqueue(node.right);
			}

			leftToRight = !leftToRight;   // change direction for the next level
			ans.Add(row);
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(3);
		root.left = new TreeNode(9);
		root.right = new TreeNode(20, new TreeNode(15), new TreeNode(7));

		var levels = new Solution().ZigzagLevelOrder(root).Select(row => string.Join(" ", row));
		Console.WriteLine(string.Join(" | ", levels));   // o/p: 3 | 20 9 | 15 7
	}
}

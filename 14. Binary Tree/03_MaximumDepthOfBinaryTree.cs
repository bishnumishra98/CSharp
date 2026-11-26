// Leetcode: 104. Maximum Depth of Binary Tree   --->   Given the root of a binary tree, return its maximum depth (height).
// Maximum depth = the number of NODES on the longest path from the root down to the farthest leaf.

// Example 1:
// Input: root = [3,9,20,null,null,15,7]
//          3
//         / \
//        9   20
//           /  \
//          15   7
// Output: 3
// Explanation: The longest paths are 3 -> 20 -> 15 and 3 -> 20 -> 7. Both have 3 nodes.

// Example 2:
// Input: root = [1,null,2]
// Output: 2

// Note: Leetcode counts NODES here. If a question counts EDGES, the answer is (nodes - 1), i.e., return -1 for null.


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
	// Approach 1 (Recursion / DFS): Height of a tree = 1 (for the root) + the taller of its 2 subtrees.
	// An empty tree (null) has height 0. Trust the recursion to give the heights of the left and right subtrees.
	// e.g., height(9) = 1,  height(20) = 1 + max(1, 1) = 2,  height(3) = 1 + max(1, 2) = 3.
	// T.C: O(n);   every node is visited once
	// S.C: O(h);   recursion stack. h = log(n) for a balanced tree, h = n for a skewed tree.
	public int MaxDepth(TreeNode root)
	{
		if (root == null) return 0;

		int leftHeight = MaxDepth(root.left);
		int rightHeight = MaxDepth(root.right);

		return Math.Max(leftHeight, rightHeight) + 1;
	}

// ------------------------------------------------------------------------------

	// Approach 2 (Level order / BFS): The height is simply the number of levels. Do a level order traversal (see
	// '02_BinaryTreeTraversals.cs') and count the levels.
	// T.C: O(n)
	// S.C: O(w);   w = max width of the tree (the queue holds one level)
	public int MaxDepth_BFS(TreeNode root)
	{
		if (root == null) return 0;

		Queue<TreeNode> q = new Queue<TreeNode>();
		q.Enqueue(root);
		int levels = 0;

		while (q.Count > 0)
		{
			int size = q.Count;
			for (int i = 0; i < size; i++)
			{
				TreeNode node = q.Dequeue();
				if (node.left != null) q.Enqueue(node.left);
				if (node.right != null) q.Enqueue(node.right);
			}
			levels++;   // one full level done
		}

		return levels;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(3);
		root.left = new TreeNode(9);
		root.right = new TreeNode(20);
		root.right.left = new TreeNode(15);
		root.right.right = new TreeNode(7);

		Console.WriteLine(new Solution().MaxDepth(root));       // o/p: 3
		Console.WriteLine(new Solution().MaxDepth_BFS(root));   // o/p: 3
	}
}

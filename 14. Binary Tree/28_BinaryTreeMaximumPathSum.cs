// Leetcode: 124. Binary Tree Maximum Path Sum   --->   A path in a binary tree is a sequence of nodes where each pair of
// neighbours is connected by an edge. A node can appear in the path at most once. The path does NOT need to pass
// through the root. The path sum = the sum of the node values in the path.
// Given the root of a binary tree, return the maximum path sum of any non-empty path.

// Example 1:
// Input: root = [1,2,3]
//       1
//      / \
//     2   3
// Output: 6   (2 -> 1 -> 3)

// Example 2:
// Input: root = [-10,9,20,null,null,15,7]
//      -10
//      /  \
//     9   20
//        /  \
//       15   7
// Output: 42   (15 -> 20 -> 7. Going up to -10 would only make it smaller.)

// First understand '10_DiameterOfBinaryTree.cs'. This is the same idea: there the longest path, here the biggest sum.


using System;

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
	// Algorithm: At every node, think of 2 different things:
	// 1. The best path that BENDS at this node (this node is its highest point):
	//    left gain + node.val + right gain.   Use it to update the answer.
	// 2. The best path this node can give to its PARENT: the parent's path can only come down ONE side of this node
	//    (a path can not split), so return  node.val + max(left gain, right gain).
	// A "gain" is the best sum going down from a child. If a gain is negative, adding it would only lower the sum, so
	// use 0 instead (that means: do not go down that side at all).
	// e.g., Example 2: node 15 -> gives 15, node 7 -> gives 7. Node 20: bend = 15 + 20 + 7 = 42 (answer so far), gives
	//       20 + 15 = 35 up. Node 9 gives 9. Root -10: bend = 9 - 10 + 35 = 34 < 42. Answer 42.
	// T.C: O(n)
	// S.C: O(h);   recursion stack, h = n for a skewed tree
	// Why start the answer at int.MinValue, not 0 ? If all values are negative (e.g., [-3]), the answer is negative
	// (-3). The path must have at least one node, so the answer can not just be 0.
	public int MaxPathSum(TreeNode root)
	{
		int best = int.MinValue;
		MaxGain(root, ref best);
		return best;
	}

	private int MaxGain(TreeNode node, ref int best)
	{
		if (node == null) return 0;

		int left = Math.Max(0, MaxGain(node.left, ref best));     // skip a negative side
		int right = Math.Max(0, MaxGain(node.right, ref best));

		best = Math.Max(best, left + node.val + right);            // path that bends here

		return node.val + Math.Max(left, right);                   // path the parent can use
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(-10);
		root.left = new TreeNode(9);
		root.right = new TreeNode(20, new TreeNode(15), new TreeNode(7));

		Console.WriteLine(new Solution().MaxPathSum(root));   // o/p: 42
	}
}

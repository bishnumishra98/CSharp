// Leetcode: 101. Symmetric Tree   --->   Given the root of a binary tree, check whether it is a mirror of itself
// (i.e., symmetric around its center).

// Example 1:
// Input: root = [1,2,2,3,4,4,3]
//          1
//        /   \
//       2     2
//      / \   / \
//     3   4 4   3
// Output: true

// Example 2:
// Input: root = [1,2,2,null,3,null,3]
//       1
//      / \
//     2   2
//      \   \
//       3   3
// Output: false

// First understand '04_SameTree.cs'. This is the same code with ONE change.


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
	// Algorithm: A tree is symmetric if its left subtree is the MIRROR image of its right subtree.
	// Two trees p and q are mirrors if:
	//   their roots have the same value,
	//   AND p's LEFT is a mirror of q's RIGHT (the outer pair: 3 and 3 above),
	//   AND p's RIGHT is a mirror of q's LEFT (the inner pair: 4 and 4 above).
	// Same Tree compared left with left. Mirror compares left with right. That is the only change.
	// T.C: O(n)
	// S.C: O(h);   recursion stack, h = n for a skewed tree
	public bool IsSymmetric(TreeNode root)
	{
		if (root == null) return true;   // an empty tree is symmetric
		return IsMirror(root.left, root.right);
	}

	private bool IsMirror(TreeNode p, TreeNode q)
	{
		if (p == null || q == null) return p == q;

		return p.val == q.val && IsMirror(p.left, q.right) && IsMirror(p.right, q.left);
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(3), new TreeNode(4));
		root.right = new TreeNode(2, new TreeNode(4), new TreeNode(3));

		Console.WriteLine(new Solution().IsSymmetric(root));   // o/p: True
	}
}

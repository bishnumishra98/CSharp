// Leetcode: 100. Same Tree   --->   Given the roots of two binary trees p and q, check if they are the same.
// Two binary trees are the same if they have the same shape, and the nodes have the same values.

// Example 1:
// Input: p = [1,2,3], q = [1,2,3]
//     1           1
//    / \         / \
//   2   3       2   3
// Output: true

// Example 2:
// Input: p = [1,2], q = [1,null,2]
//     1        1
//    /          \
//   2            2
// Output: false   (same values, different shape)

// Example 3:
// Input: p = [1,2,1], q = [1,1,2]
//     1           1
//    / \         / \
//   2   1       1   2
// Output: false


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
	// Algorithm: Walk both trees together (a preorder walk). At every step compare the two current nodes:
	// 1. Both null: same (nothing there in both trees).
	// 2. Only one is null: different shape, so not the same.
	// 3. Both exist: their values must be equal, AND their left subtrees must be the same, AND their right subtrees.
	// T.C: O(min(m, n));   m, n = nodes in the 2 trees. We stop at the first difference, so we never walk past the
	//                      smaller tree.
	// S.C: O(min(h1, h2));   recursion stack (h = n for skewed trees)
	public bool IsSameTree(TreeNode p, TreeNode q)
	{
		// Cases 1 and 2 in one line: if at least one is null, they are the same only if BOTH are null.
		if (p == null || q == null) return p == q;

		return p.val == q.val && IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
	}
}


class Program
{
	static void Main()
	{
		TreeNode p = new TreeNode(1, new TreeNode(2), new TreeNode(3));
		TreeNode q = new TreeNode(1, new TreeNode(2), new TreeNode(3));

		Console.WriteLine(new Solution().IsSameTree(p, q));   // o/p: True
	}
}

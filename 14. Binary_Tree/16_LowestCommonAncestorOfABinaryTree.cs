// Leetcode: 236. Lowest Common Ancestor of a Binary Tree   --->   Given a binary tree, find the lowest common ancestor
// (LCA) of two given nodes p and q in the tree.
// LCA = the LOWEST (deepest) node that has both p and q below it. A node counts as being below itself.
// All node values are unique, and p and q are both in the tree.

// Example 1:
// Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
//              _ 3 _
//            /       \
//           5         1
//          / \       / \
//         6   2     0   8
//            / \
//           7   4
// Output: 3

// Example 2:
// Input: same tree, p = 5, q = 4
// Output: 5   (4 is below 5, and 5 counts as below itself)

// Example 3:
// Input: root = [1,2], p = 1, q = 2
// Output: 1


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
	// Algorithm: Ask each subtree "did you find p or q ?" and pass the answer up.
	// 1. If the node is null, return null (nothing found here).
	// 2. If the node IS p or q, return it (found one, no need to look below it: if the other one is below, this node is
	//    the LCA anyway).
	// 3. Otherwise search left and right:
	//    I.   Both sides return a node: p is on one side and q on the other. This node is where they split, so it is the
	//         LCA. Return this node.
	//    II.  Only one side returns a node: pass that one up (it is either p / q, or the LCA already found below).
	//    III. Both null: return null.
	// e.g., p = 5, q = 1: at 3, the left side returns 5 and the right side returns 1. Both found, so 3 is the LCA.
	//       p = 5, q = 4: at 5, return 5 at once (step 2). At 3, left = 5, right = null, so 5 is passed up: answer 5.
	// T.C: O(n)
	// S.C: O(h);   recursion stack, h = n for a skewed tree
	public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
	{
		if (root == null || root == p || root == q) return root;

		TreeNode left = LowestCommonAncestor(root.left, p, q);
		TreeNode right = LowestCommonAncestor(root.right, p, q);

		if (left != null && right != null) return root;   // p and q are on different sides

		return left ?? right;   // the non-null one (or null if both are null)
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(3);
		root.left = new TreeNode(5, new TreeNode(6), new TreeNode(2, new TreeNode(7), new TreeNode(4)));
		root.right = new TreeNode(1, new TreeNode(0), new TreeNode(8));

		TreeNode p = root.left;    // 5
		TreeNode q = root.right;   // 1

		Console.WriteLine(new Solution().LowestCommonAncestor(root, p, q).val);   // o/p: 3
	}
}

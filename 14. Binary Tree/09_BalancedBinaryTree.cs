// Leetcode: 110. Balanced Binary Tree   --->   Given a binary tree, determine if it is height-balanced.
// Height-balanced = for EVERY node, the heights of its left and right subtrees differ by at most 1.

// Example 1:
// Input: root = [3,9,20,null,null,15,7]
//         3
//        / \
//       9  20
//          / \
//         15  7
// Output: true

// Example 2:
// Input: root = [1,2,2,3,3,null,null,4,4]
//           1
//          / \
//         2   2
//        / \
//       3   3
//      / \
//     4   4
// Output: false   (at the root: left height = 3, right height = 1, difference = 2)

// Example 3:
// Input: root = []
// Output: true

// A node is balanced only if all 3 are true:
// 1. Its left subtree is balanced.
// 2. Its right subtree is balanced.
// 3. |height(left) - height(right)| <= 1.
// Heights are found like in '03_MaximumDepthOfBinaryTree.cs'.


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
	private int Height(TreeNode root)
	{
		if (root == null) return 0;
		return Math.Max(Height(root.left), Height(root.right)) + 1;
	}

	// Brute force: Check the 3 rules at every node, computing heights with a separate Height() call.
	// The problem: Height() walks the whole subtree, and it is called again for every node inside it. A node deep down
	// is measured once for each of its ancestors.
	// T.C: O(n^2);   skewed tree: Height() costs n + (n-1) + ... + 1. (O(n log n) for a balanced tree.)
	// S.C: O(h);   recursion stack
	public bool IsBalanced_BruteForce(TreeNode root)
	{
		if (root == null) return true;

		if (Math.Abs(Height(root.left) - Height(root.right)) > 1) return false;

		return IsBalanced_BruteForce(root.left) && IsBalanced_BruteForce(root.right);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Find heights bottom-up ONCE, and check the balance on the way back up.
	// CheckHeight returns the height of the subtree, or -1 if it is NOT balanced. A -1 from any child is passed straight
	// up (no need to look further: one unbalanced node makes the whole tree unbalanced).
	// e.g., Example 2: node 3 (with 4, 4) -> height 2. Left node 2 -> height 3. Right node 2 -> height 1.
	//       Root: |3 - 1| = 2 > 1, so return -1. Not balanced.
	// T.C: O(n);   every node is visited once
	// S.C: O(h);   recursion stack, h = n for a skewed tree
	public bool IsBalanced(TreeNode root)
	{
		return CheckHeight(root) != -1;
	}

	private int CheckHeight(TreeNode root)
	{
		if (root == null) return 0;

		int leftHeight = CheckHeight(root.left);
		if (leftHeight == -1) return -1;     // left subtree is not balanced

		int rightHeight = CheckHeight(root.right);
		if (rightHeight == -1) return -1;    // right subtree is not balanced

		if (Math.Abs(leftHeight - rightHeight) > 1) return -1;   // this node is not balanced

		return Math.Max(leftHeight, rightHeight) + 1;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(3);
		root.left = new TreeNode(9);
		root.right = new TreeNode(20, new TreeNode(15), new TreeNode(7));

		Console.WriteLine(new Solution().IsBalanced_BruteForce(root));   // o/p: True
		Console.WriteLine(new Solution().IsBalanced(root));              // o/p: True
	}
}

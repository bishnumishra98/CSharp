// Minimum and Maximum in a BST   --->   Given the root of a BST, return the node with the smallest value and the node with
// the largest value.

// Example:
//          5
//         / \
//        1   7
//         \   \
//          3   9
//         / \
//        2   4
// Output: min = 1, max = 9


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
	// Minimum: Everything smaller than a node is on its LEFT. So keep going left until there is no left child.
	// That leftmost node is the smallest. (It does not have to be a leaf: 1 above has a right child.)
	// T.C: O(h)
	// S.C: O(1)
	public TreeNode MinNode(TreeNode root)
	{
		if (root == null) return null;

		TreeNode temp = root;
		while (temp.left != null) temp = temp.left;
		return temp;
	}

// ------------------------------------------------------------------------------

	// Maximum: the mirror. Keep going RIGHT. The rightmost node is the largest.
	// T.C: O(h)
	// S.C: O(1)
	public TreeNode MaxNode(TreeNode root)
	{
		if (root == null) return null;

		TreeNode temp = root;
		while (temp.right != null) temp = temp.right;
		return temp;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(5);
		root.left = new TreeNode(1, null, new TreeNode(3, new TreeNode(2), new TreeNode(4)));
		root.right = new TreeNode(7, null, new TreeNode(9));

		Console.WriteLine(new Solution().MinNode(root).val);   // o/p: 1
		Console.WriteLine(new Solution().MaxNode(root).val);   // o/p: 9
	}
}

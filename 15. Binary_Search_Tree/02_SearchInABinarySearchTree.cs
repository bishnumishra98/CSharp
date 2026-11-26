// Leetcode: 700. Search in a Binary Search Tree   --->   You are given the root of a BST and an integer val. Find the node
// whose value equals val and return the subtree rooted at that node. If there is no such node, return null.

// Example 1:
// Input: root = [4,2,7,1,3], val = 2
//         4
//        / \
//       2   7
//      / \
//     1   3
// Output: [2,1,3]

// Example 2:
// Input: root = [4,2,7,1,3], val = 5
// Output: []


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
	// Algorithm: Binary search on a tree. Start at the root:
	//   val == node: found.
	//   val <  node: it can only be in the LEFT subtree, go left.
	//   val >  node: it can only be in the RIGHT subtree, go right.
	// Falling off the tree (null) means it is not there.
	// e.g., val = 2: 2 < 4, go left -> 2 == 2, found.
	//       val = 5: 5 > 4, go right -> 5 < 7, go left -> null. Not found.
	// No recursion is needed, because we only ever go down ONE path.
	// T.C: O(h);   h = log(n) for a balanced BST, h = n for a skewed one
	// S.C: O(1)
	public TreeNode SearchBST(TreeNode root, int val)
	{
		while (root != null && root.val != val)
		{
			root = val < root.val ? root.left : root.right;
		}
		return root;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(4);
		root.left = new TreeNode(2, new TreeNode(1), new TreeNode(3));
		root.right = new TreeNode(7);

		TreeNode ans = new Solution().SearchBST(root, 2);
		Console.WriteLine($"{ans.val} {ans.left.val} {ans.right.val}");   // o/p: 2 1 3
	}
}

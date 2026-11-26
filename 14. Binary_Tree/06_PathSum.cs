// Leetcode: 112. Path Sum   --->   Given the root of a binary tree and an integer targetSum, return true if the tree has
// a root-to-leaf path such that adding up all the values along the path equals targetSum.
// A leaf is a node with no children.

// Example 1:
// Input: root = [5,4,8,11,null,13,4,7,2,null,null,null,1], targetSum = 22
//               5
//              / \
//             4   8
//            /   / \
//           11  13  4
//          /  \      \
//         7    2      1
// Output: true
// Explanation: 5 + 4 + 11 + 2 = 22

// Example 2:
// Input: root = [1,2,3], targetSum = 5
// Output: false   (1 + 2 = 3, 1 + 3 = 4)

// Example 3:
// Input: root = [], targetSum = 0
// Output: false   (an empty tree has no root-to-leaf path)


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
	// Algorithm: Go down the tree (DFS). At each node, subtract its value from targetSum: what is left is the sum we
	// still need from the rest of the path. At a LEAF, the path is complete: it is valid if nothing is left (0).
	// e.g., 22 -> at 5: 17 -> at 4: 13 -> at 11: 2 -> at leaf 2: 0. Found.
	// Why check at the leaf and not at null ? The path must END at a leaf. A node with one child is not a leaf, so its
	// missing (null) side does not count as a path.
	// T.C: O(n)
	// S.C: O(h);   recursion stack
	public bool HasPathSum(TreeNode root, int targetSum)
	{
		if (root == null) return false;

		targetSum -= root.val;

		if (root.left == null && root.right == null) return targetSum == 0;   // leaf

		return HasPathSum(root.left, targetSum) || HasPathSum(root.right, targetSum);
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(5);
		root.left = new TreeNode(4);
		root.right = new TreeNode(8);
		root.left.left = new TreeNode(11, new TreeNode(7), new TreeNode(2));
		root.right.left = new TreeNode(13);
		root.right.right = new TreeNode(4, null, new TreeNode(1));

		Console.WriteLine(new Solution().HasPathSum(root, 22));   // o/p: True
	}
}

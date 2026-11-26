// Leetcode: 1373. Maximum Sum BST in Binary Tree   --->   Given a binary tree, return the maximum SUM of all values of any
// subtree that is also a BST. (An empty tree counts as a BST with sum 0.)

// Example 1:
// Input: root = [1,4,3,2,4,2,5,null,null,null,null,null,null,4,6]
//          _ 1 _
//         /     \
//        4       3
//       / \     / \
//      2   4   2   5
//                 / \
//                4   6
// Output: 20   (the subtree at 3: 3 + 2 + 5 + 4 + 6 = 20)

// Example 2:
// Input: root = [4,3,null,1,2]
//         4
//        /
//       3
//      / \
//     1   2
// Output: 2   (3 is not a BST because 2 is on its right. The best is the single node 2.)

// Example 3:
// Input: root = [-4,-2,-5]
// Output: 0   (every BST here has a negative sum, so the empty tree, with sum 0, is the best)

// First understand '16_LargestBSTInABinaryTree.cs'. Same bottom-up idea: there we tracked the SIZE, here the SUM.


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
	// Algorithm: Postorder. Each call returns (isBST, sum, min, max) about its subtree:
	//   null: (true, 0, int.MaxValue, int.MinValue)   (same trick as in the Largest BST file)
	//   BST here (both children are BSTs and left.max < node < right.min): sum = left.sum + right.sum + node.
	//      Update the best answer with it.
	//   Not a BST: (false, ...). The sum does not matter any more, because no parent can be a BST.
	// Why keep the best answer on the side, and not just "the largest one below" like the size ? Because a BST's sum
	// can be SMALLER than a BST inside it (negative values). e.g., a BST with -10 at the root and 5 as its right child:
	// the whole BST sums to -5, but the subtree 5 alone is better. So EVERY BST we meet is compared with the best.
	// 'best' starts at 0: the empty tree (Example 3).
	// T.C: O(n)
	// S.C: O(h);   recursion stack
	public int MaxSumBST(TreeNode root)
	{
		int best = 0;
		Check(root, ref best);
		return best;
	}

	private (bool isBST, int sum, int min, int max) Check(TreeNode root, ref int best)
	{
		if (root == null) return (true, 0, int.MaxValue, int.MinValue);

		var left = Check(root.left, ref best);
		var right = Check(root.right, ref best);

		if (left.isBST && right.isBST && root.val > left.max && root.val < right.min)
		{
			int sum = left.sum + right.sum + root.val;
			best = Math.Max(best, sum);
			return (true, sum, Math.Min(root.val, left.min), Math.Max(root.val, right.max));
		}

		return (false, 0, int.MinValue, int.MaxValue);
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(4, new TreeNode(2), new TreeNode(4));
		root.right = new TreeNode(3, new TreeNode(2), new TreeNode(5, new TreeNode(4), new TreeNode(6)));

		Console.WriteLine(new Solution().MaxSumBST(root));   // o/p: 20
	}
}

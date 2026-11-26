// Leetcode: 543. Diameter of Binary Tree   --->   Given the root of a binary tree, return the length of its diameter.
// Diameter = the length of the LONGEST path between any two nodes in the tree. The path may or may not pass through the
// root. Length = number of EDGES on the path.

// Example 1:
// Input: root = [1,2,3,4,5]
//          1
//         / \
//        2   3
//       / \
//      4   5
// Output: 3
// Explanation: The path 4 -> 2 -> 1 -> 3 (or 5 -> 2 -> 1 -> 3) has 3 edges.

// Example 2:
//            1
//           / \
//          2   3
//         / \
//        4   5
//       /     \
//      6       7
//     /         \
//    8           9
// Output: 6
// Explanation: The path 8 -> 6 -> 4 -> 2 -> 5 -> 7 -> 9 has 6 edges. It does NOT pass through the root 1.

// Key idea: Every path has one HIGHEST node, where it "bends". At that node the path goes down the left side and down
// the right side. So the longest path that bends at a node = height(left child) + height(right child), heights counted
// in NODES. (The left child's height in nodes = the number of edges from this node down to the deepest leaf on the
// left.) The diameter is the biggest such value over ALL nodes.
// e.g., Example 2 at node 2: height(left = 4) = 3 (4, 6, 8), height(right = 5) = 3 (5, 7, 9). 3 + 3 = 6.
//       At the root 1: height(left) = 4, height(right) = 1. 4 + 1 = 5 only.


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

	// Brute force: For every node, compute height(left) + height(right) with separate Height() calls, and keep the max.
	// Same problem as the brute force in '09_BalancedBinaryTree.cs': nodes are measured again and again.
	// T.C: O(n^2);   for a skewed tree
	// S.C: O(h)
	public int DiameterOfBinaryTree_BruteForce(TreeNode root)
	{
		if (root == null) return 0;

		int throughRoot = Height(root.left) + Height(root.right);
		int leftBest = DiameterOfBinaryTree_BruteForce(root.left);
		int rightBest = DiameterOfBinaryTree_BruteForce(root.right);

		return Math.Max(throughRoot, Math.Max(leftBest, rightBest));
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Compute heights bottom-up ONCE (like '03_MaximumDepthOfBinaryTree.cs'). While we have both
	// heights at a node, also update the best diameter: lh + rh. The function still RETURNS the height (for the parent),
	// and the diameter is kept on the side in a 'ref' variable.
	// T.C: O(n);   every node is visited once
	// S.C: O(h);   recursion stack, h = n for a skewed tree
	public int DiameterOfBinaryTree(TreeNode root)
	{
		int diameter = 0;
		HeightAndDiameter(root, ref diameter);
		return diameter;
	}

	private int HeightAndDiameter(TreeNode root, ref int diameter)
	{
		if (root == null) return 0;

		int lh = HeightAndDiameter(root.left, ref diameter);
		int rh = HeightAndDiameter(root.right, ref diameter);

		diameter = Math.Max(diameter, lh + rh);   // longest path that bends at this node

		return Math.Max(lh, rh) + 1;              // height of this node, for its parent
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2);
		root.right = new TreeNode(3);
		root.left.left = new TreeNode(4, new TreeNode(6, new TreeNode(8), null), null);
		root.left.right = new TreeNode(5, null, new TreeNode(7, null, new TreeNode(9)));

		Console.WriteLine(new Solution().DiameterOfBinaryTree_BruteForce(root));   // o/p: 6
		Console.WriteLine(new Solution().DiameterOfBinaryTree(root));              // o/p: 6
	}
}

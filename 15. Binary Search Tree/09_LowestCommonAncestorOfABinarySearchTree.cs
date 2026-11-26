// Leetcode: 235. Lowest Common Ancestor of a Binary Search Tree   --->   Given a BST, find the lowest common ancestor
// (LCA) of two given nodes p and q.
// LCA = the lowest (deepest) node that has both p and q below it (a node counts as being below itself).

// Example 1:
// Input: root = [6,2,8,0,4,7,9,null,null,3,5], p = 3, q = 5
//           6
//         /   \
//        2     8
//       / \   / \
//      0   4 7   9
//         / \
//        3   5
// Output: 4   (3 and 5 are on the 2 sides of 4)
// Note: The C++ explanation here said "The LCA of nodes 2 and 8 is 6", which belongs to another example. Fixed.

// Example 2:
// Input: same tree, p = 2, q = 4
// Output: 2   (4 is below 2, and 2 counts as below itself)

// For ANY binary tree, see '14. Binary Tree/16_LowestCommonAncestorOfABinaryTree.cs' (O(n), it must search both sides).
// In a BST the values tell us which side to go, so we only walk ONE path.


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
	// Algorithm: Start at the root, and walk down:
	//   Both p and q are SMALLER than the node: both are in the left subtree, so the LCA is there too. Go left.
	//   Both are BIGGER: go right.
	//   Otherwise (one on each side, or one of them IS this node): this is the place where their paths split. It is
	//   the LCA.
	// e.g., p = 3, q = 5: at 6 both are smaller, go left -> at 2 both are bigger, go right -> at 4: 3 < 4 < 5. Answer 4.
	//       p = 2, q = 4: at 6 both smaller, go left -> at 2: p is this node. Answer 2.
	// T.C: O(h)
	// S.C: O(1)
	public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
	{
		while (root != null)
		{
			if (p.val < root.val && q.val < root.val) root = root.left;
			else if (p.val > root.val && q.val > root.val) root = root.right;
			else break;   // split point
		}
		return root;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(6);
		root.left = new TreeNode(2, new TreeNode(0), new TreeNode(4, new TreeNode(3), new TreeNode(5)));
		root.right = new TreeNode(8, new TreeNode(7), new TreeNode(9));

		TreeNode p = root.left.right.left;    // 3
		TreeNode q = root.left.right.right;   // 5

		Console.WriteLine(new Solution().LowestCommonAncestor(root, p, q).val);   // o/p: 4
	}
}

// GFG: Tree Boundary Traversal   --->   Given the root of a binary tree, return its boundary traversal in this order:
// 1. Left boundary: from the root, keep going down the left edge (prefer the left child, else the right child).
//    Leaves are NOT included here.
// 2. Leaves: all leaf nodes, from left to right.
// 3. Right boundary in REVERSE: the right edge (prefer the right child, else the left child), without leaves,
//    added from bottom to top.
// The root is added once, and every leaf is added only in step 2 (so nothing repeats).

// Example 1:
// Input: root = [1, 2, 3, 4, 5, 6, 7, N, N, 8, 9, N, N, N, N]
//            _ 1 _
//           /     \
//          2       3
//         / \     / \
//        4   5   6   7
//           / \
//          8   9
// Output: [1, 2, 4, 8, 9, 6, 7, 3]
// Left boundary: [1, 2]   (4 is a leaf, so it is left out here)
// Leaves: [4, 8, 9, 6, 7]
// Right boundary reversed: [3]   (7 is a leaf)

// Example 2:
// Input: root = [1, N, 2, N, 3, N, 4, N, N]
//     1
//      \
//       2
//        \
//         3
//          \
//           4
// Output: [1, 4, 3, 2]
// Left boundary: [1]   (no left subtree, so only the root)
// Leaves: [4]
// Right boundary reversed: [3, 2]

// Problem link: https://www.geeksforgeeks.org/problems/boundary-traversal-of-binary-tree/1


using System;
using System.Collections.Generic;

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
	private bool IsLeaf(TreeNode node) => node.left == null && node.right == null;

	// Algorithm: Do the 3 parts one after another.
	// 1. Add the root (only if it is not a leaf; a single-node tree's root is added as a leaf in step 3).
	// 2. Left boundary: start at root.left, add every non-leaf, move left (or right if there is no left).
	// 3. Leaves: any DFS that goes left before right (preorder here) meets the leaves from left to right.
	// 4. Right boundary: start at root.right, collect every non-leaf moving right (or left), then add them reversed.
	// T.C: O(h) + O(n) + O(h) = O(n)
	// S.C: O(h);   recursion stack for the leaves, and the temp list of the right boundary
	public List<int> BoundaryTraversal(TreeNode root)
	{
		List<int> res = new List<int>();
		if (root == null) return res;

		if (!IsLeaf(root)) res.Add(root.val);

		AddLeftBoundary(root, res);
		AddLeaves(root, res);
		AddRightBoundary(root, res);

		return res;
	}

	private void AddLeftBoundary(TreeNode root, List<int> res)
	{
		TreeNode curr = root.left;
		while (curr != null)
		{
			if (!IsLeaf(curr)) res.Add(curr.val);
			curr = curr.left != null ? curr.left : curr.right;
		}
	}

	private void AddLeaves(TreeNode root, List<int> res)
	{
		if (root == null) return;

		if (IsLeaf(root))
		{
			res.Add(root.val);
			return;
		}

		AddLeaves(root.left, res);
		AddLeaves(root.right, res);
	}

	private void AddRightBoundary(TreeNode root, List<int> res)
	{
		List<int> temp = new List<int>();
		TreeNode curr = root.right;
		while (curr != null)
		{
			if (!IsLeaf(curr)) temp.Add(curr.val);
			curr = curr.right != null ? curr.right : curr.left;
		}

		for (int i = temp.Count - 1; i >= 0; i--) res.Add(temp[i]);   // bottom to top
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(4), new TreeNode(5, new TreeNode(8), new TreeNode(9)));
		root.right = new TreeNode(3, new TreeNode(6), new TreeNode(7));

		Console.WriteLine(string.Join(" ", new Solution().BoundaryTraversal(root)));   // o/p: 1 2 4 8 9 6 7 3
	}
}

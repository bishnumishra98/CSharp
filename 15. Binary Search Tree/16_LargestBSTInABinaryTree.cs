// GFG: Largest BST   --->   Given a binary tree, find the SIZE (number of nodes) of its largest subtree that is a BST.
// A subtree = a node with EVERYTHING below it. It is a BST if left < node < right holds everywhere inside it (no
// duplicates).

// Example 1:
// Input: root = [5, 2, 4, 1, 3]
//          5
//         / \
//        2   4
//       / \
//      1   3
// Output: 3   (the subtree 2, 1, 3. The whole tree is not a BST, because 4 is on the right of 5 but smaller.)

// Example 2:
// Input: root = [6, 7, 3, N, 2, 2, 4]
//          _ 6 _
//         /     \
//        7       3
//         \     / \
//          2   2   4
// Output: 3   (the subtree 3, 2, 4)

// Problem link: https://www.geeksforgeeks.org/problems/largest-bst/1

// First understand '08_ValidateBinarySearchTree.cs'.


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
	// Brute force: Check every node, top-down. If the subtree at a node is a BST, its size is the answer for that part
	// (anything inside it is smaller). Otherwise, try its left and right subtrees.
	// The problem: The BST check walks the whole subtree, and is repeated for every node inside it.
	// T.C: O(n^2)
	// S.C: O(h)
	public int LargestBst_BruteForce(TreeNode root)
	{
		if (root == null) return 0;
		if (IsBST(root, long.MinValue, long.MaxValue)) return Size(root);
		return Math.Max(LargestBst_BruteForce(root.left), LargestBst_BruteForce(root.right));
	}

	private bool IsBST(TreeNode root, long min, long max)
	{
		if (root == null) return true;
		if (root.val <= min || root.val >= max) return false;
		return IsBST(root.left, min, root.val) && IsBST(root.right, root.val, max);
	}

	private int Size(TreeNode root)
	{
		if (root == null) return 0;
		return 1 + Size(root.left) + Size(root.right);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Go BOTTOM-UP (postorder), so every node is checked once, using what its children found.
	// A node's subtree is a BST when:
	//   1. its left subtree is a BST,  2. its right subtree is a BST,
	//   3. node > the BIGGEST value on the left,  and  node < the SMALLEST value on the right.
	// So each call returns 4 things about its subtree: (isBST, size of the largest BST inside, min value, max value).
	// ● Empty subtree (null): (true, 0, min = int.MaxValue, max = int.MinValue). These "upside-down" values make rule 3
	//   always pass for a missing child: any node > int.MinValue, and any node < int.MaxValue.
	// ● BST here: size = left.size + right.size + 1, min = Math.Min(node, left.min), max = Math.Max(node, right.max).
	//   (Math.Min / Math.Max handle the missing-child case: e.g., no left child means min = the node itself.)
	// ● Not a BST: no tree above can be a BST either. Return (false, Math.Max(left.size, right.size), any min/max).
	//   The isBST = false is enough to stop the parents. (The C++ code also set min = INT_MIN, max = INT_MAX here as a
	//   second guard, so that rule 3 also fails.)
	// e.g., Example 1: 1 -> (true, 1, 1, 1). 3 -> (true, 1, 3, 3). 2: 2 > 1 and 2 < 3 -> (true, 3, 1, 3).
	//       4 -> (true, 1, 4, 4). 5: 5 > 3 but 5 < 4 is false -> (false, max(3, 1) = 3). Answer 3.
	// T.C: O(n)
	// S.C: O(h);   recursion stack
	public int LargestBst(TreeNode root)
	{
		return Check(root).size;
	}

	private (bool isBST, int size, int min, int max) Check(TreeNode root)
	{
		if (root == null) return (true, 0, int.MaxValue, int.MinValue);

		var left = Check(root.left);
		var right = Check(root.right);

		if (left.isBST && right.isBST && root.val > left.max && root.val < right.min)
		{
			return (true, left.size + right.size + 1, Math.Min(root.val, left.min), Math.Max(root.val, right.max));
		}

		return (false, Math.Max(left.size, right.size), int.MinValue, int.MaxValue);
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(5);
		root.left = new TreeNode(2, new TreeNode(1), new TreeNode(3));
		root.right = new TreeNode(4);

		Console.WriteLine(new Solution().LargestBst_BruteForce(root));   // o/p: 3
		Console.WriteLine(new Solution().LargestBst(root));              // o/p: 3
	}
}

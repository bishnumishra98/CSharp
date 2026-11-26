// Leetcode: 1008. Construct Binary Search Tree from Preorder Traversal   --->   Given an array of the PREORDER traversal
// of a BST (all values unique), construct the tree and return its root.

// Example 1:
// Input: preorder = [8,5,1,7,10,12]
//          8
//         / \
//        5   10
//       / \    \
//      1   7    12
// Output: [8,5,10,1,7,null,12]

// Example 2:
// Input: preorder = [4,2,1,3,6,5,7]
//            4
//          /   \
//         2     6
//        / \   / \
//       1   3 5   7
// Output: [4,2,6,1,3,5,7]

// Why does preorder ALONE give one BST, when a normal binary tree also needed the inorder
// ('14. Binary_Tree/20_ConstructBinaryTreeFromPreorderAndInorderTraversal.cs') ? For a BST the inorder is just the
// sorted values, so we already know it.


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
	// Brute force (from the C++ code): The first value is the root. Of the rest, the values SMALLER than the root form
	// the left subtree's preorder, the BIGGER ones form the right subtree's preorder. Split them into 2 new lists and
	// build each side the same way.
	// e.g., [4 | 2 1 3 6 5 7]: root 4, left [2 1 3], right [6 5 7].
	// The problem: Every level copies all the values below it into new lists.
	// T.C: O(n^2) in the worst case (a skewed tree: n + (n-1) + ... + 1), O(n log n) for a balanced one
	// S.C: O(n^2) in the worst case for the lists (they all exist at once along one path), O(n) for a balanced tree
	// Note: The C++ comment said T.C: O(N). The copying makes it O(N^2) in the worst case.
	public TreeNode BstFromPreorder_BruteForce(List<int> preorder)
	{
		if (preorder.Count == 0) return null;

		TreeNode root = new TreeNode(preorder[0]);
		List<int> left = new List<int>(), right = new List<int>();

		for (int i = 1; i < preorder.Count; i++)
		{
			if (preorder[i] < root.val) left.Add(preorder[i]);
			else right.Add(preorder[i]);
		}

		root.left = BstFromPreorder_BruteForce(left);
		root.right = BstFromPreorder_BruteForce(right);
		return root;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Read the preorder ONCE, left to right, with one shared index i, and an UPPER BOUND.
	// Build(bound) builds the subtree made of the next values that are smaller than 'bound':
	// 1. If we are out of values, or the next value is > bound, it does not belong to this subtree: return null.
	// 2. Otherwise it is this subtree's root (preorder: the node comes first). Take it (i++).
	// 3. Its left subtree = the next values smaller than the root: Build(root.val).
	//    Its right subtree = the next values bigger than the root, but still under the old bound: Build(bound).
	// (This is the range idea of '08_ValidateBinarySearchTree.cs'. The lower bound is not needed: preorder guarantees
	//  that a value reaching a right subtree is already bigger than that subtree's root.)
	// e.g., [8,5,1,7,10,12]: Build(inf) takes 8. Left = Build(8): takes 5. Left = Build(5): takes 1.
	//       1's left = Build(1): 7 > 1, null. 1's right = Build(5): 7 > 5, null.
	//       Back at 5: right = Build(8): takes 7. 7's left = Build(7): 10 > 7, null. 7's right = Build(8): 10 > 8, null.
	//       Back at 8: right = Build(inf): takes 10. 10's left = Build(10): 12 > 10, null. 10's right = Build(inf): 12.
	// T.C: O(n);   every value is taken once (each null check is O(1), and there are at most 2 per node)
	// S.C: O(h);   recursion stack
	public TreeNode BstFromPreorder(int[] preorder)
	{
		int i = 0;
		return Build(preorder, ref i, int.MaxValue);
	}

	private TreeNode Build(int[] preorder, ref int i, int bound)
	{
		if (i == preorder.Length || preorder[i] > bound) return null;

		TreeNode root = new TreeNode(preorder[i]);
		i++;

		root.left = Build(preorder, ref i, root.val);
		root.right = Build(preorder, ref i, bound);
		return root;
	}
}


class Program
{
	// Level order as text, only to print the tree (see '14. Binary_Tree/02_BinaryTreeTraversals.cs').
	static string LevelOrder(TreeNode root)
	{
		List<string> levels = new List<string>();
		Queue<TreeNode> q = new Queue<TreeNode>();
		q.Enqueue(root);

		while (q.Count > 0)
		{
			int size = q.Count;
			List<int> level = new List<int>();
			for (int i = 0; i < size; i++)
			{
				TreeNode node = q.Dequeue();
				level.Add(node.val);
				if (node.left != null) q.Enqueue(node.left);
				if (node.right != null) q.Enqueue(node.right);
			}
			levels.Add(string.Join(" ", level));
		}

		return string.Join(" | ", levels);
	}

	static void Main()
	{
		int[] preorder = { 8, 5, 1, 7, 10, 12 };

		Console.WriteLine(LevelOrder(new Solution().BstFromPreorder_BruteForce(new List<int>(preorder))));   // o/p: 8 | 5 10 | 1 7 12
		Console.WriteLine(LevelOrder(new Solution().BstFromPreorder(preorder)));                             // o/p: 8 | 5 10 | 1 7 12
	}
}

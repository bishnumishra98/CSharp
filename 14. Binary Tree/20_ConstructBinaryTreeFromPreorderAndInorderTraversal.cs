// Leetcode: 105. Construct Binary Tree from Preorder and Inorder Traversal   --->   Given two integer arrays preorder and
// inorder, where preorder is the preorder traversal of a binary tree and inorder is the inorder traversal of the same
// tree, construct and return the binary tree. All values are unique.

// Example 1:
// Input: preorder = [3,9,20,15,7], inorder = [9,3,15,20,7]
//          3
//        /   \
//       9     20
//            /  \
//           15   7
// Output: [3,9,20,null,null,15,7]

// Example 2:
// Input: preorder = [-1], inorder = [-1]
// Output: [-1]

// ● Good to know: Inorder + Preorder, or Inorder + Postorder, give exactly ONE tree. But Preorder + Postorder can
//   match many trees (e.g., a root with one child: pre and post do not tell if the child is left or right).


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
	// Algorithm:
	// 1. Preorder is Node, Left, Right, so its FIRST value is the root.
	// 2. Find the root in inorder (Left, Node, Right): everything on its LEFT is the left subtree, everything on its
	//    RIGHT is the right subtree. Let numsLeft = how many values are in the left subtree.
	// 3. In preorder, right after the root come the numsLeft values of the left subtree, then the right subtree.
	// 4. Build the left and right subtrees the same way, on these smaller ranges (recursion).
	// e.g., pre = [3 | 9 | 20 15 7], in = [9 | 3 | 15 20 7]: root 3, inorder index 1, numsLeft = 1.
	//       Left: pre [9], in [9].   Right: pre [20, 15, 7], in [15, 20, 7] -> root 20, left [15], right [7].
	// To find the root in inorder in O(1), first store {value -> index in inorder} in a Dictionary.
	// (Searching the inorder array every time instead costs O(n) per node: O(n^2) in total.)
	// T.C: O(n)
	// S.C: O(n);   the dictionary + O(h) recursion stack
	public TreeNode BuildTree(int[] preorder, int[] inorder)
	{
		Dictionary<int, int> inIndex = new Dictionary<int, int>();   // {value -> its index in inorder}
		for (int i = 0; i < inorder.Length; i++) inIndex[inorder[i]] = i;

		return Build(preorder, 0, preorder.Length - 1, 0, inorder.Length - 1, inIndex);
	}

	private TreeNode Build(int[] preorder, int preStart, int preEnd, int inStart, int inEnd, Dictionary<int, int> inIndex)
	{
		if (preStart > preEnd || inStart > inEnd) return null;   // empty range: no node

		TreeNode root = new TreeNode(preorder[preStart]);

		int inRoot = inIndex[root.val];      // where the root is in inorder
		int numsLeft = inRoot - inStart;     // size of the left subtree

		root.left = Build(preorder, preStart + 1, preStart + numsLeft, inStart, inRoot - 1, inIndex);
		root.right = Build(preorder, preStart + numsLeft + 1, preEnd, inRoot + 1, inEnd, inIndex);

		return root;
	}
}


class Program
{
	// Level order as text, only to print the answer (see '02_BinaryTreeTraversals.cs').
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
		int[] preorder = { 3, 9, 20, 15, 7 };
		int[] inorder = { 9, 3, 15, 20, 7 };

		TreeNode root = new Solution().BuildTree(preorder, inorder);
		Console.WriteLine(LevelOrder(root));   // o/p: 3 | 9 20 | 15 7
	}
}

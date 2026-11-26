// Leetcode: 106. Construct Binary Tree from Inorder and Postorder Traversal   --->   Given two integer arrays inorder
// and postorder, where inorder is the inorder traversal of a binary tree and postorder is the postorder traversal of the
// same tree, construct and return the binary tree. All values are unique.

// Example 1:
// Input: inorder = [9,3,15,20,7], postorder = [9,15,7,20,3]
//          3
//        /   \
//       9     20
//            /  \
//           15   7
// Output: [3,9,20,null,null,15,7]

// Example 2:
// Input: inorder = [-1], postorder = [-1]
// Output: [-1]

// First understand '20_ConstructBinaryTreeFromPreorderAndInorderTraversal.cs'. Only the place of the root changes.


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
	// 1. Postorder is Left, Right, Node, so its LAST value is the root.
	// 2. Find the root in inorder: the left side is the left subtree (numsLeft values), the right side is the right
	//    subtree.
	// 3. In postorder, the FIRST numsLeft values are the left subtree, then come the right subtree, then the root.
	// 4. Build both subtrees the same way.
	// e.g., in = [9 | 3 | 15 20 7], post = [9 | 15 7 20 | 3]: root 3, numsLeft = 1.
	//       Left: in [9], post [9].   Right: in [15, 20, 7], post [15, 7, 20] -> root 20, left [15], right [7].
	// T.C: O(n)
	// S.C: O(n);   the dictionary + O(h) recursion stack
	public TreeNode BuildTree(int[] inorder, int[] postorder)
	{
		Dictionary<int, int> inIndex = new Dictionary<int, int>();   // {value -> its index in inorder}
		for (int i = 0; i < inorder.Length; i++) inIndex[inorder[i]] = i;

		return Build(postorder, 0, postorder.Length - 1, 0, inorder.Length - 1, inIndex);
	}

	private TreeNode Build(int[] postorder, int postStart, int postEnd, int inStart, int inEnd, Dictionary<int, int> inIndex)
	{
		if (postStart > postEnd || inStart > inEnd) return null;

		TreeNode root = new TreeNode(postorder[postEnd]);   // last value = root

		int inRoot = inIndex[root.val];
		int numsLeft = inRoot - inStart;

		root.left = Build(postorder, postStart, postStart + numsLeft - 1, inStart, inRoot - 1, inIndex);
		root.right = Build(postorder, postStart + numsLeft, postEnd - 1, inRoot + 1, inEnd, inIndex);

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
		int[] inorder = { 9, 3, 15, 20, 7 };
		int[] postorder = { 9, 15, 7, 20, 3 };

		TreeNode root = new Solution().BuildTree(inorder, postorder);
		Console.WriteLine(LevelOrder(root));   // o/p: 3 | 9 20 | 15 7
	}
}

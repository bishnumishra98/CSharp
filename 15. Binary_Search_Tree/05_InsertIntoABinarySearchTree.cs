// Leetcode: 701. Insert into a Binary Search Tree   --->   You are given the root of a BST and a value to insert into the
// tree. Return the root of the BST after the insertion. The value is not in the tree yet.
// There can be several valid answers. Any tree that is still a BST is accepted.

// Example 1:
// Input: root = [4,2,7,1,3], val = 5
//          4                      4
//         / \                   /   \
//        2   7      --->       2     7
//       / \                   / \   /
//      1   3                 1   3 5
// Output: [4,2,7,1,3,5]

// Example 2:
// Input: root = [40,20,60,10,30,50,70], val = 25
// Output: [40,20,60,10,30,50,70,null,null,25]   (25 becomes the left child of 30)

// A recursive Insert is in '01_BSTIntroduction.cs'. Here is the iterative one, with O(1) space.


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
	// Algorithm: A new value ALWAYS goes in as a new LEAF (no need to move any existing node). Search for the value as in
	// '02_SearchInABinarySearchTree.cs'. The search ends at a node whose next step is null: that empty spot is exactly
	// where the value belongs. Put it there.
	// e.g., val = 5: 5 > 4, go right -> 5 < 7, but 7 has no left child. So 7.left = 5.
	// T.C: O(h)
	// S.C: O(1)
	public TreeNode InsertIntoBST(TreeNode root, int val)
	{
		if (root == null) return new TreeNode(val);

		TreeNode curr = root;

		while (true)
		{
			if (val > curr.val)
			{
				if (curr.right == null)
				{
					curr.right = new TreeNode(val);
					break;
				}
				curr = curr.right;
			}
			else
			{
				if (curr.left == null)
				{
					curr.left = new TreeNode(val);
					break;
				}
				curr = curr.left;
			}
		}

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
		TreeNode root = new TreeNode(4);
		root.left = new TreeNode(2, new TreeNode(1), new TreeNode(3));
		root.right = new TreeNode(7);

		root = new Solution().InsertIntoBST(root, 5);
		Console.WriteLine(LevelOrder(root));   // o/p: 4 | 2 7 | 1 3 5
	}
}

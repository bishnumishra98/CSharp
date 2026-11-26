// Leetcode: 108. Convert Sorted Array to Binary Search Tree   --->   Given an integer array sorted in increasing order
// (this is the INORDER of the BST), convert it to a height-balanced BST.
// Height-balanced = for every node, the heights of its two subtrees differ by at most 1.

// Example 1:
// Input: nums = [1,2,3,4,5,6,7]
//             4
//           /   \
//          2     6
//         / \   / \
//        1   3 5   7
// Output: [4,2,6,1,3,5,7]

// Example 2:
// Input: nums = [1,3]
// Output: [1,null,3] or [3,1]   (both are balanced BSTs)

// The problem: Inserting the values one by one in sorted order (like '01_BSTIntroduction.cs') puts each value to the
// right of the last one: a skewed tree of height n. We want height log(n).


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
	// Algorithm: Make the MIDDLE element the root. Then half of the values are on its left (all smaller) and half on its
	// right (all bigger), so the tree is balanced at the root, and it is a BST at the root.
	// Do the same for the left half (it becomes the left subtree) and the right half (the right subtree).
	// e.g., [1 2 3 | 4 | 5 6 7]: root 4. Left half [1 | 2 | 3]: root 2. Right half [5 | 6 | 7]: root 6.
	// T.C: O(n);   every element becomes a node once
	// S.C: O(log n);   recursion stack (the tree we build is balanced, so its height is log n)
	// Note: The C++ comment said S.C: O(H) with "H = NlogN on average". The height is log(N), not N log N.
	public TreeNode SortedArrayToBST(int[] nums)
	{
		return Build(nums, 0, nums.Length - 1);
	}

	private TreeNode Build(int[] nums, int start, int end)
	{
		if (start > end) return null;   // empty range

		int mid = start + (end - start) / 2;   // same as (start + end) / 2, but can not overflow
		TreeNode root = new TreeNode(nums[mid]);

		root.left = Build(nums, start, mid - 1);
		root.right = Build(nums, mid + 1, end);

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
		TreeNode root = new Solution().SortedArrayToBST(new int[] { 1, 2, 3, 4, 5, 6, 7 });
		Console.WriteLine(LevelOrder(root));   // o/p: 4 | 2 6 | 1 3 5 7
	}
}

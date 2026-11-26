// GFG: Kth Ancestor in a Tree   --->   Given a binary tree, a node value, and a positive integer k, return the k-th
// ancestor of that node (the node k steps ABOVE it). If there is no such ancestor, return -1.
// All node values are distinct, and the node is in the tree.

// Example 1:
// Input: k = 2, node = 4
//          1
//         / \
//        2   3
//       / \
//      4   5
// Output: 1   (1 step up from 4 is 2, 2 steps up is 1)

// Example 2:
// Input: k = 1, node = 3
//          1
//        /   \
//       2     3
// Output: 1

// Problem link: https://www.geeksforgeeks.org/problems/kth-ancestor-in-a-tree/1


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
	// Approach 1: Find the path root -> node (like '17_PathFromPToQ.cs'). The node is the last value in the path, its
	// parent the one before it, and so on. So the k-th ancestor is at index (path.Count - 1 - k), if that is >= 0.
	// e.g., path to 4 = [1, 2, 4]. k = 2: index 3 - 1 - 2 = 0 -> 1.
	// T.C: O(n)
	// S.C: O(h);   the path and the recursion stack
	public int KthAncestor_Path(TreeNode root, int k, int node)
	{
		List<int> path = new List<int>();
		GetPath(root, node, path);

		int index = path.Count - 1 - k;
		return index >= 0 ? path[index] : -1;
	}

	private bool GetPath(TreeNode root, int target, List<int> path)
	{
		if (root == null) return false;

		path.Add(root.val);
		if (root.val == target) return true;
		if (GetPath(root.left, target, path) || GetPath(root.right, target, path)) return true;

		path.RemoveAt(path.Count - 1);
		return false;
	}

// ------------------------------------------------------------------------------

	// Approach 2 (no path list): Count k down while coming BACK UP from the node.
	// 1. Null: return -1 (not found here). The node itself: return its value (the "found" signal).
	// 2. If the left or right subtree found the node, then THIS node is one more step up: k--.
	//    I.  If k is now 0, this node is the k-th ancestor: return its value.
	//    II. Otherwise, pass the found signal up unchanged.
	// 3. In the end, if the answer is -1 or the node itself, the tree was not tall enough: return -1.
	// Why does the answer survive above the ancestor ? Above it, k goes to -1, -2, ... (never 0 again), so every level
	// just passes the same value up.
	// e.g., k = 2, node = 4: at 4 return 4. At 2: k = 1, pass 4 up. At 1: k = 0, return 1. Answer: 1.
	// T.C: O(n)
	// S.C: O(h);   recursion stack
	// Note: -1 is used as the "not found" signal, so this assumes no node has the value -1.
	public int KthAncestor(TreeNode root, int k, int node)
	{
		int result = Find(root, ref k, node);
		return (result == -1 || result == node) ? -1 : result;
	}

	private int Find(TreeNode root, ref int k, int node)
	{
		if (root == null) return -1;
		if (root.val == node) return node;

		int left = Find(root.left, ref k, node);
		int right = Find(root.right, ref k, node);

		if (left != -1 || right != -1)   // the node is below this one
		{
			k--;
			if (k == 0) return root.val;     // this is the k-th ancestor
			return left != -1 ? left : right;
		}

		return -1;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(4), new TreeNode(5));
		root.right = new TreeNode(3);

		Console.WriteLine(new Solution().KthAncestor_Path(root, 2, 4));   // o/p: 1
		Console.WriteLine(new Solution().KthAncestor(root, 2, 4));        // o/p: 1
	}
}

// Path from Node p to Node q   --->   Given a binary tree and two nodes p and q in it, return the values of the nodes on
// the path from p to q (both included).
// All node values are unique, and p and q are both in the tree.

// Example 1:
// Input: p = 4, q = 3
//         1
//        / \
//       2   3
//      / \
//     4   5
// Output: [4, 2, 1, 3]

// Example 2:
// Input: p = 7, q = 4
//         3
//        / \
//       5   1
//      / \
//     6   2
//        / \
//       7   4
// Output: [7, 2, 4]

// First understand '16_LowestCommonAncestorOfABinaryTree.cs'. The path from p to q always goes UP from p to their LCA,
// and then DOWN from the LCA to q.


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
	// Root-to-target path (DFS + backtracking, like '15_PathSumII.cs'): Add the node to 'path'. If it is the target, or
	// the target is found in the left or right subtree, return true and keep the path. If not, remove the node again
	// (backtrack) and return false.
	private bool GetPath(TreeNode root, TreeNode target, List<int> path)
	{
		if (root == null) return false;

		path.Add(root.val);

		if (root == target) return true;
		if (GetPath(root.left, target, path) || GetPath(root.right, target, path)) return true;

		path.RemoveAt(path.Count - 1);   // target is not below this node
		return false;
	}

	// Algorithm:
	// 1. Find pathP = root -> p, and pathQ = root -> q.
	// 2. Both paths start the same (from the root). Walk them together while they match. The LAST matching node is the
	//    LCA; let i = the first index where they differ.
	//    e.g., Example 1: pathP = [1, 2, 4], pathQ = [1, 3]. They match at index 0 only, so i = 1 and the LCA = 1.
	// 3. Answer = pathP from its end back to index i (p going up, LCA not included) + the LCA + pathQ from index i to its
	//    end (going down to q).
	//    e.g., [4, 2] + [1] + [3] = [4, 2, 1, 3]
	// This also works when p is above q: then pathP is the start of pathQ, the first loop adds nothing, and the LCA is p.
	// T.C: O(n);   2 DFS walks + walking the paths
	// S.C: O(h);   the paths and the recursion stack, h = n for a skewed tree
	public List<int> PathFromPToQ(TreeNode root, TreeNode p, TreeNode q)
	{
		List<int> pathP = new List<int>(), pathQ = new List<int>(), result = new List<int>();

		GetPath(root, p, pathP);
		GetPath(root, q, pathQ);

		int i = 0;
		while (i < pathP.Count && i < pathQ.Count && pathP[i] == pathQ[i]) i++;

		for (int j = pathP.Count - 1; j >= i; j--) result.Add(pathP[j]);   // p up to just below the LCA
		result.Add(pathP[i - 1]);                                          // the LCA
		for (int j = i; j < pathQ.Count; j++) result.Add(pathQ[j]);        // down from the LCA to q

		return result;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(4), new TreeNode(5));
		root.right = new TreeNode(3);

		TreeNode p = root.left.left;   // 4
		TreeNode q = root.right;       // 3

		Console.WriteLine(string.Join(" ", new Solution().PathFromPToQ(root, p, q)));   // o/p: 4 2 1 3
	}
}

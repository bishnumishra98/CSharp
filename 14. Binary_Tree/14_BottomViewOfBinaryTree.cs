// GFG: Bottom View of Binary Tree   --->   Given the root of a binary tree, return its bottom view: the nodes you can see
// when you look at the tree from the BOTTOM, from the leftmost to the rightmost.
// If several nodes are the bottom-most on the same vertical line, take the one that comes LATER in level order.

// Example 1:
// Input: root = [1, 2, 3, 4, 5, N, 6]
//          1
//         / \
//        2   3
//       / \   \
//      4   5   6
// Output: [4, 2, 5, 3, 6]
// Explanation: 1 and 5 are on the same vertical line (column 0). 5 is lower, so 5 is seen.

// Example 2:
// Input: root = [20, 8, 22, 5, 3, 4, 25, N, N, 10, 14, N, N, 28, N]
//          _ 20 _
//         /      \
//        8       22
//       / \     /  \
//      5   3   4   25
//         / \      /
//        10 14    28
// Output: [5, 10, 4, 28, 25]
// Explanation: 3 and 4 are both at the bottom of column 0 (same level), and 4 comes later in level order. Then 14 and
//              28 are both at column 1 on the last level, 28 comes later.

// Problem link: https://www.geeksforgeeks.org/problems/bottom-view-of-binary-tree/1

// First understand '13_TopViewOfBinaryTree.cs'. This is the same code with ONE change.


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
	// Algorithm: Same BFS with columns as the top view. Top view kept the FIRST node of each column. Bottom view keeps
	// the LAST one: so always overwrite the column's value. BFS goes top to bottom (and left to right in a level), so
	// the last node written is the bottom-most one, and on a tie, the later one in level order (as the problem asks).
	// T.C: O(n);   (the C++ map made it O(n log n), see the note in '13_TopViewOfBinaryTree.cs')
	// S.C: O(n)
	public List<int> BottomView(TreeNode root)
	{
		List<int> ans = new List<int>();
		if (root == null) return ans;

		Dictionary<int, int> bottom = new Dictionary<int, int>();   // {column -> lowest value seen so far}
		Queue<(TreeNode node, int col)> q = new Queue<(TreeNode node, int col)>();
		q.Enqueue((root, 0));
		int minCol = 0, maxCol = 0;

		while (q.Count > 0)
		{
			var (node, col) = q.Dequeue();

			bottom[col] = node.val;   // always overwrite (the only change from top view)
			minCol = Math.Min(minCol, col);
			maxCol = Math.Max(maxCol, col);

			if (node.left != null) q.Enqueue((node.left, col - 1));
			if (node.right != null) q.Enqueue((node.right, col + 1));
		}

		for (int c = minCol; c <= maxCol; c++) ans.Add(bottom[c]);
		return ans;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(4), new TreeNode(5));
		root.right = new TreeNode(3, null, new TreeNode(6));

		Console.WriteLine(string.Join(" ", new Solution().BottomView(root)));   // o/p: 4 2 5 3 6
	}
}

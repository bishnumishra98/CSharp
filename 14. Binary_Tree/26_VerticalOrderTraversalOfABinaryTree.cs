// Leetcode: 987. Vertical Order Traversal of a Binary Tree   --->   Given the root of a binary tree, return its vertical
// order traversal.
// For a node at (row, col), its left child is at (row + 1, col - 1) and its right child at (row + 1, col + 1). The root
// is at (0, 0).
// The vertical order traversal = for every column from the leftmost to the rightmost, the list of its nodes from top
// to bottom. If several nodes are at the SAME row and column, sort them by value.

// Example 1:
// Input: root = [3,9,20,null,null,15,7]
//          3
//         / \
//        9   20
//           /  \
//          15   7
// Output: [[9],[3,15],[20],[7]]
// Explanation: col -1: 9 | col 0: 3, 15 | col 1: 20 | col 2: 7

// Example 2:
// Input: root = [1,2,3,4,6,5,7]
//            1
//          /   \
//         2     3
//        / \   / \
//       4   6 5   7
// Output: [[4],[2],[1,5,6],[3],[7]]
// Explanation: 6 and 5 are both at (row 2, col 0), so they are sorted by value: 5 before 6.

// First understand the columns in '13_TopViewOfBinaryTree.cs'. Here we keep EVERY node of a column, not only the top.


using System;
using System.Collections.Generic;
using System.Linq;

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
	// 1. Visit every node (BFS here) and note its (col, row, val).
	// 2. Sort all notes by col, then by row, then by val. That is exactly the order the problem asks for:
	//    left to right columns, top to bottom inside a column, and smaller value first on a tie.
	// 3. Cut the sorted list into groups of the same column. Each group is one answer list.
	// e.g., Example 2 sorted: (-2,2,4) (-1,1,2) (0,0,1) (0,2,5) (0,2,6) (1,1,3) (2,2,7)
	//       groups: [4] [2] [1,5,6] [3] [7]
	// T.C: O(n log n);   for the sort
	// S.C: O(n)
	// Note: The C++ code used map<col, map<row, multiset<val>>>: nested sorted containers that sort while inserting.
	//       C# has no multiset (a sorted set that allows duplicates), so collecting everything and sorting once is the
	//       simplest way. Same T.C.
	public IList<IList<int>> VerticalTraversal(TreeNode root)
	{
		List<(int col, int row, int val)> nodes = new List<(int col, int row, int val)>();
		Queue<(TreeNode node, int col, int row)> q = new Queue<(TreeNode node, int col, int row)>();
		q.Enqueue((root, 0, 0));

		while (q.Count > 0)
		{
			var (node, col, row) = q.Dequeue();
			nodes.Add((col, row, node.val));

			if (node.left != null) q.Enqueue((node.left, col - 1, row + 1));
			if (node.right != null) q.Enqueue((node.right, col + 1, row + 1));
		}

		// GroupBy keeps the sorted order, both between the groups and inside each group.
		return nodes.OrderBy(t => t.col)
		            .ThenBy(t => t.row)
		            .ThenBy(t => t.val)
		            .GroupBy(t => t.col)
		            .Select(g => (IList<int>)g.Select(t => t.val).ToList())
		            .ToList();
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(4), new TreeNode(6));
		root.right = new TreeNode(3, new TreeNode(5), new TreeNode(7));

		var columns = new Solution().VerticalTraversal(root).Select(col => string.Join(" ", col));
		Console.WriteLine(string.Join(" | ", columns));   // o/p: 4 | 2 | 1 5 6 | 3 | 7
	}
}

// Binary Tree Traversals   --->   Traversal = visiting every node of the tree once, in some order.
// A linked list has only one order (front to back), but a tree has many. 4 classic ones:
//
//               ______ 10 _______
//             /                   \
//        ___ 20 ___                30
//       /          \                 \
//      40          50                60
//     /  \        /  \              /  \
//    70  80      90  100          110  120
//                      \          /     /
//                      130      140   150
//
// I.   Preorder:  Node, Left, Right (NLR).   Visit the node FIRST, then its subtrees.
//      10 20 40 70 80 50 90 100 130 30 60 110 140 120 150
// II.  Inorder:   Left, Node, Right (LNR).   Visit the node in the MIDDLE. (For a BST this gives sorted order.)
//      70 40 80 20 90 50 100 130 10 30 140 110 60 150 120
// III. Postorder: Left, Right, Node (LRN).   Visit the node LAST, after both subtrees.
//      70 80 40 90 130 100 50 20 140 110 150 120 60 30 10
// IV.  Level order (Breadth First Search / BFS): level by level, left to right.
//      10 | 20 30 | 40 50 60 | 70 80 90 100 110 120 | 130 140 150
//
// Trick to remember: Pre / In / Post tells WHERE the Node goes. Left always comes before Right.
// The first 3 are Depth First Search (DFS): go as deep as possible first, then come back.


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
	// Same as '01_BinaryTreeIntroduction.cs': build a tree from its preorder, -1 = no node.
	public TreeNode BuildTree(int[] nodes, ref int i)
	{
		if (nodes[i] == -1) return null;
		TreeNode root = new TreeNode(nodes[i]);
		i++;
		root.left = BuildTree(nodes, ref i);
		i++;
		root.right = BuildTree(nodes, ref i);
		return root;
	}

// ------------------------------------------------------------------------------

	// I. Preorder (N L R): Add the node, then go left, then go right. Stop at null.
	// T.C: O(n);   every node is visited once
	// S.C: O(h);   recursion stack, h = height (h = n for a skewed tree)
	public List<int> PreOrder(TreeNode root)
	{
		List<int> ans = new List<int>();
		PreOrder(root, ans);
		return ans;
	}

	private void PreOrder(TreeNode root, List<int> ans)
	{
		if (root == null) return;   // base case
		ans.Add(root.val);          // N
		PreOrder(root.left, ans);   // L
		PreOrder(root.right, ans);  // R
	}

// ------------------------------------------------------------------------------

	// II. Inorder (L N R): Only the line that adds the node moves to the middle.
	// T.C: O(n)
	// S.C: O(h)
	public List<int> InOrder(TreeNode root)
	{
		List<int> ans = new List<int>();
		InOrder(root, ans);
		return ans;
	}

	private void InOrder(TreeNode root, List<int> ans)
	{
		if (root == null) return;
		InOrder(root.left, ans);    // L
		ans.Add(root.val);          // N
		InOrder(root.right, ans);   // R
	}

// ------------------------------------------------------------------------------

	// III. Postorder (L R N): The node is added last.
	// T.C: O(n)
	// S.C: O(h)
	public List<int> PostOrder(TreeNode root)
	{
		List<int> ans = new List<int>();
		PostOrder(root, ans);
		return ans;
	}

	private void PostOrder(TreeNode root, List<int> ans)
	{
		if (root == null) return;
		PostOrder(root.left, ans);   // L
		PostOrder(root.right, ans);  // R
		ans.Add(root.val);           // N
	}

// ------------------------------------------------------------------------------

	// IV. Level order (Leetcode: 102. Binary Tree Level Order Traversal): Use a QUEUE (first in, first out).
	// 1. Put the root in the queue.
	// 2. While the queue is not empty: 'size' = how many nodes are in the queue right now = all nodes of ONE level.
	//    Take out exactly 'size' nodes. For each one: add its value to this level, and put its children (if any) in the
	//    queue. The children go behind, so they are taken out only in the next round (the next level).
	// e.g., queue [10] -> level [10], queue [20, 30] -> level [20, 30], queue [40, 50, 60] -> ...
	// Note: The C++ code pushed a NULL into the queue as a "new line" marker after every level. Counting 'size' does the
	//       same job without the marker, and is the common way in C#.
	// T.C: O(n)
	// S.C: O(w);   w = max width (nodes in the widest level). Up to n / 2 for the last level of a full tree, so O(n).
	public List<List<int>> LevelOrder(TreeNode root)
	{
		List<List<int>> ans = new List<List<int>>();
		if (root == null) return ans;

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

			ans.Add(level);
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nodes = { 10, 20, 40, 70, -1, -1, 80, -1, -1, 50, 90, -1, -1, 100, -1, 130, -1, -1,
		                30, -1, 60, 110, 140, -1, -1, -1, 120, 150, -1, -1, -1 };
		int i = 0;
		Solution sol = new Solution();
		TreeNode root = sol.BuildTree(nodes, ref i);

		Console.WriteLine(string.Join(" ", sol.PreOrder(root)));      // o/p: 10 20 40 70 80 50 90 100 130 30 60 110 140 120 150
		Console.WriteLine(string.Join(" ", sol.InOrder(root)));       // o/p: 70 40 80 20 90 50 100 130 10 30 140 110 60 150 120
		Console.WriteLine(string.Join(" ", sol.PostOrder(root)));     // o/p: 70 80 40 90 130 100 50 20 140 110 150 120 60 30 10

		var levels = sol.LevelOrder(root).Select(level => string.Join(" ", level));
		Console.WriteLine(string.Join(" | ", levels));                // o/p: 10 | 20 30 | 40 50 60 | 70 80 90 100 110 120 | 130 140 150
	}
}

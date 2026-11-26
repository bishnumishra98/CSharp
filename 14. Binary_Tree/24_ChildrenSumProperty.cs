// Children Sum Property   --->   Given the root of a binary tree, change the tree so that every node's value equals the
// sum of its children's values (leaves have no children, so they can stay as they are).
// You can only INCREASE values (by any amount), never decrease them. Any valid tree is accepted.

// Example 1:
// Input:
//          _ 40 _
//         /      \
//        10      20
//       /  \    /  \
//      2    5  30  40
// Output (one possible answer):
//          _ 150 _
//         /       \
//        80       70
//       /  \     /  \
//      40   40  30  40
// Another valid answer:
//          _ 115 _
//         /       \
//        45       70
//       /  \     /  \
//      40   5   30  40

// The problem: We want to fix a parent by making it the sum of its children. But if the parent is ALREADY bigger than
// its children's sum, we can not decrease it. So we must make the children bigger first.


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
	// Algorithm: Fix things on the way DOWN, and again on the way back UP.
	// Going down (at each node, before the recursion):
	//   I.  childSum >= node: just raise the node to childSum (a raise is allowed).
	//   II. childSum <  node: raise both children to the node's value. Now each child alone is >= the node, so later
	//       their sum will be >= the node too.
	// Recurse into both children (they push the same rule further down).
	// Coming back up: the children may have grown, so set node = left + right (for non-leaf nodes). This never
	// decreases the node, because on the way down we made sure the children are big enough.
	// e.g., Example 1: 40 has children 10 + 20 = 30 < 40, so both become 40.
	//       Left 40: children 2 + 5 = 7 < 40, so both become 40. Back up: 40 + 40 = 80.
	//       Right 40: children 30 + 40 = 70 >= 40, so it becomes 70. Back up: 70.
	//       Root back up: 80 + 70 = 150.
	// T.C: O(n)
	// S.C: O(h);   recursion stack, h = n for a skewed tree
	public void ChangeTree(TreeNode root)
	{
		if (root == null) return;

		int childSum = 0;
		if (root.left != null) childSum += root.left.val;
		if (root.right != null) childSum += root.right.val;

		if (childSum >= root.val)
		{
			root.val = childSum;
		}
		else
		{
			if (root.left != null) root.left.val = root.val;
			if (root.right != null) root.right.val = root.val;
		}

		ChangeTree(root.left);
		ChangeTree(root.right);

		// Back up: the children may have changed, so the node becomes their sum (leaves keep their value).
		int total = 0;
		if (root.left != null) total += root.left.val;
		if (root.right != null) total += root.right.val;
		if (root.left != null || root.right != null) root.val = total;
	}
}


class Program
{
	// Level order as text, only to print the tree (see '02_BinaryTreeTraversals.cs').
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
		TreeNode root = new TreeNode(40);
		root.left = new TreeNode(10, new TreeNode(2), new TreeNode(5));
		root.right = new TreeNode(20, new TreeNode(30), new TreeNode(40));

		new Solution().ChangeTree(root);
		Console.WriteLine(LevelOrder(root));   // o/p: 150 | 80 70 | 40 40 30 40
	}
}

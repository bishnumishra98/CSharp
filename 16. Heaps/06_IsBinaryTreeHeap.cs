// GFG: Is Binary Tree Heap   --->   Given a binary tree, check whether it is a MAX heap.
// A max heap needs both:
// I.  Complete: every level full except maybe the last, and the last level filled from the left.
// II. Heap order: every node's value >= its children's values.

// Example 1:
//          5
//         / \
//        3   4
//       / \
//      1   2
// Output: true

// Example 2:
//          10
//         /  \
//        20   30
// Output: false   (complete, but 10 < 20)

// Problem link: https://www.geeksforgeeks.org/problems/is-binary-tree-heap/1


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
	// Algorithm: Check the 2 rules separately.
	// I.  Complete: exactly '14. Binary_Tree/12_CheckCompletenessOfABinaryTree.cs'. Level order, pushing null children
	//     too. Once a null is seen, no real node may come after it.
	// II. Heap order: DFS. At every node, check it is >= its left and right child (if they exist), then check both
	//     subtrees.
	// T.C: O(n);   2 walks
	// S.C: O(n);   the queue in rule I (the DFS stack is only O(log n), because a complete tree is balanced)
	public bool IsHeap(TreeNode root)
	{
		return IsComplete(root) && HasHeapOrder(root);
	}

	private bool IsComplete(TreeNode root)
	{
		if (root == null) return true;

		Queue<TreeNode> q = new Queue<TreeNode>();
		q.Enqueue(root);
		bool seenNull = false;

		while (q.Count > 0)
		{
			TreeNode node = q.Dequeue();
			if (node == null)
			{
				seenNull = true;
			}
			else
			{
				if (seenNull) return false;
				q.Enqueue(node.left);
				q.Enqueue(node.right);
			}
		}
		return true;
	}

	private bool HasHeapOrder(TreeNode root)
	{
		if (root == null) return true;

		if (root.left != null && root.val < root.left.val) return false;
		if (root.right != null && root.val < root.right.val) return false;

		return HasHeapOrder(root.left) && HasHeapOrder(root.right);
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(5);
		root.left = new TreeNode(3, new TreeNode(1), new TreeNode(2));
		root.right = new TreeNode(4);

		Console.WriteLine(new Solution().IsHeap(root));   // o/p: True
	}
}

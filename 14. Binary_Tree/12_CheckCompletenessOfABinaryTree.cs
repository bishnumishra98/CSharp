// Leetcode: 958. Check Completeness of a Binary Tree   --->   Given the root of a binary tree, determine if it is a
// complete binary tree.
// Complete binary tree: every level, except possibly the last, is completely filled, and all nodes in the last level
// are as far LEFT as possible.

// Example 1:
// Input: root = [1,2,3,4,5,6]
//           1
//         /   \
//        2     3
//       / \   /
//      4   5 6
// Output: true

// Example 2:
// Input: root = [1,2,3,4,5,null,7]
//           1
//         /   \
//        2     3
//       / \     \
//      4   5     7
// Output: false
// Explanation: There is a gap before 7 (3 has no left child), so 7 is not as far left as possible.


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
	// Algorithm: Do a level order traversal, but push children EVEN IF THEY ARE NULL. Read the queue in order:
	// in a complete tree, all real nodes come first, and then only nulls. So once we see the first null, any real node
	// after it means there was a gap: not complete.
	// e.g., Example 1 in level order: 1 2 3 4 5 6 null null ...   (no real node after a null) -> true
	//       Example 2 in level order: 1 2 3 4 5 null 7 ...        (7 comes after a null)      -> false
	// T.C: O(n)
	// S.C: O(n);   the queue (it also holds the null children)
	public bool IsCompleteTree(TreeNode root)
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
				seenNull = true;   // from now on, only nulls are allowed
			}
			else
			{
				if (seenNull) return false;   // a real node after a gap

				q.Enqueue(node.left);    // pushed even when null, to find gaps
				q.Enqueue(node.right);
			}
		}

		return true;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(4), new TreeNode(5));
		root.right = new TreeNode(3, new TreeNode(6), null);

		Console.WriteLine(new Solution().IsCompleteTree(root));   // o/p: True
	}
}

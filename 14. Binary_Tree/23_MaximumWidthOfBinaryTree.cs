// Leetcode: 662. Maximum Width of Binary Tree   --->   Given the root of a binary tree, return the maximum width of the
// tree (the largest width among all levels).
// Width of a level = the length between its leftmost and rightmost non-null nodes, COUNTING the null spots in between
// (the spots that would have nodes if the tree were complete down to that level).
// The answer fits in a 32-bit int.

// Example 1:
// Input: root = [1,3,2,5,3,null,9]
//           1
//          / \
//         3   2
//        / \   \
//       5   3   9
// Output: 4   (last level: 5, 3, null, 9)

// Example 2:
// Input: root = [1,3,2,5,null,null,9,6,null,7]
//           1
//          / \
//         3   2
//        /     \
//       5       9
//      /       /
//     6       7
// Output: 7   (last level: 6, null, null, null, null, null, 7)

// Example 3:
// Input: root = [1,3,2,5]
// Output: 2   (level 2: 3, 2)

// ● Give every node an INDEX, as if the tree were complete (like the array of a heap):
//   root = 0, the left child of index i = 2i + 1, the right child = 2i + 2. Nulls also "use up" their indexes.
//   ( ) = index,  .. = null
//                __________ 10(0) __________
//               /                           \
//            _ 20(1) _                  __ 30(2) __
//           /         \                /           \
//         ..(3)      ..(4)           40(5)         50(6)
//                                   /     \       /     \
//                                60(11) ..(12) ..(13)  70(14)
//   Width of a level = last index - first index + 1. Here the last level: 14 - 11 + 1 = 4.


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
	// Brute force: Level order traversal with the index of each node in the queue. For every level, width =
	// (index of the last node - index of the first node + 1).
	// The problem: The index doubles at every level. At depth 31 it no longer fits in an int, and at depth 63 not even in
	// a long (Leetcode allows trees 3000 levels deep). In C++ that overflow is an error (Leetcode stops with a runtime
	// error). In C#, int math silently wraps around, and by luck the difference (last - first) still comes out right,
	// but code that depends on overflow is fragile: it crashes inside a 'checked' block, and is hard to trust.
	// T.C: O(n)
	// S.C: O(w);   w = max width, for the queue
	public int WidthOfBinaryTree_BruteForce(TreeNode root)
	{
		if (root == null) return 0;
		int maxWidth = 0;

		Queue<(TreeNode node, int index)> q = new Queue<(TreeNode node, int index)>();
		q.Enqueue((root, 0));

		while (q.Count > 0)
		{
			int size = q.Count;
			int first = q.Peek().index, last = first;

			for (int i = 0; i < size; i++)
			{
				var (node, index) = q.Dequeue();
				last = index;   // ends as the rightmost index of the level

				if (node.left != null) q.Enqueue((node.left, 2 * index + 1));
				if (node.right != null) q.Enqueue((node.right, 2 * index + 2));
			}

			maxWidth = Math.Max(maxWidth, last - first + 1);
		}

		return maxWidth;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Same, but NORMALIZE the indexes at every level: subtract the first index of the level from every
	// index of that level, so each level starts again at 0. Widths do not change (we subtract the same number from both
	// ends), but the numbers stay small: an index is now at most about 2 * (width of the level above), and the width
	// fits in an int, so a long is always enough.
	// e.g., the tree above with normalized indexes:
	//       level 1: 20(1), 30(2)  -> minus 1 -> 20(0), 30(1)
	//       level 2: 40(3), 50(4)  -> minus 3 -> 40(0), 50(1)      (children of 30(1): 2*1+1 = 3 and 2*1+2 = 4)
	//       level 3: 60(1), 70(4)  -> minus 1 -> 60(0), 70(3)      width = 3 - 0 + 1 = 4
	// T.C: O(n)
	// S.C: O(w)
	public int WidthOfBinaryTree(TreeNode root)
	{
		if (root == null) return 0;
		long maxWidth = 0;

		Queue<(TreeNode node, long index)> q = new Queue<(TreeNode node, long index)>();
		q.Enqueue((root, 0));

		while (q.Count > 0)
		{
			int size = q.Count;
			long levelMin = q.Peek().index;   // first index of this level
			long last = 0;

			for (int i = 0; i < size; i++)
			{
				var (node, index) = q.Dequeue();
				long normalized = index - levelMin;   // this level now starts at 0
				last = normalized;

				if (node.left != null) q.Enqueue((node.left, 2 * normalized + 1));
				if (node.right != null) q.Enqueue((node.right, 2 * normalized + 2));
			}

			maxWidth = Math.Max(maxWidth, last + 1);   // first normalized index is 0, so width = last - 0 + 1
		}

		return (int)maxWidth;
	}
}


class Program
{
	static void Main()
	{
		//        10
		//       /  \
		//     20    30
		//          /  \
		//        40    50
		//       /        \
		//     60          70
		TreeNode root = new TreeNode(10);
		root.left = new TreeNode(20);
		root.right = new TreeNode(30, new TreeNode(40, new TreeNode(60), null), new TreeNode(50, null, new TreeNode(70)));

		Console.WriteLine(new Solution().WidthOfBinaryTree_BruteForce(root));   // o/p: 4
		Console.WriteLine(new Solution().WidthOfBinaryTree(root));              // o/p: 4
	}
}

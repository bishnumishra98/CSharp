// Morris Traversal   --->   Inorder and preorder traversal in O(1) extra space: no recursion and no stack.
// (Leetcode: 94. Binary Tree Inorder Traversal and 144. Binary Tree Preorder Traversal, follow up.)

// The problem: Recursive traversals (see '02_BinaryTreeTraversals.cs') use O(h) space for the recursion stack. The
// stack is needed only to come BACK UP to a node after we finish its left subtree.
// The fix: Before going into the left subtree, leave a temporary link ("thread") that leads back up. After the left
// subtree, which node do we visit last in inorder ? Its RIGHTMOST node, called the inorder predecessor of the current
// node. Its right link is null (it is the rightmost), so we can borrow it: predecessor.right = curr. When the walk
// reaches the predecessor and follows its right link, it lands back on curr. Then we remove the thread, so the tree is
// unchanged at the end.
//
// Example:
//          1
//         / \
//        2   3
//       / \
//      4   5
//           \
//            6
// Inorder predecessor of 1 = rightmost node of its left subtree (2, 4, 5, 6) = 6. Thread: 6.right = 1.
// Inorder predecessor of 2 = rightmost node of (4) = 4. Thread: 4.right = 2.

// Algorithm (inorder):
// curr = root. While curr != null:
// 1. No left child: visit curr, go right (that may be a real right child, or a thread going back up).
// 2. Has a left child: find the predecessor (go to curr.left, then right as long as possible, stopping at null or at
//    curr itself).
//    I.  predecessor.right == null: first time here. Make the thread (predecessor.right = curr), and go left.
//    II. predecessor.right == curr: we came back up through the thread, so the left subtree is done. Remove the thread,
//        visit curr, go right.
// Preorder is the same, with ONE line moved: visit curr when the thread is MADE (before going left), not when it is
// removed.

// Video explanation: https://www.youtube.com/watch?v=80Zug6D1_r4 (especially from 17:24)


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
	// Inorder (Left, Node, Right).
	// T.C: O(n);   finding predecessors walks every edge at most about 2 more times (once to make the thread, once to
	//              remove it), so the total stays O(n)
	// S.C: O(1);   only the answer list
	public List<int> MorrisInorder(TreeNode root)
	{
		List<int> inorder = new List<int>();
		TreeNode curr = root;

		while (curr != null)
		{
			if (curr.left == null)
			{
				inorder.Add(curr.val);
				curr = curr.right;
			}
			else
			{
				TreeNode pred = curr.left;
				while (pred.right != null && pred.right != curr) pred = pred.right;

				if (pred.right == null)       // first visit: make the thread, go left
				{
					pred.right = curr;
					curr = curr.left;
				}
				else                          // came back through the thread: left subtree is done
				{
					pred.right = null;        // remove the thread
					inorder.Add(curr.val);
					curr = curr.right;
				}
			}
		}

		return inorder;
	}

// ------------------------------------------------------------------------------

	// Preorder (Node, Left, Right): the same, but curr is visited when the thread is made.
	// T.C: O(n)
	// S.C: O(1)
	public List<int> MorrisPreorder(TreeNode root)
	{
		List<int> preorder = new List<int>();
		TreeNode curr = root;

		while (curr != null)
		{
			if (curr.left == null)
			{
				preorder.Add(curr.val);
				curr = curr.right;
			}
			else
			{
				TreeNode pred = curr.left;
				while (pred.right != null && pred.right != curr) pred = pred.right;

				if (pred.right == null)
				{
					pred.right = curr;
					preorder.Add(curr.val);   // visit BEFORE going left (the only change)
					curr = curr.left;
				}
				else
				{
					pred.right = null;
					curr = curr.right;
				}
			}
		}

		return preorder;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(4), new TreeNode(5, null, new TreeNode(6)));
		root.right = new TreeNode(3);

		Console.WriteLine(string.Join(" ", new Solution().MorrisInorder(root)));    // o/p: 4 2 5 6 1 3
		Console.WriteLine(string.Join(" ", new Solution().MorrisPreorder(root)));   // o/p: 1 2 4 5 6 3
	}
}

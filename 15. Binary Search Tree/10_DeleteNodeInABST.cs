// Leetcode: 450. Delete Node in a BST   --->   Given the root of a BST and a key, delete the node with that key. Return
// the root of the BST after the deletion (the root itself may change).

// Example 1:
// Input: root = [5,3,6,2,4,null,7], key = 3
//          5                   5                  5
//         / \                 / \                / \
//        3   6     --->      4   6      OR      2   6
//       / \   \             /     \              \   \
//      2   4   7           2       7              4   7
// Output: [5,4,6,2,null,null,7]   (both trees are accepted)

// Example 2:
// Input: root = [5,3,6,2,4,null,7], key = 0
// Output: [5,3,6,2,4,null,7]   (0 is not in the tree, nothing changes)

// Deleting a node has 3 cases:
// I.   It has no children (a leaf): just remove it.
// II.  It has ONE child: the child takes its place.
// III. It has TWO children: the hard case. Someone must take its place and keep the BST order. 2 ways below.


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
	// Approach 1 (from the C++ code, iterative): Find the PARENT of the node to delete, and replace the node by a new
	// subtree made like this:
	//   No left child: the right subtree takes its place (covers cases I and II).
	//   No right child: the left subtree takes its place.
	//   Both: the LEFT subtree takes its place, and the right subtree is hung under the RIGHTMOST node of the left
	//         subtree. Why is that safe ? The rightmost node of the left subtree is the biggest value on the left, and
	//         every value on the right is bigger than it. So it can have the whole right subtree as its right child.
	// e.g., delete 3: its left is 2, the rightmost of (2) is 2. 2.right = 4 (the right subtree). Now 2 replaces 3.
	// T.C: O(h)
	// S.C: O(1)
	// The downside: hanging a whole subtree at the bottom can make the tree TALLER (worse for later searches).
	public TreeNode DeleteNode_Rejoin(TreeNode root, int key)
	{
		if (root == null) return null;
		if (root.val == key) return Rejoin(root);

		TreeNode curr = root;
		while (curr != null)
		{
			if (key < curr.val)
			{
				if (curr.left != null && curr.left.val == key)
				{
					curr.left = Rejoin(curr.left);
					break;
				}
				curr = curr.left;
			}
			else
			{
				if (curr.right != null && curr.right.val == key)
				{
					curr.right = Rejoin(curr.right);
					break;
				}
				curr = curr.right;
			}
		}

		return root;
	}

	// Returns the subtree that replaces 'node' after deleting it.
	private TreeNode Rejoin(TreeNode node)
	{
		if (node.left == null) return node.right;
		if (node.right == null) return node.left;

		TreeNode rightmost = node.left;
		while (rightmost.right != null) rightmost = rightmost.right;

		rightmost.right = node.right;
		return node.left;
	}

// ------------------------------------------------------------------------------

	// Approach 2 (the textbook way, recursive): For case III, replace the node's VALUE with its inorder SUCCESSOR (the
	// smallest value in its right subtree, the next value in sorted order). The successor is bigger than everything on
	// the left and smaller than the rest of the right, so the order stays correct. Then delete the successor from the
	// right subtree (it has no left child, so that is the easy case I or II).
	// e.g., delete 3: the smallest in its right subtree (4) is 4. Write 4 into the node, then delete the old 4 below.
	// The height never grows, because no subtree is moved down.
	// T.C: O(h)
	// S.C: O(h);   recursion stack
	public TreeNode DeleteNode(TreeNode root, int key)
	{
		if (root == null) return null;   // key not found

		if (key < root.val)
		{
			root.left = DeleteNode(root.left, key);
		}
		else if (key > root.val)
		{
			root.right = DeleteNode(root.right, key);
		}
		else   // this is the node to delete
		{
			if (root.left == null) return root.right;   // cases I and II
			if (root.right == null) return root.left;

			TreeNode successor = root.right;            // case III
			while (successor.left != null) successor = successor.left;

			root.val = successor.val;
			root.right = DeleteNode(root.right, successor.val);
		}

		return root;
	}
}


class Program
{
	// Level order as text, only to print the tree (see '14. Binary Tree/02_BinaryTreeTraversals.cs').
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

	// [5,3,6,2,4,null,7], built fresh each time because deleting changes the tree.
	static TreeNode MakeTree()
	{
		TreeNode root = new TreeNode(5);
		root.left = new TreeNode(3, new TreeNode(2), new TreeNode(4));
		root.right = new TreeNode(6, null, new TreeNode(7));
		return root;
	}

	static void Main()
	{
		Console.WriteLine(LevelOrder(new Solution().DeleteNode_Rejoin(MakeTree(), 3)));   // o/p: 5 | 2 6 | 4 7
		Console.WriteLine(LevelOrder(new Solution().DeleteNode(MakeTree(), 3)));          // o/p: 5 | 4 6 | 2 7
	}
}

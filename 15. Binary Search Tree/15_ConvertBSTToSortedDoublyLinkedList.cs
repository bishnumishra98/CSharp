// Convert BST to Sorted Doubly Linked List   --->   Given the root of a BST, convert it into a doubly linked list whose
// nodes are in SORTED order. Use the tree's own nodes: 'left' becomes 'prev', 'right' becomes 'next'. Return the head
// (the smallest node).

// Example:
// Input:
//           100
//          /   \
//         50   200
//        /  \     \
//       5   60    300
// Output: 5 <-> 50 <-> 60 <-> 100 <-> 200 <-> 300

// Sorted order of a BST = its inorder (see '01_BSTIntroduction.cs'). For a doubly linked list, see
// '11. Linked List/04_DoublyLinkedList.cs'.


using System;
using System.Collections.Generic;

public class TreeNode
{
	public int val;
	public TreeNode left;    // in the list: prev
	public TreeNode right;   // in the list: next
	public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
	{
		this.val = val; this.left = left; this.right = right;
	}
}

public class Solution
{
	// Brute force: Store the inorder values in a list, then build a brand-new doubly linked list from it.
	// The problem: It makes n NEW nodes, and uses O(n) extra memory, instead of reusing the tree's nodes.
	// T.C: O(n)
	// S.C: O(n);   the list and the new nodes
	public TreeNode BSTToDLL_BruteForce(TreeNode root)
	{
		List<int> sorted = new List<int>();
		InOrder(root, sorted);
		if (sorted.Count == 0) return null;

		TreeNode head = new TreeNode(sorted[0]);
		TreeNode prev = head;
		for (int i = 1; i < sorted.Count; i++)
		{
			TreeNode curr = new TreeNode(sorted[i]);
			prev.right = curr;   // prev <-> curr
			curr.left = prev;
			prev = curr;
		}
		return head;
	}

	private void InOrder(TreeNode root, List<int> list)
	{
		if (root == null) return;
		InOrder(root.left, list);
		list.Add(root.val);
		InOrder(root.right, list);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm (in place): Reverse inorder (Right, Node, Left) visits the nodes from the BIGGEST to the
	// smallest. Keep 'head' = the list built so far (all values bigger than the current node). Add each node to the
	// FRONT of that list:
	//   node.right = head  (next = the old first node)
	//   head.left = node   (the old first node's prev = this node)
	//   head = node
	// Why is it safe to overwrite links ?
	//   node.right: its right subtree is already finished before we reach the node.
	//   node.left: we do not touch it here. The call ReverseInOrder(root.left, ...) has already read it, and it gets
	//              overwritten later, when the next smaller node is put in front (head.left = that node).
	// e.g., 300 -> head 300. 200 -> 200 <-> 300. 100 -> 100 <-> 200 <-> 300. 60, 50, 5 the same way.
	// T.C: O(n)
	// S.C: O(h);   recursion stack only
	public TreeNode BSTToDLL(TreeNode root)
	{
		TreeNode head = null;
		ReverseInOrder(root, ref head);
		return head;
	}

	private void ReverseInOrder(TreeNode root, ref TreeNode head)
	{
		if (root == null) return;

		ReverseInOrder(root.right, ref head);   // R

		root.right = head;                      // N: put root in front of the list
		if (head != null) head.left = root;
		head = root;

		ReverseInOrder(root.left, ref head);    // L
	}
}


class Program
{
	static TreeNode MakeTree()
	{
		TreeNode root = new TreeNode(100);
		root.left = new TreeNode(50, new TreeNode(5), new TreeNode(60));
		root.right = new TreeNode(200, null, new TreeNode(300));
		return root;
	}

	// Walk forward with 'right' (next).
	static string ListText(TreeNode head)
	{
		List<int> values = new List<int>();
		for (TreeNode t = head; t != null; t = t.right) values.Add(t.val);
		return string.Join(" <-> ", values);
	}

	static void Main()
	{
		Console.WriteLine(ListText(new Solution().BSTToDLL_BruteForce(MakeTree())));   // o/p: 5 <-> 50 <-> 60 <-> 100 <-> 200 <-> 300
		Console.WriteLine(ListText(new Solution().BSTToDLL(MakeTree())));              // o/p: 5 <-> 50 <-> 60 <-> 100 <-> 200 <-> 300
	}
}

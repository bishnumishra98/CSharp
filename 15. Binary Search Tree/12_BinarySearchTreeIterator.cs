// Leetcode: 173. Binary Search Tree Iterator   --->   Implement the BSTIterator class, an iterator over the INORDER
// traversal of a BST (so it gives the values in sorted order):
// ● BSTIterator(TreeNode root): creates the iterator. It starts BEFORE the smallest value.
// ● int Next(): moves to the next value and returns it.
// ● bool HasNext(): true if there is still a next value.
// Next() is always called only when there is a next value.
// Follow up: Can Next() and HasNext() run in average O(1) time and use O(h) memory ?

// Example 1:
//        7
//       / \
//      3  15
//        /  \
//       9   20
// Input:  ["BSTIterator", "next", "next", "hasNext", "next", "hasNext", "next", "hasNext", "next", "hasNext"]
//         [[[7, 3, 15, null, null, 9, 20]], [], [], [], [], [], [], [], [], []]
// Output: [null, 3, 7, true, 9, true, 15, true, 20, false]

// Brute force (idea only): In the constructor, store the whole inorder in a list, and keep an index. Next() returns
// list[index++], HasNext() checks index < list.Count.
// T.C: O(n) for the constructor, O(1) for Next() and HasNext().   S.C: O(n)   <- the follow up wants O(h) memory.


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

// Optimal: The iterative inorder with a stack from '07_KthSmallestAndKthLargestElementInBST.cs', but paused after
// every value. The stack holds the "path still to finish": its top is always the next smallest value.
// 1. Constructor: push the root and all nodes down its LEFT edge (7, 3). The top (3) is the smallest.
// 2. Next(): pop the top, that is the answer. If it has a right child, push that child and all nodes down ITS left
//    edge (the values just after the popped one are there).
// 3. HasNext(): there is a next value exactly when the stack is not empty.
// e.g., stack [7, 3] -> Next() = 3 (no right child), stack [7] -> Next() = 7, push 15, 9: stack [15, 9] -> Next() = 9
//       -> Next() = 15, push 20 -> Next() = 20, stack empty -> HasNext() = false.
// T.C: Constructor O(h). HasNext() O(1). Next() is O(1) on AVERAGE: one call may push up to h nodes, but over all n
//      calls every node is pushed once and popped once, so n calls cost O(n) in total.
//      (The C++ comment said Next() is O(1). That is only true on average ("amortized"), not for every single call.)
// S.C: O(h);   the stack never holds more than one path
public class BSTIterator
{
	private Stack<TreeNode> st = new Stack<TreeNode>();

	public BSTIterator(TreeNode root)
	{
		PushLeft(root);
	}

	public int Next()
	{
		TreeNode curr = st.Pop();
		PushLeft(curr.right);   // does nothing if there is no right child
		return curr.val;
	}

	public bool HasNext()
	{
		return st.Count > 0;
	}

	// Push 'node' and every node down its left edge.
	private void PushLeft(TreeNode node)
	{
		while (node != null)
		{
			st.Push(node);
			node = node.left;
		}
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(7);
		root.left = new TreeNode(3);
		root.right = new TreeNode(15, new TreeNode(9), new TreeNode(20));

		BSTIterator it = new BSTIterator(root);
		List<int> values = new List<int>();
		while (it.HasNext()) values.Add(it.Next());

		Console.WriteLine(string.Join(" ", values));   // o/p: 3 7 9 15 20
		Console.WriteLine(it.HasNext());               // o/p: False
	}
}

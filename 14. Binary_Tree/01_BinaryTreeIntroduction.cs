// Tree   --->   A NON-linear data structure made of NODES connected by EDGES, like a family tree. It shows a hierarchy
// (who is above whom). Each node holds data and can have 0 or more children.
//
// ● Types by the max number of children (k-ary tree = a node has at most k children):
//   I.   Unary tree:   k = 1, each node has at most 1 child (it is basically a linked list).
//   II.  Binary tree:  k = 2, each node has at most 2 children: a LEFT child and a RIGHT child.
//   III. Ternary tree: k = 3, each node has at most 3 children.
//   IV.  Generic tree: any number of children.

// Binary Tree   --->   A tree where every node has at most 2 children (left and right).
//
//                ___ 10 ___          <- root (level 0)
//              /            \
//          _ 20 _            50      <- level 1
//        /        \
//       30         40                <- level 2 (30, 40 and 50 are leaves)
//
// ● Words you must know:
//   I.    Node:    one box of the tree. It has data + a left link + a right link.
//   II.   Root:    the topmost node (10). Every traversal starts here.
//   III.  Parent / Child: 20 is the parent of 30 and 40. 30 and 40 are the children of 20.
//   IV.   Leaf:    a node with NO children (30, 40, 50).
//   V.    Subtree: a node + everything below it. (20, 30, 40) is the left subtree of 10.
//   VI.   Level (Depth) of a node: number of edges from the root to it. Root is at level 0.
//   VII.  Height of a tree: the longest path from the root down to a leaf.
//         Careful: some count EDGES (height of a single node = 0), Leetcode counts NODES (single node = 1).
//         Above tree: 2 edges or 3 nodes.
//   VIII. Degree of a node: how many children it has (0, 1 or 2 in a binary tree).
//
// ● Special binary trees:
//   I.   Full (Strict) binary tree: every node has 0 or 2 children, never exactly 1.
//   II.  Complete binary tree: every level is full except maybe the last, and the last level is filled from the LEFT.
//        (Heaps are complete binary trees.)
//   III. Perfect binary tree: every level is completely full. All leaves are on the same level.
//   IV.  Balanced binary tree: for every node, the heights of its left and right subtrees differ by at most 1.
//   V.   Skewed tree: every node has only one child (all left or all right). It looks like a linked list, so its
//        height = n. This is the WORST case for most tree algorithms.
//   VI.  Binary Search Tree (BST): left subtree < node < right subtree, for every node. (See '15. Binary_Search_Tree'.)

// ● Why the recursion stack in tree problems is O(h):
//   A recursive function goes down one path at a time, so at most h calls are waiting at once (h = height).
//   Balanced tree: h = log(n).   Skewed tree: h = n.

// ● C++ vs C#: C++ uses 'Node* left' (pointer) and 'root->left'. In C# every class object is a reference, so
//   'TreeNode left' works the same way, and we write 'root.left'. No delete needed (the Garbage Collector frees nodes).


using System;
using System.Collections.Generic;

// Leetcode's node class for a binary tree (same in every Leetcode C# problem).
public class TreeNode
{
	public int val;
	public TreeNode left;
	public TreeNode right;

	// new TreeNode(5) makes a node with val = 5, and no children (left = right = null).
	public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
	{
		this.val = val;
		this.left = left;
		this.right = right;
	}
}

public class Solution
{
	// Making a big tree by hand ('root.left.right.left = ...') is tiring. So, build it from an array instead.
	// The array is the PREORDER of the tree (node, then its left subtree, then its right subtree), with -1 for "no node".
	// e.g., the tree at the top: {10, 20, 30, -1, -1, 40, -1, -1, 50, -1, -1}
	//       10 -> go left: 20 -> go left: 30 -> its left: -1, its right: -1 -> back to 20, its right: 40 -> -1, -1
	//       -> back to 10, its right: 50 -> -1, -1.
	// 'i' is passed by 'ref', so every recursive call moves the SAME index forward (in C++ it was 'int& i').
	// T.C: O(n);   each array element is read once
	// S.C: O(h);   recursion stack
	public TreeNode BuildTree(int[] nodes, ref int i)
	{
		if (nodes[i] == -1) return null;   // -1 means: no node here

		TreeNode root = new TreeNode(nodes[i]);
		i++;
		root.left = BuildTree(nodes, ref i);    // build the whole left subtree first
		i++;
		root.right = BuildTree(nodes, ref i);   // then the right subtree

		return root;
	}
}


class Program
{
	static void Main()
	{
		// I. Building the tree by hand:
		TreeNode root = new TreeNode(10);
		root.left = new TreeNode(20);
		root.right = new TreeNode(50);
		root.left.left = new TreeNode(30);
		root.left.right = new TreeNode(40);
		Console.WriteLine(root.left.right.val);                                   // o/p: 40

		// II. Building the same tree from a preorder array:
		int[] nodes = { 10, 20, 30, -1, -1, 40, -1, -1, 50, -1, -1 };
		int i = 0;
		TreeNode root2 = new Solution().BuildTree(nodes, ref i);
		Console.WriteLine($"{root2.val} {root2.left.val} {root2.left.left.val} {root2.left.right.val} {root2.right.val}");
		// o/p: 10 20 30 40 50
	}
}

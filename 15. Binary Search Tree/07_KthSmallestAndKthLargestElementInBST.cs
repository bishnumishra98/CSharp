// Leetcode: 230. Kth Smallest Element in a BST   --->   Given the root of a BST and an integer k, return the k-th
// smallest value (1-indexed) of all the values in the tree.
// GFG: Kth Largest Element in BST   --->   Same, but return the k-th LARGEST value. Do not change the tree.

// Example 1 (Kth smallest):
// Input: root = [5,3,6,2,4,null,null,1], k = 3
//            5
//           / \
//          3   6
//         / \
//        2   4
//       /
//      1
// Output: 3   (sorted: 1 2 3 4 5 6, the 3rd is 3)

// Example 2 (Kth largest):
// Input: same tree, k = 2
// Output: 5   (from the biggest: 6 5 4 ..., the 2nd is 5)

// Problem link (Kth largest): https://www.geeksforgeeks.org/problems/kth-largest-element-in-bst/1

// Key fact: The inorder traversal of a BST is sorted (see '01_BSTIntroduction.cs').


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
	// Brute force: Store the whole inorder (a sorted list), and return the element at index k - 1.
	// The problem: It visits ALL n nodes and stores them, even when k is small.
	// T.C: O(n)
	// S.C: O(n);   the list (+ O(h) recursion stack)
	public int KthSmallest_BruteForce(TreeNode root, int k)
	{
		List<int> sorted = new List<int>();
		InOrder(root, sorted);
		return sorted[k - 1];
	}

	private void InOrder(TreeNode root, List<int> list)
	{
		if (root == null) return;
		InOrder(root.left, list);
		list.Add(root.val);
		InOrder(root.right, list);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Do the inorder traversal ITERATIVELY with a stack, and stop as soon as we have taken k values.
	// Iterative inorder:
	// 1. Go as far LEFT as possible, pushing every node on the way. (The top of the stack is now the smallest node
	//    not visited yet.)
	// 2. Pop the top: that is the next value in sorted order. Count it: k--. If k is 0, it is the answer.
	// 3. Move to its RIGHT child (the values between it and the next stack node live there), and repeat from step 1.
	// e.g., k = 3: push 5, 3, 2, 1. Pop 1 (k = 2). Pop 2 (k = 1). Pop 3 (k = 0) -> answer 3. Nodes 4 and 6 are never
	//       touched.
	// T.C: O(h + k);   going down the first left path is h steps, then k pops
	// S.C: O(h);   the stack
	public int KthSmallest(TreeNode root, int k)
	{
		Stack<TreeNode> st = new Stack<TreeNode>();
		TreeNode curr = root;

		while (curr != null || st.Count > 0)
		{
			while (curr != null)   // step 1
			{
				st.Push(curr);
				curr = curr.left;
			}

			curr = st.Pop();       // step 2
			k--;
			if (k == 0) return curr.val;

			curr = curr.right;     // step 3
		}

		return -1;   // k is bigger than the number of nodes
	}

// ------------------------------------------------------------------------------

	// Kth largest: The k-th largest is the k-th value of the REVERSE inorder (Right, Node, Left), which gives values
	// from biggest to smallest. So the same code, with left and right swapped.
	// e.g., k = 2: push 5, 6. Pop 6 (k = 1). Go to 6's left: null. Pop 5 (k = 0) -> answer 5.
	// T.C: O(h + k)
	// S.C: O(h)
	public int KthLargest(TreeNode root, int k)
	{
		Stack<TreeNode> st = new Stack<TreeNode>();
		TreeNode curr = root;

		while (curr != null || st.Count > 0)
		{
			while (curr != null)
			{
				st.Push(curr);
				curr = curr.right;   // right instead of left
			}

			curr = st.Pop();
			k--;
			if (k == 0) return curr.val;

			curr = curr.left;        // left instead of right
		}

		return -1;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(5);
		root.left = new TreeNode(3, new TreeNode(2, new TreeNode(1), null), new TreeNode(4));
		root.right = new TreeNode(6);

		Console.WriteLine(new Solution().KthSmallest_BruteForce(root, 3));   // o/p: 3
		Console.WriteLine(new Solution().KthSmallest(root, 3));              // o/p: 3
		Console.WriteLine(new Solution().KthLargest(root, 2));               // o/p: 5
	}
}

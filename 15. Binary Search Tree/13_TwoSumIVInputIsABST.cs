// Leetcode: 653. Two Sum IV - Input is a BST   --->   Given the root of a BST and an integer k, return true if there are
// two DIFFERENT nodes in the BST whose values add up to k, or false otherwise.

// Example 1:
// Input: root = [5,3,6,2,4,null,7], k = 9
//          5
//         / \
//        3   6
//       / \   \
//      2   4   7
// Output: true   (2 + 7, or 3 + 6, or 4 + 5)

// Example 2:
// Input: root = [5,3,6,2,4,null,7], k = 28
// Output: false

// Another way (works for any binary tree): DFS with a HashSet of values seen so far; for each node, check if k - val is
// in the set (like Two Sum on an array). T.C: O(n), S.C: O(n). The BST order lets us do better on space.


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

// The iterator from '12_BinarySearchTreeIterator.cs', with one extra switch:
//   reverse = false: gives values from SMALLEST to biggest (go left, then right).
//   reverse = true:  gives values from BIGGEST to smallest (go right, then left).
public class BSTIterator
{
	private Stack<TreeNode> st = new Stack<TreeNode>();
	private bool reverse;

	public BSTIterator(TreeNode root, bool reverse)
	{
		this.reverse = reverse;
		PushAll(root);
	}

	public int Next()
	{
		TreeNode curr = st.Pop();
		PushAll(reverse ? curr.left : curr.right);
		return curr.val;
	}

	private void PushAll(TreeNode node)
	{
		while (node != null)
		{
			st.Push(node);
			node = reverse ? node.right : node.left;
		}
	}
}

public class Solution
{
	// Better: Inorder gives a SORTED list. Then use 2 pointers (like Two Sum on a sorted array): i at the smallest,
	// j at the biggest.
	//   sum < k: we need a bigger sum, so move i forward (to a bigger value).
	//   sum > k: we need a smaller sum, so move j backward.
	//   sum == k: found.
	// e.g., [2 3 4 5 6 7], k = 9: 2 + 7 = 9. Found at once.
	// T.C: O(n)
	// S.C: O(n);   the list
	public bool FindTarget_TwoPointers(TreeNode root, int k)
	{
		List<int> sorted = new List<int>();
		InOrder(root, sorted);

		int i = 0, j = sorted.Count - 1;
		while (i < j)
		{
			int sum = sorted[i] + sorted[j];
			if (sum == k) return true;
			if (sum < k) i++;
			else j--;
		}
		return false;
	}

	private void InOrder(TreeNode root, List<int> list)
	{
		if (root == null) return;
		InOrder(root.left, list);
		list.Add(root.val);
		InOrder(root.right, list);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: The same 2 pointers, but WITHOUT the list. The 2 pointers become 2 BST iterators:
	// one that gives values from the smallest up (i), one from the biggest down (j). "Move i forward" = i.Next(),
	// "move j backward" = j.Next().
	// When do we stop ? When the 2 values meet (left >= right): every pair has been checked.
	// T.C: O(n);   each node is taken at most once by each iterator
	// S.C: O(h);   2 stacks of at most h nodes
	public bool FindTarget(TreeNode root, int k)
	{
		if (root == null) return false;

		BSTIterator small = new BSTIterator(root, false);
		BSTIterator big = new BSTIterator(root, true);

		int left = small.Next(), right = big.Next();
		while (left < right)
		{
			int sum = left + right;
			if (sum == k) return true;
			if (sum < k) left = small.Next();
			else right = big.Next();
		}
		return false;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(5);
		root.left = new TreeNode(3, new TreeNode(2), new TreeNode(4));
		root.right = new TreeNode(6, null, new TreeNode(7));

		Console.WriteLine(new Solution().FindTarget_TwoPointers(root, 9));   // o/p: True
		Console.WriteLine(new Solution().FindTarget(root, 9));               // o/p: True
	}
}

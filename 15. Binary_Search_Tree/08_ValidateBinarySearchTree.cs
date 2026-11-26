// Leetcode: 98. Validate Binary Search Tree   --->   Given the root of a binary tree, determine if it is a valid BST.
// A valid BST:
// ● The left subtree of a node contains only values STRICTLY less than the node's value.
// ● The right subtree of a node contains only values STRICTLY greater than the node's value.
// ● Both the left and right subtrees are also BSTs.

// Example 1:
// Input: root = [2,1,3]
//       2
//      / \
//     1   3
// Output: true

// Example 2:
// Input: root = [5,1,4,null,null,3,6]
//       5
//      / \
//     1   4
//        / \
//       3   6
// Output: false   (4 is in the right subtree of 5, but 4 < 5)

// The trap: Checking only "left child < node < right child" at every node is NOT enough.
//          5
//         / \
//        4   6
//           / \
//          3   7
// Every node is fine with its own children, but 3 is in the RIGHT subtree of 5 and 3 < 5. Not a BST.


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
	// Brute force: A tree is a BST exactly when its inorder is STRICTLY increasing. Store the inorder and check that
	// every value is smaller than the next one.
	// T.C: O(n)
	// S.C: O(n);   the list
	// Note: The C++ loop was 'i < inorderArray.size() - 1'. size() is unsigned, so for an EMPTY tree 0 - 1 wraps around
	//       to a huge number and the loop reads outside the array. In C#, Count is an int, so Count - 1 = -1 and the loop
	//       just does not run.
	public bool IsValidBST_BruteForce(TreeNode root)
	{
		List<int> list = new List<int>();
		InOrder(root, list);

		for (int i = 0; i < list.Count - 1; i++)
		{
			if (list[i] >= list[i + 1]) return false;
		}
		return true;
	}

	private void InOrder(TreeNode root, List<int> list)
	{
		if (root == null) return;
		InOrder(root.left, list);
		list.Add(root.val);
		InOrder(root.right, list);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Give every node a RANGE (min, max) its value must be strictly inside.
	//   The root can be anything: (-infinity, +infinity).
	//   Going LEFT from a node with value v: everything there must be < v, so the range becomes (min, v).
	//   Going RIGHT: everything there must be > v, so the range becomes (v, max).
	// The range collects the rules of ALL ancestors, not only the parent. That catches the trap above:
	// 5 -> right: (5, inf) -> 6 -> left: (5, 6). 3 is not inside (5, 6), so false.
	// Why long ? A node may hold int.MinValue or int.MaxValue itself. Starting the range at long.MinValue /
	// long.MaxValue keeps "strictly inside" correct for those values too.
	// T.C: O(n)
	// S.C: O(h);   recursion stack (no list needed)
	public bool IsValidBST(TreeNode root)
	{
		return IsInRange(root, long.MinValue, long.MaxValue);
	}

	private bool IsInRange(TreeNode root, long min, long max)
	{
		if (root == null) return true;
		if (root.val <= min || root.val >= max) return false;

		return IsInRange(root.left, min, root.val) && IsInRange(root.right, root.val, max);
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(5);
		root.left = new TreeNode(1);
		root.right = new TreeNode(4, new TreeNode(3), new TreeNode(6));

		Console.WriteLine(new Solution().IsValidBST_BruteForce(root));   // o/p: False
		Console.WriteLine(new Solution().IsValidBST(root));              // o/p: False
	}
}

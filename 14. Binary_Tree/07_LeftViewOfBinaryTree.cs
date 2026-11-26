// GFG: Left View of Binary Tree   --->   Given the root of a binary tree, return its left view: the nodes you can see
// when you stand on the LEFT side of the tree. If the tree is empty, return an empty list.

// Example 1:
// Input: root = [1, 2, 3, 4, 5, N, N]
//         1
//        / \
//       2   3
//      / \
//     4   5
// Output: [1, 2, 4]

// Example 2:
// Input: root = [1, 2, 3, N, N, 4, N, N, 5, N, N]
//         1
//        / \
//       2   3
//          /
//         4
//          \
//           5
// Output: [1, 2, 4, 5]
// Explanation: 4 and 5 are on the right side of the root, but nothing else is on their level, so they are seen too.

// Problem link: https://www.geeksforgeeks.org/problems/left-view-of-binary-tree/1

// The left view = the FIRST (leftmost) node of every level.


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
	// Algorithm: A preorder DFS that remembers the current level (root = level 0).
	// Go LEFT before right, so the first node we meet on any level is its leftmost one.
	// How do we know it is the first visit to a level ? ans.Count = number of levels already seen. If level == ans.Count,
	// this level has no node in 'ans' yet, so this node is the one seen from the left.
	// e.g., Example 2: 1 (level 0, new) -> 2 (level 1, new) -> 3 (level 1, already have 2) -> 4 (level 2, new)
	//       -> 5 (level 3, new).   Answer: 1 2 4 5
	// (A BFS also works: take the first node of every level.)
	// T.C: O(n)
	// S.C: O(h);   recursion stack, h = n for a skewed tree
	public List<int> LeftView(TreeNode root)
	{
		List<int> ans = new List<int>();
		Dfs(root, 0, ans);
		return ans;
	}

	private void Dfs(TreeNode root, int level, List<int> ans)
	{
		if (root == null) return;

		if (level == ans.Count) ans.Add(root.val);   // first node seen on this level

		Dfs(root.left, level + 1, ans);    // left first
		Dfs(root.right, level + 1, ans);
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2);
		root.right = new TreeNode(3);
		root.right.left = new TreeNode(4, null, new TreeNode(5));

		Console.WriteLine(string.Join(" ", new Solution().LeftView(root)));   // o/p: 1 2 4 5
	}
}

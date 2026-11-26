// Leetcode: 199. Binary Tree Right Side View   --->   Given the root of a binary tree, imagine yourself standing on the
// right side of it. Return the values of the nodes you can see, ordered from top to bottom.

// Example 1:
// Input: root = [1,2,3,null,5,null,4]
//          1
//         / \
//        2   3
//         \   \
//          5   4
// Output: [1,3,4]

// Example 2:
// Input: root = [1,2,3,4,5,6,7,null,8]
//           _ 1 _
//          /     \
//         2       3
//        / \     / \
//       4   5   6   7
//        \
//         8
// Output: [1,3,7,8]   (8 is the only node on the last level, so it is seen even though it is on the left)

// Example 3:
// Input: root = []
// Output: []

// First understand '07_LeftViewOfBinaryTree.cs'. This is the same code with the 2 recursive calls swapped.


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
	// Algorithm: DFS that remembers the level, but go RIGHT before left. Then the first node we meet on every level is
	// its rightmost one. Add it if this level is not in 'ans' yet (level == ans.Count).
	// T.C: O(n)
	// S.C: O(h);   recursion stack, h = n for a skewed tree
	public IList<int> RightSideView(TreeNode root)
	{
		List<int> ans = new List<int>();
		Dfs(root, 0, ans);
		return ans;
	}

	private void Dfs(TreeNode root, int level, List<int> ans)
	{
		if (root == null) return;

		if (level == ans.Count) ans.Add(root.val);   // first node seen on this level

		Dfs(root.right, level + 1, ans);   // right first
		Dfs(root.left, level + 1, ans);
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(4, null, new TreeNode(8)), new TreeNode(5));
		root.right = new TreeNode(3, new TreeNode(6), new TreeNode(7));

		Console.WriteLine(string.Join(" ", new Solution().RightSideView(root)));   // o/p: 1 3 7 8
	}
}

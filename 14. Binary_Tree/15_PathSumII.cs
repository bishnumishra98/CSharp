// Leetcode: 113. Path Sum II   --->   Given the root of a binary tree and an integer targetSum, return ALL root-to-leaf
// paths where the sum of the node values equals targetSum. Each path is a list of node values.
// A leaf is a node with no children.

// Example 1:
// Input: root = [5,4,8,11,null,13,4,7,2,null,null,5,1], targetSum = 22
//                5
//              /   \
//             4     8
//            /     / \
//           11    13  4
//          /  \      / \
//         7    2    5   1
// Output: [[5,4,11,2],[5,8,4,5]]
// Explanation: 5 + 4 + 11 + 2 = 22  and  5 + 8 + 4 + 5 = 22

// Example 2:
// Input: root = [1,2,3], targetSum = 5
// Output: []

// Example 3:
// Input: root = [1,2], targetSum = 0
// Output: []

// First understand '06_PathSum.cs'. There we only said yes / no. Here we must also remember the path itself.


using System;
using System.Collections.Generic;
using System.Linq;

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
	// Algorithm: DFS + Backtracking.
	// 1. Keep ONE list 'path' = the nodes from the root to the current node.
	// 2. At a node: add it to 'path', and subtract its value from targetSum.
	// 3. At a leaf with targetSum == 0: save a COPY of 'path' (the list keeps changing after this, so we can not save
	//    the list itself).
	// 4. Go left, go right. Then remove this node from 'path' (backtrack) before going back up, so the parent's path is
	//    exactly as it was.
	// T.C: O(n * h);   every node is visited once, but each found path is copied (up to h values each). For a balanced
	//                  tree that is O(n log n), worst case O(n^2).
	// S.C: O(h);   'path' and the recursion stack (not counting the answer)
	// Note: The C++ comment said T.C: O(n). That forgot the cost of copying each path into the answer.
	public IList<IList<int>> PathSum(TreeNode root, int targetSum)
	{
		IList<IList<int>> result = new List<IList<int>>();
		Dfs(root, targetSum, new List<int>(), result);
		return result;
	}

	private void Dfs(TreeNode root, int targetSum, List<int> path, IList<IList<int>> result)
	{
		if (root == null) return;

		path.Add(root.val);
		targetSum -= root.val;

		if (root.left == null && root.right == null && targetSum == 0)
		{
			result.Add(new List<int>(path));   // save a copy
		}

		Dfs(root.left, targetSum, path, result);
		Dfs(root.right, targetSum, path, result);

		path.RemoveAt(path.Count - 1);   // backtrack
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(5);
		root.left = new TreeNode(4, new TreeNode(11, new TreeNode(7), new TreeNode(2)), null);
		root.right = new TreeNode(8, new TreeNode(13), new TreeNode(4, new TreeNode(5), new TreeNode(1)));

		var paths = new Solution().PathSum(root, 22).Select(p => string.Join(" ", p));
		Console.WriteLine(string.Join(" | ", paths));   // o/p: 5 4 11 2 | 5 8 4 5
	}
}

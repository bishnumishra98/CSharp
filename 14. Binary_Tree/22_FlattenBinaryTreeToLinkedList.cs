// Leetcode: 114. Flatten Binary Tree to Linked List   --->   Given the root of a binary tree, flatten the tree into a
// "linked list" in place:
// ● Use the same TreeNode class: the RIGHT child points to the next node in the list, and the LEFT child is always null.
// ● The list must be in the same order as the PREORDER traversal of the tree.
// Follow up: Can you do it with O(1) extra space ?

// Example 1:
// Input: root = [1,2,5,3,4,null,6]
//          1                 1
//         / \                 \
//        2   5      --->       2
//       / \   \                 \
//      3   4   6                 3
//                                 \
//                                  4
//                                   \
//                                    5
//                                     \
//                                      6
// Output: [1,null,2,null,3,null,4,null,5,null,6]

// Example 2:
// Input: root = []
// Output: []

// Example 3:
// Input: root = [0]
// Output: [0]


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
	// Better: Reverse preorder (Right, Left, Node).
	// We want every node's right to point to the NEXT node in preorder. Hard going forward, but easy going BACKWARDS:
	// visit the nodes in reverse preorder (6, 5, 4, 3, 2, 1), and keep 'prev' = the node visited just before (= the next
	// node in preorder). Set node.right = prev, node.left = null, and then prev = node.
	// Reverse of preorder (N L R) is (R L N): go right first, then left, then handle the node.
	// e.g., 6: right = null, prev = 6 -> 5: right = 6 -> 4: right = 5 -> 3: right = 4 -> 2: right = 3 -> 1: right = 2.
	// Why is it safe to overwrite node.right ? Both subtrees were already processed before we touch the node.
	// T.C: O(n)
	// S.C: O(h);   recursion stack, h = n for a skewed tree
	public void Flatten_Recursive(TreeNode root)
	{
		TreeNode prev = null;
		ReversePreorder(root, ref prev);
	}

	private void ReversePreorder(TreeNode root, ref TreeNode prev)
	{
		if (root == null) return;

		ReversePreorder(root.right, ref prev);
		ReversePreorder(root.left, ref prev);

		root.right = prev;
		root.left = null;
		prev = root;
	}

// ------------------------------------------------------------------------------

	// Optimal (the follow up, O(1) space, the same idea as '27_MorrisTraversal.cs'):
	// Walk down with 'curr'. If curr has a left subtree:
	//   1. Find the LAST node of the left subtree in preorder = its rightmost node ('pre').
	//   2. In preorder, curr.right comes right after 'pre'. So hang curr.right under it: pre.right = curr.right.
	//   3. Move the left subtree to the right: curr.right = curr.left, curr.left = null.
	// Then step to curr.right and repeat. No recursion and no stack.
	// e.g., curr 1: rightmost of left (2, 3, 4) is 4. 4.right = 5. Now 1 -> 2 (3, 4 -> 5 -> 6).
	//       curr 2: rightmost of left (3) is 3. 3.right = 4. Now 1 -> 2 -> 3 -> 4 -> 5 -> 6.
	// T.C: O(n);   each 'find the rightmost' walk only goes over right edges of a left subtree, and once moved, those
	//              edges are on the main line and never walked by such a search again
	// S.C: O(1)
	public void Flatten(TreeNode root)
	{
		TreeNode curr = root;

		while (curr != null)
		{
			if (curr.left != null)
			{
				TreeNode pre = curr.left;
				while (pre.right != null) pre = pre.right;   // rightmost node of the left subtree

				pre.right = curr.right;
				curr.right = curr.left;
				curr.left = null;
			}
			curr = curr.right;
		}
	}
}


class Program
{
	// 1 -> (2 -> 3, 4) and (5 -> 6), built fresh each time because Flatten changes the tree.
	static TreeNode MakeTree()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2, new TreeNode(3), new TreeNode(4));
		root.right = new TreeNode(5, null, new TreeNode(6));
		return root;
	}

	// Walk the flattened list through the right links.
	static string ListText(TreeNode head)
	{
		List<int> values = new List<int>();
		for (TreeNode t = head; t != null; t = t.right) values.Add(t.val);
		return string.Join(" -> ", values);
	}

	static void Main()
	{
		TreeNode root1 = MakeTree();
		new Solution().Flatten_Recursive(root1);
		Console.WriteLine(ListText(root1));   // o/p: 1 -> 2 -> 3 -> 4 -> 5 -> 6

		TreeNode root2 = MakeTree();
		new Solution().Flatten(root2);
		Console.WriteLine(ListText(root2));   // o/p: 1 -> 2 -> 3 -> 4 -> 5 -> 6
	}
}

// Binary Search Tree (BST)   --->   A binary tree with one extra rule, for EVERY node:
//   all values in its LEFT subtree  <  the node's value  <  all values in its RIGHT subtree.
//
//          5
//         / \
//        1   7
//         \   \
//          3   9
//         / \
//        2   4
//
// Check node 5: left side {1, 2, 3, 4} are all smaller, right side {7, 9} are all bigger. The same holds at 1, 3 and 7.
// Careful: it is not enough that each child is on the correct side of its PARENT. The whole subtree must be.
//          5
//         / \
//        3   7          6 > 3, so 6 is fine as the right child of 3.
//         \             But 6 is in the LEFT subtree of 5, and 6 > 5. So this is NOT a BST.
//          6            (See '08_ValidateBinarySearchTree.cs'.)

// ● Why do we like it ?
//   I.   Searching is like Binary Search (see '06. Searching'): at every node, the value we want is either here, or
//        only on the left (if smaller), or only on the right (if bigger). We throw away one side at every step.
//        Search in a normal binary tree: O(n) (must look everywhere). Search in a BST: O(h), h = height.
//   II.  Insert and delete are also O(h).
//   III. INORDER traversal (Left, Node, Right) of a BST gives the values in SORTED order. Above: 1 2 3 4 5 7 9.
//        Many BST problems are solved with this one fact.
//
// ● The height decides everything:
//   Balanced BST: h = log(n), so search / insert / delete are O(log n).
//   Skewed BST (e.g., values inserted in sorted order 1, 2, 3, 4 ... each one goes to the right): h = n, so O(n),
//   no better than a linked list. (Self-balancing BSTs like AVL and Red-Black trees fix this. C#'s SortedSet<T> and
//   SortedDictionary<K, V> are Red-Black trees inside.)
//
// ● Duplicates: In this folder a BST has unique values (as in most problems). If duplicates are allowed, a problem says
//   which side they go to (usually right, like the Insert below).


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
	// Insert one value (recursive): Smaller than the node: it belongs in the left subtree, so insert it there. Otherwise,
	// in the right subtree. When we fall off the tree (null), that empty spot is where the new node goes.
	// It returns the root of the subtree, so the parent can link the new node: root.left = Insert(root.left, value).
	// (An iterative version is in '05_InsertIntoABinarySearchTree.cs'.)
	// T.C: O(h)
	// S.C: O(h);   recursion stack
	public TreeNode Insert(TreeNode root, int value)
	{
		if (root == null) return new TreeNode(value);

		if (value < root.val) root.left = Insert(root.left, value);
		else root.right = Insert(root.right, value);

		return root;
	}

// ------------------------------------------------------------------------------

	// Build a BST: start with an empty tree and insert the values one by one.
	// e.g., {5, 1, 3, 4, 2, 7, 9}: 5 is the root. 1 < 5: left of 5. 3: < 5, > 1: right of 1. 4: right of 3.
	//       2: left of 3. 7: right of 5. 9: right of 7.   (The tree at the top.)
	// T.C: O(n log n) on average, O(n^2) in the worst case (sorted input makes a skewed tree: 1 + 2 + ... + n steps)
	// S.C: O(n);   the n nodes (+ O(h) recursion stack)
	public TreeNode BuildBST(int[] values)
	{
		TreeNode root = null;
		foreach (int v in values) root = Insert(root, v);
		return root;
	}

// ------------------------------------------------------------------------------

	// Inorder traversal (see '14. Binary Tree/02_BinaryTreeTraversals.cs'). For a BST the result is sorted.
	// T.C: O(n)
	// S.C: O(h)
	public List<int> InOrder(TreeNode root)
	{
		List<int> ans = new List<int>();
		InOrder(root, ans);
		return ans;
	}

	private void InOrder(TreeNode root, List<int> ans)
	{
		if (root == null) return;
		InOrder(root.left, ans);
		ans.Add(root.val);
		InOrder(root.right, ans);
	}
}


class Program
{
	static void Main()
	{
		Solution sol = new Solution();
		TreeNode root = sol.BuildBST(new int[] { 5, 1, 3, 4, 2, 7, 9 });

		Console.WriteLine(string.Join(" ", sol.InOrder(root)));                  // o/p: 1 2 3 4 5 7 9
		Console.WriteLine(new Solution().Insert(root, 6).right.left.val);        // o/p: 6   (6 > 5 -> right, 6 < 7 -> left of 7)
	}
}

// GFG: Predecessor and Successor   --->   Given the root of a BST and an integer key, find the inorder predecessor and the
// inorder successor of the key. If either one does not exist, it is null.
// Predecessor = the value just SMALLER than key (strictly). Successor = the value just BIGGER than key (strictly).
// The key itself does not have to be in the tree.

// Example 1:
// Input: root = [50, 30, 70, 20, 40, 60, 80], key = 65
//          _ 50 _
//         /      \
//        30      70
//       /  \    /  \
//      20  40  60  80
// Output: [60, 70]

// Example 2:
// Input: root = [8, 1, 9, N, 4, N, 10, 3], key = 8
//          8
//         / \
//        1   9
//         \   \
//          4  10
//         /
//        3
// Output: [4, 9]   (8 is in the tree, but it is neither its own predecessor nor successor)

// Problem link: https://www.geeksforgeeks.org/problems/predecessor-and-successor/1

// Very close to '04_CeilAndFloorInBST.cs'. The only difference: Floor / Ceil may be the key itself (<=, >=), the
// predecessor / successor may NOT (<, >).


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
	// Brute force: The inorder is sorted. Walk it: the last node < key is the predecessor, the first node > key is the
	// successor.
	// T.C: O(n)
	// S.C: O(n);   the list of nodes
	public TreeNode[] FindPreSuc_BruteForce(TreeNode root, int key)
	{
		List<TreeNode> sorted = new List<TreeNode>();
		InOrder(root, sorted);

		TreeNode pre = null, suc = null;
		foreach (TreeNode node in sorted)
		{
			if (node.val < key) pre = node;
			if (node.val > key)
			{
				suc = node;
				break;
			}
		}
		return new TreeNode[] { pre, suc };
	}

	private void InOrder(TreeNode root, List<TreeNode> list)
	{
		if (root == null) return;
		InOrder(root.left, list);
		list.Add(root);
		InOrder(root.right, list);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: 2 separate walks from the root, each down ONE path.
	// Predecessor: node < key: a possible answer. Save it and go RIGHT (try a bigger one, still < key).
	//              node >= key: too big. Go LEFT.
	// Successor:   node > key: a possible answer. Save it and go LEFT (try a smaller one, still > key).
	//              node <= key: too small. Go RIGHT.
	// e.g., key = 65. Predecessor: 50 < 65, pre = 50, right -> 70 >= 65, left -> 60 < 65, pre = 60, right -> null.
	//                 Successor:   50 <= 65, right -> 70 > 65, suc = 70, left -> 60 <= 65, right -> null.
	// T.C: O(h);   2 walks of at most h steps
	// S.C: O(1)
	public TreeNode[] FindPreSuc(TreeNode root, int key)
	{
		TreeNode pre = null, suc = null;

		for (TreeNode curr = root; curr != null; )
		{
			if (curr.val < key)
			{
				pre = curr;
				curr = curr.right;
			}
			else curr = curr.left;
		}

		for (TreeNode curr = root; curr != null; )
		{
			if (curr.val > key)
			{
				suc = curr;
				curr = curr.left;
			}
			else curr = curr.right;
		}

		return new TreeNode[] { pre, suc };
	}
}


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(50);
		root.left = new TreeNode(30, new TreeNode(20), new TreeNode(40));
		root.right = new TreeNode(70, new TreeNode(60), new TreeNode(80));

		TreeNode[] brute = new Solution().FindPreSuc_BruteForce(root, 65);
		TreeNode[] best = new Solution().FindPreSuc(root, 65);
		Console.WriteLine($"{brute[0]?.val} {brute[1]?.val}");   // o/p: 60 70     ('?.' gives null, printed as empty, if not found)
		Console.WriteLine($"{best[0]?.val} {best[1]?.val}");     // o/p: 60 70
	}
}

// GFG: Ceil in BST and Floor in BST   --->   Given the root of a BST and an integer x:
// I.  Ceil(x)  = the SMALLEST value in the BST that is >= x.   Return -1 if there is none.
// II. Floor(x) = the LARGEST value in the BST that is <= x.    Return -1 if there is none.
// (All values are >= 1, so -1 can never be a real answer.)

// Example 1 (Ceil):
// Input: x = 6
//          10
//         /  \
//        5    11
//       / \
//      4   7
//           \
//            8
// Output: 7   (6 is not in the tree. The smallest value bigger than 6 is 7.)

// Example 2 (Floor):
// Input: x = 87
//     2
//      \
//       81
//      /  \
//     42   87
//      \     \
//      66     90
//      /
//     45
// Output: 87   (87 is in the tree, so floor = 87)

// Example 3 (Floor):
// Input: x = 5, root = [6, N, 8, 7, 9]
// Output: -1   (every value is bigger than 5)

// Problem links: https://www.geeksforgeeks.org/problems/implementing-ceil-in-bst/1
//                https://www.geeksforgeeks.org/problems/floor-in-bst/1


using System;

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
	// Ceil: Walk down like a search, and remember the best answer so far.
	//   node == x: x itself is in the tree, so it is the ceil. Stop.
	//   node >  x: this node is a possible ceil. Save it, then go LEFT to look for a smaller one that is still >= x.
	//   node <  x: this node is too small (and so is its whole left subtree). Go RIGHT.
	// e.g., x = 6: 10 > 6, ceil = 10, go left -> 5 < 6, go right -> 7 > 6, ceil = 7, go left -> null. Answer 7.
	// T.C: O(h)
	// S.C: O(1)
	public int FindCeil(TreeNode root, int x)
	{
		int ceil = -1;

		while (root != null)
		{
			if (root.val == x) return x;

			if (root.val > x)
			{
				ceil = root.val;
				root = root.left;
			}
			else
			{
				root = root.right;
			}
		}

		return ceil;
	}

// ------------------------------------------------------------------------------

	// Floor: The mirror of Ceil.
	//   node == x: answer x. Stop.
	//   node <  x: a possible floor. Save it, then go RIGHT to look for a bigger one that is still <= x.
	//   node >  x: too big. Go LEFT.
	// T.C: O(h)
	// S.C: O(1)
	public int FindFloor(TreeNode root, int x)
	{
		int floor = -1;

		while (root != null)
		{
			if (root.val == x) return x;

			if (root.val < x)
			{
				floor = root.val;
				root = root.right;
			}
			else
			{
				root = root.left;
			}
		}

		return floor;
	}
}


class Program
{
	static void Main()
	{
		TreeNode root1 = new TreeNode(10);
		root1.left = new TreeNode(5, new TreeNode(4), new TreeNode(7, null, new TreeNode(8)));
		root1.right = new TreeNode(11);

		TreeNode root2 = new TreeNode(2);
		root2.right = new TreeNode(81, new TreeNode(42, null, new TreeNode(66, new TreeNode(45), null)),
		                               new TreeNode(87, null, new TreeNode(90)));

		Console.WriteLine(new Solution().FindCeil(root1, 6));     // o/p: 7
		Console.WriteLine(new Solution().FindFloor(root2, 87));   // o/p: 87
	}
}

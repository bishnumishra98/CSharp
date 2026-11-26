// Leetcode: 297. Serialize and Deserialize Binary Tree   --->   Serialization = turning a data structure into a string
// (or bytes), so it can be saved in a file or sent over a network, and rebuilt later.
// Design an algorithm to serialize a binary tree into a string, and deserialize that string back into the SAME tree.
// Any format is allowed, as long as the 2 functions work together.

// Example 1:
// Input: root = [1,2,3,null,null,4,5]
//         1
//        / \
//       2   3
//          / \
//         4   5
// Output: [1,2,3,null,null,4,5]

// Example 2:
// Input: root = []
// Output: []

// The problem: The values alone are not enough. "1,2,3" could be many different shapes. We must also write down
// WHERE the children are missing. So nulls are written too, as '#'.


using System;
using System.Collections.Generic;
using System.Text;

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

public class Codec
{
	// Serialize: Level order traversal (BFS), but also push NULL children. Write each node's value, or '#' for null,
	// followed by a comma.
	// e.g., Example 1: "1,2,3,#,#,4,5,#,#,#,#,"
	//       1 | 2 3 | (2's children) # # (3's children) 4 5 | (4's children) # # (5's children) # #
	// StringBuilder is used instead of 's += ...', because adding to a string in a loop copies the whole string every
	// time (strings in C# can not change), which would be O(n^2).
	// T.C: O(n)
	// S.C: O(n)
	public string Serialize(TreeNode root)
	{
		if (root == null) return "";

		StringBuilder sb = new StringBuilder();
		Queue<TreeNode> q = new Queue<TreeNode>();
		q.Enqueue(root);

		while (q.Count > 0)
		{
			TreeNode node = q.Dequeue();

			if (node == null)
			{
				sb.Append("#,");
				continue;
			}

			sb.Append(node.val).Append(',');
			q.Enqueue(node.left);    // nulls too
			q.Enqueue(node.right);
		}

		return sb.ToString();
	}

// ------------------------------------------------------------------------------

	// Deserialize: Do the same BFS again, reading the tokens in the same order they were written.
	// 1. Split the string at the commas. The first token is the root. Put it in a queue.
	// 2. Take a node out of the queue. The NEXT 2 tokens are its left and right child (that is the order Serialize
	//    wrote them). For each one that is not '#', make a node, link it, and put it in the queue.
	// Every real node in the queue gets its 2 tokens, exactly like in Serialize, so the shape comes out the same.
	// e.g., tokens 1 2 3 # # 4 5 # # # #: root 1. Node 1 reads (2, 3). Node 2 reads (#, #). Node 3 reads (4, 5).
	//       Node 4 reads (#, #). Node 5 reads (#, #).
	// T.C: O(n)
	// S.C: O(n)
	public TreeNode Deserialize(string data)
	{
		if (data.Length == 0) return null;

		string[] tokens = data.Split(',', StringSplitOptions.RemoveEmptyEntries);   // drop the empty one after the last ','
		int i = 0;

		TreeNode root = new TreeNode(int.Parse(tokens[i++]));
		Queue<TreeNode> q = new Queue<TreeNode>();
		q.Enqueue(root);

		while (q.Count > 0)
		{
			TreeNode node = q.Dequeue();

			string leftToken = tokens[i++];
			if (leftToken != "#")
			{
				node.left = new TreeNode(int.Parse(leftToken));
				q.Enqueue(node.left);
			}

			string rightToken = tokens[i++];
			if (rightToken != "#")
			{
				node.right = new TreeNode(int.Parse(rightToken));
				q.Enqueue(node.right);
			}
		}

		return root;
	}
}

// Note: This problem was in the C++ Binary_Search_Tree folder, but it works on ANY binary tree, so it is here.


class Program
{
	static void Main()
	{
		TreeNode root = new TreeNode(1);
		root.left = new TreeNode(2);
		root.right = new TreeNode(3, new TreeNode(4), new TreeNode(5));

		Codec codec = new Codec();
		string data = codec.Serialize(root);
		Console.WriteLine(data);                                // o/p: 1,2,3,#,#,4,5,#,#,#,#,

		TreeNode copy = codec.Deserialize(data);
		Console.WriteLine($"{copy.val} {copy.left.val} {copy.right.val} {copy.right.left.val} {copy.right.right.val}");
		// o/p: 1 2 3 4 5   (same shape: 2 is the left child, 4 and 5 are under 3)
	}
}

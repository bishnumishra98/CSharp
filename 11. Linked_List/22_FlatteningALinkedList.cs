// GFG: Flattening a Linked List   --->   Given a linked list containing n head nodes where every node in the linked list
// contains two pointers:
// (i) next points to the next node in the list.
// (ii) bottom points to a sub-linked list where the current node is the head.
// Each of the sub-linked lists nodes and the head nodes are sorted in ascending order based on their data. Flatten the
// linked list such that all the nodes appear in a single level while maintaining the sorted order.
// Note: The flattened list will be printed using the bottom pointer instead of the next pointer.

// Example 1:
// Input:
//      5 -> 10 -> 19 -> 28      (next pointers)
//      |     |     |     |
//      7    20    22    40      (bottom pointers)
//      |                 |
//      8                45
// Output: 5 -> 7 -> 8 -> 10 -> 19 -> 20 -> 22 -> 28 -> 40 -> 45

// Problem link: https://www.geeksforgeeks.org/problems/flattening-a-linked-list/1

// Each column is a sorted list (joined by 'bottom'). So this is "merge n sorted lists" (like '23_MergeKSortedLists.cs').


using System;
using System.Collections.Generic;

public class Node
{
	public int data;
	public Node next, bottom;
	public Node(int data) { this.data = data; }
}

public class Solution
{
	// Brute force: Collect all values, sort them, and build a new list joined by 'bottom'.
	// T.C: O(N logN);   where N = total number of nodes
	// S.C: O(N);   for the values and the new nodes
	public Node Flatten_Sorting(Node root)
	{
		List<int> values = new List<int>();
		for (Node col = root; col != null; col = col.next)
		{
			for (Node t = col; t != null; t = t.bottom) values.Add(t.data);
		}
		values.Sort();

		Node dummy = new Node(0);
		Node tail = dummy;
		foreach (int v in values)
		{
			tail.bottom = new Node(v);
			tail = tail.bottom;
		}
		return dummy.bottom;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Recursion. Flatten everything on the right first, then merge it with the current column.
	// flatten(root) = merge(root's column, flatten(root.next))
	// Merging works like '12_MergeTwoSortedLists.cs', but follows 'bottom' instead of 'next'. Set next = null on every
	// node we use, as the answer uses only 'bottom'.
	// T.C: O(n * N);   where n = number of columns. Each of the n merges can walk up to N nodes.
	// S.C: O(n);   recursion stack
	public Node Flatten_Recursive(Node root)
	{
		if (root == null || root.next == null) return root;

		Node rightFlattened = Flatten_Recursive(root.next);
		return Merge(root, rightFlattened);
	}

	private Node Merge(Node a, Node b)
	{
		Node dummy = new Node(0);
		Node tail = dummy;

		while (a != null && b != null)
		{
			if (a.data < b.data)
			{
				tail.bottom = a;
				a = a.bottom;
			}
			else
			{
				tail.bottom = b;
				b = b.bottom;
			}
			tail = tail.bottom;
			tail.next = null;
		}

		tail.bottom = (a != null) ? a : b;
		if (tail.bottom != null) tail.bottom.next = null;
		return dummy.bottom;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Min-heap (PriorityQueue), same as '23_MergeKSortedLists.cs'.
	// 1. Put the top node of every column into a min-heap (the smallest value always comes out first).
	// 2. Take out the smallest node, attach it to the answer, and put its 'bottom' node (the next one in its column) into
	//    the heap.
	// 3. Repeat until the heap is empty.
	// T.C: O(N log n);   N nodes go in and out of a heap that holds at most n nodes
	// S.C: O(n);   for the heap
	public Node Flatten(Node root)
	{
		PriorityQueue<Node, int> heap = new PriorityQueue<Node, int>();
		for (Node col = root; col != null; col = col.next) heap.Enqueue(col, col.data);

		Node dummy = new Node(0);
		Node tail = dummy;

		while (heap.Count > 0)
		{
			Node smallest = heap.Dequeue();
			if (smallest.bottom != null) heap.Enqueue(smallest.bottom, smallest.bottom.data);

			tail.bottom = smallest;
			tail = smallest;
			tail.next = null;
		}

		tail.bottom = null;
		return dummy.bottom;
	}
}


class Program
{
	static Node BuildExample()   // the list from Example 1
	{
		int[][] columns = { new[] { 5, 7, 8 }, new[] { 10, 20 }, new[] { 19, 22 }, new[] { 28, 40, 45 } };
		Node head = null, prevTop = null;

		foreach (int[] col in columns)
		{
			Node top = new Node(col[0]), t = top;
			for (int i = 1; i < col.Length; i++) { t.bottom = new Node(col[i]); t = t.bottom; }

			if (prevTop == null) head = top;
			else prevTop.next = top;
			prevTop = top;
		}
		return head;
	}

	static string Print(Node head)
	{
		List<int> values = new List<int>();
		for (Node t = head; t != null; t = t.bottom) values.Add(t.data);
		return string.Join(" -> ", values);
	}

	static void Main()
	{
		Console.WriteLine(Print(new Solution().Flatten_Sorting(BuildExample())));     // o/p: 5 -> 7 -> 8 -> 10 -> 19 -> 20 -> 22 -> 28 -> 40 -> 45
		Console.WriteLine(Print(new Solution().Flatten_Recursive(BuildExample())));   // o/p: 5 -> 7 -> 8 -> 10 -> 19 -> 20 -> 22 -> 28 -> 40 -> 45
		Console.WriteLine(Print(new Solution().Flatten(BuildExample())));             // o/p: 5 -> 7 -> 8 -> 10 -> 19 -> 20 -> 22 -> 28 -> 40 -> 45
	}
}

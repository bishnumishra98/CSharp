// Leetcode: 23. Merge k Sorted Lists   --->   You are given an array of k linked-lists lists, each linked-list is sorted
// in ascending order. Merge all the linked-lists into one sorted linked-list and return it.

// Example 1:
// Input: lists = [[1,4,5],[1,3,4],[2,6]]
// Output: [1,1,2,3,4,4,5,6]
// Explanation: The linked-lists are: 1->4->5, 1->3->4, 2->6. Merging them into one sorted linked list:
// 1->1->2->3->4->4->5->6

// Example 2:
// Input: lists = []
// Output: []

// Example 3:
// Input: lists = [[]]
// Output: []

// N = total number of nodes in all lists, k = number of lists.


using System;
using System.Collections.Generic;

public class ListNode
{
	public int val;
	public ListNode next;
	public ListNode(int val = 0, ListNode next = null) { this.val = val; this.next = next; }
}

public class Solution
{
	// Brute force: Collect all values, sort them, and build a new list.
	// T.C: O(N logN)
	// S.C: O(N);   for the values and the new nodes
	public ListNode MergeKLists_Sorting(ListNode[] lists)
	{
		List<int> values = new List<int>();
		foreach (ListNode head in lists)
		{
			for (ListNode t = head; t != null; t = t.next) values.Add(t.val);
		}
		values.Sort();

		ListNode dummy = new ListNode();
		ListNode tail = dummy;
		foreach (int v in values)
		{
			tail.next = new ListNode(v);
			tail = tail.next;
		}
		return dummy.next;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Merge the lists one by one: ((list0 + list1) + list2) + ... using MergeTwoLists()
	// ('12_MergeTwoSortedLists.cs').
	// Problem: The merged list keeps growing, and it is walked again in every merge.
	// T.C: O(N * k)
	// S.C: O(1)
	public ListNode MergeKLists_OneByOne(ListNode[] lists)
	{
		ListNode result = null;
		foreach (ListNode head in lists) result = MergeTwoLists(result, head);
		return result;
	}

	private ListNode MergeTwoLists(ListNode a, ListNode b)
	{
		ListNode dummy = new ListNode();
		ListNode tail = dummy;
		while (a != null && b != null)
		{
			if (a.val <= b.val) { tail.next = a; a = a.next; }
			else { tail.next = b; b = b.next; }
			tail = tail.next;
		}
		tail.next = (a != null) ? a : b;
		return dummy.next;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Min-heap (PriorityQueue).
	// At any moment, the next node of the answer is the smallest of the k FRONT nodes. A min-heap gives the smallest of k
	// items in O(logk), instead of checking all k.
	// 1. Put the head of every non-empty list into the heap.
	// 2. Take out the smallest node, attach it to the answer, and put its next node (from the same list) into the heap.
	// 3. Repeat until the heap is empty.
	// PriorityQueue<TElement, TPriority> (C# .NET 6+): Enqueue(item, priority) adds, Dequeue() removes the item with the
	// SMALLEST priority. Here the item is the node, and its priority is its value. (More in the Heaps folder.)
	// T.C: O(N logk);   N nodes, each goes in and out of a heap of size at most k
	// S.C: O(k);   for the heap
	// (Merging the lists in pairs, like merge sort: (0+1), (2+3), ... then again, is also O(N logk), with O(1) space.)
	public ListNode MergeKLists(ListNode[] lists)
	{
		PriorityQueue<ListNode, int> heap = new PriorityQueue<ListNode, int>();
		foreach (ListNode head in lists)
		{
			if (head != null) heap.Enqueue(head, head.val);
		}

		ListNode dummy = new ListNode();
		ListNode tail = dummy;

		while (heap.Count > 0)
		{
			ListNode smallest = heap.Dequeue();
			tail.next = smallest;
			tail = smallest;

			if (smallest.next != null) heap.Enqueue(smallest.next, smallest.next.val);
		}

		return dummy.next;
	}
}


class Program
{
	static ListNode Build(params int[] values)
	{
		ListNode dummy = new ListNode();
		ListNode tail = dummy;
		foreach (int v in values) { tail.next = new ListNode(v); tail = tail.next; }
		return dummy.next;
	}

	static string Print(ListNode head)
	{
		List<int> values = new List<int>();
		for (ListNode t = head; t != null; t = t.next) values.Add(t.val);
		return values.Count == 0 ? "(empty)" : string.Join(" -> ", values);
	}

	static ListNode[] Example() => new[] { Build(1, 4, 5), Build(1, 3, 4), Build(2, 6) };

	static void Main()
	{
		Console.WriteLine(Print(new Solution().MergeKLists_Sorting(Example())));     // o/p: 1 -> 1 -> 2 -> 3 -> 4 -> 4 -> 5 -> 6
		Console.WriteLine(Print(new Solution().MergeKLists_OneByOne(Example())));    // o/p: 1 -> 1 -> 2 -> 3 -> 4 -> 4 -> 5 -> 6
		Console.WriteLine(Print(new Solution().MergeKLists(Example())));             // o/p: 1 -> 1 -> 2 -> 3 -> 4 -> 4 -> 5 -> 6
	}
}

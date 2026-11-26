// Leetcode: 21. Merge Two Sorted Lists   --->   You are given the heads of two sorted linked lists list1 and list2.
// Merge the two lists into one sorted list. The list should be made by splicing together the nodes of the first two
// lists. Return the head of the merged linked list.

// Example 1:
// Input: list1 = [1,2,4], list2 = [1,3,4]
// Output: [1,1,2,3,4,4]

// Example 2:
// Input: list1 = [], list2 = []
// Output: []

// Example 3:
// Input: list1 = [], list2 = [0]
// Output: [0]

// Same idea as the merge step of merge sort ('07. Sorting\04_MergeSort.cs'), but with nodes instead of an array.


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
	// Brute force: Copy all values of both lists into a List<int>, sort it, and build a new linked list.
	// Problem: It ignores that both lists are already sorted, and it creates new nodes instead of reusing the old ones
	//          (the problem asks to splice the existing nodes).
	// T.C: O((n1 + n2) * log(n1 + n2));   for sorting
	// S.C: O(n1 + n2);   for the values and the new nodes
	public ListNode MergeTwoLists_Sorting(ListNode list1, ListNode list2)
	{
		List<int> values = new List<int>();
		for (ListNode t = list1; t != null; t = t.next) values.Add(t.val);
		for (ListNode t = list2; t != null; t = t.next) values.Add(t.val);
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

	// Optimal algorithm: Two pointers + a dummy node. Reuse the existing nodes, only change the 'next' links.
	// 1. 'dummy' is a fake node in front of the answer. 'tail' is the last node of the answer so far.
	//    (With dummy, we don't need a special case for choosing the first node.)
	// 2. While both lists have nodes: attach the smaller of the 2 front nodes to tail, and move that list ahead.
	// 3. One list is finished. The other one is already sorted, so attach the rest of it in one go.
	// 4. The answer starts at dummy.next.
	// e.g., [1, 2, 4] and [1, 3, 4]:  1 (list2, as 1 is not < 1), 1, 2, 3, 4 (list2), then attach the rest [4].
	// T.C: O(n1 + n2)
	// S.C: O(1);   no new nodes (only the dummy)
	public ListNode MergeTwoLists(ListNode list1, ListNode list2)
	{
		ListNode dummy = new ListNode();
		ListNode tail = dummy;

		while (list1 != null && list2 != null)
		{
			if (list1.val < list2.val)
			{
				tail.next = list1;
				list1 = list1.next;
			}
			else
			{
				tail.next = list2;
				list2 = list2.next;
			}
			tail = tail.next;
		}

		tail.next = (list1 != null) ? list1 : list2;   // attach whatever is left
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

	static void Main()
	{
		Console.WriteLine(Print(new Solution().MergeTwoLists_Sorting(Build(1, 2, 4), Build(1, 3, 4))));   // o/p: 1 -> 1 -> 2 -> 3 -> 4 -> 4
		Console.WriteLine(Print(new Solution().MergeTwoLists(Build(1, 2, 4), Build(1, 3, 4))));           // o/p: 1 -> 1 -> 2 -> 3 -> 4 -> 4
	}
}

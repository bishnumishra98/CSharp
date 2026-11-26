// Leetcode: 82. Remove Duplicates from Sorted List II   --->   Given the head of a sorted linked list, delete all nodes
// that have duplicate numbers, leaving only distinct numbers from the original list. Return the linked list sorted as
// well.

// Example 1:
// Input: head = [1,2,3,3,4,4,5]
// Output: [1,2,5]

// Example 2:
// Input: head = [1,1,1,2,3]
// Output: [2,3]

// Difference from '11_RemoveDuplicatesFromSortedList.cs': There, one copy of each value is kept (1,1,2 -> 1,2).
// Here, a value that repeats is removed COMPLETELY (1,1,2 -> 2). Even the head can be removed, so use a dummy node.


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
	// Brute force: Count every value with a Dictionary. Then rebuild the list keeping only nodes whose value count is 1.
	// (Works for an unsorted list too.)
	// T.C: O(n)
	// S.C: O(n);   for the Dictionary
	public ListNode DeleteDuplicates_Count(ListNode head)
	{
		Dictionary<int, int> count = new Dictionary<int, int>();
		for (ListNode t = head; t != null; t = t.next) count[t.val] = count.GetValueOrDefault(t.val) + 1;

		ListNode dummy = new ListNode(0, head);
		ListNode prev = dummy;
		while (prev.next != null)
		{
			if (count[prev.next.val] > 1) prev.next = prev.next.next;   // remove it
			else prev = prev.next;
		}
		return dummy.next;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: One pass with a dummy node and a 'prev' pointer (the last node we are sure to keep).
	// 1. dummy -> head. prev = dummy, curr = head.
	// 2. If curr starts a group of duplicates (curr.val == curr.next.val):
	//    move curr to the LAST node of that group, then skip the whole group: prev.next = curr.next.
	//    (Don't move prev: the next group may also be duplicates.)
	//    Else curr is unique: keep it, prev = curr.
	// 3. curr = curr.next, and repeat.
	// e.g., 1 2 3 3 4 4 5:  keep 1, keep 2, skip 3 3 (2 -> 4), skip 4 4 (2 -> 5), keep 5  ->  1 2 5
	// T.C: O(n)
	// S.C: O(1)
	public ListNode DeleteDuplicates(ListNode head)
	{
		ListNode dummy = new ListNode(0, head);
		ListNode prev = dummy, curr = head;

		while (curr != null)
		{
			if (curr.next != null && curr.val == curr.next.val)
			{
				while (curr.next != null && curr.val == curr.next.val) curr = curr.next;   // last node of the group
				prev.next = curr.next;                                                     // skip the whole group
			}
			else prev = curr;

			curr = curr.next;
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

	static void Main()
	{
		Console.WriteLine(Print(new Solution().DeleteDuplicates_Count(Build(1, 2, 3, 3, 4, 4, 5))));   // o/p: 1 -> 2 -> 5
		Console.WriteLine(Print(new Solution().DeleteDuplicates(Build(1, 2, 3, 3, 4, 4, 5))));         // o/p: 1 -> 2 -> 5
	}
}

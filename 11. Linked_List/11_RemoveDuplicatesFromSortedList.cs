// Leetcode: 83. Remove Duplicates from Sorted List   --->   Given the head of a sorted linked list, delete all
// duplicates such that each element appears only once. Return the linked list sorted as well.

// Example 1:
// Input: head = [1,1,2]
// Output: [1,2]

// Example 2:
// Input: head = [1,1,2,3,3]
// Output: [1,2,3]


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
	// Brute force (works even if the list is NOT sorted): Remember the values seen so far in a HashSet. If the next node's
	// value was already seen, skip that node.
	// T.C: O(n)
	// S.C: O(n);   for the HashSet
	public ListNode DeleteDuplicates_HashSet(ListNode head)
	{
		if (head == null) return null;

		HashSet<int> seen = new HashSet<int> { head.val };
		ListNode curr = head;

		while (curr.next != null)
		{
			if (seen.Contains(curr.next.val)) curr.next = curr.next.next;   // skip the duplicate
			else
			{
				seen.Add(curr.next.val);
				curr = curr.next;
			}
		}
		return head;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: The list is sorted, so duplicates always sit right next to each other. Compare each node with the
	// next one only:
	// -> curr.val == curr.next.val: the next node is a duplicate. Skip it: curr.next = curr.next.next.
	//    (Don't move curr yet: the new next node can be another duplicate, e.g., 1 -> 1 -> 1.)
	// -> Else: move curr ahead.
	// e.g., 1 -> 1 -> 2 -> 3 -> 3:  skip the second 1  ->  1 -> 2 -> 3 -> 3  ->  skip the second 3  ->  1 -> 2 -> 3
	// T.C: O(n)
	// S.C: O(1)
	public ListNode DeleteDuplicates(ListNode head)
	{
		ListNode curr = head;

		while (curr != null && curr.next != null)
		{
			if (curr.val == curr.next.val) curr.next = curr.next.next;
			else curr = curr.next;
		}

		return head;
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
		Console.WriteLine(Print(new Solution().DeleteDuplicates_HashSet(Build(1, 1, 2, 3, 3))));   // o/p: 1 -> 2 -> 3
		Console.WriteLine(Print(new Solution().DeleteDuplicates(Build(1, 1, 2, 3, 3))));           // o/p: 1 -> 2 -> 3
	}
}

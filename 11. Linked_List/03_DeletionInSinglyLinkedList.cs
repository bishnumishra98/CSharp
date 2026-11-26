// Deletion in a Singly Linked List   --->   Delete the node at the head, at the tail, or at a given position
// (positions start from 1).

// Example:
// Input: 2 -> 4 -> 6 -> 8 -> 10,  delete head, delete tail, then delete position 2
// Output: 4 -> 8

// How do we "delete" a node ? Just UNLINK it: make the node before it point to the node after it. Nothing points to it
// any more, so C#'s Garbage Collector frees its memory automatically. (C++ needs 'delete node' for this.)
//
//   before:  [2] -> [4] -> [6] -> [8]          delete 6:  prev = 4,  prev.next = prev.next.next
//   after:   [2] -> [4] --------> [8]          [6] is not reachable any more


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
	// 1. Delete head: the second node becomes the new head.
	// T.C: O(1)
	// S.C: O(1)
	public ListNode DeleteHead(ListNode head)
	{
		if (head == null) return null;   // nothing to delete
		return head.next;
	}

// ------------------------------------------------------------------------------

	// 2. Delete tail: walk to the SECOND LAST node, and set its next to null.
	//    (In a singly list we can't go back from the tail, so we must stop one node earlier.)
	//    If there is only 1 node, the list becomes empty.
	// T.C: O(n)
	// S.C: O(1)
	public ListNode DeleteTail(ListNode head)
	{
		if (head == null || head.next == null) return null;

		ListNode secondLast = head;
		while (secondLast.next.next != null) secondLast = secondLast.next;

		secondLast.next = null;
		return head;
	}

// ------------------------------------------------------------------------------

	// 3. Delete at a position (1-based):
	//    -> position == 1: delete head.
	//    -> Else walk to the node just BEFORE it ('prev', at position - 1), and skip over the node: prev.next = prev.next.next
	//    -> If the position doesn't exist (too big or < 1), change nothing.
	// T.C: O(position)
	// S.C: O(1)
	public ListNode DeleteAtPosition(ListNode head, int position)
	{
		if (head == null || position < 1) return head;
		if (position == 1) return head.next;

		ListNode prev = head;
		for (int i = 1; i < position - 1 && prev != null; i++) prev = prev.next;

		if (prev == null || prev.next == null) return head;   // position is beyond the list
		prev.next = prev.next.next;
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
		Solution sol = new Solution();
		ListNode head = Build(2, 4, 6, 8, 10);

		head = sol.DeleteHead(head);           // 4 -> 6 -> 8 -> 10
		head = sol.DeleteTail(head);           // 4 -> 6 -> 8
		head = sol.DeleteAtPosition(head, 2);  // 4 -> 8

		Console.WriteLine(Print(head));   // o/p: 4 -> 8
	}
}

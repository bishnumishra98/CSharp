// Insertion in a Singly Linked List   --->   Insert a new node at the head, at the tail, or at a given position
// (positions start from 1).

// Example:
// Input: 2 -> 4 -> 6 -> 8 -> 10,  insert 50 at head, 60 at tail, 55 at position 3
// Output: 50 -> 2 -> 55 -> 4 -> 6 -> 8 -> 10 -> 60

// Note: Inserting at the head CHANGES the head. So every method returns the (possibly new) head, and the caller writes
//       head = InsertAtHead(head, 50);   (Leetcode style.)
//       Another way is 'ref ListNode head', which lets the method change the caller's variable directly, like
//       'Node*& head' in C++.

// Note: The order of the 2 link changes matters. Always attach the NEW node first, then change the old links.
//       If we change prev.next first, we lose the rest of the list.


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
	// 1. Insert at head:
	//    newNode.next = head  ->  newNode becomes the new head.
	//    [50] -> [2] -> [4] -> ...
	// T.C: O(1)
	// S.C: O(1)
	public ListNode InsertAtHead(ListNode head, int val)
	{
		ListNode newNode = new ListNode(val);
		newNode.next = head;
		return newNode;   // new head
	}

// ------------------------------------------------------------------------------

	// 2. Insert at tail:
	//    Walk to the last node (whose next is null), and set last.next = newNode.
	//    If the list is empty, the new node is the whole list.
	// T.C: O(n);   to reach the tail (O(1) if we keep a 'tail' reference)
	// S.C: O(1)
	public ListNode InsertAtTail(ListNode head, int val)
	{
		ListNode newNode = new ListNode(val);
		if (head == null) return newNode;

		ListNode tail = head;
		while (tail.next != null) tail = tail.next;

		tail.next = newNode;
		return head;
	}

// ------------------------------------------------------------------------------

	// 3. Insert at a position (1-based):
	//    -> position <= 1: same as insert at head.
	//    -> Else walk to the node just BEFORE the position (position - 1), call it 'prev'.
	//       newNode.next = prev.next   (attach the new node to the rest of the list first)
	//       prev.next = newNode        (then link prev to the new node)
	//    -> If the list is shorter than position - 1, insert at the tail.
	// e.g., insert 55 at position 3 in 2 -> 4 -> 6:  prev = 4.  55 -> 6, then 4 -> 55.  Result: 2 -> 4 -> 55 -> 6
	// T.C: O(position)
	// S.C: O(1)
	public ListNode InsertAtPosition(ListNode head, int val, int position)
	{
		if (position <= 1 || head == null) return InsertAtHead(head, val);

		ListNode prev = head;
		for (int i = 1; i < position - 1 && prev.next != null; i++) prev = prev.next;

		ListNode newNode = new ListNode(val);
		newNode.next = prev.next;
		prev.next = newNode;
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

		head = sol.InsertAtHead(head, 50);
		head = sol.InsertAtTail(head, 60);
		head = sol.InsertAtPosition(head, 55, 3);

		Console.WriteLine(Print(head));   // o/p: 50 -> 2 -> 55 -> 4 -> 6 -> 8 -> 10 -> 60
	}
}

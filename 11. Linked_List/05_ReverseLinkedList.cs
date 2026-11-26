// Leetcode: 206. Reverse Linked List   --->   Given the head of a singly linked list, reverse the list, and return the
// reversed list.

// Example 1:
// Input: head = [1,2,3,4,5]
// Output: [5,4,3,2,1]

// Example 2:
// Input: head = [1,2]
// Output: [2,1]

// Example 3:
// Input: head = []
// Output: []


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
	// Brute force: Reverse the VALUES using a stack (last in, first out).
	// 1. Walk the list and push every value onto a stack.
	// 2. Walk again and overwrite each node's value with stack.Pop(). The stack gives them back in reverse order.
	// The nodes stay where they are, only their values change.
	// T.C: O(2n) = O(n)
	// S.C: O(n);   for the stack
	public ListNode ReverseList_Stack(ListNode head)
	{
		Stack<int> stack = new Stack<int>();
		for (ListNode t = head; t != null; t = t.next) stack.Push(t.val);
		for (ListNode t = head; t != null; t = t.next) t.val = stack.Pop();
		return head;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm (iterative): Turn every arrow around, one node at a time.
	// Keep 3 references: prev (already reversed part), curr (node being turned), next (rest of the list).
	// For every node:
	//   1. next = curr.next       (save the rest, as we are about to break this link)
	//   2. curr.next = prev       (turn the arrow backwards)
	//   3. prev = curr            (move prev ahead)
	//   4. curr = next            (move curr ahead)
	// When curr becomes null, prev is the old tail = the new head.
	// e.g., 1 -> 2 -> 3:
	//       null <- 1    2 -> 3        prev = 1, curr = 2
	//       null <- 1 <- 2    3        prev = 2, curr = 3
	//       null <- 1 <- 2 <- 3        prev = 3, curr = null  ->  new head = 3
	// T.C: O(n)
	// S.C: O(1)
	public ListNode ReverseList(ListNode head)
	{
		ListNode prev = null, curr = head;

		while (curr != null)
		{
			ListNode next = curr.next;
			curr.next = prev;
			prev = curr;
			curr = next;
		}

		return prev;
	}

// ------------------------------------------------------------------------------

	// Recursive approach: "Reverse the rest, then attach myself at the end."
	// 1. Base case: an empty list or a single node is already reversed. Return it.
	// 2. Reverse everything after head: newHead = ReverseList_Recursive(head.next).
	//    Now head.next (the old second node) is the LAST node of the reversed part.
	// 3. Put head after it: head.next.next = head, and cut head's old link: head.next = null.
	// e.g., 1 -> 2 -> 3:  reverse(2 -> 3) gives 3 -> 2, and 1 still points to 2.  2.next = 1, 1.next = null  ->  3 -> 2 -> 1
	// T.C: O(n)
	// S.C: O(n);   recursion stack
	public ListNode ReverseList_Recursive(ListNode head)
	{
		if (head == null || head.next == null) return head;

		ListNode newHead = ReverseList_Recursive(head.next);
		head.next.next = head;
		head.next = null;

		return newHead;
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
		Console.WriteLine(Print(new Solution().ReverseList_Stack(Build(1, 2, 3, 4, 5))));       // o/p: 5 -> 4 -> 3 -> 2 -> 1
		Console.WriteLine(Print(new Solution().ReverseList(Build(1, 2, 3, 4, 5))));             // o/p: 5 -> 4 -> 3 -> 2 -> 1
		Console.WriteLine(Print(new Solution().ReverseList_Recursive(Build(1, 2, 3, 4, 5))));   // o/p: 5 -> 4 -> 3 -> 2 -> 1
	}
}

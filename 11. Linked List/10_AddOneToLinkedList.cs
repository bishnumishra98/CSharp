// GFG: Add 1 to a Linked List Number   --->   A number is stored in a linked list, one digit per node, with the most
// significant digit first (e.g., 4 -> 5 -> 6 is 456). Add 1 to the number and return the head of the new list.

// Example 1:
// Input: 4 -> 5 -> 6
// Output: 4 -> 5 -> 7

// Example 2:
// Input: 1 -> 5 -> 9
// Output: 1 -> 6 -> 0

// Example 3:
// Input: 9 -> 9 -> 9
// Output: 1 -> 0 -> 0 -> 0   (a new node is needed at the front)

// Problem link: https://www.geeksforgeeks.org/problems/add-1-to-a-number-represented-as-linked-list/1

// Why not convert the list to a number, add 1, and convert back ? The list can have 100s of digits, far too big for
// int or long. So add digit by digit, like on paper.
// The problem: Addition starts from the LAST digit, but a singly linked list can only be walked from the FIRST.


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
	// Same as '05_ReverseLinkedList.cs'.
	private ListNode Reverse(ListNode head)
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

	// Approach 1: Reverse, add, reverse back.
	// 1. Reverse the list, so the last digit comes first.
	// 2. Walk with carry = 1 (the 1 we add): digit = val + carry, write digit % 10, carry = digit / 10.
	//    Once carry becomes 0, nothing else changes, so we can stop early.
	// 3. If carry is still 1 after the last node (all digits were 9), add a new node with 1.
	// 4. Reverse back.
	// e.g., 1 -> 5 -> 9:  reversed 9 -> 5 -> 1.  9 + 1 = 10 -> 0, carry 1.  5 + 1 = 6, carry 0, stop.  0 -> 6 -> 1
	//       reversed back: 1 -> 6 -> 0
	// T.C: O(3n) = O(n);   2 reversals + 1 walk
	// S.C: O(1)
	public ListNode AddOne_Reverse(ListNode head)
	{
		head = Reverse(head);

		int carry = 1;
		ListNode curr = head, last = null;
		while (curr != null && carry > 0)
		{
			int sum = curr.val + carry;
			curr.val = sum % 10;
			carry = sum / 10;
			last = curr;
			curr = curr.next;
		}

		if (carry > 0) last.next = new ListNode(carry);   // all digits were 9

		return Reverse(head);
	}

// ------------------------------------------------------------------------------

	// Approach 2: Recursion (no reversing).
	// Recursion goes down to the last node first, and does its work on the way BACK UP, i.e., from the last digit to the
	// first. That is exactly the order addition needs.
	// AddCarry(node) returns the carry coming out of this node:
	// • Base case: past the last node (null), return 1 (the 1 we add).
	// • Else: carry = AddCarry(node.next); sum = node.val + carry; node.val = sum % 10; return sum / 10.
	// If the first node still gives a carry, put a new node with 1 in front.
	// T.C: O(n)
	// S.C: O(n);   recursion stack
	public ListNode AddOne(ListNode head)
	{
		int carry = AddCarry(head);
		if (carry > 0) return new ListNode(carry, head);
		return head;
	}

	private int AddCarry(ListNode node)
	{
		if (node == null) return 1;

		int sum = node.val + AddCarry(node.next);
		node.val = sum % 10;
		return sum / 10;
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
		return string.Join(" -> ", values);
	}

	static void Main()
	{
		Console.WriteLine(Print(new Solution().AddOne_Reverse(Build(9, 9, 9))));   // o/p: 1 -> 0 -> 0 -> 0
		Console.WriteLine(Print(new Solution().AddOne(Build(9, 9, 9))));           // o/p: 1 -> 0 -> 0 -> 0
	}
}

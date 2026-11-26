// Leetcode: 445. Add Two Numbers II   --->   You are given two non-empty linked lists representing two non-negative
// integers. The most significant digit comes first and each of their nodes contains a single digit. Add the two numbers
// and return the sum as a linked list.
// You may assume the two numbers do not contain any leading zero, except the number 0 itself.

// Example 1:
// Input: l1 = [7,2,4,3], l2 = [5,6,4]
// Output: [7,8,0,7]
// Explanation: 7243 + 564 = 7807

// Example 2:
// Input: l1 = [2,4,3], l2 = [5,6,4]
// Output: [8,0,7]

// Example 3:
// Input: l1 = [0], l2 = [0]
// Output: [0]

// Follow up: Could you solve it without reversing the input lists?
// (Leetcode 2. Add Two Numbers is the same problem, but the digits are already stored in REVERSE order. That is just the
//  middle step of approach 1 below, without any reversing.)


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
	// Approach 1: Reverse both lists, add digit by digit from the front (which is now the last digit), reverse the answer.
	// Adding: sum = digit1 + digit2 + carry. Write sum % 10, carry = sum / 10. Keep going while EITHER list has digits
	// or a carry is left.
	// Problem: It changes (reverses) the input lists, which the follow-up doesn't allow.
	// T.C: O(n1 + n2)
	// S.C: O(1);   ignoring the answer list
	public ListNode AddTwoNumbers_Reverse(ListNode l1, ListNode l2)
	{
		l1 = Reverse(l1);
		l2 = Reverse(l2);

		ListNode dummy = new ListNode();
		ListNode tail = dummy;
		int carry = 0;

		while (l1 != null || l2 != null || carry > 0)
		{
			int sum = carry;
			if (l1 != null) { sum += l1.val; l1 = l1.next; }
			if (l2 != null) { sum += l2.val; l2 = l2.next; }

			tail.next = new ListNode(sum % 10);
			tail = tail.next;
			carry = sum / 10;
		}

		return Reverse(dummy.next);
	}

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

// ------------------------------------------------------------------------------

	// Approach 2 (follow-up): Two stacks, the input lists are not changed.
	// A stack gives items back in reverse order (last in, first out), so pushing the digits and popping them gives the
	// last digit first, without reversing the lists.
	// Build the answer from the back: each new digit node goes in FRONT of the answer (newNode.next = ans), so no reversing
	// is needed at the end either.
	// e.g., 7243 + 564:  3 + 4 = 7 -> [7].  4 + 6 = 10 -> 0, carry 1 -> [0, 7].  2 + 5 + 1 = 8 -> [8, 0, 7].  7 -> [7, 8, 0, 7]
	// T.C: O(n1 + n2)
	// S.C: O(n1 + n2);   for the 2 stacks
	public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
	{
		Stack<int> s1 = new Stack<int>(), s2 = new Stack<int>();
		for (ListNode t = l1; t != null; t = t.next) s1.Push(t.val);
		for (ListNode t = l2; t != null; t = t.next) s2.Push(t.val);

		ListNode ans = null;
		int carry = 0;

		while (s1.Count > 0 || s2.Count > 0 || carry > 0)
		{
			int sum = carry;
			if (s1.Count > 0) sum += s1.Pop();
			if (s2.Count > 0) sum += s2.Pop();

			ans = new ListNode(sum % 10, ans);   // put the new digit in front
			carry = sum / 10;
		}

		return ans;
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
		Console.WriteLine(Print(new Solution().AddTwoNumbers_Reverse(Build(7, 2, 4, 3), Build(5, 6, 4))));   // o/p: 7 -> 8 -> 0 -> 7
		Console.WriteLine(Print(new Solution().AddTwoNumbers(Build(7, 2, 4, 3), Build(5, 6, 4))));           // o/p: 7 -> 8 -> 0 -> 7
	}
}

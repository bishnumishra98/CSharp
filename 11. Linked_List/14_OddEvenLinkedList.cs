// Leetcode: 328. Odd Even Linked List   --->   Given the head of a singly linked list, group all the nodes with odd
// indices together followed by the nodes with even indices, and return the reordered list.
// The first node is considered odd, and the second node is even, and so on.
// Note that the relative order inside both the even and odd groups should remain as it was in the input.
// You must solve the problem in O(1) extra space complexity and O(n) time complexity.

// Example 1:
// Input: head = [1,2,3,4,5]
// Output: [1,3,5,2,4]

// Example 2:
// Input: head = [2,1,3,5,6,4,7]
// Output: [2,3,6,7,1,5,4]

// Note: "odd/even" is about the node's POSITION (1st, 2nd, 3rd ...), not its value.


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
	// Brute force: Copy the values at odd positions, then the values at even positions, into a List. Then write them back
	// into the nodes in that order.
	// T.C: O(n)
	// S.C: O(n);   for the List (the problem asks for O(1), so this is only a first idea)
	public ListNode OddEvenList_Values(ListNode head)
	{
		List<int> values = new List<int>();
		int pos = 1;
		for (ListNode t = head; t != null; t = t.next, pos++) if (pos % 2 == 1) values.Add(t.val);
		pos = 1;
		for (ListNode t = head; t != null; t = t.next, pos++) if (pos % 2 == 0) values.Add(t.val);

		int i = 0;
		for (ListNode t = head; t != null; t = t.next) t.val = values[i++];
		return head;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Split the list into 2 chains (odd nodes and even nodes) by re-linking, then join them.
	// 1. odd = 1st node, even = 2nd node. Remember evenHead (the 2nd node), as the odd chain will end by pointing to it.
	// 2. While the even chain can take another step:
	//    odd.next = even.next   (the next odd node is the one right after the current even node)   odd = odd.next
	//    even.next = odd.next   (the next even node is the one right after the new odd node)        even = even.next
	// 3. Join: odd.next = evenHead.
	// e.g., 1 2 3 4 5:  odd chain 1 -> 3 -> 5,  even chain 2 -> 4.  Join: 1 -> 3 -> 5 -> 2 -> 4
	// T.C: O(n)
	// S.C: O(1)
	public ListNode OddEvenList(ListNode head)
	{
		if (head == null || head.next == null) return head;

		ListNode odd = head, even = head.next, evenHead = head.next;

		while (even != null && even.next != null)
		{
			odd.next = even.next;
			odd = odd.next;
			even.next = odd.next;
			even = even.next;
		}

		odd.next = evenHead;
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
		Console.WriteLine(Print(new Solution().OddEvenList_Values(Build(2, 1, 3, 5, 6, 4, 7))));   // o/p: 2 -> 3 -> 6 -> 7 -> 1 -> 5 -> 4
		Console.WriteLine(Print(new Solution().OddEvenList(Build(2, 1, 3, 5, 6, 4, 7))));          // o/p: 2 -> 3 -> 6 -> 7 -> 1 -> 5 -> 4
	}
}

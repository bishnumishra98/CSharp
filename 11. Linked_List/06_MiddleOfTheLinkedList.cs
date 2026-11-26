// Leetcode: 876. Middle of the Linked List   --->   Given the head of a singly linked list, return the middle node of
// the linked list.
// If there are two middle nodes, return the second middle node.

// Example 1:
// Input: head = [1,2,3,4,5]
// Output: [3,4,5]
// Explanation: The middle node of the list is node 3.

// Example 2:
// Input: head = [1,2,3,4,5,6]
// Output: [4,5,6]
// Explanation: Since the list has two middle nodes with values 3 and 4, we return the second one.

// Middle position (1-based) for n nodes = n / 2 + 1.   e.g., n = 5 -> 3rd node.   n = 6 -> 4th node (the second middle).


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
	// Brute force: Count the nodes (n), then walk n / 2 steps from head.
	// T.C: O(n + n/2);   2 passes
	// S.C: O(1)
	public ListNode MiddleNode_Count(ListNode head)
	{
		int n = 0;
		for (ListNode t = head; t != null; t = t.next) n++;

		ListNode mid = head;
		for (int i = 0; i < n / 2; i++) mid = mid.next;
		return mid;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Slow and fast pointers (tortoise and hare), in ONE pass.
	// 'slow' moves 1 step, 'fast' moves 2 steps at a time. When fast reaches the end, slow has covered half the distance,
	// i.e., it is at the middle.
	// Loop while (fast != null && fast.next != null), i.e., while fast can still take 2 steps.
	// e.g., 1 2 3 4 5:  (slow, fast) = (1, 1) -> (2, 3) -> (3, 5). fast.next is null, stop. Middle = 3.
	//       1 2 3 4 5 6: (1, 1) -> (2, 3) -> (3, 5) -> (4, null). Stop. Middle = 4 (the second middle).
	// (To get the FIRST middle in an even list, loop while (fast.next != null && fast.next.next != null) instead.
	//  That version is used in '15_PalindromeLinkedList.cs' and '19_SortList.cs'.)
	// T.C: O(n/2) = O(n)
	// S.C: O(1)
	public ListNode MiddleNode(ListNode head)
	{
		ListNode slow = head, fast = head;

		while (fast != null && fast.next != null)
		{
			slow = slow.next;
			fast = fast.next.next;
		}

		return slow;
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

	static void Main()
	{
		ListNode head = Build(1, 2, 3, 4, 5);

		Console.WriteLine(new Solution().MiddleNode_Count(head).val);   // o/p: 3
		Console.WriteLine(new Solution().MiddleNode(head).val);         // o/p: 3
	}
}

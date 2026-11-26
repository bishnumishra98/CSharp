// Leetcode: 141. Linked List Cycle   --->   Given head, the head of a linked list, determine if the linked list has a
// cycle in it.
// There is a cycle in a linked list if there is some node in the list that can be reached again by continuously
// following the next pointer. Return true if there is a cycle in the linked list. Otherwise, return false.

// Example 1:
// Input: head = [3,2,0,-4], pos = 1   (the tail connects back to the node at index 1)
// Output: true
//
//   3 -> 2 -> 0 -> -4
//        ^          |
//        |__________|

// Example 2:
// Input: head = [1], pos = -1   (no cycle)
// Output: false

// Note: In a list with a cycle, a normal walk never reaches null. It goes round and round forever.


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
	// Brute force: Remember every node we visit in a HashSet. If we reach a node that is already in the set, we came back
	// to it, so there is a cycle. If we reach null, there is no cycle.
	// Note: The set stores the NODES (references), not their values. Two different nodes can have the same value.
	// T.C: O(n)
	// S.C: O(n);   for the HashSet
	public bool HasCycle_HashSet(ListNode head)
	{
		HashSet<ListNode> visited = new HashSet<ListNode>();

		for (ListNode t = head; t != null; t = t.next)
		{
			if (!visited.Add(t)) return true;   // Add() returns false if t is already in the set
		}
		return false;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Floyd's cycle detection (tortoise and hare).
	// 'slow' moves 1 step, 'fast' moves 2 steps.
	// • No cycle: fast reaches the end (null) first. Return false.
	// • Cycle: both get stuck going round the loop. Inside the loop, fast gains 1 step on slow every move, so the gap
	//   between them shrinks by 1 each time, and fast surely lands ON slow. When slow == fast, return true.
	//   (Like 2 runners on a circular track: the faster one always catches up from behind.)
	// T.C: O(n)
	// S.C: O(1)
	public bool HasCycle(ListNode head)
	{
		ListNode slow = head, fast = head;

		while (fast != null && fast.next != null)
		{
			slow = slow.next;
			fast = fast.next.next;
			if (slow == fast) return true;   // same node (reference), not just the same value
		}

		return false;
	}
}


class Program
{
	static void Main()
	{
		// 3 -> 2 -> 0 -> -4 -> back to 2
		ListNode head = new ListNode(3, new ListNode(2, new ListNode(0, new ListNode(-4))));
		head.next.next.next.next = head.next;

		Console.WriteLine(new Solution().HasCycle_HashSet(head));   // o/p: True
		Console.WriteLine(new Solution().HasCycle(head));           // o/p: True
	}
}

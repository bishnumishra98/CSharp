// GFG: Remove loop in Linked List   --->   Given the head of a linked list that may contain a loop. A loop means that
// the last node of the linked list is connected back to a node in the same list. If a loop exists, remove it so that
// the list becomes a normal linked list (the last node points to null). Do not change anything else.

// Example 1:
// Input: 10 -> 20 -> 30 -> 40 -> 50 -> 60 -> 70 -> 80, and 80 connects back to 30
// Output: 10 -> 20 -> 30 -> 40 -> 50 -> 60 -> 70 -> 80   (80.next is now null)

// Example 2:
// Input: 1 -> 3 -> 4, no loop
// Output: 1 -> 3 -> 4   (nothing changes)

// Problem link: https://www.geeksforgeeks.org/problems/remove-loop-in-linked-list/1
// First understand '08_LinkedListCycleII.cs'.


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
	// Brute force: Walk with a HashSet, and remember the previous node. The moment curr.next is a node we have already
	// visited, curr is the last node of the loop. Cut it: curr.next = null.
	// T.C: O(n)
	// S.C: O(n)
	public void RemoveLoop_HashSet(ListNode head)
	{
		HashSet<ListNode> visited = new HashSet<ListNode>();

		for (ListNode curr = head; curr != null; curr = curr.next)
		{
			visited.Add(curr);
			if (curr.next != null && visited.Contains(curr.next))
			{
				curr.next = null;   // curr is the last node of the loop
				return;
			}
		}
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Floyd's algorithm.
	// 1. Find the start of the loop ('08_LinkedListCycleII.cs'). If there is no loop, do nothing.
	// 2. The last node of the loop is the one whose next is the loop start. Walk round the loop from the start until
	//    node.next == start, then cut it: node.next = null.
	// (This also works when the loop starts at head itself, e.g., the last node points back to the first node.)
	// T.C: O(n)
	// S.C: O(1)
	public void RemoveLoop(ListNode head)
	{
		ListNode slow = head, fast = head;

		while (fast != null && fast.next != null)
		{
			slow = slow.next;
			fast = fast.next.next;
			if (slow == fast) break;
		}

		if (fast == null || fast.next == null) return;   // no loop: nothing to remove

		// Find the start of the loop
		slow = head;
		while (slow != fast)
		{
			slow = slow.next;
			fast = fast.next;
		}
		ListNode start = slow;

		// Find the last node of the loop, and cut it
		ListNode last = start;
		while (last.next != start) last = last.next;
		last.next = null;
	}
}


class Program
{
	static ListNode BuildWithLoop(int loopAt, params int[] values)   // loopAt = index the tail connects back to
	{
		ListNode[] nodes = new ListNode[values.Length];
		for (int i = 0; i < values.Length; i++) nodes[i] = new ListNode(values[i]);
		for (int i = 0; i < values.Length - 1; i++) nodes[i].next = nodes[i + 1];
		nodes[values.Length - 1].next = nodes[loopAt];
		return nodes[0];
	}

	static string Print(ListNode head)
	{
		List<int> values = new List<int>();
		for (ListNode t = head; t != null; t = t.next) values.Add(t.val);
		return string.Join(" -> ", values);
	}

	static void Main()
	{
		ListNode head1 = BuildWithLoop(2, 10, 20, 30, 40, 50, 60, 70, 80);
		new Solution().RemoveLoop_HashSet(head1);
		Console.WriteLine(Print(head1));   // o/p: 10 -> 20 -> 30 -> 40 -> 50 -> 60 -> 70 -> 80

		ListNode head2 = BuildWithLoop(2, 10, 20, 30, 40, 50, 60, 70, 80);
		new Solution().RemoveLoop(head2);
		Console.WriteLine(Print(head2));   // o/p: 10 -> 20 -> 30 -> 40 -> 50 -> 60 -> 70 -> 80
	}
}

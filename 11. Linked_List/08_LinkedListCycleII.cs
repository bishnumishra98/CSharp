// Leetcode: 142. Linked List Cycle II   --->   Given the head of a linked list, return the node where the cycle begins.
// If there is no cycle, return null.
// Do not modify the linked list.

// Example 1:
// Input: head = [10,20,30,40,50,60,70,80], the tail (80) connects back to 30
// Output: the node with value 30
//
//   10 -> 20 -> 30 -> 40 -> 50 -> 60 -> 70 -> 80
//               ^                             |
//               |_____________________________|

// Example 2:
// Input: head = [1], no cycle
// Output: null

// First understand '07_LinkedListCycle.cs'.


// Dry run image (in this folder): 08_LinkedListCycleII.jpg

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
	// Brute force: Walk and remember every node in a HashSet. The FIRST node we see for the second time is where the cycle
	// begins (we entered the loop there, went round, and came back to it).
	// T.C: O(n)
	// S.C: O(n)
	public ListNode DetectCycle_HashSet(ListNode head)
	{
		HashSet<ListNode> visited = new HashSet<ListNode>();

		for (ListNode t = head; t != null; t = t.next)
		{
			if (!visited.Add(t)) return t;
		}
		return null;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Floyd's algorithm, in 2 phases.
	// Phase 1: Move slow (1 step) and fast (2 steps) until they meet (cycle found), or fast reaches null (no cycle).
	// Phase 2: Put slow back at head. Keep fast where they met. Now move BOTH 1 step at a time. They meet exactly at the
	//          start of the cycle.
	//
	// Why does phase 2 work ? (Proof in the image.)
	//   A = distance from head to the cycle start
	//   B = distance from the cycle start to the meeting point (going forward)
	//   C = length of the cycle
	//   When they meet, slow has walked A + B (+ some full rounds), and fast has walked twice that. The extra distance fast
	//   walked is a whole number of rounds, so:  A + B = k * C  (for some whole number k),  i.e.,  A = k * C - B.
	//   In phase 2, slow walks A steps from head to the cycle start. In the same A steps, fast walks k * C - B from the
	//   meeting point: (C - B) brings it to the cycle start, and the remaining full rounds bring it back there again.
	//   So both arrive at the cycle start at the same moment.
	// e.g., the list above: A = 2 (10 -> 20 -> 30), C = 6 (30 to 80). Slow and fast meet at 70, so B = 4.
	//       A + B = 6 = 1 * C.  Phase 2: slow 10 -> 20 -> 30,  fast 70 -> 80 -> 30.  Both reach 30.
	// T.C: O(n)
	// S.C: O(1)
	public ListNode DetectCycle(ListNode head)
	{
		ListNode slow = head, fast = head;

		while (fast != null && fast.next != null)
		{
			slow = slow.next;
			fast = fast.next.next;

			if (slow == fast)   // phase 1 done: cycle exists
			{
				slow = head;    // phase 2
				while (slow != fast)
				{
					slow = slow.next;
					fast = fast.next;
				}
				return slow;
			}
		}

		return null;   // fast reached the end: no cycle
	}
}


class Program
{
	static void Main()
	{
		// 10 -> 20 -> ... -> 80 -> back to 30
		ListNode[] nodes = new ListNode[8];
		for (int i = 0; i < 8; i++) nodes[i] = new ListNode((i + 1) * 10);
		for (int i = 0; i < 7; i++) nodes[i].next = nodes[i + 1];
		nodes[7].next = nodes[2];
		ListNode head = nodes[0];

		Console.WriteLine(new Solution().DetectCycle_HashSet(head).val);   // o/p: 30
		Console.WriteLine(new Solution().DetectCycle(head).val);           // o/p: 30
	}
}

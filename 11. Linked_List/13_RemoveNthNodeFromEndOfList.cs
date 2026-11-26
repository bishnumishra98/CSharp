// Leetcode: 19. Remove Nth Node From End of List   --->   Given the head of a linked list, remove the nth node from the
// end of the list and return its head.

// Example 1:
// Input: head = [1,2,3,4,5], n = 2
// Output: [1,2,3,5]

// Example 2:
// Input: head = [1], n = 1
// Output: []

// Example 3:
// Input: head = [1,2], n = 1
// Output: [1]

// Constraints: 1 <= n <= length of the list
// Follow up: Could you do this in one pass?

// Note: If n == length, the node to remove is the HEAD. A dummy node in front of head handles this like any other case:
//       we always need the node BEFORE the one we remove, and the dummy is the node before head.


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
	// Brute force: Two passes.
	// 1. Count the length L.
	// 2. The nth node from the end is the (L - n + 1)th node from the start. So the node BEFORE it is at position L - n.
	//    Walk L - n steps from the dummy (which is position 0) to reach it, and skip the next node.
	// T.C: O(2L) in the worst case
	// S.C: O(1)
	public ListNode RemoveNthFromEnd_TwoPass(ListNode head, int n)
	{
		int length = 0;
		for (ListNode t = head; t != null; t = t.next) length++;

		ListNode dummy = new ListNode(0, head);
		ListNode prev = dummy;
		for (int i = 0; i < length - n; i++) prev = prev.next;

		prev.next = prev.next.next;
		return dummy.next;   // not 'head', as head itself may have been removed
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: One pass with a gap of n between two pointers.
	// 1. Start 'fast' and 'slow' at the dummy. Move fast n steps ahead first.
	// 2. Move both 1 step at a time until fast is on the LAST node. The gap between them stays n, so slow ends just
	//    before the nth node from the end.
	// 3. Skip it: slow.next = slow.next.next.
	// e.g., 1 2 3 4 5, n = 2:  fast moves to 2.  Then both move: (slow, fast) = (1, 3), (2, 4), (3, 5). fast is last.
	//       slow = 3, so remove 3.next = 4  ->  1 2 3 5
	// T.C: O(L)
	// S.C: O(1)
	public ListNode RemoveNthFromEnd(ListNode head, int n)
	{
		ListNode dummy = new ListNode(0, head);
		ListNode slow = dummy, fast = dummy;

		for (int i = 0; i < n; i++) fast = fast.next;

		while (fast.next != null)
		{
			slow = slow.next;
			fast = fast.next;
		}

		slow.next = slow.next.next;
		return dummy.next;
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
		Console.WriteLine(Print(new Solution().RemoveNthFromEnd_TwoPass(Build(1, 2, 3, 4, 5), 2)));   // o/p: 1 -> 2 -> 3 -> 5
		Console.WriteLine(Print(new Solution().RemoveNthFromEnd(Build(1, 2, 3, 4, 5), 2)));           // o/p: 1 -> 2 -> 3 -> 5
	}
}

// Leetcode: 61. Rotate List   --->   Given the head of a linked list, rotate the list to the right by k places.

// Example 1:
// Input: head = [1,2,3,4,5], k = 2
// Output: [4,5,1,2,3]
// Explanation: rotate 1 step: [5,1,2,3,4],  rotate 2 steps: [4,5,1,2,3]

// Example 2:
// Input: head = [0,1,2], k = 4
// Output: [2,0,1]

// Constraints: 0 <= k <= 2 * 10^9
// Key fact: Rotating a list of length n by n places gives the same list back. So rotating by k is the same as rotating
//           by k % n. (k can be 2 billion, so this matters a lot.)


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
	// Brute force: Rotate 1 step at a time, (k % n) times. One step = move the last node to the front.
	// T.C: O(n * (k % n)) = O(n^2) worst case;   each step walks to the second last node
	// S.C: O(1)
	public ListNode RotateRight_OneByOne(ListNode head, int k)
	{
		if (head == null || head.next == null) return head;

		int n = 0;
		for (ListNode t = head; t != null; t = t.next) n++;

		for (int step = 0; step < k % n; step++)
		{
			ListNode secondLast = head;
			while (secondLast.next.next != null) secondLast = secondLast.next;

			ListNode last = secondLast.next;
			secondLast.next = null;
			last.next = head;
			head = last;
		}
		return head;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Make a circle, then cut it at the right place.
	// 1. Walk to the tail and count the length n (one pass).
	// 2. k = k % n. If k == 0, nothing changes.
	// 3. Join tail to head (now it's a circle).
	// 4. The new tail is the (n - k)th node from the start, and the node after it is the new head. Cut the circle there.
	// e.g., 1 2 3 4 5, k = 2:  n = 5.  Circle: 1 2 3 4 5 1 2 ...  New tail = 3rd node (3), new head = 4.
	//       Cut after 3:  4 -> 5 -> 1 -> 2 -> 3
	// T.C: O(n)
	// S.C: O(1)
	public ListNode RotateRight(ListNode head, int k)
	{
		if (head == null || head.next == null) return head;

		ListNode tail = head;
		int n = 1;
		while (tail.next != null)
		{
			tail = tail.next;
			n++;
		}

		k = k % n;
		if (k == 0) return head;

		tail.next = head;   // make a circle

		ListNode newTail = head;
		for (int i = 1; i < n - k; i++) newTail = newTail.next;

		ListNode newHead = newTail.next;
		newTail.next = null;   // cut the circle
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
		Console.WriteLine(Print(new Solution().RotateRight_OneByOne(Build(1, 2, 3, 4, 5), 2)));   // o/p: 4 -> 5 -> 1 -> 2 -> 3
		Console.WriteLine(Print(new Solution().RotateRight(Build(1, 2, 3, 4, 5), 2)));            // o/p: 4 -> 5 -> 1 -> 2 -> 3
	}
}

// Leetcode: 25. Reverse Nodes in k-Group   --->   Given the head of a linked list, reverse the nodes of the list k at a
// time, and return the modified list. k is a positive integer and is less than or equal to the length of the linked
// list. If the number of nodes is not a multiple of k then left-out nodes, in the end, should remain as it is.
// You may not alter the values in the list's nodes, only nodes themselves may be changed.

// Example 1:
// Input: head = [1,2,3,4,5], k = 2
// Output: [2,1,4,3,5]

// Example 2:
// Input: head = [1,2,3,4,5], k = 3
// Output: [3,2,1,4,5]

// Follow-up: Can you solve the problem in O(1) extra memory space?
// Video link: https://www.youtube.com/watch?v=lIar1skcQYI&t=357s
// First understand '05_ReverseLinkedList.cs'.


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
	// Brute force: Copy the values into an array, reverse every full group of k inside the array, and write the values back.
	// Problem: The problem says "You may not alter the values", so this is NOT accepted. It only shows the idea.
	// T.C: O(n)
	// S.C: O(n);   for the array
	public ListNode ReverseKGroup_Values(ListNode head, int k)
	{
		List<int> values = new List<int>();
		for (ListNode t = head; t != null; t = t.next) values.Add(t.val);

		for (int start = 0; start + k <= values.Count; start += k) values.Reverse(start, k);   // reverse k items from 'start'

		int i = 0;
		for (ListNode t = head; t != null; t = t.next) t.val = values[i++];
		return head;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Cut out one group of k nodes at a time, reverse it, and join it back.
	// For each group:
	// 1. groupStart = first node of the group. Walk k - 1 steps to find kthNode (the last node of the group).
	//    If there are fewer than k nodes left, stop: the rest stays as it is (it is still linked to the previous group).
	// 2. Save nextGroupStart = kthNode.next, and cut the group off: kthNode.next = null.
	// 3. Reverse the group. Now kthNode is the group's FIRST node, and groupStart is its LAST node.
	// 4. Join it back:
	//    -> If this is the first group, the whole list's head becomes kthNode.
	//    -> Else, the previous group's last node points to kthNode.
	//    -> The group's new last node (groupStart) points to nextGroupStart (so the list stays connected, even if the
	//       rest is not reversed).
	// 5. prevGroupEnd = groupStart. Move on: groupStart = nextGroupStart.
	// e.g., 1 2 3 4 5, k = 2:  group [1 2] -> [2 1], head = 2.   group [3 4] -> [4 3], 1 -> 4.   [5] is too short, stays.
	//       2 -> 1 -> 4 -> 3 -> 5
	// T.C: O(2n) = O(n);   every node is walked once to find kthNode, and once when reversing
	// S.C: O(1)
	public ListNode ReverseKGroup(ListNode head, int k)
	{
		ListNode groupStart = head, prevGroupEnd = null;

		while (groupStart != null)
		{
			ListNode kthNode = groupStart;
			for (int i = 1; i < k && kthNode != null; i++) kthNode = kthNode.next;
			if (kthNode == null) break;   // fewer than k nodes left

			ListNode nextGroupStart = kthNode.next;
			kthNode.next = null;          // cut the group off

			Reverse(groupStart);          // now kthNode is the group's first node, groupStart is its last

			if (prevGroupEnd == null) head = kthNode;   // first group
			else prevGroupEnd.next = kthNode;
			groupStart.next = nextGroupStart;           // reconnect to the rest

			prevGroupEnd = groupStart;
			groupStart = nextGroupStart;
		}

		return head;
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
		Console.WriteLine(Print(new Solution().ReverseKGroup_Values(Build(1, 2, 3, 4, 5), 2)));   // o/p: 2 -> 1 -> 4 -> 3 -> 5
		Console.WriteLine(Print(new Solution().ReverseKGroup(Build(1, 2, 3, 4, 5), 2)));          // o/p: 2 -> 1 -> 4 -> 3 -> 5
	}
}

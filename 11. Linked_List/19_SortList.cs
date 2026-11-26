// Leetcode: 148. Sort List   --->   Given the head of a linked list, return the list after sorting it in ascending
// order.

// Example 1:
// Input: head = [4,2,1,3]
// Output: [1,2,3,4]

// Example 2:
// Input: head = [-1,5,3,4,0]
// Output: [-1,0,3,4,5]

// Example 3:
// Input: head = [10,50,60,40,30,20]
// Output: [10,20,30,40,50,60]

// Follow up: Can you sort the linked list in O(n logn) time and O(1) memory (i.e. constant space)?

// Why merge sort for a linked list ? Quick sort and heap sort need jumping to any index (arr[i]), which is slow in a
// linked list. Merge sort only walks forward and re-links nodes, which a linked list does well. Merging 2 sorted lists
// needs no extra array either (see '12_MergeTwoSortedLists.cs').


// Dry run image (in this folder): 19_SortList.jpg

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
	// Brute force: Copy the values into a List, sort it, and write the values back into the nodes in order.
	// T.C: O(nlogn)
	// S.C: O(n);   for the List
	public ListNode SortList_Values(ListNode head)
	{
		List<int> values = new List<int>();
		for (ListNode t = head; t != null; t = t.next) values.Add(t.val);
		values.Sort();

		int i = 0;
		for (ListNode t = head; t != null; t = t.next) t.val = values[i++];
		return head;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Merge sort on the list.
	// 1. Base case: 0 or 1 node is already sorted.
	// 2. Find the middle with slow/fast pointers. Use the FIRST middle for an even length, so that a 2-node list splits
	//    into 1 + 1. (With the second middle, [a, b] would split into [a, b] + [], and recurse forever.)
	// 3. Cut the list after the middle: rightHead = mid.next, mid.next = null.
	// 4. Sort both halves recursively, then merge them ('12_MergeTwoSortedLists.cs').
	// e.g., 10 50 60 40 30 20 -> [10 50 60] [40 30 20] -> ... -> [10 50 60] and [20 30 40] -> merge -> 10 20 30 40 50 60
	// T.C: O(nlogn);   the list is halved logn times, and each level merges n nodes in total
	// S.C: O(logn);   recursion stack (no extra array, unlike merge sort on an array)
	//      (The follow-up's O(1) space needs a bottom-up merge sort without recursion. It is much harder to write, and
	//       rarely expected in interviews.)
	public ListNode SortList(ListNode head)
	{
		if (head == null || head.next == null) return head;

		ListNode mid = FindFirstMiddle(head);
		ListNode rightHead = mid.next;
		mid.next = null;   // cut into 2 halves

		ListNode left = SortList(head);
		ListNode right = SortList(rightHead);
		return Merge(left, right);
	}

	private ListNode FindFirstMiddle(ListNode head)
	{
		ListNode slow = head, fast = head;
		while (fast.next != null && fast.next.next != null)
		{
			slow = slow.next;
			fast = fast.next.next;
		}
		return slow;
	}

	private ListNode Merge(ListNode p1, ListNode p2)
	{
		ListNode dummy = new ListNode();
		ListNode tail = dummy;

		while (p1 != null && p2 != null)
		{
			if (p1.val <= p2.val)
			{
				tail.next = p1;
				p1 = p1.next;
			}
			else
			{
				tail.next = p2;
				p2 = p2.next;
			}
			tail = tail.next;
		}

		tail.next = (p1 != null) ? p1 : p2;
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
		Console.WriteLine(Print(new Solution().SortList_Values(Build(10, 50, 60, 40, 30, 20))));   // o/p: 10 -> 20 -> 30 -> 40 -> 50 -> 60
		Console.WriteLine(Print(new Solution().SortList(Build(10, 50, 60, 40, 30, 20))));          // o/p: 10 -> 20 -> 30 -> 40 -> 50 -> 60
	}
}

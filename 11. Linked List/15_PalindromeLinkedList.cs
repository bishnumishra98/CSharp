// Leetcode: 234. Palindrome Linked List   --->   Given the head of a singly linked list, return true if it is a
// palindrome or false otherwise.

// Example 1:
// Input: head = [1,2,2,1]
// Output: true

// Example 2:
// Input: head = [1,2]
// Output: false

// Follow up: Could you do it in O(n) time and O(1) space?


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
	// Brute force: Copy the values into a List, then check it with two pointers.
	// (Same check as '04. Fundamental Programs\06_PalindromeString.cs'.)
	// T.C: O(n)
	// S.C: O(n);   for the List
	public bool IsPalindrome_List(ListNode head)
	{
		List<int> values = new List<int>();
		for (ListNode t = head; t != null; t = t.next) values.Add(t.val);

		for (int i = 0, j = values.Count - 1; i < j; i++, j--)
		{
			if (values[i] != values[j]) return false;
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Reverse the second half, and compare it with the first half.
	// 1. Find the end of the first half with slow/fast pointers ('06_MiddleOfTheLinkedList.cs'). For an even length, use
	//    the FIRST middle, so both halves have the same size. For an odd length, the middle node stays in the first half
	//    (the middle doesn't need to match anything).
	// 2. Reverse the second half ('05_ReverseLinkedList.cs').
	// 3. Walk both halves together and compare values. The second half is never longer, so stop when it ends.
	// 4. Reverse the second half back, to put the list back as it was. (Good practice: the caller may still use the list.)
	// e.g., 1 2 3 2 1:  first half 1 2 3,  second half 2 1 -> reversed 1 2.  Compare 1=1, 2=2. Palindrome.
	// T.C: O(n)
	// S.C: O(1)
	public bool IsPalindrome(ListNode head)
	{
		if (head == null || head.next == null) return true;

		// 1. End of the first half
		ListNode slow = head, fast = head;
		while (fast.next != null && fast.next.next != null)
		{
			slow = slow.next;
			fast = fast.next.next;
		}

		// 2. Reverse the second half
		ListNode secondHalf = Reverse(slow.next);

		// 3. Compare
		bool isPalindrome = true;
		for (ListNode p1 = head, p2 = secondHalf; p2 != null; p1 = p1.next, p2 = p2.next)
		{
			if (p1.val != p2.val)
			{
				isPalindrome = false;
				break;
			}
		}

		// 4. Restore the list
		slow.next = Reverse(secondHalf);

		return isPalindrome;
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

	static void Main()
	{
		ListNode head = Build(1, 2, 3, 2, 1);

		Console.WriteLine(new Solution().IsPalindrome_List(head));   // o/p: True
		Console.WriteLine(new Solution().IsPalindrome(head));        // o/p: True
	}
}

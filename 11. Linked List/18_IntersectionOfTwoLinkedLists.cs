// Leetcode: 160. Intersection of Two Linked Lists   --->   Given the heads of two singly linked-lists headA and headB,
// return the node at which the two lists intersect. If the two linked lists have no intersection at all, return null.
// The test cases are generated such that there are no cycles anywhere in the entire linked structure.
// Note that the linked lists must retain their original structure after the function returns.

// Example 1:
// Input: listA = [4,1,8,4,5], listB = [5,6,1,8,4,5], they share the nodes 8 -> 4 -> 5
// Output: Intersected at '8'
//
//   A:       4 -> 1 \
//                    8 -> 4 -> 5
//   B:  5 -> 6 -> 1 /
//
// Explanation: Note that the answer is not the node with value 1. Both lists have a node with value 1, but they are 2
// DIFFERENT nodes (different places in memory). From 8 onwards, both lists use the very SAME nodes.

// Example 2:
// Input: listA = [2,6,4], listB = [1,5]
// Output: No intersection

// So we compare nodes by REFERENCE (a == b means the same node object), never by value.


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
	// Brute force: Put every node of list A into a HashSet. Then walk list B. The first node of B that is in the set is
	// the intersection.
	// T.C: O(n1 + n2)
	// S.C: O(n1);   for the HashSet
	public ListNode GetIntersectionNode_HashSet(ListNode headA, ListNode headB)
	{
		HashSet<ListNode> nodesOfA = new HashSet<ListNode>();
		for (ListNode t = headA; t != null; t = t.next) nodesOfA.Add(t);

		for (ListNode t = headB; t != null; t = t.next)
		{
			if (nodesOfA.Contains(t)) return t;
		}
		return null;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Line up the two lists by their lengths.
	// After the intersection, both lists share the same tail part. So if both pointers are the same distance from the END,
	// they reach the intersection at the same moment.
	// 1. Find both lengths. Move the pointer of the longer list ahead by the difference.
	// 2. Now move both together, 1 step at a time, until they are the same node (or both null = no intersection).
	// e.g., A has 5 nodes, B has 6. Move B 1 step ahead (to 6). Then: (4, 6), (1, 1), (8, 8) -> same node. Answer 8.
	// T.C: O(n1 + n2)
	// S.C: O(1)
	public ListNode GetIntersectionNode_Length(ListNode headA, ListNode headB)
	{
		int lenA = 0, lenB = 0;
		for (ListNode t = headA; t != null; t = t.next) lenA++;
		for (ListNode t = headB; t != null; t = t.next) lenB++;

		ListNode a = headA, b = headB;
		for (int i = 0; i < lenA - lenB; i++) a = a.next;   // only runs if A is longer
		for (int i = 0; i < lenB - lenA; i++) b = b.next;   // only runs if B is longer

		while (a != b)
		{
			a = a.next;
			b = b.next;
		}
		return a;   // the intersection, or null if they never meet
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Switch heads (no counting needed).
	// Walk a through A, and when it ends, continue from the head of B. Walk b through B, and when it ends, continue from
	// the head of A.
	// Why does it work ? Let A = (a1 nodes before intersection) + (common part c), B = (b1 nodes) + c.
	//   Pointer a walks a1 + c + b1 steps to reach the intersection the second time round.
	//   Pointer b walks b1 + c + a1 steps. Same total! So they arrive at the intersection together.
	//   If there is no intersection, both reach null together after a1 + b1 steps (c = 0), and null == null ends the loop.
	// T.C: O(n1 + n2)
	// S.C: O(1)
	public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
	{
		ListNode a = headA, b = headB;

		while (a != b)
		{
			a = (a == null) ? headB : a.next;
			b = (b == null) ? headA : b.next;
		}

		return a;
	}
}


class Program
{
	static void Main()
	{
		ListNode common = new ListNode(8, new ListNode(4, new ListNode(5)));            // shared part
		ListNode headA = new ListNode(4, new ListNode(1, common));                      // 4 -> 1 -> 8 -> 4 -> 5
		ListNode headB = new ListNode(5, new ListNode(6, new ListNode(1, common)));     // 5 -> 6 -> 1 -> 8 -> 4 -> 5

		Console.WriteLine(new Solution().GetIntersectionNode_HashSet(headA, headB).val);   // o/p: 8
		Console.WriteLine(new Solution().GetIntersectionNode_Length(headA, headB).val);    // o/p: 8
		Console.WriteLine(new Solution().GetIntersectionNode(headA, headB).val);           // o/p: 8
	}
}

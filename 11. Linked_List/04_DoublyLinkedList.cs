// Doubly Linked List   --->   Every node has 2 links: 'prev' (to the node before it) and 'next' (to the node after it).
// So we can walk both forward and backward.
//
//   null <- [2] <=> [4] <=> [6] <=> [8] -> null
//           head                    tail
//
// ● Good: Delete a node in O(1) if we already have it (its prev is known, no need to walk to it).
//         Walk backward from the tail.
// ● Cost: One extra reference per node, and every insert/delete must update BOTH prev and next links.
// ● C#'s built-in LinkedList<T> is a doubly linked list. The LRU Cache ('24_LRUCache.cs') is built on this idea.

// Example:
// Input: empty list. Insert 2 at head, 4 at head, 6 at tail, 8 at position 3. Then delete position 2.
// Output: 4 -> 2 -> 8 -> 6  then  4 -> 8 -> 6


using System;
using System.Collections.Generic;

public class DListNode
{
	public int val;
	public DListNode prev, next;
	public DListNode(int val) { this.val = val; }
}

public class Solution
{
	// 1. Insert at head:  newNode.next = head,  head.prev = newNode,  newNode is the new head.
	// T.C: O(1)
	// S.C: O(1)
	public DListNode InsertAtHead(DListNode head, int val)
	{
		DListNode newNode = new DListNode(val);
		if (head == null) return newNode;

		newNode.next = head;
		head.prev = newNode;
		return newNode;
	}

// ------------------------------------------------------------------------------

	// 2. Insert at tail:  walk to the tail,  tail.next = newNode,  newNode.prev = tail.
	// T.C: O(n);   to reach the tail (O(1) if we keep a 'tail' reference)
	// S.C: O(1)
	public DListNode InsertAtTail(DListNode head, int val)
	{
		DListNode newNode = new DListNode(val);
		if (head == null) return newNode;

		DListNode tail = head;
		while (tail.next != null) tail = tail.next;

		tail.next = newNode;
		newNode.prev = tail;
		return head;
	}

// ------------------------------------------------------------------------------

	// 3. Insert at a position (1-based): walk to 'prev' (the node at position - 1). 'after' = prev.next.
	//    Now 4 links change:
	//      newNode.prev = prev,   newNode.next = after,   prev.next = newNode,   after.prev = newNode (if after exists)
	//    prev <=> after   becomes   prev <=> newNode <=> after
	// T.C: O(position)
	// S.C: O(1)
	public DListNode InsertAtPosition(DListNode head, int val, int position)
	{
		if (position <= 1 || head == null) return InsertAtHead(head, val);

		DListNode prev = head;
		for (int i = 1; i < position - 1 && prev.next != null; i++) prev = prev.next;

		DListNode newNode = new DListNode(val);
		DListNode after = prev.next;

		newNode.prev = prev;
		newNode.next = after;
		prev.next = newNode;
		if (after != null) after.prev = newNode;

		return head;
	}

// ------------------------------------------------------------------------------

	// 4. Delete at a position (1-based): walk to the node itself ('curr'). No need to stop one early, because curr.prev
	//    tells us the node before it. Then join its neighbours:
	//      curr.prev.next = curr.next   (or move head forward if curr is the head)
	//      curr.next.prev = curr.prev   (only if curr is not the tail)
	//    prev <=> curr <=> next   becomes   prev <=> next
	// Careful: If the list has only 1 node, deleting it makes the list empty. (head.next is null, so we must not
	//          touch head.next.prev.)
	// T.C: O(position)
	// S.C: O(1)
	public DListNode DeleteAtPosition(DListNode head, int position)
	{
		if (head == null || position < 1) return head;

		DListNode curr = head;
		for (int i = 1; i < position && curr != null; i++) curr = curr.next;
		if (curr == null) return head;   // position is beyond the list

		if (curr.prev != null) curr.prev.next = curr.next;
		else head = curr.next;                               // deleting the head

		if (curr.next != null) curr.next.prev = curr.prev;   // deleting a node that is not the tail

		return head;
	}

// ------------------------------------------------------------------------------

	// Walk backward: go to the tail, then follow 'prev' links. (Only possible in a doubly linked list.)
	// T.C: O(n)
	// S.C: O(n);   for the output string
	public string PrintBackward(DListNode head)
	{
		if (head == null) return "(empty)";

		DListNode tail = head;
		while (tail.next != null) tail = tail.next;

		List<int> values = new List<int>();
		for (DListNode t = tail; t != null; t = t.prev) values.Add(t.val);
		return string.Join(" -> ", values);
	}
}


class Program
{
	static string Print(DListNode head)
	{
		List<int> values = new List<int>();
		for (DListNode t = head; t != null; t = t.next) values.Add(t.val);
		return values.Count == 0 ? "(empty)" : string.Join(" -> ", values);
	}

	static void Main()
	{
		Solution sol = new Solution();
		DListNode head = null;

		head = sol.InsertAtHead(head, 2);
		head = sol.InsertAtHead(head, 4);
		head = sol.InsertAtTail(head, 6);
		head = sol.InsertAtPosition(head, 8, 3);
		Console.WriteLine(Print(head));               // o/p: 4 -> 2 -> 8 -> 6

		head = sol.DeleteAtPosition(head, 2);
		Console.WriteLine(Print(head));               // o/p: 4 -> 8 -> 6
		Console.WriteLine(sol.PrintBackward(head));   // o/p: 6 -> 8 -> 4
	}
}

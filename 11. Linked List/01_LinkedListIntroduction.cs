// Linked List   --->   A linear data structure where elements are stored in NODES, and each node points to the next
// node. Unlike an array, the nodes are NOT stored next to each other in memory. Each node only knows where the next one is.
//
//   head
//    |
//   [2 | •]--> [4 | •]--> [6 | •]--> [8 | •]--> [10 | •]--> null
//    data next
//
// ● A node has 2 parts:
//   I.  Data: the value stored in the node.
//   II. Next: a reference (link) to the next node. The last node's next is null (nothing after it).
// ● 'head' is a reference to the first node. If we lose the head, we lose the whole list.

// ● Linked list vs Array:
//   Feature                       |  Array                         |  Linked list
//   --------------------------------------------------------------------------------------------
//   Memory                        |  One continuous block          |  Nodes anywhere in memory
//   Access the i-th element       |  O(1): arr[i]                  |  O(n): walk from head
//   Insert/delete at the front    |  O(n): shift everything        |  O(1): change 2 links
//   Insert/delete in the middle   |  O(n): shift elements          |  O(1) once you are at the spot (O(n) to reach it)
//   Size                          |  Fixed (List<T> resizes)       |  Grows and shrinks node by node
//   Extra memory                  |  None                          |  One 'next' reference per node

// ● Types of linked list (see the image):
//   I.   Singly linked list: each node has 'next' only. You can go forward only.
//   II.  Doubly linked list: each node has 'prev' and 'next'. You can go both ways. (See '04_DoublyLinkedList.cs'.)
//   III. Circular singly linked list: like singly, but the last node points back to the first (no null at the end).
//   IV.  Circular doubly linked list: like doubly, but the last and first nodes point to each other.

// ● C++ vs C#:
//   C++: Node* next (a pointer), node->data, new Node(5), delete node, NULL / nullptr
//   C#:  ListNode next (a reference), node.val,  new ListNode(5), no delete (the Garbage Collector frees a node
//        automatically once nothing refers to it), null
//   Every class object in C# is used through a reference, so 'ListNode next' works just like 'Node* next' in C++.

// ● C# has a built-in doubly linked list: LinkedList<T> (AddFirst, AddLast, RemoveFirst, RemoveLast, Find ...).
//   But interviews ask you to work with your OWN nodes, like Leetcode's ListNode class below.

// Dry run image (in this folder): 01_LinkedListIntroduction.jpg

using System;
using System.Collections.Generic;

// Leetcode's node class for a singly linked list (same in every Leetcode C# problem).
public class ListNode
{
	public int val;
	public ListNode next;

	// Constructor: new ListNode(5) makes a node with val = 5 and next = null.
	public ListNode(int val = 0, ListNode next = null)
	{
		this.val = val;     // 'this.val' = the field of this object, 'val' = the parameter
		this.next = next;
	}
}

public class Solution
{
	// Build a linked list from an array, and return its head.
	// Trick: a 'dummy' node in front, so the first real node is added the same way as all the others (no special case).
	// T.C: O(n)
	// S.C: O(n);   for the n new nodes
	public ListNode BuildList(int[] arr)
	{
		ListNode dummy = new ListNode();
		ListNode tail = dummy;

		foreach (int x in arr)
		{
			tail.next = new ListNode(x);
			tail = tail.next;
		}

		return dummy.next;
	}

// ------------------------------------------------------------------------------

	// Traverse (walk through) the list: start at head, and keep moving to 'next' until null.
	// Always walk with a separate variable (temp), so 'head' itself stays at the first node.
	// T.C: O(n)
	// S.C: O(n);   for the output string
	public string PrintList(ListNode head)
	{
		List<int> values = new List<int>();
		ListNode temp = head;

		while (temp != null)
		{
			values.Add(temp.val);
			temp = temp.next;   // move to the next node
		}

		return string.Join(" -> ", values);
	}

// ------------------------------------------------------------------------------

	// Length = number of nodes. Same walk, just count.
	// T.C: O(n)
	// S.C: O(1)
	public int Length(ListNode head)
	{
		int count = 0;
		for (ListNode temp = head; temp != null; temp = temp.next) count++;
		return count;
	}

// ------------------------------------------------------------------------------

	// Search: is 'target' in the list ? (No index access like arr[i], so walk until found.)
	// T.C: O(n)
	// S.C: O(1)
	public bool Search(ListNode head, int target)
	{
		for (ListNode temp = head; temp != null; temp = temp.next)
		{
			if (temp.val == target) return true;
		}
		return false;
	}
}


class Program
{
	static void Main()
	{
		// Making nodes by hand and linking them:
		ListNode first = new ListNode(2);
		ListNode second = new ListNode(4);
		first.next = second;                       // 2 -> 4
		second.next = new ListNode(6);             // 2 -> 4 -> 6

		Solution sol = new Solution();
		Console.WriteLine(sol.PrintList(first));                                  // o/p: 2 -> 4 -> 6

		ListNode head = sol.BuildList(new int[] { 2, 4, 6, 8, 10 });
		Console.WriteLine(sol.Length(head));                                      // o/p: 5
		Console.WriteLine(sol.Search(head, 8));                                   // o/p: True
	}
}

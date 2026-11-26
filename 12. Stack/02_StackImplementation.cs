// Stack Implementation   --->   Build our own stack (without Stack<T>), with Push, Pop, Peek, IsEmpty and Size.
// 2 ways:
// I.  Using an array: fast and simple, but has a fixed capacity.
// II. Using a linked list: grows without any limit (one node per item).

// Example:
// Push 10, 20, 30 into a stack of capacity 3. Push 40 -> "Stack overflow" (full).
// Peek -> 30. Pop twice -> top is 10. Pop twice -> "Stack underflow" (empty).

// ● Overflow:  pushing into a FULL stack (only possible with a fixed-size array).
// ● Underflow: popping from an EMPTY stack.


using System;
using System.Collections.Generic;

// I. Array implementation.
// 'top' is the index of the top item. -1 means the stack is empty.
// Push: top++, then arr[top] = x.   Pop: top--.   (The old value stays in the array, but it is outside the stack now.)
// T.C: O(1) for every operation
// S.C: O(capacity)
public class ArrayStack
{
	private readonly int[] arr;
	private int top = -1;

	public ArrayStack(int capacity) { arr = new int[capacity]; }

	public void Push(int x)
	{
		if (top == arr.Length - 1)
		{
			Console.WriteLine("Stack overflow");
			return;
		}
		arr[++top] = x;   // first top++, then store at the new top
	}

	public int Pop()
	{
		if (top == -1)
		{
			Console.WriteLine("Stack underflow");
			return -1;
		}
		return arr[top--];   // first read arr[top], then top--
	}

	public int Peek() => top == -1 ? -1 : arr[top];
	public bool IsEmpty() => top == -1;
	public int Size() => top + 1;

	public string Print()   // bottom to top
	{
		List<int> items = new List<int>();
		for (int i = 0; i <= top; i++) items.Add(arr[i]);
		return string.Join(" ", items);
	}
}

// ------------------------------------------------------------------------------

// II. Linked list implementation.
// The HEAD of the list is the top of the stack, because adding/removing at the head is O(1).
// Push: new node in front of head.   Pop: head = head.next.
// No capacity, so no overflow (until the computer runs out of memory).
// T.C: O(1) for every operation
// S.C: O(n);   one node per item
public class LinkedListStack
{
	private class Node
	{
		public int val;
		public Node next;
		public Node(int val, Node next) { this.val = val; this.next = next; }
	}

	private Node head;   // top of the stack
	private int count;

	public void Push(int x)
	{
		head = new Node(x, head);
		count++;
	}

	public int Pop()
	{
		if (head == null)
		{
			Console.WriteLine("Stack underflow");
			return -1;
		}
		int val = head.val;
		head = head.next;
		count--;
		return val;
	}

	public int Peek() => head == null ? -1 : head.val;
	public bool IsEmpty() => head == null;
	public int Size() => count;
}


class Program
{
	static void Main()
	{
		ArrayStack st = new ArrayStack(3);
		st.Push(10);
		st.Push(20);
		st.Push(30);
		Console.WriteLine(st.Print());          // o/p: 10 20 30
		st.Push(40);                            // o/p: Stack overflow
		Console.WriteLine(st.Peek());           // o/p: 30
		st.Pop();
		st.Pop();
		Console.WriteLine(st.Peek());           // o/p: 10
		st.Pop();
		st.Pop();                               // o/p: Stack underflow
		Console.WriteLine(st.IsEmpty());        // o/p: True

		LinkedListStack ls = new LinkedListStack();
		ls.Push(10);
		ls.Push(20);
		ls.Push(30);
		Console.WriteLine(ls.Pop() + " " + ls.Peek() + " " + ls.Size());   // o/p: 30 20 2
	}
}

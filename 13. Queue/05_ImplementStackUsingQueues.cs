// Leetcode: 225. Implement Stack using Queues   --->   Implement a last-in-first-out (LIFO) stack using only two
// queues. The implemented stack should support all the functions of a normal stack (push, top, pop, and empty).
// Implement the MyStack class:
// • void push(int x) Pushes element x to the top of the stack.
// • int pop() Removes the element on the top of the stack and returns it.
// • int top() Returns the element on the top of the stack.
// • boolean empty() Returns true if the stack is empty, false otherwise.
// You must use only standard operations of a queue: push to back, peek/pop from front, size and is empty.

// Example:
// push(1), push(2), top() -> 2, pop() -> 2, empty() -> false

// Follow-up: Can you implement the stack using only one queue?

// The problem: A queue gives back the OLDEST item, but a stack must give back the NEWEST one.
// Idea: Keep the queue's FRONT always equal to the stack's TOP. So every new item must be moved to the front when it is
//       pushed.


using System;
using System.Collections.Generic;

// Approach 1: Two queues.
// Push(x): put x into the empty helper queue first, then move every item of the main queue behind it. Now x is at the
//          front, followed by the older items in newest-to-oldest order. Swap the 2 queues (helper becomes main).
// Pop/Top: the front of the main queue.
// T.C: Push O(n), Pop/Top/Empty O(1)
// S.C: O(n)
public class MyStack_TwoQueues
{
	private Queue<int> main = new Queue<int>(), helper = new Queue<int>();

	public void Push(int x)
	{
		helper.Enqueue(x);
		while (main.Count > 0) helper.Enqueue(main.Dequeue());
		(main, helper) = (helper, main);   // swap: main now has x at the front, helper is empty
	}

	public int Pop() => main.Dequeue();
	public int Top() => main.Peek();
	public bool Empty() => main.Count == 0;
}

// ------------------------------------------------------------------------------

// Approach 2 (follow-up): One queue, by rotating.
// Push(x): enqueue x at the back, then take the (count - 1) OLDER items from the front and enqueue them at the back
//          again, one by one. Now x has come round to the front.
// e.g., queue [3, 2, 1] (3 at front = top). Push 4: [3, 2, 1, 4] -> rotate 3 times -> [4, 3, 2, 1]. 4 is on top.
// T.C: Push O(n), Pop/Top/Empty O(1)
// S.C: O(n)
public class MyStack
{
	private readonly Queue<int> q = new Queue<int>();

	public void Push(int x)
	{
		q.Enqueue(x);
		for (int i = 0; i < q.Count - 1; i++) q.Enqueue(q.Dequeue());
	}

	public int Pop() => q.Dequeue();
	public int Top() => q.Peek();
	public bool Empty() => q.Count == 0;
}


class Program
{
	static void Main()
	{
		MyStack_TwoQueues s1 = new MyStack_TwoQueues();
		s1.Push(1);
		s1.Push(2);
		s1.Push(3);
		Console.WriteLine(s1.Top() + " " + s1.Pop() + " " + s1.Pop() + " " + s1.Empty());   // o/p: 3 3 2 False

		MyStack s2 = new MyStack();
		s2.Push(1);
		s2.Push(2);
		s2.Push(3);
		Console.WriteLine(s2.Top() + " " + s2.Pop() + " " + s2.Pop() + " " + s2.Empty());   // o/p: 3 3 2 False
	}
}

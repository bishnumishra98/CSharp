// Leetcode: 232. Implement Queue using Stacks   --->   Implement a first in first out (FIFO) queue using only two stacks.
// The implemented queue should support all the functions of a normal queue (push, peek, pop, and empty).
// Implement the MyQueue class:
// • void push(int x) Pushes element x to the back of the queue.
// • int pop() Removes the element from the front of the queue and returns it.
// • int peek() Returns the element at the front of the queue.
// • boolean empty() Returns true if the queue is empty, false otherwise.
// You must use only standard operations of a stack: push to top, peek/pop from top, size, and is empty.

// Example:
// push(1), push(2), peek() -> 1, pop() -> 1, empty() -> false

// Follow-up: Can you implement the queue such that each operation is amortized O(1) time complexity?

// The problem: A stack gives back the NEWEST item, but a queue must give back the OLDEST one.
// Key trick: Pouring one stack into another REVERSES the order. Reversing LIFO gives FIFO.


using System;
using System.Collections.Generic;

// Approach 1: Make push expensive. Keep the main stack always in QUEUE order (oldest item on top).
// Push(x): move everything from main to helper, push x into main (it goes to the bottom), move everything back.
// Then pop/peek just use the top of main, which is the oldest item.
// T.C: Push O(n), Pop/Peek/Empty O(1)
// S.C: O(n)
public class MyQueue_CostlyPush
{
	private readonly Stack<int> main = new Stack<int>(), helper = new Stack<int>();

	public void Push(int x)
	{
		while (main.Count > 0) helper.Push(main.Pop());
		main.Push(x);
		while (helper.Count > 0) main.Push(helper.Pop());
	}

	public int Pop() => main.Pop();
	public int Peek() => main.Peek();
	public bool Empty() => main.Count == 0;
}

// ------------------------------------------------------------------------------

// Approach 2 (optimal, the follow-up): Two stacks, 'input' and 'output'. Move items only when really needed.
// • Push(x): just push into 'input'. O(1)
// • Pop / Peek: if 'output' is empty, pour ALL of 'input' into 'output' (this reverses them, so the oldest is on top of
//   'output'). Then pop/peek the top of 'output'.
//   If 'output' is not empty, its top is already the oldest item. Don't touch 'input'.
// Why amortized O(1) ? Every item is moved from input to output at most ONCE in its life. So n operations do at most n
// moves in total, i.e., O(1) per operation on average (even though one single pop can take O(n)).
// e.g., push 1, push 2  ->  input [1, 2] (2 on top).  pop: output empty, pour  ->  output [2, 1] (1 on top). pop -> 1.
//       push 3  ->  input [3].  pop  ->  output still has 2 on top -> 2.  pop: output empty, pour 3 -> 3.
// T.C: Push O(1), Pop/Peek amortized O(1), Empty O(1)
// S.C: O(n)
public class MyQueue
{
	private readonly Stack<int> input = new Stack<int>(), output = new Stack<int>();

	public void Push(int x) => input.Push(x);

	public int Pop()
	{
		MoveIfNeeded();
		return output.Pop();
	}

	public int Peek()
	{
		MoveIfNeeded();
		return output.Peek();
	}

	public bool Empty() => input.Count == 0 && output.Count == 0;

	private void MoveIfNeeded()
	{
		if (output.Count == 0)
		{
			while (input.Count > 0) output.Push(input.Pop());
		}
	}
}


class Program
{
	static void Main()
	{
		MyQueue_CostlyPush q1 = new MyQueue_CostlyPush();
		q1.Push(1);
		q1.Push(2);
		q1.Push(3);
		Console.WriteLine(q1.Peek() + " " + q1.Pop() + " " + q1.Pop() + " " + q1.Empty());   // o/p: 1 1 2 False

		MyQueue q2 = new MyQueue();
		q2.Push(1);
		q2.Push(2);
		q2.Push(3);
		Console.WriteLine(q2.Peek() + " " + q2.Pop() + " " + q2.Pop() + " " + q2.Empty());   // o/p: 1 1 2 False
	}
}

// GFG: Queue Reversal   --->   Given a queue, reverse it. Use only standard queue operations (enqueue, dequeue, front,
// empty).

// Example 1:
// Input:  q = [10, 20, 30, 40, 50]   (10 at the front)
// Output: q = [50, 40, 30, 20, 10]

// Example 2:
// Input:  q = [4, 3, 1, 10, 2, 6]
// Output: q = [6, 2, 10, 1, 3, 4]

// Problem link: https://www.geeksforgeeks.org/problems/queue-reversal/1


using System;
using System.Collections.Generic;

public class Solution
{
	// Approach 1: Use a stack. Move every item from the queue into a stack, then back. The stack reverses the order (LIFO).
	// T.C: O(n)
	// S.C: O(n);   for the stack
	public void ReverseQueue_Stack(Queue<int> q)
	{
		Stack<int> st = new Stack<int>();
		while (q.Count > 0) st.Push(q.Dequeue());
		while (st.Count > 0) q.Enqueue(st.Pop());
	}

// ------------------------------------------------------------------------------

	// Approach 2: Recursion (the call stack works as the stack).
	// 1. Take out the front item and remember it in this call.
	// 2. Reverse the rest of the queue recursively.
	// 3. Put the remembered item at the BACK. The first item ends up last, the second item second last, and so on.
	// e.g., [1, 2, 3]: take 1 -> take 2 -> take 3 -> empty. Coming back: add 3, add 2, add 1 -> [3, 2, 1]
	// T.C: O(n)
	// S.C: O(n);   recursion stack (and very long queues can overflow the stack, so approach 1 is safer)
	public void ReverseQueue(Queue<int> q)
	{
		if (q.Count == 0) return;

		int front = q.Dequeue();
		ReverseQueue(q);
		q.Enqueue(front);
	}
}


class Program
{
	static void Main()
	{
		Queue<int> q1 = new Queue<int>(new[] { 10, 20, 30, 40, 50 });
		new Solution().ReverseQueue_Stack(q1);
		Console.WriteLine(string.Join(" ", q1));   // o/p: 50 40 30 20 10

		Queue<int> q2 = new Queue<int>(new[] { 10, 20, 30, 40, 50 });
		new Solution().ReverseQueue(q2);
		Console.WriteLine(string.Join(" ", q2));   // o/p: 50 40 30 20 10
	}
}

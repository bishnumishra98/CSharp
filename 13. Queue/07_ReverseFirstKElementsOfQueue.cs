// GFG: Reverse First K elements of Queue   --->   Given an integer k and a queue of integers, we need to reverse the
// order of the first k elements of the queue, leaving the other elements in the same relative order.
// Only the standard queue operations are allowed: enqueue, dequeue, size, front.
// If the size of queue is smaller than the given k, then return the original queue.

// Example 1:
// Input: q = [1, 2, 3, 4, 5], k = 3
// Output: [3, 2, 1, 4, 5]

// Example 2:
// Input: q = [4, 3, 2, 1], k = 4
// Output: [1, 2, 3, 4]

// Problem link: https://www.geeksforgeeks.org/problems/reverse-first-k-elements-of-queue/1


using System;
using System.Collections.Generic;

public class Solution
{
	// Approach 1: Build a new queue.
	// 1. Move the first k items into a stack (it reverses them).
	// 2. Pop the stack into a NEW queue, then move the rest of the old queue into the new queue.
	// T.C: O(n)
	// S.C: O(n);   for the stack (k) and the new queue (n)
	public Queue<int> ReverseFirstK_NewQueue(Queue<int> q, int k)
	{
		if (k <= 0 || k > q.Count) return q;

		Stack<int> st = new Stack<int>();
		for (int i = 0; i < k; i++) st.Push(q.Dequeue());

		Queue<int> ans = new Queue<int>();
		while (st.Count > 0) ans.Enqueue(st.Pop());
		while (q.Count > 0) ans.Enqueue(q.Dequeue());
		return ans;
	}

// ------------------------------------------------------------------------------

	// Approach 2 (better): In place, only a stack of size k.
	// 1. Move the first k items into a stack.
	// 2. Pop them back into the SAME queue. They are now reversed, but at the BACK: [4, 5, 3, 2, 1]
	// 3. Move the (n - k) items that are now in front to the back, one by one: [3, 2, 1, 4, 5]
	// T.C: O(n)
	// S.C: O(k);   for the stack
	public Queue<int> ReverseFirstK(Queue<int> q, int k)
	{
		if (k <= 0 || k > q.Count) return q;

		Stack<int> st = new Stack<int>();
		for (int i = 0; i < k; i++) st.Push(q.Dequeue());
		while (st.Count > 0) q.Enqueue(st.Pop());

		int rest = q.Count - k;
		for (int i = 0; i < rest; i++) q.Enqueue(q.Dequeue());
		return q;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(string.Join(" ", new Solution().ReverseFirstK_NewQueue(new Queue<int>(new[] { 1, 2, 3, 4, 5 }), 3)));   // o/p: 3 2 1 4 5
		Console.WriteLine(string.Join(" ", new Solution().ReverseFirstK(new Queue<int>(new[] { 1, 2, 3, 4, 5 }), 3)));            // o/p: 3 2 1 4 5
	}
}

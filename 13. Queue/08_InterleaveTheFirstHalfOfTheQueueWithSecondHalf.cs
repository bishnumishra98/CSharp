// GFG: Interleave the First Half of the Queue with Second Half   --->   Given a queue q[] of even size. Your task is to
// rearrange the queue by interleaving its first half with the second half.
// Interleaving means: the first element from the first half, then the first element from the second half, then the
// second element from the first half, then the second element from the second half, and so on.

// Example 1:
// Input: q[] = [2, 4, 3, 1]
// Output: [2, 3, 4, 1]

// Example 2:
// Input: q[] = [10, 20, 30, 40, 50, 60]
// Output: [10, 40, 20, 50, 30, 60]
// Explanation: First half = [10, 20, 30], second half = [40, 50, 60]. Take one from each, in turn.

// Problem link: https://www.geeksforgeeks.org/problems/interleave-the-first-half-of-the-queue-with-second-half/1


using System;
using System.Collections.Generic;

public class Solution
{
	// Approach 1: Copy both halves into 2 lists, then enqueue them back in turns.
	// T.C: O(n)
	// S.C: O(n);   for the 2 lists
	public void Rearrange_TwoLists(Queue<int> q)
	{
		int half = q.Count / 2;
		List<int> first = new List<int>(), second = new List<int>();
		for (int i = 0; i < half; i++) first.Add(q.Dequeue());
		for (int i = 0; i < half; i++) second.Add(q.Dequeue());

		for (int i = 0; i < half; i++)
		{
			q.Enqueue(first[i]);
			q.Enqueue(second[i]);
		}
	}

// ------------------------------------------------------------------------------

	// Approach 2 (better): Move only the first half out.
	// 1. Move the first half into a helper queue. The original queue now holds only the second half.
	// 2. Repeat 'half' times: move one item from the helper to the back, then move one item from the FRONT of the
	//    original queue (the second half) to the back.
	//    After this, the second half items have all been rotated to the back in the right order.
	// e.g., [10, 20, 30, 40, 50, 60]:  helper = [10, 20, 30],  q = [40, 50, 60]
	//       10 -> q = [40, 50, 60, 10],  40 to back -> [50, 60, 10, 40]
	//       20 -> [50, 60, 10, 40, 20],  50 to back -> [60, 10, 40, 20, 50]
	//       30 -> [60, 10, 40, 20, 50, 30],  60 to back -> [10, 40, 20, 50, 30, 60]
	// T.C: O(n)
	// S.C: O(n / 2);   for the helper queue
	public void Rearrange(Queue<int> q)
	{
		int half = q.Count / 2;
		Queue<int> firstHalf = new Queue<int>();
		for (int i = 0; i < half; i++) firstHalf.Enqueue(q.Dequeue());

		for (int i = 0; i < half; i++)
		{
			q.Enqueue(firstHalf.Dequeue());   // next item of the first half
			q.Enqueue(q.Dequeue());           // next item of the second half (it is at the front)
		}
	}
}


class Program
{
	static void Main()
	{
		Queue<int> q1 = new Queue<int>(new[] { 10, 20, 30, 40, 50, 60 });
		new Solution().Rearrange_TwoLists(q1);
		Console.WriteLine(string.Join(" ", q1));   // o/p: 10 40 20 50 30 60

		Queue<int> q2 = new Queue<int>(new[] { 10, 20, 30, 40, 50, 60 });
		new Solution().Rearrange(q2);
		Console.WriteLine(string.Join(" ", q2));   // o/p: 10 40 20 50 30 60
	}
}

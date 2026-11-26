// GFG: First negative in every window of size k   --->   Given an array arr[] and a positive integer k, find the first
// negative integer for each and every window (contiguous subarray) of size k.
// Note: If a window does not contain a negative integer, then return 0 for that window.

// Example 1:
// Input: arr[] = [-8, 2, 3, -6, 10], k = 2
// Output: [-8, 0, -6, -6]
// Explanation: Windows: [-8, 2] -> -8,  [2, 3] -> 0,  [3, -6] -> -6,  [-6, 10] -> -6

// Example 2:
// Input: arr[] = [12, -1, -7, 8, -15, 30, 16, 28], k = 3
// Output: [-1, -1, -7, -15, -15, 0]

// Example 3:
// Input: arr[] = [12, 1, 3, 5], k = 3
// Output: [0, 0]

// Problem link: https://www.geeksforgeeks.org/problems/first-negative-integer-in-every-window-of-size-k3345/1


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: For every window, scan its k elements for the first negative.
	// T.C: O(n * k)
	// S.C: O(1);   ignoring the answer
	public List<int> FirstNegInt_BruteForce(int[] arr, int k)
	{
		List<int> ans = new List<int>();

		for (int start = 0; start + k <= arr.Length; start++)
		{
			int firstNeg = 0;
			for (int j = start; j < start + k; j++)
			{
				if (arr[j] < 0)
				{
					firstNeg = arr[j];
					break;
				}
			}
			ans.Add(firstNeg);
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Sliding window + a queue of the INDEXES of negative numbers.
	// The queue holds the negatives of the current window in order, so its FRONT is the first negative of the window.
	// For every new index i (the window is [i - k + 1 ... i]):
	// 1. If arr[i] is negative, enqueue i.
	// 2. If the front index has slid out of the window (front <= i - k), dequeue it.
	// 3. Once the first full window is ready (i >= k - 1), the answer is arr[front], or 0 if the queue is empty.
	// Why indexes and not values ? To know when a negative has left the window.
	// T.C: O(n);   every index is enqueued and dequeued at most once
	// S.C: O(k);   for the queue
	public List<int> FirstNegInt(int[] arr, int k)
	{
		List<int> ans = new List<int>();
		Queue<int> negatives = new Queue<int>();   // indexes

		for (int i = 0; i < arr.Length; i++)
		{
			if (arr[i] < 0) negatives.Enqueue(i);
			if (negatives.Count > 0 && negatives.Peek() <= i - k) negatives.Dequeue();   // out of the window

			if (i >= k - 1) ans.Add(negatives.Count > 0 ? arr[negatives.Peek()] : 0);
		}
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 12, -1, -7, 8, -15, 30, 16, 28 };
		int k = 3;

		Console.WriteLine(string.Join(" ", new Solution().FirstNegInt_BruteForce(arr, k)));   // o/p: -1 -1 -7 -15 -15 0
		Console.WriteLine(string.Join(" ", new Solution().FirstNegInt(arr, k)));              // o/p: -1 -1 -7 -15 -15 0
	}
}

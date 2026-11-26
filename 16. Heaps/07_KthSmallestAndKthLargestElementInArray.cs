// Kth Smallest Element in an Array   --->   Given an integer array nums and an integer k, return the k-th smallest element.
// Leetcode: 215. Kth Largest Element in an Array   --->   Return the k-th LARGEST element. (It is the k-th largest in
// sorted order, not the k-th distinct one.) Can you solve it without sorting ?

// Example 1 (Kth smallest):
// Input: nums = [7,10,4,3,20,15], k = 4
// Output: 10   (sorted: 3 4 7 10 15 20, the 4th is 10)

// Example 2 (Kth largest):
// Input: nums = [3,2,3,1,2,4,5,5,6], k = 4
// Output: 4   (from the biggest: 6 5 5 4 ..., the 4th is 4)


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: Sort, and take index k - 1 (smallest) or n - k (largest).
	// T.C: O(n log n)
	// S.C: O(log n);   Array.Sort works in place, using a small stack
	public int KthSmallest_Sort(int[] nums, int k)
	{
		int[] copy = (int[])nums.Clone();   // do not change the caller's array
		Array.Sort(copy);
		return copy[k - 1];
	}

// ------------------------------------------------------------------------------

	// Better: Put ALL n values in a min heap (built in O(n)), then remove the smallest k - 1 times. The top is now the
	// k-th smallest.
	// T.C: O(n + k log n)
	// S.C: O(n);   the heap holds everything
	public int KthSmallest_MinHeap(int[] nums, int k)
	{
		List<(int, int)> items = new List<(int, int)>();
		foreach (int x in nums) items.Add((x, x));
		PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>(items);   // O(n) build

		for (int i = 0; i < k - 1; i++) minHeap.Dequeue();
		return minHeap.Peek();
	}

// ------------------------------------------------------------------------------

	// Optimal (heap): Keep only the k SMALLEST values seen so far, in a MAX heap of size k. Its top is the biggest of
	// those k, i.e., the k-th smallest so far.
	//   For every value x: add it. If the heap now has more than k values, remove the top (the biggest): it can not be
	//   among the k smallest any more.
	// In the end the heap holds the k smallest values, and its top is the k-th smallest.
	// e.g., [7,10,4,3,20,15], k = 4: after 7, 10, 4, 3 the heap is {10, 7, 4, 3}, top 10.
	//       20: heap {20, 10, 7, 4, 3}, too big, remove 20. 15: remove 15. Top: 10.
	// Why a MAX heap to find the smallest ? Because the one we must throw away each time is the biggest of the kept ones.
	// T.C: O(n log k);   n adds and removes on a heap of size k (much better than n log n when k is small)
	// S.C: O(k)
	public int KthSmallest(int[] nums, int k)
	{
		PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

		foreach (int x in nums)
		{
			maxHeap.Enqueue(x, x);
			if (maxHeap.Count > k) maxHeap.Dequeue();   // drop the biggest
		}
		return maxHeap.Peek();
	}

// ------------------------------------------------------------------------------

	// Kth largest (Leetcode 215): the mirror. Keep the k LARGEST values in a MIN heap of size k. When it grows past k,
	// remove the smallest. The top is the k-th largest.
	// T.C: O(n log k)
	// S.C: O(k)
	public int FindKthLargest(int[] nums, int k)
	{
		PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();

		foreach (int x in nums)
		{
			minHeap.Enqueue(x, x);
			if (minHeap.Count > k) minHeap.Dequeue();   // drop the smallest
		}
		return minHeap.Peek();
	}

// ------------------------------------------------------------------------------

	// Fastest on average: Quickselect. Use the partition step of Quick Sort ('07. Sorting/05_QuickSort.cs'): after one
	// partition, the pivot is at its FINAL sorted index p. If p is the index we want, done. If not, the answer is only on
	// ONE side, so continue there (Quick Sort would sort both sides).
	// The k-th largest is at sorted index n - k.
	// A random pivot avoids the O(n^2) worst case on sorted input (it can still happen, but is extremely unlikely).
	// T.C: O(n) on average (n + n/2 + n/4 + ... = 2n), O(n^2) in the worst case
	// S.C: O(1);   a loop, no recursion (it works on a copy, so O(n) if you count that)
	public int FindKthLargest_Quickselect(int[] nums, int k)
	{
		int[] arr = (int[])nums.Clone();
		int target = arr.Length - k;           // sorted index of the answer
		int low = 0, high = arr.Length - 1;
		Random rand = new Random(0);           // fixed seed, so the run is repeatable

		while (true)
		{
			int p = Partition(arr, low, high, rand.Next(low, high + 1));
			if (p == target) return arr[p];
			if (p < target) low = p + 1;
			else high = p - 1;
		}
	}

	// Lomuto partition: move the pivot to the end, put every value smaller than it to the left, then put the pivot
	// right after them. Returns the pivot's final index.
	private int Partition(int[] arr, int low, int high, int pivotIndex)
	{
		(arr[pivotIndex], arr[high]) = (arr[high], arr[pivotIndex]);
		int pivot = arr[high], store = low;

		for (int i = low; i < high; i++)
		{
			if (arr[i] < pivot)
			{
				(arr[i], arr[store]) = (arr[store], arr[i]);
				store++;
			}
		}

		(arr[store], arr[high]) = (arr[high], arr[store]);
		return store;
	}
}


class Program
{
	static void Main()
	{
		int[] a = { 7, 10, 4, 3, 20, 15 };
		int[] b = { 3, 2, 3, 1, 2, 4, 5, 5, 6 };

		Console.WriteLine(new Solution().KthSmallest_Sort(a, 4));             // o/p: 10
		Console.WriteLine(new Solution().KthSmallest_MinHeap(a, 4));          // o/p: 10
		Console.WriteLine(new Solution().KthSmallest(a, 4));                  // o/p: 10
		Console.WriteLine(new Solution().FindKthLargest(b, 4));               // o/p: 4
		Console.WriteLine(new Solution().FindKthLargest_Quickselect(b, 4));   // o/p: 4
	}
}

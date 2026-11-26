// Bubble Sort   --->   Sort an array in ascending order.
// Idea: Compare 2 neighbours at a time, and swap them if the left one is bigger. This way, the largest element
//       "bubbles up" to the right end in one pass. After pass 1, the largest is at the last index. After pass 2, the
//       second largest is at the second last index, and so on. So n - 1 passes are enough.

// Illustration: arr = [55, 45, 35, 25, 15], n = 5
// Pass 1: [45, 35, 25, 15, 55]   // 55 bubbles to the last index
// Pass 2: [35, 25, 15, 45, 55]   // 45 bubbles to the second last index
// Pass 3: [25, 15, 35, 45, 55]   // 35 reaches its place
// Pass 4: [15, 25, 35, 45, 55]   // 25 reaches its place
// No pass 5 is needed, as the only element left (15) is already at its place.

// Video link: https://www.youtube.com/watch?v=xli_FI7CuzA


using System;

public class Solution
{
	// Basic bubble sort:
	// 1. Do n - 1 passes (i = 0 to n - 2).
	// 2. In each pass, compare arr[j] and arr[j + 1] and swap if arr[j] > arr[j + 1].
	//    After pass i, the last i elements are already in place, so j goes only till n - 2 - i.
	// T.C: O(n^2);   in every case, even if the array is already sorted
	// S.C: O(1)
	public void BubbleSort_Basic(int[] arr)
	{
		int n = arr.Length;

		for (int i = 0; i < n - 1; i++)
		{
			for (int j = 0; j < n - 1 - i; j++)
			{
				if (arr[j] > arr[j + 1]) (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);   // swap
			}
		}
	}

// ------------------------------------------------------------------------------

	// Optimised bubble sort: Stop early if a pass makes no swap.
	// Problem with basic version: For an already sorted array like [1, 2, 3, 4, 5], it still does all n - 1 passes.
	// Solution: If a whole pass makes no swap, every neighbour pair is in order, i.e., the array is sorted. So stop.
	// T.C: O(n^2) worst/average,  O(n) best (already sorted array: just 1 pass with no swap)
	// S.C: O(1)
	public void BubbleSort(int[] arr)
	{
		int n = arr.Length;

		for (int i = 0; i < n - 1; i++)
		{
			bool swapped = false;

			for (int j = 0; j < n - 1 - i; j++)
			{
				if (arr[j] > arr[j + 1])
				{
					(arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
					swapped = true;
				}
			}

			if (!swapped) break;   // no swap in this pass, so the array is sorted
		}
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 55, 45, 35, 25, 15 };
		int[] copy = (int[])arr.Clone();   // both methods sort the array itself, so each one gets its own copy

		new Solution().BubbleSort_Basic(arr);
		Console.WriteLine(string.Join(" ", arr));    // o/p: 15 25 35 45 55

		new Solution().BubbleSort(copy);
		Console.WriteLine(string.Join(" ", copy));   // o/p: 15 25 35 45 55
	}
}

// Merge Sort   --->   Sort an array in ascending order.
// Idea: Divide and Conquer.
// 1. Divide: Cut the array into 2 halves, again and again (using recursion), until each part has only 1 element.
//            An array of 1 element is already sorted.
// 2. Conquer (merge): Join the sorted parts back, 2 at a time, in sorted order, until the whole array is one sorted part.

// Illustration: arr = [45, 35, 55, 15, 25]
// Step 1: Divide
//            [45, 35, 55, 15, 25]
//            /                  \
//      [45, 35, 55]          [15, 25]
//       /        \           /      \
//   [45, 35]    [55]       [15]    [25]
//    /    \
//  [45]  [35]
//
// Step 2: Merge
//  [45]  [35]
//    \    /
//   [35, 45]    [55]       [15]    [25]
//       \        /           \      /
//     [35, 45, 55]          [15, 25]
//            \                 /
//          [15, 25, 35, 45, 55]

// Order of calls for arr = [20, 10, 50, 40, 30, 60] (MS = MergeSort call, M = Merge call):
//   MS[0..5] -> MS[0..2] -> MS[0..1] -> MS[0..0] (1 element, return)
//                                    -> MS[1..1] (1 element, return)
//                                    -> M1: [20, 10] becomes [10, 20]
//                        -> MS[2..2] (1 element, return)
//                        -> M2: [10, 20] + [50] = [10, 20, 50]
//            -> MS[3..5] -> MS[3..4] -> MS[3..3], MS[4..4]
//                                    -> M3: [40, 30] becomes [30, 40]
//                        -> MS[5..5]
//                        -> M4: [30, 40] + [60] = [30, 40, 60]
//            -> M5: [10, 20, 50] + [30, 40, 60] = [10, 20, 30, 40, 50, 60]
// Notice: The LEFT half is fully sorted before the right half even starts. That's how recursion goes (depth first).

// Video link: https://www.youtube.com/watch?v=4VqmGXwpLqc


// Dry run image (in this folder): 04_MergeSort.jpg

using System;

public class Solution
{
	// T.C: O(nlogn) in every case;   the array is halved logn times, and each level merges n elements in total
	// S.C: O(n);   for the 'temp' array in Merge(), plus O(logn) recursion stack
	public void MergeSort(int[] arr)
	{
		Sort(arr, 0, arr.Length - 1);
	}

	private void Sort(int[] arr, int start, int end)
	{
		if (start >= end) return;   // 0 or 1 element: already sorted

		int mid = start + (end - start) / 2;

		Sort(arr, start, mid);         // sort the left half
		Sort(arr, mid + 1, end);       // sort the right half
		Merge(arr, start, mid, end);   // merge both sorted halves
	}

	// Merge 2 sorted parts: arr[start ... mid] (left) and arr[mid + 1 ... end] (right).
	// 1. Put 2 pointers on the first element of each part. Copy the smaller one into 'temp', and move that pointer.
	//    (Use <= so that for equal elements the left one goes first. This keeps equal elements in their original
	//    order, which makes merge sort "stable".)
	// 2. When one part is finished, copy the rest of the other part as it is (it is already sorted).
	// 3. Copy 'temp' back into arr[start ... end].
	private void Merge(int[] arr, int start, int mid, int end)
	{
		int[] temp = new int[end - start + 1];
		int left = start, right = mid + 1, k = 0;

		while (left <= mid && right <= end)
		{
			if (arr[left] <= arr[right]) temp[k++] = arr[left++];
			else temp[k++] = arr[right++];
		}

		while (left <= mid) temp[k++] = arr[left++];     // left part has elements left
		while (right <= end) temp[k++] = arr[right++];   // right part has elements left

		for (int i = start; i <= end; i++)
		{
			arr[i] = temp[i - start];
		}
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 20, 10, 50, 40, 30, 60 };

		new Solution().MergeSort(arr);
		Console.WriteLine(string.Join(" ", arr));   // o/p: 10 20 30 40 50 60
	}
}

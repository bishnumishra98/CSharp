// GFG: Count Inversions   --->   Given an array of integers arr[]. You have to find the Inversion Count of the array.
// Note: Inversion count is the number of pairs of elements (i, j) such that i < j and arr[i] > arr[j].
// (It tells how far the array is from being sorted: a sorted array has 0, a reverse-sorted array has the most.)

// Example 1:
// Input: arr[] = [2, 4, 1, 3, 5]
// Output: 3
// Explanation: The sequence 2, 4, 1, 3, 5 has three inversions (2, 1), (4, 1), (4, 3).

// Example 2:
// Input: arr[] = [2, 3, 4, 5, 6]
// Output: 0
// Explanation: As the sequence is already sorted so there is no inversion count.

// Example 3:
// Input: arr[] = [10, 10, 10]
// Output: 0
// Explanation: As all the elements of array are same, so there is no inversion count.

// Constraints: 1 <= arr.size() <= 10^5
// Why 'long' for the count ? With 10^5 elements in reverse order, there are n * (n - 1) / 2 ≈ 5 * 10^9 inversions,
// which doesn't fit in int.

// Problem link: https://www.geeksforgeeks.org/problems/inversion-of-array-1587115620/1
// First understand '07. Sorting\04_MergeSort.cs'.


// Dry run image (in this folder): 13_CountInversions.jpg

using System;

public class Solution
{
	// Brute force: Check every pair (i, j) with i < j.
	// T.C: O(n^2)
	// S.C: O(1)
	public long InversionCount_BruteForce(int[] arr)
	{
		long count = 0;
		for (int i = 0; i < arr.Length; i++)
		{
			for (int j = i + 1; j < arr.Length; j++)
			{
				if (arr[i] > arr[j]) count++;
			}
		}
		return count;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Merge sort, and count while merging.
	// Every inversion (i, j) is one of 3 kinds: both in the left half, both in the right half, or one in each half.
	// The first two are counted by the recursive calls. The third kind is counted while merging the 2 sorted halves:
	// • When we take arr[right] (because arr[left] > arr[right]), arr[right] is smaller than arr[left] AND smaller than
	//   every element after 'left' in the left half (the left half is sorted). All of them are before arr[right] in the
	//   array, so each makes an inversion with it. That is (mid - left + 1) inversions at once.
	// e.g., left half = [2, 4], right half = [1, 3, 5]:
	//       2 vs 1: take 1  ->  1 is smaller than 2 and 4, so +2 inversions: (2, 1), (4, 1)
	//       2 vs 3: take 2
	//       4 vs 3: take 3  ->  3 is smaller than 4, so +1 inversion: (4, 3)
	//       4 vs 5: take 4, then 5.  Total = 3
	// Note: This sorts the array. Pass a copy if the original order is still needed.
	// T.C: O(nlogn)
	// S.C: O(n);   for the temp array in Merge()
	public long InversionCount(int[] arr)
	{
		return MergeSort(arr, 0, arr.Length - 1);
	}

	private long MergeSort(int[] arr, int start, int end)
	{
		if (start >= end) return 0;

		int mid = start + (end - start) / 2;
		long count = 0;
		count += MergeSort(arr, start, mid);        // inversions inside the left half
		count += MergeSort(arr, mid + 1, end);      // inversions inside the right half
		count += Merge(arr, start, mid, end);       // inversions across the halves
		return count;
	}

	private long Merge(int[] arr, int start, int mid, int end)
	{
		int[] temp = new int[end - start + 1];
		int left = start, right = mid + 1, k = 0;
		long count = 0;

		while (left <= mid && right <= end)
		{
			if (arr[left] <= arr[right]) temp[k++] = arr[left++];   // <=, because equal elements are NOT an inversion
			else
			{
				count += mid - left + 1;   // arr[right] is smaller than all remaining left elements
				temp[k++] = arr[right++];
			}
		}

		while (left <= mid) temp[k++] = arr[left++];
		while (right <= end) temp[k++] = arr[right++];

		for (int i = start; i <= end; i++) arr[i] = temp[i - start];

		return count;
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 2, 4, 1, 3, 5 };

		Console.WriteLine(new Solution().InversionCount_BruteForce(arr));                // o/p: 3
		Console.WriteLine(new Solution().InversionCount((int[])arr.Clone()));            // o/p: 3
	}
}

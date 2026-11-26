// Leetcode: 852. Peak Index in a Mountain Array   --->   You are given an integer mountain array arr of length n
// where the values increase to a peak element and then decrease.
// Return the index of the peak element.
// Your task is to solve it in O(log(n)) time complexity.

// Example 1:
// Input: arr = [0,1,0]
// Output: 1

// Example 2:
// Input: arr = [0,2,1,0]
// Output: 1

// Example 3:
// Input: arr = [0,10,5,2]
// Output: 1

// Extension: The mountain can also have equal neighbours (duplicates), e.g., arr = [1,2,3,3,3,3,3,3,7,2,1] -> Output: 8


using System;

public class Solution
{
	// Brute force: The peak is the first element that is bigger than its next element.
	// T.C: O(n)
	// S.C: O(1)
	public int PeakIndexInMountainArray_BruteForce(int[] arr)
	{
		for (int i = 0; i < arr.Length - 1; i++)
		{
			if (arr[i] > arr[i + 1]) return i;
		}
		return arr.Length - 1;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search.
	// The mountain has 2 slopes: the left slope A (going up) and the right slope B (going down).
	// 1. Initialize start = 0, end = n - 1. While start < end, find mid and check which slope mid is on:
	//    Case 1: arr[mid] < arr[mid + 1]  ->  mid is on slope A (going up). The peak is on the right: start = mid + 1.
	//    Case 2: arr[mid] > arr[mid + 1]  ->  mid is on slope B (going down). The peak is on the left, OR mid itself
	//                                         is the peak. So don't skip mid: end = mid (not mid - 1).
	// 2. When the loop ends, start == end, and both point to the peak. Return start.
	// Why 'while (start < end)' and not 'while (start <= end)' ?
	// When start == end, mid is also the same. Case 2 sets end = mid, which changes nothing. So the loop would run
	// forever (infinite loop). That's why we stop as soon as start reaches end.
	// T.C: O(logn)
	// S.C: O(1)
	public int PeakIndexInMountainArray(int[] arr)
	{
		int start = 0, end = arr.Length - 1;

		while (start < end)
		{
			int mid = start + (end - start) / 2;

			if (arr[mid] < arr[mid + 1]) start = mid + 1;
			else end = mid;   // arr[mid] > arr[mid + 1]. Equal values never come in a strict mountain.
		}

		return start;
	}

// ------------------------------------------------------------------------------

	// Extension: Mountain with duplicates (flat parts), e.g., [1, 2, 3, 3, 3, 3, 3, 3, 7, 2, 1].
	// Problem: When arr[mid] == arr[mid + 1], we are on a flat part and we can't tell if the peak is on the left or right.
	// Solution: Use recursion. For the 2 normal cases, go to one side (same as above). For the flat case, search both
	//           halves and return the index whose value is bigger.
	// T.C: O(logn) on average, O(n) in the worst case (when there are many duplicates, we search both halves)
	// S.C: O(logn);   recursion stack
	public int PeakIndexWithDuplicates(int[] arr)
	{
		return FindPeak(arr, 0, arr.Length - 1);
	}

	private int FindPeak(int[] arr, int start, int end)
	{
		if (start == end) return start;

		int mid = start + (end - start) / 2;

		if (arr[mid] > arr[mid + 1]) return FindPeak(arr, start, mid);        // going down: peak is on the left or at mid
		else if (arr[mid] < arr[mid + 1]) return FindPeak(arr, mid + 1, end); // going up: peak is on the right

		// arr[mid] == arr[mid + 1]: flat part, so check both halves
		int leftPeak = FindPeak(arr, start, mid);
		int rightPeak = FindPeak(arr, mid + 1, end);
		return (arr[leftPeak] >= arr[rightPeak]) ? leftPeak : rightPeak;
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 0, 10, 5, 2 };

		Console.WriteLine(new Solution().PeakIndexInMountainArray_BruteForce(arr));                                // o/p: 1
		Console.WriteLine(new Solution().PeakIndexInMountainArray(arr));                                           // o/p: 1
		Console.WriteLine(new Solution().PeakIndexWithDuplicates(new int[] { 1, 2, 3, 3, 3, 3, 3, 3, 7, 2, 1 }));   // o/p: 8
	}
}

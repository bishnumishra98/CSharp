// Find Missing Element in Sorted Array   --->   A sorted array contains the numbers from 1 to n + 1, but exactly one of
// them is missing (so the array has n elements). Find the missing number.

// Example 1:
// Input: arr = [1, 2, 3, 4, 5, 7, 8, 9]
// Output: 6

// Example 2:
// Input: arr = [1, 2, 3, 4, 5, 6, 7, 8]
// Output: 9


using System;

public class Solution
{
	// Brute force: XOR approach (same as Leetcode 268. Missing Number in '05. Arrays\04_MissingNumber.cs').
	// XOR all numbers from 1 to n + 1, and XOR all elements of 'arr'. Every number present comes 2 times and cancels out.
	// Only the missing number is left. This doesn't use the fact that the array is sorted.
	// T.C: O(n)
	// S.C: O(1)
	public int FindMissing_XOR(int[] arr)
	{
		int n = arr.Length;
		int ans = 0;
		for (int i = 1; i <= n + 1; i++) ans ^= i;
		for (int i = 0; i < n; i++) ans ^= arr[i];
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search.
	// Observation:   Array: [1, 2, 3, 4, 5, 7, 8, 9]
	//                Index:  0  1  2  3  4  5  6  7
	// Before the missing number: element = index + 1, i.e., arr[i] - i == 1.
	// After the missing number:  element = index + 2, i.e., arr[i] - i == 2.
	// So we need to find the FIRST index where arr[i] - i == 2. That index + 1 is the missing number.
	// 1. Initialize start = 0, end = n - 1, ans = -1.
	// 2. While start <= end, find mid:
	//    Case 1: arr[mid] - mid == 1  ->  Missing number is on the right. So, start = mid + 1.
	//    Case 2: arr[mid] - mid == 2  ->  mid is after the missing number. Store ans = mid, as mid can be the
	//                                     first such index. Then look for an earlier one on the left: end = mid - 1.
	// 3. If ans == -1, no index is shifted, so the missing number is the last one, i.e., n + 1. Else, it is ans + 1.
	// T.C: O(logn)
	// S.C: O(1)
	public int FindMissing(int[] arr)
	{
		int n = arr.Length;
		int start = 0, end = n - 1;
		int ans = -1;

		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (arr[mid] - mid == 1) start = mid + 1;
			else
			{
				ans = mid;
				end = mid - 1;
			}
		}

		return (ans == -1) ? n + 1 : ans + 1;
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 1, 2, 3, 4, 5, 7, 8, 9 };

		Console.WriteLine(new Solution().FindMissing_XOR(arr));   // o/p: 6
		Console.WriteLine(new Solution().FindMissing(arr));       // o/p: 6
	}
}

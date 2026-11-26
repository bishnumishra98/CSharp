// Leetcode: 153. Find Minimum in Rotated Sorted Array   --->   Suppose an array of length n sorted in ascending order
// is rotated between 1 and n times. For example, the array nums = [0,1,2,4,5,6,7] might become:
// • [4,5,6,7,0,1,2] if it was rotated 4 times.
// • [0,1,2,4,5,6,7] if it was rotated 7 times.
// Notice that rotating an array [a[0], a[1], a[2], ..., a[n-1]] 1 time results in the array [a[n-1], a[0], a[1],
// a[2], ..., a[n-2]].
// Given the sorted rotated array nums of unique elements, return the minimum element of this array.
// You must write an algorithm that runs in O(log n) time.

// Example 1:
// Input: nums = [3,4,5,1,2]
// Output: 1
// Explanation: The original array was [1,2,3,4,5] rotated 3 times.

// Example 2:
// Input: nums = [4,5,6,7,0,1,2]
// Output: 0
// Explanation: The original array was [0,1,2,4,5,6,7] and it was rotated 4 times.

// Example 3:
// Input: nums = [11,13,15,17]
// Output: 11
// Explanation: The original array was [11,13,15,17] and it was rotated 4 times.

// What is a pivot index ?
// In a rotated sorted array, the pivot is the LARGEST element, i.e., the point where the values drop from the largest
// to the smallest. e.g., in [40, 50, 60, 70, 10, 20, 30], the pivot is 70 (index 3).
// A rotated sorted array is made of 2 sorted lines:
// ● Line A (upper line): 40, 50, 60, 70   ->  the pivot is the last element of line A
// ● Line B (lower line): 10, 20, 30       ->  the minimum is the first element of line B
// Every element of line A is bigger than every element of line B.


using System;

public class Solution
{
	// Brute force: Check every element and keep the smallest one.
	// T.C: O(n)
	// S.C: O(1)
	public int FindMin_BruteForce(int[] nums)
	{
		int mini = nums[0];
		for (int i = 1; i < nums.Length; i++) mini = Math.Min(mini, nums[i]);
		return mini;
	}

// ------------------------------------------------------------------------------

	// Find pivot index (largest element) using binary search. At 'mid', there can be 4 cases:
	// Case 1: nums[mid] > nums[mid + 1]  ->  Only the pivot is bigger than its next element. Return mid.
	// Case 2: nums[mid] < nums[mid - 1]  ->  Only the smallest element (first of line B) is smaller than its previous
	//                                        element. The pivot is just before it. Return mid - 1.
	// Case 3: nums[mid] < nums[start]    ->  mid is on line B. The pivot is on line A, i.e., on the left: end = mid - 1.
	// Case 4: nums[mid] >= nums[start]   ->  mid is on line A. The pivot is on the right: start = mid + 1.
	// If only 1 element is left (start == end), that element is the pivot. (This also handles a non-rotated array,
	// where the pivot is the last element.)
	// T.C: O(logn)
	// S.C: O(1)
	public int FindPivotIndex(int[] nums)
	{
		int n = nums.Length;
		int start = 0, end = n - 1;

		while (start <= end)
		{
			if (start == end) return start;

			int mid = start + (end - start) / 2;

			if (mid + 1 < n && nums[mid] > nums[mid + 1]) return mid;
			else if (mid - 1 >= 0 && nums[mid] < nums[mid - 1]) return mid - 1;
			else if (nums[mid] < nums[start]) end = mid - 1;
			else start = mid + 1;   // nums[mid] >= nums[start]
		}

		return -1;   // never reached
	}

// ------------------------------------------------------------------------------

	// Better algorithm: The minimum is just after the pivot (the largest element).
	// Edge case: If the pivot is the last element (array is not rotated), the minimum is the first element.
	// T.C: O(logn)
	// S.C: O(1)
	public int FindMin_UsingPivot(int[] nums)
	{
		int pivotIndex = FindPivotIndex(nums);
		if (pivotIndex == nums.Length - 1) return nums[0];
		return nums[pivotIndex + 1];
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Compare nums[mid] with nums[end] directly (fewer cases, no edge case).
	// Case 1: nums[mid] > nums[end]  ->  mid is on line A (the upper line). The minimum is on the right of mid:
	//                                    start = mid + 1.
	// Case 2: nums[mid] < nums[end]  ->  mid is on line B (from mid to end is sorted). The minimum is mid itself or on
	//                                    its left: end = mid (not mid - 1, because mid can be the minimum).
	// When start == end, it points to the minimum. Use 'while (start < end)' to avoid an infinite loop
	// (same reason as in '06_PeakIndexInAMountainArray.cs').
	// e.g., [4, 5, 6, 7, 0, 1, 2]: mid = 3 (7 > 2) -> start = 4. mid = 5 (1 < 2) -> end = 5. mid = 4 (0 < 1) -> end = 4.
	//       start == end = 4 -> nums[4] = 0.
	// T.C: O(logn)
	// S.C: O(1)
	public int FindMin(int[] nums)
	{
		int start = 0, end = nums.Length - 1;

		while (start < end)
		{
			int mid = start + (end - start) / 2;

			if (nums[mid] > nums[end]) start = mid + 1;
			else end = mid;
		}

		return nums[start];
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 4, 5, 6, 7, 0, 1, 2 };

		Console.WriteLine(new Solution().FindMin_BruteForce(nums));   // o/p: 0
		Console.WriteLine(new Solution().FindPivotIndex(nums));       // o/p: 3
		Console.WriteLine(new Solution().FindMin_UsingPivot(nums));   // o/p: 0
		Console.WriteLine(new Solution().FindMin(nums));              // o/p: 0
	}
}

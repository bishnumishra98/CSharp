// Leetcode: 33. Search in Rotated Sorted Array   --->   There is an integer array nums sorted in ascending order (with
// distinct values).
// Prior to being passed to your function, nums is possibly rotated at an unknown pivot index k (1 <= k < nums.length)
// such that the resulting array is [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]] (0-indexed).
// For example, [0,1,2,4,5,6,7] might be rotated at pivot index 3 and become [4,5,6,7,0,1,2].
// Given the array nums after the possible rotation and an integer target, return the index of target if it is in nums,
// or -1 if it is not in nums.
// You must write an algorithm with O(log n) runtime complexity.

// Example 1:
// Input: nums = [4,5,6,7,0,1,2], target = 0
// Output: 4

// Example 2:
// Input: nums = [4,5,6,7,0,1,2], target = 3
// Output: -1

// Example 3:
// Input: nums = [1], target = 0
// Output: -1


using System;

public class Solution
{
	// Brute force: Linear search.
	// T.C: O(n)
	// S.C: O(1)
	public int Search_Linear(int[] nums, int target)
	{
		for (int i = 0; i < nums.Length; i++)
		{
			if (nums[i] == target) return i;
		}
		return -1;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Find pivot, then do binary search on the correct sorted part.
	// 1. Find the pivot index (largest element). It splits the array into 2 sorted lines:
	//    Line A: nums[0 ... pivot],  Line B: nums[pivot + 1 ... n - 1]
	//    (How to find the pivot: see '10_FindMinimumInRotatedSortedArray.cs'.)
	// 2. If nums[0] <= target <= nums[pivot], the target can only be on line A. Else, it can only be on line B.
	//    Apply normal binary search on that line.
	// T.C: O(2 * logn) = O(logn)
	// S.C: O(1)
	public int Search_UsingPivot(int[] nums, int target)
	{
		int n = nums.Length;
		int pivotIndex = FindPivotIndex(nums);

		if (target >= nums[0] && target <= nums[pivotIndex]) return BinarySearch(nums, 0, pivotIndex, target);
		return BinarySearch(nums, pivotIndex + 1, n - 1, target);
	}

	private int FindPivotIndex(int[] nums)
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
			else start = mid + 1;
		}

		return -1;
	}

	private int BinarySearch(int[] nums, int start, int end, int target)
	{
		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (target == nums[mid]) return mid;
			else if (target > nums[mid]) start = mid + 1;
			else end = mid - 1;
		}
		return -1;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: One binary search, without finding the pivot.
	// Key idea: If we cut a rotated sorted array at 'mid', at least ONE of the two halves is always sorted.
	// 1. If nums[mid] == target, return mid.
	// 2. If nums[start] <= nums[mid], the LEFT half [start ... mid] is sorted.
	//    -> If target lies in it (nums[start] <= target < nums[mid]), go left: end = mid - 1.
	//    -> Else, go right: start = mid + 1.
	// 3. Else, the RIGHT half [mid ... end] is sorted.
	//    -> If target lies in it (nums[mid] < target <= nums[end]), go right: start = mid + 1.
	//    -> Else, go left: end = mid - 1.
	// e.g., [4, 5, 6, 7, 0, 1, 2], target = 0
	//       mid = 3 (7). Left half [4, 5, 6, 7] is sorted, but 0 is not in it. Go right: start = 4.
	//       mid = 5 (1). Left half [0, 1] is sorted, and 0 is in it. Go left: end = 4.
	//       mid = 4 (0). Found at index 4.
	// T.C: O(logn);   only 1 binary search, instead of 2 in the better approach
	// S.C: O(1)
	public int Search(int[] nums, int target)
	{
		int start = 0, end = nums.Length - 1;

		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (nums[mid] == target) return mid;

			if (nums[start] <= nums[mid])   // left half is sorted
			{
				if (nums[start] <= target && target < nums[mid]) end = mid - 1;
				else start = mid + 1;
			}
			else   // right half is sorted
			{
				if (nums[mid] < target && target <= nums[end]) start = mid + 1;
				else end = mid - 1;
			}
		}

		return -1;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 4, 5, 6, 7, 0, 1, 2 };
		int target = 0;

		Console.WriteLine(new Solution().Search_Linear(nums, target));       // o/p: 4
		Console.WriteLine(new Solution().Search_UsingPivot(nums, target));   // o/p: 4
		Console.WriteLine(new Solution().Search(nums, target));              // o/p: 4
	}
}

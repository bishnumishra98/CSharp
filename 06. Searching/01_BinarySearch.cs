// Leetcode: 704. Binary Search   --->   Given an array of integers nums which is sorted in ascending order, and an
// integer target, write a function to search target in nums. If target exists, then return its index. Otherwise,
// return -1.
// You must write an algorithm with O(log n) runtime complexity.

// Example 1:
// Input: nums = [-1,0,3,5,9,12], target = 9
// Output: 4
// Explanation: 9 exists in nums and its index is 4

// Example 2:
// Input: nums = [-1,0,3,5,9,12], target = 2
// Output: -1
// Explanation: 2 does not exist in nums so return -1

// Note: Binary search works only on sorted arrays (in general, on "monotonic" data, i.e., data that only goes up or
// only goes down).


using System;

public class Solution
{
	// Linear search (brute force): Check every element one by one, from left to right.
	// If an element is equal to target, return its index. If the loop ends, the target is not present, so return -1.
	// It works on unsorted arrays too, but it doesn't use the fact that the array is sorted.
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

	// Binary search: Since the array is sorted, look at the middle element and throw away half of the array every time.
	// 1. Initialize start = 0 and end = n - 1.
	// 2. While start <= end, find mid = start + (end - start) / 2 and compare nums[mid] with target:
	//    Case 1: target == nums[mid]  ->  Target found. Return mid.
	//    Case 2: target > nums[mid]   ->  Target can only be on the right part. So, start = mid + 1.
	//    Case 3: target < nums[mid]   ->  Target can only be on the left part. So, end = mid - 1.
	// 3. If the loop ends (start crosses end), the target is not present. Return -1.
	// Why 'start <= end' and not 'start < end' ? When start == end, 1 element is still left to check.
	// Why 'start + (end - start) / 2' and not '(start + end) / 2' ? Both give the same answer, but 'start + end' can
	// go out of int range (overflow) when both are very big numbers.
	// T.C: O(logn);   every step cuts the array into half
	// S.C: O(1)
	public int Search(int[] nums, int target)
	{
		int start = 0, end = nums.Length - 1;

		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (target == nums[mid]) return mid;
			else if (target > nums[mid]) start = mid + 1;   // go to right part
			else end = mid - 1;                             // go to left part
		}

		return -1;
	}
}

// Built-in binary search in C# (works only on sorted arrays/lists):
// ● Array.BinarySearch(arr, target) / list.BinarySearch(target)
//   Returns the index of target if found. If not found, it returns a negative number. Taking its bitwise NOT (~) gives
//   the index where target should be inserted, i.e., the index of the first element greater than target.
//   e.g., arr = [1, 2, 5, 7, 9]  ->  Array.BinarySearch(arr, 7) = 3,   ~Array.BinarySearch(arr, 6) = 3
// ● If the array has duplicates, it returns the index of ANY one of them, not necessarily the first one.
//   To find the first or last occurrence, see '05_FindFirstAndLastPositionOfElementInSortedArray.cs'.


class Program
{
	static void Main()
	{
		int[] nums = { -1, 0, 3, 5, 9, 12 };
		int target = 9;

		Console.WriteLine(new Solution().Search_Linear(nums, target));   // o/p: 4
		Console.WriteLine(new Solution().Search(nums, target));          // o/p: 4
	}
}

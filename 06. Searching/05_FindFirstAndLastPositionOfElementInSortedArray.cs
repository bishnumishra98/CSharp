// Leetcode: 34. Find First and Last Position of Element in Sorted Array   --->   Given an array of integers nums
// sorted in non-decreasing order, find the starting and ending position of a given target value.
// If target is not found in the array, return [-1, -1].
// You must write an algorithm with O(log n) runtime complexity.

// Example 1:
// Input: nums = [5,7,7,8,8,10], target = 8
// Output: [3,4]

// Example 2:
// Input: nums = [5,7,7,8,8,10], target = 6
// Output: [-1,-1]

// Example 3:
// Input: nums = [], target = 0
// Output: [-1,-1]

// Extra: Total number of occurrences of target = last - first + 1 (only when target is found).


using System;

public class Solution
{
	// Brute force: Go through the array once. The first time we see target, store it as 'first'.
	// Every time we see target, update 'last'.
	// T.C: O(n)
	// S.C: O(1)
	public int[] SearchRange_BruteForce(int[] nums, int target)
	{
		int first = -1, last = -1;

		for (int i = 0; i < nums.Length; i++)
		{
			if (nums[i] == target)
			{
				if (first == -1) first = i;
				last = i;
			}
		}

		return new int[] { first, last };
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search, 2 times.
	// In normal binary search, we return mid as soon as we find target. But here, mid may not be the first or last
	// occurrence. So, when target is found, store it in 'ans' and keep searching:
	// ● First occurrence: store ans = mid, then go left (end = mid - 1) to find an earlier occurrence.
	// ● Last occurrence:  store ans = mid, then go right (start = mid + 1) to find a later occurrence.
	// Both searches are the same, except for this one line, so we use one helper with a 'findFirst' flag.
	// T.C: O(2 * logn) = O(logn)
	// S.C: O(1)
	public int[] SearchRange(int[] nums, int target)
	{
		return new int[] { FindPosition(nums, target, true), FindPosition(nums, target, false) };
	}

	private int FindPosition(int[] nums, int target, bool findFirst)
	{
		int start = 0, end = nums.Length - 1;
		int ans = -1;

		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (target > nums[mid]) start = mid + 1;
			else if (target < nums[mid]) end = mid - 1;
			else
			{
				ans = mid;
				if (findFirst) end = mid - 1;   // look for an earlier occurrence
				else start = mid + 1;           // look for a later occurrence
			}
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 5, 7, 7, 8, 8, 10 };
		int target = 8;

		Console.WriteLine(string.Join(" ", new Solution().SearchRange_BruteForce(nums, target)));   // o/p: 3 4
		Console.WriteLine(string.Join(" ", new Solution().SearchRange(nums, target)));              // o/p: 3 4
	}
}

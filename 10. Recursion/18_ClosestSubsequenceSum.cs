// Leetcode: 1755. Closest Subsequence Sum   --->   You are given an integer array nums and an integer goal.
// You want to choose a subsequence of nums such that the sum of its elements is the closest possible to goal. That is,
// if the sum of the subsequence's elements is sum, then you want to minimize the absolute difference abs(sum - goal).
// Return the minimum possible value of abs(sum - goal).
// Note that a subsequence of an array is an array formed by removing some elements (possibly all or none) of the
// original array.

// Example 1:
// Input: nums = [5,-7,3,5], goal = 6
// Output: 0
// Explanation: Choose the whole array as a subsequence, with a sum of 6. This is equal to the goal, so the absolute
// difference is 0.

// Example 2:
// Input: nums = [7,-9,15,-2], goal = -5
// Output: 1
// Explanation: Choose the subsequence [7,-9,-2], with a sum of -4. The absolute difference is abs(-4 - (-5)) = 1.

// Example 3:
// Input: nums = [1,2,3], goal = -7
// Output: 7

// Constraints: 1 <= nums.length <= 40,  -10^7 <= nums[i] <= 10^7,  -10^9 <= goal <= 10^9
// First understand '04_SubsetSums.cs'.


using System;
using System.Collections.Generic;

public class Solution
{
	// All subset sums of arr[index ...], added to 'sum' (same as '04_SubsetSums.cs').
	private void SubsetSums(int index, int[] arr, int sum, List<int> sums)
	{
		if (index == arr.Length)
		{
			sums.Add(sum);
			return;
		}

		SubsetSums(index + 1, arr, sum + arr[index], sums);   // pick
		SubsetSums(index + 1, arr, sum, sums);                // not pick
	}

	// Brute force: Find all 2^n subset sums, and take the one closest to goal.
	// Problem: n can be 40, and 2^40 ≈ 10^12 subsets. Far too slow.
	// T.C: O(2^n)
	// S.C: O(2^n);   to store all the sums
	public int MinAbsDifference_BruteForce(int[] nums, int goal)
	{
		List<int> sums = new List<int>();
		SubsetSums(0, nums, 0, sums);

		int best = int.MaxValue;
		foreach (int s in sums) best = Math.Min(best, Math.Abs(s - goal));
		return best;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Meet in the Middle.
	// Idea: Split nums into 2 halves. Every subsequence = (a part from the left half) + (a part from the right half).
	//       So every subsequence sum = leftSum + rightSum.
	//       Each half has only n/2 = 20 elements, so 2^20 ≈ 10^6 sums each. Much smaller than 2^40.
	// 1. Find all subset sums of the left half and of the right half.
	// 2. Sort the right half's sums.
	// 3. For every leftSum, we want a rightSum as close as possible to (goal - leftSum). Find it with binary search in the
	//    sorted right sums: check the first value >= (goal - leftSum), and the value just before it. One of these two is
	//    the closest.
	// e.g., nums = [5, -7, 3, 5], goal = 6:  left = [5, -7] -> sums {-2, 5, -7, 0},  right = [3, 5] -> sums {0, 3, 5, 8}
	//       leftSum = -2: need 8 -> found 8 exactly -> difference 0.
	// T.C: O(2^(n/2) * n);   making and sorting 2^(n/2) sums, and one binary search (log(2^(n/2)) = n/2) for each left sum
	// S.C: O(2^(n/2));   for the two lists of sums
	public int MinAbsDifference(int[] nums, int goal)
	{
		int n = nums.Length;
		int[] leftHalf = nums[..(n / 2)];   // [..x] = elements from index 0 to x - 1 (C# range syntax)
		int[] rightHalf = nums[(n / 2)..];  // [x..] = elements from index x to the end

		List<int> leftSums = new List<int>(), rightSums = new List<int>();
		SubsetSums(0, leftHalf, 0, leftSums);
		SubsetSums(0, rightHalf, 0, rightSums);
		rightSums.Sort();

		int best = int.MaxValue;
		foreach (int leftSum in leftSums)
		{
			int need = goal - leftSum;
			int idx = LowerBound(rightSums, need);   // first index with rightSums[idx] >= need

			if (idx < rightSums.Count) best = Math.Min(best, Math.Abs(leftSum + rightSums[idx] - goal));
			if (idx > 0) best = Math.Min(best, Math.Abs(leftSum + rightSums[idx - 1] - goal));
			if (best == 0) return 0;   // can't do better than exact
		}

		return best;
	}

	// Binary search: index of the first element >= target (or list.Count if every element is smaller).
	private int LowerBound(List<int> list, int target)
	{
		int low = 0, high = list.Count - 1;
		while (low <= high)
		{
			int mid = low + (high - low) / 2;
			if (list[mid] < target) low = mid + 1;
			else high = mid - 1;
		}
		return low;
	}
}

// Note: Leetcode 2035. Partition Array Into Two Arrays to Minimize Sum Difference uses the same Meet in the Middle idea,
//       but it is rarely asked (mostly in senior-level interviews), so it is not included here.


class Program
{
	static void Main()
	{
		int[] nums = { 5, -7, 3, 5 };
		int goal = 6;

		Console.WriteLine(new Solution().MinAbsDifference_BruteForce(nums, goal));   // o/p: 0
		Console.WriteLine(new Solution().MinAbsDifference(nums, goal));              // o/p: 0
	}
}

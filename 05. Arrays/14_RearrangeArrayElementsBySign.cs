// Leetcode: 2149. Rearrange Array Elements by Sign   --->   You are given a 0-indexed integer array nums of
// even length consisting of an equal number of positive and negative integers.
// You should return the array of nums such that the array follows the given conditions:
// 1. Every consecutive pair of integers have opposite signs.
// 2. For all integers with the same sign, the order in which they were present in nums is preserved.
// 3. The rearranged array begins with a positive integer.
// Return the modified array after rearranging the elements to satisfy the aforementioned conditions.

// Example 1:
// Input: nums = [3,1,-2,-5,2,-4]
// Output: [3,-2,1,-5,2,-4]
// Explanation:
// The positive integers in nums are [3,1,2]. The negative integers are [-2,-5,-4].
// The only possible way to rearrange them such that they satisfy all conditions is [3,-2,1,-5,2,-4].

// Example 2:
// Input: nums = [-1,1]
// Output: [1,-1]


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force algorithm:
	// 1. Put all positive numbers in one list and all negative numbers in another list (order stays the same).
	// 2. Build the answer by taking 1 positive, then 1 negative, then 1 positive, and so on.
	// T.C: O(n) + O(n / 2) = O(1.5n)
	// S.C: O(2n)
	public int[] RearrangeArray_BruteForce(int[] nums)
	{
		int n = nums.Length;
		List<int> positive = new List<int>();
		List<int> negative = new List<int>();

		for (int i = 0; i < n; i++)
		{
			if (nums[i] > 0) positive.Add(nums[i]);
			else negative.Add(nums[i]);
		}

		int[] arr = new int[n];
		int index = 0;

		for (int i = 0; i < n / 2; i++)
		{
			arr[index++] = positive[i];
			arr[index++] = negative[i];
		}

		return arr;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Two pointer approach
	// In the answer, positives always sit at even indexes (0, 2, 4...) and negatives at odd indexes (1, 3, 5...).
	// 1. Keep 'even' = 0 (next place for a positive) and 'odd' = 1 (next place for a negative).
	// 2. Go through nums: put a positive at ans[even] and do even += 2; put a negative at ans[odd] and do odd += 2.
	// T.C: O(n)
	// S.C: O(n)
	public int[] RearrangeArray(int[] nums)
	{
		int[] ans = new int[nums.Length];
		int even = 0, odd = 1;

		for (int i = 0; i < nums.Length; i++)
		{
			if (nums[i] > 0)
			{
				ans[even] = nums[i];
				even += 2;
			}
			else   // if (nums[i] < 0)
			{
				ans[odd] = nums[i];
				odd += 2;
			}
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 3, 1, -2, -5, 2, -4 };

		// int[] ans = new Solution().RearrangeArray_BruteForce(nums);
		int[] ans = new Solution().RearrangeArray(nums);

		Console.WriteLine(string.Join(" ", ans));   // o/p: 3 -2 1 -5 2 -4
	}
}

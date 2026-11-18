// Leetcode: 1. Two Sum   --->   Given an array of integers nums and an integer target, return indices of the two numbers such
// that they add up to target.
// You may assume that each input would have exactly one solution, and you may not use the same element twice.
// You can return the answer in any order.

// Example 1:
// Input: nums = [2,7,11,15], target = 9
// Output: [0,1]
// Explanation: Because nums[0] + nums[1] == 9, we return [0, 1].

// Example 2:
// Input: nums = [3,2,4], target = 6
// Output: [1,2]

// Example 3:
// Input: nums = [3,3], target = 6
// Output: [0,1]


using System;
using System.Collections.Generic;

public class Solution
{
	// T.C: O(n^2)
	// S.C: O(1)
	public int[] TwoSum_BruteForce(int[] nums, int target)
	{
		for (int i = 0; i < nums.Length - 1; i++)
		{
			for (int j = i + 1; j < nums.Length; j++)
			{
				if (nums[i] + nums[j] == target) return new int[] { i, j };
			}
		}

		return new int[0];   // if no pair found, return blank array
	}

// ----------------------------------------------------------------------------------------------------------------

	// Hashing techinique using Dictionary
	// T.C: O(n)
	// S.C: O(n)
	public int[] TwoSum(int[] nums, int target)
	{
		Dictionary<int, int> map = new Dictionary<int, int>();

		for (int i = 0; i < nums.Length; i++)
		{
			int complement = target - nums[i];
			// If the complement exists in the map, return the stored index for the complement and the current index,
			// else create an entry for nums[i] in the map.
			if (map.ContainsKey(complement)) return new int[] { i, map[complement] };
			map[nums[i]] = i;
		}

		return new int[0];   // if no pair found, return blank array
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 2, 7, 11, 15 };
		int target = 9;

		int[] ans1 = new Solution().TwoSum_BruteForce(nums, target);
		Array.Sort(ans1);
		foreach (var it in ans1) Console.Write(it + " ");

		Console.WriteLine();

		int[] ans2 = new Solution().TwoSum(nums, target);
		Array.Sort(ans2);
		foreach (var it in ans2) Console.Write(it + " ");
	}
}

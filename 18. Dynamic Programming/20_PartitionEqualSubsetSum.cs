// Leetcode: 416. Partition Equal Subset Sum   --->   Given an integer array nums, return true if you can split it into
// TWO subsets with the SAME sum.

// Example 1:
// Input: nums = [1,5,11,5]
// Output: true    ([1, 5, 5] and [11], both 11)

// Example 2:
// Input: nums = [1,2,3,5]
// Output: false   (the total is 11, odd, so two equal halves are impossible)

// First understand '19_SubsetSumProblem.cs'.


using System;

public class Solution
{
	// Algorithm: If the two halves are equal, each is total / 2.
	// 1. If the total is ODD, it can not be split into two equal whole numbers: false.
	// 2. Otherwise, find ANY subset with sum total / 2 (Subset Sum). The elements left over automatically add up to the
	//    other total / 2.
	// T.C: O(n * total)
	// S.C: O(total)
	public bool CanPartition(int[] nums)
	{
		int total = 0;
		foreach (int x in nums) total += x;

		if (total % 2 == 1) return false;
		return IsSubsetSum(nums, total / 2);
	}

	// Space-optimized Subset Sum, from '19_SubsetSumProblem.cs'.
	private bool IsSubsetSum(int[] arr, int sum)
	{
		bool[] prev = new bool[sum + 1];
		prev[0] = true;
		if (arr[0] <= sum) prev[arr[0]] = true;

		for (int i = 1; i < arr.Length; i++)
		{
			bool[] curr = new bool[sum + 1];
			curr[0] = true;
			for (int t = 1; t <= sum; t++) curr[t] = prev[t] || (arr[i] <= t && prev[t - arr[i]]);
			prev = curr;
		}
		return prev[sum];
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().CanPartition(new int[] { 1, 5, 11, 5 }));   // o/p: True
	}
}

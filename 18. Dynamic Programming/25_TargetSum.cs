// Leetcode: 494. Target Sum   --->   Given an integer array nums (non-negative) and an integer target, put a '+' or a '-'
// in front of EVERY number. Count the ways for the expression to evaluate to target.

// Example 1:
// Input: nums = [1,1,1,1,1], target = 3
// Output: 5
// Explanation: -1+1+1+1+1, +1-1+1+1+1, +1+1-1+1+1, +1+1+1-1+1, +1+1+1+1-1

// Example 2:
// Input: nums = [1], target = 1
// Output: 1

// First understand '23_PartitionsWithGivenDifference.cs'. This is the SAME problem in disguise.


using System;

public class Solution
{
	// Brute force: try '+' and '-' for every number.
	// T.C: O(2^n)
	// S.C: O(n)
	public int FindTargetSumWays_Recursion(int[] nums, int target)
	{
		return Ways(nums.Length - 1, target, nums);
	}

	private int Ways(int i, int remaining, int[] nums)
	{
		if (i < 0) return remaining == 0 ? 1 : 0;
		return Ways(i - 1, remaining - nums[i], nums) + Ways(i - 1, remaining + nums[i], nums);
	}

// ------------------------------------------------------------------------------

	// Optimal: Collect the numbers with '+' into one group P and the numbers with '-' into another group M. The
	// expression is sum(P) - sum(M). For example: -1 +2 +3 -1 = (2 + 3) - (1 + 1).
	// So "signs that give target" = "split into 2 subsets whose difference is target". That is exactly
	// Partitions with Given Difference: count the subsets with sum (total - target) / 2 (they become M).
	// A negative target works the same way: flipping every sign turns target into -target, so the count for target and
	// -target is the same. Math.Abs keeps the formula simple.
	// T.C: O(n * s);   s = (total - |target|) / 2
	// S.C: O(s)
	public int FindTargetSumWays(int[] nums, int target)
	{
		int total = 0;
		foreach (int x in nums) total += x;

		int d = Math.Abs(target);
		if (total - d < 0 || (total - d) % 2 == 1) return 0;
		return PerfectSum(nums, (total - d) / 2);
	}

	// Space-optimized Perfect Sum, from '22_PerfectSumProblem.cs'.
	private int PerfectSum(int[] arr, int target)
	{
		int[] prev = new int[target + 1];
		prev[0] = arr[0] == 0 ? 2 : 1;
		if (arr[0] != 0 && arr[0] <= target) prev[arr[0]] = 1;

		for (int i = 1; i < arr.Length; i++)
		{
			int[] curr = new int[target + 1];
			for (int t = 0; t <= target; t++) curr[t] = prev[t] + (arr[i] <= t ? prev[t - arr[i]] : 0);
			prev = curr;
		}
		return prev[target];
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 1, 1, 1, 1 };

		Console.WriteLine(new Solution().FindTargetSumWays_Recursion(nums, 3));   // o/p: 5
		Console.WriteLine(new Solution().FindTargetSumWays(nums, 3));             // o/p: 5
	}
}

// Leetcode: 198. House Robber   --->   Houses stand in a row, nums[i] is the money in house i. You can not rob two
// ADJACENT houses (their alarms are connected). Return the maximum money you can rob.

// Example 1:
// Input: nums = [1,2,3,1]
// Output: 4    (rob house 0 and house 2: 1 + 3)

// Example 2:
// Input: nums = [2,7,9,3,1]
// Output: 12   (rob houses 0, 2 and 4: 2 + 9 + 1)

// DP pattern: TAKE / NOT TAKE (pick or skip). It appears in many problems later (subset sum, knapsack ...).

// The recursion: f(i) = the most money from houses 0..i. At house i there are 2 choices:
//   Rob it:      nums[i] + f(i - 2)    (then house i - 1 can not be robbed)
//   Skip it:     f(i - 1)
//   f(i) = max(rob, skip)
//   f(0) = nums[0],  f(negative) = 0   (no houses left)


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n);   recursion stack
	public int Rob_Recursion(int[] nums)
	{
		return MaxMoney(nums.Length - 1, nums);
	}

	private int MaxMoney(int i, int[] nums)
	{
		if (i < 0) return 0;
		if (i == 0) return nums[0];

		int rob = nums[i] + MaxMoney(i - 2, nums);
		int skip = MaxMoney(i - 1, nums);
		return Math.Max(rob, skip);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public int Rob_Memoization(int[] nums)
	{
		int[] dp = new int[nums.Length];
		Array.Fill(dp, -1);
		return MaxMoney(nums.Length - 1, nums, dp);
	}

	private int MaxMoney(int i, int[] nums, int[] dp)
	{
		if (i < 0) return 0;
		if (i == 0) return nums[0];
		if (dp[i] != -1) return dp[i];

		int rob = nums[i] + MaxMoney(i - 2, nums, dp);
		int skip = MaxMoney(i - 1, nums, dp);
		return dp[i] = Math.Max(rob, skip);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public int Rob_Tabulation(int[] nums)
	{
		int n = nums.Length;
		int[] dp = new int[n];
		dp[0] = nums[0];

		for (int i = 1; i < n; i++)
		{
			int rob = nums[i] + (i >= 2 ? dp[i - 2] : 0);
			int skip = dp[i - 1];
			dp[i] = Math.Max(rob, skip);
		}
		return dp[n - 1];
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(1)
	public int Rob_SpaceOptimized(int[] nums)
	{
		int prev = nums[0], prev2 = 0;   // prev = f(i - 1), prev2 = f(i - 2)

		for (int i = 1; i < nums.Length; i++)
		{
			int curr = Math.Max(nums[i] + prev2, prev);
			prev2 = prev;
			prev = curr;
		}
		return prev;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 2, 7, 9, 3, 1 };

		Console.WriteLine(new Solution().Rob_Recursion(nums));        // o/p: 12
		Console.WriteLine(new Solution().Rob_Memoization(nums));      // o/p: 12
		Console.WriteLine(new Solution().Rob_Tabulation(nums));       // o/p: 12
		Console.WriteLine(new Solution().Rob_SpaceOptimized(nums));   // o/p: 12
	}
}

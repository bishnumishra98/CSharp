// Leetcode: 673. Number of Longest Increasing Subsequence   --->   Given an integer array nums, return HOW MANY longest
// strictly increasing subsequences there are.

// Example 1:
// Input: nums = [1,3,5,4,7]
// Output: 2   ([1,3,4,7] and [1,3,5,7])

// Example 2:
// Input: nums = [2,2,2,2,2]
// Output: 5   (the LIS length is 1, and each of the 5 elements is one)

// First understand FindLIS in '46_LongestIncreasingSubsequence.cs'.


using System;

public class Solution
{
	// Algorithm: Next to dp[i] (the LIS length ending at i), keep count[i] = HOW MANY increasing subsequences of that
	// length end at i. For every j < i with nums[j] < nums[i]:
	//   1 + dp[j] > dp[i]:   a LONGER subsequence ending at i is found. The old ones do not matter any more:
	//                        dp[i] = 1 + dp[j],  count[i] = count[j]   (each one ending at j, extended by nums[i])
	//   1 + dp[j] == dp[i]:  ANOTHER group of the same best length:  count[i] += count[j]
	// The answer: add count[i] for every i where dp[i] is the overall LIS length.
	// e.g., nums  = [1, 3, 5, 8, 4, 7]
	//       dp    = [1, 2, 3, 4, 3, 4]
	//       count = [1, 1, 1, 1, 1, 2]   (7: from 5 (dp 3, count 1) and from 4 (dp 3, count 1) -> 2)
	//       LIS length 4, at index 3 (count 1) and index 5 (count 2): 3 in total.
	// T.C: O(n^2)
	// S.C: O(n)
	public int FindNumberOfLIS(int[] nums)
	{
		int n = nums.Length, longest = 1;
		int[] dp = new int[n], count = new int[n];

		for (int i = 0; i < n; i++)
		{
			dp[i] = 1;
			count[i] = 1;
			for (int j = 0; j < i; j++)
			{
				if (nums[j] >= nums[i]) continue;

				if (1 + dp[j] > dp[i])
				{
					dp[i] = 1 + dp[j];
					count[i] = count[j];
				}
				else if (1 + dp[j] == dp[i])
				{
					count[i] += count[j];
				}
			}
			longest = Math.Max(longest, dp[i]);
		}

		int total = 0;
		for (int i = 0; i < n; i++)
		{
			if (dp[i] == longest) total += count[i];
		}
		return total;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().FindNumberOfLIS(new int[] { 1, 3, 5, 4, 7 }));   // o/p: 2
	}
}

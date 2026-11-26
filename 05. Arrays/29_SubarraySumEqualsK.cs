// Leetcode: 560. Subarray Sum Equals K   --->   Given an array of integers nums and an integer k, return the total
// number of subarrays whose sum equals to k.
// A subarray is a contiguous non-empty sequence of elements within an array.

// Example 1:
// Input: nums = [1,1,1], k = 2
// Output: 2

// Example 2:
// Input: nums = [1,2,3], k = 3
// Output: 2

// NOTE: Understand 28_LongestSubarrayWithSumK.cs first. This problem uses the same prefix sum idea, but here we COUNT
//       the subarrays instead of finding the longest one.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force algorithm: Same as the optimised brute force of 28_LongestSubarrayWithSumK.cs.
	// Generate all subarrays using 2 loops (keep adding nums[j] to 'sum'), and count those whose sum is k.
	// T.C: O(n^2)
	// S.C: O(1)
	public int SubarraySum_BruteForce(int[] nums, int k)
	{
		int n = nums.Length;
		int count = 0;

		for (int i = 0; i < n; i++)
		{
			int sum = 0;
			for (int j = i; j < n; j++)
			{
				sum += nums[j];
				if (sum == k) count++;
			}
		}

		return count;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Prefix sum + Dictionary
	// If the current prefix sum is 'preSum', and we previously saw the prefix sum 'preSum - k', then there is a subarray
	// ending at the current index with sum k. If 'preSum - k' was seen 3 times before, then there are 3 such subarrays.
	// 1. Keep a Dictionary 'mp' that stores {prefix sum -> how many times it has occurred}, and a 'count' variable.
	//    Start with mp[0] = 1, meaning "a prefix sum of 0 has occurred once" (before the first element, the sum is 0).
	//    mp[0] = 1 handles the case when the prefix sum itself is equal to k (the subarray starts from index 0).
	// 2. For each element:
	//    i.   Add it to 'preSum'.
	//    ii.  If (preSum - k) exists in mp, there are mp[preSum - k] subarrays ending here with sum k. Add that to count.
	//    iii. Increase the frequency of the current prefix sum in mp, i.e., mp[preSum]++.
	// 3. Return 'count'.
	// T.C: O(n)
	// S.C: O(n)
	public int SubarraySum(int[] nums, int k)
	{
		int n = nums.Length;
		Dictionary<int, int> mp = new Dictionary<int, int>();   // {prefixSum -> frequency}
		mp[0] = 1;
		int preSum = 0, count = 0;

		for (int i = 0; i < n; i++)
		{
			preSum += nums[i];
			int complement = preSum - k;
			if (mp.ContainsKey(complement)) count += mp[complement];
			// Keep this line at the end. Otherwise you will get wrong output for test cases like nums = [1], k = 0.
			// GetValueOrDefault(key) gives the stored value if the key exists, else 0.
			mp[preSum] = mp.GetValueOrDefault(preSum) + 1;
		}

		return count;
	}

	// ● Why mp[preSum]++ (count the frequency), why not simply mp[preSum] = 1 ?
	//   Because the same prefix sum can occur many times (when the array has 0s or negatives), and each time it gives a
	//   different subarray. Dry run the test case: nums = [1, -1, 0], k = 0. The answer is 3: [1, -1], [0] and [1, -1, 0].
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().SubarraySum_BruteForce(new int[] { 1, 1, 1 }, 2));   // o/p: 2
		Console.WriteLine(new Solution().SubarraySum(new int[] { 1, 1, 1 }, 2));              // o/p: 2
	}
}

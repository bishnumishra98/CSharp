// Leetcode: 368. Largest Divisible Subset   --->   Given a set of DISTINCT positive integers, return the largest subset
// where for every pair (a, b), a % b == 0 or b % a == 0 (one divides the other). Any valid answer is accepted.

// Example 1:
// Input: nums = [1,2,3]
// Output: [1,2]   ([1,3] is also accepted)

// Example 2:
// Input: nums = [1,2,4,8]
// Output: [1,2,4,8]

// First understand FindLIS in '46_LongestIncreasingSubsequence.cs'. This is the same code with 2 changes.


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm:
	// Change 1: SORT the numbers. A SUBSET may be in any order, so sorting is allowed. Once sorted, a chain where each
	//           number divides the next one, like 2 | 4 | 8 | 16, has every pair dividing each other (divisibility
	//           carries over: 2 | 4 and 4 | 8 means 2 | 8).
	// Change 2: In the LIS code, replace "nums[j] < nums[i]" with "nums[i] % nums[j] == 0".
	// Then print the chain exactly like FindLIS (parent array, walk back, reverse).
	// e.g., sorted [2, 4, 7, 8, 16]: dp = [1, 2, 1, 3, 4], parent = [0, 0, 2, 1, 3]. Biggest dp at 16:
	//       16 -> 8 -> 4 -> 2 -> reversed [2, 4, 8, 16].
	// T.C: O(n^2);   + O(n log n) for the sort
	// S.C: O(n)
	public IList<int> LargestDivisibleSubset(int[] nums)
	{
		int n = nums.Length;
		int[] a = (int[])nums.Clone();
		Array.Sort(a);   // change 1

		int[] dp = new int[n], parent = new int[n];
		int last = 0;

		for (int i = 0; i < n; i++)
		{
			dp[i] = 1;
			parent[i] = i;
			for (int j = 0; j < i; j++)
			{
				if (a[i] % a[j] == 0 && 1 + dp[j] > dp[i])   // change 2
				{
					dp[i] = 1 + dp[j];
					parent[i] = j;
				}
			}
			if (dp[i] > dp[last]) last = i;
		}

		List<int> result = new List<int> { a[last] };
		while (parent[last] != last)
		{
			last = parent[last];
			result.Add(a[last]);
		}
		result.Reverse();
		return result;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(string.Join(" ", new Solution().LargestDivisibleSubset(new int[] { 4, 2, 8, 16, 7 })));   // o/p: 2 4 8 16
	}
}

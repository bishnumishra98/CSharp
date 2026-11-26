// GFG: Longest Bitonic Subsequence   --->   A subsequence is BITONIC if it first strictly increases and then strictly
// decreases (like a mountain). Return the length of the longest bitonic subsequence. Here a sequence that ONLY
// increases or ONLY decreases does NOT count (both slopes must exist); return 0 if there is none.

// Example 1:
// Input: nums = [1, 2, 5, 3, 2]
// Output: 5   (1, 2, 5 up, then 3, 2 down)

// Example 2:
// Input: nums = [1, 11, 2, 10, 4, 5, 2, 1]
// Output: 6   (1, 2, 10, 4, 2, 1)

// Example 3:
// Input: nums = [10, 20, 30]
// Output: 0   (only increasing)

// Problem link: https://www.geeksforgeeks.org/problems/longest-bitonic-subsequence0824/1

// First understand '46_LongestIncreasingSubsequence.cs'.


using System;

public class Solution
{
	// Algorithm: Try every index i as the PEAK of the mountain.
	// 1. up[i]   = the LIS that ENDS at i (the left slope, going up to i)          - the normal LIS dp, left to right.
	// 2. down[i] = the longest decreasing subsequence that STARTS at i (right slope) - the same dp, right to left.
	// 3. A mountain with peak i has length up[i] + down[i] - 1 (the peak is counted in both).
	//    Only count it if up[i] > 1 AND down[i] > 1, so that both slopes really exist.
	// e.g., nums = [1, 11, 2, 10, 4, 5, 2, 1]
	//       up   = [1,  2, 2,  3, 3, 4, 2, 1]
	//       down = [1,  5, 2,  4, 3, 3, 2, 1]
	//       At index 3 (10): 3 + 4 - 1 = 6. (Index 1 and index 5 also give 6.)
	// Why the "> 1" rule ? For [1, 4, 5, 2, 7, 8, 9], index 6 (9) has up = 6 and down = 1: just an increasing run, not a
	// mountain. With the rule, the best is index 2 (5): up 3 (1, 4, 5) + down 2 (5, 2) - 1 = 4.
	// (Note: The C++ comment about this rule was cut off in the middle of a sentence.)
	// T.C: O(n^2)
	// S.C: O(n)
	public int LongestBitonicSequence(int[] nums)
	{
		int n = nums.Length;
		int[] up = new int[n], down = new int[n];

		for (int i = 0; i < n; i++)
		{
			up[i] = 1;
			for (int j = 0; j < i; j++)
			{
				if (nums[j] < nums[i]) up[i] = Math.Max(up[i], 1 + up[j]);
			}
		}

		for (int i = n - 1; i >= 0; i--)
		{
			down[i] = 1;
			for (int j = n - 1; j > i; j--)
			{
				if (nums[j] < nums[i]) down[i] = Math.Max(down[i], 1 + down[j]);
			}
		}

		int best = 0;
		for (int i = 0; i < n; i++)
		{
			if (up[i] > 1 && down[i] > 1) best = Math.Max(best, up[i] + down[i] - 1);
		}
		return best;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().LongestBitonicSequence(new int[] { 1, 11, 2, 10, 4, 5, 2, 1 }));   // o/p: 6
	}
}

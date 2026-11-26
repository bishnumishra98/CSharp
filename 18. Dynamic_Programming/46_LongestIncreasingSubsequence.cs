// Leetcode: 300. Longest Increasing Subsequence   --->   Given an integer array nums, return the length of the longest
// STRICTLY increasing subsequence (LIS). Follow up: also PRINT one LIS.

// Example 1:
// Input: nums = [10,9,2,5,3,7,101,18]
// Output: 4   ([2,3,7,101] or [2,5,7,18] ...)

// Example 2:
// Input: nums = [0,1,0,3,2,3]
// Output: 4   ([0,1,2,3])

// Example 3:
// Input: nums = [7,7,7,7,7,7,7]
// Output: 1   (strictly increasing: equal values do not count)

// DP on LIS: the base of the next 5 files.


using System;
using System.Collections.Generic;

public class Solution
{
	// I. Recursion (take / not take): f(i, prev) = the longest increasing subsequence from index i on, if the last
	//    element taken so far is nums[prev] (prev = -1: nothing taken yet).
	//    Take nums[i] (only if prev == -1 or nums[i] > nums[prev]):  1 + f(i + 1, i)
	//    Skip it:                                                    f(i + 1, prev)
	//    f(n, any) = 0
	// T.C: O(2^n)
	// S.C: O(n)
	public int LengthOfLIS_Recursion(int[] nums)
	{
		return Longest(0, -1, nums);
	}

	private int Longest(int i, int prev, int[] nums)
	{
		if (i == nums.Length) return 0;

		int skip = Longest(i + 1, prev, nums);
		int take = (prev == -1 || nums[i] > nums[prev]) ? 1 + Longest(i + 1, i, nums) : 0;
		return Math.Max(take, skip);
	}

// ------------------------------------------------------------------------------

	// II. Memoization: prev goes from -1 to n - 1, and an array index can not be -1, so store it at prev + 1 (a SHIFT).
	// T.C: O(n^2)
	// S.C: O(n^2);   dp + O(n) recursion stack
	public int LengthOfLIS_Memoization(int[] nums)
	{
		int n = nums.Length;
		int[,] dp = new int[n, n + 1];
		for (int i = 0; i < n; i++)
		{
			for (int p = 0; p <= n; p++) dp[i, p] = -1;
		}
		return Longest(0, -1, nums, dp);
	}

	private int Longest(int i, int prev, int[] nums, int[,] dp)
	{
		if (i == nums.Length) return 0;
		if (dp[i, prev + 1] != -1) return dp[i, prev + 1];

		int skip = Longest(i + 1, prev, nums, dp);
		int take = (prev == -1 || nums[i] > nums[prev]) ? 1 + Longest(i + 1, i, nums, dp) : 0;
		return dp[i, prev + 1] = Math.Max(take, skip);
	}

// ------------------------------------------------------------------------------

	// III. A simpler O(n^2) DP (the one to know by heart):
	//   dp[i] = the length of the longest increasing subsequence that ENDS at nums[i].
	//   Every element alone is an LIS of length 1. For each i, look at every earlier j: if nums[j] < nums[i], nums[i]
	//   can extend the subsequence ending at j:  dp[i] = max(dp[i], 1 + dp[j]).
	//   The answer is the biggest dp[i].
	// To PRINT one LIS, also remember parent[i] = the j that gave dp[i] its value. Then start at the index with the
	// biggest dp and follow the parents back (and reverse).
	// e.g., nums   = [10, 9, 2, 5, 3, 7, 1, 18]
	//       dp     = [ 1, 1, 1, 2, 2, 3, 1,  4]
	//       parent = [ 0, 1, 2, 2, 2, 3, 6,  5]     (an index that came from nowhere is its own parent)
	//       Biggest dp is at index 7: 7 -> 5 -> 3 -> 2 (its own parent, stop): 18, 7, 5, 2 -> reversed: [2, 5, 7, 18].
	// T.C: O(n^2)
	// S.C: O(n)
	public List<int> FindLIS(int[] nums)
	{
		int n = nums.Length;
		int[] dp = new int[n], parent = new int[n];
		int lastIndex = 0;

		for (int i = 0; i < n; i++)
		{
			dp[i] = 1;
			parent[i] = i;
			for (int j = 0; j < i; j++)
			{
				if (nums[j] < nums[i] && 1 + dp[j] > dp[i])
				{
					dp[i] = 1 + dp[j];
					parent[i] = j;
				}
			}
			if (dp[i] > dp[lastIndex]) lastIndex = i;
		}

		List<int> lis = new List<int> { nums[lastIndex] };
		while (parent[lastIndex] != lastIndex)
		{
			lastIndex = parent[lastIndex];
			lis.Add(nums[lastIndex]);
		}
		lis.Reverse();
		return lis;
	}

// ------------------------------------------------------------------------------

	// IV. Optimal (Binary Search): Keep a list 'tails', where tails[k] = the SMALLEST possible last element of an
	//     increasing subsequence of length k + 1 seen so far. 'tails' is always sorted.
	//     For each x:
	//       x > tails' last element:  x extends the longest one: append it.
	//       otherwise:                find the first tails[k] >= x (binary search, "lower bound") and replace it with x.
	//                                 A smaller ending is better: it leaves more room for later numbers.
	//     The answer is tails.Count.
	// e.g., [0, 1, 0, 3, 2, 3]: [0] -> [0, 1] -> 0 replaces 0: [0, 1] -> [0, 1, 3] -> 2 replaces 3: [0, 1, 2] -> [0, 1, 2, 3].
	// Careful: 'tails' is NOT always a real LIS (its values may come from different subsequences). Only its LENGTH is
	//          right. To print one, use FindLIS above.
	// T.C: O(n log n)
	// S.C: O(n)
	public int LengthOfLIS(int[] nums)
	{
		List<int> tails = new List<int>();

		foreach (int x in nums)
		{
			int k = tails.BinarySearch(x);   // the index if x is found, else ~(the index of the first bigger element)
			if (k < 0) k = ~k;
			if (k == tails.Count) tails.Add(x);
			else tails[k] = x;
		}
		return tails.Count;
	}
}

// Note on List<T>.BinarySearch: if the value is found, it returns its index. If not, it returns a NEGATIVE number:
// the bitwise complement (~) of the index of the first element that is bigger. '~' turns it back into that index.
// (Values in 'tails' are distinct, so "found" already is the first element >= x, the C++ lower_bound.)


class Program
{
	static void Main()
	{
		int[] nums = { 10, 9, 2, 5, 3, 7, 1, 18 };

		Console.WriteLine(new Solution().LengthOfLIS_Recursion(nums));     // o/p: 4
		Console.WriteLine(new Solution().LengthOfLIS_Memoization(nums));   // o/p: 4
		Console.WriteLine(string.Join(" ", new Solution().FindLIS(nums))); // o/p: 2 5 7 18
		Console.WriteLine(new Solution().LengthOfLIS(nums));               // o/p: 4
	}
}

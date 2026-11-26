// GFG: Minimum Sum Partition   --->   Given an array of non-negative integers, split it into two sets (of ANY sizes) so
// that the absolute difference between their sums is as SMALL as possible. Return that difference.

// Example 1:
// Input: arr = [1, 6, 11, 5]
// Output: 1   ({1, 5, 6} = 12 and {11} = 11)

// Example 2:
// Input: arr = [1, 4]
// Output: 3

// Example 3:
// Input: arr = [1]
// Output: 1   ({1} and {})

// Problem link: https://www.geeksforgeeks.org/problems/minimum-sum-partition3317/1

// Note: '10. Recursion' has a harder version (Leetcode 2035) where the two parts must have EQUAL sizes. Here any sizes
//       are allowed, which makes it a plain Subset Sum problem.

// First understand '19_SubsetSumProblem.cs'.


using System;

public class Solution
{
	// Algorithm: Let total = the sum of all elements. If one set has sum s1, the other has s2 = total - s1, and the
	// difference is |s1 - s2| = |total - 2 * s1|.
	// So we only need to know WHICH sums s1 are possible, and pick the one that makes |total - 2 * s1| smallest.
	// The LAST row of the Subset Sum table tells exactly that: dp[n - 1][s] is true if some subset adds up to s.
	// Only s1 from 0 to total / 2 needs checking: every s1 above total / 2 is the mirror (s2) of one below it.
	// e.g., [1, 6, 11, 5], total 23. Possible sums up to 11: 0, 1, 5, 6, 7, 11. The best is s1 = 11: |23 - 22| = 1.
	// T.C: O(n * total)
	// S.C: O(total);   only the last row is kept (space-optimized Subset Sum)
	public int MinDifference(int[] arr)
	{
		int total = 0;
		foreach (int x in arr) total += x;

		bool[] possible = SubsetSums(arr, total);

		int best = int.MaxValue;
		for (int s1 = 0; s1 <= total / 2; s1++)
		{
			if (possible[s1]) best = Math.Min(best, total - 2 * s1);   // s1 <= total / 2, so this is never negative
		}
		return best;
	}

	// The last row of the Subset Sum table: possible[s] = can some subset add up to s ?
	private bool[] SubsetSums(int[] arr, int total)
	{
		bool[] prev = new bool[total + 1];
		prev[0] = true;
		if (arr[0] <= total) prev[arr[0]] = true;

		for (int i = 1; i < arr.Length; i++)
		{
			bool[] curr = new bool[total + 1];
			curr[0] = true;
			for (int t = 1; t <= total; t++) curr[t] = prev[t] || (arr[i] <= t && prev[t - arr[i]]);
			prev = curr;
		}
		return prev;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().MinDifference(new int[] { 1, 6, 11, 5 }));   // o/p: 1
	}
}

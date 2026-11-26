// Leetcode: 354. Russian Doll Envelopes   --->   envelopes[i] = [width, height]. An envelope fits inside another only if
// BOTH its width and its height are strictly smaller. Return the maximum number of envelopes you can put one inside
// the other (no rotating).

// Example 1:
// Input: envelopes = [[5,4],[6,4],[6,7],[2,3]]
// Output: 3   ([2,3] -> [5,4] -> [6,7])

// Example 2:
// Input: envelopes = [[1,1],[1,1],[1,1]]
// Output: 1

// First understand LengthOfLIS (binary search) in '46_LongestIncreasingSubsequence.cs'.


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm: 2D becomes 1D:
	// 1. Sort by width INCREASING. Now any chain of envelopes is an increasing subsequence in this order... as long as
	//    the heights also increase. So the answer is the LIS of the HEIGHTS.
	// 2. The trap: envelopes with the SAME width can not go inside each other, but their heights could still form an
	//    increasing run (e.g., [4,5] then [4,6]: heights 5, 6). Fix: for equal widths, sort by height DECREASING. Then
	//    inside one width the heights go down, so the LIS can take at most one envelope of each width.
	// e.g., [[4,5],[4,6],[6,7],[2,3],[1,1]] -> sorted [[1,1],[2,3],[4,6],[4,5],[6,7]] -> heights [1,3,6,5,7] -> LIS 4.
	//       (Sorting heights increasing would give [1,3,5,6,7] -> LIS 5, which wrongly nests [4,5] in [4,6].)
	// T.C: O(n log n);   sort + LIS with binary search
	// S.C: O(n)
	public int MaxEnvelopes(int[][] envelopes)
	{
		int[][] e = (int[][])envelopes.Clone();
		Array.Sort(e, (a, b) => a[0] != b[0] ? a[0].CompareTo(b[0]) : b[1].CompareTo(a[1]));

		List<int> tails = new List<int>();   // LIS of the heights, as in '46_LongestIncreasingSubsequence.cs'
		foreach (int[] env in e)
		{
			int k = tails.BinarySearch(env[1]);
			if (k < 0) k = ~k;
			if (k == tails.Count) tails.Add(env[1]);
			else tails[k] = env[1];
		}
		return tails.Count;
	}
}


class Program
{
	static void Main()
	{
		int[][] envelopes = { new[] { 5, 4 }, new[] { 6, 4 }, new[] { 6, 7 }, new[] { 2, 3 } };

		Console.WriteLine(new Solution().MaxEnvelopes(envelopes));   // o/p: 3
	}
}

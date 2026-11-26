// GFG: Partitions with Given Difference   --->   Split the array into two subsets (each element goes into exactly one,
// a subset may be empty), with sums s1 and s2. Count the splits where s1 >= s2 and s1 - s2 == d.

// Example 1:
// Input: arr = [5, 2, 6, 4], d = 3
// Output: 1   ({6, 4} = 10 and {5, 2} = 7)

// Example 2:
// Input: arr = [1, 1, 1, 1], d = 0
// Output: 6   (choose which 2 of the four 1's go into s1: 4 * 3 / 2 = 6 ways)

// Problem link: https://www.geeksforgeeks.org/problems/partitions-with-given-difference/1

// First understand '22_PerfectSumProblem.cs'.


using System;

public class Solution
{
	// Algorithm (a little algebra turns it into Perfect Sum):
	//   s1 + s2 = total     and     s1 - s2 = d
	//   Subtract them:  2 * s2 = total - d   ->   s2 = (total - d) / 2
	// So every valid split is just "a subset with sum (total - d) / 2" (it becomes s2, the rest becomes s1). Count those.
	// Two cases have no answer:
	//   total - d < 0:   s2 would be negative (a sum of non-negative numbers can not be).
	//   total - d odd:   s2 would not be a whole number.
	// e.g., [5, 2, 6, 4], d = 3: total = 17, s2 = (17 - 3) / 2 = 7. Subsets with sum 7: only {5, 2}. Answer 1.
	// T.C: O(n * s2)
	// S.C: O(s2)
	public int CountPartitions(int[] arr, int d)
	{
		int total = 0;
		foreach (int x in arr) total += x;

		if (total - d < 0 || (total - d) % 2 == 1) return 0;
		return PerfectSum(arr, (total - d) / 2);
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
		Console.WriteLine(new Solution().CountPartitions(new int[] { 5, 2, 6, 4 }, 3));   // o/p: 1
	}
}

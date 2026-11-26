// GFG: Perfect Sum Problem (Count Subsets with Sum K)   --->   Given an array of NON-NEGATIVE integers and a target,
// COUNT the subsets whose sum is exactly target.

// Example 1:
// Input: arr = [5, 2, 3, 10, 6, 8], target = 10
// Output: 3   ({5, 2, 3}, {2, 8}, {10})

// Example 2:
// Input: arr = [5, 7, 8], target = 3
// Output: 0

// Example 3:
// Input: arr = [35, 2, 8, 22], target = 0
// Output: 1   (only the empty subset)

// Example 4:
// Input: arr = [0, 0, 4], target = 4
// Output: 4   ({4}, {0a, 4}, {0b, 4}, {0a, 0b, 4}: the two 0's are different elements)

// Problem link: https://www.geeksforgeeks.org/problems/perfect-sum-problem5633/1

// First understand '19_SubsetSumProblem.cs'. There: "is it possible ?" (OR of take / not take). Here: "how many ?"
// (SUM of take / not take).

// The recursion: f(i, target) = the number of subsets of arr[0..i] with sum target.
//   f(i, target) = f(i - 1, target) + f(i - 1, target - arr[i])     (the 2nd only if arr[i] <= target)
//   Base case at i == 0:
//     arr[0] == 0 and target == 0: 2  (take the 0, or not: both give sum 0)
//     target == 0 or arr[0] == target: 1
//     otherwise: 0
// Careful with ZEROS: Subset Sum stopped as soon as target == 0. Here that would be WRONG: with target 0 left, the
// zeros still below can be taken or not, and each choice is a different subset. So we always go down to i == 0.


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n)
	public int PerfectSum_Recursion(int[] arr, int target)
	{
		return Count(arr.Length - 1, target, arr);
	}

	private int Count(int i, int target, int[] arr)
	{
		if (i == 0)
		{
			if (target == 0 && arr[0] == 0) return 2;
			if (target == 0 || arr[0] == target) return 1;
			return 0;
		}

		int notTake = Count(i - 1, target, arr);
		int take = arr[i] <= target ? Count(i - 1, target - arr[i], arr) : 0;
		return notTake + take;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * target)
	// S.C: O(n * target)
	public int PerfectSum_Memoization(int[] arr, int target)
	{
		int n = arr.Length;
		int[,] dp = new int[n, target + 1];
		for (int i = 0; i < n; i++)
		{
			for (int t = 0; t <= target; t++) dp[i, t] = -1;
		}
		return Count(n - 1, target, arr, dp);
	}

	private int Count(int i, int target, int[] arr, int[,] dp)
	{
		if (i == 0)
		{
			if (target == 0 && arr[0] == 0) return 2;
			if (target == 0 || arr[0] == target) return 1;
			return 0;
		}
		if (dp[i, target] != -1) return dp[i, target];

		int notTake = Count(i - 1, target, arr, dp);
		int take = arr[i] <= target ? Count(i - 1, target - arr[i], arr, dp) : 0;
		return dp[i, target] = notTake + take;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * target)
	// S.C: O(n * target)
	public int PerfectSum_Tabulation(int[] arr, int target)
	{
		int n = arr.Length;
		int[,] dp = new int[n, target + 1];

		dp[0, 0] = arr[0] == 0 ? 2 : 1;                     // row 0, the same rules as the base case
		if (arr[0] != 0 && arr[0] <= target) dp[0, arr[0]] = 1;

		for (int i = 1; i < n; i++)
		{
			for (int t = 0; t <= target; t++)   // t starts at 0: zeros change the count for t = 0 too
			{
				int notTake = dp[i - 1, t];
				int take = arr[i] <= t ? dp[i - 1, t - arr[i]] : 0;
				dp[i, t] = notTake + take;
			}
		}
		return dp[n - 1, target];
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * target)
	// S.C: O(target)
	public int PerfectSum_SpaceOptimized(int[] arr, int target)
	{
		int[] prev = new int[target + 1];
		prev[0] = arr[0] == 0 ? 2 : 1;
		if (arr[0] != 0 && arr[0] <= target) prev[arr[0]] = 1;

		for (int i = 1; i < arr.Length; i++)
		{
			int[] curr = new int[target + 1];
			for (int t = 0; t <= target; t++)
			{
				curr[t] = prev[t] + (arr[i] <= t ? prev[t - arr[i]] : 0);
			}
			prev = curr;
		}
		return prev[target];
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 5, 2, 3, 10, 6, 8 };

		Console.WriteLine(new Solution().PerfectSum_Recursion(arr, 10));        // o/p: 3
		Console.WriteLine(new Solution().PerfectSum_Memoization(arr, 10));      // o/p: 3
		Console.WriteLine(new Solution().PerfectSum_Tabulation(arr, 10));       // o/p: 3
		Console.WriteLine(new Solution().PerfectSum_SpaceOptimized(arr, 10));   // o/p: 3
	}
}

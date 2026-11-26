// GFG: Subset Sum Problem   --->   Given an array of positive integers arr and a value sum, is there a SUBSET of arr whose
// elements add up to exactly sum ?

// Example 1:
// Input: arr = [3, 34, 4, 12, 5, 2], sum = 9
// Output: true    (4 + 3 + 2 = 9)

// Example 2:
// Input: arr = [3, 34, 4, 12, 5, 2], sum = 30
// Output: false

// Example 3:
// Input: arr = [1, 2, 3], sum = 6
// Output: true    (the whole array)

// Problem link: https://www.geeksforgeeks.org/problems/subset-sum-problem-1611555638/1

// DP on SUBSETS / SUBSEQUENCES: the most common DP pattern. The next ~10 files are all built on this one.
// It is the TAKE / NOT TAKE pattern of '06_HouseRobber.cs', with one more thing to remember: the target left.

// The recursion: f(i, target) = can some subset of arr[0..i] add up to target ?
//   Not take arr[i]:  f(i - 1, target)
//   Take arr[i]:      f(i - 1, target - arr[i])      (only if arr[i] <= target)
//   f(i, target) = notTake OR take
//   Base cases: target == 0 -> true (the empty subset).   i == 0 -> true only if arr[0] == target.


// Dry run image (in this folder): 19_SubsetSumProblem.jpg

using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n);   recursion stack
	public bool IsSubsetSum_Recursion(int[] arr, int sum)
	{
		return CanMake(arr.Length - 1, sum, arr);
	}

	private bool CanMake(int i, int target, int[] arr)
	{
		if (target == 0) return true;
		if (i == 0) return arr[0] == target;

		bool notTake = CanMake(i - 1, target, arr);
		bool take = arr[i] <= target && CanMake(i - 1, target - arr[i], arr);
		return notTake || take;
	}

// ------------------------------------------------------------------------------

	// dp[i, target]: -1 = not solved, 0 = false, 1 = true.
	// T.C: O(n * sum)
	// S.C: O(n * sum);   dp + O(n) recursion stack
	public bool IsSubsetSum_Memoization(int[] arr, int sum)
	{
		int n = arr.Length;
		int[,] dp = new int[n, sum + 1];
		for (int i = 0; i < n; i++)
		{
			for (int t = 0; t <= sum; t++) dp[i, t] = -1;
		}
		return CanMake(n - 1, sum, arr, dp);
	}

	private bool CanMake(int i, int target, int[] arr, int[,] dp)
	{
		if (target == 0) return true;
		if (i == 0) return arr[0] == target;
		if (dp[i, target] != -1) return dp[i, target] == 1;

		bool notTake = CanMake(i - 1, target, arr, dp);
		bool take = arr[i] <= target && CanMake(i - 1, target - arr[i], arr, dp);
		dp[i, target] = (notTake || take) ? 1 : 0;
		return notTake || take;
	}

// ------------------------------------------------------------------------------

	// dp[i, t] = can arr[0..i] make the sum t ?  (See the image for the filled table.)
	// Base cases: column t = 0 is all true. Row 0: only t = arr[0] is true.
	// T.C: O(n * sum)
	// S.C: O(n * sum)
	public bool IsSubsetSum_Tabulation(int[] arr, int sum)
	{
		int n = arr.Length;
		bool[,] dp = new bool[n, sum + 1];

		for (int i = 0; i < n; i++) dp[i, 0] = true;
		if (arr[0] <= sum) dp[0, arr[0]] = true;   // if arr[0] > sum, the index would be outside the table

		for (int i = 1; i < n; i++)
		{
			for (int t = 1; t <= sum; t++)
			{
				bool notTake = dp[i - 1, t];
				bool take = arr[i] <= t && dp[i - 1, t - arr[i]];
				dp[i, t] = notTake || take;
			}
		}
		return dp[n - 1, sum];
	}

// ------------------------------------------------------------------------------

	// Each row only needs the row above it.
	// T.C: O(n * sum)
	// S.C: O(sum)
	public bool IsSubsetSum_SpaceOptimized(int[] arr, int sum)
	{
		bool[] prev = new bool[sum + 1];
		prev[0] = true;
		if (arr[0] <= sum) prev[arr[0]] = true;

		for (int i = 1; i < arr.Length; i++)
		{
			bool[] curr = new bool[sum + 1];   // a new array (in C#, 'prev = curr' shares one array, it does not copy)
			curr[0] = true;
			for (int t = 1; t <= sum; t++)
			{
				curr[t] = prev[t] || (arr[i] <= t && prev[t - arr[i]]);
			}
			prev = curr;
		}
		return prev[sum];
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 3, 34, 4, 12, 5, 2 };

		Console.WriteLine(new Solution().IsSubsetSum_Recursion(arr, 9));        // o/p: True
		Console.WriteLine(new Solution().IsSubsetSum_Memoization(arr, 9));      // o/p: True
		Console.WriteLine(new Solution().IsSubsetSum_Tabulation(arr, 9));       // o/p: True
		Console.WriteLine(new Solution().IsSubsetSum_SpaceOptimized(arr, 9));   // o/p: True
	}
}

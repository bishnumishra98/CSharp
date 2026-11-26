// Leetcode: 312. Burst Balloons   --->   n balloons in a row, nums[i] is painted on balloon i. Burst ALL of them. Bursting
// balloon i gives nums[left] * nums[i] * nums[right] coins, where left and right are its CURRENT neighbours (a missing
// neighbour counts as 1). Return the maximum coins.

// Example 1:
// Input: nums = [3,1,5,8]
// Output: 167
// Explanation: [3,1,5,8] -> burst 1 -> [3,5,8] -> burst 5 -> [3,8] -> burst 3 -> [8] -> burst 8 -> []
//              coins = 3*1*5 + 3*5*8 + 1*3*8 + 1*8*1 = 15 + 120 + 24 + 8 = 167

// Example 2:
// Input: nums = [1,5]
// Output: 10

// First understand '52_MatrixChainMultiplication.cs' (partition DP).

// The problem: If we choose the balloon to burst FIRST, its two sides are NOT independent: after it is gone, the
// balloons on the left and right become neighbours, and their coins depend on each other.
// The fix: choose the balloon to burst LAST in the range instead. While it is still there, it is a WALL: nothing on its
// left ever touches anything on its right. So the two sides are independent problems.
// When it is finally burst, every other balloon in the range is gone, so its neighbours are the balloons just OUTSIDE
// the range: nums[i - 1] and nums[j + 1].

// Setup: add a 1 at both ends: [1, 3, 1, 5, 8, 1]. The real balloons are 1..n, and the edges always have a neighbour.
// The recursion: f(i, j) = the most coins from bursting balloons i..j (with balloons i - 1 and j + 1 still standing).
//   f(i, j) = max over k (the LAST one burst) of:  f(i, k - 1) + f(k + 1, j) + nums[i - 1] * nums[k] * nums[j + 1]
//   f(i, j) = 0 if i > j


using System;

public class Solution
{
	private int[] Pad(int[] nums)
	{
		int[] a = new int[nums.Length + 2];
		a[0] = a[a.Length - 1] = 1;
		Array.Copy(nums, 0, a, 1, nums.Length);   // works on a copy (the C++ code changed the caller's array)
		return a;
	}

	// T.C: exponential
	// S.C: O(n)
	public int MaxCoins_Recursion(int[] nums)
	{
		return Coins(1, nums.Length, Pad(nums));
	}

	private int Coins(int i, int j, int[] a)
	{
		if (i > j) return 0;

		int best = 0;
		for (int k = i; k <= j; k++) best = Math.Max(best, Coins(i, k - 1, a) + Coins(k + 1, j, a) + a[i - 1] * a[k] * a[j + 1]);
		return best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n^3)
	// S.C: O(n^2)
	public int MaxCoins_Memoization(int[] nums)
	{
		int n = nums.Length;
		int[,] dp = new int[n + 1, n + 1];
		for (int i = 0; i <= n; i++)
		{
			for (int j = 0; j <= n; j++) dp[i, j] = -1;
		}
		return Coins(1, n, Pad(nums), dp);
	}

	private int Coins(int i, int j, int[] a, int[,] dp)
	{
		if (i > j) return 0;
		if (dp[i, j] != -1) return dp[i, j];

		int best = 0;
		for (int k = i; k <= j; k++)
		{
			best = Math.Max(best, Coins(i, k - 1, a, dp) + Coins(k + 1, j, a, dp) + a[i - 1] * a[k] * a[j + 1]);
		}
		return dp[i, j] = best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n^3)
	// S.C: O(n^2)
	public int MaxCoins(int[] nums)
	{
		int n = nums.Length;
		int[] a = Pad(nums);
		int[,] dp = new int[n + 2, n + 2];

		for (int i = n; i >= 1; i--)
		{
			for (int j = i; j <= n; j++)
			{
				int best = 0;
				for (int k = i; k <= j; k++) best = Math.Max(best, dp[i, k - 1] + dp[k + 1, j] + a[i - 1] * a[k] * a[j + 1]);
				dp[i, j] = best;
			}
		}
		return dp[1, n];
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 3, 1, 5, 8 };

		Console.WriteLine(new Solution().MaxCoins_Recursion(nums));     // o/p: 167
		Console.WriteLine(new Solution().MaxCoins_Memoization(nums));   // o/p: 167
		Console.WriteLine(new Solution().MaxCoins(nums));               // o/p: 167
	}
}

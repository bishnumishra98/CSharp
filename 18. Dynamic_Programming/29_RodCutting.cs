// GFG: Rod Cutting   --->   A rod has length n. price[i] is the price of a piece of length i + 1 (price has n values).
// Cut the rod into pieces (or do not cut it) and sell the pieces. Return the MAXIMUM money you can get.

// Example 1:
// Input: price = [1, 5, 8, 9, 10, 17, 17, 20]   (n = 8)
// Output: 22   (pieces of length 2 and 6: 5 + 17)

// Example 2:
// Input: price = [3, 5, 8, 9, 10, 17, 17, 20]
// Output: 24   (8 pieces of length 1: 8 * 3)

// Example 3:
// Input: price = [3]
// Output: 3

// Problem link: https://www.geeksforgeeks.org/problems/rod-cutting0840/1

// This is '28_KnapsackWithDuplicateItems.cs' in disguise:
//   capacity       = the rod length n
//   item i         = a piece of length i + 1 (its "weight") with value price[i]
//   unlimited      = we may cut many pieces of the same length
// f(i, len) = the most money from pieces of lengths 1..i + 1, for a rod of length len.
//   f(i, len) = max( f(i - 1, len),  price[i] + f(i, len - (i + 1)) )
//   f(0, len) = len * price[0]   (only pieces of length 1)


using System;

public class Solution
{
	// T.C: more than O(2^n)
	// S.C: O(n)
	public int CutRod_Recursion(int[] price)
	{
		int n = price.Length;
		return Best(n - 1, n, price);
	}

	private int Best(int i, int len, int[] price)
	{
		if (i == 0) return len * price[0];

		int notTake = Best(i - 1, len, price);
		int take = i + 1 <= len ? price[i] + Best(i, len - (i + 1), price) : 0;
		return Math.Max(take, notTake);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n^2)
	// S.C: O(n^2)
	// Note: The C++ memoized solve() returned max(take, notTake) WITHOUT storing it in dp, so nothing was ever reused
	//       and it was still exponential. The answer must be saved: dp[i, len] = ...
	public int CutRod_Memoization(int[] price)
	{
		int n = price.Length;
		int[,] dp = new int[n, n + 1];
		for (int i = 0; i < n; i++)
		{
			for (int l = 0; l <= n; l++) dp[i, l] = -1;
		}
		return Best(n - 1, n, price, dp);
	}

	private int Best(int i, int len, int[] price, int[,] dp)
	{
		if (i == 0) return len * price[0];
		if (dp[i, len] != -1) return dp[i, len];

		int notTake = Best(i - 1, len, price, dp);
		int take = i + 1 <= len ? price[i] + Best(i, len - (i + 1), price, dp) : 0;
		return dp[i, len] = Math.Max(take, notTake);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n^2)
	// S.C: O(n^2)
	public int CutRod_Tabulation(int[] price)
	{
		int n = price.Length;
		int[,] dp = new int[n, n + 1];
		for (int l = 0; l <= n; l++) dp[0, l] = l * price[0];

		for (int i = 1; i < n; i++)
		{
			for (int l = 0; l <= n; l++)
			{
				int take = i + 1 <= l ? price[i] + dp[i, l - (i + 1)] : 0;
				dp[i, l] = Math.Max(take, dp[i - 1, l]);
			}
		}
		return dp[n - 1, n];
	}

// ------------------------------------------------------------------------------

	// One row, left to right (unlimited supply).
	// T.C: O(n^2)
	// S.C: O(n)
	public int CutRod(int[] price)
	{
		int n = price.Length;
		int[] dp = new int[n + 1];
		for (int l = 0; l <= n; l++) dp[l] = l * price[0];

		for (int i = 1; i < n; i++)
		{
			int pieceLength = i + 1;
			for (int l = pieceLength; l <= n; l++) dp[l] = Math.Max(dp[l], price[i] + dp[l - pieceLength]);
		}
		return dp[n];
	}
}


class Program
{
	static void Main()
	{
		int[] price = { 1, 5, 8, 9, 10, 17, 17, 20 };

		Console.WriteLine(new Solution().CutRod_Recursion(price));     // o/p: 22
		Console.WriteLine(new Solution().CutRod_Memoization(price));   // o/p: 22
		Console.WriteLine(new Solution().CutRod_Tabulation(price));    // o/p: 22
		Console.WriteLine(new Solution().CutRod(price));               // o/p: 22
	}
}

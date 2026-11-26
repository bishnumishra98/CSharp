// Leetcode: 518. Coin Change II   --->   Given coin values coins[] and an amount, return the NUMBER OF COMBINATIONS that
// add up to the amount (0 if none). There is an unlimited number of each coin. The answer fits in an int.

// Example 1:
// Input: amount = 5, coins = [1,2,5]
// Output: 4   (5,  2 + 2 + 1,  2 + 1 + 1 + 1,  1 + 1 + 1 + 1 + 1)

// Example 2:
// Input: amount = 3, coins = [2]
// Output: 0

// Example 3:
// Input: amount = 10, coins = [10]
// Output: 1

// First understand '26_CoinChange.cs'. Same recursion, but COUNT the ways (+) instead of the fewest coins (min).
// "Combinations": 2 + 1 + 2 and 1 + 2 + 2 are the SAME combination. Going through the coins one by one (take coin i
// some times, then never again) automatically counts each combination once.

// The recursion: f(i, amount) = the number of ways with coins[0..i].
//   f(i, amount) = f(i - 1, amount) + f(i, amount - coins[i])       (the 2nd only if coins[i] <= amount)
//   f(0, amount) = 1 if amount is a multiple of coins[0], else 0

// Note: The ANSWER fits in an int, but some in-between counts (for smaller amounts) may not. So 'long' is used inside.


using System;

public class Solution
{
	// T.C: more than O(2^n)
	// S.C: O(amount)
	public int Change_Recursion(int amount, int[] coins)
	{
		return (int)Ways(coins.Length - 1, amount, coins);
	}

	private long Ways(int i, int amount, int[] coins)
	{
		if (i == 0) return amount % coins[0] == 0 ? 1 : 0;

		long notTake = Ways(i - 1, amount, coins);
		long take = coins[i] <= amount ? Ways(i, amount - coins[i], coins) : 0;
		return notTake + take;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * amount)
	// S.C: O(n * amount)
	public int Change_Memoization(int amount, int[] coins)
	{
		int n = coins.Length;
		long[,] dp = new long[n, amount + 1];
		for (int i = 0; i < n; i++)
		{
			for (int a = 0; a <= amount; a++) dp[i, a] = -1;
		}
		return (int)Ways(n - 1, amount, coins, dp);
	}

	private long Ways(int i, int amount, int[] coins, long[,] dp)
	{
		if (i == 0) return amount % coins[0] == 0 ? 1 : 0;
		if (dp[i, amount] != -1) return dp[i, amount];

		long notTake = Ways(i - 1, amount, coins, dp);
		long take = coins[i] <= amount ? Ways(i, amount - coins[i], coins, dp) : 0;
		return dp[i, amount] = notTake + take;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * amount)
	// S.C: O(n * amount)
	public int Change_Tabulation(int amount, int[] coins)
	{
		int n = coins.Length;
		long[,] dp = new long[n, amount + 1];
		for (int a = 0; a <= amount; a++) dp[0, a] = a % coins[0] == 0 ? 1 : 0;

		for (int i = 1; i < n; i++)
		{
			for (int a = 0; a <= amount; a++)
			{
				dp[i, a] = dp[i - 1, a] + (coins[i] <= a ? dp[i, a - coins[i]] : 0);
			}
		}
		return (int)dp[n - 1, amount];
	}

// ------------------------------------------------------------------------------

	// One row, left to right (unlimited supply, like '26_CoinChange.cs').
	// T.C: O(n * amount)
	// S.C: O(amount)
	public int Change(int amount, int[] coins)
	{
		long[] dp = new long[amount + 1];
		for (int a = 0; a <= amount; a++) dp[a] = a % coins[0] == 0 ? 1 : 0;

		for (int i = 1; i < coins.Length; i++)
		{
			for (int a = coins[i]; a <= amount; a++) dp[a] += dp[a - coins[i]];
		}
		return (int)dp[amount];
	}
}


class Program
{
	static void Main()
	{
		int[] coins = { 1, 2, 5 };

		Console.WriteLine(new Solution().Change_Recursion(5, coins));     // o/p: 4
		Console.WriteLine(new Solution().Change_Memoization(5, coins));   // o/p: 4
		Console.WriteLine(new Solution().Change_Tabulation(5, coins));    // o/p: 4
		Console.WriteLine(new Solution().Change(5, coins));               // o/p: 4
	}
}

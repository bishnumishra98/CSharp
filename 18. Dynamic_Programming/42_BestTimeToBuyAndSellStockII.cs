// Leetcode: 122. Best Time to Buy and Sell Stock II   --->   prices[i] is the price on day i. You may buy and sell as
// MANY times as you like, but you can hold at most ONE share at a time (sell before you buy again). Return the maximum
// profit.

// Example 1:
// Input: prices = [7,1,5,3,6,4]
// Output: 7   (buy at 1, sell at 5: +4.  buy at 3, sell at 6: +3)

// Example 2:
// Input: prices = [1,2,3,4,5]
// Output: 4   (buy at 1, sell at 5)

// Example 3:
// Input: prices = [7,6,4,3,1]
// Output: 0

// DP on STOCKS: the state is (day, canBuy). canBuy = true means we hold NO share now (so we may buy); false means we
// hold one (so we may sell). The next 3 files use the same state with one small change each.

// The recursion: f(day, canBuy) = the most profit from this day to the end.
//   canBuy:   buy today:  -prices[day] + f(day + 1, false)     or skip:  f(day + 1, true)
//   !canBuy:  sell today: +prices[day] + f(day + 1, true)      or skip:  f(day + 1, false)
//   f(n, any) = 0   (no days left)
// Answer: f(0, true).


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n)
	public int MaxProfit_Recursion(int[] prices)
	{
		return Best(0, true, prices);
	}

	private int Best(int day, bool canBuy, int[] prices)
	{
		if (day == prices.Length) return 0;

		if (canBuy) return Math.Max(-prices[day] + Best(day + 1, false, prices), Best(day + 1, true, prices));
		return Math.Max(prices[day] + Best(day + 1, true, prices), Best(day + 1, false, prices));
	}

// ------------------------------------------------------------------------------

	// dp[day, 1] = canBuy, dp[day, 0] = holding a share.
	// T.C: O(n * 2)
	// S.C: O(n * 2);   dp + O(n) recursion stack
	public int MaxProfit_Memoization(int[] prices)
	{
		int n = prices.Length;
		int[,] dp = new int[n, 2];
		for (int d = 0; d < n; d++) dp[d, 0] = dp[d, 1] = -1;
		return Best(0, 1, prices, dp);
	}

	private int Best(int day, int canBuy, int[] prices, int[,] dp)
	{
		if (day == prices.Length) return 0;
		if (dp[day, canBuy] != -1) return dp[day, canBuy];

		if (canBuy == 1) return dp[day, 1] = Math.Max(-prices[day] + Best(day + 1, 0, prices, dp), Best(day + 1, 1, prices, dp));
		return dp[day, 0] = Math.Max(prices[day] + Best(day + 1, 1, prices, dp), Best(day + 1, 0, prices, dp));
	}

// ------------------------------------------------------------------------------

	// The recursion goes FORWARD in days, so the table is filled BACKWARDS: from day n - 1 down to day 0.
	// Row n (no days left) is all 0.
	// T.C: O(n * 2)
	// S.C: O(n * 2)
	public int MaxProfit_Tabulation(int[] prices)
	{
		int n = prices.Length;
		int[,] dp = new int[n + 1, 2];

		for (int day = n - 1; day >= 0; day--)
		{
			dp[day, 1] = Math.Max(-prices[day] + dp[day + 1, 0], dp[day + 1, 1]);
			dp[day, 0] = Math.Max(prices[day] + dp[day + 1, 1], dp[day + 1, 0]);
		}
		return dp[0, 1];
	}

// ------------------------------------------------------------------------------

	// Each day only needs the NEXT day's 2 values: keep them in 2 variables.
	// T.C: O(n)
	// S.C: O(1)
	public int MaxProfit_SpaceOptimized(int[] prices)
	{
		int aheadBuy = 0, aheadHold = 0;   // f(day + 1, canBuy) and f(day + 1, holding)

		for (int day = prices.Length - 1; day >= 0; day--)
		{
			int buy = Math.Max(-prices[day] + aheadHold, aheadBuy);
			int hold = Math.Max(prices[day] + aheadBuy, aheadHold);
			aheadBuy = buy;
			aheadHold = hold;
		}
		return aheadBuy;
	}

// ------------------------------------------------------------------------------

	// Greedy (simplest for THIS version only): unlimited trades means we can take EVERY rise. Buying at a low and
	// selling at the next high is the same as adding up all the daily rises in between: e.g., 1 -> 2 -> 5 gives
	// (2 - 1) + (5 - 2) = 5 - 1. So add every positive difference between neighbouring days.
	// T.C: O(n)
	// S.C: O(1)
	public int MaxProfit(int[] prices)
	{
		int profit = 0;
		for (int i = 1; i < prices.Length; i++) profit += Math.Max(0, prices[i] - prices[i - 1]);
		return profit;
	}
}


class Program
{
	static void Main()
	{
		int[] prices = { 7, 1, 5, 3, 6, 4 };

		Console.WriteLine(new Solution().MaxProfit_Recursion(prices));        // o/p: 7
		Console.WriteLine(new Solution().MaxProfit_Memoization(prices));      // o/p: 7
		Console.WriteLine(new Solution().MaxProfit_Tabulation(prices));       // o/p: 7
		Console.WriteLine(new Solution().MaxProfit_SpaceOptimized(prices));   // o/p: 7
		Console.WriteLine(new Solution().MaxProfit(prices));                  // o/p: 7
	}
}

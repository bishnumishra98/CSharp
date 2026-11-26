// Leetcode: 309. Best Time to Buy and Sell Stock with Cooldown   --->   Same as '42_BestTimeToBuyAndSellStockII.cs'
// (unlimited transactions, one share at a time), but after you SELL, you can not buy on the NEXT day (one day of
// cooldown). Return the maximum profit.

// Example 1:
// Input: prices = [1,2,3,0,2]
// Output: 3   (buy at 1, sell at 2, cooldown, buy at 0, sell at 2)

// Example 2:
// Input: prices = [1]
// Output: 0

// The only change from Stock II: after a SELL on 'day', the next decision is on day + 2 (day + 1 is the cooldown).
//   !canBuy:  max( prices[day] + f(day + 2, true),   f(day + 1, false) )
// Because of day + 2, the base case becomes day >= n (it can jump past n).


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
		if (day >= prices.Length) return 0;

		if (canBuy) return Math.Max(-prices[day] + Best(day + 1, false, prices), Best(day + 1, true, prices));
		return Math.Max(prices[day] + Best(day + 2, true, prices), Best(day + 1, false, prices));
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * 2)
	// S.C: O(n * 2)
	public int MaxProfit_Memoization(int[] prices)
	{
		int n = prices.Length;
		int[,] dp = new int[n, 2];
		for (int d = 0; d < n; d++) dp[d, 0] = dp[d, 1] = -1;
		return Best(0, 1, prices, dp);
	}

	private int Best(int day, int canBuy, int[] prices, int[,] dp)
	{
		if (day >= prices.Length) return 0;
		if (dp[day, canBuy] != -1) return dp[day, canBuy];

		if (canBuy == 1) return dp[day, 1] = Math.Max(-prices[day] + Best(day + 1, 0, prices, dp), Best(day + 1, 1, prices, dp));
		return dp[day, 0] = Math.Max(prices[day] + Best(day + 2, 1, prices, dp), Best(day + 1, 0, prices, dp));
	}

// ------------------------------------------------------------------------------

	// n + 2 rows, so that dp[day + 2] exists for the last days.
	// T.C: O(n * 2)
	// S.C: O(n * 2)
	public int MaxProfit_Tabulation(int[] prices)
	{
		int n = prices.Length;
		int[,] dp = new int[n + 2, 2];

		for (int day = n - 1; day >= 0; day--)
		{
			dp[day, 1] = Math.Max(-prices[day] + dp[day + 1, 0], dp[day + 1, 1]);
			dp[day, 0] = Math.Max(prices[day] + dp[day + 2, 1], dp[day + 1, 0]);
		}
		return dp[0, 1];
	}

// ------------------------------------------------------------------------------

	// Needs the next day AND the day after it: keep both in variables.
	// T.C: O(n)
	// S.C: O(1)
	public int MaxProfit(int[] prices)
	{
		int aheadBuy = 0, aheadHold = 0;   // day + 1
		int ahead2Buy = 0;                 // day + 2 (only the canBuy value is ever read from it)

		for (int day = prices.Length - 1; day >= 0; day--)
		{
			int buy = Math.Max(-prices[day] + aheadHold, aheadBuy);
			int hold = Math.Max(prices[day] + ahead2Buy, aheadHold);

			ahead2Buy = aheadBuy;   // slide: day + 1 becomes day + 2
			aheadBuy = buy;
			aheadHold = hold;
		}
		return aheadBuy;
	}
}


class Program
{
	static void Main()
	{
		int[] prices = { 1, 2, 3, 0, 2 };

		Console.WriteLine(new Solution().MaxProfit_Recursion(prices));     // o/p: 3
		Console.WriteLine(new Solution().MaxProfit_Memoization(prices));   // o/p: 3
		Console.WriteLine(new Solution().MaxProfit_Tabulation(prices));    // o/p: 3
		Console.WriteLine(new Solution().MaxProfit(prices));               // o/p: 3
	}
}

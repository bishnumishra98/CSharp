// Leetcode: 121. Best Time to Buy and Sell Stock   --->   prices[i] is a stock's price on day i. Choose ONE day to buy
// and a LATER day to sell. Return the maximum profit, or 0 if no profit is possible.

// Example 1:
// Input: prices = [7,1,5,3,6,4]
// Output: 5   (buy on day 1 at 1, sell on day 4 at 6)

// Example 2:
// Input: prices = [7,6,4,3,1]
// Output: 0   (the price only falls, so do not trade)

// The first of the "stock" problems. This one needs no DP table, only "remember the best thing from the past" (the
// cheapest price so far), which is the core idea of DP. The next 4 files build real DP on top of it.


using System;

public class Solution
{
	// Brute force: try every pair (buy day i, sell day j > i).
	// T.C: O(n^2)
	// S.C: O(1)
	public int MaxProfit_BruteForce(int[] prices)
	{
		int best = 0;
		for (int i = 0; i < prices.Length; i++)
		{
			for (int j = i + 1; j < prices.Length; j++) best = Math.Max(best, prices[j] - prices[i]);
		}
		return best;
	}

// ------------------------------------------------------------------------------

	// Optimal: If we SELL on day i, the best day to have bought is the cheapest day BEFORE i. So walk the days once,
	// remembering the cheapest price so far. For each day: profit = today's price - cheapest so far. Keep the best.
	// e.g., [7,1,5,3,6,4]: cheapest becomes 1 on day 1. Day 4 (price 6): 6 - 1 = 5, the best.
	// T.C: O(n)
	// S.C: O(1)
	public int MaxProfit(int[] prices)
	{
		int cheapest = prices[0], best = 0;
		for (int i = 1; i < prices.Length; i++)
		{
			best = Math.Max(best, prices[i] - cheapest);
			cheapest = Math.Min(cheapest, prices[i]);
		}
		return best;
	}
}


class Program
{
	static void Main()
	{
		int[] prices = { 7, 1, 5, 3, 6, 4 };

		Console.WriteLine(new Solution().MaxProfit_BruteForce(prices));   // o/p: 5
		Console.WriteLine(new Solution().MaxProfit(prices));              // o/p: 5
	}
}

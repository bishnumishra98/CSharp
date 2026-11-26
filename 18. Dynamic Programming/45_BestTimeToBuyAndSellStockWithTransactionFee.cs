// Leetcode: 714. Best Time to Buy and Sell Stock with Transaction Fee   --->   Same as
// '42_BestTimeToBuyAndSellStockII.cs' (unlimited transactions, one share at a time), but every transaction costs a fee
// (paid once per buy + sell). Return the maximum profit.

// Example 1:
// Input: prices = [1,3,2,8,4,9], fee = 2
// Output: 8   ((8 - 1 - 2) + (9 - 4 - 2) = 5 + 3)

// Example 2:
// Input: prices = [1,3,7,5,10,3], fee = 3
// Output: 6

// The only change from Stock II: pay the fee when SELLING (once per transaction).
//   !canBuy:  max( prices[day] - fee + f(day + 1, true),   f(day + 1, false) )


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n)
	public int MaxProfit_Recursion(int[] prices, int fee)
	{
		return Best(0, true, prices, fee);
	}

	private int Best(int day, bool canBuy, int[] prices, int fee)
	{
		if (day == prices.Length) return 0;

		if (canBuy) return Math.Max(-prices[day] + Best(day + 1, false, prices, fee), Best(day + 1, true, prices, fee));
		return Math.Max(prices[day] - fee + Best(day + 1, true, prices, fee), Best(day + 1, false, prices, fee));
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * 2)
	// S.C: O(n * 2)
	public int MaxProfit_Memoization(int[] prices, int fee)
	{
		int n = prices.Length;
		int[,] dp = new int[n, 2];
		for (int d = 0; d < n; d++) dp[d, 0] = dp[d, 1] = -1;
		return Best(0, 1, prices, fee, dp);
	}

	private int Best(int day, int canBuy, int[] prices, int fee, int[,] dp)
	{
		if (day == prices.Length) return 0;
		if (dp[day, canBuy] != -1) return dp[day, canBuy];

		int result;
		if (canBuy == 1) result = Math.Max(-prices[day] + Best(day + 1, 0, prices, fee, dp), Best(day + 1, 1, prices, fee, dp));
		else result = Math.Max(prices[day] - fee + Best(day + 1, 1, prices, fee, dp), Best(day + 1, 0, prices, fee, dp));
		return dp[day, canBuy] = result;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * 2)
	// S.C: O(n * 2)
	public int MaxProfit_Tabulation(int[] prices, int fee)
	{
		int n = prices.Length;
		int[,] dp = new int[n + 1, 2];

		for (int day = n - 1; day >= 0; day--)
		{
			dp[day, 1] = Math.Max(-prices[day] + dp[day + 1, 0], dp[day + 1, 1]);
			dp[day, 0] = Math.Max(prices[day] - fee + dp[day + 1, 1], dp[day + 1, 0]);
		}
		return dp[0, 1];
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(1)
	public int MaxProfit(int[] prices, int fee)
	{
		int aheadBuy = 0, aheadHold = 0;

		for (int day = prices.Length - 1; day >= 0; day--)
		{
			int buy = Math.Max(-prices[day] + aheadHold, aheadBuy);
			int hold = Math.Max(prices[day] - fee + aheadBuy, aheadHold);
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
		int[] prices = { 1, 3, 2, 8, 4, 9 };

		Console.WriteLine(new Solution().MaxProfit_Recursion(prices, 2));     // o/p: 8
		Console.WriteLine(new Solution().MaxProfit_Memoization(prices, 2));   // o/p: 8
		Console.WriteLine(new Solution().MaxProfit_Tabulation(prices, 2));    // o/p: 8
		Console.WriteLine(new Solution().MaxProfit(prices, 2));               // o/p: 8
	}
}

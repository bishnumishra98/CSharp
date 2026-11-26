// Leetcode: 123. Best Time to Buy and Sell Stock III   --->   Same as '42_BestTimeToBuyAndSellStockII.cs', but at most
// TWO transactions (a transaction = one buy + one sell). Return the maximum profit.
// Leetcode: 188. Best Time to Buy and Sell Stock IV   --->   The same with at most K transactions (see the last method).

// Example 1:
// Input: prices = [3,3,5,0,0,3,1,4]
// Output: 6   (buy at 0, sell at 3: +3.  buy at 1, sell at 4: +3)

// Example 2:
// Input: prices = [1,2,3,4,5]
// Output: 4   (one transaction is enough; buying twice before selling is not allowed)

// Example 3:
// Input: prices = [7,6,4,3,1]
// Output: 0

// The state gets a 3rd part: cap = how many transactions are still allowed. A transaction ends when we SELL, so cap
// drops by 1 on a sell.
//   f(day, canBuy, cap):
//     canBuy:   max( -prices[day] + f(day + 1, false, cap),       f(day + 1, true, cap) )
//     !canBuy:  max( +prices[day] + f(day + 1, true, cap - 1),    f(day + 1, false, cap) )
//   f(n, _, _) = 0,   f(_, _, 0) = 0   (no days, or no transactions left)


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n)
	public int MaxProfit_Recursion(int[] prices)
	{
		return Best(0, true, 2, prices);
	}

	private int Best(int day, bool canBuy, int cap, int[] prices)
	{
		if (day == prices.Length || cap == 0) return 0;

		if (canBuy) return Math.Max(-prices[day] + Best(day + 1, false, cap, prices), Best(day + 1, true, cap, prices));
		return Math.Max(prices[day] + Best(day + 1, true, cap - 1, prices), Best(day + 1, false, cap, prices));
	}

// ------------------------------------------------------------------------------

	// dp[day, canBuy, cap]: a 3D array, n x 2 x 3.
	// T.C: O(n * 2 * 3)
	// S.C: O(n * 2 * 3)
	public int MaxProfit_Memoization(int[] prices)
	{
		int n = prices.Length;
		int[,,] dp = new int[n, 2, 3];
		for (int d = 0; d < n; d++)
		{
			for (int b = 0; b < 2; b++)
			{
				for (int c = 0; c < 3; c++) dp[d, b, c] = -1;
			}
		}
		return Best(0, 1, 2, prices, dp);
	}

	private int Best(int day, int canBuy, int cap, int[] prices, int[,,] dp)
	{
		if (day == prices.Length || cap == 0) return 0;
		if (dp[day, canBuy, cap] != -1) return dp[day, canBuy, cap];

		int result;
		if (canBuy == 1) result = Math.Max(-prices[day] + Best(day + 1, 0, cap, prices, dp), Best(day + 1, 1, cap, prices, dp));
		else result = Math.Max(prices[day] + Best(day + 1, 1, cap - 1, prices, dp), Best(day + 1, 0, cap, prices, dp));
		return dp[day, canBuy, cap] = result;
	}

// ------------------------------------------------------------------------------

	// Fill from the last day backwards. cap starts at 1 (cap = 0 is the base case: all 0).
	// T.C: O(n * 2 * 3)
	// S.C: O(n * 2 * 3)
	public int MaxProfit_Tabulation(int[] prices)
	{
		int n = prices.Length;
		int[,,] dp = new int[n + 1, 2, 3];

		for (int day = n - 1; day >= 0; day--)
		{
			for (int cap = 1; cap <= 2; cap++)
			{
				dp[day, 1, cap] = Math.Max(-prices[day] + dp[day + 1, 0, cap], dp[day + 1, 1, cap]);
				dp[day, 0, cap] = Math.Max(prices[day] + dp[day + 1, 1, cap - 1], dp[day + 1, 0, cap]);
			}
		}
		return dp[0, 1, 2];
	}

// ------------------------------------------------------------------------------

	// Stock IV (at most k transactions), space-optimized: each day only needs the next day's (2 x (k + 1)) table.
	// Stock III is simply k = 2.
	// T.C: O(n * k)
	// S.C: O(k)
	public int MaxProfit(int k, int[] prices)
	{
		int[,] ahead = new int[2, k + 1];   // the next day's values (all 0 after the last day)

		for (int day = prices.Length - 1; day >= 0; day--)
		{
			int[,] curr = new int[2, k + 1];
			for (int cap = 1; cap <= k; cap++)
			{
				curr[1, cap] = Math.Max(-prices[day] + ahead[0, cap], ahead[1, cap]);
				curr[0, cap] = Math.Max(prices[day] + ahead[1, cap - 1], ahead[0, cap]);
			}
			ahead = curr;
		}
		return ahead[1, k];
	}
}


class Program
{
	static void Main()
	{
		int[] prices = { 3, 3, 5, 0, 0, 3, 1, 4 };

		Console.WriteLine(new Solution().MaxProfit_Recursion(prices));     // o/p: 6
		Console.WriteLine(new Solution().MaxProfit_Memoization(prices));   // o/p: 6
		Console.WriteLine(new Solution().MaxProfit_Tabulation(prices));    // o/p: 6
		Console.WriteLine(new Solution().MaxProfit(2, prices));            // o/p: 6
	}
}

// Leetcode: 322. Coin Change   --->   Given coin values coins[] and an amount, return the FEWEST coins that add up to the
// amount, or -1 if it is impossible. There is an UNLIMITED number of each coin.

// Example 1:
// Input: coins = [1,2,5], amount = 11
// Output: 3    (5 + 5 + 1)

// Example 2:
// Input: coins = [2], amount = 3
// Output: -1

// Example 3:
// Input: coins = [1], amount = 0
// Output: 0

// Why not greedy (always the biggest coin that fits) ? coins = [1, 3, 4], amount = 6: greedy takes 4 + 1 + 1 = 3 coins,
// but 3 + 3 = 2 coins.

// UNLIMITED supply: the new idea of this group of problems (also Coin Change II, Unbounded Knapsack, Rod Cutting).
// The recursion: f(i, amount) = the fewest coins from coins[0..i] that make amount.
//   Not take coin i:  f(i - 1, amount)                 (move on, never use coin i again)
//   Take coin i:      1 + f(i, amount - coins[i])       <- stay at i, NOT i - 1: the same coin may be taken AGAIN
//   f(i, amount) = min(take, notTake)
//   Base case, only coin 0 left: if amount is a multiple of coins[0], amount / coins[0] coins; else impossible (a huge
//   number, so min never picks it).
// (In 0-1 Knapsack, 'take' moved to i - 1 because each item could be used only once. That one index is the whole
//  difference between "limited" and "unlimited".)
// Note: The C++ comment said the correct formula is "1 + solve(i - 1, amount - coins[i])", the same as the "mistake"
//       right above it. The code itself correctly used solve(i, ...).


// Dry run image (in this folder): 26_CoinChange.jpg

using System;

public class Solution
{
	private const int INF = 1_000_000_000;   // "impossible". Not int.MaxValue: 1 + int.MaxValue overflows.

	// T.C: more than O(2^n): 'take' stays on the same index, so a path can be up to amount steps long
	// S.C: O(amount);   recursion stack (each 'take' lowers the amount)
	public int CoinChange_Recursion(int[] coins, int amount)
	{
		int ans = Fewest(coins.Length - 1, amount, coins);
		return ans >= INF ? -1 : ans;
	}

	private int Fewest(int i, int amount, int[] coins)
	{
		if (i == 0) return amount % coins[0] == 0 ? amount / coins[0] : INF;

		int notTake = Fewest(i - 1, amount, coins);
		int take = coins[i] <= amount ? 1 + Fewest(i, amount - coins[i], coins) : INF;
		return Math.Min(take, notTake);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * amount)
	// S.C: O(n * amount);   dp + O(amount) recursion stack
	public int CoinChange_Memoization(int[] coins, int amount)
	{
		int n = coins.Length;
		int[,] dp = new int[n, amount + 1];
		for (int i = 0; i < n; i++)
		{
			for (int a = 0; a <= amount; a++) dp[i, a] = -1;
		}

		int ans = Fewest(n - 1, amount, coins, dp);
		return ans >= INF ? -1 : ans;
	}

	private int Fewest(int i, int amount, int[] coins, int[,] dp)
	{
		if (i == 0) return amount % coins[0] == 0 ? amount / coins[0] : INF;
		if (dp[i, amount] != -1) return dp[i, amount];

		int notTake = Fewest(i - 1, amount, coins, dp);
		int take = coins[i] <= amount ? 1 + Fewest(i, amount - coins[i], coins, dp) : INF;
		return dp[i, amount] = Math.Min(take, notTake);
	}

// ------------------------------------------------------------------------------

	// 'take' reads dp[i, a - coin], from the SAME row (that is the "stay at i").
	// T.C: O(n * amount)
	// S.C: O(n * amount)
	public int CoinChange_Tabulation(int[] coins, int amount)
	{
		int n = coins.Length;
		int[,] dp = new int[n, amount + 1];
		for (int a = 0; a <= amount; a++) dp[0, a] = a % coins[0] == 0 ? a / coins[0] : INF;

		for (int i = 1; i < n; i++)
		{
			for (int a = 0; a <= amount; a++)
			{
				int notTake = dp[i - 1, a];
				int take = coins[i] <= a ? 1 + dp[i, a - coins[i]] : INF;
				dp[i, a] = Math.Min(take, notTake);
			}
		}
		return dp[n - 1, amount] >= INF ? -1 : dp[n - 1, amount];
	}

// ------------------------------------------------------------------------------

	// ONE row: dp[a] starts as the previous row's value (notTake). Going LEFT to RIGHT, dp[a - coin] is already the
	// CURRENT row's value: exactly the "take, and stay at i" we want. (0-1 Knapsack had to go right to left to avoid
	// that, see '24_01Knapsack.cs'.)
	// T.C: O(n * amount)
	// S.C: O(amount)
	public int CoinChange(int[] coins, int amount)
	{
		int[] dp = new int[amount + 1];
		for (int a = 0; a <= amount; a++) dp[a] = a % coins[0] == 0 ? a / coins[0] : INF;

		for (int i = 1; i < coins.Length; i++)
		{
			for (int a = coins[i]; a <= amount; a++)   // left to right
			{
				dp[a] = Math.Min(dp[a], 1 + dp[a - coins[i]]);
			}
		}
		return dp[amount] >= INF ? -1 : dp[amount];
	}
}


class Program
{
	static void Main()
	{
		int[] coins = { 1, 2, 5 };

		Console.WriteLine(new Solution().CoinChange_Recursion(coins, 11));     // o/p: 3
		Console.WriteLine(new Solution().CoinChange_Memoization(coins, 11));   // o/p: 3
		Console.WriteLine(new Solution().CoinChange_Tabulation(coins, 11));    // o/p: 3
		Console.WriteLine(new Solution().CoinChange(coins, 11));               // o/p: 3
	}
}

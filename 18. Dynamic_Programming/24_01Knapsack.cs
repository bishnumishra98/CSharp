// GFG: 0-1 Knapsack Problem   --->   There are n items; item i has a weight wt[i] and a value val[i]. A bag (knapsack)
// can carry a total weight of at most W. Choose items so that their total weight <= W and their total value is the
// MAXIMUM. Each item can be taken at most once, and only whole (0 = leave it, 1 = take it, hence "0-1").

// Example 1:
// Input: W = 4, val = [1, 2, 3], wt = [4, 5, 1]
// Output: 3    (only item 2 (weight 1, value 3); item 0 + item 2 weigh 5 > 4)

// Example 2:
// Input: W = 3, val = [1, 2, 3], wt = [4, 5, 6]
// Output: 0    (everything is too heavy)

// Example 3:
// Input: W = 5, val = [10, 40, 30, 50], wt = [5, 4, 2, 3]
// Output: 80   (items 2 and 3: weight 5, value 30 + 50)

// Problem link: https://www.geeksforgeeks.org/problems/0-1-knapsack-problem0945/1

// Why not greedy (take the best value / weight first) ? It fails because items can not be cut. e.g., W = 10,
// items (weight 6, value 60) and two of (weight 5, value 45): best ratio is 10 per kg (the 6 kg item), but then only
// 4 kg are left and nothing fits: 60. Taking both 5 kg items gives 90.

// The recursion: f(i, W) = the best value using items 0..i with capacity W. Take / not take again:
//   Not take item i:  f(i - 1, W)
//   Take item i:      val[i] + f(i - 1, W - wt[i])     (only if wt[i] <= W)
//   f(i, W) = max(take, notTake)
//   f(0, W) = val[0] if wt[0] <= W, else 0


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n)
	public int Knapsack_Recursion(int W, int[] val, int[] wt)
	{
		return Best(val.Length - 1, W, val, wt);
	}

	private int Best(int i, int W, int[] val, int[] wt)
	{
		if (i == 0) return wt[0] <= W ? val[0] : 0;

		int notTake = Best(i - 1, W, val, wt);
		int take = wt[i] <= W ? val[i] + Best(i - 1, W - wt[i], val, wt) : 0;
		return Math.Max(take, notTake);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * W)
	// S.C: O(n * W)
	public int Knapsack_Memoization(int W, int[] val, int[] wt)
	{
		int n = val.Length;
		int[,] dp = new int[n, W + 1];
		for (int i = 0; i < n; i++)
		{
			for (int w = 0; w <= W; w++) dp[i, w] = -1;
		}
		return Best(n - 1, W, val, wt, dp);
	}

	private int Best(int i, int W, int[] val, int[] wt, int[,] dp)
	{
		if (i == 0) return wt[0] <= W ? val[0] : 0;
		if (dp[i, W] != -1) return dp[i, W];

		int notTake = Best(i - 1, W, val, wt, dp);
		int take = wt[i] <= W ? val[i] + Best(i - 1, W - wt[i], val, wt, dp) : 0;
		return dp[i, W] = Math.Max(take, notTake);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * W)
	// S.C: O(n * W)
	public int Knapsack_Tabulation(int W, int[] val, int[] wt)
	{
		int n = val.Length;
		int[,] dp = new int[n, W + 1];
		for (int w = wt[0]; w <= W; w++) dp[0, w] = val[0];   // item 0 fits in every capacity >= wt[0]

		for (int i = 1; i < n; i++)
		{
			for (int w = 0; w <= W; w++)
			{
				int notTake = dp[i - 1, w];
				int take = wt[i] <= w ? val[i] + dp[i - 1, w - wt[i]] : 0;
				dp[i, w] = Math.Max(take, notTake);
			}
		}
		return dp[n - 1, W];
	}

// ------------------------------------------------------------------------------

	// Two rows: prev and curr.
	// T.C: O(n * W)
	// S.C: O(2 * W) = O(W)
	public int Knapsack_SpaceOptimized(int W, int[] val, int[] wt)
	{
		int[] prev = new int[W + 1];
		for (int w = wt[0]; w <= W; w++) prev[w] = val[0];

		for (int i = 1; i < val.Length; i++)
		{
			int[] curr = new int[W + 1];
			for (int w = 0; w <= W; w++)
			{
				int take = wt[i] <= w ? val[i] + prev[w - wt[i]] : 0;
				curr[w] = Math.Max(take, prev[w]);
			}
			prev = curr;
		}
		return prev[W];
	}

// ------------------------------------------------------------------------------

	// ONE row only: curr[w] needs prev[w] and prev[w - wt[i]], both at index w or to its LEFT. If we fill the row from
	// RIGHT to LEFT, inside the same array, every cell we read on the left still holds the OLD (previous row) value,
	// because it has not been overwritten yet.
	// (Left to right would be wrong: dp[w - wt[i]] would already hold the new value, i.e., item i would be counted
	//  twice. That "mistake" is exactly the unbounded knapsack, see '28_KnapsackWithDuplicateItems.cs'.)
	// T.C: O(n * W)
	// S.C: O(W)
	public int Knapsack_OneRow(int W, int[] val, int[] wt)
	{
		int[] dp = new int[W + 1];
		for (int w = wt[0]; w <= W; w++) dp[w] = val[0];

		for (int i = 1; i < val.Length; i++)
		{
			for (int w = W; w >= wt[i]; w--)   // right to left (smaller w can not take item i anyway)
			{
				dp[w] = Math.Max(dp[w], val[i] + dp[w - wt[i]]);
			}
		}
		return dp[W];
	}
}


class Program
{
	static void Main()
	{
		int[] val = { 10, 40, 30, 50 }, wt = { 5, 4, 2, 3 };

		Console.WriteLine(new Solution().Knapsack_Recursion(5, val, wt));        // o/p: 80
		Console.WriteLine(new Solution().Knapsack_Memoization(5, val, wt));      // o/p: 80
		Console.WriteLine(new Solution().Knapsack_Tabulation(5, val, wt));       // o/p: 80
		Console.WriteLine(new Solution().Knapsack_SpaceOptimized(5, val, wt));   // o/p: 80
		Console.WriteLine(new Solution().Knapsack_OneRow(5, val, wt));           // o/p: 80
	}
}

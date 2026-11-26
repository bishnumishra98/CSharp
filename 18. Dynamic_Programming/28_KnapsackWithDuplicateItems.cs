// GFG: Knapsack with Duplicate Items (Unbounded Knapsack)   --->   Same as '24_01Knapsack.cs', but every item can be
// taken ANY number of times. Return the maximum total value with total weight <= capacity.

// Example 1:
// Input: val = [1, 1], wt = [2, 1], capacity = 3
// Output: 3    (item 1 three times)

// Example 2:
// Input: val = [6, 1, 7, 7], wt = [1, 3, 4, 5], capacity = 8
// Output: 48   (item 0 eight times: 8 * 6)

// Example 3:
// Input: val = [6, 8, 7, 100], wt = [2, 3, 4, 5], capacity = 1
// Output: 0

// Problem link: https://www.geeksforgeeks.org/problems/knapsack-with-duplicate-items4201/1

// 0-1 Knapsack + the "unlimited supply" change from '26_CoinChange.cs': after taking item i, STAY at i.
//   f(i, cap) = max( f(i - 1, cap),  val[i] + f(i, cap - wt[i]) )
//   f(0, cap) = (cap / wt[0]) * val[0]   (take item 0 as many times as it fits)


using System;

public class Solution
{
	// T.C: more than O(2^n)
	// S.C: O(capacity)
	public int KnapSack_Recursion(int[] val, int[] wt, int capacity)
	{
		return Best(val.Length - 1, capacity, val, wt);
	}

	private int Best(int i, int cap, int[] val, int[] wt)
	{
		if (i == 0) return (cap / wt[0]) * val[0];

		int notTake = Best(i - 1, cap, val, wt);
		int take = wt[i] <= cap ? val[i] + Best(i, cap - wt[i], val, wt) : 0;
		return Math.Max(take, notTake);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * capacity)
	// S.C: O(n * capacity);   dp + O(capacity) recursion stack (the C++ comment said O(n) for the stack)
	public int KnapSack_Memoization(int[] val, int[] wt, int capacity)
	{
		int n = val.Length;
		int[,] dp = new int[n, capacity + 1];
		for (int i = 0; i < n; i++)
		{
			for (int c = 0; c <= capacity; c++) dp[i, c] = -1;
		}
		return Best(n - 1, capacity, val, wt, dp);
	}

	private int Best(int i, int cap, int[] val, int[] wt, int[,] dp)
	{
		if (i == 0) return (cap / wt[0]) * val[0];
		if (dp[i, cap] != -1) return dp[i, cap];

		int notTake = Best(i - 1, cap, val, wt, dp);
		int take = wt[i] <= cap ? val[i] + Best(i, cap - wt[i], val, wt, dp) : 0;
		return dp[i, cap] = Math.Max(take, notTake);
	}

// ------------------------------------------------------------------------------

	// T.C: O(n * capacity)
	// S.C: O(n * capacity)
	public int KnapSack_Tabulation(int[] val, int[] wt, int capacity)
	{
		int n = val.Length;
		int[,] dp = new int[n, capacity + 1];
		for (int c = 0; c <= capacity; c++) dp[0, c] = (c / wt[0]) * val[0];

		for (int i = 1; i < n; i++)
		{
			for (int c = 0; c <= capacity; c++)
			{
				int take = wt[i] <= c ? val[i] + dp[i, c - wt[i]] : 0;   // same row: item i may be taken again
				dp[i, c] = Math.Max(take, dp[i - 1, c]);
			}
		}
		return dp[n - 1, capacity];
	}

// ------------------------------------------------------------------------------

	// One row, LEFT to RIGHT (the opposite of 0-1 Knapsack's right to left, see '24_01Knapsack.cs').
	// T.C: O(n * capacity)
	// S.C: O(capacity)
	public int KnapSack(int[] val, int[] wt, int capacity)
	{
		int[] dp = new int[capacity + 1];
		for (int c = 0; c <= capacity; c++) dp[c] = (c / wt[0]) * val[0];

		for (int i = 1; i < val.Length; i++)
		{
			for (int c = wt[i]; c <= capacity; c++) dp[c] = Math.Max(dp[c], val[i] + dp[c - wt[i]]);
		}
		return dp[capacity];
	}
}


class Program
{
	static void Main()
	{
		int[] val = { 6, 1, 7, 7 }, wt = { 1, 3, 4, 5 };

		Console.WriteLine(new Solution().KnapSack_Recursion(val, wt, 8));     // o/p: 48
		Console.WriteLine(new Solution().KnapSack_Memoization(val, wt, 8));   // o/p: 48
		Console.WriteLine(new Solution().KnapSack_Tabulation(val, wt, 8));    // o/p: 48
		Console.WriteLine(new Solution().KnapSack(val, wt, 8));               // o/p: 48
	}
}

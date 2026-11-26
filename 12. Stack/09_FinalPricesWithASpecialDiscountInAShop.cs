// Leetcode: 1475. Final Prices With a Special Discount in a Shop   --->   You are given an integer array prices where
// prices[i] is the price of the ith item in a shop.
// There is a special discount for items in the shop. If you buy the ith item, then you will receive a discount equivalent
// to prices[j] where j is the minimum index such that j > i and prices[j] <= prices[i]. Otherwise, you will not receive
// any discount at all.
// Return an integer array answer where answer[i] is the final price you will pay for the ith item of the shop,
// considering the special discount.

// Example 1:
// Input: prices = [8,4,6,2,3]
// Output: [4,2,4,2,3]
// Explanation: Item 0 (8) gets discount prices[1] = 4 -> 8 - 4 = 4. Item 1 (4) gets prices[3] = 2 -> 2.
// Item 2 (6) gets prices[3] = 2 -> 4. Items 3 and 4 get no discount.

// Example 2:
// Input: prices = [1,2,3,4,5]
// Output: [1,2,3,4,5]

// Example 3:
// Input: prices = [10,1,1,6]
// Output: [9,0,1,6]

// The discount is the "next smaller OR EQUAL element" ('07_NextAndPreviousSmallerOrGreaterElement.cs'). Since equal also
// counts, pop only while top > x (strictly bigger), not >=. In example 3, the first 1 gets the second 1 as discount.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: For every item, walk right to the first price <= it.
	// T.C: O(n^2)
	// S.C: O(1);   ignoring the answer
	public int[] FinalPrices_BruteForce(int[] prices)
	{
		int n = prices.Length;
		int[] ans = (int[])prices.Clone();

		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				if (prices[j] <= prices[i])
				{
					ans[i] = prices[i] - prices[j];
					break;
				}
			}
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Monotonic stack, right to left, pop while top > price. The top is the discount (or none).
	// T.C: O(n)
	// S.C: O(n);   for the stack
	public int[] FinalPrices(int[] prices)
	{
		int n = prices.Length;
		int[] ans = new int[n];
		Stack<int> st = new Stack<int>();

		for (int i = n - 1; i >= 0; i--)
		{
			while (st.Count > 0 && st.Peek() > prices[i]) st.Pop();
			ans[i] = st.Count > 0 ? prices[i] - st.Peek() : prices[i];
			st.Push(prices[i]);
		}
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] prices = { 10, 1, 1, 6 };

		Console.WriteLine(string.Join(" ", new Solution().FinalPrices_BruteForce(prices)));   // o/p: 9 0 1 6
		Console.WriteLine(string.Join(" ", new Solution().FinalPrices(prices)));              // o/p: 9 0 1 6
	}
}

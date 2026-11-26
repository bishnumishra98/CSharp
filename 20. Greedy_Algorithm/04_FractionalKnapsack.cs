// GFG: Fractional Knapsack   --->   Items have values val[i] and weights wt[i]. A bag holds at most 'capacity' weight. You
// MAY take a FRACTION of an item (e.g., half of it, for half its value). Return the maximum total value.

// Example 1:
// Input: val = [60, 100, 120], wt = [10, 20, 30], capacity = 50
// Output: 240.000000   (all of items 1 and 2 (weight 30, value 160), then 20 of item 3's 30 kg: 120 * 20/30 = 80)

// Example 2:
// Input: val = [60, 100], wt = [10, 20], capacity = 50
// Output: 160.000000   (everything fits)

// Problem link: https://www.geeksforgeeks.org/problems/fractional-knapsack-1587115620/1

// Compare with '18. Dynamic_Programming/24_01Knapsack.cs': there items can NOT be cut, and greedy fails. Here they can,
// and greedy is exactly right.


using System;

public class Solution
{
	// Algorithm: Value per kg (density) = val / wt. Fill the bag with the DENSEST items first: take whole items while they
	// fit, then a fraction of the next one to fill the rest, and stop.
	// Why does greedy work here ? Every kilogram of capacity should hold the most valuable kilogram available. Because
	// items can be cut, there is never a "leftover gap" that a denser item could not fill.
	// T.C: O(n log n);   sort by density
	// S.C: O(n)
	public double FractionalKnapsack(int[] val, int[] wt, int capacity)
	{
		int n = val.Length;
		int[] order = new int[n];
		for (int i = 0; i < n; i++) order[i] = i;
		Array.Sort(order, (a, b) => ((double)val[b] / wt[b]).CompareTo((double)val[a] / wt[a]));   // densest first

		double total = 0;
		foreach (int i in order)
		{
			if (capacity == 0) break;

			if (wt[i] <= capacity)   // the whole item fits
			{
				total += val[i];
				capacity -= wt[i];
			}
			else                     // take only what fits
			{
				total += (double)val[i] * capacity / wt[i];
				capacity = 0;
			}
		}
		return total;
	}
}


class Program
{
	static void Main()
	{
		double best = new Solution().FractionalKnapsack(new int[] { 60, 100, 120 }, new int[] { 10, 20, 30 }, 50);
		Console.WriteLine(best.ToString("F6"));   // o/p: 240.000000
	}
}

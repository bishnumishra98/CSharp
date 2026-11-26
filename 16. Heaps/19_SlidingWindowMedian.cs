// Leetcode: 480. Sliding Window Median   --->   You are given an integer array nums and an integer k. A window of size k
// slides from the very left of the array to the very right, one step at a time. Return the median of every window.
// (Median: the middle value of the sorted window, or the average of the 2 middle values if k is even.)

// Example 1:
// Input: nums = [1,3,-1,-3,5,3,6,7], k = 3
// Output: [1,-1,-1,3,5,6]
// Explanation:
//   Window              Sorted       Median
//   [1  3  -1]          -1  1  3       1
//   [3  -1  -3]         -3 -1  3      -1
//   [-1  -3  5]         -3 -1  5      -1
//   [-3  5  3]          -3  3  5       3
//   [5  3  6]            3  5  6       5
//   [3  6  7]            3  6  7       6

// Example 2:
// Input: nums = [1,2,3,4,2,3,1,4,2], k = 3
// Output: [2,3,3,3,2,3,2]

// Note: The C++ file for this problem was empty (only the title). This solution is new.

// First understand '16_FindMedianFromDataStream.cs'. The new problem: numbers also LEAVE the window, and a heap can
// not remove a value from the middle quickly.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: For every window, copy it, sort it, and read the middle.
	// T.C: O((n - k + 1) * k log k)
	// S.C: O(k)
	public double[] MedianSlidingWindow_BruteForce(int[] nums, int k)
	{
		double[] ans = new double[nums.Length - k + 1];
		int[] window = new int[k];

		for (int i = 0; i + k <= nums.Length; i++)
		{
			Array.Copy(nums, i, window, 0, k);
			Array.Sort(window);
			ans[i] = k % 2 == 1 ? window[k / 2] : ((long)window[k / 2 - 1] + window[k / 2]) / 2.0;
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: The two-halves idea from Find Median, but with 2 SortedSets instead of 2 heaps.
	// A SortedSet (a self-balancing BST inside) can do what a heap can not: REMOVE any value in O(log k).
	// It also gives its Min and Max in O(log k).
	// ● low  = the smaller half (its Max is the "top"),  high = the bigger half (its Min is the "top").
	// ● A set can not hold the same value twice, so store (value, index) pairs. The index makes every item unique,
	//   e.g., the two 3's in Example 1 are (3, 1) and (3, 5).
	// ● Keep low.Count == high.Count or low.Count == high.Count + 1. Then the median is low.Max (k odd), or the average
	//   of low.Max and high.Min (k even).
	// Each step: add the new number to the correct half, remove the number that left the window (from whichever half
	// has it), and rebalance by moving one top across.
	// T.C: O(n log k)
	// S.C: O(k)
	// (The other common way uses 2 heaps with "lazy deletion": remember which values should be removed, and throw them
	//  away only when they reach a top. It has the same T.C but is harder to get right.)
	public double[] MedianSlidingWindow(int[] nums, int k)
	{
		SortedSet<(int val, int idx)> low = new SortedSet<(int val, int idx)>();
		SortedSet<(int val, int idx)> high = new SortedSet<(int val, int idx)>();
		double[] ans = new double[nums.Length - k + 1];

		for (int i = 0; i < nums.Length; i++)
		{
			// 1. Add nums[i] to the correct half.
			var item = (nums[i], i);
			if (low.Count == 0 || item.CompareTo(low.Max) <= 0) low.Add(item);
			else high.Add(item);

			// 2. Remove the number that just left the window.
			if (i >= k)
			{
				var old = (nums[i - k], i - k);
				if (!low.Remove(old)) high.Remove(old);   // Remove returns false if it was not in that set
			}

			// 3. Rebalance the sizes.
			while (low.Count > high.Count + 1) Move(low, high, low.Max);
			while (high.Count > low.Count) Move(high, low, high.Min);

			// 4. Read the median once the first window is full.
			if (i >= k - 1)
			{
				ans[i - k + 1] = k % 2 == 1 ? low.Max.val : ((long)low.Max.val + high.Min.val) / 2.0;
			}
		}
		return ans;
	}

	private void Move(SortedSet<(int val, int idx)> from, SortedSet<(int val, int idx)> to, (int val, int idx) item)
	{
		from.Remove(item);
		to.Add(item);
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 3, -1, -3, 5, 3, 6, 7 };

		Console.WriteLine(string.Join(" ", new Solution().MedianSlidingWindow_BruteForce(nums, 3)));   // o/p: 1 -1 -1 3 5 6
		Console.WriteLine(string.Join(" ", new Solution().MedianSlidingWindow(nums, 3)));              // o/p: 1 -1 -1 3 5 6
	}
}

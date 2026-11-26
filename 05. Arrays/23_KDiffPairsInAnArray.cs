// Leetcode: 532. K-diff Pairs in an Array   --->   Given an array of integers nums and an integer k, return the number of
// unique k-diff pairs in the array.
// A k-diff pair is an integer pair (nums[i], nums[j]), where the following are true:
// • 0 <= i, j < nums.length
// • i != j
// • |nums[i] - nums[j]| == k
// Notice that |val| denotes the absolute value of val.

// Example 1:
// Input: nums = [3,1,4,1,5], k = 2
// Output: 2
// Explanation: There are two 2-diff pairs in the array, (1, 3) and (3, 5).
// Although we have two 1s in the input, we should only return the number of unique pairs.

// Example 2:
// Input: nums = [1,2,3,4,5], k = 1
// Output: 4
// Explanation: There are four 1-diff pairs in the array, (1, 2), (2, 3), (3, 4) and (4, 5).

// Example 3:
// Input: nums = [1,3,1,5,4], k = 0
// Output: 1
// Explanation: There is one 0-diff pair in the array, (1, 1).


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force algorithm: Check every pair (i, j) using 2 loops. If |nums[i] - nums[j]| == k, store the pair.
	// To count only unique pairs, store them in a HashSet with the smaller number first, so (3, 1) and (1, 3) become the same.
	// NOTE (C#): A tuple (int, int) is used because it compares by values. HashSet<int[]> would not remove duplicates.
	// T.C: O(n^2)
	// S.C: O(m);   where m is the no.of unique pairs. In the worst case m = n - 1.
	public int FindPairs_BruteForce(int[] nums, int k)
	{
		HashSet<(int, int)> st = new HashSet<(int, int)>();

		for (int i = 0; i < nums.Length - 1; i++)
		{
			for (int j = i + 1; j < nums.Length; j++)
			{
				if (Math.Abs(nums[i] - nums[j]) == k)
				{
					st.Add((Math.Min(nums[i], nums[j]), Math.Max(nums[i], nums[j])));
				}
			}
		}

		return st.Count;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Two pointer approach
	// 1. Sort the array and initialize two pointers i and j on the 0th and 1st index respectively.
	// 2. Find diff = nums[j] - nums[i] until j reaches the end of the array. There can be 3 cases:
	//    Case 1: If diff > k: move i ahead to reduce the difference.
	//    Case 2: If diff < k: move j ahead to increase the difference.
	//    Case 3: If diff == k: store the pair (nums[i], nums[j]) in a set to count only unique pairs,
	//            and move both i and j ahead to find a new pair.
	// 3. Return the size of the set.
	//    Edge case: The problem says i != j (an element can't make a pair with itself). When k = 0, i might move ahead and
	//               land on the same index as j. In that case, move j ahead before the next iteration, so that i != j.
	// T.C: O(nlogn) + O(n) = O(nlogn)
	// S.C: O(m);   where m is the no.of unique pairs. In the worst case m = n - 1.
	public int FindPairs_TwoPointer(int[] nums, int k)
	{
		Array.Sort(nums);
		HashSet<(int, int)> st = new HashSet<(int, int)>();

		int i = 0, j = 1;
		while (j < nums.Length)
		{
			int diff = nums[j] - nums[i];
			if (diff > k) i++;
			else if (diff < k) j++;
			else   // if (diff == k)
			{
				st.Add((nums[i], nums[j]));   // array is sorted, so nums[i] <= nums[j] already
				i++;
				j++;
			}
			if (i == j) j++;   // VERY IMPORTANT EDGE CASE
		}

		return st.Count;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Frequency Dictionary (no sorting needed)
	// 1. Count how many times each number appears: freq = {number -> count}. Using the dictionary keys also removes
	//    duplicate numbers, so every pair is counted only once.
	// 2. For every unique number 'num' in the dictionary:
	//    -> If k == 0: the pair is (num, num). It is possible only if num appears 2 or more times.
	//    -> If k > 0 : the pair is (num, num + k). Check if 'num + k' is present in the dictionary.
	//       We only look "upwards" (num + k, not num - k). So the pair (1, 3) is counted only once, from 1.
	// e.g., nums = [3, 1, 4, 1, 5], k = 2 -> freq = {3: 1, 1: 2, 4: 1, 5: 1}
	//       3 -> 5 present (count 1), 1 -> 3 present (count 2), 4 -> 6 absent, 5 -> 7 absent   -> answer = 2
	// T.C: O(n)
	// S.C: O(n)
	public int FindPairs(int[] nums, int k)
	{
		Dictionary<int, int> freq = new Dictionary<int, int>();
		foreach (int num in nums)
		{
			freq[num] = freq.GetValueOrDefault(num) + 1;
		}

		int count = 0;
		// Each item of a Dictionary is a KeyValuePair: pair.Key is the number, pair.Value is its count.
		foreach (KeyValuePair<int, int> pair in freq)
		{
			if (k == 0)
			{
				if (pair.Value >= 2) count++;
			}
			else if (freq.ContainsKey(pair.Key + k))
			{
				count++;
			}
		}

		return count;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().FindPairs_BruteForce(new int[] { 3, 1, 4, 1, 5 }, 2));   // o/p: 2
		Console.WriteLine(new Solution().FindPairs_TwoPointer(new int[] { 3, 1, 4, 1, 5 }, 2));   // o/p: 2
		Console.WriteLine(new Solution().FindPairs(new int[] { 3, 1, 4, 1, 5 }, 2));              // o/p: 2
	}
}

// Leetcode: 169. Majority Element   --->   Given an array nums of size n, return the majority element.
// The majority element is the element that appears more than ⌊n / 2⌋ times. You may assume that the majority element
// always exists in the array.

// Example 1:
// Input: nums = [3,2,3]
// Output: 3

// Example 2:
// Input: nums = [2,2,1,1,1,2,2]
// Output: 2

// Follow-up: Could you solve the problem in linear time and in O(1) space?


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: For every element, count how many times it appears. Return the one that appears more than n / 2 times.
	// T.C: O(n^2)
	// S.C: O(1)
	public int MajorityElement_BruteForce(int[] nums)
	{
		int n = nums.Length;

		for (int i = 0; i < n; i++)
		{
			int count = 0;
			for (int j = 0; j < n; j++)
			{
				if (nums[j] == nums[i]) count++;
			}
			if (count > n / 2) return nums[i];
		}

		return -1;   // never reached, as the majority element always exists
	}

// ------------------------------------------------------------------------------

	// Sorting: After sorting, the majority element is always at the middle index n / 2.
	// Why ? It fills more than half of the array, so wherever its block starts, it must cover the middle.
	// e.g., [2, 2, 1, 1, 1, 2, 2] -> sorted [1, 1, 1, 2, 2, 2, 2], middle (index 3) = 2
	// Note: Array.Sort() changes the input array.
	// T.C: O(nlogn);   for sorting
	// S.C: O(1);   ignoring the space used inside Array.Sort()
	public int MajorityElement_Sorting(int[] nums)
	{
		Array.Sort(nums);
		return nums[nums.Length / 2];
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Hashing. Count every element in a Dictionary. Return the one whose count goes above n / 2.
	// T.C: O(n)
	// S.C: O(n);   for the Dictionary
	public int MajorityElement_Hashing(int[] nums)
	{
		Dictionary<int, int> freq = new Dictionary<int, int>();

		foreach (int num in nums)
		{
			freq[num] = freq.GetValueOrDefault(num) + 1;
			if (freq[num] > nums.Length / 2) return num;
		}

		return -1;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Moore's Voting Algorithm.
	// Idea: Think of it as an election. Every different pair of elements "cancel" each other's vote. The majority element
	//       has more votes than all others combined, so it is the one left at the end.
	// 1. Keep a 'candidate' and its 'count' (starts at 0).
	// 2. For every element:
	//    -> If count == 0, make this element the new candidate.
	//    -> If the element == candidate, count++ (a vote for it). Else count-- (a vote cancelled).
	// 3. The candidate left at the end is the majority element. (If it might not exist, count it once more to verify.)
	// e.g., [2, 2, 1, 1, 1, 2, 2]
	//       2 (cand = 2, count 1), 2 (count 2), 1 (count 1), 1 (count 0), 1 (cand = 1, count 1), 2 (count 0),
	//       2 (cand = 2, count 1)  ->  answer = 2
	// T.C: O(n)
	// S.C: O(1)
	public int MajorityElement(int[] nums)
	{
		int candidate = 0, count = 0;

		foreach (int num in nums)
		{
			if (count == 0) candidate = num;

			if (num == candidate) count++;
			else count--;
		}

		return candidate;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 2, 2, 1, 1, 1, 2, 2 };

		Console.WriteLine(new Solution().MajorityElement_BruteForce(nums));                 // o/p: 2
		Console.WriteLine(new Solution().MajorityElement_Sorting((int[])nums.Clone()));     // o/p: 2   (a copy, as it sorts the array)
		Console.WriteLine(new Solution().MajorityElement_Hashing(nums));                    // o/p: 2
		Console.WriteLine(new Solution().MajorityElement(nums));                            // o/p: 2
	}
}

// Leetcode: 540. Single Element in a Sorted Array   --->   You are given a sorted array consisting of only integers
// where every element appears exactly twice, except for one element which appears exactly once.
// Return the single element that appears only once.
// Your solution must run in O(log n) time and O(1) space.

// Example 1:
// Input: nums = [1,1,2,3,3,4,4,8,8]
// Output: 2

// Example 2:
// Input: nums = [3,3,7,7,10,11,11]
// Output: 10

// Same algorithm also solves: "Find Odd Occurring Element" ---> All elements come in pairs (next to each other), and a
// value can come in many pairs, e.g., [10, 10, 2, 2, 5, 5, 2, 5, 5, 20, 20, 11, 11, 10, 10] -> Output: 2.
// The array doesn't need to be sorted there; only the "pairs next to each other" pattern matters.


using System;

public class Solution
{
	// Brute force: XOR of all elements. a ^ a = 0, so all pairs cancel out and only the single element is left.
	// T.C: O(n)
	// S.C: O(1)
	public int SingleNonDuplicate_XOR(int[] nums)
	{
		int ans = 0;
		for (int i = 0; i < nums.Length; i++) ans ^= nums[i];
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search using the index pattern.
	//    Array: [1, 1, 2, 3, 3, 4, 4, 8, 8]
	//    Index:  0  1  2  3  4  5  6  7  8
	// ● Before the single element, every pair starts at an EVEN index: (0, 1).
	// ● After the single element, every pair starts at an ODD index: (3, 4), (5, 6), (7, 8).
	// ● The single element itself is always at an even index (because all elements before it come in pairs).
	// So, at 'mid', check which side we are on:
	// Case 1: mid is ODD.
	//         I.   nums[mid] == nums[mid - 1]  ->  pair is (odd - 1, odd) = starts at even. We are BEFORE the single
	//                                              element. Go right: start = mid + 1.
	//         II.  nums[mid] == nums[mid + 1]  ->  pair starts at odd. We are AFTER the single element. Go left: end = mid - 1.
	//         III. mid can't be the single element, as the single element is always at an even index.
	// Case 2: mid is EVEN.
	//         I.   nums[mid] == nums[mid + 1]  ->  pair starts at even. We are BEFORE the single element.
	//                                              Go right: start = mid + 2 (mid + 1 is the same value, so skip it).
	//         II.  nums[mid] == nums[mid - 1]  ->  pair starts at odd. We are AFTER the single element.
	//                                              Go left: end = mid - 2 (mid - 1 is the same value, so skip it).
	//         III. nums[mid] is not equal to any neighbour  ->  It is the single element. Return it.
	// T.C: O(logn)
	// S.C: O(1)
	public int SingleNonDuplicate(int[] nums)
	{
		int n = nums.Length;
		int start = 0, end = n - 1;

		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (mid % 2 == 1)   // mid is odd
			{
				if (nums[mid] == nums[mid - 1]) start = mid + 1;
				else end = mid - 1;   // nums[mid] == nums[mid + 1]
			}
			else   // mid is even
			{
				if (mid + 1 < n && nums[mid] == nums[mid + 1]) start = mid + 2;
				else if (mid - 1 >= 0 && nums[mid] == nums[mid - 1]) end = mid - 2;
				else return nums[mid];
			}
		}

		return -1;   // never reached for a valid input
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 1, 2, 3, 3, 4, 4, 8, 8 };

		Console.WriteLine(new Solution().SingleNonDuplicate_XOR(nums));   // o/p: 2
		Console.WriteLine(new Solution().SingleNonDuplicate(nums));       // o/p: 2
	}
}

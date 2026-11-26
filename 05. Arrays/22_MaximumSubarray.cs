// Leetcode: 53. Maximum Subarray   --->   Given an integer array nums, find the subarray with the largest sum,
// and return its sum.

// Example 1:
// Input: nums = [-2,1,-3,4,-1,2,1,-5,4]
// Output: 6
// Explanation: The subarray [4,-1,2,1] has the largest sum 6.

// Example 2:
// Input: nums = [1]
// Output: 1
// Explanation: The subarray [1] has the largest sum 1.

// Example 3:
// Input: nums = [5,4,-1,7,8]
// Output: 23
// Explanation: The subarray [5,4,-1,7,8] has the largest sum 23.

// Constraints:
// 1 <= nums.length <= 10^5
// -10^4 <= nums[i] <= 10^4

// (What is a subarray? See 16_MaximumAverageSubarrayI.cs)


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force algorithm: Generate all subarrays and find the one with the largest sum.
	// 1. 'i' marks where the subarray starts, and 'j' marks where it ends (j goes from i to n - 1).
	// 2. For every new 'i', start 'sum' from 0, and keep adding nums[j] as j moves ahead. So 'sum' is always the sum of
	//    the subarray from i to j (no need for a 3rd loop to find the sum again).
	// 3. Keep the maximum sum seen so far.
	// T.C: O(n^2)
	// S.C: O(1)
	public int MaxSubArray_BruteForce(int[] nums)
	{
		int n = nums.Length;
		// Never initialize maxSum to 0, because the maximum sum can even be negative (e.g., nums = [-4, -3]).
		// In that case, you will get the wrong output 0.
		int maxSum = int.MinValue;
		for (int i = 0; i < n; i++)
		{
			int sum = 0;
			for (int j = i; j < n; j++)
			{
				sum += nums[j];
				maxSum = Math.Max(maxSum, sum);
			}
		}
		return maxSum;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Kadane's algorithm
	// Carry a running sum while moving through the array, and keep updating the maximum sum found so far.
	// If the running sum becomes negative, reset it to 0. Why? A negative sum only makes any future subarray smaller,
	// so it's better to drop it and start a new subarray from the next element.
	// e.g., nums = [-2, 1, -3, 4, -1, 2, 1, -5, 4]
	//       sum: -2 (reset to 0), 1, -2 (reset to 0), 4, 3, 5, 6, 1, 5   -> maxSum = 6
	// T.C: O(n)
	// S.C: O(1)
	public int MaxSubArray(int[] nums)
	{
		int n = nums.Length;
		int maxSum = int.MinValue, sum = 0;
		for (int i = 0; i < n; i++)
		{
			sum += nums[i];
			maxSum = Math.Max(maxSum, sum);
			if (sum < 0) sum = 0;
		}
		return maxSum;
	}

// ------------------------------------------------------------------------------

	// ● Follow up question: Print the elements of the maximum subarray.
	// Algorithm: It is very simple.
	// 1. In Kadane's algorithm, whenever the sum is 0, a new subarray starts from that index. So keep track of that
	//    starting index in 'start'.
	// 2. Whenever we find a new maximum sum, save the starting index (already marked when sum was 0) in 'ansStart' and
	//    the current index in 'ansEnd'.
	// 3. At the end, the elements from 'ansStart' to 'ansEnd' form the maximum subarray.
	// T.C: O(n)
	// S.C: O(1);   apart from the answer list
	public List<int> MaxSubArray_Print(int[] nums)
	{
		int n = nums.Length;
		int maxSum = int.MinValue, sum = 0;

		int start = 0, ansStart = 0, ansEnd = 0;
		for (int i = 0; i < n; i++)
		{
			if (sum == 0) start = i;   // a new subarray starts from index 'i' if sum is 0
			sum += nums[i];
			// If current sum is greater than maxSum, update maxSum and the indices of the subarray that gives this sum
			if (sum > maxSum)
			{
				maxSum = sum;
				ansStart = start;   // start index of the maximum subarray
				ansEnd = i;         // end index of the maximum subarray
			}
			if (sum < 0) sum = 0;
		}

		List<int> ans = new List<int>();
		for (int i = ansStart; i <= ansEnd; i++) ans.Add(nums[i]);
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };

		Console.WriteLine(new Solution().MaxSubArray_BruteForce(nums));                // o/p: 6
		Console.WriteLine(new Solution().MaxSubArray(nums));                           // o/p: 6
		Console.WriteLine(string.Join(" ", new Solution().MaxSubArray_Print(nums)));   // o/p: 4 -1 2 1
	}
}

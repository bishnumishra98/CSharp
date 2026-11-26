// Leetcode: 643. Maximum Average Subarray I   --->   You are given an integer array nums consisting of n elements,
// and an integer k.
// Find a contiguous subarray whose length is equal to k that has the maximum average value and return this value.
// Any answer with a calculation error less than 10^-5 will be accepted.

// Example 1:
// Input: nums = [1,12,-5,-6,50,3], k = 4
// Output: 12.75000
// Explanation: Maximum average is (12 - 5 - 6 + 50) / 4 = 51 / 4 = 12.75

// Example 2:
// Input: nums = [5], k = 1
// Output: 5.00000

// ● What is a subarray ?
//   A subarray is a contiguous (continuous, without gaps) part of the array. Example:
//   For the arr: [1, 2, 3, 4, 5, 6]
//   Subarrays can be: [1] or [2] or [1, 2] or [1, 2, 3] or [2, 3] or [4, 5, 6], etc.
//   But [1, 3, 4] or [1, 3] or [2, 3, 5] cannot be called a subarray (elements are skipped).


using System;

public class Solution
{
	// Brute force algorithm: Two pointer approach
	// 1. Pointers i and j surround a window of k elements (j = i + k - 1).
	// 2. For every window, find the sum of its k elements using a loop, and keep the maximum sum.
	// 3. Move both pointers 1 step ahead to check the next window of k elements.
	// 4. Average = maxSum / k.
	// T.C: O((n - k) * k) = O(n * k);   O(n - k) for outer loop, and O(k) for inner loop
	// S.C: O(1)
	public double FindMaxAverage_BruteForce(int[] nums, int k)
	{
		int i = 0, j = k - 1;   // i and j surround the range of first k elements in 'nums'
		int maxSum = int.MinValue;   // smallest possible int, so that any real sum is bigger than it

		while (j < nums.Length)
		{
			int sum = 0;
			for (int x = i; x <= j; x++)
			{
				sum = sum + nums[x];
			}
			maxSum = Math.Max(maxSum, sum);
			// Move pointers to check next k elements in 'nums'
			i++;
			j++;
		}

		// (double) is needed. Otherwise int / int gives an int, and the decimal part is lost (51 / 4 = 12, not 12.75).
		double avg = (double)maxSum / k;
		return avg;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Sliding window technique
	// Problem with brute force: Neighbouring windows share k - 1 elements, but we add them again for every window.
	// Solution: When the window slides 1 step, only 1 element leaves (from the left) and 1 element enters (from the right).
	// 1. Find the sum of the first k elements.
	// 2. Slide the window: subtract the element that leaves (nums[i]) and add the element that enters (nums[j]).
	// 3. Update maxSum after every slide.
	// T.C: O(k + (n - k)) = O(n)
	// S.C: O(1)
	public double FindMaxAverage(int[] nums, int k)
	{
		int sum = 0;

		// Calculate sum of first k elements
		for (int x = 0; x < k; x++) sum = sum + nums[x];

		int maxSum = sum;
		// Now slide the window: subtract and add one element in the window at a time; calculate sum and update maxSum if needed.
		int i = 0, j = k;
		while (j < nums.Length)
		{
			sum = sum - nums[i];
			sum = sum + nums[j];
			maxSum = Math.Max(maxSum, sum);
			i++;
			j++;
		}

		double avg = (double)maxSum / k;
		return avg;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 12, -5, -6, 50, 3 };
		int k = 4;

		Console.WriteLine(new Solution().FindMaxAverage_BruteForce(nums, k));   // o/p: 12.75
		Console.WriteLine(new Solution().FindMaxAverage(nums, k));              // o/p: 12.75
	}
}

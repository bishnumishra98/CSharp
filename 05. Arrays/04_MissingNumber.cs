// Leetcode: 268. Missing Number   --->   Given an array nums containing n distinct numbers in the range [0, n],
// return the only number in the range that is missing from the array.

// Example 1:
// Input: nums = [3,0,1]
// Output: 2
// Explanation: n = 3 since there are 3 numbers, so all numbers are in the range [0,3]. 2 is the missing number
// in the range since it does not appear in nums.

// Example 2:
// Input: nums = [0,1]
// Output: 2
// Explanation: n = 2 since there are 2 numbers, so all numbers are in the range [0,2]. 2 is the missing number
// in the range since it does not appear in nums.

// Example 3:
// Input: nums = [9,6,4,2,3,5,7,0,1]
// Output: 8
// Explanation: n = 9 since there are 9 numbers, so all numbers are in the range [0,9]. 8 is the missing number
// in the range since it does not appear in nums.


using System;

public class Solution
{
	// Mathematical approach:
	// 1. Sum of all numbers from 0 to n = n * (n + 1) / 2.
	// 2. Find the sum of all elements of 'nums'.
	// 3. The difference between these 2 sums is the missing number.
	// T.C: O(n)
	// S.C: O(1)
	public int MissingNumber_Math(int[] nums)
	{
		int n = nums.Length;
		int sum = 0;
		for (int i = 0; i < n; i++)
		{
			sum += nums[i];
		}

		int totalSum = (n * (n + 1)) / 2;

		return totalSum - sum;
	}

// ------------------------------------------------------------------------------

	// XOR approach:
	// XOR (^) rules: a ^ a = 0 (same numbers cancel each other) and a ^ 0 = a.
	// 1. XOR all numbers from 0 to n.
	// 2. XOR all elements of 'nums' into the same variable.
	// 3. Every number that is present comes 2 times, so it cancels out. Only the missing number is left.
	// T.C: O(n)
	// S.C: O(1)
	public int MissingNumber(int[] nums)
	{
		int missingNumber = 0;

		// XOR all numbers from 0 to n
		for (int i = 0; i <= nums.Length; i++)
		{
			missingNumber ^= i;
		}
		// For nums = [3, 0, 1]: missingNumber = 0 ^ 1 ^ 2 ^ 3

		// XOR all numbers in the array
		for (int i = 0; i < nums.Length; i++)
		{
			missingNumber ^= nums[i];
		}
		// missingNumber = 0 ^ 1 ^ 2 ^ 3 ^ 3 ^ 0 ^ 1 = 2

		return missingNumber;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 3, 0, 1 };

		Console.WriteLine(new Solution().MissingNumber_Math(nums));   // o/p: 2
		Console.WriteLine(new Solution().MissingNumber(nums));        // o/p: 2
	}
}

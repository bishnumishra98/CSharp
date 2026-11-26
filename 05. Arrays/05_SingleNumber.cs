// Leetcode: 136. Single Number   --->   Given a non-empty array of integers nums, every element appears twice except
// for one. Find that single one.
// You must implement a solution with a linear runtime complexity and use only constant extra space.

// Example 1:
// Input: nums = [2,2,1]
// Output: 1

// Example 2:
// Input: nums = [4,1,2,1,2]
// Output: 4

// Example 3:
// Input: nums = [1]
// Output: 1


using System;

public class Solution
{
	// XOR approach (same idea as 04_MissingNumber.cs):
	// XOR (^) rules: a ^ a = 0 (same numbers cancel each other) and a ^ 0 = a.
	// XOR all elements. Every number that appears twice cancels itself out, so only the single number is left.
	// e.g., 4 ^ 1 ^ 2 ^ 1 ^ 2 = 4 ^ (1 ^ 1) ^ (2 ^ 2) = 4 ^ 0 ^ 0 = 4
	// T.C: O(n)
	// S.C: O(1)
	public int SingleNumber(int[] nums)
	{
		int ans = 0;
		for (int i = 0; i < nums.Length; i++) ans ^= nums[i];
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 4, 1, 2, 1, 2 };
		Console.WriteLine(new Solution().SingleNumber(nums));   // o/p: 4
	}
}

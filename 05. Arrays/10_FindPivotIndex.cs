// Leetcode: 724. Find Pivot Index   --->   Given an array of integers nums, calculate the pivot index of this array.
// The pivot index is the index where the sum of all the numbers strictly to the left of the index is equal to the sum of all
// the numbers strictly to the index's right.
// If the index is on the left edge of the array, then the left sum is 0 because there are no elements to the left. This also
// applies to the right edge of the array.
// Return the leftmost pivot index. If no such index exists, return -1.

// Example 1:
// Input: nums = [1,7,3,6,5,6]
// Output: 3
// Explanation:
// The pivot index is 3.
// Left sum = nums[0] + nums[1] + nums[2] = 1 + 7 + 3 = 11
// Right sum = nums[4] + nums[5] = 5 + 6 = 11

// Example 2:
// Input: nums = [1,2,3]
// Output: -1
// Explanation: There is no index that satisfies the conditions in the problem statement.

// Example 3:
// Input: nums = [2,1,-1]
// Output: 0
// Explanation:
// The pivot index is 0.
// Left sum = 0 (no elements to the left of index 0)
// Right sum = nums[1] + nums[2] = 1 + -1 = 0


using System;
using System.Linq;   // for Sum()

public class Solution
{
	// Brute force algorithm:
	// 1. For every index i, find the sum of all elements on its left (leftSum) and on its right (rightSum).
	// 2. If at any index leftSum and rightSum are equal, return that index.
	// T.C: O(n^2)
	// S.C: O(1)
	public int PivotIndex_BruteForce(int[] nums)
	{
		for (int i = 0; i < nums.Length; i++)
		{
			int leftSum = 0, rightSum = 0;

			for (int j = 0; j < i; j++)
			{
				leftSum += nums[j];
			}

			for (int j = i + 1; j < nums.Length; j++)
			{
				rightSum += nums[j];
			}

			if (leftSum == rightSum) return i;
		}

		return -1;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm:
	// 1. Calculate the total sum of all elements in the array.
	// 2. Initialize a variable 'left' with 0. It will store the sum of all elements on the left of 'i'.
	// 3. For each index 'i', check if 'i' is the pivot index or not:
	//    - Total = left + nums[i] + right. At the pivot index, left == right.
	//      So the condition becomes: total == nums[i] + left + left, i.e., total == nums[i] + (2 * left).
	//      If this is true, return 'i'.
	//    - If 'i' is not the pivot index, just add nums[i] to 'left' and move ahead.
	// 4. If no such index is found, return -1.
	// T.C: O(n)
	// S.C: O(1)
	public int PivotIndex(int[] nums)
	{
		int total = nums.Sum();   // sum of all elements of 'nums'

		int left = 0;
		for (int i = 0; i < nums.Length; i++)
		{
			if (total == nums[i] + (2 * left)) return i;
			left += nums[i];
		}

		return -1;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 7, 3, 6, 5, 6 };

		Console.WriteLine(new Solution().PivotIndex_BruteForce(nums));   // o/p: 3
		Console.WriteLine(new Solution().PivotIndex(nums));              // o/p: 3
	}
}

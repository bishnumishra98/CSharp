// Leetcode: 213. House Robber II   --->   Same as '06_HouseRobber.cs', but the houses stand in a CIRCLE: the first house
// and the last house are neighbours. Return the maximum money you can rob without robbing two adjacent houses.

// Example 1:
// Input: nums = [2,3,2]
// Output: 3   (houses 0 and 2 are neighbours now, so 2 + 2 is not allowed)

// Example 2:
// Input: nums = [1,2,3,1]
// Output: 4   (1 + 3)

// Example 3:
// Input: nums = [1,2,3]
// Output: 3


using System;

public class Solution
{
	// Algorithm: The only new rule: house 0 and house n - 1 can not BOTH be robbed. So at least one of them is skipped:
	//   Case 1: skip the last house  -> a normal row: houses 0 .. n - 2.
	//   Case 2: skip the first house -> a normal row: houses 1 .. n - 1.
	// Every valid plan fits in at least one case, so the answer is the max of the two. Each case is solved with the
	// space-optimized House Robber, on an index RANGE of the same array.
	// T.C: O(n)
	// S.C: O(1)
	// Note: The C++ code copied the two ranges into 2 new arrays, so its S.C was O(n), not the O(1) it said. Passing a
	//       start and an end index avoids the copies.
	public int Rob(int[] nums)
	{
		int n = nums.Length;
		if (n == 1) return nums[0];

		return Math.Max(RobRange(nums, 0, n - 2), RobRange(nums, 1, n - 1));
	}

	// House Robber on nums[start .. end].
	private int RobRange(int[] nums, int start, int end)
	{
		int prev = 0, prev2 = 0;
		for (int i = start; i <= end; i++)
		{
			int curr = Math.Max(nums[i] + prev2, prev);
			prev2 = prev;
			prev = curr;
		}
		return prev;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().Rob(new int[] { 2, 3, 2 }));   // o/p: 3
	}
}

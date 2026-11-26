// Leetcode: 42. Trapping Rain Water   --->   Given n non-negative integers representing an elevation map where the
// width of each bar is 1, compute how much water it can trap after raining.

// Example 1:
// Input: height = [0,1,0,2,1,0,1,3,2,1,2,1]
// Output: 6
// Explanation: The elevation map is represented by array [0,1,0,2,1,0,1,3,2,1,2,1]. In this case, 6 units of rain
// water are being trapped.
//
//                             █
//                 █  ~  ~  ~  █  █  ~  █
//           █  ~  █  █  ~  █  █  █  █  █  █      (█ = building, ~ = trapped water)
//   idx: 0  1  2  3  4  5  6  7  8  9  10 11

// Example 2:
// Input: height = [4,2,0,3,2,5]
// Output: 9

// Better algorithm: Think about ONE building at a time.
// 1. Water can stay on top of building i only if there is a taller building somewhere on its LEFT and somewhere on
//    its RIGHT (like the 2 walls of a glass).
// 2. The water level on top of building i = min(tallest building on its left, tallest building on its right).
//    (Water overflows from the shorter wall.)
// 3. Water on top of building i = water level - height[i].
// 4. Add this for all buildings.
// To quickly know "tallest on the left" and "tallest on the right" for every index, pre-calculate 2 arrays:
// -> prefixMax[i] = tallest building from index 0 to i       (filled from left to right)
// -> suffixMax[i] = tallest building from index i to n - 1   (filled from right to left)
// NOTE: prefixMax[i] and suffixMax[i] include height[i] itself, so the water is never negative.


using System;

public class Solution
{
	// T.C: O(3n) ~ O(n)
	// S.C: O(2n) ~ O(n)
	public int Trap_PrefixSuffix(int[] height)
	{
		int n = height.Length;
		int[] prefixMax = new int[n];   // prefixMax[i] = maximum height of building from 0th to ith index
		int[] suffixMax = new int[n];   // suffixMax[i] = maximum height of building from ith to (n - 1)th index

		prefixMax[0] = height[0];
		for (int i = 1; i < n; i++)
		{
			prefixMax[i] = Math.Max(prefixMax[i - 1], height[i]);
		}

		suffixMax[n - 1] = height[n - 1];
		for (int i = n - 2; i >= 0; i--)
		{
			suffixMax[i] = Math.Max(suffixMax[i + 1], height[i]);
		}

		int totalWater = 0;
		for (int i = 0; i < n; i++)
		{
			int leftMax = prefixMax[i];    // height of the tallest building on the left of ith building (including itself)
			int rightMax = suffixMax[i];   // height of the tallest building on the right of ith building (including itself)

			int water = Math.Min(leftMax, rightMax) - height[i];   // units of water that can be stored on top of ith building
			totalWater += water;
		}

		return totalWater;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Two pointers (no prefixMax / suffixMax arrays)
	// Problem with the better approach: It needs 2 extra arrays just to know the tallest building on each side.
	// Key observation: Water on top of a building = min(leftMax, rightMax) - height. Only the SMALLER of the 2 walls
	//                  matters. We don't need the exact bigger wall, we only need to know that a bigger wall exists.
	// 1. Keep 'left' at the start and 'right' at the end. Also keep leftMax (tallest seen from the left so far) and
	//    rightMax (tallest seen from the right so far).
	// 2. If height[left] < height[right]:
	//    There is a building on the right (height[right]) that is taller than the current left building. So for 'left',
	//    the right wall is surely big enough, and the water is decided only by leftMax.
	//    -> Update leftMax, add (leftMax - height[left]) water, and move left ahead.
	// 3. Else (height[right] <= height[left]): the same logic from the other side.
	//    -> Update rightMax, add (rightMax - height[right]) water, and move right back.
	// 4. Stop when left and right meet.
	// T.C: O(n);   single pass
	// S.C: O(1)
	public int Trap(int[] height)
	{
		int left = 0, right = height.Length - 1;
		int leftMax = 0, rightMax = 0;
		int totalWater = 0;

		while (left < right)
		{
			if (height[left] < height[right])
			{
				leftMax = Math.Max(leftMax, height[left]);
				totalWater += leftMax - height[left];   // 0 if this building is itself the tallest so far
				left++;
			}
			else
			{
				rightMax = Math.Max(rightMax, height[right]);
				totalWater += rightMax - height[right];
				right--;
			}
		}

		return totalWater;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().Trap_PrefixSuffix(new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }));   // o/p: 6
		Console.WriteLine(new Solution().Trap(new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }));                // o/p: 6
	}
}

// Leetcode: 11. Container With Most Water   --->   You are given an integer array height of length n. There are n vertical
// lines drawn such that the two endpoints of the ith line are (i, 0) and (i, height[i]).
// Find two lines that together with the x-axis form a container, such that the container contains the most water.
// Return the maximum amount of water a container can store.
// Notice that you may not slant the container.

// Example 1:
// Input: height = [1,8,6,2,5,4,8,3,7]
// Output: 49
// Explanation: The above vertical lines are represented by array [1,8,6,2,5,4,8,3,7]. In this case, the max area of
// water the container can contain is 49 (between the lines at index 1 and 8: width 7 * height min(8, 7) = 49).

// Example 2:
// Input: height = [1,1]
// Output: 1

// Problem link: https://leetcode.com/problems/container-with-most-water/

// Algorithm: Two pointer greedy approach.
// 1. Start with two pointers, one at the beginning (i) and one at the end (j) of the array.
// 2. Calculate the water between these 2 lines and keep track of the maximum found so far.
//    Water = (j - i) * min(height[i], height[j]).
//    Here, (j - i) is the width of the container and min(height[i], height[j]) is the height up to which it can hold
//    water (water overflows from the shorter line).
// 3. Move the pointer pointing to the shorter line inward. Why? The shorter line limits the water. Moving it inward may
//    find a taller line and increase the water. But moving the taller line inward can never increase the water, because
//    the width decreases and the height is still limited by the same shorter line.
// 4. Repeat steps 2 and 3 until the two pointers meet.


using System;

public class Solution
{
	// T.C: O(n)
	// S.C: O(1)
	public int MaxArea(int[] height)
	{
		int i = 0, j = height.Length - 1;

		int maxVol = 0;
		while (i < j)
		{
			int volume = (j - i) * Math.Min(height[i], height[j]);
			maxVol = Math.Max(maxVol, volume);

			if (height[i] < height[j]) i++;
			else j--;   // if (height[i] >= height[j])
		}

		return maxVol;
	}
}


class Program
{
	static void Main()
	{
		int[] height = { 1, 8, 6, 2, 5, 4, 8, 3, 7 };

		Console.WriteLine(new Solution().MaxArea(height));   // o/p: 49
	}
}

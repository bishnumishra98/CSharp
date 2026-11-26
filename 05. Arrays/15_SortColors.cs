// Leetcode: 75. Sort Colors (also known as Dutch National Flag problem)   --->   Given an array nums with n objects
// colored red, white, or blue, sort them in-place so that objects of the same color are adjacent, with the colors in
// the order red, white, and blue.
// We will use the integers 0, 1, and 2 to represent the color red, white, and blue, respectively.
// You must solve this problem without using the library's sort function.

// Example 1:
// Input: nums = [2,0,2,1,1,0]
// Output: [0,0,1,1,2,2]

// Example 2:
// Input: nums = [2,0,1]
// Output: [0,1,2]

// NOTE: This is the bigger version of 03_Segregate0sAnd1s.cs (there we had only 0s and 1s, here we have 0s, 1s and 2s).


using System;

public class Solution
{
	// Brute force algorithm 1: Just sort the array (not allowed by the problem, but good to know).
	// T.C: O(nlogn)
	// S.C: O(1)
	public void SortColors_BruteForce1(int[] nums)
	{
		Array.Sort(nums);
	}

// ------------------------------------------------------------------------------

	// Brute force algorithm 2: Count the number of 0s, 1s and 2s in the array, and then write those many 0s, 1s and 2s
	// in the array in sequence.
	// T.C: O(2n)
	// S.C: O(1)
	public void SortColors_BruteForce2(int[] nums)
	{
		int n = nums.Length;
		int zeroCount = 0, oneCount = 0, twoCount = 0;

		for (int i = 0; i < n; i++)
		{
			if (nums[i] == 0) zeroCount++;
			else if (nums[i] == 1) oneCount++;
			else twoCount++;
		}

		int index = 0;
		for (int i = 0; i < zeroCount; i++) nums[index++] = 0;
		for (int i = 0; i < oneCount; i++) nums[index++] = 1;
		for (int i = 0; i < twoCount; i++) nums[index++] = 2;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Dutch National Flag algorithm (3 pointer approach)
	// The algorithm is based on 4 rules (ranges):
	// [0 to low - 1]      -> contains 0
	// [low to mid - 1]    -> contains 1
	// [mid to high]       -> contains random numbers (not yet checked)
	// [high + 1 to n - 1] -> contains 2
	// 1. Initialize low, mid and high with 0, 0 and n - 1 respectively. (At the start, everything is "not yet checked".)
	// 2. Our aim is to keep 0, 1, 2 in their ranges given above. For example, take the following unsorted array:

	//           0       0      1      1      0       1       2       0       1       2       0       2       2
	//    Index: 0     low-1   low    mid-1  mid                                             high   high+1   n-1
	//    Look at nums[mid] and move the pointers accordingly.
	//    -> If nums[mid] == 0, then 0 is at a wrong position. Its correct position is right after the 0s, i.e., at 'low'.
	//       Thus, swap(nums[mid], nums[low]). Now the ranges are broken for 1s, so fix them by moving low and mid 1 step ahead.

	//           0       0      0      1      1       1       2       0       1       2       0       2       2
	//    Index: 0            low-1   low    mid-1   mid                                     high   high+1   n-1
	//    -> If nums[mid] == 1, then 1 is already at the right place (just after the 1s range).
	//       Hence, no swapping is needed, just move mid ahead by 1 step.

	//           0       0      0      1      1       1       2       0       1       2       0       2       2
	//    Index: 0            low-1   low            mid-1   mid                             high   high+1   n-1
	//    -> If nums[mid] == 2, then 2 is at a wrong position. Its correct position is just before the 2s, i.e., at 'high'.
	//       Thus, swap(nums[mid], nums[high]), and move high back towards left by 1 step.
	//       NOTE: Do NOT move mid here, because the number that came from 'high' to 'mid' is not yet checked.

	//           0       0      0      1      1       1       0       0       1       2       2       2       2
	//    Index: 0             low-1  low            mid-1   mid                     high   high+1           n-1

	//    Repeat the process until mid crosses high, and the array automatically becomes sorted.

	// T.C: O(n)
	// S.C: O(1)
	public void SortColors(int[] nums)
	{
		int low = 0, mid = 0, high = nums.Length - 1;

		// 0 to low - 1      -> 0
		// low to mid - 1    -> 1
		// mid to high       -> random numbers
		// high + 1 to n - 1 -> 2
		while (mid <= high)
		{
			if (nums[mid] == 0)
			{
				(nums[mid], nums[low]) = (nums[low], nums[mid]);   // swap
				low++;
				mid++;
			}
			else if (nums[mid] == 1)
			{
				mid++;
			}
			else   // if (nums[mid] == 2)
			{
				(nums[mid], nums[high]) = (nums[high], nums[mid]);   // swap
				high--;
			}
		}
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 2, 0, 2, 1, 1, 0 };

		// new Solution().SortColors_BruteForce1(nums);
		// new Solution().SortColors_BruteForce2(nums);
		new Solution().SortColors(nums);

		Console.WriteLine(string.Join(" ", nums));   // o/p: 0 0 1 1 2 2
	}
}

// Leetcode: 189. Rotate Array   --->   Given an integer array nums, rotate the array to the right by k steps,
// where k is non-negative.

// Example 1:
// Input: nums = [1,2,3,4,5,6,7], k = 3
// Output: [5,6,7,1,2,3,4]
// Explanation:
// rotate 1 steps to the right: [7,1,2,3,4,5,6]
// rotate 2 steps to the right: [6,7,1,2,3,4,5]
// rotate 3 steps to the right: [5,6,7,1,2,3,4]

// Example 2:
// Input: nums = [-1,-100,3,99], k = 2
// Output: [3,99,-1,-100]
// Explanation:
// rotate 1 steps to the right: [99,-1,-100,3]
// rotate 2 steps to the right: [3,99,-1,-100]


using System;

public class Solution
{
	// Modulus method:
	// 1. After rotating by k, the element at index i goes to index (i + k). If (i + k) goes beyond the last index,
	//    it comes back from the start. '% n' does this "come back from the start" for us.
	//    e.g., n = 7, k = 3: element at index 5 goes to (5 + 3) % 7 = 1.
	// 2. Put every element at its new index in a new array 'temp', then copy 'temp' back into 'nums'.
	// T.C: O(n)
	// S.C: O(n)
	public void Rotate_ModulusMethod(int[] nums, int k)
	{
		int n = nums.Length;
		int[] temp = new int[n];

		for (int i = 0; i < n; i++)
		{
			temp[(i + k) % n] = nums[i];
		}

		// NOTE (C#): 'nums = temp;' will NOT change the caller's array (it only changes the local variable).
		//            So copy the elements back one by one.
		for (int i = 0; i < n; i++)
		{
			nums[i] = temp[i];
		}
	}

// ------------------------------------------------------------------------------

	// Reversal method: reverse (0, n - 1) -> reverse (0, k - 1) -> reverse (k, n - 1)
	// e.g., nums = [1,2,3,4,5,6,7], k = 3
	//       reverse whole array   : [7,6,5,4,3,2,1]
	//       reverse first k items : [5,6,7,4,3,2,1]
	//       reverse the rest      : [5,6,7,1,2,3,4]   <- answer
	// T.C: O(n)
	// S.C: O(1)
	public void Rotate(int[] nums, int k)
	{
		int n = nums.Length;
		k = k % n;   // to handle the case when k > no.of elements (rotating n times gives the same array)

		// NOTE: Array.Reverse(array, startIndex, length) takes the COUNT of elements to reverse, not the end index.
		// reverse from (0, n - 1)
		Array.Reverse(nums);

		// reverse from (0, k - 1)
		Array.Reverse(nums, 0, k);

		// reverse from (k, n - 1)
		Array.Reverse(nums, k, n - k);
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 2, 3, 4, 5, 6, 7 };
		int k = 3;

		// new Solution().Rotate_ModulusMethod(nums, k);
		new Solution().Rotate(nums, k);

		Console.WriteLine(string.Join(" ", nums));   // o/p: 5 6 7 1 2 3 4
	}
}

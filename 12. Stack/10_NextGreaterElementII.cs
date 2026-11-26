// Leetcode: 503. Next Greater Element II   --->   Given a circular integer array nums (i.e., the next element of
// nums[nums.length - 1] is nums[0]), return the next greater number for every element in nums.
// The next greater number of a number x is the first greater number to its traversing-order next in the array, which
// means you could search circularly to find its next greater number. If it doesn't exist, return -1 for this number.

// Example 1:
// Input: nums = [1,2,1]
// Output: [2,-1,2]
// Explanation: The first 1's next greater number is 2. The number 2 can't find a next greater number. The second 1's next
// greater number needs to search circularly, which is also 2.

// Example 2:
// Input: nums = [6,8,0,1,3]
// Output: [8,-1,1,3,6]
// Explanation: Normal (not circular) answer would be [8,-1,1,3,-1]. In the circular array, after 3 comes 6 again, so 3
// gets 6.

// First understand '07_NextAndPreviousSmallerOrGreaterElement.cs'. The same trick works for all 4 problems there, when
// the array is circular.

// Idea: A circular array behaves like the array written TWICE: [6, 8, 0, 1, 3, 6, 8, 0, 1, 3]. For the last element 3,
// the elements after it (in the second copy) are 6, 8, 0, 1, which is everything that comes after it circularly.
// We don't need to really build the doubled array. Index i from 0 to 2n - 1 maps to the real index i % n:
//   i:      0  1  2  3  4  5  6  7  8  9
//   i % n:  0  1  2  3  4  0  1  2  3  4


// Dry run image (in this folder): 10_NextGreaterElementII.jpg

using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: For every i, look at the next n - 1 elements circularly: (i + 1) % n, (i + 2) % n, ...
	// T.C: O(n^2)
	// S.C: O(1);   ignoring the answer
	public int[] NextGreaterElements_BruteForce(int[] nums)
	{
		int n = nums.Length;
		int[] ans = new int[n];

		for (int i = 0; i < n; i++)
		{
			ans[i] = -1;
			for (int step = 1; step < n; step++)
			{
				int j = (i + step) % n;
				if (nums[j] > nums[i])
				{
					ans[i] = nums[j];
					break;
				}
			}
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Next Greater Element (right to left, pop while top <= x) on the doubled array.
	// 1. Walk i from 2n - 1 down to 0, and use x = nums[i % n].
	// 2. Pop while top <= x. Push x at the end, as usual.
	// 3. Write the answer only when i < n (the first copy). The second copy (i >= n) is only there to fill the stack with
	//    the elements that come "after" the end.
	// T.C: O(2n) = O(n)
	// S.C: O(n);   for the stack
	public int[] NextGreaterElements(int[] nums)
	{
		int n = nums.Length;
		int[] ans = new int[n];
		Stack<int> st = new Stack<int>();

		for (int i = 2 * n - 1; i >= 0; i--)
		{
			int x = nums[i % n];
			while (st.Count > 0 && st.Peek() <= x) st.Pop();

			if (i < n) ans[i] = st.Count > 0 ? st.Peek() : -1;

			st.Push(x);
		}
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 6, 8, 0, 1, 3 };

		Console.WriteLine(string.Join(" ", new Solution().NextGreaterElements_BruteForce(nums)));   // o/p: 8 -1 1 3 6
		Console.WriteLine(string.Join(" ", new Solution().NextGreaterElements(nums)));              // o/p: 8 -1 1 3 6
	}
}

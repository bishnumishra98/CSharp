// Leetcode: 496. Next Greater Element I   --->   The next greater element of some element x in an array is the first
// greater element that is to the right of x in the same array.
// You are given two distinct 0-indexed integer arrays nums1 and nums2, where nums1 is a subset of nums2.
// For each 0 <= i < nums1.length, find the index j such that nums1[i] == nums2[j] and determine the next greater element
// of nums2[j] in nums2. If there is no next greater element, then the answer for this query is -1.
// Return an array ans of length nums1.length such that ans[i] is the next greater element as described above.

// Example 1:
// Input: nums1 = [4,1,2], nums2 = [1,3,4,2]
// Output: [-1,3,-1]
// Explanation: 4 has no greater element on its right in nums2 -> -1. 1 -> 3. 2 -> -1.

// Example 2:
// Input: nums1 = [2,4], nums2 = [1,2,3,4]
// Output: [3,-1]

// Constraints: All integers in nums1 and nums2 are unique.
// First understand '07_NextAndPreviousSmallerOrGreaterElement.cs'.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: For every x in nums1, find x in nums2, then walk right from there until a greater element.
	// T.C: O(n1 * n2)
	// S.C: O(1);   ignoring the answer
	public int[] NextGreaterElement_BruteForce(int[] nums1, int[] nums2)
	{
		int[] ans = new int[nums1.Length];

		for (int i = 0; i < nums1.Length; i++)
		{
			int j = Array.IndexOf(nums2, nums1[i]);
			ans[i] = -1;
			for (int k = j + 1; k < nums2.Length; k++)
			{
				if (nums2[k] > nums1[i])
				{
					ans[i] = nums2[k];
					break;
				}
			}
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Find the next greater element of EVERY number in nums2 once (monotonic stack, right to left), and
	// store it in a Dictionary (number -> its next greater). Then each query from nums1 is just a lookup.
	// (A Dictionary works because all values are unique.)
	// T.C: O(n1 + n2)
	// S.C: O(n2);   for the Dictionary and the stack
	public int[] NextGreaterElement(int[] nums1, int[] nums2)
	{
		Dictionary<int, int> nextGreater = new Dictionary<int, int>();
		Stack<int> st = new Stack<int>();

		for (int i = nums2.Length - 1; i >= 0; i--)
		{
			while (st.Count > 0 && st.Peek() <= nums2[i]) st.Pop();
			nextGreater[nums2[i]] = st.Count > 0 ? st.Peek() : -1;
			st.Push(nums2[i]);
		}

		int[] ans = new int[nums1.Length];
		for (int i = 0; i < nums1.Length; i++) ans[i] = nextGreater[nums1[i]];
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums1 = { 4, 1, 2 }, nums2 = { 1, 3, 4, 2 };

		Console.WriteLine(string.Join(" ", new Solution().NextGreaterElement_BruteForce(nums1, nums2)));   // o/p: -1 3 -1
		Console.WriteLine(string.Join(" ", new Solution().NextGreaterElement(nums1, nums2)));              // o/p: -1 3 -1
	}
}

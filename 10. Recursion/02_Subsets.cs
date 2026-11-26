// Leetcode: 78. Subsets   --->   Given an integer array nums of unique elements, return all possible subsets (the power
// set). The solution set must not contain duplicate subsets. Return the solution in any order.
// (Also called "All Subsequences of an Array". A subsequence keeps the original order of elements: for [6, 7, 8],
// [6, 8] is a subsequence, but [8, 6] is not.)

// Example 1:
// Input: nums = [6,7,8]
// Output: [[6,7,8],[6,7],[6,8],[6],[7,8],[7],[8],[]]

// Example 2:
// Input: nums = [0]
// Output: [[0],[]]

// Pattern: PICK / NOT PICK. For every element, there are exactly 2 choices: put it in the subset, or leave it out.
//          n elements, 2 choices each  ->  2^n subsets.
//
// Recursion tree for [6, 7, 8] (f(index, current subset)):
//                                    f(0, [])
//                       pick 6 /                  \ not pick 6
//                      f(1, [6])                    f(1, [])
//               pick 7 /      \ not          pick 7 /      \ not
//            f(2, [6,7])     f(2, [6])        f(2, [7])      f(2, [])
//             /      \        /     \          /     \        /    \
//        [6,7,8]   [6,7]   [6,8]    [6]     [7,8]    [7]     [8]    []      <- index = 3: a full subset, save it


// Dry run image (in this folder): 02_Subsets.jpg

using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the subsets

public class Solution
{
	// Recursive approach (Pick / Not pick):
	// 1. Base case: index == n  ->  every element has been decided. 'current' is one subset. Save a COPY of it.
	//    (A copy, because 'current' keeps changing as recursion goes on.)
	// 2. Pick:     add nums[index] to 'current', and go to index + 1.
	//    Then BACKTRACK: remove it again, so 'current' is back to how it was before this choice.
	// 3. Not pick: go to index + 1 without adding it.
	// T.C: O(2^n * n);   2^n subsets, and copying each one costs up to n
	// S.C: O(n);   recursion depth and 'current' (ignoring the answer)
	public IList<IList<int>> Subsets_Recursive(int[] nums)
	{
		List<IList<int>> ans = new List<IList<int>>();
		Generate(0, nums, new List<int>(), ans);
		return ans;
	}

	private void Generate(int index, int[] nums, List<int> current, List<IList<int>> ans)
	{
		if (index == nums.Length)
		{
			ans.Add(new List<int>(current));   // save a copy
			return;
		}

		current.Add(nums[index]);                    // pick
		Generate(index + 1, nums, current, ans);
		current.RemoveAt(current.Count - 1);         // backtrack

		Generate(index + 1, nums, current, ans);     // not pick
	}

// ------------------------------------------------------------------------------

	// Iterative approach: Bit masking (no recursion).
	// Idea: Write every number from 0 to 2^n - 1 in binary with n bits. Each bit says pick (1) or not pick (0) for one element.
	//       For [6, 7, 8]:  mask 101 -> bits 0 and 2 are set -> pick nums[0] and nums[2] -> [6, 8]
	//                       mask 000 -> []      mask 111 -> [6, 7, 8]
	//       So the 2^n masks give exactly the 2^n subsets.
	// (mask & (1 << i)) != 0 checks if bit i is set. (See '04. Fundamental Programs\16_SetKthBit.cs'.)
	// T.C: O(2^n * n)
	// S.C: O(1);   ignoring the answer
	public IList<IList<int>> Subsets(int[] nums)
	{
		int n = nums.Length;
		List<IList<int>> ans = new List<IList<int>>();

		for (int mask = 0; mask < (1 << n); mask++)
		{
			List<int> subset = new List<int>();
			for (int i = 0; i < n; i++)
			{
				if ((mask & (1 << i)) != 0) subset.Add(nums[i]);
			}
			ans.Add(subset);
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 6, 7, 8 };

		var ans1 = new Solution().Subsets_Recursive(nums);
		Console.WriteLine(string.Join(" ", ans1.Select(s => "[" + string.Join(",", s) + "]")));   // o/p: [6,7,8] [6,7] [6,8] [6] [7,8] [7] [8] []

		var ans2 = new Solution().Subsets(nums);
		Console.WriteLine(string.Join(" ", ans2.Select(s => "[" + string.Join(",", s) + "]")));   // o/p: [] [6] [7] [6,7] [8] [6,8] [7,8] [6,7,8]
	}
}

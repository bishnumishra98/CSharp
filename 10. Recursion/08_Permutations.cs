// Leetcode 46. Permutations   --->   Given an array nums of distinct integers, return all the possible permutations. You
// can return the answer in any order.
// (A permutation is an arrangement of ALL the elements in some order. n elements have n! permutations.)

// Example 1:
// Input: nums = [1,2,3]
// Output: [[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]

// Example 2:
// Input: nums = [0,1]
// Output: [[0,1],[1,0]]

// Example 3:
// Input: nums = [1]
// Output: [[1]]


// Dry run images (in this folder): 08_Permutations_bruteForce.jpg, 08_Permutations_optimalApproach.jpg

using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the permutations

public class Solution
{
	// Approach 1: Build the permutation one position at a time, using a 'used' array.
	// 1. If 'current' has all n elements, save a copy.
	// 2. Else try every element that is not used yet as the next one: mark it used, add it, recurse, then backtrack
	//    (remove it and mark it unused again, so it can be used at a later position in other permutations).
	// Recursion tree for [1, 2, 3]:
	//                          []
	//            /             |             \
	//          [1]            [2]            [3]
	//         /   \          /   \          /   \
	//     [1,2]  [1,3]    [2,1]  [2,3]    [3,1]  [3,2]
	//       |      |        |      |        |      |
	//   [1,2,3] [1,3,2] [2,1,3] [2,3,1] [3,1,2] [3,2,1]
	// T.C: O(n! * n);   n! permutations, and copying each one costs n
	// S.C: O(n);   'used' array + 'current' + recursion depth (ignoring the answer)
	public IList<IList<int>> Permute_UsedArray(int[] nums)
	{
		List<IList<int>> ans = new List<IList<int>>();
		Build(nums, new List<int>(), new bool[nums.Length], ans);
		return ans;
	}

	private void Build(int[] nums, List<int> current, bool[] used, List<IList<int>> ans)
	{
		if (current.Count == nums.Length)
		{
			ans.Add(new List<int>(current));
			return;
		}

		for (int i = 0; i < nums.Length; i++)
		{
			if (used[i]) continue;

			used[i] = true;
			current.Add(nums[i]);
			Build(nums, current, used, ans);
			current.RemoveAt(current.Count - 1);   // backtrack
			used[i] = false;
		}
	}

// ------------------------------------------------------------------------------

	// Approach 2 (better): Swap in place. No 'used' array, no 'current' list.
	// Idea: Fix one position at a time. For position 'index', every element from 'index' to n - 1 gets a turn to sit
	//       there: swap it into position 'index', fix the rest recursively, then swap it back (backtrack).
	// When index == n, every position is fixed, so nums itself is one permutation.
	// e.g., [1, 2, 3], index 0:  swap(0,0) -> [1,2,3] -> fix index 1 ...  gives [1,2,3], [1,3,2]
	//                            swap(0,1) -> [2,1,3] -> fix index 1 ...  gives [2,1,3], [2,3,1]
	//                            swap(0,2) -> [3,2,1] -> fix index 1 ...  gives [3,2,1], [3,1,2]
	// T.C: O(n! * n)
	// S.C: O(n);   recursion depth only (ignoring the answer)
	public IList<IList<int>> Permute(int[] nums)
	{
		List<IList<int>> ans = new List<IList<int>>();
		Swap(0, nums, ans);
		return ans;
	}

	private void Swap(int index, int[] nums, List<IList<int>> ans)
	{
		if (index == nums.Length)
		{
			ans.Add(new List<int>(nums));
			return;
		}

		for (int i = index; i < nums.Length; i++)
		{
			(nums[index], nums[i]) = (nums[i], nums[index]);
			Swap(index + 1, nums, ans);
			(nums[index], nums[i]) = (nums[i], nums[index]);   // backtrack: swap back
		}
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 2, 3 };

		var ans1 = new Solution().Permute_UsedArray(nums);
		Console.WriteLine(string.Join(" ", ans1.Select(p => "[" + string.Join(",", p) + "]")));   // o/p: [1,2,3] [1,3,2] [2,1,3] [2,3,1] [3,1,2] [3,2,1]

		var ans2 = new Solution().Permute(nums);
		Console.WriteLine(string.Join(" ", ans2.Select(p => "[" + string.Join(",", p) + "]")));   // o/p: [1,2,3] [1,3,2] [2,1,3] [2,3,1] [3,2,1] [3,1,2]
	}
}

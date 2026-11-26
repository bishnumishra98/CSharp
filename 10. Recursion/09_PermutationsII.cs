// Leetcode 47. Permutations II   --->   Given a collection of numbers, nums, that might contain duplicates, return all
// possible unique permutations in any order.

// Example 1:
// Input: nums = [1,1,2]
// Output: [[1,1,2],[1,2,1],[2,1,1]]

// Example 2:
// Input: nums = [1,2,3]
// Output: [[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]

// First understand '08_Permutations.cs'. The problem: with [1, 1, 2], the two 1s are equal, so swapping them gives the
// same permutation again. 3! = 6 permutations are generated, but only 3 are different.


using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the permutations

public class Solution
{
	// Brute force: Generate all n! permutations (swap method), and keep only new ones using a HashSet of string keys.
	// T.C: O(n! * n);   building each key costs n
	// S.C: O(n! * n);   for the HashSet of keys
	public IList<IList<int>> PermuteUnique_HashSet(int[] nums)
	{
		List<IList<int>> ans = new List<IList<int>>();
		GenerateAll(0, nums, new HashSet<string>(), ans);
		return ans;
	}

	private void GenerateAll(int index, int[] nums, HashSet<string> seen, List<IList<int>> ans)
	{
		if (index == nums.Length)
		{
			if (seen.Add(string.Join(",", nums))) ans.Add(new List<int>(nums));
			return;
		}

		for (int i = index; i < nums.Length; i++)
		{
			(nums[index], nums[i]) = (nums[i], nums[index]);
			GenerateAll(index + 1, nums, seen, ans);
			(nums[index], nums[i]) = (nums[i], nums[index]);
		}
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Never create a duplicate.
	// In the swap method, position 'index' gets every element from 'index' to n - 1 as its value. If two of them are
	// equal, putting either one at 'index' leads to exactly the same permutations. So at each position, use every VALUE
	// only once: keep a small HashSet 'tried' of the values already placed at this position, and skip repeats.
	// e.g., [1, 1, 2], index 0: try 1 (tried = {1}), the second 1 is skipped, try 2 (tried = {1, 2}).
	// Note: 'tried' is created fresh inside every call, because each position has its own choices.
	// T.C: O(n! * n) worst case (all distinct), but no duplicate is ever built
	// S.C: O(n^2);   recursion depth n, and each level has a 'tried' set of up to n values (ignoring the answer)
	public IList<IList<int>> PermuteUnique(int[] nums)
	{
		List<IList<int>> ans = new List<IList<int>>();
		Generate(0, nums, ans);
		return ans;
	}

	private void Generate(int index, int[] nums, List<IList<int>> ans)
	{
		if (index == nums.Length)
		{
			ans.Add(new List<int>(nums));
			return;
		}

		HashSet<int> tried = new HashSet<int>();
		for (int i = index; i < nums.Length; i++)
		{
			if (!tried.Add(nums[i])) continue;   // this value was already placed at 'index'

			(nums[index], nums[i]) = (nums[i], nums[index]);
			Generate(index + 1, nums, ans);
			(nums[index], nums[i]) = (nums[i], nums[index]);   // backtrack
		}
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 1, 2 };

		var ans1 = new Solution().PermuteUnique_HashSet(nums);
		Console.WriteLine(string.Join(" ", ans1.Select(p => "[" + string.Join(",", p) + "]")));   // o/p: [1,1,2] [1,2,1] [2,1,1]

		var ans2 = new Solution().PermuteUnique(nums);
		Console.WriteLine(string.Join(" ", ans2.Select(p => "[" + string.Join(",", p) + "]")));   // o/p: [1,1,2] [1,2,1] [2,1,1]
	}
}

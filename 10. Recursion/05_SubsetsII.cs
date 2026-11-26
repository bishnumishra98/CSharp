// Leetcode: 90. Subsets II   --->   Given an integer array nums that may contain duplicates, return all possible subsets
// (the power set). The solution set must not contain duplicate subsets. Return the solution in any order.

// Example 1:
// Input: nums = [1,2,2]
// Output: [[],[1],[1,2],[1,2,2],[2],[2,2]]

// Example 2:
// Input: nums = [0]
// Output: [[],[0]]

// The problem: With pick / not pick on [1, 2, 2], picking the first 2 or the second 2 gives the same subset [1, 2] twice.


// Dry run image (in this folder): 05_SubsetsII.jpg

using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the subsets

public class Solution
{
	// Brute force: Generate all 2^n subsets with pick / not pick (like '02_Subsets.cs'), and drop the duplicates with a
	// HashSet.
	// 1. Sort nums first. Then equal subsets are built in the same order, e.g., always [1, 2] and never [2, 1].
	// 2. A HashSet<List<int>> can't spot two equal lists (it compares references, not contents). So turn each subset into a
	//    string key like "1,2", and keep a subset only if its key is new.
	// T.C: O(2^n * n);   2^n subsets, building each key costs up to n
	// S.C: O(2^n * n);   for the HashSet of keys
	public IList<IList<int>> SubsetsWithDup_HashSet(int[] nums)
	{
		Array.Sort(nums);
		List<IList<int>> ans = new List<IList<int>>();
		Generate(0, nums, new List<int>(), new HashSet<string>(), ans);
		return ans;
	}

	private void Generate(int index, int[] nums, List<int> current, HashSet<string> seen, List<IList<int>> ans)
	{
		if (index == nums.Length)
		{
			if (seen.Add(string.Join(",", current))) ans.Add(new List<int>(current));   // Add() is false for a repeat
			return;
		}

		current.Add(nums[index]);
		Generate(index + 1, nums, current, seen, ans);
		current.RemoveAt(current.Count - 1);

		Generate(index + 1, nums, current, seen, ans);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Never create a duplicate in the first place.
	// New way of thinking: Instead of pick / not pick, each call decides "which element comes NEXT in the subset ?".
	// 1. Sort nums, so equal elements sit next to each other.
	// 2. Every call first saves 'current' as a subset (every node of this tree is a valid subset).
	// 3. Then try every i from 'index' to n - 1 as the next element:
	//    -> SKIP nums[i] if it is the same as nums[i - 1] and i > index. Why ? In THIS call, we already tried that value as
	//       the next element (at i - 1). Trying the same value again would build exactly the same subsets again.
	//       (i == index is allowed: that is the first try in this call, e.g., picking the second 2 right after the first 2,
	//       which makes [2, 2].)
	//    -> Else add nums[i], recurse with index = i + 1, and backtrack.
	// e.g., [1, 2, 2]:
	//       []  -> next = 1: [1] -> next = 2: [1,2] -> next = 2: [1,2,2]
	//                            -> next = 2 again (i = 2): skip, same as before
	//           -> next = 2: [2] -> next = 2: [2,2]
	//           -> next = 2 again (i = 2): skip
	//       Subsets: [], [1], [1,2], [1,2,2], [2], [2,2]
	// T.C: O(2^n * n);   at most 2^n subsets, each copied in O(n)
	// S.C: O(n);   recursion depth (ignoring the answer)
	public IList<IList<int>> SubsetsWithDup(int[] nums)
	{
		Array.Sort(nums);
		List<IList<int>> ans = new List<IList<int>>();
		FindSubsets(0, nums, new List<int>(), ans);
		return ans;
	}

	private void FindSubsets(int index, int[] nums, List<int> current, List<IList<int>> ans)
	{
		ans.Add(new List<int>(current));

		for (int i = index; i < nums.Length; i++)
		{
			if (i > index && nums[i] == nums[i - 1]) continue;   // same value already tried as the next element here

			current.Add(nums[i]);
			FindSubsets(i + 1, nums, current, ans);
			current.RemoveAt(current.Count - 1);   // backtrack
		}
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 2, 2 };

		var ans1 = new Solution().SubsetsWithDup_HashSet((int[])nums.Clone());
		Console.WriteLine(string.Join(" ", ans1.Select(s => "[" + string.Join(",", s) + "]")));   // o/p: [1,2,2] [1,2] [1] [2,2] [2] []

		var ans2 = new Solution().SubsetsWithDup((int[])nums.Clone());
		Console.WriteLine(string.Join(" ", ans2.Select(s => "[" + string.Join(",", s) + "]")));   // o/p: [] [1] [1,2] [1,2,2] [2] [2,2]
	}
}

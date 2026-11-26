// Leetcode: 40. Combination Sum II   --->   Given a collection of candidate numbers (candidates) and a target number
// (target), find all unique combinations in candidates where the candidate numbers sum to target.
// Each number in candidates may only be used once in the combination.
// Note: The solution set must not contain duplicate combinations.

// Example 1:
// Input: candidates = [10,1,2,7,6,1,5], target = 8
// Output: [[1,1,6],[1,2,5],[1,7],[2,6]]

// Example 2:
// Input: candidates = [2,5,2,1,2], target = 5
// Output: [[1,2,2],[5]]

// Constraints: 1 <= candidates[i] <= 50 (all positive)

// This is '05_SubsetsII.cs' (no duplicate subsets) + '03_SubsequencesWithSumK.cs' (sum must be target), together.


// Dry run image (in this folder): 07_CombinationSumII.jpg

using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the combinations

public class Solution
{
	// Brute force: Pick / not pick every subsequence with sum = target, and drop duplicates with a HashSet of string keys.
	// Sort first, so equal combinations are always built in the same order (e.g., always "1,2,2").
	// T.C: O(2^n * n)
	// S.C: O(2^n * n);   for the HashSet of keys (in the worst case)
	public IList<IList<int>> CombinationSum2_HashSet(int[] candidates, int target)
	{
		Array.Sort(candidates);
		List<IList<int>> ans = new List<IList<int>>();
		Generate(0, candidates, target, new List<int>(), new HashSet<string>(), ans);
		return ans;
	}

	private void Generate(int index, int[] arr, int target, List<int> current, HashSet<string> seen, List<IList<int>> ans)
	{
		if (index == arr.Length)
		{
			if (target == 0 && seen.Add(string.Join(",", current))) ans.Add(new List<int>(current));
			return;
		}

		if (arr[index] <= target)   // pick (only if it fits; all numbers are positive)
		{
			current.Add(arr[index]);
			Generate(index + 1, arr, target - arr[index], current, seen, ans);
			current.RemoveAt(current.Count - 1);
		}

		Generate(index + 1, arr, target, current, seen, ans);   // not pick
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: "Which element comes NEXT ?" (same as '05_SubsetsII.cs'), plus the target.
	// 1. Sort the candidates.
	// 2. If target == 0, 'current' is a valid combination. Save it and stop (adding more positive numbers can't help).
	// 3. Try every i from 'index' as the next element:
	//    -> Skip it if it is equal to candidates[i - 1] and i > index (same value already tried here -> duplicates).
	//    -> If candidates[i] > target, STOP the loop (break, not continue). The array is sorted, so every later number
	//       is even bigger and can't fit either. This cuts off many useless branches.
	//    -> Else add it, recurse with i + 1 (each number can be used only once), and backtrack.
	// T.C: O(2^n * n) worst case, but much faster in practice because of the skipping and the 'break'
	// S.C: O(n);   recursion depth (ignoring the answer)
	public IList<IList<int>> CombinationSum2(int[] candidates, int target)
	{
		Array.Sort(candidates);
		List<IList<int>> ans = new List<IList<int>>();
		Find(0, candidates, target, new List<int>(), ans);
		return ans;
	}

	private void Find(int index, int[] arr, int target, List<int> current, List<IList<int>> ans)
	{
		if (target == 0)
		{
			ans.Add(new List<int>(current));
			return;
		}

		for (int i = index; i < arr.Length; i++)
		{
			if (i > index && arr[i] == arr[i - 1]) continue;   // skip duplicates at the same level
			if (arr[i] > target) break;                        // too big, and everything after it is bigger

			current.Add(arr[i]);
			Find(i + 1, arr, target - arr[i], current, ans);
			current.RemoveAt(current.Count - 1);   // backtrack
		}
	}
}


class Program
{
	static void Main()
	{
		int[] candidates = { 10, 1, 2, 7, 6, 1, 5 };
		int target = 8;

		var ans1 = new Solution().CombinationSum2_HashSet((int[])candidates.Clone(), target);
		Console.WriteLine(string.Join(" ", ans1.Select(c => "[" + string.Join(",", c) + "]")));   // o/p: [1,1,6] [1,2,5] [1,7] [2,6]

		var ans2 = new Solution().CombinationSum2((int[])candidates.Clone(), target);
		Console.WriteLine(string.Join(" ", ans2.Select(c => "[" + string.Join(",", c) + "]")));   // o/p: [1,1,6] [1,2,5] [1,7] [2,6]
	}
}

// Leetcode: 39. Combination Sum   --->   Given an array of distinct integers candidates and a target integer target,
// return a list of all unique combinations of candidates where the chosen numbers sum to target. You may return the
// combinations in any order.
// The same number may be chosen from candidates an unlimited number of times. Two combinations are unique if the
// frequency of at least one of the chosen numbers is different.

// Example 1:
// Input: candidates = [2,3,6,7], target = 7
// Output: [[2,2,3],[7]]
// Explanation: 2 and 3 are candidates, and 2 + 2 + 3 = 7. Note that 2 can be used multiple times.
// 7 is a candidate, and 7 = 7. These are the only two combinations.

// Example 2:
// Input: candidates = [2,3,5], target = 8
// Output: [[2,2,2,2],[2,3,3],[3,5]]

// Example 3:
// Input: candidates = [2], target = 1
// Output: []

// Constraints: 1 <= candidates[i] <= 200 (all positive), 1 <= target <= 40


// Dry run images (in this folder): 06_CombinationSum_PartI.jpg, 06_CombinationSum_PartII_TC.jpg, 06_CombinationSum_PartIII_SC.jpg

using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the combinations

public class Solution
{
	// Algorithm: PICK / NOT PICK (like '03_SubsequencesWithSumK.cs'), with one change:
	// • Pick:     add candidates[index], and STAY at the same index (the same number can be picked again).
	// • Not pick: move to index + 1 (we are done with this number).
	// • Pick only if candidates[index] <= target. Since all numbers are positive, going below 0 can never come back to 0.
	//   This check is also what stops the recursion: without it, "pick and stay" would call itself forever.
	// e.g., [2, 3], target = 7:
	//       Keep picking 2: [2] -> [2,2] -> [2,2,2] (target left = 1). Now 2 and 3 are both bigger than 1, so nothing fits.
	//       Go back to [2,2] (target left = 3), not pick 2 any more, pick 3: [2,2,3], target left = 0. Found!
	// T.C: O(2^t * k);   where t = target / smallest candidate (the deepest path), k = length of a combination (to copy it)
	// S.C: O(t);   recursion depth (ignoring the answer)
	public IList<IList<int>> CombinationSum(int[] candidates, int target)
	{
		List<IList<int>> ans = new List<IList<int>>();
		Find(0, candidates, target, new List<int>(), ans);
		return ans;
	}

	private void Find(int index, int[] candidates, int target, List<int> current, List<IList<int>> ans)
	{
		if (index == candidates.Length)
		{
			if (target == 0) ans.Add(new List<int>(current));
			return;
		}

		if (candidates[index] <= target)   // pick (and stay at the same index)
		{
			current.Add(candidates[index]);
			Find(index, candidates, target - candidates[index], current, ans);
			current.RemoveAt(current.Count - 1);   // backtrack
		}

		Find(index + 1, candidates, target, current, ans);   // not pick (move on)
	}
}


class Program
{
	static void Main()
	{
		int[] candidates = { 2, 3, 6, 7 };
		int target = 7;

		var ans = new Solution().CombinationSum(candidates, target);
		Console.WriteLine(string.Join(" ", ans.Select(c => "[" + string.Join(",", c) + "]")));   // o/p: [2,2,3] [7]
	}
}

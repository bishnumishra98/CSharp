// Subsequences with Sum K   --->   Given an array arr and a target k, find the subsequences whose sum is equal to k.
// 3 versions of the same question (all are asked in interviews):
// I.   Return ALL subsequences with sum k.
// II.  Return ANY ONE subsequence with sum k (stop as soon as one is found).
// III. Return the COUNT of subsequences with sum k.

// Example 1:
// Input: arr = [1, 2, 1], k = 3
// Output: I. [[1,2],[2,1]]   II. [1,2]   III. 2

// Example 2:
// Input: arr = [1, 2, 1, 4, 2, 3], k = 4
// Output: I. [[1,2,1],[1,1,2],[1,3],[2,2],[1,3],[4]]   II. [1,2,1]   III. 6

// First understand '02_Subsets.cs'. Same PICK / NOT PICK pattern, but we also carry the remaining target:
// picking arr[index] reduces the target by arr[index]. At the end, the subsequence is valid if the target became 0.


using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the subsequences

public class Solution
{
	// I. All subsequences with sum k.
	// T.C: O(2^n * n);   2^n subsequences, copying each valid one costs up to n
	// S.C: O(n);   recursion depth (ignoring the answer)
	public IList<IList<int>> AllWithSumK(int[] arr, int k)
	{
		List<IList<int>> ans = new List<IList<int>>();
		FindAll(0, arr, k, new List<int>(), ans);
		return ans;
	}

	private void FindAll(int index, int[] arr, int target, List<int> current, List<IList<int>> ans)
	{
		if (index == arr.Length)
		{
			if (target == 0) ans.Add(new List<int>(current));
			return;
		}

		current.Add(arr[index]);                                       // pick
		FindAll(index + 1, arr, target - arr[index], current, ans);
		current.RemoveAt(current.Count - 1);                           // backtrack

		FindAll(index + 1, arr, target, current, ans);                 // not pick
	}

// ------------------------------------------------------------------------------

	// II. Any one subsequence with sum k.
	// Trick: Make the helper return a bool: true = "found one, stop everything". As soon as a call returns true, every
	//        caller above it also returns true right away, without trying its other choice. So the search stops early.
	// T.C: O(2^n) worst case (if no answer exists, every subsequence is still checked)
	// S.C: O(n)
	public IList<int> AnyOneWithSumK(int[] arr, int k)
	{
		List<int> current = new List<int>();
		return FindOne(0, arr, k, current) ? current : new List<int>();
	}

	private bool FindOne(int index, int[] arr, int target, List<int> current)
	{
		if (index == arr.Length) return target == 0;

		current.Add(arr[index]);                                             // pick
		if (FindOne(index + 1, arr, target - arr[index], current)) return true;
		current.RemoveAt(current.Count - 1);                                 // backtrack

		return FindOne(index + 1, arr, target, current);                     // not pick
	}

// ------------------------------------------------------------------------------

	// III. Count of subsequences with sum k.
	// No need to build the subsequence at all. Return 1 from a base case that is valid, 0 otherwise, and add up the counts
	// from both choices: count = (count if we pick) + (count if we don't pick).
	// T.C: O(2^n)
	// S.C: O(n)
	// (If all elements are positive, add 'if (target < 0) return 0;' to cut useless branches early. With negative numbers,
	//  the target can come back up, so we can't stop early. For a faster solution, see Dynamic Programming.)
	public int CountWithSumK(int[] arr, int k)
	{
		return Count(0, arr, k);
	}

	private int Count(int index, int[] arr, int target)
	{
		if (index == arr.Length) return target == 0 ? 1 : 0;

		return Count(index + 1, arr, target - arr[index])   // pick
		     + Count(index + 1, arr, target);              // not pick
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 1, 2, 1 };
		int k = 3;

		var all = new Solution().AllWithSumK(arr, k);
		Console.WriteLine(string.Join(" ", all.Select(s => "[" + string.Join(",", s) + "]")));   // o/p: [1,2] [2,1]
		Console.WriteLine("[" + string.Join(",", new Solution().AnyOneWithSumK(arr, k)) + "]");   // o/p: [1,2]
		Console.WriteLine(new Solution().CountWithSumK(arr, k));                                  // o/p: 2
	}
}

// GFG: Subset Sums   --->   Given an array arr of integers, return the sums of all subsets in it. Output sums can be
// printed in any order (here: ascending order).

// Example 1:
// Input: arr = [2, 3]
// Output: [0, 2, 3, 5]
// Explanation: Subsets: [] = 0, [2] = 2, [3] = 3, [2, 3] = 5

// Example 2:
// Input: arr = [1, 2, 3]
// Output: [0, 1, 2, 3, 3, 4, 5, 6]
// Explanation: Subsets: [] = 0, [1] = 1, [2] = 2, [3] = 3, [1, 2] = 3, [1, 3] = 4, [2, 3] = 5, [1, 2, 3] = 6

// Problem link: https://www.geeksforgeeks.org/problems/subset-sums2234/1


// Dry run image (in this folder): 04_SubsetSums.jpg

using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: Build every subset (like '02_Subsets.cs'), and add up its elements at the end.
	// T.C: O(2^n * n);   adding up each subset costs up to n
	// S.C: O(n);   recursion depth and 'current' (ignoring the answer)
	public List<int> SubsetSums_BuildSubsets(int[] arr)
	{
		List<int> ans = new List<int>();
		Build(0, arr, new List<int>(), ans);
		ans.Sort();
		return ans;
	}

	private void Build(int index, int[] arr, List<int> current, List<int> ans)
	{
		if (index == arr.Length)
		{
			int sum = 0;
			foreach (int x in current) sum += x;
			ans.Add(sum);
			return;
		}

		current.Add(arr[index]);
		Build(index + 1, arr, current, ans);
		current.RemoveAt(current.Count - 1);

		Build(index + 1, arr, current, ans);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Don't build the subset at all. Carry its running SUM instead.
	// Pick: sum + arr[index].  Not pick: sum stays the same.  Base case: save the sum.
	// No list, no add/remove, no adding up at the end.
	// T.C: O(2^n) + O(2^n * log(2^n)) = O(2^n * n);   generating the sums + sorting them
	//      (Without the sorting, it is just O(2^n).)
	// S.C: O(n);   recursion depth (ignoring the answer)
	public List<int> SubsetSums(int[] arr)
	{
		List<int> ans = new List<int>();
		FindSums(0, arr, 0, ans);
		ans.Sort();
		return ans;
	}

	private void FindSums(int index, int[] arr, int sum, List<int> ans)
	{
		if (index == arr.Length)
		{
			ans.Add(sum);
			return;
		}

		FindSums(index + 1, arr, sum + arr[index], ans);   // pick
		FindSums(index + 1, arr, sum, ans);                // not pick
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 1, 2, 3 };

		Console.WriteLine(string.Join(" ", new Solution().SubsetSums_BuildSubsets(arr)));   // o/p: 0 1 2 3 3 4 5 6
		Console.WriteLine(string.Join(" ", new Solution().SubsetSums(arr)));                // o/p: 0 1 2 3 3 4 5 6
	}
}

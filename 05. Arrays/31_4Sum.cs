// Leetcode: 18. 4Sum   --->   Given an array nums of n integers, return an array of all the unique quadruplets
// [nums[a], nums[b], nums[c], nums[d]] such that:
// ▸ 0 <= a, b, c, d < n
// ▸ a, b, c, and d are distinct.
// ▸ nums[a] + nums[b] + nums[c] + nums[d] == target
// You may return the answer in any order.

// Example 1:
// Input: nums = [1,0,-1,0,-2,2], target = 0
// Output: [[-2,-1,1,2],[-2,0,0,2],[-1,0,0,1]]

// Example 2:
// Input: nums = [2,2,2,2,2], target = 8
// Output: [[2,2,2,2]]

// NOTE: Understand 30_3Sum.cs first. 4Sum is the same idea with one more loop.


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	// Brute force algorithm: Check every combination of 4 elements using 4 loops. If their sum is target, sort the
	// quadruplet and store it in a set, so that only unique quadruplets are kept.
	// NOTE (C#): A tuple (int, int, int, int) is used because it compares by values (same reason as in 30_3Sum.cs).
	// T.C: O(n^4)
	// S.C: O(2n)
	public IList<IList<int>> FourSum_BruteForce(int[] nums, int target)
	{
		HashSet<(int, int, int, int)> st = new HashSet<(int, int, int, int)>();

		for (int i = 0; i < nums.Length - 3; i++)
		{
			for (int j = i + 1; j < nums.Length - 2; j++)
			{
				for (int k = j + 1; k < nums.Length - 1; k++)
				{
					for (int l = k + 1; l < nums.Length; l++)
					{
						// (long) to avoid integer overflow, because 4 big ints can add up to more than int.MaxValue
						if ((long)nums[i] + nums[j] + nums[k] + nums[l] == target)
						{
							int[] temp = { nums[i], nums[j], nums[k], nums[l] };
							Array.Sort(temp);
							st.Add((temp[0], temp[1], temp[2], temp[3]));
						}
					}
				}
			}
		}

		IList<IList<int>> ans = st.Select(it => (IList<int>)new List<int> { it.Item1, it.Item2, it.Item3, it.Item4 }).ToList();
		return ans;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Similar to ThreeSum_TwoPointer() in 30_3Sum.cs (hashing using a HashSet).
	// 1. Fix the first 2 elements using 'i' and 'j'.
	// 2. For each pair (i, j), run 'k' ahead and find the 4th number needed: fourth = target - (nums[i] + nums[j] + nums[k]).
	// 3. If 'fourth' is in the hashset (elements seen between j and k), a quadruplet is found. Sort it and store it in a set.
	// 4. Add nums[k] to the hashset so it can be used in future checks for this (i, j).
	// T.C: O(n^3)
	// S.C: O(2n)
	public IList<IList<int>> FourSum_ThreePointer(int[] nums, int target)
	{
		HashSet<(int, int, int, int)> st = new HashSet<(int, int, int, int)>();

		for (int i = 0; i < nums.Length - 2; i++)
		{
			for (int j = i + 1; j < nums.Length - 1; j++)
			{
				HashSet<long> hashset = new HashSet<long>();
				for (int k = j + 1; k < nums.Length; k++)
				{
					long fourth = (long)target - ((long)nums[i] + nums[j] + nums[k]);
					// If fourth element is present in hashset, a quad can be formed using 'nums[i]', 'nums[j]', 'nums[k]'
					// and the fourth element. Thus, sort and store the quad in a set to store only unique quads.
					if (hashset.Contains(fourth))
					{
						int[] temp = { nums[i], nums[j], nums[k], (int)fourth };
						Array.Sort(temp);
						st.Add((temp[0], temp[1], temp[2], temp[3]));
					}
					hashset.Add(nums[k]);
				}
			}
		}

		IList<IList<int>> ans = st.Select(it => (IList<int>)new List<int> { it.Item1, it.Item2, it.Item3, it.Item4 }).ToList();
		return ans;
	}

// ------------------------------------------------------------------------------

	// Best algorithm: 4 pointer approach (similar to ThreeSum() in 30_3Sum.cs)
	// 1. Sort the array.
	// 2. Fix the first element using 'i' and the second element using 'j' (skip duplicates for both).
	// 3. For the remaining 2 elements, use 2 pointers: 'k' just after j, and 'l' at the end.
	//    -> If sum < target, move k ahead (to increase the sum).
	//    -> If sum > target, move l back (to decrease the sum).
	//    -> If sum == target, store the quadruplet, move both k and l, and skip duplicates for both.
	// T.C: O(n^3)
	// S.C: O(n)
	public IList<IList<int>> FourSum(int[] nums, int target)
	{
		IList<IList<int>> ans = new List<IList<int>>();
		if (nums.Length < 4) return ans;
		Array.Sort(nums);

		for (int i = 0; i < nums.Length - 3; i++)
		{
			if (i > 0 && nums[i] == nums[i - 1]) continue;
			for (int j = i + 1; j < nums.Length - 2; j++)
			{
				if (j > i + 1 && nums[j] == nums[j - 1]) continue;
				int k = j + 1;
				int l = nums.Length - 1;

				while (k < l)
				{
					// (long) to avoid integer overflow, because 4 big ints can add up to more than int.MaxValue
					long sum = (long)nums[i] + nums[j] + nums[k] + nums[l];
					if (sum < target) k++;
					else if (sum > target) l--;
					else
					{
						ans.Add(new List<int> { nums[i], nums[j], nums[k], nums[l] });
						k++;
						l--;
						while (k < l && nums[k] == nums[k - 1]) k++;
						while (k < l && nums[l] == nums[l + 1]) l--;
					}
				}
			}
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 0, -1, 0, -2, 2 };
		int target = 0;

		// IList<IList<int>> ans = new Solution().FourSum_BruteForce(nums, target);
		// IList<IList<int>> ans = new Solution().FourSum_ThreePointer(nums, target);
		IList<IList<int>> ans = new Solution().FourSum(nums, target);

		foreach (var quad in ans) Console.WriteLine(string.Join(" ", quad));
		// o/p:
		// -2 -1 1 2
		// -2 0 0 2
		// -1 0 0 1
	}
}

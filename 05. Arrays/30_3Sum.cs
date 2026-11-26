// Leetcode: 15. 3Sum   --->   Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]]
// such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.
// Notice that the solution set must not contain duplicate triplets.

// Example 1:
// Input: nums = [-1,0,1,2,-1,-4]
// Output: [[-1,-1,2],[-1,0,1]]
// Explanation:
// nums[0] + nums[1] + nums[2] = (-1) + 0 + 1 = 0.
// nums[1] + nums[2] + nums[4] = 0 + 1 + (-1) = 0.
// nums[0] + nums[3] + nums[4] = (-1) + 2 + (-1) = 0.
// The distinct triplets are [-1,0,1] and [-1,-1,2].
// Notice that the order of the output and the order of the triplets does not matter.

// Example 2:
// Input: nums = [0,1,1]
// Output: []
// Explanation: The only possible triplet does not sum up to 0.

// Example 3:
// Input: nums = [0,0,0]
// Output: [[0,0,0]]
// Explanation: The only possible triplet sums up to 0.


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	// Brute force algorithm: Check every combination of 3 elements using 3 loops. If their sum is 0, sort the triplet and
	// store it in a set, so that only unique triplets are kept.
	// T.C: O(n^3)
	// S.C: O(2n)
	public IList<IList<int>> ThreeSum_BruteForce(int[] nums)
	{
		// set in order to store only unique triplets
		// NOTE (C#): HashSet<List<int>> would NOT remove duplicates, because two lists with the same numbers
		// are still 2 different objects. A tuple (int, int, int) compares by values, so duplicates are removed.
		HashSet<(int, int, int)> st = new HashSet<(int, int, int)>();

		for (int i = 0; i < nums.Length - 2; i++)
		{
			for (int j = i + 1; j < nums.Length - 1; j++)
			{
				for (int k = j + 1; k < nums.Length; k++)
				{
					if (nums[i] + nums[j] + nums[k] == 0)
					{
						int[] temp = { nums[i], nums[j], nums[k] };
						Array.Sort(temp);
						st.Add((temp[0], temp[1], temp[2]));
					}
				}
			}
		}

		// IList<IList<int>> ans = new List<IList<int>>();
		// foreach (var it in st) ans.Add(new List<int> { it.Item1, it.Item2, it.Item3 });

		// The above 2 lines can be done in 1 single line (using LINQ)
		IList<IList<int>> ans = st.Select(it => (IList<int>)new List<int> { it.Item1, it.Item2, it.Item3 }).ToList();

		return ans;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Two-pointer approach using hashing using a hashset, i.e., HashSet<int>
	// 1. Fix the first element of the triplet using index 'i' (from 0 to n - 2).
	// 2. For each 'i', initialize an empty hashset (HashSet<int>) to track elements seen so far in the inner loop.
	//    The hashset actually stores all the elements between 'i'th and 'j'th index (nums[i] and nums[j] excluded).
	// 3. Run an inner loop with index 'j' from i + 1 to n - 1:
	//    a. Calculate the third required number as '-(nums[i] + nums[j])'.
	//    b. If this third number exists in the hashset, it means we’ve found a triplet.
	//       So, sort {nums[i], nums[j], third} and insert into a set to ensure uniqueness.
	//    c. Insert nums[j] into the hashset so it can be used in future checks for this i.
	// 4. Convert the set of triplets to a list and return it.
	// T.C: O(n^2);   as hash operations like Contains() and Add() takes constant time in average case.
	// S.C: O(2n)
	public IList<IList<int>> ThreeSum_TwoPointer(int[] nums)
	{
		HashSet<(int, int, int)> st = new HashSet<(int, int, int)>();

		for (int i = 0; i < nums.Length - 1; i++)
		{
			HashSet<int> hashset = new HashSet<int>();
			for (int j = i + 1; j < nums.Length; j++)
			{
				int third = -(nums[i] + nums[j]);
				// If third element is present in hashset, it means a triplet can be formed using 'nums[i]', 'nums[j]'
				// and third element. Thus, sort and store the triplet in a set to store only unique triplets.
				// Then store the current element 'nums[j]' in hashset and move 'j' ahead.
				if (hashset.Contains(third))
				{
					int[] temp = { nums[i], nums[j], third };
					Array.Sort(temp);
					st.Add((temp[0], temp[1], temp[2]));
				}
				hashset.Add(nums[j]);
			}
		}

		IList<IList<int>> ans = st.Select(it => (IList<int>)new List<int> { it.Item1, it.Item2, it.Item3 }).ToList();
		return ans;
	}

// ---------------------------------------------------------------------------------------

	// Best algorithm: 3 pointer approach
	// 1. Sort the 'nums' array.
	// 2. Initialize 'i' as the fixed element (starting from the first element). Initialize 'j' to start from 'i + 1'
	//    and 'k' to last element of 'nums'. 'nums[i]' represents the first element, and 'nums[j]' represents the second
	//    element and 'nums[k]' represents the third element in the triplet respectively.
	// 3. Now calculate the sum of nums[i], nums[j] and nums[k]. If the sum is less than 0, j pointers needs to be moved ahead
	//    towards right, whereas if sum is greater than 0, k pointers needs to be moved towards left. If sum is found to be 0,
	//    push the triplet nums[i], nums[j] and nums[k] in 'ans'. Now as j is pushed towards right and k is pushed towards left,
	//    it might happen that even after reposition, they point to an element that has same value as the previous ones. That's
	//    why don't push the pointers just 1 step ahead or back, instead keep pushing them until they reach a new element.
	//    Once j reaches k, stop the iteration and change the position of i to next element on the left. For i too, follow the
	//    same pushing rule until it reaches a new element. In this this way the i shall move till the 3rd last element of 'nums'.
	// T.C: O(n^2)
	// S.C: O(n)
	public IList<IList<int>> ThreeSum(int[] nums)
	{
		if (nums.Length < 3) return new List<IList<int>>();
		Array.Sort(nums);
		IList<IList<int>> ans = new List<IList<int>>();

		for (int i = 0; i < nums.Length - 2; i++)
		{
			if (i > 0 && nums[i] == nums[i - 1]) continue;
			int j = i + 1;
			int k = nums.Length - 1;

			while (j < k)
			{
				int sum = nums[i] + nums[j] + nums[k];
				if (sum > 0) k--;
				else if (sum < 0) j++;
				else
				{
					List<int> temp = new List<int> { nums[i], nums[j], nums[k] };
					ans.Add(temp);
					j++;
					k--;
					while (j < k && nums[j] == nums[j - 1]) j++;
					while (j < k && nums[k] == nums[k + 1]) k--;
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
		int[] nums = { -1, 0, 1, 2, -1, -4 };

		// IList<IList<int>> ans = new Solution().ThreeSum_BruteForce(nums);
		// IList<IList<int>> ans = new Solution().ThreeSum_TwoPointer(nums);
		IList<IList<int>> ans = new Solution().ThreeSum(nums);

		foreach (var it1 in ans)
		{
			foreach (var it2 in it1)
			{
				Console.Write(it2 + " ");
			}
			Console.WriteLine();
		}
	}
}

// Leetcode: 229. Majority Element II   --->   Given an integer array of size n, find all elements that appear more than
// ⌊n / 3⌋ times.

// Example 1:
// Input: nums = [3,2,3]
// Output: [3]

// Example 2:
// Input: nums = [1]
// Output: [1]

// Example 3:
// Input: nums = [1,2]
// Output: [1,2]

// Follow up: Could you solve the problem in linear time and in O(1) space?

// Key fact: At most 2 elements can appear more than n / 3 times. (3 such elements would need more than n elements.)


using System;
using System.Collections.Generic;

public class Solution
{
	// Sorting + Binary search:
	// 1. Sort the array. Now equal elements sit next to each other.
	// 2. For each unique element, find its first and last position with binary search
	//    (see '06. Searching\05_FindFirstAndLastPositionOfElementInSortedArray.cs').
	//    Count = last - first + 1. If count > n / 3, add it to the answer.
	// 3. Jump to last + 1, which is where the next unique element starts.
	// Note: Array.Sort() changes the input array.
	// T.C: O(nlogn);   sorting is the slowest step
	// S.C: O(1);   ignoring the answer list
	public IList<int> MajorityElement_Sorting(int[] nums)
	{
		int n = nums.Length;
		List<int> ans = new List<int>();

		Array.Sort(nums);

		int i = 0;
		while (i < n)
		{
			int first = FindPosition(nums, nums[i], true);
			int last = FindPosition(nums, nums[i], false);

			if (last - first + 1 > n / 3) ans.Add(nums[i]);

			i = last + 1;   // next unique element
		}

		return ans;
	}

	private int FindPosition(int[] nums, int target, bool findFirst)
	{
		int start = 0, end = nums.Length - 1;
		int ans = -1;

		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (target > nums[mid]) start = mid + 1;
			else if (target < nums[mid]) end = mid - 1;
			else
			{
				ans = mid;
				if (findFirst) end = mid - 1;
				else start = mid + 1;
			}
		}

		return ans;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Hashing. Count every element in a Dictionary, then pick the ones with count > n / 3.
	// T.C: O(n)
	// S.C: O(n);   for the Dictionary
	public IList<int> MajorityElement_Hashing(int[] nums)
	{
		Dictionary<int, int> freq = new Dictionary<int, int>();
		foreach (int num in nums)
		{
			freq[num] = freq.GetValueOrDefault(num) + 1;
		}

		List<int> ans = new List<int>();
		foreach (var (num, count) in freq)
		{
			if (count > nums.Length / 3) ans.Add(num);
		}

		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Extended Moore's Voting Algorithm (2 candidates).
	// Same idea as '06_MajorityElement.cs', but since at most 2 elements can be the answer, keep 2 candidates.
	// A group of 3 different elements cancels out. The real answers survive this cancelling.
	// 1. Pass 1 (find candidates): For every element:
	//    -> If it equals cand1, count1++.   Else if it equals cand2, count2++.
	//    -> Else if count1 == 0, make it cand1 (count1 = 1).   Else if count2 == 0, make it cand2 (count2 = 1).
	//    -> Else, it is different from both: count1-- and count2-- (3 different elements cancel out).
	// 2. Pass 2 (verify): The candidates are only "possible" answers. e.g., in [1, 2, 3], some candidates remain, but
	//    nothing appears more than n / 3 times. So count both candidates again, and keep only those with count > n / 3.
	// Note: Checking "equals cand1/cand2" BEFORE "count == 0" is important. Otherwise the same value can become both
	//       cand1 and cand2.
	// T.C: O(2n) = O(n)
	// S.C: O(1)
	public IList<int> MajorityElement(int[] nums)
	{
		int cand1 = 0, cand2 = 0, count1 = 0, count2 = 0;

		foreach (int num in nums)
		{
			if (num == cand1) count1++;
			else if (num == cand2) count2++;
			else if (count1 == 0)
			{
				cand1 = num;
				count1 = 1;
			}
			else if (count2 == 0)
			{
				cand2 = num;
				count2 = 1;
			}
			else
			{
				count1--;
				count2--;
			}
		}

		// Pass 2: verify
		count1 = 0;
		count2 = 0;
		foreach (int num in nums)
		{
			if (num == cand1) count1++;
			else if (num == cand2) count2++;
		}

		List<int> ans = new List<int>();
		if (count1 > nums.Length / 3) ans.Add(cand1);
		if (count2 > nums.Length / 3) ans.Add(cand2);
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 3, 2, 3 };

		Console.WriteLine(string.Join(" ", new Solution().MajorityElement_Sorting((int[])nums.Clone())));    // o/p: 3
		Console.WriteLine(string.Join(" ", new Solution().MajorityElement_Hashing(nums)));                   // o/p: 3
		Console.WriteLine(string.Join(" ", new Solution().MajorityElement(nums)));                           // o/p: 3
	}
}

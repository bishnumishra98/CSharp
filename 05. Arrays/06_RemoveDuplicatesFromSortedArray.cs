// Leetcode: 26. Remove Duplicates from Sorted Array   --->   Given an integer array nums sorted in non-decreasing order,
// remove the duplicates in-place such that each unique element appears only once. The relative order of the elements
// should be kept the same. Then return the number of unique elements in nums.
// Consider the number of unique elements of nums to be k, to get accepted, you need to do the following things:
// -> Change the array nums such that the first k elements of nums contain the unique elements in the order they were
//    present in nums initially. The remaining elements of nums are not important as well as the size of nums.
// -> Return k.

// Example 1:
// Input: nums = [1,1,2]
// Output: 2, nums = [1,2,_]
// Explanation: Your function should return k = 2, with the first two elements of nums being 1 and 2 respectively.
// It does not matter what you leave beyond the returned k (hence they are underscores).

// Example 2:
// Input: nums = [0,0,1,1,1,2,2,3,3,4]
// Output: 5, nums = [0,1,2,3,4,_,_,_,_,_]
// Explanation: Your function should return k = 5, with the first five elements of nums being 0, 1, 2, 3, and 4 respectively.
// It does not matter what you leave beyond the returned k (hence they are underscores).

// Algorithm: Two pointer approach.
// 1. Initialize j with 0, and i with 1. The idea of this algorithm is: all elements till index j must be unique.
// 2. Compare nums[j] and nums[i]:
//    -> If they are equal, a duplicate is present at index i. So just move i ahead.
//    -> If they are not equal, a new unique element is present at index i. So place it just after nums[j], i.e., at
//       index j + 1, and move j ahead (j now marks the end of the updated range of unique elements).
//    Continue until i reaches the last index of nums.
// 3. As j points to the index of the last unique element, the no.of unique elements is j + 1. So return j + 1.


using System;

public class Solution
{
	// T.C: O(n);   where n = nums.Length
	// S.C: O(1)
	public int RemoveDuplicates(int[] nums)
	{
		int j = 0, i = 1;
		while (i < nums.Length)
		{
			// If a unique element is found at 'i'th index, keep it on (j + 1)th index.
			if (nums[j] != nums[i])
			{
				j++;
				nums[j] = nums[i];
			}
			i++;
		}

		return j + 1;
	}

// ------------------------------------------------------------------------------

	// Same algorithm, shorter code using a for loop.
	// T.C: O(n);   where n = nums.Length
	// S.C: O(1)
	public int RemoveDuplicates_Shorthand(int[] nums)
	{
		int j = 0;
		for (int i = 1; i < nums.Length; i++)
		{
			// If a unique element is found, keep it on (j + 1)th index, i.e., just next after nums[j].
			// nums[++j] means: first increase j by 1, then use it as the index.
			if (nums[j] != nums[i]) nums[++j] = nums[i];
		}

		return j + 1;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 };

		// int k = new Solution().RemoveDuplicates(nums);
		int k = new Solution().RemoveDuplicates_Shorthand(nums);

		Console.WriteLine("k: " + k);                 // o/p: k: 5
		Console.WriteLine(string.Join(" ", nums));    // o/p: 0 1 2 3 4 2 2 3 3 4   (only the first k elements matter)
	}
}

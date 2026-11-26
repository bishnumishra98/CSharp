// Leetcode: 128. Longest Consecutive Sequence   --->   Given an unsorted array of integers nums, return the length of
// the longest consecutive elements sequence.
// You must write an algorithm that runs in O(n) time.

// Example 1:
// Input: nums = [100,4,200,1,3,2]
// Output: 4
// Explanation: The longest consecutive elements sequence is [1, 2, 3, 4]. Therefore its length is 4.

// Example 2:
// Input: nums = [0,3,7,2,5,8,4,6,0,1]
// Output: 9

// Example 3:
// Input: nums = [1,0,1,2]
// Output: 3

// Constraints:
// 0 <= nums.length <= 10^5
// -10^9 <= nums[i] <= 10^9

// NOTE: The sequence does not need to be next to each other in the array. e.g., in [100, 4, 200, 1, 3, 2], the numbers
//       1, 2, 3, 4 are scattered, but they still form a consecutive sequence.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force algorithm: Sort the array, so consecutive numbers come next to each other.
	// 1. Go through the sorted array and compare each element with the previous one:
	//    -> If they are equal, it's a duplicate. Skip it (it neither breaks nor extends the sequence).
	//    -> If the current element = previous + 1, the sequence continues. So increase 'length'.
	//    -> Else the sequence breaks. So start a new sequence with length = 1.
	// 2. Keep the maximum length seen so far.
	// T.C: O(nlogn) + O(n) = O(nlogn)
	// S.C: O(1)
	public int LongestConsecutive_BruteForce(int[] nums)
	{
		// Edge cases
		if (nums.Length == 0) return 0;
		if (nums.Length == 1) return 1;

		Array.Sort(nums);

		int length = 1, maxLength = 1;
		for (int i = 1; i < nums.Length; i++)
		{
			if (nums[i] == nums[i - 1]) continue;
			else if (nums[i] == nums[i - 1] + 1)
			{
				length++;
				maxLength = Math.Max(maxLength, length);
			}
			else length = 1;
		}

		return maxLength;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm:
	// 1. Store all the elements in a HashSet (it also removes duplicates).
	// 2. For each element in the set:
	//    i.   Check if (element - 1) is present in the set. If it is NOT present, the current element is the starting
	//         point of a consecutive sequence. (If it IS present, skip it, because it is part of a sequence that will be
	//         counted from its own starting point.)
	//    ii.  From the starting point, keep checking for the next elements (element + 1, element + 2, ...) in the set
	//         until one is missing, and count the length of the sequence.
	//    iii. Update the maximum length found so far.
	// 3. Finally, return the maximum length found.
	// Why HashSet? Contains() takes O(1) time on average, which is needed to get O(n) overall.
	// Why is it O(n) even with a while loop inside a for loop? The while loop runs only from the START of a sequence.
	// So every element is visited at most 2 times in total: once by the for loop, once by the while loop.
	// T.C: O(n) to build the set + O(2n) for the loops = O(3n)
	// S.C: O(n)
	public int LongestConsecutive(int[] nums)
	{
		if (nums.Length == 0) return 0;   // edge case

		HashSet<int> st = new HashSet<int>(nums);   // puts all elements of 'nums' into the set

		int maxLength = 1;

		foreach (int it in st)
		{
			// If 'it - 1' is not present in the set, it means 'it' is the starting element of a sequence.
			if (!st.Contains(it - 1))
			{
				int curr = it;
				int length = 1;
				// Find the upcoming consecutive elements after 'curr' to find the length of the sequence starting from 'it'.
				while (st.Contains(curr + 1))
				{
					length++;
					curr = curr + 1;
				}
				maxLength = Math.Max(maxLength, length);   // update the maximum length found so far
			}
		}

		return maxLength;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().LongestConsecutive_BruteForce(new int[] { 100, 4, 200, 1, 3, 2 }));   // o/p: 4
		Console.WriteLine(new Solution().LongestConsecutive(new int[] { 100, 4, 200, 1, 3, 2 }));              // o/p: 4
	}
}

// Leetcode: 632. Smallest Range Covering Elements from K Lists   --->   You have k lists of integers, each sorted in
// non-decreasing order. Find the SMALLEST range [a, b] that includes at least one number from EACH list.
// Range [a, b] is smaller than [c, d] if b - a < d - c, or b - a == d - c and a < c.

// Example 1:
// Input: nums = [[4,10,15,24,26],[0,9,12,20],[5,18,22,30]]
// Output: [20,24]
// Explanation: list 1 has 24, list 2 has 20, list 3 has 22. All are inside [20, 24].

// Example 2:
// Input: nums = [[1,2,3],[1,2,3],[1,2,3]]
// Output: [1,1]

// First understand '12_MergeKSortedArrays.cs'. Same min heap of k "fronts".


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm: Keep exactly ONE pointer in every list, so the k pointed values always cover all lists. Their range is
	// [smallest of them, largest of them].
	// 1. Start with the first value of every list. Put them in a min heap, and remember the max of them.
	// 2. The current range is [heap top, max]. If it is smaller than the best so far, save it.
	// 3. To get a smaller range, the only useful move is to raise the SMALLEST value (raising any other one can only
	//    keep or grow the range). So take out the top, and put the NEXT value of the same list in. Update max.
	// 4. Stop when the list of the removed smallest value has no next value: we can not raise it any more, and without
	//    it some list would not be covered.
	// e.g., Example 1 starts with {4, 0, 5}, max 5: range [0, 5] -> raise 0 to 9: {4, 9, 5}, max 9: [4, 9]
	//       -> raise 4 to 10: [5, 10] -> ... -> {24, 20, 22}: [20, 24] -> ... until list 2 (0, 9, 12, 20) runs out.
	// T.C: O(N log k);   N = all values together; each one goes in and out of a heap of size k
	// S.C: O(k);   the heap
	// Note: The C++ comment said S.C: O(n + k), counting a "merged array" that the code does not have. It is O(k).
	public int[] SmallestRange(IList<IList<int>> nums)
	{
		PriorityQueue<(int list, int index), int> minHeap = new PriorityQueue<(int list, int index), int>();
		int max = int.MinValue;

		for (int i = 0; i < nums.Count; i++)
		{
			minHeap.Enqueue((i, 0), nums[i][0]);
			max = Math.Max(max, nums[i][0]);
		}

		int bestStart = 0, bestEnd = int.MaxValue;

		while (true)
		{
			minHeap.TryDequeue(out var front, out int min);

			if ((long)max - min < (long)bestEnd - bestStart)   // long: the difference of 2 ints can overflow an int
			{
				bestStart = min;
				bestEnd = max;
			}

			if (front.index + 1 == nums[front.list].Count) break;   // this list is used up

			int next = nums[front.list][front.index + 1];
			minHeap.Enqueue((front.list, front.index + 1), next);
			max = Math.Max(max, next);
		}

		return new int[] { bestStart, bestEnd };
	}
}


class Program
{
	static void Main()
	{
		IList<IList<int>> nums = new List<IList<int>>
		{
			new List<int> { 4, 10, 15, 24, 26 },
			new List<int> { 0, 9, 12, 20 },
			new List<int> { 5, 18, 22, 30 }
		};

		Console.WriteLine(string.Join(" ", new Solution().SmallestRange(nums)));   // o/p: 20 24
	}
}

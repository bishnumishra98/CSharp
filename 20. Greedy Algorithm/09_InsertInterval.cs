// Leetcode: 57. Insert Interval   --->   intervals is a list of NON-overlapping intervals, sorted by start. Insert
// newInterval so the list is still sorted and non-overlapping (merge where needed). Return the new list.

// Example 1:
// Input: intervals = [[1,3],[6,9]], newInterval = [2,5]
// Output: [[1,5],[6,9]]

// Example 2:
// Input: intervals = [[1,2],[3,5],[6,7],[8,10],[12,16]], newInterval = [4,8]
// Output: [[1,2],[3,10],[12,16]]   ([4,8] overlaps [3,5], [6,7] and [8,10])


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	// Algorithm: Because the list is sorted, it splits into 3 parts:
	// 1. LEFT:   intervals that end BEFORE newInterval starts (end < new.start): copy them.
	// 2. MIDDLE: intervals that overlap newInterval (start <= new.end): merge them all into newInterval
	//            (smallest start, biggest end). Then add the merged interval.
	// 3. RIGHT:  everything left over starts after newInterval: copy them.
	// T.C: O(n)
	// S.C: O(n);   the answer
	public int[][] Insert(int[][] intervals, int[] newInterval)
	{
		List<int[]> result = new List<int[]>();
		int start = newInterval[0], end = newInterval[1];
		int i = 0, n = intervals.Length;

		while (i < n && intervals[i][1] < start) result.Add(intervals[i++]);

		while (i < n && intervals[i][0] <= end)
		{
			start = Math.Min(start, intervals[i][0]);
			end = Math.Max(end, intervals[i][1]);
			i++;
		}
		result.Add(new[] { start, end });

		while (i < n) result.Add(intervals[i++]);

		return result.ToArray();
	}
}

// Note: The C++ code changed the caller's newInterval while merging. Here two local variables are used instead.


class Program
{
	static void Main()
	{
		int[][] intervals = { new[] { 1, 2 }, new[] { 3, 5 }, new[] { 6, 7 }, new[] { 8, 10 }, new[] { 12, 16 } };

		var merged = new Solution().Insert(intervals, new[] { 4, 8 }).Select(iv => $"[{iv[0]},{iv[1]}]");
		Console.WriteLine(string.Join(" ", merged));   // o/p: [1,2] [3,10] [12,16]
	}
}

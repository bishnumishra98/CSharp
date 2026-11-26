// Leetcode: 435. Non-overlapping Intervals   --->   Given intervals [start, end], return the MINIMUM number to remove so
// that the rest do not overlap. Intervals that only TOUCH ([1,2] and [2,3]) do not overlap.

// Example 1:
// Input: intervals = [[1,2],[2,3],[3,4],[1,3]]
// Output: 1   (remove [1,3])

// Example 2:
// Input: intervals = [[1,2],[1,2],[1,2]]
// Output: 2

// Example 3:
// Input: intervals = [[1,2],[2,3]]
// Output: 0

// First understand '07_NMeetingsInOneRoom.cs'. Removing the FEWEST = keeping the MOST non-overlapping intervals.


using System;

public class Solution
{
	// Algorithm: Keep as many intervals as possible with the activity selection greedy (sort by END, take an interval if
	// it starts at or after the last kept end; ">=" because touching is allowed here). Answer = total - kept.
	// T.C: O(n log n)
	// S.C: O(n);   a sorted copy
	public int EraseOverlapIntervals(int[][] intervals)
	{
		int[][] sorted = (int[][])intervals.Clone();
		Array.Sort(sorted, (a, b) => a[1].CompareTo(b[1]));

		int kept = 0;
		long lastEnd = long.MinValue;   // smaller than any start
		foreach (int[] iv in sorted)
		{
			if (iv[0] >= lastEnd)
			{
				kept++;
				lastEnd = iv[1];
			}
		}
		return intervals.Length - kept;
	}
}


class Program
{
	static void Main()
	{
		int[][] intervals = { new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 1, 3 } };

		Console.WriteLine(new Solution().EraseOverlapIntervals(intervals));   // o/p: 1
	}
}

// GFG: N Meetings in One Room   --->   Meeting i runs from start[i] to end[i]. One room, one meeting at a time, and a
// meeting may NOT start at the exact moment the previous one ends. Return the maximum number of meetings (and, as a
// follow up, which ones).

// Example 1:
// Input: start = [1, 3, 0, 5, 8, 5], end = [2, 4, 6, 7, 9, 9]
// Output: 4   (meetings (1,2), (3,4), (5,7), (8,9): numbers 1, 2, 4, 5)

// Example 2:
// Input: start = [10, 12, 20], end = [20, 25, 30]
// Output: 1

// Problem link: https://www.geeksforgeeks.org/problems/n-meetings-in-one-room-1587115620/1

// The classic ACTIVITY SELECTION problem. The intervals problems in '21. Intervals_&_SweepLine' build on it.


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm: Sort the meetings by END time. Take a meeting if it starts AFTER the last taken meeting ended.
	// Why earliest END first (and not earliest start, or shortest) ? The meeting that ends first leaves the room free as
	// EARLY as possible, so it leaves the most time for the rest. Swapping it into any best plan never makes it worse.
	// e.g., earliest start fails: (0, 100), (1, 2), (3, 4): taking (0, 100) first allows only 1 meeting instead of 2.
	// T.C: O(n log n)
	// S.C: O(n)
	public List<int> SelectedMeetings(int[] start, int[] end)
	{
		int n = start.Length;
		int[] order = new int[n];
		for (int i = 0; i < n; i++) order[i] = i;
		Array.Sort(order, (a, b) => end[a].CompareTo(end[b]));

		List<int> chosen = new List<int>();   // 1-based meeting numbers
		int lastEnd = int.MinValue;
		foreach (int i in order)
		{
			if (start[i] > lastEnd)   // strictly after (the problem does not allow touching)
			{
				chosen.Add(i + 1);
				lastEnd = end[i];
			}
		}
		chosen.Sort();
		return chosen;
	}

	// GFG asks only for the count.
	public int MaxMeetings(int[] start, int[] end) => SelectedMeetings(start, end).Count;
}


class Program
{
	static void Main()
	{
		int[] start = { 1, 3, 0, 5, 8, 5 }, end = { 2, 4, 6, 7, 9, 9 };

		Console.WriteLine(string.Join(" ", new Solution().SelectedMeetings(start, end)));   // o/p: 1 2 4 5
		Console.WriteLine(new Solution().MaxMeetings(start, end));                          // o/p: 4
	}
}

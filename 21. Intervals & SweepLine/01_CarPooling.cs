// Sweep Line (Line Sweep)   --->   A way to handle many INTERVALS on a line (time, distance ...). Instead of looking at
// every interval against every other one, turn each interval [start, end) into 2 EVENTS:
//   at 'start':  +something   (it begins)
//   at 'end':    -something   (it is over)
// Then walk ("sweep") along the line in sorted order, adding up the changes. The running total at any point is how much
// is active there (bookings, passengers, trains ...).
//
//   Interval A: [1, 5) +2      Interval B: [3, 7) +3
//   Events: 1:+2   3:+3   5:-2   7:-3
//   Running total:   from 1: 2,   from 3: 5,   from 5: 3,   from 7: 0      -> the busiest moment has 5
//
// ● The events must be visited in sorted order: sort them, or keep them in a SortedDictionary (a C# sorted map). When the
//   positions are small whole numbers, a plain array indexed by position works too (a "difference array"), with no sort.
// ● '20. Greedy Algorithm/10_MinimumPlatforms.cs' uses the same idea with two sorted lists.

// Leetcode: 1094. Car Pooling   --->   A car with 'capacity' seats only drives east. trips[i] = [passengers, from, to]:
// pick up 'passengers' at km 'from', drop them at km 'to'. Return true if every trip fits without going over capacity.

// Example 1:
// Input: trips = [[2,1,5],[3,3,7]], capacity = 4
// Output: false   (between km 3 and 5 there are 2 + 3 = 5 passengers)

// Example 2:
// Input: trips = [[2,1,5],[3,3,7]], capacity = 5
// Output: true

// Note: Passengers are dropped at 'to' BEFORE new ones get in there (the interval is [from, to)), so [2,1,5] and
//       [3,5,7] never share seats. Adding all the changes of one location together handles that automatically.


using System;
using System.Collections.Generic;

public class Solution
{
	// I. Sweep with a sorted map (from the C++ code): changes[location] += passengers at 'from', -= at 'to'. Walk the
	//    locations in order, keeping the running number of passengers; if it ever goes over capacity, false.
	// T.C: O(n log n);   n trips, SortedDictionary operations cost O(log n)
	// S.C: O(n)
	public bool CarPooling_SortedMap(int[][] trips, int capacity)
	{
		SortedDictionary<int, int> changes = new SortedDictionary<int, int>();
		foreach (int[] t in trips)
		{
			changes[t[1]] = changes.GetValueOrDefault(t[1]) + t[0];   // they get in
			changes[t[2]] = changes.GetValueOrDefault(t[2]) - t[0];   // they get out
		}

		int inCar = 0;
		foreach (int change in changes.Values)   // SortedDictionary gives the keys in increasing order
		{
			inCar += change;
			if (inCar > capacity) return false;
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// II. Difference array (the locations are small: 0 <= from < to <= 1000): an array of 1001 changes replaces the
	//     sorted map. No sorting at all.
	// T.C: O(n + L);   L = the largest location (1000)
	// S.C: O(L)
	public bool CarPooling(int[][] trips, int capacity)
	{
		int maxLocation = 0;
		foreach (int[] t in trips) maxLocation = Math.Max(maxLocation, t[2]);

		int[] changes = new int[maxLocation + 1];
		foreach (int[] t in trips)
		{
			changes[t[1]] += t[0];
			changes[t[2]] -= t[0];
		}

		int inCar = 0;
		foreach (int change in changes)
		{
			inCar += change;
			if (inCar > capacity) return false;
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		int[][] trips = { new[] { 2, 1, 5 }, new[] { 3, 3, 7 } };

		Console.WriteLine(new Solution().CarPooling_SortedMap(trips, 4));   // o/p: False
		Console.WriteLine(new Solution().CarPooling(trips, 4));             // o/p: False
	}
}

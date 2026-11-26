// GFG: Minimum Platforms   --->   arr[i] and dep[i] are the arrival and departure times of train i (same day, HHMM).
// Return the minimum number of platforms so that no train waits. A platform can NOT be used by a train arriving at the
// same moment another one departs from it (so equal times need 2 platforms).

// Example 1:
// Input: arr = [900, 940, 950, 1100, 1500, 1800], dep = [910, 1200, 1120, 1130, 1900, 2000]
// Output: 3   (from 11:00 to 11:20 three trains are at the station)

// Example 2:
// Input: arr = [900, 1235, 1100], dep = [1000, 1240, 1200]
// Output: 1

// Problem link: https://www.geeksforgeeks.org/problems/minimum-platforms-1587115620/1


using System;

public class Solution
{
	// Algorithm (sweep through time): The answer is the MOST trains at the station at the same moment.
	// Sort the arrivals and the departures SEPARATELY (which train leaves does not matter, only when). Walk both lists in
	// time order with two pointers, like a merge:
	//   next arrival <= next departure: a train comes in first -> trains++, move to the next arrival.
	//                                   ('<=': on a tie the arrival counts first, because it can not share the platform)
	//   otherwise:                      a train leaves first   -> trains--, move to the next departure.
	// The biggest 'trains' value seen is the answer.
	// e.g., events: 900A 910D 940A 950A 1100A 1120D ... -> counts 1, 0, 1, 2, 3, 2 ... -> max 3.
	// T.C: O(n log n)
	// S.C: O(n);   sorted copies
	public int FindPlatform(int[] arr, int[] dep)
	{
		int[] a = (int[])arr.Clone(), d = (int[])dep.Clone();
		Array.Sort(a);
		Array.Sort(d);

		int i = 0, j = 0, trains = 0, best = 0;
		while (i < a.Length)   // after the last arrival, the count can only go down
		{
			if (a[i] <= d[j])
			{
				trains++;
				i++;
				best = Math.Max(best, trains);
			}
			else
			{
				trains--;
				j++;
			}
		}
		return best;
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 900, 940, 950, 1100, 1500, 1800 }, dep = { 910, 1200, 1120, 1130, 1900, 2000 };

		Console.WriteLine(new Solution().FindPlatform(arr, dep));   // o/p: 3
	}
}

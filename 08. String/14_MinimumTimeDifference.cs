// Leetcode: 539. Minimum Time Difference   --->   Given a list of 24-hour clock time points in "HH:MM" format, return
// the minimum minutes difference between any two time-points in the list.

// Example 1:
// Input: timePoints = ["23:59","00:00"]
// Output: 1

// Example 2:
// Input: timePoints = ["00:00","23:59","00:00"]
// Output: 0

// Constraints: 2 <= timePoints.length <= 2 * 10^4

// Step for both approaches: Convert "HH:MM" into minutes from midnight: HH * 60 + MM. e.g., "23:59" -> 1439.
// The clock is a circle: after 23:59 comes 00:00. So the gap between the last and first time is
// (1440 - last + first), where 1440 = minutes in a day. e.g., 23:59 and 00:00 -> 1440 - 1439 + 0 = 1 minute.


using System;

public class Solution
{
	// "HH:MM" -> minutes. (c - '0') turns a digit character into its number.
	private int ToMinutes(string t)
	{
		int hours = (t[0] - '0') * 10 + (t[1] - '0');
		int minutes = (t[3] - '0') * 10 + (t[4] - '0');
		return hours * 60 + minutes;
	}

	// Sorting: The smallest gap is always between 2 neighbours after sorting (or between the last and the first, around
	// midnight).
	// T.C: O(nlogn)
	// S.C: O(n);   for the minutes array
	public int FindMinDifference_Sorting(string[] timePoints)
	{
		int n = timePoints.Length;
		int[] mins = new int[n];
		for (int i = 0; i < n; i++) mins[i] = ToMinutes(timePoints[i]);

		Array.Sort(mins);

		int ans = 1440 - mins[n - 1] + mins[0];   // gap around midnight
		for (int i = 1; i < n; i++)
		{
			ans = Math.Min(ans, mins[i] - mins[i - 1]);
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Bucket (no sorting needed).
	// There are only 1440 possible times in a day. So keep a bool array 'seen' of size 1440, mark every time, and then walk
	// through it from 0 to 1439. The marked times come out in sorted order automatically.
	// Shortcut: If there are more than 1440 times, two of them must be the same (pigeonhole principle). So the answer is 0.
	//           Also, if a time is already marked, it's a duplicate. Answer 0.
	// T.C: O(n + 1440) = O(n)
	// S.C: O(1440) = O(1)
	public int FindMinDifference(string[] timePoints)
	{
		if (timePoints.Length > 1440) return 0;

		bool[] seen = new bool[1440];
		foreach (string t in timePoints)
		{
			int m = ToMinutes(t);
			if (seen[m]) return 0;   // same time twice
			seen[m] = true;
		}

		int first = -1, prev = -1, ans = int.MaxValue;
		for (int m = 0; m < 1440; m++)
		{
			if (!seen[m]) continue;

			if (first == -1) first = m;
			else ans = Math.Min(ans, m - prev);
			prev = m;
		}

		return Math.Min(ans, 1440 - prev + first);   // 'prev' is now the last time, check the gap around midnight
	}
}


class Program
{
	static void Main()
	{
		string[] timePoints = { "23:59", "00:00" };

		Console.WriteLine(new Solution().FindMinDifference_Sorting(timePoints));   // o/p: 1
		Console.WriteLine(new Solution().FindMinDifference(timePoints));           // o/p: 1
	}
}

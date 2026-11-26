// Leetcode: 731. My Calendar II   --->   Implement a calendar where Book(start, end) adds the event [start, end) (start
// included, end not) and returns true, UNLESS it would create a TRIPLE booking (some moment shared by 3 events). Then it
// returns false and does not add the event. Double bookings are allowed.

// Example:
// Input:  ["MyCalendarTwo","book","book","book","book","book","book"]
//         [[],[10,20],[50,60],[10,40],[5,15],[5,10],[25,55]]
// Output: [null,true,true,true,false,true,true]
// Explanation:
//   [10,20) and [50,60): fine.   [10,40): double booked on [10,20), fine.
//   [5,15): [10,15) would be triple booked -> false.
//   [5,10): touches 10 but does not include it -> fine.
//   [25,55): double on [25,40) and [50,55), never triple -> fine.

// First understand '01_CarPooling.cs' (sweep line).


using System;
using System.Collections.Generic;

// I. Two lists (from the C++ code):
//    'bookings' = every accepted event.   'doubles' = every DOUBLE-booked part.
//    Book(start, end):
//    1. If it overlaps any double-booked part, that moment would be triple booked: false.
//    2. Otherwise, every overlap with an accepted event becomes a new double-booked part. Add the event; true.
//    Two intervals [a, b) and [c, d) overlap exactly when max(a, c) < min(b, d); that overlap is [max(a, c), min(b, d)).
// T.C: O(n) per Book;   n = events so far. The double-booked parts never overlap each other (that would be a triple
//                       booking, which is never allowed in), so there are at most O(n) of them.
// S.C: O(n)
// Note: The C++ comment said O(n^2) per call. The argument above shows the overlap list stays O(n), so a call is O(n),
//       and n calls are O(n^2) in total.
public class MyCalendarTwo_Lists
{
	private List<(int start, int end)> bookings = new List<(int start, int end)>();
	private List<(int start, int end)> doubles = new List<(int start, int end)>();

	public bool Book(int start, int end)
	{
		foreach (var (s, e) in doubles)
		{
			if (Math.Max(start, s) < Math.Min(end, e)) return false;   // would be triple booked
		}

		foreach (var (s, e) in bookings)
		{
			int overlapStart = Math.Max(start, s), overlapEnd = Math.Min(end, e);
			if (overlapStart < overlapEnd) doubles.Add((overlapStart, overlapEnd));
		}

		bookings.Add((start, end));
		return true;
	}
}

// II. Sweep line: keep a SortedDictionary {time -> change}: +1 at every start, -1 at every end.
//     Book(start, end): add the event's +1 / -1, then sweep the timeline adding up the changes. If the running count ever
//     reaches 3, there is a triple booking: UNDO the +1 / -1 and return false. Otherwise keep it.
// T.C: O(n) per Book;   the sweep walks all 2n points (the dictionary updates are O(log n))
// S.C: O(n)
// This one is easy to change for "at most k bookings" (Leetcode 732 asks for the biggest count): just change the 3.
public class MyCalendarTwo
{
	private SortedDictionary<int, int> timeline = new SortedDictionary<int, int>();

	private void Add(int time, int change) => timeline[time] = timeline.GetValueOrDefault(time) + change;

	public bool Book(int start, int end)
	{
		Add(start, +1);
		Add(end, -1);

		int active = 0;
		foreach (int change in timeline.Values)
		{
			active += change;
			if (active >= 3)
			{
				Add(start, -1);   // undo
				Add(end, +1);
				return false;
			}
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		int[][] events =
		{
			new[] { 10, 20 }, new[] { 50, 60 }, new[] { 10, 40 }, new[] { 5, 15 }, new[] { 5, 10 }, new[] { 25, 55 }
		};

		MyCalendarTwo_Lists lists = new MyCalendarTwo_Lists();
		MyCalendarTwo sweep = new MyCalendarTwo();
		List<bool> a = new List<bool>(), b = new List<bool>();
		foreach (int[] e in events)
		{
			a.Add(lists.Book(e[0], e[1]));
			b.Add(sweep.Book(e[0], e[1]));
		}

		Console.WriteLine(string.Join(" ", a));   // o/p: True True True False True True
		Console.WriteLine(string.Join(" ", b));   // o/p: True True True False True True
	}
}

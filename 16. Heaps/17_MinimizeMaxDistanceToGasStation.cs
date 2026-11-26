// GFG: Minimize Max Distance to Gas Station   --->   Gas stations stand on a number line at positions stations[0],
// stations[1], ..., stations[n-1] (strictly increasing). We add k more stations anywhere (not only at whole numbers),
// so that d, the MAXIMUM distance between two neighbouring stations, is as small as possible. Return the smallest
// possible d, to 2 decimal places.

// Example 1:
// Input: stations = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10], k = 9
// Output: 0.50   (one new station in the middle of each of the 9 gaps)

// Example 2:
// Input: stations = [3, 6, 12, 19, 33, 44, 67, 72, 89, 95], k = 2
// Output: 14.00   (one station in the gap 44..67 (23 -> 11.5 + 11.5), one in 72..89 (17 -> 8.5 + 8.5). The biggest
//                 gap left is 19..33 = 14.)

// Problem link: https://www.geeksforgeeks.org/problems/minimize-max-distance-to-gas-station/1

// ● Key idea: We never need the exact positions. If a gap of length L gets c new stations, spread evenly, it becomes
//   (c + 1) equal parts of length L / (c + 1). So we only track howMany[i] = how many stations went into gap i.
// ● Greedy: Put each new station into the gap whose parts are CURRENTLY the longest. (Adding anywhere else can not
//   lower the maximum.)


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: k times, scan all n - 1 gaps to find the longest part, and add a station there.
	// T.C: O(k * n)
	// S.C: O(n);   howMany
	public double FindSmallestMaxDist_BruteForce(int[] stations, int k)
	{
		int n = stations.Length;
		int[] howMany = new int[n - 1];

		for (int added = 0; added < k; added++)
		{
			double maxPart = -1;
			int maxIndex = -1;
			for (int i = 0; i < n - 1; i++)
			{
				double part = (double)(stations[i + 1] - stations[i]) / (howMany[i] + 1);
				if (part > maxPart)
				{
					maxPart = part;
					maxIndex = i;
				}
			}
			howMany[maxIndex]++;
		}

		double answer = 0;
		for (int i = 0; i < n - 1; i++)
		{
			answer = Math.Max(answer, (double)(stations[i + 1] - stations[i]) / (howMany[i] + 1));
		}
		return answer;
	}

// ------------------------------------------------------------------------------

	// Better (max heap): The "scan all gaps for the longest part" step is exactly what a max heap does in O(log n).
	// Keep every gap in a max heap, with its current part length as the priority. k times: take the top gap, give it one
	// more station, compute its new part length, and put it back. In the end the top is the answer.
	// T.C: O(n + k log n);   building the heap is O(n)
	// S.C: O(n)
	// Note: The C++ code pushed the gaps one by one: O(n log n). Building from a list is O(n).
	public double FindSmallestMaxDist_Heap(int[] stations, int k)
	{
		int n = stations.Length;
		int[] howMany = new int[n - 1];

		List<(int, double)> gaps = new List<(int, double)>();
		for (int i = 0; i < n - 1; i++) gaps.Add((i, stations[i + 1] - stations[i]));

		var longestFirst = Comparer<double>.Create((a, b) => b.CompareTo(a));
		PriorityQueue<int, double> maxHeap = new PriorityQueue<int, double>(gaps, longestFirst);

		for (int added = 0; added < k; added++)
		{
			int i = maxHeap.Dequeue();
			howMany[i]++;
			maxHeap.Enqueue(i, (double)(stations[i + 1] - stations[i]) / (howMany[i] + 1));
		}

		maxHeap.TryPeek(out _, out double answer);
		return answer;
	}

// ------------------------------------------------------------------------------

	// Optimal (Binary search on the answer): Turn the question around: "Given a distance d, how many new stations do
	// we need so that no part is longer than d ?"
	//   A gap of length L needs ceil(L / d) parts, i.e., ceil(L / d) - 1 new stations.
	// The bigger d is, the fewer stations are needed. So binary search for the SMALLEST d that needs <= k stations
	// (like the "binary search on answers" problems in '06. Searching').
	// d is a real number, so search until the range is tiny (1e-6), not until low > high.
	// T.C: O(n * log(maxGap / 1e-6));   about 40 rounds of O(n) each, and it does not depend on k
	// S.C: O(1)
	public double FindSmallestMaxDist(int[] stations, int k)
	{
		double low = 0, high = 0;
		for (int i = 0; i < stations.Length - 1; i++) high = Math.Max(high, stations[i + 1] - stations[i]);

		while (high - low > 1e-6)
		{
			double mid = (low + high) / 2;
			if (StationsNeeded(stations, mid) <= k) high = mid;   // mid works, try smaller
			else low = mid;                                       // mid is too small
		}
		return high;
	}

	private long StationsNeeded(int[] stations, double d)
	{
		long count = 0;
		for (int i = 0; i < stations.Length - 1; i++)
		{
			count += (long)Math.Ceiling((stations[i + 1] - stations[i]) / d) - 1;
		}
		return count;
	}
}


class Program
{
	static void Main()
	{
		int[] stations = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

		Console.WriteLine(new Solution().FindSmallestMaxDist_BruteForce(stations, 9).ToString("F2"));   // o/p: 0.50
		Console.WriteLine(new Solution().FindSmallestMaxDist_Heap(stations, 9).ToString("F2"));         // o/p: 0.50
		Console.WriteLine(new Solution().FindSmallestMaxDist(stations, 9).ToString("F2"));              // o/p: 0.50
	}
}

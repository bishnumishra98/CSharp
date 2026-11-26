// GFG: Aggressive Cows   --->   You are given an array with unique elements of stalls[], which denote the positions of
// stalls. You are also given an integer k which denotes the number of aggressive cows. Your task is to assign stalls to
// k cows such that the minimum distance between any two of them is the maximum possible.

// Example 1:
// Input: stalls[] = [1, 2, 4, 8, 9], k = 3
// Output: 3
// Explanation: The first cow can be placed at stalls[0], the second cow at stalls[2] and the third cow at stalls[3].
// The minimum distance between cows, in this case, is 3, which also is the largest among all possible ways.

// Example 2:
// Input: stalls[] = [10, 1, 2, 7, 5], k = 3
// Output: 4
// Explanation: The first cow can be placed at stalls[0], the second cow at stalls[1] and the third cow at stalls[4].
// The minimum distance between cows, in this case, is 4, which also is the largest among all possible ways.

// Example 3:
// Input: stalls[] = [2, 12, 11, 3, 26, 7], k = 5
// Output: 1
// Explanation: There are 6 stalls and only 5 cows. The minimum distance between cows in this case is 1, which is the
// largest among all possible ways.

// Problem link: https://www.geeksforgeeks.org/problems/aggressive-cows/1

// Understand the problem: Cows fight when they are close. So we want to keep them as far apart as possible, i.e.,
// make the SMALLEST gap between any 2 cows as BIG as possible.
// Search space of the answer (the minimum gap):
// ● Smallest possible gap = 1
// ● Biggest possible gap  = stalls[n - 1] - stalls[0] (after sorting), i.e., one cow on the first stall, one on the last.


using System;

public class Solution
{
	// Can we place 'k' cows so that every 2 cows are at least 'minDistance' apart ?
	// Greedy: Put the first cow on the first stall (the earlier we start, the more room is left for others).
	// Then go stall by stall, and put the next cow on the first stall that is at least 'minDistance' away from the
	// last placed cow. In the end, if we placed k or more cows, return true.
	private bool CanWePlace(int[] stalls, int minDistance, int k)
	{
		int cows = 1;
		int last = stalls[0];   // position of the last placed cow

		for (int i = 1; i < stalls.Length; i++)
		{
			if (stalls[i] - last >= minDistance)
			{
				cows++;
				last = stalls[i];
			}
		}

		return cows >= k;
	}

	// Brute force: Linear search. Sort the stalls, and try every gap from 1 to (max - min).
	// The moment a gap 'd' doesn't work, the answer is d - 1 (the last gap that worked).
	// T.C: O(nlogn) + O((max - min) * n);   sorting + trying every gap
	// S.C: O(1)
	public int AggressiveCows_BruteForce(int[] stalls, int k)
	{
		if (k > stalls.Length) return -1;   // more cows than stalls

		Array.Sort(stalls);
		int maxGap = stalls[stalls.Length - 1] - stalls[0];

		for (int d = 1; d <= maxGap; d++)
		{
			if (!CanWePlace(stalls, d, k)) return d - 1;
		}

		return maxGap;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search on answer (gap).
	// If gap 'd' works, every smaller gap also works. So the pattern is sorted:
	// gap:     1  2  3  4  5 ...
	// works?   Y  Y  Y  N  N ...    ->  We need the LAST Y.
	// 1. Sort the stalls. start = 1, end = stalls[n - 1] - stalls[0]. Find mid (a gap):
	//    -> If CanWePlace(mid): mid works. Store ans = mid, and try a bigger gap: start = mid + 1.
	//    -> Else: mid is too big. Try a smaller gap: end = mid - 1.
	// T.C: O(nlogn) + O(log(max - min) * n);   sorting + binary search
	// S.C: O(1)
	public int AggressiveCows(int[] stalls, int k)
	{
		if (k > stalls.Length) return -1;

		Array.Sort(stalls);
		int start = 1, end = stalls[stalls.Length - 1] - stalls[0];

		int ans = 0;
		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (CanWePlace(stalls, mid, k))
			{
				ans = mid;
				start = mid + 1;
			}
			else end = mid - 1;
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] stalls = { 1, 2, 4, 8, 9 };
		int k = 3;

		Console.WriteLine(new Solution().AggressiveCows_BruteForce(stalls, k));   // o/p: 3
		Console.WriteLine(new Solution().AggressiveCows(stalls, k));              // o/p: 3
	}
}

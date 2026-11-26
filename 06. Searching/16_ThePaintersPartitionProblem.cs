// GFG: The Painter's Partition Problem-II   --->   Dilpreet wants to paint his dog's home that has n boards with
// different lengths. The length of ith board is given by arr[i] where arr[] is an array of n integers. He hired k
// painters for this work and each painter takes 1 unit time to paint 1 unit of the board.
// Return the minimum time to get this job done if all painters start together with the constraint that any painter
// will only paint continuous boards, say boards numbered [2,3,4] or only board [1] or nothing but not boards [2,4,5].

// Example 1:
// Input: arr[] = [5, 10, 30, 20, 15], k = 3
// Output: 35
// Explanation: The most optimal way will be: Painter 1 allocation : [5,10], Painter 2 allocation : [30],
// Painter 3 allocation : [20,15], Job will be done when all painters finish i.e. at time = max(5+10, 30, 20+15) = 35

// Example 2:
// Input: arr[] = [10, 20, 30, 40], k = 2
// Output: 60
// Explanation: The most optimal way to paint: Painter 1 allocation : [10,20,30], Painter 2 allocation : [40],
// Job will be complete at time = 60

// Example 3:
// Input: arr[] = [100, 200, 300, 400], k = 1
// Output: 1000
// Explanation: There is only one painter, so the painter must paint all boards sequentially. The total time taken
// will be the sum of all board lengths, i.e., 100 + 200 + 300 + 400 = 1000.

// Problem link: https://www.geeksforgeeks.org/problems/the-painters-partition-problem1535/1

// This is the same problem as '15_AllocateMinimumPages.cs':  books -> boards,  students -> painters,  pages -> time.
// Only 1 difference: here a painter can paint nothing. So if k > number of boards, it's NOT -1. Each board gets its
// own painter, and the answer is the longest board, i.e., max(arr).


using System;

public class Solution
{
	// Can all boards be painted by at most 'k' painters, if no painter can work more than 'maxTime' ?
	// Same greedy as IsPossible() in '15_AllocateMinimumPages.cs'.
	private bool IsPossible(int[] arr, long maxTime, int k)
	{
		int painters = 1;
		long time = 0;   // time taken by the current painter

		for (int i = 0; i < arr.Length; i++)
		{
			if (time + arr[i] <= maxTime) time += arr[i];
			else
			{
				painters++;
				time = arr[i];
			}
		}

		return painters <= k;
	}

	// Binary search on answer (time).
	// 1. start = max(arr) (the longest board), end = sum(arr) (one painter paints everything).
	// 2. If IsPossible(mid): store ans = mid, try less time: end = mid - 1. Else, try more time: start = mid + 1.
	// Note: With k >= n, the binary search also returns max(arr), so no special check is needed.
	// T.C: O(log(sum - max) * n);   where n = arr.Length
	// S.C: O(1)
	public long MinTime(int[] arr, int k)
	{
		long start = 0, end = 0;   // long, because the sum of all boards can be big
		for (int i = 0; i < arr.Length; i++)
		{
			start = Math.Max(start, arr[i]);
			end += arr[i];
		}

		long ans = end;
		while (start <= end)
		{
			long mid = start + (end - start) / 2;

			if (IsPossible(arr, mid, k))
			{
				ans = mid;
				end = mid - 1;
			}
			else start = mid + 1;
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 5, 10, 30, 20, 15 };
		int k = 3;

		Console.WriteLine(new Solution().MinTime(arr, k));   // o/p: 35
	}
}

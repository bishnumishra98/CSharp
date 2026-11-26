// GFG: Job Sequencing Problem   --->   Each job takes 1 unit of time and has a deadline and a profit. Only one job runs at
// a time, and a job pays its profit only if it FINISHES by its deadline. Return [number of jobs done, maximum profit].

// Example 1:
// Input: deadline = [4, 1, 1, 1], profit = [20, 1, 40, 30]
// Output: [2, 60]   (the job with profit 40 at time 1, the one with 20 at any time up to 4)

// Example 2:
// Input: deadline = [2, 1, 2, 1, 1], profit = [100, 19, 27, 25, 15]
// Output: [2, 127]  (100 + 27)

// Example 3:
// Input: deadline = [3, 1, 2, 2], profit = [50, 10, 20, 30]
// Output: [3, 100]  (50 + 20 + 30)

// Problem link: https://www.geeksforgeeks.org/problems/job-sequencing-problem-1587115620/1


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	// Better (from the C++ code): Take jobs from the MOST profitable down. Put each one in the LATEST free time slot on or
	// before its deadline (so the earlier slots stay free for jobs with earlier deadlines). If no slot is free, skip it.
	// T.C: O(n log n + n * D);   D = the biggest deadline: each job may scan back over D slots
	// S.C: O(D)
	// Note: The C++ comment said O(n log n). The backwards scan for a free slot makes it O(n * D) in the worst case.
	//       It also made n + 1 slots, which breaks if a deadline is bigger than n; here the slots go up to the biggest
	//       deadline.
	public int[] JobSequencing_Slots(int[] deadline, int[] profit)
	{
		int n = deadline.Length, maxDeadline = deadline.Max();
		int[] order = Enumerable.Range(0, n).OrderByDescending(i => profit[i]).ToArray();
		bool[] used = new bool[maxDeadline + 1];   // slots 1..maxDeadline

		int jobs = 0, total = 0;
		foreach (int i in order)
		{
			for (int t = deadline[i]; t >= 1; t--)
			{
				if (used[t]) continue;
				used[t] = true;
				jobs++;
				total += profit[i];
				break;
			}
		}
		return new[] { jobs, total };
	}

// ------------------------------------------------------------------------------

	// Optimal (min-heap): Go through the jobs by DEADLINE (earliest first), and keep the jobs we plan to do in a min-heap
	// of profits.
	//   Add each job. If the heap now has MORE jobs than the deadline allows (heap size > deadline, since every job takes
	//   one time unit), drop the LEAST profitable job in the heap.
	// The heap always holds the best set of jobs that can all finish on time so far.
	// e.g., Example 1 by deadline: (1,1) (1,40) (1,30) (4,20). Add 1. Add 40: size 2 > 1, drop 1. Add 30: drop 30.
	//       Add 20: size 2 <= 4. Heap {40, 20}: 2 jobs, profit 60.
	// T.C: O(n log n)
	// S.C: O(n)
	public int[] JobSequencing(int[] deadline, int[] profit)
	{
		int n = deadline.Length;
		int[] order = Enumerable.Range(0, n).OrderBy(i => deadline[i]).ToArray();

		PriorityQueue<int, int> heap = new PriorityQueue<int, int>();   // profits, smallest on top
		foreach (int i in order)
		{
			heap.Enqueue(profit[i], profit[i]);
			if (heap.Count > deadline[i]) heap.Dequeue();   // too many jobs for this deadline: drop the cheapest
		}

		int total = 0;
		foreach (var (p, _) in heap.UnorderedItems) total += p;
		return new[] { heap.Count, total };
	}
}


class Program
{
	static void Main()
	{
		int[] deadline = { 4, 1, 1, 1 }, profit = { 20, 1, 40, 30 };

		Console.WriteLine(string.Join(" ", new Solution().JobSequencing_Slots(deadline, profit)));   // o/p: 2 60
		Console.WriteLine(string.Join(" ", new Solution().JobSequencing(deadline, profit)));         // o/p: 2 60
	}
}

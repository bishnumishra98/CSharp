// GFG: Shortest Job First   --->   bt[i] is how long process i takes to run. All processes are ready at time 0, and one
// CPU runs them one after another, always picking the SHORTEST job next. Return the AVERAGE waiting time (time spent
// before starting), rounded down.

// Example 1:
// Input: bt = [4, 3, 7, 1, 2]
// Output: 4   (order 1, 2, 3, 4, 7. Waits: 0, 1, 3, 6, 10 = 20. 20 / 5 = 4.)

// Example 2:
// Input: bt = [1, 2, 3, 4]
// Output: 2   (waits 0, 1, 3, 6 = 10. 10 / 4 = 2.5 -> 2)

// Problem link: https://www.geeksforgeeks.org/problems/shortest-job-first/1


using System;

public class Solution
{
	// Algorithm: Sort, then walk: each job waits for the total time of all jobs before it.
	// Why does shortest-first give the smallest total wait ? A job's run time is added to the wait of EVERY job after it.
	// Putting short jobs first means the small numbers are the ones repeated many times.
	// T.C: O(n log n)
	// S.C: O(1)   (a sorted copy is made so the caller's array is not changed)
	public long Solve(int[] bt)
	{
		int[] jobs = (int[])bt.Clone();
		Array.Sort(jobs);

		long totalWait = 0, clock = 0;
		foreach (int t in jobs)
		{
			totalWait += clock;   // this job waited until now
			clock += t;
		}
		return totalWait / jobs.Length;   // integer division rounds down (all values are >= 0)
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().Solve(new int[] { 4, 3, 7, 1, 2 }));   // o/p: 4
	}
}

// GFG: Minimum Multiplications to Reach End   --->   Given start, end and an array arr. In one step, multiply the current
// number by any arr[i] and take the result modulo 100000. Return the fewest steps to turn start into end, or -1.

// Example 1:
// Input: arr = [2, 5, 7], start = 3, end = 30
// Output: 2   (3 * 2 = 6,  6 * 5 = 30)

// Example 2:
// Input: arr = [3, 4, 65], start = 7, end = 66175
// Output: 4   (7 -> 21 -> 63 -> 4095 -> 266175 % 100000 = 66175)

// Problem link: https://www.geeksforgeeks.org/problems/minimum-multiplications-to-reach-end/1


using System;
using System.Collections.Generic;

public class Solution
{
	private const int MOD = 100000;

	// Algorithm: An IMPLICIT graph. The nodes are the numbers 0..99999; from x there is an edge to (x * a) % MOD for every
	// a in arr. Every step costs 1, so the fewest steps = BFS (unit-weight shortest path).
	// T.C: O(MOD * arr.Length);   each of the 100000 numbers is processed at most once
	// S.C: O(MOD)
	// Note: Two fixes from the C++ code:
	//   1. node * arr[i] can be up to about 10^5 * 10^5 = 10^10, which overflows an int (the C++ used int). Use long.
	//   2. If start == end the answer is 0. The C++ code only checked 'end' for NEW numbers, so it never returned 0.
	public int MinimumMultiplications(int[] arr, int start, int end)
	{
		if (start == end) return 0;

		int[] steps = new int[MOD];
		Array.Fill(steps, -1);   // -1 = not reached
		steps[start] = 0;

		Queue<int> q = new Queue<int>();
		q.Enqueue(start);
		while (q.Count > 0)
		{
			int x = q.Dequeue();
			foreach (int a in arr)
			{
				int next = (int)((long)x * a % MOD);
				if (steps[next] != -1) continue;

				steps[next] = steps[x] + 1;
				if (next == end) return steps[next];
				q.Enqueue(next);
			}
		}
		return -1;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().MinimumMultiplications(new int[] { 3, 4, 65 }, 7, 66175));   // o/p: 4
	}
}

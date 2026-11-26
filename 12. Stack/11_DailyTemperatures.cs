// Leetcode: 739. Daily Temperatures   --->   Given an array of integers temperatures represents the daily temperatures,
// return an array answer such that answer[i] is the number of days you have to wait after the ith day to get a warmer
// temperature. If there is no future day for which this is possible, keep answer[i] == 0 instead.

// Example 1:
// Input: temperatures = [73,74,75,71,69,72,76,73]
// Output: [1,1,4,2,1,1,0,0]

// Example 2:
// Input: temperatures = [30,40,50,60]
// Output: [1,1,1,0]

// Example 3:
// Input: temperatures = [30,60,90]
// Output: [1,1,0]

// This is the Next Greater Element ('07_NextAndPreviousSmallerOrGreaterElement.cs'), but we need HOW FAR it is, not its
// value. So keep INDEXES in the stack: the answer is (index of the warmer day) - i.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: For every day, walk forward to the first warmer day.
	// T.C: O(n^2)
	// S.C: O(1);   ignoring the answer
	public int[] DailyTemperatures_BruteForce(int[] temperatures)
	{
		int n = temperatures.Length;
		int[] ans = new int[n];   // all 0 by default

		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				if (temperatures[j] > temperatures[i])
				{
					ans[i] = j - i;
					break;
				}
			}
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Monotonic stack of INDEXES, right to left.
	// 1. Pop every index whose temperature is <= today's (not warmer, and today is closer, so they are never needed again).
	// 2. If the stack is not empty, the top is the nearest warmer day: ans[i] = top - i. Else 0.
	// 3. Push i.
	// e.g., [73, 74, 75, 71, 69, 72, 76, 73] at i = 2 (75): the stack (top first) holds days 3 (71), 5 (72), 6 (76).
	//       Pop day 3 and day 5 (71 and 72 are not warmer than 75). Top = day 6 (76), so ans[2] = 6 - 2 = 4.
	// T.C: O(n)
	// S.C: O(n);   for the stack
	public int[] DailyTemperatures(int[] temperatures)
	{
		int n = temperatures.Length;
		int[] ans = new int[n];
		Stack<int> st = new Stack<int>();   // indexes

		for (int i = n - 1; i >= 0; i--)
		{
			while (st.Count > 0 && temperatures[st.Peek()] <= temperatures[i]) st.Pop();
			ans[i] = st.Count > 0 ? st.Peek() - i : 0;
			st.Push(i);
		}
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] temperatures = { 73, 74, 75, 71, 69, 72, 76, 73 };

		Console.WriteLine(string.Join(" ", new Solution().DailyTemperatures_BruteForce(temperatures)));   // o/p: 1 1 4 2 1 1 0 0
		Console.WriteLine(string.Join(" ", new Solution().DailyTemperatures(temperatures)));              // o/p: 1 1 4 2 1 1 0 0
	}
}

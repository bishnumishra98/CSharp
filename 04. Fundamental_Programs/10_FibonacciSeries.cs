// Leetcode: 509. Fibonacci Number   --->   The Fibonacci numbers, commonly denoted F(n) form a sequence, called the
// Fibonacci sequence, such that each number is the sum of the two preceding ones, starting from 0 and 1. That is,
// F(0) = 0, F(1) = 1
// F(n) = F(n - 1) + F(n - 2), for n > 1.
// Given n, calculate F(n).

// Example 1:
// Input: n = 2
// Output: 1
// Explanation: F(2) = F(1) + F(0) = 1 + 0 = 1.

// Example 2:
// Input: n = 4
// Output: 3
// Explanation: F(4) = F(3) + F(2) = 2 + 1 = 3.

// Extension: Print the first n numbers of the Fibonacci series.
// Input: n = 6
// Output: 0 1 1 2 3 5

//   n:     0  1  2  3  4  5  6   7   8   9
//   F(n):  0  1  1  2  3  5  8  13  21  34


using System;
using System.Collections.Generic;

public class Solution
{
	// Recursive approach: Write the formula directly. F(n) = F(n - 1) + F(n - 2), and stop at F(0) = 0, F(1) = 1.
	// Problem: It calculates the same values again and again. e.g., F(5) calls F(4) and F(3), and F(4) again calls
	//          F(3). The calls double at every level, so it is very slow for big n.
	//          (Storing already calculated values fixes this. That is "memoization", in the Dynamic Programming folder.)
	// T.C: O(2^n)
	// S.C: O(n);   recursion stack
	public int Fib_Recursive(int n)
	{
		if (n <= 1) return n;   // F(0) = 0, F(1) = 1
		return Fib_Recursive(n - 1) + Fib_Recursive(n - 2);
	}

// ------------------------------------------------------------------------------

	// Iterative approach: Keep only the last 2 numbers, and move forward.
	// 1. prev = F(0) = 0, curr = F(1) = 1.
	// 2. Repeat n - 1 times: next = prev + curr, then shift: prev = curr, curr = next.
	// e.g., n = 4:  (0, 1) -> (1, 1) -> (1, 2) -> (2, 3). F(4) = 3.
	// T.C: O(n)
	// S.C: O(1)
	public int Fib(int n)
	{
		if (n <= 1) return n;

		int prev = 0, curr = 1;
		for (int i = 2; i <= n; i++)
		{
			int next = prev + curr;
			prev = curr;
			curr = next;
		}

		return curr;
	}

// ------------------------------------------------------------------------------

	// Extension: First n numbers of the series, iteratively. Same idea as Fib(), but add every number to a list.
	// T.C: O(n)
	// S.C: O(n);   for the answer list
	public List<int> FibSeries_Iterative(int n)
	{
		List<int> series = new List<int>();
		int prev = 0, curr = 1;

		for (int i = 0; i < n; i++)
		{
			series.Add(prev);
			int next = prev + curr;
			prev = curr;
			curr = next;
		}

		return series;
	}

// ------------------------------------------------------------------------------

	// Extension: First n numbers of the series, recursively.
	// Pass the current 2 numbers (a, b) to the next call: add 'a', then call with (b, a + b) and 1 less number to print.
	// e.g., n = 3: add 0, call (1, 1) -> add 1, call (1, 2) -> add 1, n = 0, stop.  ->  0 1 1
	// T.C: O(n)
	// S.C: O(n);   recursion stack + answer list
	public List<int> FibSeries_Recursive(int n)
	{
		List<int> series = new List<int>();
		AddSeries(n, 0, 1, series);
		return series;
	}

	private void AddSeries(int n, int a, int b, List<int> series)
	{
		if (n <= 0) return;
		series.Add(a);
		AddSeries(n - 1, b, a + b, series);
	}
}


class Program
{
	static void Main()
	{
		int n = 4;

		Console.WriteLine(new Solution().Fib_Recursive(n));                                // o/p: 3
		Console.WriteLine(new Solution().Fib(n));                                          // o/p: 3
		Console.WriteLine(string.Join(" ", new Solution().FibSeries_Iterative(6)));        // o/p: 0 1 1 2 3 5
		Console.WriteLine(string.Join(" ", new Solution().FibSeries_Recursive(6)));        // o/p: 0 1 1 2 3 5
	}
}

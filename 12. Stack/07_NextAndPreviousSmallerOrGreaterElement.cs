// Next / Previous Smaller / Greater Element   --->   4 classic problems with ONE pattern (Monotonic Stack).
// For every element arr[i], find:
// I.   Next Smaller Element (NSE):     the first element to its RIGHT that is SMALLER than it.
// II.  Previous Smaller Element (PSE): the first element to its LEFT that is SMALLER than it.
// III. Next Greater Element (NGE):     the first element to its RIGHT that is GREATER than it.
// IV.  Previous Greater Element (PGE): the first element to its LEFT that is GREATER than it.
// If there is no such element, put -1.

// Example 1: arr = [8, 4, 6, 2, 3]
//   NSE:  4  2  2 -1 -1
//   PSE: -1 -1  4 -1  2
//   NGE: -1  6 -1  3 -1
//   PGE: -1  8  8  6  6

// Example 2: arr = [6, 8, 0, 1, 3]
//   NGE:  8 -1  1  3 -1
//   PGE: -1 -1  8  8  8

// Brute force (any of the 4): For every i, walk right (or left) from i until you find a smaller (or greater) element.
// T.C: O(n^2)

// Optimal: Monotonic stack. The stack keeps only the elements that can still be an answer for the elements we will see
// later. It stays sorted ("monotonic"), so the answer is always on top.
// For NEXT problems walk from RIGHT to LEFT (the answer is on the right, so we must have seen it already).
// For PREVIOUS problems walk from LEFT to RIGHT.
// At every element x:
//   1. Pop every element from the stack that can NOT be an answer for x (for "smaller": pop everything >= x;
//      for "greater": pop everything <= x).
//   2. Now the top (if any) is the answer for x. Empty stack means -1.
//   3. Push x (it may be the answer for elements that come later).
// Why can we pop forever ? Say we look for the next smaller element, walking right to left. If an element y on the
// right is >= x, then for any element further left, x is closer AND not bigger than y, so x is always a better (or equal)
// answer than y. y will never be needed again.
//
//   Problem   |  Walk direction   |  Pop while top ...   |  Stack (bottom -> top)
//   ------------------------------------------------------------------------------------
//   NSE       |  right to left    |  top >= x            |  increasing (small at bottom)
//   PSE       |  left to right    |  top >= x            |  increasing
//   NGE       |  right to left    |  top <= x            |  decreasing (big at bottom)
//   PGE       |  left to right    |  top <= x            |  decreasing
//
// T.C: O(n) for each;   every element is pushed once and popped at most once (the inner while loop runs at most n
//                       times in TOTAL, not n times for every element)
// S.C: O(n);   for the stack

// Note: The C++ code started the stack with -1 as a "fake bottom", and stopped popping when it reached -1. That only works
//       if every number is >= 0. Checking stack.Count > 0 instead works for negative numbers too.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force for the Next Smaller Element (the other 3 are the same with a different direction / comparison).
	// T.C: O(n^2)
	// S.C: O(1);   ignoring the answer
	public int[] NextSmaller_BruteForce(int[] arr)
	{
		int n = arr.Length;
		int[] ans = new int[n];

		for (int i = 0; i < n; i++)
		{
			ans[i] = -1;
			for (int j = i + 1; j < n; j++)
			{
				if (arr[j] < arr[i])
				{
					ans[i] = arr[j];
					break;
				}
			}
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// I. Next Smaller Element: right to left, pop while top >= x.
	// e.g., [8, 4, 6, 2, 3]:
	//       i=4 (3): stack []      -> -1,  push 3   [3]
	//       i=3 (2): pop 3         -> -1,  push 2   [2]
	//       i=2 (6): top 2 < 6     ->  2,  push 6   [2, 6]
	//       i=1 (4): pop 6, top 2  ->  2,  push 4   [2, 4]
	//       i=0 (8): top 4 < 8     ->  4,  push 8   [2, 4, 8]       Answer: 4 2 2 -1 -1
	public int[] NextSmaller(int[] arr)
	{
		int n = arr.Length;
		int[] ans = new int[n];
		Stack<int> st = new Stack<int>();

		for (int i = n - 1; i >= 0; i--)
		{
			while (st.Count > 0 && st.Peek() >= arr[i]) st.Pop();
			ans[i] = st.Count > 0 ? st.Peek() : -1;
			st.Push(arr[i]);
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// II. Previous Smaller Element: left to right, pop while top >= x. (Only the loop direction changed.)
	public int[] PreviousSmaller(int[] arr)
	{
		int n = arr.Length;
		int[] ans = new int[n];
		Stack<int> st = new Stack<int>();

		for (int i = 0; i < n; i++)
		{
			while (st.Count > 0 && st.Peek() >= arr[i]) st.Pop();
			ans[i] = st.Count > 0 ? st.Peek() : -1;
			st.Push(arr[i]);
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// III. Next Greater Element: right to left, pop while top <= x. (Only the comparison changed.)
	public int[] NextGreater(int[] arr)
	{
		int n = arr.Length;
		int[] ans = new int[n];
		Stack<int> st = new Stack<int>();

		for (int i = n - 1; i >= 0; i--)
		{
			while (st.Count > 0 && st.Peek() <= arr[i]) st.Pop();
			ans[i] = st.Count > 0 ? st.Peek() : -1;
			st.Push(arr[i]);
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// IV. Previous Greater Element: left to right, pop while top <= x.
	public int[] PreviousGreater(int[] arr)
	{
		int n = arr.Length;
		int[] ans = new int[n];
		Stack<int> st = new Stack<int>();

		for (int i = 0; i < n; i++)
		{
			while (st.Count > 0 && st.Peek() <= arr[i]) st.Pop();
			ans[i] = st.Count > 0 ? st.Peek() : -1;
			st.Push(arr[i]);
		}
		return ans;
	}
}

// Variation: Many problems need the INDEX of the answer, not its value (e.g., '11_DailyTemperatures.cs' and
// '15_LargestRectangleInHistogram.cs'). Then push indexes i instead of values, and compare arr[st.Peek()] with arr[i].


class Program
{
	static void Main()
	{
		int[] arr = { 8, 4, 6, 2, 3 };

		Console.WriteLine(string.Join(" ", new Solution().NextSmaller_BruteForce(arr)));   // o/p: 4 2 2 -1 -1
		Console.WriteLine(string.Join(" ", new Solution().NextSmaller(arr)));              // o/p: 4 2 2 -1 -1
		Console.WriteLine(string.Join(" ", new Solution().PreviousSmaller(arr)));          // o/p: -1 -1 4 -1 2
		Console.WriteLine(string.Join(" ", new Solution().NextGreater(arr)));              // o/p: -1 6 -1 3 -1
		Console.WriteLine(string.Join(" ", new Solution().PreviousGreater(arr)));          // o/p: -1 8 8 6 6
	}
}

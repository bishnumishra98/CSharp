// Leetcode: 84. Largest Rectangle in Histogram   --->   Given an array of integers heights representing the histogram's
// bar height where the width of each bar is 1, return the area of the largest rectangle in the histogram.

// Example 1:
// Input: heights = [2,1,5,6,2,3]
// Output: 10
// Explanation: The largest rectangle uses the bars 5 and 6, with height 5 and width 2: 5 * 2 = 10.
//
//         #
//       # #
//       # #
//       # #   #
//   #   # # # #
//   # # # # # #
//   2 1 5 6 2 3      (the best rectangle: height 5, on the bars 5 and 6)

// Example 2:
// Input: heights = [2,4]
// Output: 4

// Extension: Largest SQUARE in a histogram. Same input, return the area of the largest square.
// Example: heights = [2,1,5,6,2,3]  ->  4   (a 2 x 2 square, e.g., on bars 5 and 6)

// Key idea: In the best rectangle, some bar is the SHORTEST one, and the rectangle has exactly that bar's height.
// So for every bar i, find how far the rectangle of height heights[i] can stretch:
//   to the left until the first SMALLER bar (previous smaller element), and
//   to the right until the first SMALLER bar (next smaller element).
//   width = (index of next smaller) - (index of previous smaller) - 1,   area = heights[i] * width


// Dry run image (in this folder): 15_LargestRectangleInHistogram.jpg

using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: For every bar, walk left and right while the bars are at least as tall.
	// T.C: O(n^2)
	// S.C: O(1)
	public int LargestRectangleArea_BruteForce(int[] heights)
	{
		int n = heights.Length, maxArea = 0;

		for (int i = 0; i < n; i++)
		{
			int left = i, right = i;
			while (left > 0 && heights[left - 1] >= heights[i]) left--;
			while (right < n - 1 && heights[right + 1] >= heights[i]) right++;

			maxArea = Math.Max(maxArea, heights[i] * (right - left + 1));
		}
		return maxArea;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Find the previous and next smaller element INDEXES with monotonic stacks
	// ('07_NextAndPreviousSmallerOrGreaterElement.cs'), then compute each bar's area.
	// • No smaller bar on the left -> use index -1. No smaller bar on the right -> use index n. (So the width formula
	//   still works: the rectangle reaches the array's edge.)
	// e.g., heights = [2, 1, 5, 6, 2, 3]
	//       prevSmaller = [-1, -1, 1, 2, 1, 4]
	//       nextSmaller = [ 1,  6, 4, 4, 6, 6]
	//       width       = [ 1,  6, 2, 1, 4, 1]
	//       area        = [ 2,  6, 10, 6, 8, 3]   ->  max = 10
	// T.C: O(3n) = O(n)
	// S.C: O(n);   for the 2 arrays and the stack
	public int LargestRectangleArea_TwoPass(int[] heights)
	{
		int n = heights.Length;
		int[] prevSmaller = PrevSmallerIndex(heights);
		int[] nextSmaller = NextSmallerIndex(heights);

		int maxArea = 0;
		for (int i = 0; i < n; i++)
		{
			int width = nextSmaller[i] - prevSmaller[i] - 1;
			maxArea = Math.Max(maxArea, heights[i] * width);
		}
		return maxArea;
	}

	private int[] PrevSmallerIndex(int[] h)
	{
		int n = h.Length;
		int[] ans = new int[n];
		Stack<int> st = new Stack<int>();
		for (int i = 0; i < n; i++)
		{
			while (st.Count > 0 && h[st.Peek()] >= h[i]) st.Pop();
			ans[i] = st.Count > 0 ? st.Peek() : -1;
			st.Push(i);
		}
		return ans;
	}

	private int[] NextSmallerIndex(int[] h)
	{
		int n = h.Length;
		int[] ans = new int[n];
		Stack<int> st = new Stack<int>();
		for (int i = n - 1; i >= 0; i--)
		{
			while (st.Count > 0 && h[st.Peek()] >= h[i]) st.Pop();
			ans[i] = st.Count > 0 ? st.Peek() : n;
			st.Push(i);
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: One pass with one stack.
	// Walk left to right, keeping a stack of indexes with increasing heights. When bar i is SHORTER than the bar on top,
	// the top bar can't stretch any further right: i is its next smaller bar. And the bar below it in the stack is its
	// previous smaller bar. So we can compute its area right now, while popping.
	// 1. For i = 0 to n (at i = n use height 0, a fake bar shorter than everything, so every bar left in the stack gets
	//    popped at the end):
	//    while the stack is not empty and heights[top] >= currentHeight:
	//        height = heights[pop()]
	//        left   = stack is empty ? -1 : stack.Peek()     (previous smaller)
	//        width  = i - left - 1                           (i is the next smaller)
	//        maxArea = max(maxArea, height * width)
	//    push i
	// T.C: O(n);   every index is pushed and popped once
	// S.C: O(n);   for the stack
	public int LargestRectangleArea(int[] heights)
	{
		int n = heights.Length, maxArea = 0;
		Stack<int> st = new Stack<int>();

		for (int i = 0; i <= n; i++)
		{
			int curr = (i == n) ? 0 : heights[i];

			while (st.Count > 0 && heights[st.Peek()] >= curr)
			{
				int height = heights[st.Pop()];
				int left = st.Count > 0 ? st.Peek() : -1;
				maxArea = Math.Max(maxArea, height * (i - left - 1));
			}

			st.Push(i);
		}
		return maxArea;
	}

// ------------------------------------------------------------------------------

	// Extension: Largest square in a histogram.
	// For bar i, the rectangle it rules has height heights[i] and width w (same as above). The biggest square that fits
	// inside it has side = min(height, width).
	// T.C: O(n)
	// S.C: O(n)
	public int LargestSquareArea(int[] heights)
	{
		int n = heights.Length;
		int[] prevSmaller = PrevSmallerIndex(heights);
		int[] nextSmaller = NextSmallerIndex(heights);

		int maxArea = 0;
		for (int i = 0; i < n; i++)
		{
			int side = Math.Min(heights[i], nextSmaller[i] - prevSmaller[i] - 1);
			maxArea = Math.Max(maxArea, side * side);
		}
		return maxArea;
	}
}


class Program
{
	static void Main()
	{
		int[] heights = { 2, 1, 5, 6, 2, 3 };

		Console.WriteLine(new Solution().LargestRectangleArea_BruteForce(heights));   // o/p: 10
		Console.WriteLine(new Solution().LargestRectangleArea_TwoPass(heights));      // o/p: 10
		Console.WriteLine(new Solution().LargestRectangleArea(heights));              // o/p: 10
		Console.WriteLine(new Solution().LargestSquareArea(heights));                 // o/p: 4
	}
}

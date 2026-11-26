// Leetcode: 973. K Closest Points to Origin   --->   Given an array of points where points[i] = [xi, yi] is a point on the
// X-Y plane, and an integer k, return the k points closest to the origin (0, 0). The answer can be in any order.
// Distance = sqrt(x^2 + y^2) (the usual straight-line distance).

// Example 1:
// Input: points = [[1,3],[-2,2]], k = 1
// Output: [[-2,2]]
// Explanation: (1, 3): distance sqrt(10). (-2, 2): distance sqrt(8). sqrt(8) < sqrt(10), so (-2, 2) is closer.

// Example 2:
// Input: points = [[3,3],[5,-1],[-2,4]], k = 2
// Output: [[3,3],[-2,4]]   (squared distances 18, 26, 20)

// Note: The C++ file for this problem was empty (only the title). This solution is new.

// Trick: Compare x^2 + y^2 instead of sqrt(x^2 + y^2). The square root does not change which one is smaller, and
// integers avoid floating point errors. (With |x|, |y| <= 10^4, x^2 + y^2 <= 2 * 10^8, which fits in an int.)

// This is "k smallest", so it is the same pattern as '07_KthSmallestAndKthLargestElementInArray.cs'.


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	// Brute force: Sort all points by distance, take the first k.
	// T.C: O(n log n)
	// S.C: O(n)
	public int[][] KClosest_Sort(int[][] points, int k)
	{
		return points.OrderBy(p => p[0] * p[0] + p[1] * p[1]).Take(k).ToArray();
	}

// ------------------------------------------------------------------------------

	// Optimal (heap): Keep the k closest points seen so far in a MAX heap (by distance) of size k. When it grows to k + 1,
	// remove the top: the FARTHEST of them, which can not be in the answer.
	// T.C: O(n log k)
	// S.C: O(k)
	// (Quickselect, like in '07_KthSmallestAndKthLargestElementInArray.cs', is O(n) on average.)
	public int[][] KClosest(int[][] points, int k)
	{
		PriorityQueue<int[], int> maxHeap = new PriorityQueue<int[], int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

		foreach (int[] p in points)
		{
			maxHeap.Enqueue(p, p[0] * p[0] + p[1] * p[1]);
			if (maxHeap.Count > k) maxHeap.Dequeue();   // drop the farthest
		}

		int[][] ans = new int[k][];
		for (int i = 0; i < k; i++) ans[i] = maxHeap.Dequeue();
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[][] points = { new[] { 1, 3 }, new[] { -2, 2 } };

		var sorted = new Solution().KClosest_Sort(points, 1).Select(p => $"[{p[0]},{p[1]}]");
		var heap = new Solution().KClosest(points, 1).Select(p => $"[{p[0]},{p[1]}]");
		Console.WriteLine(string.Join(" ", sorted));   // o/p: [-2,2]
		Console.WriteLine(string.Join(" ", heap));     // o/p: [-2,2]
	}
}

// Leetcode: 239. Sliding Window Maximum   --->   You are given an array of integers nums, there is a sliding window of
// size k which is moving from the very left of the array to the very right. You can only see the k numbers in the window.
// Each time the sliding window moves right by one position.
// Return the max sliding window.

// Example 1:
// Input: nums = [1,3,-1,-3,5,3,6,7], k = 3
// Output: [3,3,5,5,6,7]
// Explanation:
// Window position                Max
// ---------------               -----
// [1  3  -1] -3  5  3  6  7       3
//  1 [3  -1  -3] 5  3  6  7       3
//  1  3 [-1  -3  5] 3  6  7       5
//  1  3  -1 [-3  5  3] 6  7       5
//  1  3  -1  -3 [5  3  6] 7       6
//  1  3  -1  -3  5 [3  6  7]      7

// Example 2:
// Input: nums = [1], k = 1
// Output: [1]

// Constraints: 1 <= nums.length <= 10^5,  1 <= k <= nums.length


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: For every window, scan its k numbers for the max.
	// T.C: O(n * k);   too slow for n = 10^5 and a big k
	// S.C: O(1);   ignoring the answer
	public int[] MaxSlidingWindow_BruteForce(int[] nums, int k)
	{
		int[] ans = new int[nums.Length - k + 1];

		for (int start = 0; start + k <= nums.Length; start++)
		{
			int max = nums[start];
			for (int j = start; j < start + k; j++) max = Math.Max(max, nums[j]);
			ans[start] = max;
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Max-heap (PriorityQueue) of (value, index).
	// The biggest value is always on top. But the top may be an OLD number that has already left the window. So before
	// reading the top, remove tops whose index is outside the window ("lazy removal").
	// C#'s PriorityQueue gives the SMALLEST priority first, so use -value as the priority to get the biggest value first.
	// T.C: O(n logn);   up to n items in the heap
	// S.C: O(n)
	public int[] MaxSlidingWindow_Heap(int[] nums, int k)
	{
		int[] ans = new int[nums.Length - k + 1];
		PriorityQueue<int, int> heap = new PriorityQueue<int, int>();   // element = index, priority = -value

		for (int i = 0; i < nums.Length; i++)
		{
			heap.Enqueue(i, -nums[i]);
			if (i < k - 1) continue;   // the first window is not full yet

			while (heap.Peek() <= i - k) heap.Dequeue();   // the top has left the window
			ans[i - k + 1] = nums[heap.Peek()];
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Monotonic deque of INDEXES (values decreasing from front to back).
	// Key fact: If a newer number is bigger than an older one, the older one can NEVER be the max again (the newer one is
	//           in every future window the older one is in, and it's bigger). So throw the older one away.
	// For every index i:
	// 1. Remove from the BACK every index whose value is <= nums[i] (they can never be the max any more).
	// 2. Add i at the back.
	// 3. Remove from the FRONT the index that has slid out of the window (front <= i - k).
	// 4. Once the first window is full (i >= k - 1), the FRONT is the index of the max.
	// The deque stays sorted: biggest value at the front. We use LinkedList<int> as the deque (C# has no Deque<T>).
	// e.g., [1, 3, -1, -3, 5], k = 3:
	//       i=0 (1):  [0]
	//       i=1 (3):  3 >= 1, remove 0 -> [1]
	//       i=2 (-1): [1, 2]               window [0..2], max = nums[1] = 3
	//       i=3 (-3): [1, 2, 3]            window [1..3], max = 3
	//       i=4 (5):  remove 3, 2, 1 -> [4]  window [2..4], max = 5
	// T.C: O(n);   every index is added and removed at most once
	// S.C: O(k);   for the deque
	public int[] MaxSlidingWindow(int[] nums, int k)
	{
		int[] ans = new int[nums.Length - k + 1];
		LinkedList<int> dq = new LinkedList<int>();   // indexes

		for (int i = 0; i < nums.Length; i++)
		{
			while (dq.Count > 0 && nums[dq.Last.Value] <= nums[i]) dq.RemoveLast();
			dq.AddLast(i);

			if (dq.First.Value <= i - k) dq.RemoveFirst();   // out of the window

			if (i >= k - 1) ans[i - k + 1] = nums[dq.First.Value];
		}
		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 3, -1, -3, 5, 3, 6, 7 };
		int k = 3;

		Console.WriteLine(string.Join(" ", new Solution().MaxSlidingWindow_BruteForce(nums, k)));   // o/p: 3 3 5 5 6 7
		Console.WriteLine(string.Join(" ", new Solution().MaxSlidingWindow_Heap(nums, k)));         // o/p: 3 3 5 5 6 7
		Console.WriteLine(string.Join(" ", new Solution().MaxSlidingWindow(nums, k)));              // o/p: 3 3 5 5 6 7
	}
}

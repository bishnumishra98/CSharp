// Leetcode: 658. Find K Closest Elements   --->   Given a sorted integer array arr, two integers k and x, return
// the k closest integers to x in the array. The result should also be sorted in ascending order.
// An integer a is closer to x than an integer b if:
// • |a - x| < |b - x|, or
// • |a - x| == |b - x| and a < b

// Example 1:
// Input: arr = [1,2,3,4,5], k = 4, x = 3
// Output: [1,2,3,4]

// Example 2:
// Input: arr = [1,1,2,3,4,5], k = 4, x = -1
// Output: [1,1,2,3]

// Better algorithm: Two pointer approach (shrink the window from both sides).
// 1. Initialize low and high on 0 and n - 1 respectively. The window [low, high] will shrink until only the k closest
//    elements are left. Distance of arr[low] from x is 'x - arr[low]', and distance of arr[high] from x is 'arr[high] - x'.
//    (For elements that are on the same side of x, these values can be negative, but the comparison below still works,
//    because the array is sorted.)
// 2. There can be 3 cases:
//    Case 1: arr[low] is nearer to x than arr[high]: 'x - arr[low] < arr[high] - x'.
//            arr[high] is the farthest one, so drop it: move high towards left.
//    Case 2: arr[low] and arr[high] are at the same distance: 'x - arr[low] == arr[high] - x'.
//            In a tie, the smaller element wins, and the smaller one is arr[low]. So still drop arr[high]: move high left.
//    Case 3: arr[low] is farther from x than arr[high]: 'x - arr[low] > arr[high] - x'.
//            Drop arr[low]: move low towards right.
//    Continue until exactly k elements are left between low and high, i.e., while (high - low >= k).
// 3. All the elements from 'low' to 'high' are the k closest elements to x.


using System;
using System.Collections.Generic;

public class Solution
{
	// T.C: O(n - k);   where n = arr.Length
	// S.C: O(k);   for the answer list
	public IList<int> FindClosestElements_TwoPointer(int[] arr, int k, int x)
	{
		int low = 0, high = arr.Length - 1;

		while (high - low >= k)
		{
			if (x - arr[low] <= arr[high] - x) high--;
			else low++;   // if (x - arr[low] > arr[high] - x)
		}

		List<int> ans = new List<int>();
		for (int i = low; i <= high; i++) ans.Add(arr[i]);
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search on the START index of the answer window.
	// Problem with two pointers: It removes only 1 element per step, so it takes (n - k) steps.
	// Solution: The answer is always k elements next to each other: arr[start], ..., arr[start + k - 1].
	//           So we only need to find 'start'. It can be anything from 0 to n - k. Binary search can find it quickly.
	// 1. low = 0, high = n - k (possible values of 'start').
	// 2. Take mid, and think of the window arr[mid ... mid + k - 1]. Compare:
	//    -> arr[mid]     : the first element inside the window
	//    -> arr[mid + k] : the first element just outside the window, on the right
	//    If arr[mid + k] is closer to x than arr[mid], i.e., (x - arr[mid] > arr[mid + k] - x), the window should move
	//    right. So the answer starts after mid: low = mid + 1.
	//    Else (arr[mid] is closer, or it's a tie and the smaller one wins), the answer starts at mid or before: high = mid.
	// 3. When low == high, 'low' is the start of the answer. Return arr[low ... low + k - 1].
	// e.g., arr = [1, 2, 3, 4, 5], k = 4, x = 3 -> low = 0, high = 1
	//       mid = 0: x - arr[0] = 2, arr[4] - x = 2. Not greater (tie), so high = 0. Now low == high = 0.
	//       Answer = arr[0 ... 3] = [1, 2, 3, 4]
	// T.C: O(log(n - k) + k);   binary search + copying k elements
	// S.C: O(k);   for the answer list
	public IList<int> FindClosestElements(int[] arr, int k, int x)
	{
		int low = 0, high = arr.Length - k;

		while (low < high)
		{
			int mid = low + (high - low) / 2;   // same as (low + high) / 2, but it can't overflow for very big numbers
			if (x - arr[mid] > arr[mid + k] - x) low = mid + 1;
			else high = mid;
		}

		List<int> ans = new List<int>();
		for (int i = low; i < low + k; i++) ans.Add(arr[i]);
		return ans;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(string.Join(" ", new Solution().FindClosestElements_TwoPointer(new int[] { 1, 2, 3, 4, 5 }, 4, 3)));   // o/p: 1 2 3 4
		Console.WriteLine(string.Join(" ", new Solution().FindClosestElements(new int[] { 1, 2, 3, 4, 5 }, 4, 3)));              // o/p: 1 2 3 4
	}
}

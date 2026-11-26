// Exponential Search   --->   Exponential search (also called galloping search or doubling search) is used to search
// a target in a sorted array whose size is unknown or infinite (unbounded), like a huge sorted data stream.
// Return the index of target, or -1 if it is not present.

// Example 1:
// Input: arr = [10, 20, 30, 40, 50, 60, 70, 80, 90], target = 60
// Output: 5

// Example 2:
// Input: arr = [10, 20, 30, 40, 50], target = 35
// Output: -1

// Problem with normal binary search: It needs 'end = n - 1', but in an infinite array we don't know n.
// Solution: First find a small range [start, end] where the target must be, by doubling the index. Then apply binary
//           search only inside that range.


using System;

public class Solution
{
	// Algorithm:
	// 1. If arr[0] is the target, return 0.
	// 2. Start with i = 1 and keep doubling it (1, 2, 4, 8, 16, ...) while arr[i] <= target.
	//    Now arr[i] > target (or i went past the end), and in the previous step arr[i / 2] <= target.
	//    So the target, if present, is between index i / 2 and i.
	// 3. Apply binary search on [i / 2, min(i, n - 1)]. 'min' is needed so that 'end' doesn't go outside the array.
	//    e.g., arr = [10, 20, 30, 40, 50, 60, 70, 80, 90], target = 60
	//          i = 1 (20 <= 60), i = 2 (30 <= 60), i = 4 (50 <= 60), i = 8 (90 > 60), stop.
	//          Binary search on index 4 to 8 -> found at 5.
	// T.C: O(logk);   where k = index of target. Doubling takes logk steps, and the range [i / 2, i] has i / 2
	//                 elements, so binary search also takes logk steps. In the worst case k = n, so O(logn).
	// S.C: O(1)
	public int ExponentialSearch(int[] arr, int target)
	{
		if (arr.Length == 0 || target < arr[0]) return -1;
		if (arr[0] == target) return 0;

		int i = 1;
		while (i < arr.Length && arr[i] <= target) i *= 2;

		return BinarySearch(arr, target, i / 2, Math.Min(i, arr.Length - 1));
	}

	private int BinarySearch(int[] arr, int target, int start, int end)
	{
		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (target == arr[mid]) return mid;
			else if (target > arr[mid]) start = mid + 1;
			else end = mid - 1;
		}
		return -1;
	}
}

// Note: Here we used arr.Length only to stay inside the array. For a truly infinite data source (like a stream or
// an API), we can't use Length. Instead, we read elements one by one and stop when the source says "no more data"
// (an exception or a special value). The algorithm stays the same.


class Program
{
	static void Main()
	{
		int[] arr = { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
		int target = 60;

		Console.WriteLine(new Solution().ExponentialSearch(arr, target));   // o/p: 5
	}
}

// Search in a Nearly Sorted Array   --->   A nearly sorted array is a sorted array in which each element may be moved
// by at most 1 position, i.e., an element that should be at index i (in the sorted array) can be at index i - 1, i or
// i + 1. Given a nearly sorted array and a target, return the index of target, or -1 if it is not present.

// Example 1:
// Input: arr = [20, 10, 30, 50, 40, 70, 60], target = 70
// Output: 5

// Example 2:
// Input: arr = [10, 3, 40, 20, 50, 80, 70], target = 90
// Output: -1


using System;

public class Solution
{
	// Brute force: Linear search.
	// T.C: O(n)
	// S.C: O(1)
	public int Search_Linear(int[] arr, int target)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			if (arr[i] == target) return i;
		}
		return -1;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search with 2 extra checks.
	// The element that should be at 'mid' can also be at 'mid - 1' or 'mid + 1'. So, check all 3 places:
	// Case 1: arr[mid] == target      ->  return mid
	// Case 2: arr[mid - 1] == target  ->  return mid - 1
	// Case 3: arr[mid + 1] == target  ->  return mid + 1
	// If none of them is the target, do normal binary search, but jump by 2:
	// Case 4: target < arr[mid]  ->  go left:  end = mid - 2   (mid - 1 is already checked in case 2)
	// Case 5: target > arr[mid]  ->  go right: start = mid + 2 (mid + 1 is already checked in case 3)
	// T.C: O(logn)
	// S.C: O(1)
	public int Search(int[] arr, int target)
	{
		int n = arr.Length;
		int start = 0, end = n - 1;

		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (arr[mid] == target) return mid;
			else if (mid - 1 >= 0 && arr[mid - 1] == target) return mid - 1;
			else if (mid + 1 < n && arr[mid + 1] == target) return mid + 1;
			else if (target < arr[mid]) end = mid - 2;
			else start = mid + 2;   // target > arr[mid]
		}

		return -1;
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 20, 10, 30, 50, 40, 70, 60 };
		int target = 70;

		Console.WriteLine(new Solution().Search_Linear(arr, target));   // o/p: 5
		Console.WriteLine(new Solution().Search(arr, target));          // o/p: 5
	}
}

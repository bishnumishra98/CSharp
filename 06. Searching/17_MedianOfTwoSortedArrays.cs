// Leetcode: 4. Median of Two Sorted Arrays   --->   Given two sorted arrays nums1 and nums2 of size m and n
// respectively, return the median of the two sorted arrays.
// The overall run time complexity should be O(log (m+n)).

// Example 1:
// Input: nums1 = [1,3], nums2 = [2]
// Output: 2.00000
// Explanation: merged array = [1,2,3] and median is 2.

// Example 2:
// Input: nums1 = [1,2], nums2 = [3,4]
// Output: 2.50000
// Explanation: merged array = [1,2,3,4] and median is (2 + 3) / 2 = 2.5.

// Median of a sorted array of length 'total':
// ● Odd total:  the middle element, i.e., merged[total / 2]
// ● Even total: average of the 2 middle elements, i.e., (merged[total / 2 - 1] + merged[total / 2]) / 2


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: Merge both arrays into one sorted list (like merge step of merge sort), then pick the middle.
	// T.C: O(m + n)
	// S.C: O(m + n);   for the merged list
	public double FindMedianSortedArrays_BruteForce(int[] nums1, int[] nums2)
	{
		List<int> merged = new List<int>();
		int i = 0, j = 0;

		while (i < nums1.Length && j < nums2.Length)
		{
			if (nums1[i] < nums2[j]) merged.Add(nums1[i++]);
			else merged.Add(nums2[j++]);
		}
		while (i < nums1.Length) merged.Add(nums1[i++]);
		while (j < nums2.Length) merged.Add(nums2[j++]);

		int total = merged.Count;
		if (total % 2 == 1) return merged[total / 2];
		return (merged[total / 2 - 1] + merged[total / 2]) / 2.0;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Same merge, but don't store anything.
	// Problem with brute force: We store all m + n elements, but we only need the 2 middle ones.
	// Solution: Walk through both arrays with the same 2 pointers, and count the position of each element in the
	//           (imaginary) merged array. Just remember the elements at position (total / 2 - 1) and (total / 2).
	//           We can also stop as soon as we reach position total / 2.
	// T.C: O(m + n);   actually (m + n) / 2 steps
	// S.C: O(1)
	public double FindMedianSortedArrays_Better(int[] nums1, int[] nums2)
	{
		int total = nums1.Length + nums2.Length;
		int i = 0, j = 0;
		int prev = 0, curr = 0;   // elements at position (count - 1) and count of the merged array

		for (int count = 0; count <= total / 2; count++)
		{
			prev = curr;
			if (j >= nums2.Length || (i < nums1.Length && nums1[i] < nums2[j])) curr = nums1[i++];
			else curr = nums2[j++];
		}

		if (total % 2 == 1) return curr;
		return (prev + curr) / 2.0;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search on the partition.
	// Idea: Cut the merged array into a LEFT half and a RIGHT half, such that every element of the left half is <= every
	//       element of the right half. Then the median comes only from the elements next to the cut.
	// ● Left half size: left = (total + 1) / 2   (for odd total, the left half keeps the extra element)
	// ● The left half takes 'mid1' elements from nums1 and 'mid2 = left - mid1' elements from nums2.
	//   So, once we choose mid1, everything else is fixed. We only need to find the correct mid1 (0 to nums1.Length).
	//
	// For a chosen mid1, look at the 4 elements next to the cut:
	//   l1 = nums1[mid1 - 1] (last of nums1 in left)   |   r1 = nums1[mid1] (first of nums1 in right)
	//   l2 = nums2[mid2 - 1] (last of nums2 in left)   |   r2 = nums2[mid2] (first of nums2 in right)
	//   If a side is empty, use int.MinValue for l1/l2 and int.MaxValue for r1/r2, so it never breaks the checks.
	// We already know l1 <= r1 and l2 <= r2 (each array is sorted). We only need to check across the arrays:
	// Case 1: l1 <= r2 && l2 <= r1  ->  Correct cut.
	//                                   Odd total:  median = max(l1, l2)
	//                                   Even total: median = (max(l1, l2) + min(r1, r2)) / 2
	// Case 2: l1 > r2  ->  Took too many from nums1. Take fewer: end = mid1 - 1.
	// Case 3: l2 > r1  ->  Took too few from nums1 (too many from nums2). Take more: start = mid1 + 1.
	//
	// e.g., nums1 = [1, 3, 5], nums2 = [2, 4, 6]  ->  total = 6, left = 3
	//       mid1 = 1, mid2 = 2:  left = [1 | 2, 4], right = [3, 5 | 6].  l2 = 4 > r1 = 3  ->  take more from nums1.
	//       mid1 = 2, mid2 = 1:  left = [1, 3 | 2], right = [5 | 4, 6].  3 <= 4 and 2 <= 5  ->  correct cut.
	//       median = (max(3, 2) + min(5, 4)) / 2 = (3 + 4) / 2 = 3.5
	// Tip: Always binary search on the SMALLER array (swap if needed). The range is smaller, and mid2 never goes
	//      out of nums2's bounds.
	// T.C: O(log(min(m, n)))
	// S.C: O(1)
	public double FindMedianSortedArrays(int[] nums1, int[] nums2)
	{
		if (nums1.Length > nums2.Length) return FindMedianSortedArrays(nums2, nums1);   // keep nums1 as the smaller one

		int n1 = nums1.Length, n2 = nums2.Length;
		int total = n1 + n2;
		int left = (total + 1) / 2;

		int start = 0, end = n1;
		while (start <= end)
		{
			int mid1 = start + (end - start) / 2;   // elements taken from nums1 in the left half
			int mid2 = left - mid1;                 // elements taken from nums2 in the left half

			int l1 = (mid1 > 0) ? nums1[mid1 - 1] : int.MinValue;
			int l2 = (mid2 > 0) ? nums2[mid2 - 1] : int.MinValue;
			int r1 = (mid1 < n1) ? nums1[mid1] : int.MaxValue;
			int r2 = (mid2 < n2) ? nums2[mid2] : int.MaxValue;

			if (l1 <= r2 && l2 <= r1)
			{
				if (total % 2 == 1) return Math.Max(l1, l2);
				return ((double)Math.Max(l1, l2) + Math.Min(r1, r2)) / 2;
			}
			else if (l1 > r2) end = mid1 - 1;
			else start = mid1 + 1;   // l2 > r1
		}

		return 0;   // never reached
	}
}


class Program
{
	static void Main()
	{
		int[] nums1 = { 1, 3 };
		int[] nums2 = { 2 };

		Console.WriteLine(new Solution().FindMedianSortedArrays_BruteForce(nums1, nums2));   // o/p: 2
		Console.WriteLine(new Solution().FindMedianSortedArrays_Better(nums1, nums2));       // o/p: 2
		Console.WriteLine(new Solution().FindMedianSortedArrays(nums1, nums2));              // o/p: 2
	}
}

// GFG: Common in 3 Sorted Arrays   --->   You are given three arrays sorted in increasing order. Find the elements
// that are common in all three arrays.
// If there are no such elements return an empty array. In this case, the output will be -1.
// Note: can you handle the duplicates without using any additional Data Structure?

// Example 1:
// Input: arr1 = [1, 5, 10, 20, 40, 80], arr2 = [6, 7, 20, 80, 100], arr3 = [3, 4, 15, 20, 30, 70, 80, 120]
// Output: [20, 80]
// Explanation: 20 and 80 are the only common elements in arr1, arr2 and arr3.

// Example 2:
// Input: arr1 = [1, 2, 3, 4, 5], arr2 = [6, 7], arr3 = [8, 9, 10]
// Output: [-1]
// Explanation: There are no common elements in arr1, arr2 and arr3.

// Example 3:
// Input: arr1 = [1, 1, 1, 2, 2, 2], arr2 = [1, 1, 2, 2, 2], arr3 = [1, 1, 1, 1, 2, 2, 2, 2]
// Output: [1, 2]
// Explanation: We do not need to consider duplicates.

// Problem link: https://www.geeksforgeeks.org/problems/common-elements1132/1


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force algorithm: Check every combination (i, j, k) using 3 loops. If all 3 elements are equal, it is common.
	// A SortedSet is used so that duplicates are not stored, and the answer stays in increasing order.
	// (SortedSet = a HashSet that also keeps its items sorted.)
	// T.C: O(n1 * n2 * n3);   where n1, n2 and n3 are sizes of arr1, arr2 and arr3 respectively.
	// S.C: O(n)
	public List<int> CommonElements_BruteForce(int[] arr1, int[] arr2, int[] arr3)
	{
		SortedSet<int> st = new SortedSet<int>();

		for (int i = 0; i < arr1.Length; i++)
		{
			for (int j = 0; j < arr2.Length; j++)
			{
				for (int k = 0; k < arr3.Length; k++)
				{
					if (arr1[i] == arr2[j] && arr2[j] == arr3[k]) st.Add(arr1[i]);
				}
			}
		}

		return new List<int>(st);   // convert the set into a list
	}

// ------------------------------------------------------------------------------

	// Better algorithm: 3 pointer approach, taking advantage of the fact that all arrays are sorted.
	// Keep 3 pointers i, j, k on the 0th index of all arrays. The goal is to move the pointer that points to the
	// smallest element (it can never be common), until all 3 pointers point to the same value.
	// There can be 4 cases:
	// Case 1: If all pointers point to the same value, store the common element, then move all pointers ahead.
	// Case 2: If arr1[i] < arr2[j], arr1[i] can't be common in all the 3 arrays (arr2 has only bigger elements ahead).
	//         So move i ahead to check a bigger element in arr1.
	// Case 3: If arr2[j] < arr3[k], arr2[j] can't be common. So move j ahead.
	// Case 4: If none of the above is true, arr3[k] is the smallest among the 3. So move k ahead.
	// T.C: O(n1 + n2 + n3);   where n1, n2 and n3 are sizes of arr1, arr2 and arr3 respectively.
	// S.C: O(n)
	public List<int> CommonElements_TimeOptimised(int[] arr1, int[] arr2, int[] arr3)
	{
		SortedSet<int> st = new SortedSet<int>();   // set is used to ensure we don't store any duplicate common elements
		int i = 0, j = 0, k = 0;

		while (i < arr1.Length && j < arr2.Length && k < arr3.Length)
		{
			if (arr1[i] == arr2[j] && arr2[j] == arr3[k])
			{
				st.Add(arr1[i]);   // store this element in set, then move all pointers
				i++;
				j++;
				k++;
			}
			else if (arr1[i] < arr2[j]) i++;   // if element pointed by i is smaller, then move i
			else if (arr2[j] < arr3[k]) j++;   // if element pointed by j is smaller, then move j
			else k++;   // if element pointed by k is smaller, then move k
		}

		return new List<int>(st);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Same 3 pointer approach, but without the set (this answers the "Note" in the problem).
	// Since the arrays are sorted, duplicates of a common element are found one after another. So before adding, just
	// check that the last added element is not the same.
	// T.C: O(n1 + n2 + n3);   where n1, n2 and n3 are sizes of arr1, arr2 and arr3 respectively.
	// S.C: O(1);   no extra data structure, apart from the answer list itself
	public List<int> CommonElements(int[] arr1, int[] arr2, int[] arr3)
	{
		List<int> commonElements = new List<int>();
		int i = 0, j = 0, k = 0;

		while (i < arr1.Length && j < arr2.Length && k < arr3.Length)
		{
			if (arr1[i] == arr2[j] && arr2[j] == arr3[k])
			{
				// To avoid duplicates, only add if the list is empty or the last added element is different
				if (commonElements.Count == 0 || commonElements[commonElements.Count - 1] != arr1[i])
				{
					commonElements.Add(arr1[i]);
				}
				i++;
				j++;
				k++;
			}
			else if (arr1[i] < arr2[j]) i++;   // if element pointed by i is smaller, then move i
			else if (arr2[j] < arr3[k]) j++;   // if element pointed by j is smaller, then move j
			else k++;   // if element pointed by k is smaller, then move k
		}

		return commonElements;
	}
}


class Program
{
	static void Main()
	{
		int[] arr1 = { 1, 1, 1, 2, 2, 2 }, arr2 = { 1, 1, 2, 2, 2 }, arr3 = { 1, 1, 1, 1, 2, 2, 2, 2 };

		Console.WriteLine(string.Join(" ", new Solution().CommonElements_BruteForce(arr1, arr2, arr3)));      // o/p: 1 2
		Console.WriteLine(string.Join(" ", new Solution().CommonElements_TimeOptimised(arr1, arr2, arr3)));   // o/p: 1 2
		Console.WriteLine(string.Join(" ", new Solution().CommonElements(arr1, arr2, arr3)));                 // o/p: 1 2
	}
}

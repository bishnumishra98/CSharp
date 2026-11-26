// GFG: Move all negative elements to end   --->   Given an unsorted array arr[] having both negative and positive
// integers. The task is to place all negative elements at the end of the array.
// Note: Don't return any array, just do it in-place on the array.

// This problem comes in 2 versions:
// Version 1: Order of elements does NOT matter.
//            Input : arr[] = [1, -1, 3, 2, -7, -5, 11, 6]
//            Output: [1, 3, 2, 11, 6, -5, -1, -7]   (any order is fine, just negatives at the end)
// Version 2: Order of elements MUST stay the same (positives in their order, negatives in their order).
//            Input : arr[] = [1, -1, 3, 2, -7, -5, 11, 6]
//            Output: [1, 3, 2, 11, 6, -1, -7, -5]

// Problem link: https://www.geeksforgeeks.org/problems/move-all-negative-elements-to-end1813/1


using System;
using System.Collections.Generic;

public class Solution
{
	// Version 1 (order does not matter):
	// Brute force algorithm: Just sort the array in descending order -> Array.Sort(arr); Array.Reverse(arr);
	//                        T.C: O(nlogn)

	// Optimal algorithm: Two pointer approach
	// 1. 'i' visits every element. 'j' marks the place where the next positive number should go.
	//    So all elements before index 'j' are always positive (0 is also treated as positive).
	// 2. If arr[i] is positive, swap it with arr[j] and move j ahead. In any case, move i ahead.
	// 3. At the end, all positives are on the left and all negatives are pushed to the end.
	// NOTE: To shift negatives to the LEFT instead, just change the condition to 'arr[i] < 0'.
	// T.C: O(n)
	// S.C: O(1)
	public void SegregateElements_AnyOrder(int[] arr)
	{
		int i = 0, j = 0;

		while (i < arr.Length)
		{
			if (arr[i] >= 0)
			{
				(arr[i], arr[j]) = (arr[j], arr[i]);   // swap
				j++;
			}
			i++;
		}
	}

// ------------------------------------------------------------------------------

	// Version 2 (order must stay the same):
	// The two pointer approach above changes the order of elements, so it will fail here.
	// Algorithm: Use an extra list.
	// 1. First put all positive numbers in a list 'temp' (in the same order).
	// 2. Then put all negative numbers in 'temp' (in the same order).
	// 3. Copy 'temp' back into 'arr'.
	// T.C: O(2n)
	// S.C: O(n)
	public void SegregateElements(int[] arr)
	{
		List<int> temp = new List<int>();

		// Store positive numbers first
		for (int i = 0; i < arr.Length; i++)
		{
			if (arr[i] >= 0) temp.Add(arr[i]);   // 0 is also treated as a positive number
		}

		// Store negative numbers next
		for (int i = 0; i < arr.Length; i++)
		{
			if (arr[i] < 0) temp.Add(arr[i]);
		}

		// Copy the elements of 'temp' back into the original array.
		// NOTE (C#): Writing 'arr = temp.ToArray();' will NOT work. It only changes the local variable 'arr' inside this
		//            method, and the caller's array stays the same. So we must copy element by element.
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = temp[i];
		}
	}
}


class Program
{
	static void Main()
	{
		int[] arr1 = { 1, -1, 3, 2, -7, -5, 11, 6 };
		new Solution().SegregateElements_AnyOrder(arr1);
		Console.WriteLine(string.Join(" ", arr1));   // o/p: 1 3 2 11 6 -5 -1 -7

		int[] arr2 = { 1, -1, 3, 2, -7, -5, 11, 6 };
		new Solution().SegregateElements(arr2);
		Console.WriteLine(string.Join(" ", arr2));   // o/p: 1 3 2 11 6 -1 -7 -5
	}
}

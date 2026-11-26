// GFG: First Repeating Element   --->   Given an array arr[], find the first repeating element. The element should
// occur more than once and the index of its first occurrence should be the smallest.
// Note: The position you return should be according to 1-based indexing.

// Example 1:
// Input: arr[] = [1, 5, 3, 4, 3, 5, 6]
// Output: 2
// Explanation: 5 appears twice and its first appearance is at index 2 which is less than 3 whose first occurring index is 3.

// Example 2:
// Input: arr[] = [1, 2, 3, 4]
// Output: -1
// Explanation: All elements appear only once so answer is -1.

// Problem link: https://www.geeksforgeeks.org/problems/first-repeating-element4018/1

// NOTE: "First repeating" means the repeating element that appears FIRST in the array (from left), not the one whose
//       2nd copy comes first. In [1, 5, 3, 4, 3, 5, 6], 3 repeats before 5 does, but 5 appears first in the array.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force algorithm:
	// 1. Collect all duplicate elements in a list 'duplicates' (using a HashSet to check if we have seen an element before).
	// 2. Go through the array from the start. The first element that is present in 'duplicates' is the answer.
	//    Return its index + 1 (1-based indexing).
	// T.C: O(n^2)
	// S.C: O(n)
	public int FirstRepeated_BruteForce(int[] arr)
	{
		HashSet<int> seen = new HashSet<int>();
		List<int> duplicates = new List<int>();

		// Collect all duplicate elements in 'duplicates' list
		for (int i = 0; i < arr.Length; i++)
		{
			if (seen.Contains(arr[i])) duplicates.Add(arr[i]);
			seen.Add(arr[i]);
		}

		// Check each element of the array (from the start) against every element of 'duplicates'.
		// The first match is the first repeating element. So return its index (index starts from 1).
		for (int i = 0; i < arr.Length; i++)
		{
			for (int j = 0; j < duplicates.Count; j++)
			{
				if (arr[i] == duplicates[j]) return i + 1;   // considering 1 based indexing
			}
		}

		return -1;   // if no duplicates found
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm:
	// 1. Store the frequency (count) of every element in a Dictionary (key: element, value: frequency).
	// 2. Go through the array from the start, and return the index of the first element whose frequency is more than 1.
	// T.C: O(n)
	// S.C: O(n)
	public int FirstRepeated(int[] arr)
	{
		Dictionary<int, int> freq = new Dictionary<int, int>();

		// Store the frequency of each element in the dictionary
		// GetValueOrDefault(key) gives the stored value if the key exists, else 0.
		for (int i = 0; i < arr.Length; i++)
		{
			freq[arr[i]] = freq.GetValueOrDefault(arr[i]) + 1;
		}

		// Return the first element that repeats, i.e., element whose frequency is more than 1.
		for (int i = 0; i < arr.Length; i++)
		{
			if (freq[arr[i]] > 1) return i + 1;   // considering 1 based indexing
		}

		return -1;   // if no duplicates found
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 1, 5, 3, 4, 3, 5, 6 };

		Console.WriteLine(new Solution().FirstRepeated_BruteForce(arr));   // o/p: 2
		Console.WriteLine(new Solution().FirstRepeated(arr));              // o/p: 2
	}
}

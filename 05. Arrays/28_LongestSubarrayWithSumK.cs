// GFG: Longest Subarray with Sum K   --->   Given an array arr[] containing integers and an integer k, your task is to
// find the length of the longest subarray where the sum of its elements is equal to the given value k. If there is no
// subarray with sum equal to k, return 0.

// Example 1:
// Input: arr[] = [10, 5, 2, 7, 1, -10], k = 15
// Output: 6
// Explanation: Subarrays with sum = 15 are [5, 2, 7, 1], [10, 5] and [10, 5, 2, 7, 1, -10]. The length of the longest
// subarray with a sum of 15 is 6.

// Example 2:
// Input: arr[] = [-5, 8, -14, 2, 4, 12], k = -5
// Output: 5
// Explanation: Only subarray with sum = -5 is [-5, 8, -14, 2, 4] of length 5.

// Example 3:
// Input: arr[] = [10, -10, 20, 30], k = 5
// Output: 0
// Explanation: No subarray with sum = 5 is present in arr[].

// Constraints:
// 1 ≤ arr.size() ≤ 10^5
// -10^4 ≤ arr[i] ≤ 10^4
// -10^9 ≤ k ≤ 10^9

// Problem link: https://www.geeksforgeeks.org/problems/longest-sub-array-with-sum-k0809/1

// (What is a subarray? See 16_MaximumAverageSubarrayI.cs)


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force algorithm: Generate all subarrays and find the longest one having sum k.
	// 1. 'i' marks where the subarray starts, and 'j' marks where it ends (j goes from i to n - 1).
	// 2. For every subarray from i to j, find its sum using a 3rd loop. Its length is 'j - i + 1'.
	// 3. If the sum is k, update the maximum length.
	// T.C: Little less than O(n^3)
	// S.C: O(1)
	public int LongestSubarray_BruteForce(int[] arr, int k)
	{
		int maxLength = 0;

		for (int i = 0; i < arr.Length; i++)
		{
			for (int j = i; j < arr.Length; j++)
			{
				int sum = 0;
				for (int x = i; x <= j; x++) sum += arr[x];   // sum of elements of the subarray that ranges from 'i' to 'j'
				if (sum == k) maxLength = Math.Max(maxLength, j - i + 1);   // update the maximum length of the subarray whose sum equals k
			}
		}

		return maxLength;
	}

// ------------------------------------------------------------------------------

	// Brute force optimisation: The algorithm remains the same, but we remove the 3rd loop.
	// No need to calculate the sum every time starting from index 'i'. For every new 'i', start 'sum' from 0, and just
	// keep adding arr[j] as 'j' moves ahead. So 'sum' is always the sum of the subarray from i to j.
	// T.C: O(n^2)
	// S.C: O(1)
	public int LongestSubarray_BruteForceOptimised(int[] arr, int k)
	{
		int maxLength = 0;

		for (int i = 0; i < arr.Length; i++)
		{
			int sum = 0;
			for (int j = i; j < arr.Length; j++)
			{
				sum += arr[j];   // sum of elements of the subarray that ranges from 'i' to 'j'
				if (sum == k) maxLength = Math.Max(maxLength, j - i + 1);   // update the maximum length of the subarray whose sum equals k
			}
		}

		return maxLength;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Prefix sum + Dictionary
	// ● Prefix sum at index i = sum of all elements from index 0 to index i.
	//   e.g., arr = [10, 5, 2, 7, 1, -10] -> prefix sums = [10, 15, 17, 24, 25, 15]
	//
	// ● The main idea (reverse engineering):
	//   arr   = [.  .  .  .  .  .  .  .  .  .]
	//   Index:   0  1  2  3  4  5  6  7  8  9
	//   Suppose the prefix sum till index 6 is 'sum', and the prefix sum till index 2 is 'sum - k'.
	//   Then the sum of elements from index 3 to index 6 = sum - (sum - k) = k.
	//   So a subarray with sum k is found, and its length = 6 - 2 = 4.
	//
	// ● Algorithm:
	//   1. Keep a Dictionary 'prefixSum' that stores {prefix sum -> the FIRST index where this prefix sum occurred}.
	//   2. Move through the array and keep adding elements to 'sum' (the prefix sum till index i).
	//      -> If sum == k, the subarray from index 0 to i has sum k. Its length is i + 1.
	//      -> Find rem = sum - k. If 'rem' is present in the dictionary at some index, then the subarray from
	//         (prefixSum[rem] + 1) to i has sum k. Its length = i - prefixSum[rem].
	//      -> Store 'sum' in the dictionary only if it is not already present (see the note below the method for why).
	//   3. Keep the maximum length found.
	//
	// ● Illustration: arr = [1, 3, 1, 1, 1, 1, 1, 5, 4, 2], k = 5
	//                 Index: 0  1  2  3  4  5  6  7  8  9
	//   i = 0: sum = 1.  rem = -4, not in dictionary.            store {1: 0}
	//   i = 1: sum = 4.  rem = -1, not in dictionary.            store {4: 1}
	//   i = 2: sum = 5.  sum == k, so length = 2 + 1 = 3.        store {5: 2}   maxLength = 3
	//   i = 3: sum = 6.  rem = 1, found at index 0. length = 3 - 0 = 3.         store {6: 3}
	//   i = 4: sum = 7.  rem = 2, not in dictionary.             store {7: 4}
	//   i = 5: sum = 8.  rem = 3, not in dictionary.             store {8: 5}
	//   i = 6: sum = 9.  rem = 4, found at index 1. length = 6 - 1 = 5.         store {9: 6}   maxLength = 5
	//   i = 7: sum = 14. rem = 9, found at index 6. length = 7 - 6 = 1.         store {14: 7}
	//   i = 8: sum = 18. rem = 13, not in dictionary.            store {18: 8}
	//   i = 9: sum = 20. rem = 15, not in dictionary.            store {20: 9}
	//   Answer: 5 (the subarray [1, 1, 1, 1, 1] from index 2 to 6).
	//
	// T.C: O(n);   Dictionary operations take O(1) on average.
	// S.C: O(n)
	public int LongestSubarray(int[] arr, int k)
	{
		Dictionary<int, int> prefixSum = new Dictionary<int, int>();
		int sum = 0;
		int maxLength = 0;

		for (int i = 0; i < arr.Length; i++)
		{
			sum += arr[i];

			// If the subarray from index 0 to i has sum k
			if (sum == k) maxLength = Math.Max(maxLength, i + 1);

			int rem = sum - k;
			// If 'rem' is already present in the dictionary, a subarray with sum k ends at the current index 'i'.
			// It starts just after the index where 'rem' occurred. So its length = i - prefixSum[rem].
			if (prefixSum.ContainsKey(rem))
			{
				int length = i - prefixSum[rem];
				maxLength = Math.Max(maxLength, length);
			}

			// Add entry in the dictionary only if this prefix sum is not already present
			if (!prefixSum.ContainsKey(sum)) prefixSum[sum] = i;
		}

		return maxLength;
	}

	// ● Why add an entry in the dictionary only if the prefix sum is not already present ?
	//   It is important when the array contains 0s (or negatives). If we overwrite, the index of a prefix sum gets
	//   replaced by a newer (bigger) index, which gives a SHORTER length, i.e., a wrong answer.
	//   ● Illustration if we overwrite:
	//     arr = [2, 0, 0, 1, 1, 1], k = 3
	//     Index: 0  1  2  3  4  5
	//     - At i = 0, sum = 2, entry: {2: 0}
	//     - At i = 1, sum = 2, entry: {2: 1}   <- overwritten
	//     - At i = 2, sum = 2, entry: {2: 2}   <- overwritten
	//     - At i = 3, sum = 3, sum == k, maxLength = 3 + 1 = 4
	//     - At i = 5, sum = 5, rem = 5 - 3 = 2, found at index 2. length = 5 - 2 = 3. maxLength stays 4.
	//     But that is wrong. The longest subarray is [0, 0, 1, 1, 1] from index 1 to 5, i.e., of length 5.
	//     If we keep the FIRST index of every prefix sum, prefixSum[2] stays 0, and length = 5 - 0 = 5, which is correct.
	//
	// ● NOTE: Don't skip storing the prefix sum when sum == k (e.g., using 'continue'). That prefix sum may be needed later.
	//   e.g., arr = [5, 1, 4], k = 5: at i = 0, sum = 5 (= k). At i = 2, sum = 10, rem = 5. If 5 was never stored, the
	//   subarray [1, 4] (length 2) is missed, and the answer would wrongly be 1.

// ------------------------------------------------------------------------------

	// ● Follow up question: Can you print the longest subarray with sum k ?
	//   Answer: Yes, the code is very simple. Same algorithm, but whenever a longer subarray is found, save its elements.
	// T.C: O(n);   Dictionary operations take O(1) on average.
	// S.C: O(n)
	public List<int> PrintLongestSubarray(int[] arr, int k)
	{
		Dictionary<int, int> prefixSum = new Dictionary<int, int>();
		int sum = 0;
		int maxLength = 0;

		List<int> ans = new List<int>();
		for (int i = 0; i < arr.Length; i++)
		{
			sum += arr[i];

			// If the subarray from index 0 to i has sum k (and it is longer than the best so far)
			if (sum == k && i + 1 > maxLength)
			{
				maxLength = i + 1;
				ans.Clear();   // always clear the answer before adding new elements
				for (int x = 0; x <= i; x++) ans.Add(arr[x]);
			}

			int rem = sum - k;
			if (prefixSum.ContainsKey(rem))
			{
				int length = i - prefixSum[rem];
				if (length > maxLength)
				{
					maxLength = length;
					ans.Clear();
					for (int x = prefixSum[rem] + 1; x <= i; x++) ans.Add(arr[x]);
				}
			}

			// Add entry in the dictionary only if this prefix sum is not already present
			if (!prefixSum.ContainsKey(sum)) prefixSum[sum] = i;
		}

		return ans;
	}

// ------------------------------------------------------------------------------

	// ● Follow up question: If the array contains only positive integers (and 0s), can you further optimise it ?
	//   Answer: Yes, using the sliding window technique (2 pointers). It works only when there are no negatives, because
	//           then adding an element can only increase the sum, and removing an element can only decrease it.
	//   Algorithm:
	//   1. Initialize 2 pointers 'i' and 'j' at index 0. The window [i, j] is the current subarray, and 'sum' is its sum.
	//   2. Move 'j' ahead (add arr[j] to sum) to grow the window.
	//      -> As soon as the sum exceeds k, move 'i' ahead (subtract arr[i] from sum) to shrink the window, until
	//         sum <= k again.
	//      -> If sum == k, a subarray is found. Update maxLength with its length 'j - i + 1'.
	//   3. Keep doing this until 'j' crosses the last index.
	//   Pro tip: Don't run a loop to find the sum of the window again and again. When j moves ahead, add arr[j] to sum;
	//            when i moves ahead, subtract arr[i] from sum.
	// T.C: O(2n) = O(n);   'i' and 'j' each move at most n times
	// S.C: O(1)
	public int LongestSubarray_Positives(int[] arr, int k)
	{
		int n = arr.Length;
		int i = 0, j = 0;
		int sum = arr[0];   // it's given in the problem that at least 1 element will be present in the array
		int maxLength = 0;

		while (j < n)
		{
			// If sum is greater than k, move ahead 'i' pointer until sum becomes less than or equal to k
			while (i <= j && sum > k)
			{
				sum -= arr[i];
				i++;
			}
			if (sum == k) maxLength = Math.Max(maxLength, j - i + 1);   // update the maximum length of the subarray whose sum equals k
			// Move ahead 'j' pointer to include next element in the sum
			j++;
			if (j < n) sum += arr[j];   // to avoid index out of range, check if j is still within the bounds of the array
		}

		return maxLength;
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 2, 0, 0, 1, 1, 1 };
		int k = 3;

		Console.WriteLine(new Solution().LongestSubarray_BruteForce(arr, k));                    // o/p: 5
		Console.WriteLine(new Solution().LongestSubarray_BruteForceOptimised(arr, k));           // o/p: 5
		Console.WriteLine(new Solution().LongestSubarray(arr, k));                               // o/p: 5
		Console.WriteLine(string.Join(" ", new Solution().PrintLongestSubarray(arr, k)));        // o/p: 0 0 1 1 1
		Console.WriteLine(new Solution().LongestSubarray_Positives(arr, k));                     // o/p: 5
	}
}

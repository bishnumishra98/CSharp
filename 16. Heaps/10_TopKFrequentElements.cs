// Leetcode: 347. Top K Frequent Elements   --->   Given an integer array nums and an integer k, return the k most frequent
// elements. The answer can be in any order. It is guaranteed that the answer is unique.
// Follow up: Your algorithm's time complexity must be better than O(n log n).

// Example 1:
// Input: nums = [1,1,1,2,2,3], k = 2
// Output: [1,2]   (1 appears 3 times, 2 appears 2 times, 3 once)

// Example 2:
// Input: nums = [1], k = 1
// Output: [1]

// Note: The C++ file for this problem was empty (only the title). This solution is new.

// Step 1 for every approach: count each value with a Dictionary {value -> count} (see '01. CSharp_Program/17_Dictionary.cs').
// Let m = the number of DIFFERENT values (m <= n).


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	// Brute force: Sort the different values by count (biggest first), take the first k.
	// T.C: O(n + m log m)
	// S.C: O(m)
	public int[] TopKFrequent_Sort(int[] nums, int k)
	{
		return nums.GroupBy(x => x).OrderByDescending(g => g.Count()).Take(k).Select(g => g.Key).ToArray();
	}

// ------------------------------------------------------------------------------

	// Better (heap): Keep the k most frequent values in a MIN heap by count, of size k. When it grows past k, remove the
	// top: the LEAST frequent of them. (Same pattern as the k largest in '07_KthSmallestAndKthLargestElementInArray.cs'.)
	// T.C: O(n + m log k)
	// S.C: O(m);   the dictionary (the heap is only k)
	public int[] TopKFrequent_Heap(int[] nums, int k)
	{
		Dictionary<int, int> count = new Dictionary<int, int>();
		foreach (int x in nums) count[x] = count.GetValueOrDefault(x) + 1;

		PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();   // element = value, priority = its count
		foreach (var (value, c) in count)
		{
			minHeap.Enqueue(value, c);
			if (minHeap.Count > k) minHeap.Dequeue();
		}

		int[] ans = new int[k];
		for (int i = k - 1; i >= 0; i--) ans[i] = minHeap.Dequeue();   // least frequent comes out first, so fill backwards
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal (Bucket sort): A count is always between 1 and n. So make n + 1 "buckets": buckets[c] = the list of values
	// that appear exactly c times. Then walk the buckets from n down to 1, collecting values until we have k.
	// No sorting at all.
	// e.g., [1,1,1,2,2,3]: buckets[3] = [1], buckets[2] = [2], buckets[1] = [3]. From the top: 1, 2. Done.
	// T.C: O(n)
	// S.C: O(n)
	public int[] TopKFrequent(int[] nums, int k)
	{
		Dictionary<int, int> count = new Dictionary<int, int>();
		foreach (int x in nums) count[x] = count.GetValueOrDefault(x) + 1;

		List<int>[] buckets = new List<int>[nums.Length + 1];
		foreach (var (value, c) in count)
		{
			buckets[c] ??= new List<int>();   // make the list only when it is needed
			buckets[c].Add(value);
		}

		List<int> ans = new List<int>();
		for (int c = nums.Length; c >= 1 && ans.Count < k; c--)
		{
			if (buckets[c] == null) continue;
			foreach (int value in buckets[c])
			{
				ans.Add(value);
				if (ans.Count == k) break;
			}
		}
		return ans.ToArray();
	}
}


class Program
{
	static void Main()
	{
		int[] nums = { 1, 1, 1, 2, 2, 3 };

		Console.WriteLine(string.Join(" ", new Solution().TopKFrequent_Sort(nums, 2)));   // o/p: 1 2
		Console.WriteLine(string.Join(" ", new Solution().TopKFrequent_Heap(nums, 2)));   // o/p: 1 2
		Console.WriteLine(string.Join(" ", new Solution().TopKFrequent(nums, 2)));        // o/p: 1 2
	}
}

// Leetcode: 658. Find K Closest Elements   --->   Given a SORTED integer array arr, and two integers k and x, return the k
// integers in the array closest to x. The result must be sorted in ascending order.
// a is closer to x than b if |a - x| < |b - x|, or |a - x| == |b - x| and a < b (on a tie, the SMALLER one wins).

// Example 1:
// Input: arr = [1,2,3,4,5], k = 4, x = 3
// Output: [1,2,3,4]   (1 and 5 are both 2 away, 1 wins the tie)

// Example 2:
// Input: arr = [1,1,2,3,4,5], k = 4, x = -1
// Output: [1,1,2,3]

// Note: The C++ file for this problem was empty (only the title). This solution is new.


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	// Better (heap): Keep the k closest values in a MAX heap, ordered by (distance, value), so the top is the WORST kept
	// value: the farthest one, and on a tie the bigger one. When the heap grows past k, remove it.
	// The tuple priority does the tie rule for us: (2, 5) is "bigger" than (2, 1), so 5 is thrown out before 1.
	// Then sort the k kept values.
	// T.C: O(n log k + k log k)
	// S.C: O(k)
	// It does not use the fact that arr is sorted. The optimal one does.
	public IList<int> FindClosestElements_Heap(int[] arr, int k, int x)
	{
		var worstFirst = Comparer<(int dist, int val)>.Create((a, b) => b.CompareTo(a));
		PriorityQueue<int, (int dist, int val)> maxHeap = new PriorityQueue<int, (int dist, int val)>(worstFirst);

		foreach (int a in arr)
		{
			maxHeap.Enqueue(a, (Math.Abs(a - x), a));
			if (maxHeap.Count > k) maxHeap.Dequeue();
		}

		List<int> ans = new List<int>();
		while (maxHeap.Count > 0) ans.Add(maxHeap.Dequeue());
		ans.Sort();
		return ans;
	}

// ------------------------------------------------------------------------------

	// Optimal (Binary search on the window start): The answer is always k NEIGHBOURS in arr: a window arr[s .. s + k - 1],
	// for some start s between 0 and n - k. Binary search for s.
	// Compare the window starting at mid with the one starting at mid + 1. They share everything except arr[mid] (only in
	// the first) and arr[mid + k] (only in the second). So:
	//   x - arr[mid] > arr[mid + k] - x: arr[mid] is farther than arr[mid + k], so the window must start AFTER mid:
	//                                    low = mid + 1.
	//   otherwise:                       arr[mid] is at least as good (it wins ties, being smaller), so the start is
	//                                    mid or before: high = mid.
	// (No Math.Abs needed: written this way the comparison also works when x is outside the window.)
	// e.g., [1,2,3,4,5], k = 4, x = 3: low 0, high 1. mid 0: x - arr[0] = 2, arr[4] - x = 2. Not >, so high = 0.
	//       Start 0: [1,2,3,4].
	// T.C: O(log(n - k) + k);   the search, plus copying k values
	// S.C: O(1);   not counting the answer
	public IList<int> FindClosestElements(int[] arr, int k, int x)
	{
		int low = 0, high = arr.Length - k;

		while (low < high)
		{
			int mid = low + (high - low) / 2;
			if (x - arr[mid] > arr[mid + k] - x) low = mid + 1;
			else high = mid;
		}

		return arr.Skip(low).Take(k).ToList();
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 1, 2, 3, 4, 5 };

		Console.WriteLine(string.Join(" ", new Solution().FindClosestElements_Heap(arr, 4, 3)));   // o/p: 1 2 3 4
		Console.WriteLine(string.Join(" ", new Solution().FindClosestElements(arr, 4, 3)));        // o/p: 1 2 3 4
	}
}

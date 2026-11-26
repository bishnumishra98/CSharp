// Leetcode: 1962. Remove Stones to Minimize the Total   --->   You are given an array piles, where piles[i] is the number
// of stones in the i-th pile, and an integer k. Do this operation EXACTLY k times:
//   Choose any pile and remove floor(piles[i] / 2) stones from it.
// The same pile can be chosen more than once. Return the minimum possible total number of stones left.

// Example 1:
// Input: piles = [5,4,9], k = 2
// Output: 12
// Explanation: 9 -> 9 - 4 = 5, piles [5,4,5]. Then 5 -> 5 - 2 = 3, piles [3,4,5]. Total 12.

// Example 2:
// Input: piles = [4,3,6,7], k = 3
// Output: 12
// Explanation: 7 -> 4: [4,3,6,4]. 6 -> 3: [4,3,3,4]. 4 -> 2: [2,3,3,4]. Total 12.


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm (Greedy + max heap): Each operation removes HALF of a pile, so to remove the most stones, always pick the
	// BIGGEST pile right now. A max heap gives the biggest pile in O(log n), again and again (the halved pile goes back
	// in, and may still be the biggest).
	// 1. Put all piles in a max heap (built in O(n)).
	// 2. k times: take the biggest, remove floor(x / 2) from it (x - x / 2 with integer division), put it back.
	// 3. Add up what is left.
	// T.C: O(n + k log n)
	// S.C: O(n);   the heap
	// Note: The C++ code pushed the piles one by one (O(n log n), not the O(n) its comment said), and summed by popping
	//       every item (another O(n log n)). Building from a list and summing UnorderedItems are both O(n).
	public int MinStoneSum(int[] piles, int k)
	{
		List<(int, int)> items = new List<(int, int)>();
		foreach (int p in piles) items.Add((p, p));
		PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>(items, Comparer<int>.Create((a, b) => b.CompareTo(a)));

		for (int i = 0; i < k; i++)
		{
			int largest = maxHeap.Dequeue();
			largest -= largest / 2;
			maxHeap.Enqueue(largest, largest);
		}

		int sum = 0;
		foreach (var (pile, _) in maxHeap.UnorderedItems) sum += pile;   // order does not matter for a sum
		return sum;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().MinStoneSum(new int[] { 5, 4, 9 }, 2));   // o/p: 12
	}
}

// GFG: Merge K Sorted Arrays   --->   Given k arrays, each sorted in ascending order, merge them into one sorted array.

// Example:
// Input: arr = [[5, 10, 15, 20], [3, 6], [2, 4, 6, 8]]
// Output: [2, 3, 4, 5, 6, 6, 8, 10, 15, 20]

// For 2 sorted arrays, 2 pointers are enough (the merge step of '07. Sorting/04_MergeSort.cs'). For k arrays, we would
// need k pointers, and finding the smallest of k fronts costs O(k) each time. A min heap finds it in O(log k).
// (Same idea as '11. Linked List/23_MergeKSortedLists.cs', with arrays instead of lists.)
// Let N = the total number of elements.


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	// Brute force: Put everything in one list and sort it. (It ignores that the arrays are already sorted.)
	// T.C: O(N log N)
	// S.C: O(N)
	public List<int> MergeKArrays_Sort(int[][] arr)
	{
		return arr.SelectMany(row => row).OrderBy(x => x).ToList();
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm (min heap of the k "fronts"):
	// 1. Put the FIRST element of every array in a min heap. Store where it came from: (row, col), with the value as its
	//    priority.
	// 2. Take out the smallest. It is the next element of the answer.
	// 3. Put the NEXT element of the same array (row, col + 1) into the heap, if there is one.
	// 4. Repeat until the heap is empty.
	// The heap never holds more than k items (one per array).
	// e.g., heap {2 (row 2), 3 (row 1), 5 (row 0)}: take 2, add 4 from row 2. Take 3, add 6 from row 1. Take 4, add 6 ...
	// Note: The C++ code needed a custom comparator struct for its tuples. In C#, the value is simply passed as the
	//       priority, so no comparator is needed.
	// T.C: O(N log k)
	// S.C: O(k);   the heap (not counting the answer)
	public List<int> MergeKArrays(int[][] arr)
	{
		PriorityQueue<(int row, int col), int> minHeap = new PriorityQueue<(int row, int col), int>();

		for (int r = 0; r < arr.Length; r++)
		{
			if (arr[r].Length > 0) minHeap.Enqueue((r, 0), arr[r][0]);
		}

		List<int> result = new List<int>();
		while (minHeap.Count > 0)
		{
			var (row, col) = minHeap.Dequeue();
			result.Add(arr[row][col]);

			if (col + 1 < arr[row].Length) minHeap.Enqueue((row, col + 1), arr[row][col + 1]);
		}

		return result;
	}
}


class Program
{
	static void Main()
	{
		int[][] arr = { new[] { 5, 10, 15, 20 }, new[] { 3, 6 }, new[] { 2, 4, 6, 8 } };

		Console.WriteLine(string.Join(" ", new Solution().MergeKArrays_Sort(arr)));   // o/p: 2 3 4 5 6 6 8 10 15 20
		Console.WriteLine(string.Join(" ", new Solution().MergeKArrays(arr)));        // o/p: 2 3 4 5 6 6 8 10 15 20
	}
}

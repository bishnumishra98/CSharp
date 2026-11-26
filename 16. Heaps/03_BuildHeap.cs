// Build Heap (Heapify an array)   --->   Given an unsorted array, rearrange it IN PLACE so that it becomes a max heap (or
// a min heap).

// Example: arr = [30, 20, 10, 40, 60, 50]
//   Max heap: [60, 40, 50, 30, 20, 10]
//   Min heap: [10, 20, 30, 40, 60, 50]

// Brute force: Start with an empty heap and Insert every value (see '02_HeapImplementation.cs').
// T.C: O(n log n)

// Better idea: Heapify from the BOTTOM up.
// ● Heapify(i) = the "move down" step of delete: if arr[i] is smaller than its bigger child, swap and keep going down.
//   It fixes node i, but only if both its subtrees are ALREADY heaps.
// ● Leaves (index n / 2 to n - 1) are already heaps of size 1. So start at the last non-leaf (n / 2 - 1) and go
//   backwards to the root, calling Heapify on each. When we reach a node, both its subtrees are already heaps.
// e.g., Max heap of [30, 20, 10, 40, 60, 50], n = 6, start at i = 2:
//       i = 2 (10): child 50 is bigger, swap   -> [30, 20, 50, 40, 60, 10]
//       i = 1 (20): children 40, 60, swap 60   -> [30, 60, 50, 40, 20, 10]
//       i = 0 (30): children 60, 50, swap 60   -> [60, 30, 50, 40, 20, 10]
//                   30 now at i = 1: children 40, 20, swap 40 -> [60, 40, 50, 30, 20, 10]
//
// ● Why is this O(n) and not O(n log n) ? Heapify costs the height of the node, NOT the height of the tree.
//   Half the nodes are leaves (cost 0), a quarter are just above them (cost 1), an eighth cost 2, and so on:
//   n/4 * 1 + n/8 * 2 + n/16 * 3 + ...  =  n * (1/4 + 2/8 + 3/16 + ...)  =  n * 1  =  O(n).
//   Only the few nodes near the top are expensive.


using System;

public class Solution
{
	// Heapify for a MAX heap: move arr[index] down until it is bigger than both children.
	// T.C: O(log n)
	// S.C: O(log n);   recursion stack (a while loop would make it O(1), see '04_HeapSort.cs')
	private void HeapifyMax(int[] arr, int n, int index)
	{
		int largest = index, left = 2 * index + 1, right = 2 * index + 2;

		if (left < n && arr[left] > arr[largest]) largest = left;
		if (right < n && arr[right] > arr[largest]) largest = right;

		if (largest != index)
		{
			(arr[index], arr[largest]) = (arr[largest], arr[index]);
			HeapifyMax(arr, n, largest);   // the swapped-down value may still be too small
		}
	}

	// T.C: O(n)
	// S.C: O(log n);   recursion stack
	public void BuildMaxHeap(int[] arr)
	{
		int n = arr.Length;
		for (int i = n / 2 - 1; i >= 0; i--) HeapifyMax(arr, n, i);
	}

// ------------------------------------------------------------------------------

	// Heapify for a MIN heap: the same, but pick the SMALLER child.
	private void HeapifyMin(int[] arr, int n, int index)
	{
		int smallest = index, left = 2 * index + 1, right = 2 * index + 2;

		if (left < n && arr[left] < arr[smallest]) smallest = left;
		if (right < n && arr[right] < arr[smallest]) smallest = right;

		if (smallest != index)
		{
			(arr[index], arr[smallest]) = (arr[smallest], arr[index]);
			HeapifyMin(arr, n, smallest);
		}
	}

	// T.C: O(n)
	// S.C: O(log n)
	public void BuildMinHeap(int[] arr)
	{
		int n = arr.Length;
		for (int i = n / 2 - 1; i >= 0; i--) HeapifyMin(arr, n, i);
	}
}


class Program
{
	static void Main()
	{
		int[] a = { 30, 20, 10, 40, 60, 50 };
		new Solution().BuildMaxHeap(a);
		Console.WriteLine(string.Join(" ", a));   // o/p: 60 40 50 30 20 10

		int[] b = { 30, 20, 10, 40, 60, 50 };
		new Solution().BuildMinHeap(b);
		Console.WriteLine(string.Join(" ", b));   // o/p: 10 20 30 40 60 50
	}
}

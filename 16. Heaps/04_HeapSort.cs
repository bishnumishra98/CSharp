// Heap Sort   --->   Sort an array in ascending order using a max heap, in place.

// Example: arr = [30, 20, 10, 40, 60, 50]
// Output:  [10, 20, 30, 40, 50, 60]

// Algorithm:
// 1. Build a max heap from the array (see '03_BuildHeap.cs'). Now the biggest value is at arr[0].
// 2. Swap arr[0] with the LAST element of the heap: the biggest value is now at the end, in its final place.
// 3. The heap is now 1 smaller (the end is "sorted", not part of the heap any more). The new root may break the heap,
//    so Heapify(0) on the smaller heap.
// 4. Repeat steps 2 and 3 until the heap has 1 element.
// It is like Selection Sort ('07. Sorting/02_SelectionSort.cs'): pick the biggest and put it at the end. But the heap
// finds the biggest in O(log n) instead of O(n).
// e.g., after building: [60, 40, 50, 30, 20, 10]
//       swap 60 <-> 10, heapify -> [50, 40, 10, 30, 20 | 60]
//       swap 50 <-> 20, heapify -> [40, 30, 10, 20 | 50, 60]
//       ... -> [10, 20, 30, 40, 50, 60]
//
//   Heap used   |  Order       |  In place ?
//   ------------------------------------------------------------
//   Max heap    |  Ascending   |  Yes (biggest goes to the end)
//   Min heap    |  Descending  |  Yes (smallest goes to the end)
//
// ● Heap Sort vs the others: O(n log n) ALWAYS (Quick Sort can hit O(n^2)), and O(1) extra space (Merge Sort needs O(n)).
//   But it is NOT stable (equal values can change order), and in practice it is usually slower than Quick Sort
//   (it jumps around the array, which is bad for the CPU cache).


using System;

public class Solution
{
	// Heapify with a while loop instead of recursion, so no recursion stack is used.
	// Note: The C++ code used a recursive heapify, so its S.C was O(log n). With this loop it is O(1).
	private void Heapify(int[] arr, int n, int index)
	{
		while (true)
		{
			int largest = index, left = 2 * index + 1, right = 2 * index + 2;

			if (left < n && arr[left] > arr[largest]) largest = left;
			if (right < n && arr[right] > arr[largest]) largest = right;

			if (largest == index) return;

			(arr[index], arr[largest]) = (arr[largest], arr[index]);
			index = largest;
		}
	}

	// T.C: O(n log n);   O(n) to build the heap + (n - 1) heapifies of O(log n)
	// S.C: O(1)
	public void HeapSort(int[] arr)
	{
		int n = arr.Length;

		for (int i = n / 2 - 1; i >= 0; i--) Heapify(arr, n, i);   // step 1: build the max heap

		for (int end = n - 1; end > 0; end--)
		{
			(arr[0], arr[end]) = (arr[end], arr[0]);   // step 2: biggest to its final place
			Heapify(arr, end, 0);                      // step 3: the heap is now arr[0 .. end - 1]
		}
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 30, 20, 10, 40, 60, 50 };
		new Solution().HeapSort(arr);
		Console.WriteLine(string.Join(" ", arr));   // o/p: 10 20 30 40 50 60
	}
}

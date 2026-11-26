// Heap Implementation   --->   Build a MAX heap from scratch, stored in an array (List<int> here, so it can grow).
// Operations: Insert, Peek, Delete the root (ExtractMax).
// First read '01_HeapIntroduction.cs' for the index formulas.

// I. Insert: Put the new value at the END (the next free spot keeps the tree complete). It may now be bigger than its
//    parent, so move it UP: while it is bigger than its parent, swap them ("heapify up" / "sift up").
//    e.g., inserting 20, 50, 60, 30, 10, 40 one by one:
//
//    20      50       60         60          60            _ 60 _
//           /        /  \       /  \        /  \          /      \
//         20       20    50   30    50    30    50      30        50
//                             /           / \           / \       /
//                           20          20  10        20  10    40
//
//    e.g., 60 is added as the right child of 50: 60 > 50, swap. Now 60 is the root.
//          30 is added under 20: 30 > 20, swap. 30 < 60, stop.
//    Array: [60, 30, 50, 20, 10, 40]

// II. Delete the root: We can not just remove arr[0] (that leaves a hole at the top). Instead:
//    1. Copy the LAST value to the root, and remove the last spot. (The tree stays complete.)
//    2. The new root is probably too small. Move it DOWN: swap it with its BIGGER child, while that child is bigger
//       ("heapify down" / "sift down"). Why the bigger child ? It becomes the parent of the other child, so it must
//       beat it.
//    e.g., [60, 30, 50, 20, 10, 40]: 40 goes to the root -> [40, 30, 50, 20, 10].
//          Children of 40 are 30 and 50. The bigger is 50 > 40, swap -> [50, 30, 40, 20, 10]. 40 has no children, stop.


using System;
using System.Collections.Generic;

public class MaxHeap
{
	private List<int> arr = new List<int>();

	public int Count => arr.Count;

	// T.C: O(1)
	public int Peek()
	{
		if (arr.Count == 0) throw new InvalidOperationException("Heap is empty");
		return arr[0];
	}

	// T.C: O(log n);   the value moves up at most the height of the tree
	// S.C: O(1)
	public void Insert(int val)
	{
		arr.Add(val);
		int index = arr.Count - 1;

		while (index > 0)
		{
			int parent = (index - 1) / 2;
			if (arr[index] <= arr[parent]) break;   // heap order is fine now

			(arr[index], arr[parent]) = (arr[parent], arr[index]);   // swap
			index = parent;
		}
	}

	// Remove and return the biggest value (the root).
	// T.C: O(log n);   the value moves down at most the height of the tree
	// S.C: O(1)
	public int ExtractMax()
	{
		if (arr.Count == 0) throw new InvalidOperationException("Heap is empty");

		int max = arr[0];
		arr[0] = arr[arr.Count - 1];   // last value to the root
		arr.RemoveAt(arr.Count - 1);   // removing the LAST item of a List is O(1)

		int index = 0, n = arr.Count;
		while (true)
		{
			int left = 2 * index + 1, right = 2 * index + 2;
			int largest = index;

			if (left < n && arr[left] > arr[largest]) largest = left;
			if (right < n && arr[right] > arr[largest]) largest = right;

			if (largest == index) break;   // bigger than both children: done

			(arr[index], arr[largest]) = (arr[largest], arr[index]);
			index = largest;
		}

		return max;
	}

	public override string ToString() => string.Join(" ", arr);
}

// Min heap: the same code with the comparisons flipped (arr[index] >= arr[parent] to stop going up, and pick the
// SMALLER child when going down).


class Program
{
	static void Main()
	{
		MaxHeap heap = new MaxHeap();
		foreach (int x in new int[] { 20, 50, 60, 30, 10, 40 }) heap.Insert(x);

		Console.WriteLine(heap);                // o/p: 60 30 50 20 10 40
		Console.WriteLine(heap.Peek());         // o/p: 60
		Console.WriteLine(heap.ExtractMax());   // o/p: 60
		Console.WriteLine(heap);                // o/p: 50 30 40 20 10
	}
}

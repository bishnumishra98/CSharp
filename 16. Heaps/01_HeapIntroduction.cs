// Heap   --->   A special binary tree with 2 rules:
// I.  Heap order:
//     ● Max heap: every parent >= its children. So the BIGGEST value is always at the root.
//     ● Min heap: every parent <= its children. So the SMALLEST value is always at the root.
// II. Shape: it is a COMPLETE binary tree: every level is full, except maybe the last one, which is filled from the LEFT.
//
//     Max heap:            Min heap:
//         50                   10
//        /  \                 /  \
//      30    40             20    30
//     /  \                 /  \
//   10    20             40    50
//
// ● Unlike a BST, a heap does NOT keep left < right. It only promises "parent beats children". That weaker rule is
//   what makes it fast: we never need the whole order, only the top.

// ● Why "complete" ? Because then the tree fits PERFECTLY in an array, with no gaps and no left / right links.
//   Write the nodes level by level, left to right:
//
//     Index:    0   1   2   3   4
//     Array:  [10, 20, 30, 40, 50]        (the min heap above)
//
//   For the node at index i (0-indexed):
//     Left child  = 2i + 1
//     Right child = 2i + 2
//     Parent      = (i - 1) / 2   (integer division)
//   e.g., 20 is at index 1: its children are at 3 (40) and 4 (50), its parent is at (1 - 1) / 2 = 0 (10).
//   The last NON-leaf node is at index n / 2 - 1. Everything after it is a leaf.
//
// ● Height: a complete tree with n nodes has height log(n). That is why heap operations are O(log n).
//
//   Operation              |  Time
//   ------------------------------------
//   Peek (see min / max)   |  O(1)        the root, arr[0]
//   Insert                 |  O(log n)    add at the end, then move it UP
//   Delete the root        |  O(log n)    move the last value to the root, then move it DOWN
//   Build a heap of n      |  O(n)        (see '03_BuildHeap.cs')
//   Search any value       |  O(n)        a heap is not sorted, only the top is known
//
// ● Where heaps are used: "give me the smallest / biggest, again and again". e.g., top-K problems, merging sorted lists,
//   Dijkstra's shortest path, task scheduling, running medians. C# has one built in: PriorityQueue (see
//   '05_PriorityQueue.cs').


using System;

public class Solution
{
	// Check if an array is a max heap: every parent must be >= its children. Using the index formulas, check every
	// non-leaf i (from 0 to n / 2 - 1) against its children 2i + 1 and 2i + 2 (if they exist).
	// (GFG: Does array represent Heap.)
	// T.C: O(n)
	// S.C: O(1)
	public bool IsMaxHeap(int[] arr)
	{
		int n = arr.Length;

		for (int i = 0; i <= n / 2 - 1; i++)
		{
			int left = 2 * i + 1, right = 2 * i + 2;

			if (arr[i] < arr[left]) return false;                  // a non-leaf always has a left child
			if (right < n && arr[i] < arr[right]) return false;
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		int[] heap = { 50, 30, 40, 10, 20 };   // the max heap above

		int i = 1;   // 30
		Console.WriteLine($"{heap[2 * i + 1]} {heap[2 * i + 2]} {heap[(i - 1) / 2]}");   // o/p: 10 20 50   (children and parent of 30)
		Console.WriteLine(new Solution().IsMaxHeap(heap));                               // o/p: True
	}
}

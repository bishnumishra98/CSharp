// Quick Sort   --->   Sort an array in ascending order.
// Idea: Divide and Conquer.
// 1. Pick one element as the "pivot" (here, the first element).
// 2. Partition: Rearrange the array so that all elements <= pivot are on its left, and all elements > pivot are on its
//    right. Now the pivot is at its FINAL sorted position.
// 3. Repeat the same on the left part and the right part (recursion), until parts have 0 or 1 element.

// How to partition (pivot = arr[low]):
// 1. Put i at low and j at high.
// 2. Move i right until arr[i] > pivot   (i stops at an element that belongs to the RIGHT side).
//    Move j left until arr[j] <= pivot   (j stops at an element that belongs to the LEFT side).
//    If i < j, both are on the wrong sides, so swap them. Repeat until i and j cross.
// 3. Now j is at the last element of the "<= pivot" side. Swap the pivot (arr[low]) with arr[j].
//    The pivot is now at index j, its correct place. Return j.
// e.g., arr = [30, 40, 20, 70, 10], pivot = 30
//       i stops at 40, j stops at 10  ->  swap  ->  [30, 10, 20, 70, 40]
//       i stops at 70, j stops at 20  ->  i > j, stop.
//       Swap pivot with arr[j] = 20   ->  [20, 10, 30, 70, 40]. 30 is at its final place (index 2).

// Video link: https://www.youtube.com/watch?v=WIrA4YexLRQ&t=417s


using System;

public class Solution
{
	// T.C: O(nlogn) average/best,  O(n^2) worst
	//      Worst case: The pivot is always the smallest or largest element, e.g., an already sorted array with the first
	//      element as pivot. Then each partition removes only 1 element. (Picking a random pivot avoids this in practice.)
	// S.C: O(logn) average,  O(n) worst;   only the recursion stack, as it sorts in place (no temp array, unlike merge sort)
	public void QuickSort(int[] arr)
	{
		Sort(arr, 0, arr.Length - 1);
	}

	private void Sort(int[] arr, int low, int high)
	{
		if (low >= high) return;   // 0 or 1 element: already sorted

		int pivotIndex = Partition(arr, low, high);
		Sort(arr, low, pivotIndex - 1);    // sort the left part
		Sort(arr, pivotIndex + 1, high);   // sort the right part
	}

	private int Partition(int[] arr, int low, int high)
	{
		int pivot = arr[low];
		int i = low, j = high;

		while (i < j)
		{
			while (i <= high - 1 && arr[i] <= pivot) i++;   // 'i <= high - 1' keeps i inside the array
			while (j >= low + 1 && arr[j] > pivot) j--;     // 'j >= low + 1' keeps j inside the array
			if (i < j) (arr[i], arr[j]) = (arr[j], arr[i]);
		}

		(arr[low], arr[j]) = (arr[j], arr[low]);   // put the pivot at its correct place
		return j;
	}
}

// ● Note: "Hoare" and "Lomuto" are names of 2 partition METHODS, not of which element is picked as pivot.
//   - Hoare-style (used above): 2 pointers move towards each other from both ends and swap wrong pairs.
//   - Lomuto: 1 pointer scans from left to right, and keeps all smaller elements at the front. Simpler to write,
//             but does more swaps.
//   Any element can be the pivot in either method (first, last, middle or random).

// ● Summary of sorting algorithms:
//   Algorithm   |  Best      |  Average   |  Worst     |  Space     |  Stable ?
//   -----------------------------------------------------------------------------
//   Bubble      |  O(n)      |  O(n^2)    |  O(n^2)    |  O(1)      |  Yes
//   Selection   |  O(n^2)    |  O(n^2)    |  O(n^2)    |  O(1)      |  No
//   Insertion   |  O(n)      |  O(n^2)    |  O(n^2)    |  O(1)      |  Yes
//   Merge       |  O(nlogn)  |  O(nlogn)  |  O(nlogn)  |  O(n)      |  Yes
//   Quick       |  O(nlogn)  |  O(nlogn)  |  O(n^2)    |  O(logn)   |  No
//   Stable = equal elements keep their original order. e.g., sorting students by marks: if Amit and Ravi both have 90,
//   and Amit came first in the input, a stable sort keeps Amit before Ravi.

// ● Built-in sorting in C#:
//   - Array.Sort(arr) / list.Sort(): Uses "Introsort" = quick sort, which switches to heap sort if the recursion gets
//     too deep (so the worst case is O(nlogn)), and insertion sort for small parts. NOT stable.
//   - arr.OrderBy(x => x) (LINQ): Stable. Returns a new sorted sequence instead of changing the array.
//   - Descending: Array.Sort(arr, (a, b) => b.CompareTo(a)), or arr.OrderByDescending(x => x).


class Program
{
	static void Main()
	{
		int[] arr = { 30, 40, 20, 70, 10, 60, 50 };

		new Solution().QuickSort(arr);
		Console.WriteLine(string.Join(" ", arr));   // o/p: 10 20 30 40 50 60 70
	}
}

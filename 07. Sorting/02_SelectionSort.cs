// Selection Sort   --->   Sort an array in ascending order.
// Idea: In each pass, SELECT the smallest element from the unsorted part, and put it at the front of the unsorted part.
//       The sorted part grows from left to right. After pass 1, the smallest is at index 0. After pass 2, the second
//       smallest is at index 1, and so on. So n - 1 passes are enough.
//       (For descending order, select the largest element instead.)

// Illustration: arr = [55, 45, 35, 25, 15], n = 5
// Pass 1: [15, 45, 35, 25, 55]   // smallest (15) is swapped with index 0
// Pass 2: [15, 25, 35, 45, 55]   // next smallest (25) is swapped with index 1
// Pass 3: [15, 25, 35, 45, 55]   // 35 is already at index 2
// Pass 4: [15, 25, 35, 45, 55]   // 45 is already at index 3
// No pass 5 is needed, as the only element left (55) is already at its place.

// Video link: https://www.youtube.com/watch?v=g-PGLbMth_g


using System;

public class Solution
{
	// 1. For every i from 0 to n - 2, assume arr[i] is the smallest: minIndex = i.
	// 2. Check all elements on the right of i. If one is smaller than arr[minIndex], update minIndex.
	// 3. Swap arr[i] and arr[minIndex].
	// Good point: It does at most n - 1 swaps, the fewest among simple sorts. Useful when a swap (write) is costly.
	// T.C: O(n^2);   in every case, as it always scans the whole unsorted part
	// S.C: O(1)
	public void SelectionSort(int[] arr)
	{
		int n = arr.Length;

		for (int i = 0; i < n - 1; i++)
		{
			int minIndex = i;

			for (int j = i + 1; j < n; j++)
			{
				if (arr[j] < arr[minIndex]) minIndex = j;
			}

			(arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);   // swap
		}
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 55, 45, 35, 25, 15 };

		new Solution().SelectionSort(arr);
		Console.WriteLine(string.Join(" ", arr));   // o/p: 15 25 35 45 55
	}
}

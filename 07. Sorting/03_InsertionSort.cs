// Insertion Sort   --->   Sort an array in ascending order.
// Idea: Just like sorting playing cards in your hand. Pick one card at a time (the "key"), and INSERT it at the correct
//       place among the cards on its left, which are already sorted. To make room, shift all bigger cards one step right.

// Illustration: arr = [45, 35, 55, 15, 25], n = 5
// Pass 1: [35, 45, 55, 15, 25]   // key = 35. 45 shifts right, 35 goes to index 0
// Pass 2: [35, 45, 55, 15, 25]   // key = 55. Already bigger than 45, stays
// Pass 3: [15, 35, 45, 55, 25]   // key = 15. 55, 45, 35 shift right, 15 goes to index 0
// Pass 4: [15, 25, 35, 45, 55]   // key = 25. 55, 45, 35 shift right, 25 goes to index 1

// Video link: https://www.youtube.com/watch?v=JU767SDMDvA


using System;

public class Solution
{
	// 1. For every i from 1 to n - 1, take key = arr[i]. Everything on the left of i is already sorted.
	// 2. Start j from i - 1. While arr[j] > key, shift arr[j] one step right (arr[j + 1] = arr[j]) and move j left.
	// 3. Now arr[j] <= key (or j = -1), so the key's place is j + 1. Put it there.
	// Good point: On an already (or almost) sorted array, the while loop hardly runs, so it is very fast.
	//             That's why C#'s own Array.Sort() uses insertion sort for small parts of the array.
	// T.C: O(n^2) worst/average,  O(n) best (already sorted array)
	// S.C: O(1)
	public void InsertionSort(int[] arr)
	{
		int n = arr.Length;

		for (int i = 1; i < n; i++)
		{
			int key = arr[i];
			int j = i - 1;   // last index of the sorted part

			while (j >= 0 && arr[j] > key)
			{
				arr[j + 1] = arr[j];   // shift the bigger element one step right
				j--;
			}

			arr[j + 1] = key;   // insert the key at its correct place
		}
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 45, 35, 55, 15, 25 };

		new Solution().InsertionSort(arr);
		Console.WriteLine(string.Join(" ", arr));   // o/p: 15 25 35 45 55
	}
}

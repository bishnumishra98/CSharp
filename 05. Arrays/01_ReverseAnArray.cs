// Problem: Reverse an array.

// Example:
// Input: arr = [1, 2, 3, 4, 5, 6, 7, 8]
// Output: [8, 7, 6, 5, 4, 3, 2, 1]


using System;

public class Solution
{
	// Brute force algorithm: Use an extra array.
	// 1. Create a new array 'reversedArr' of the same size.
	// 2. Fill it from the end of 'arr', i.e., reversedArr[i] = arr[n - 1 - i].
	// 3. Copy 'reversedArr' back into 'arr'.
	// T.C: O(n)
	// S.C: O(n)
	public void ReverseArray_BruteForce(int[] arr)
	{
		int n = arr.Length;
		int[] reversedArr = new int[n];

		for (int i = 0; i < n; i++)
		{
			reversedArr[i] = arr[n - 1 - i];
		}

		// Copy the reversed array back to the original array
		for (int i = 0; i < n; i++)
		{
			arr[i] = reversedArr[i];
		}
	}

// ------------------------------------------------------------------------------

	// Built-in way: Array.Reverse() reverses the array in-place (it uses two pointers inside).
	// T.C: O(n)
	// S.C: O(1)
	public void ReverseArray_BuiltIn(int[] arr)
	{
		Array.Reverse(arr);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Two pointer approach
	// 1. Keep pointer 'start' at the first index and 'end' at the last index.
	// 2. Swap arr[start] and arr[end], then move start ahead and end back.
	// 3. Stop when they meet.
	// T.C: O(n)
	// S.C: O(1)
	public void ReverseArray(int[] arr)
	{
		int start = 0;
		int end = arr.Length - 1;

		while (start < end)
		{
			// C# has no swap() like C++. This tuple line swaps the 2 values in one step.
			// It is the same as: int temp = arr[start]; arr[start] = arr[end]; arr[end] = temp;
			(arr[start], arr[end]) = (arr[end], arr[start]);
			start++;
			end--;
		}
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8 };

		// new Solution().ReverseArray_BruteForce(arr);
		// new Solution().ReverseArray_BuiltIn(arr);
		new Solution().ReverseArray(arr);

		Console.WriteLine(string.Join(" ", arr));   // o/p: 8 7 6 5 4 3 2 1
	}
}

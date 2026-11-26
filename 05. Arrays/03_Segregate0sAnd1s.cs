// GFG: Segregate 0s and 1s   --->   Given an array arr consisting of only 0's and 1's in random order.
// Modify the array in-place to segregate 0s onto the left side and 1s onto the right side of the array.

// Example 1:
// Input: arr[] = [0, 0, 1, 1, 0]
// Output: [0, 0, 0, 1, 1]
// Explanation: After segregation, all the 0's are on the left and 1's are on the right.

// Example 2:
// Input: arr[] = [1, 1, 1, 1]
// Output: [1, 1, 1, 1]
// Explanation: There are no 0s in the given array, so the modified array is [1, 1, 1, 1].


using System;

public class Solution
{
	// Brute force algorithm 1: Just sort the array.
	// T.C: O(nlogn)
	// S.C: O(1)
	public void Segregate0and1_BruteForce1(int[] arr)
	{
		Array.Sort(arr);
	}

// ------------------------------------------------------------------------------

	// Brute force algorithm 2: Count the 0s and 1s, then write that many 0s first and then that many 1s.
	// T.C: O(2n)
	// S.C: O(1)
	public void Segregate0and1_BruteForce2(int[] arr)
	{
		int zeroCount = 0, oneCount = 0;

		for (int i = 0; i < arr.Length; i++)
		{
			if (arr[i] == 0) zeroCount++;
			else oneCount++;
		}

		// Inserting 0s and 1s on indexes
		int index = 0;
		while (zeroCount > 0)
		{
			arr[index] = 0;
			index++;
			zeroCount--;
		}
		while (oneCount > 0)
		{
			arr[index] = 1;
			index++;
			oneCount--;
		}
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Two pointer approach
	// 1. Initialize low and high on 0 and n - 1 index respectively.
	// 2. Maintain the rule: all elements on the left of low should be 0, and all elements on the right of high should be 1.
	//    -> If arr[low] == 0, it is at the correct place. Move low ahead to check the next element.
	//    -> Else if arr[high] == 1, it is at the correct place. Move high back to check the next element.
	//    -> Else both pointers point to wrong elements (1 on left, 0 on right). So swap them and move both pointers.
	// 3. Continue the process until low reaches high.
	// T.C: O(n)
	// S.C: O(1)
	public void Segregate0and1(int[] arr)
	{
		int low = 0, high = arr.Length - 1;

		while (low < high)
		{
			if (arr[low] == 0) low++;
			else if (arr[high] == 1) high--;
			else
			{
				(arr[low], arr[high]) = (arr[high], arr[low]);   // swap
				low++;
				high--;
			}
		}
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 0, 0, 1, 1, 0 };

		// new Solution().Segregate0and1_BruteForce1(arr);
		// new Solution().Segregate0and1_BruteForce2(arr);
		new Solution().Segregate0and1(arr);

		Console.WriteLine(string.Join(" ", arr));   // o/p: 0 0 0 1 1
	}
}

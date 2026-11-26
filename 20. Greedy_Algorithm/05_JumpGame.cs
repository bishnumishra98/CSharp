// Leetcode: 55. Jump Game   --->   arr[i] is the LONGEST jump you can make from index i (any shorter jump is allowed too).
// Starting at index 0, can you reach the last index ?

// Example 1:
// Input: arr = [1, 2, 0, 3, 0, 0]
// Output: true    (0 -> 1 -> 3 -> 5)

// Example 2:
// Input: arr = [1, 0, 2]
// Output: false   (stuck at index 1)

// Example 3:
// Input: arr = [3, 2, 1, 0, 4]
// Output: false   (every path lands on index 3, which has 0)


using System;

public class Solution
{
	// Algorithm: Keep 'maxReach' = the farthest index reachable so far. Walk i from left to right:
	//   if i > maxReach, index i can not be reached, and so nothing after it: false.
	//   otherwise, from i we can reach i + arr[i]: maxReach = max(maxReach, i + arr[i]).
	//   Once maxReach >= the last index: true.
	// Why is one number enough ? Every index up to maxReach is reachable (we can always make a SHORTER jump), so the set
	// of reachable indexes is always one unbroken block starting at 0.
	// T.C: O(n)
	// S.C: O(1)
	public bool CanJump(int[] arr)
	{
		int maxReach = 0;
		for (int i = 0; i < arr.Length; i++)
		{
			if (i > maxReach) return false;
			maxReach = Math.Max(maxReach, i + arr[i]);
			if (maxReach >= arr.Length - 1) return true;   // early stop
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().CanJump(new int[] { 1, 2, 0, 3, 0, 0 }));   // o/p: True
	}
}

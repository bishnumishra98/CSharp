// Leetcode: 45. Jump Game II   --->   nums[i] is the longest jump from index i. Return the MINIMUM number of jumps to reach
// the last index (it is always reachable).

// Example 1:
// Input: nums = [2,3,1,1,4]
// Output: 2   (0 -> 1 -> 4)

// Example 2:
// Input: nums = [1,2,3,1,2,0,2,5]
// Output: 5   (0 -> 1 -> 2 -> 4 -> 6 -> 7)

// First understand '05_JumpGame.cs'.


using System;

public class Solution
{
	// Algorithm (BFS by levels, without a queue): The indexes reachable with exactly k jumps (and not fewer) form one
	// block [l, r]. Start with [0, 0] (0 jumps). From the block, the next block goes from r + 1 to the FARTHEST index
	// any of its indexes can reach. Each new block costs one more jump. Stop when the block reaches the last index.
	// e.g., [2,3,1,1,4]: block [0,0]. From it the farthest is 0 + 2 = 2 -> block [1,2], jumps 1.
	//       From 1: 1 + 3 = 4, from 2: 3. Farthest 4 -> block [3,4], jumps 2. 4 is the last index. Answer 2.
	// Every index is looked at once (the blocks do not overlap).
	// T.C: O(n)
	// S.C: O(1)
	public int Jump(int[] nums)
	{
		int jumps = 0, l = 0, r = 0;

		while (r < nums.Length - 1)
		{
			int farthest = 0;
			for (int i = l; i <= r; i++) farthest = Math.Max(farthest, i + nums[i]);

			l = r + 1;
			r = farthest;
			jumps++;
		}
		return jumps;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().Jump(new int[] { 1, 2, 3, 1, 2, 0, 2, 5 }));   // o/p: 5
	}
}

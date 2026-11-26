// Leetcode: 287. Find the Duplicate Number   --->   Given an array of integers nums containing n + 1 integers where
// each integer is in the range [1, n] inclusive.
// There is only one repeated number in nums, return this repeated number.
// You must solve the problem without modifying the array nums and using only constant extra space.

// Example 1:
// Input: nums = [1,3,4,2,2]
// Output: 2

// Example 2:
// Input: nums = [3,1,3,4,2]
// Output: 3

// Example 3:
// Input: nums = [3,3,3,3,3]
// Output: 3

// NOTE: The first 3 approaches are easy, but they either modify the array (sorting) or use extra space (Dictionary/HashSet).
//       The last approach (Floyd's cycle detection) follows all the rules: no modifying and only O(1) extra space.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force algorithm: Sort the array. Then duplicate numbers come next to each other, so check neighbours.
	// NOTE: This modifies the array, which the problem does not allow.
	// T.C: O(nlogn) + O(n) = O(nlogn)
	// S.C: O(1)
	public int FindDuplicate_BruteForce(int[] nums)
	{
		Array.Sort(nums);

		for (int i = 0; i < nums.Length - 1; i++)
		{
			if (nums[i] == nums[i + 1]) return nums[i];
		}

		return -1;   // if no duplicates found
	}

// ------------------------------------------------------------------------------

	// Hashing using a Dictionary:
	// If nums[i] is already present as a key in the dictionary, it is the duplicate, so return it.
	// Else, add nums[i] as a key (value can be anything, for instance, let's store i).
	// T.C: O(n)
	// S.C: O(n)
	public int FindDuplicate_Dictionary(int[] nums)
	{
		Dictionary<int, int> map = new Dictionary<int, int>();

		for (int i = 0; i < nums.Length; i++)
		{
			if (map.ContainsKey(nums[i])) return nums[i];
			map[nums[i]] = i;
		}

		return -1;   // if no duplicates found
	}

// ------------------------------------------------------------------------------

	// THE SAME CAN BE DONE USING A HASHSET ALSO (we only need to know "seen or not", so no value is needed).
	// T.C: O(n)
	// S.C: O(n)
	public int FindDuplicate_HashSet(int[] nums)
	{
		HashSet<int> seen = new HashSet<int>();

		for (int i = 0; i < nums.Length; i++)
		{
			if (seen.Contains(nums[i])) return nums[i];
			seen.Add(nums[i]);
		}

		return -1;   // if no duplicates found
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Floyd's cycle detection (slow and fast pointers, also called "tortoise and hare")
	// ● Idea: Think of the array as a chain of jumps: from index i, jump to index nums[i].
	//   nums = [1, 3, 4, 2, 2]
	//   Index:  0  1  2  3  4
	//   Start at index 0: 0 -> 1 -> 3 -> 2 -> 4 -> 2 -> 4 -> 2 ...   (stuck in the loop 2 -> 4 -> 2)
	//   Index 3 and index 4 both jump to 2, because the value 2 is present twice. So the duplicate number is exactly
	//   the place where the loop (cycle) starts. Our job is to find the start of the cycle.
	//   (All values are in [1, n] and there are n + 1 indexes, so every jump lands on a valid index, and a cycle is sure.)
	// ● Algorithm:
	//   Phase 1 (find a meeting point inside the cycle):
	//     Move 'slow' 1 jump at a time and 'fast' 2 jumps at a time. Since there is a cycle, fast will catch up with slow
	//     somewhere inside the cycle (like a faster runner lapping a slower runner on a circular track).
	//   Phase 2 (find the start of the cycle):
	//     Put 'slow' back at the start, and now move BOTH 1 jump at a time. The point where they meet again is the start
	//     of the cycle, i.e., the duplicate number. (It is a math fact: the distance from the start to the cycle entrance
	//     equals the distance from the meeting point to the cycle entrance. Just remember the 2 phases.)
	// Dry run for nums = [1, 3, 4, 2, 2]:
	//   Phase 1: slow = 1, fast = 1 -> (slow = 3, fast = 2) -> (slow = 2, fast = 2)   met at 2
	//   Phase 2: slow = nums[0] = 1, fast = 2 -> (slow = 3, fast = 4) -> (slow = 2, fast = 2)   met at 2 -> answer = 2
	// T.C: O(n)
	// S.C: O(1)
	public int FindDuplicate(int[] nums)
	{
		int slow = nums[0];
		int fast = nums[0];

		// Phase 1: slow moves 1 jump, fast moves 2 jumps, until they meet.
		// do-while is used because slow and fast are equal at the start, and we still want to move them at least once.
		do
		{
			slow = nums[slow];          // 1 jump
			fast = nums[nums[fast]];    // 2 jumps
		} while (slow != fast);

		// Phase 2: put slow back at the start, move both 1 jump at a time. Where they meet is the duplicate.
		slow = nums[0];
		while (slow != fast)
		{
			slow = nums[slow];
			fast = nums[fast];
		}

		return slow;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().FindDuplicate_BruteForce(new int[] { 1, 3, 4, 2, 2 }));   // o/p: 2
		Console.WriteLine(new Solution().FindDuplicate_Dictionary(new int[] { 1, 3, 4, 2, 2 }));   // o/p: 2
		Console.WriteLine(new Solution().FindDuplicate_HashSet(new int[] { 1, 3, 4, 2, 2 }));      // o/p: 2
		Console.WriteLine(new Solution().FindDuplicate(new int[] { 1, 3, 4, 2, 2 }));              // o/p: 2
	}
}

// Leetcode: 60. Permutation Sequence   --->   The set [1, 2, 3, ..., n] contains a total of n! unique permutations.
// By listing and labeling all of the permutations in order, we get the following sequence for n = 3:
// "123", "132", "213", "231", "312", "321"
// Given n and k, return the kth permutation sequence.

// Example 1:
// Input: n = 3, k = 3
// Output: "213"

// Example 2:
// Input: n = 4, k = 9
// Output: "2314"

// Example 3:
// Input: n = 3, k = 1
// Output: "123"

// Constraints: 1 <= n <= 9, 1 <= k <= n!

// Video link: https://www.youtube.com/watch?v=wT7gcXLYoao&t=497s


// Dry run image (in this folder): 12_PermutationSequence.jpg

using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
	// Brute force: Generate permutations in sorted (lexicographic) order, and stop at the kth one.
	// The 'used array' method from '08_Permutations.cs' tries the smallest unused number first, so on [1, 2, ..., n] it
	// already produces permutations in sorted order. No sorting needed. Just count them, and stop at k.
	// T.C: O(n! * n) worst case (when k = n!)
	// S.C: O(n);   recursion depth, 'used' array and the StringBuilder
	public string GetPermutation_BruteForce(int n, int k)
	{
		StringBuilder current = new StringBuilder();
		int count = 0;
		Find(n, k, new bool[n + 1], current, ref count);
		return current.ToString();
	}

	// Returns true when the kth permutation is found (then 'current' holds it, and all calls stop).
	// 'ref count': all calls share the same counter, instead of each call getting its own copy.
	private bool Find(int n, int k, bool[] used, StringBuilder current, ref int count)
	{
		if (current.Length == n)
		{
			count++;
			return count == k;
		}

		for (int d = 1; d <= n; d++)
		{
			if (used[d]) continue;

			used[d] = true;
			current.Append(d);
			if (Find(n, k, used, current, ref count)) return true;
			current.Length--;   // backtrack
			used[d] = false;
		}

		return false;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Jump straight to the answer, one digit at a time (no generating).
	// Idea: The n! permutations are split into n equal groups by their FIRST digit, each group has (n - 1)! permutations.
	//       e.g., n = 4: permutations 1-6 start with 1, 7-12 start with 2, 13-18 with 3, 19-24 with 4  (3! = 6 each).
	//       So the first digit of the kth permutation is in group (k - 1) / (n - 1)!. (k - 1, because we count from 0.)
	//       Then find the position INSIDE that group: (k - 1) % (n - 1)!, and repeat with the remaining digits.
	// e.g., n = 4, k = 9  ->  k = 8 (0-based),  digits = [1, 2, 3, 4],  group size = 3! = 6
	//       8 / 6 = 1 -> digits[1] = 2.  Left: [1, 3, 4].   k = 8 % 6 = 2,  group size = 2! = 2
	//       2 / 2 = 1 -> digits[1] = 3.  Left: [1, 4].      k = 2 % 2 = 0,  group size = 1! = 1
	//       0 / 1 = 0 -> digits[0] = 1.  Left: [4].         k = 0,          group size = 0! = 1
	//       0 / 1 = 0 -> digits[0] = 4.  Left: [].          Answer: "2314"
	// T.C: O(n^2);   n steps, and removing from the list costs up to n
	// S.C: O(n)
	public string GetPermutation(int n, int k)
	{
		List<int> digits = new List<int>();
		int groupSize = 1;   // will become (n - 1)!
		for (int i = 1; i <= n; i++)
		{
			digits.Add(i);
			if (i < n) groupSize *= i;
		}

		k = k - 1;   // 0-based
		StringBuilder sb = new StringBuilder();

		while (true)
		{
			int group = k / groupSize;
			sb.Append(digits[group]);
			digits.RemoveAt(group);

			if (digits.Count == 0) break;

			k = k % groupSize;
			groupSize = groupSize / digits.Count;   // (m - 1)! = m! / m
		}

		return sb.ToString();
	}
}


class Program
{
	static void Main()
	{
		int n = 4, k = 9;

		Console.WriteLine(new Solution().GetPermutation_BruteForce(n, k));   // o/p: 2314
		Console.WriteLine(new Solution().GetPermutation(n, k));              // o/p: 2314
	}
}

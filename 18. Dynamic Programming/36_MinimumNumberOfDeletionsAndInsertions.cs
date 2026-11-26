// GFG: Minimum number of deletions and insertions   --->   Turn string s1 into string s2 using the fewest operations,
// where an operation deletes a character from s1 or inserts a character into s1.

// Example 1:
// Input: s1 = "heap", s2 = "pea"
// Output: 3   (delete 'h' and 'p': "ea". Insert 'p' at the front: "pea".)

// Example 2:
// Input: s1 = "geeksforgeeks", s2 = "geeks"
// Output: 8   (delete the 8 characters of "forgeeks")

// Problem link: https://www.geeksforgeeks.org/problems/minimum-number-of-deletions-and-insertions0209/1

// First understand '31_LongestCommonSubsequence.cs'.
// (Leetcode 583. Delete Operation for Two Strings has the same answer: delete from both strings until they are equal.)


using System;

public class Solution
{
	// Algorithm: The characters we can KEEP are a common subsequence of s1 and s2; keep as many as possible: the LCS.
	// 1. Delete every character of s1 that is not in the LCS:  s1.Length - LCS deletions. Now s1 == LCS.
	// 2. Insert every character of s2 that is not in the LCS:  s2.Length - LCS insertions. Now s1 == s2.
	// e.g., "heap" -> (delete h, p) -> "ea" (the LCS) -> (insert p) -> "pea".   (4 - 2) + (3 - 2) = 3.
	// T.C: O(m * n)
	// S.C: O(n)
	public int MinOperations(string s1, string s2)
	{
		int lcs = Lcs(s1, s2);
		return (s1.Length - lcs) + (s2.Length - lcs);
	}

	// Space-optimized LCS, from '31_LongestCommonSubsequence.cs'.
	private int Lcs(string a, string b)
	{
		int n = b.Length;
		int[] prev = new int[n + 1];
		for (int i = 1; i <= a.Length; i++)
		{
			int[] curr = new int[n + 1];
			for (int j = 1; j <= n; j++)
			{
				curr[j] = a[i - 1] == b[j - 1] ? 1 + prev[j - 1] : Math.Max(prev[j], curr[j - 1]);
			}
			prev = curr;
		}
		return prev[n];
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().MinOperations("heap", "pea"));   // o/p: 3
	}
}

// Leetcode: 516. Longest Palindromic Subsequence   --->   Given a string s, return the length of its longest PALINDROMIC
// subsequence (a subsequence that reads the same forwards and backwards).

// Example 1:
// Input: s = "bbbab"
// Output: 4   ("bbbb")

// Example 2:
// Input: s = "cbbd"
// Output: 2   ("bb")

// First understand '31_LongestCommonSubsequence.cs'.


using System;

public class Solution
{
	// Algorithm (the trick): A palindrome reads the same forwards and backwards. So every palindromic subsequence of s is
	// also a subsequence of reverse(s): it is a COMMON subsequence of both. That gives LPS <= LCS(s, reverse(s)).
	// The other way also holds (a known result): a common subsequence of s and reverse(s) is not always a palindrome
	// itself, but from the longest one a palindrome of the SAME length can always be built. So:
	//   LPS(s) = LCS(s, reverse(s))
	// e.g., s = "bbbab", reverse = "babbb". LCS = "bbbb" (length 4).
	// T.C: O(n^2)
	// S.C: O(n)
	public int LongestPalindromeSubseq(string s)
	{
		char[] chars = s.ToCharArray();
		Array.Reverse(chars);
		return Lcs(s, new string(chars));
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
		Console.WriteLine(new Solution().LongestPalindromeSubseq("bbbab"));   // o/p: 4
	}
}

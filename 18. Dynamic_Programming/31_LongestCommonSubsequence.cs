// Leetcode: 1143. Longest Common Subsequence   --->   Given two strings text1 and text2, return the length of their
// longest COMMON SUBSEQUENCE (LCS), or 0 if there is none.
// A subsequence keeps some characters (maybe none) of a string, in their original order, but not necessarily next to
// each other. e.g., "ace" is a subsequence of "abcde".

// Example 1:
// Input: text1 = "abcde", text2 = "ace"
// Output: 3   ("ace")

// Example 2:
// Input: text1 = "abc", text2 = "abc"
// Output: 3

// Example 3:
// Input: text1 = "abc", text2 = "def"
// Output: 0

// DP on STRINGS: the state is 2 indexes, one in each string. LCS is the base of the next ~10 files.

// The recursion: f(i, j) = the LCS length of text1[0..i] and text2[0..j]. Compare the LAST characters:
//   text1[i] == text2[j]:  they can be matched together:  f(i, j) = 1 + f(i - 1, j - 1)
//   text1[i] != text2[j]:  they can not BOTH be in the LCS. Drop one of them and try both ways:
//                          f(i, j) = max(f(i - 1, j), f(i, j - 1))
//   f(i, j) = 0 if i < 0 or j < 0   (an empty string has no common subsequence)


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: list every subsequence of both strings (2^m and 2^n of them), and find the longest one in both.
	// T.C: O(2^m * 2^n);   comparing every pair (with a HashSet it would be O(2^m + 2^n) times the string length)
	// S.C: O(2^m + 2^n);   storing all subsequences
	public int LongestCommonSubsequence_BruteForce(string text1, string text2)
	{
		List<string> subs1 = new List<string>(), subs2 = new List<string>();
		AllSubsequences(0, text1, "", subs1);
		AllSubsequences(0, text2, "", subs2);

		int best = 0;
		foreach (string a in subs1)
		{
			foreach (string b in subs2)
			{
				if (a == b) best = Math.Max(best, a.Length);
			}
		}
		return best;
	}

	// Take / not take every character (see '10. Recursion').
	private void AllSubsequences(int i, string s, string current, List<string> result)
	{
		if (i == s.Length)
		{
			result.Add(current);
			return;
		}
		AllSubsequences(i + 1, s, current + s[i], result);   // take s[i]
		AllSubsequences(i + 1, s, current, result);          // skip s[i]
	}

// ------------------------------------------------------------------------------

	// T.C: O(2^(m + n));   on a mismatch, 2 calls
	// S.C: O(m + n);       recursion stack
	public int LongestCommonSubsequence_Recursion(string text1, string text2)
	{
		return Lcs(text1.Length - 1, text2.Length - 1, text1, text2);
	}

	private int Lcs(int i, int j, string a, string b)
	{
		if (i < 0 || j < 0) return 0;
		if (a[i] == b[j]) return 1 + Lcs(i - 1, j - 1, a, b);
		return Math.Max(Lcs(i - 1, j, a, b), Lcs(i, j - 1, a, b));
	}

// ------------------------------------------------------------------------------

	// SHIFTED indexes: f(i, j) here means the first i characters of text1 and the first j of text2. Then "empty" is
	// i == 0 or j == 0 instead of a negative index, and the same function works as a table with a row and column 0.
	// T.C: O(m * n)
	// S.C: O(m * n);   dp + O(m + n) recursion stack
	public int LongestCommonSubsequence_Memoization(string text1, string text2)
	{
		int m = text1.Length, n = text2.Length;
		int[,] dp = new int[m + 1, n + 1];
		for (int i = 0; i <= m; i++)
		{
			for (int j = 0; j <= n; j++) dp[i, j] = -1;
		}
		return Lcs(m, n, text1, text2, dp);
	}

	private int Lcs(int i, int j, string a, string b, int[,] dp)
	{
		if (i == 0 || j == 0) return 0;
		if (dp[i, j] != -1) return dp[i, j];

		if (a[i - 1] == b[j - 1]) return dp[i, j] = 1 + Lcs(i - 1, j - 1, a, b, dp);
		return dp[i, j] = Math.Max(Lcs(i - 1, j, a, b, dp), Lcs(i, j - 1, a, b, dp));
	}

// ------------------------------------------------------------------------------

	// The memoization turned into a table. Row 0 and column 0 stay 0 (empty string).
	// e.g., text1 = "abcde", text2 = "ace":
	//              a  c  e
	//           0  0  0  0
	//        a  0  1  1  1
	//        b  0  1  1  1
	//        c  0  1  2  2
	//        d  0  1  2  2
	//        e  0  1  2  3   <- answer
	// T.C: O(m * n)
	// S.C: O(m * n)
	public int LongestCommonSubsequence_Tabulation(string text1, string text2)
	{
		int m = text1.Length, n = text2.Length;
		int[,] dp = new int[m + 1, n + 1];

		for (int i = 1; i <= m; i++)
		{
			for (int j = 1; j <= n; j++)
			{
				if (text1[i - 1] == text2[j - 1]) dp[i, j] = 1 + dp[i - 1, j - 1];
				else dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
			}
		}
		return dp[m, n];
	}

// ------------------------------------------------------------------------------

	// Each row needs only the row above (prev) and the current row (curr).
	// T.C: O(m * n)
	// S.C: O(n)
	public int LongestCommonSubsequence(string text1, string text2)
	{
		int m = text1.Length, n = text2.Length;
		int[] prev = new int[n + 1];

		for (int i = 1; i <= m; i++)
		{
			int[] curr = new int[n + 1];
			for (int j = 1; j <= n; j++)
			{
				if (text1[i - 1] == text2[j - 1]) curr[j] = 1 + prev[j - 1];
				else curr[j] = Math.Max(prev[j], curr[j - 1]);
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
		Console.WriteLine(new Solution().LongestCommonSubsequence_BruteForce("abcde", "ace"));    // o/p: 3
		Console.WriteLine(new Solution().LongestCommonSubsequence_Recursion("abcde", "ace"));     // o/p: 3
		Console.WriteLine(new Solution().LongestCommonSubsequence_Memoization("abcde", "ace"));   // o/p: 3
		Console.WriteLine(new Solution().LongestCommonSubsequence_Tabulation("abcde", "ace"));    // o/p: 3
		Console.WriteLine(new Solution().LongestCommonSubsequence("abcde", "ace"));               // o/p: 3
	}
}

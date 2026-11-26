// Leetcode: 44. Wildcard Matching   --->   Does the pattern p match the WHOLE string s ? In p:
// ● '?' matches any ONE character.
// ● '*' matches any sequence of characters, INCLUDING the empty one.
// ● any other character matches only itself.

// Example 1:
// Input: s = "aa", p = "a"
// Output: false   ("a" does not cover all of "aa")

// Example 2:
// Input: s = "aa", p = "*"
// Output: true

// Example 3:
// Input: s = "cb", p = "?a"
// Output: false   ('?' matches 'c', but 'a' does not match 'b')

// Example 4:
// Input: s = "adceb", p = "*a*b"
// Output: true    (first '*' = "", 'a', second '*' = "dce", 'b')

// First understand '31_LongestCommonSubsequence.cs'.

// The recursion: f(i, j) = does p[0..j] match s[0..i] ? Look at the last pattern character p[j]:
//   p[j] == s[i] or p[j] == '?':  they match each other:   f(i - 1, j - 1)
//   p[j] == '*':  two choices, either one is enough (OR):
//                 '*' matches NOTHING more:              f(i, j - 1)    (drop the '*')
//                 '*' eats s[i] (and maybe more later):  f(i - 1, j)    (keep the '*' for the characters before)
//   otherwise:    false
//   Base cases:
//     both empty:                 true
//     p empty, s not:             false
//     s empty, p not:             true only if what is left of p is all '*' (each one matches "")


using System;

public class Solution
{
	private bool AllStars(string p, int count)
	{
		for (int k = 0; k < count; k++)
		{
			if (p[k] != '*') return false;
		}
		return true;
	}

	// T.C: O(2^(m + n))
	// S.C: O(m + n)
	public bool IsMatch_Recursion(string s, string p)
	{
		return Match(s.Length - 1, p.Length - 1, s, p);
	}

	private bool Match(int i, int j, string s, string p)
	{
		if (i < 0 && j < 0) return true;
		if (j < 0) return false;
		if (i < 0) return AllStars(p, j + 1);

		if (p[j] == s[i] || p[j] == '?') return Match(i - 1, j - 1, s, p);
		if (p[j] == '*') return Match(i, j - 1, s, p) || Match(i - 1, j, s, p);
		return false;
	}

// ------------------------------------------------------------------------------

	// Shifted indexes. dp: -1 = not solved, 0 = false, 1 = true.
	// T.C: O(m * n)
	// S.C: O(m * n)
	// Note: Both C++ memoized versions STORED answers in dp but never READ them (the line 'if (dp[i][j] != -1) return
	//       dp[i][j];' was missing), so they were still exponential.
	public bool IsMatch_Memoization(string s, string p)
	{
		int m = s.Length, n = p.Length;
		int[,] dp = new int[m + 1, n + 1];
		for (int i = 0; i <= m; i++)
		{
			for (int j = 0; j <= n; j++) dp[i, j] = -1;
		}
		return Match(m, n, s, p, dp);
	}

	private bool Match(int i, int j, string s, string p, int[,] dp)
	{
		if (i == 0 && j == 0) return true;
		if (j == 0) return false;
		if (i == 0) return AllStars(p, j);
		if (dp[i, j] != -1) return dp[i, j] == 1;

		bool result;
		if (p[j - 1] == s[i - 1] || p[j - 1] == '?') result = Match(i - 1, j - 1, s, p, dp);
		else if (p[j - 1] == '*') result = Match(i, j - 1, s, p, dp) || Match(i - 1, j, s, p, dp);
		else result = false;

		dp[i, j] = result ? 1 : 0;
		return result;
	}

// ------------------------------------------------------------------------------

	// Row 0 (s empty): dp[0, j] is true while p[0..j - 1] is all '*'. That is simply dp[0, j - 1] && p[j - 1] == '*',
	// one step each: O(n). (The C++ code re-checked the whole prefix for every j: O(n^2).)
	// Column 0 (p empty, s not): false.
	// T.C: O(m * n)
	// S.C: O(m * n)
	public bool IsMatch_Tabulation(string s, string p)
	{
		int m = s.Length, n = p.Length;
		bool[,] dp = new bool[m + 1, n + 1];
		dp[0, 0] = true;
		for (int j = 1; j <= n; j++) dp[0, j] = dp[0, j - 1] && p[j - 1] == '*';

		for (int i = 1; i <= m; i++)
		{
			for (int j = 1; j <= n; j++)
			{
				if (p[j - 1] == s[i - 1] || p[j - 1] == '?') dp[i, j] = dp[i - 1, j - 1];
				else if (p[j - 1] == '*') dp[i, j] = dp[i, j - 1] || dp[i - 1, j];
				// else stays false
			}
		}
		return dp[m, n];
	}

// ------------------------------------------------------------------------------

	// T.C: O(m * n)
	// S.C: O(n)
	public bool IsMatch(string s, string p)
	{
		int m = s.Length, n = p.Length;
		bool[] prev = new bool[n + 1];
		prev[0] = true;
		for (int j = 1; j <= n; j++) prev[j] = prev[j - 1] && p[j - 1] == '*';

		for (int i = 1; i <= m; i++)
		{
			bool[] curr = new bool[n + 1];   // curr[0] = false: p empty, s not
			for (int j = 1; j <= n; j++)
			{
				if (p[j - 1] == s[i - 1] || p[j - 1] == '?') curr[j] = prev[j - 1];
				else if (p[j - 1] == '*') curr[j] = curr[j - 1] || prev[j];
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
		Console.WriteLine(new Solution().IsMatch_Recursion("adceb", "*a*b"));     // o/p: True
		Console.WriteLine(new Solution().IsMatch_Memoization("adceb", "*a*b"));   // o/p: True
		Console.WriteLine(new Solution().IsMatch_Tabulation("adceb", "*a*b"));    // o/p: True
		Console.WriteLine(new Solution().IsMatch("adceb", "*a*b"));               // o/p: True
	}
}

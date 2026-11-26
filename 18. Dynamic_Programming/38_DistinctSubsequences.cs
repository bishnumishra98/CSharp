// Leetcode: 115. Distinct Subsequences   --->   Given strings s and t, return the number of different ways to pick a
// subsequence of s that equals t. (Ways are different if they use different POSITIONS of s.)
// The answer fits in a 32-bit int.

// Example 1:
// Input: s = "rabbbit", t = "rabbit"
// Output: 3   (s has 3 b's, and any 2 of them can be used: 3 ways)

// Example 2:
// Input: s = "babgbag", t = "bag"
// Output: 5

// First understand '31_LongestCommonSubsequence.cs'.

// The recursion: f(i, j) = the number of ways to make t[0..j] from s[0..i].
//   s[i] == t[j]:  two choices:  USE s[i] for t[j]  -> f(i - 1, j - 1)
//                                or do NOT use it (another earlier copy may be used) -> f(i - 1, j)
//                  f(i, j) = f(i - 1, j - 1) + f(i - 1, j)
//   s[i] != t[j]:  s[i] can not be used for t[j]:  f(i, j) = f(i - 1, j)
//   Base cases, in THIS order:
//     j < 0:  all of t is matched: 1 way      (check first: if both run out together, that is a success)
//     i < 0:  s ran out but t did not: 0 ways
// Unlike LCS, t never skips a character: every character of t must be matched.

// Note on overflow: Some in-between counts can be bigger than an int (even bigger than a long) although the final
// answer fits. The code only ADDS, and C# int addition wraps around (mod 2^32) without an error, so the final answer
// still comes out right. (The C++ code had the same situation; in C++ signed overflow is formally not allowed, so
// unsigned or double is often used there.)


using System;

public class Solution
{
	// T.C: O(2^m);   every call moves i, and a match makes 2 calls (the C++ comment said O(2^m * 2^n))
	// S.C: O(m)
	public int NumDistinct_Recursion(string s, string t)
	{
		return Ways(s.Length - 1, t.Length - 1, s, t);
	}

	private int Ways(int i, int j, string s, string t)
	{
		if (j < 0) return 1;
		if (i < 0) return 0;

		if (s[i] == t[j]) return Ways(i - 1, j - 1, s, t) + Ways(i - 1, j, s, t);
		return Ways(i - 1, j, s, t);
	}

// ------------------------------------------------------------------------------

	// Shifted indexes: (i, j) = the first i characters of s and the first j of t.
	// T.C: O(m * n)
	// S.C: O(m * n)
	public int NumDistinct_Memoization(string s, string t)
	{
		int m = s.Length, n = t.Length;
		int[,] dp = new int[m + 1, n + 1];
		for (int i = 0; i <= m; i++)
		{
			for (int j = 0; j <= n; j++) dp[i, j] = -1;
		}
		return Ways(m, n, s, t, dp);
	}

	private int Ways(int i, int j, string s, string t, int[,] dp)
	{
		if (j == 0) return 1;
		if (i == 0) return 0;
		if (dp[i, j] != -1) return dp[i, j];

		if (s[i - 1] == t[j - 1]) return dp[i, j] = Ways(i - 1, j - 1, s, t, dp) + Ways(i - 1, j, s, t, dp);
		return dp[i, j] = Ways(i - 1, j, s, t, dp);
	}

// ------------------------------------------------------------------------------

	// Column 0 is all 1 (t empty: 1 way). Row 0 (except [0, 0]) is 0 (s empty, t not).
	// T.C: O(m * n)
	// S.C: O(m * n)
	public int NumDistinct_Tabulation(string s, string t)
	{
		int m = s.Length, n = t.Length;
		int[,] dp = new int[m + 1, n + 1];
		for (int i = 0; i <= m; i++) dp[i, 0] = 1;

		for (int i = 1; i <= m; i++)
		{
			for (int j = 1; j <= n; j++)
			{
				dp[i, j] = dp[i - 1, j] + (s[i - 1] == t[j - 1] ? dp[i - 1, j - 1] : 0);
			}
		}
		return dp[m, n];
	}

// ------------------------------------------------------------------------------

	// ONE row: cell j needs prev[j] and prev[j - 1]. Go RIGHT to LEFT, so prev[j - 1] is still the old row's value when
	// it is read (the same trick as '24_01Knapsack.cs').
	// T.C: O(m * n)
	// S.C: O(n)
	public int NumDistinct(string s, string t)
	{
		int n = t.Length;
		int[] dp = new int[n + 1];
		dp[0] = 1;

		for (int i = 1; i <= s.Length; i++)
		{
			for (int j = n; j >= 1; j--)
			{
				if (s[i - 1] == t[j - 1]) dp[j] += dp[j - 1];
			}
		}
		return dp[n];
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().NumDistinct_Recursion("babgbag", "bag"));     // o/p: 5
		Console.WriteLine(new Solution().NumDistinct_Memoization("babgbag", "bag"));   // o/p: 5
		Console.WriteLine(new Solution().NumDistinct_Tabulation("babgbag", "bag"));    // o/p: 5
		Console.WriteLine(new Solution().NumDistinct("babgbag", "bag"));               // o/p: 5
	}
}

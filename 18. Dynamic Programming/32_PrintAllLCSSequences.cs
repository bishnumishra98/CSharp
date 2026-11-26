// GFG: Print all LCS sequences   --->   Given two strings s1 and s2, return ALL the DIFFERENT longest common subsequences,
// in sorted (lexicographical) order.

// Example 1:
// Input: s1 = "abaaa", s2 = "baabaca"
// Output: ["aaaa", "abaa", "baaa"]   (the LCS length is 4)

// Example 2:
// Input: s1 = "aaa", s2 = "a"
// Output: ["a"]

// Problem link: https://www.geeksforgeeks.org/problems/print-all-lcs-sequences3413/1

// First understand '31_LongestCommonSubsequence.cs'. That gave the LENGTH. Here we need the strings themselves.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Solution
{
	// Brute force: every subsequence of s1 that is also a subsequence of s2. Keep the longest ones in a SortedSet (no
	// duplicates, sorted).
	// Note: The C++ code added a common subsequence whenever its length equalled the BEST LENGTH SO FAR, and never
	//       removed the shorter ones found earlier (e.g., "a" before "aaaa"). The set must be emptied whenever a longer
	//       one is found.
	// T.C: O(2^m * 2^n * L)
	// S.C: O(2^m + 2^n)
	public List<string> AllLCS_BruteForce(string s1, string s2)
	{
		HashSet<string> subs2 = new HashSet<string>();
		List<string> subs1 = new List<string>();
		AllSubsequences(0, s1, "", subs1);
		List<string> temp = new List<string>();
		AllSubsequences(0, s2, "", temp);
		subs2.UnionWith(temp);

		SortedSet<string> best = new SortedSet<string>(StringComparer.Ordinal);
		int bestLength = 0;
		foreach (string a in subs1)
		{
			if (!subs2.Contains(a) || a.Length < bestLength) continue;
			if (a.Length > bestLength)
			{
				bestLength = a.Length;
				best.Clear();   // the shorter ones are not "longest" any more
			}
			best.Add(a);
		}
		return best.ToList();
	}

	private void AllSubsequences(int i, string s, string current, List<string> result)
	{
		if (i == s.Length)
		{
			result.Add(current);
			return;
		}
		AllSubsequences(i + 1, s, current + s[i], result);
		AllSubsequences(i + 1, s, current, result);
	}

// ------------------------------------------------------------------------------

	// The LCS table, shifted (row and column 0 = empty string), from '31_LongestCommonSubsequence.cs'.
	private int[,] LcsTable(string s1, string s2)
	{
		int m = s1.Length, n = s2.Length;
		int[,] dp = new int[m + 1, n + 1];
		for (int i = 1; i <= m; i++)
		{
			for (int j = 1; j <= n; j++)
			{
				if (s1[i - 1] == s2[j - 1]) dp[i, j] = 1 + dp[i - 1, j - 1];
				else dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
			}
		}
		return dp;
	}

	// ONE LCS (from the C++ code): walk BACK through the table from dp[m, n], asking "where did this value come from ?"
	//   s1[i - 1] == s2[j - 1]:  it came from 1 + dp[i - 1, j - 1]. This character IS in the LCS: add it, go diagonal.
	//   otherwise:               it came from the bigger of dp[i - 1, j] (up) and dp[i, j - 1] (left). Go there.
	// Stop when i or j reaches 0. The characters were found from the END, so add each one at the FRONT.
	// e.g., "abcde" / "ace": (5, 3) e = e, take 'e' -> (4, 2) d != c, go up -> (3, 2) c = c, take 'c' -> (2, 1) b != a,
	//       go up -> (1, 1) a = a, take 'a'. Result "ace".
	// T.C: O(m * n) for the table + O(m + n) for the walk back
	// S.C: O(m * n)
	public string OneLCS(string s1, string s2)
	{
		int[,] dp = LcsTable(s1, s2);
		int i = s1.Length, j = s2.Length;
		StringBuilder sb = new StringBuilder();

		while (i > 0 && j > 0)
		{
			if (s1[i - 1] == s2[j - 1])
			{
				sb.Insert(0, s1[i - 1]);
				i--;
				j--;
			}
			else if (dp[i - 1, j] > dp[i, j - 1]) i--;
			else j--;
		}
		return sb.ToString();
	}

// ------------------------------------------------------------------------------

	// ALL LCS (DP table + backtracking): the walk back above picks ONE way when up and left are equal. To get every LCS,
	// follow EVERY way that keeps the length:
	//   s1[i - 1] == s2[j - 1]:  every LCS of (i - 1, j - 1), with this character added at the end.
	//   otherwise:  the LCS's of (i - 1, j) if dp[i - 1, j] == dp[i, j], PLUS those of (i, j - 1) if dp[i, j - 1] ==
	//               dp[i, j] (both, if both are equal).
	//   i == 0 or j == 0: just the empty string.
	// A HashSet removes duplicates (different paths can spell the same string). The results of each (i, j) are memoized,
	// because the same cell is reached by many paths.
	// T.C: O(m * n * K * L) in the worst case;   K = the number of different LCS's, L = their length
	// S.C: O(m * n * K * L)
	public List<string> AllLCS(string s1, string s2)
	{
		int[,] dp = LcsTable(s1, s2);
		var memo = new Dictionary<(int, int), HashSet<string>>();
		return Collect(s1.Length, s2.Length, s1, s2, dp, memo).OrderBy(s => s, StringComparer.Ordinal).ToList();
	}

	private HashSet<string> Collect(int i, int j, string s1, string s2, int[,] dp, Dictionary<(int, int), HashSet<string>> memo)
	{
		if (i == 0 || j == 0) return new HashSet<string> { "" };
		if (memo.TryGetValue((i, j), out var known)) return known;

		HashSet<string> result = new HashSet<string>();
		if (s1[i - 1] == s2[j - 1])
		{
			foreach (string s in Collect(i - 1, j - 1, s1, s2, dp, memo)) result.Add(s + s1[i - 1]);
		}
		else
		{
			if (dp[i - 1, j] == dp[i, j]) result.UnionWith(Collect(i - 1, j, s1, s2, dp, memo));
			if (dp[i, j - 1] == dp[i, j]) result.UnionWith(Collect(i, j - 1, s1, s2, dp, memo));
		}

		memo[(i, j)] = result;
		return result;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(string.Join(" ", new Solution().AllLCS_BruteForce("abaaa", "baabaca")));   // o/p: aaaa abaa baaa
		Console.WriteLine(new Solution().OneLCS("abaaa", "baabaca"));                                // o/p: baaa
		Console.WriteLine(string.Join(" ", new Solution().AllLCS("abaaa", "baabaca")));              // o/p: aaaa abaa baaa
	}
}

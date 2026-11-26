// GFG: Longest Common Substring   --->   Given two strings s1 and s2, return the length of their longest common
// SUBSTRING. A substring is a CONTINUOUS part of a string (unlike a subsequence, no gaps allowed).

// Example 1:
// Input: s1 = "ABCDGH", s2 = "ACDGHR"
// Output: 4   ("CDGH")

// Example 2:
// Input: s1 = "abc", s2 = "acb"
// Output: 1   ("a", "b" or "c")

// Example 3:
// Input: s1 = "YZ", s2 = "yz"
// Output: 0   (upper and lower case are different characters)

// Problem link: https://www.geeksforgeeks.org/problems/longest-common-substring1452/1

// First understand '31_LongestCommonSubsequence.cs'.


using System;

public class Solution
{
	// Brute force: For every start i in s1 and every start j in s2, count how many characters match from there.
	// T.C: O(m * n * min(m, n));   the inner while loop can run up to min(m, n) times
	// S.C: O(1)
	// Note: The C++ comment said this was O(m * n), "the rare problem where brute force is the best". The inner loop
	//       makes it O(m * n * min(m, n)) in the worst case (e.g., both strings "aaaa...a"), so the DP below is faster.
	public int LongestCommonSubstr_BruteForce(string s1, string s2)
	{
		int best = 0;
		for (int i = 0; i < s1.Length; i++)
		{
			for (int j = 0; j < s2.Length; j++)
			{
				int len = 0;
				while (i + len < s1.Length && j + len < s2.Length && s1[i + len] == s2[j + len]) len++;
				best = Math.Max(best, len);
			}
		}
		return best;
	}

// ------------------------------------------------------------------------------

	// DP: dp[i, j] = the length of the common substring that ENDS exactly at s1[i - 1] and s2[j - 1].
	//   characters equal:  dp[i, j] = 1 + dp[i - 1, j - 1]   (extend the substring ending one step earlier)
	//   characters differ: dp[i, j] = 0                      (a substring can not skip; it is broken here)
	// The only change from LCS is the "differ" case (LCS took max of up / left there). The answer is the BIGGEST value
	// anywhere in the table, not dp[m, n].
	//               A  C  D  G  H  R
	//            0  0  0  0  0  0  0
	//         A  0  1  0  0  0  0  0
	//         B  0  0  0  0  0  0  0
	//         C  0  0  1  0  0  0  0
	//         D  0  0  0  2  0  0  0
	//         G  0  0  0  0  3  0  0
	//         H  0  0  0  0  0  4  0   <- biggest: 4
	// T.C: O(m * n)
	// S.C: O(m * n)
	public int LongestCommonSubstr_Tabulation(string s1, string s2)
	{
		int m = s1.Length, n = s2.Length, best = 0;
		int[,] dp = new int[m + 1, n + 1];

		for (int i = 1; i <= m; i++)
		{
			for (int j = 1; j <= n; j++)
			{
				if (s1[i - 1] == s2[j - 1])
				{
					dp[i, j] = 1 + dp[i - 1, j - 1];
					best = Math.Max(best, dp[i, j]);
				}
				// else dp[i, j] stays 0
			}
		}
		return best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(m * n)
	// S.C: O(n)
	public int LongestCommonSubstr(string s1, string s2)
	{
		int m = s1.Length, n = s2.Length, best = 0;
		int[] prev = new int[n + 1];

		for (int i = 1; i <= m; i++)
		{
			int[] curr = new int[n + 1];   // all 0, which is the "differ" case
			for (int j = 1; j <= n; j++)
			{
				if (s1[i - 1] == s2[j - 1])
				{
					curr[j] = 1 + prev[j - 1];
					best = Math.Max(best, curr[j]);
				}
			}
			prev = curr;
		}
		return best;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().LongestCommonSubstr_BruteForce("ABCDGH", "ACDGHR"));   // o/p: 4
		Console.WriteLine(new Solution().LongestCommonSubstr_Tabulation("ABCDGH", "ACDGHR"));   // o/p: 4
		Console.WriteLine(new Solution().LongestCommonSubstr("ABCDGH", "ACDGHR"));              // o/p: 4
	}
}

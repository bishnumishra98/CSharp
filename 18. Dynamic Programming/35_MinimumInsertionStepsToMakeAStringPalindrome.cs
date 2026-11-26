// Leetcode: 1312. Minimum Insertion Steps to Make a String Palindrome   --->   In one step you can insert any character
// anywhere in the string s. Return the minimum number of steps to make s a palindrome.

// Example 1:
// Input: s = "zzazz"
// Output: 0   (already a palindrome)

// Example 2:
// Input: s = "mbadm"
// Output: 2   ("mbdadbm" or "mdbabdm")

// Example 3:
// Input: s = "leetcode"
// Output: 5   ("leetcodocteel")

// First understand '34_LongestPalindromicSubsequence.cs'.


using System;

public class Solution
{
	// Algorithm: answer = s.Length - LPS(s).
	// Why ? Keep the longest palindromic subsequence where it is: it is already a mirror of itself. Every OTHER character
	// needs a twin on the mirror side, so insert one copy of each. That is (length - LPS) insertions, and no fewer can
	// work (fewer would mean a longer palindromic subsequence existed).
	// e.g., "leetcode": LPS = 3 (e.g., "ete"). Other characters: l, e, c, o, d (5). Insert their twins: 5 steps.
	// T.C: O(n^2)
	// S.C: O(n)
	public int MinInsertions(string s)
	{
		char[] chars = s.ToCharArray();
		Array.Reverse(chars);
		return s.Length - Lcs(s, new string(chars));   // LPS = LCS(s, reverse(s))
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
		Console.WriteLine(new Solution().MinInsertions("leetcode"));   // o/p: 5
	}
}

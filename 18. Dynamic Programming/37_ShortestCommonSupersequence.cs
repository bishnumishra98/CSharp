// Leetcode: 1092. Shortest Common Supersequence   --->   Given two strings str1 and str2, return the SHORTEST string that
// has BOTH of them as subsequences. If there are several, return any.

// Example 1:
// Input: str1 = "abac", str2 = "cab"
// Output: "cabac"   (delete the first 'c' -> "abac";  delete the last "ac" -> "cab")

// Example 2:
// Input: str1 = "aaaaaaaa", str2 = "aaaaaaaa"
// Output: "aaaaaaaa"

// First understand '32_PrintAllLCSSequences.cs' (the OneLCS walk back through the table).


using System;
using System.Text;

public class Solution
{
	// Algorithm: Write both strings, but the characters they have in COMMON only once. To share as many as possible,
	// share the LCS. So the length is m + n - LCS, and the string is built by the same walk back as OneLCS:
	//   s1[i - 1] == s2[j - 1]:   a common character: write it ONCE, go diagonal.
	//   otherwise, going up (i--):   s1[i - 1] is left behind: write it.
	//   otherwise, going left (j--): s2[j - 1] is left behind: write it.
	// When one string runs out, write what is left of the other. Everything was written from the END, so reverse it.
	// e.g., "abac" / "cab":
	//           c  a  b
	//        0  0  0  0
	//     a  0  0  1  1
	//     b  0  0  1  2
	//     a  0  0  1  2
	//     c  0  1  1  2
	//   (4,3) c != b, up is bigger: write 'c', go up. (3,3) a != b: write 'a', up. (2,3) b = b: write 'b', diagonal.
	//   (1,2) a = a: write 'a', diagonal. (0,1): str1 is done, write what is left of str2: 'c'.
	//   Written: c a b a c -> reversed: "cabac".
	// T.C: O(m * n);   table + O(m + n) walk
	// S.C: O(m * n)
	public string ShortestCommonSupersequence(string str1, string str2)
	{
		int m = str1.Length, n = str2.Length;
		int[,] dp = new int[m + 1, n + 1];
		for (int a = 1; a <= m; a++)
		{
			for (int b = 1; b <= n; b++)
			{
				if (str1[a - 1] == str2[b - 1]) dp[a, b] = 1 + dp[a - 1, b - 1];
				else dp[a, b] = Math.Max(dp[a - 1, b], dp[a, b - 1]);
			}
		}

		StringBuilder sb = new StringBuilder();
		int i = m, j = n;
		while (i > 0 && j > 0)
		{
			if (str1[i - 1] == str2[j - 1])
			{
				sb.Append(str1[i - 1]);
				i--;
				j--;
			}
			else if (dp[i - 1, j] > dp[i, j - 1])
			{
				sb.Append(str1[i - 1]);
				i--;
			}
			else
			{
				sb.Append(str2[j - 1]);
				j--;
			}
		}
		while (i > 0) sb.Append(str1[--i]);   // --i: lower i first, then read str1[i]
		while (j > 0) sb.Append(str2[--j]);

		char[] chars = sb.ToString().ToCharArray();
		Array.Reverse(chars);
		return new string(chars);
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().ShortestCommonSupersequence("abac", "cab"));   // o/p: cabac
	}
}

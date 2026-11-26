// Leetcode: 72. Edit Distance   --->   Return the minimum number of operations to turn word1 into word2, where an
// operation is: INSERT a character, DELETE a character, or REPLACE a character.

// Example 1:
// Input: word1 = "horse", word2 = "ros"
// Output: 3   (horse -> rorse (replace h with r) -> rose (delete r) -> ros (delete e))

// Example 2:
// Input: word1 = "intention", word2 = "execution"
// Output: 5

// First understand '31_LongestCommonSubsequence.cs'.

// The recursion: f(i, j) = the fewest operations to turn word1[0..i] into word2[0..j]. Compare the LAST characters:
//   word1[i] == word2[j]:  nothing to do for them:  f(i - 1, j - 1)
//   word1[i] != word2[j]:  1 operation + the best of:
//     Insert word2[j] after word1[i]:   now word2[j] is matched, word1[i] still waits:   f(i, j - 1)
//     Delete word1[i]:                  word2[j] still waits:                            f(i - 1, j)
//     Replace word1[i] with word2[j]:   both are matched:                                f(i - 1, j - 1)
//   Base cases:
//     i < 0 (word1 is empty):  insert all j + 1 characters of word2[0..j]:   j + 1
//     j < 0 (word2 is empty):  delete all i + 1 characters of word1[0..i]:   i + 1
// e.g., "horse" -> "ros": e != s: try all 3 ... the best is 3.


using System;

public class Solution
{
	// T.C: O(3^(m + n))
	// S.C: O(m + n)
	public int MinDistance_Recursion(string word1, string word2)
	{
		return Ops(word1.Length - 1, word2.Length - 1, word1, word2);
	}

	private int Ops(int i, int j, string a, string b)
	{
		if (i < 0) return j + 1;
		if (j < 0) return i + 1;
		if (a[i] == b[j]) return Ops(i - 1, j - 1, a, b);

		int insert = Ops(i, j - 1, a, b), delete = Ops(i - 1, j, a, b), replace = Ops(i - 1, j - 1, a, b);
		return 1 + Math.Min(insert, Math.Min(delete, replace));
	}

// ------------------------------------------------------------------------------

	// Shifted indexes: (i, j) = the first i characters of word1 and the first j of word2. Base cases become
	// i == 0 -> j, and j == 0 -> i.
	// T.C: O(m * n)
	// S.C: O(m * n)
	public int MinDistance_Memoization(string word1, string word2)
	{
		int m = word1.Length, n = word2.Length;
		int[,] dp = new int[m + 1, n + 1];
		for (int i = 0; i <= m; i++)
		{
			for (int j = 0; j <= n; j++) dp[i, j] = -1;
		}
		return Ops(m, n, word1, word2, dp);
	}

	private int Ops(int i, int j, string a, string b, int[,] dp)
	{
		if (i == 0) return j;
		if (j == 0) return i;
		if (dp[i, j] != -1) return dp[i, j];
		if (a[i - 1] == b[j - 1]) return dp[i, j] = Ops(i - 1, j - 1, a, b, dp);

		int insert = Ops(i, j - 1, a, b, dp), delete = Ops(i - 1, j, a, b, dp), replace = Ops(i - 1, j - 1, a, b, dp);
		return dp[i, j] = 1 + Math.Min(insert, Math.Min(delete, replace));
	}

// ------------------------------------------------------------------------------

	// T.C: O(m * n)
	// S.C: O(m * n)
	public int MinDistance_Tabulation(string word1, string word2)
	{
		int m = word1.Length, n = word2.Length;
		int[,] dp = new int[m + 1, n + 1];
		for (int j = 0; j <= n; j++) dp[0, j] = j;
		for (int i = 0; i <= m; i++) dp[i, 0] = i;

		for (int i = 1; i <= m; i++)
		{
			for (int j = 1; j <= n; j++)
			{
				if (word1[i - 1] == word2[j - 1]) dp[i, j] = dp[i - 1, j - 1];
				else dp[i, j] = 1 + Math.Min(dp[i, j - 1], Math.Min(dp[i - 1, j], dp[i - 1, j - 1]));
			}
		}
		return dp[m, n];
	}

// ------------------------------------------------------------------------------

	// Two rows. Each new row starts with curr[0] = i (the base case "word2 is empty: delete i characters").
	// T.C: O(m * n)
	// S.C: O(n)
	public int MinDistance(string word1, string word2)
	{
		int m = word1.Length, n = word2.Length;
		int[] prev = new int[n + 1];
		for (int j = 0; j <= n; j++) prev[j] = j;

		for (int i = 1; i <= m; i++)
		{
			int[] curr = new int[n + 1];
			curr[0] = i;
			for (int j = 1; j <= n; j++)
			{
				if (word1[i - 1] == word2[j - 1]) curr[j] = prev[j - 1];
				else curr[j] = 1 + Math.Min(curr[j - 1], Math.Min(prev[j], prev[j - 1]));
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
		Console.WriteLine(new Solution().MinDistance_Recursion("horse", "ros"));     // o/p: 3
		Console.WriteLine(new Solution().MinDistance_Memoization("horse", "ros"));   // o/p: 3
		Console.WriteLine(new Solution().MinDistance_Tabulation("horse", "ros"));    // o/p: 3
		Console.WriteLine(new Solution().MinDistance("horse", "ros"));               // o/p: 3
	}
}

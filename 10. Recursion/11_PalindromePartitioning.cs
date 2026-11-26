// Leetcode: 131. Palindrome Partitioning   --->   Given a string s, partition s such that every substring of the
// partition is a palindrome. Return all possible palindrome partitioning of s.

// Example 1:
// Input: s = "aab"
// Output: [["a","a","b"],["aa","b"]]

// Example 2:
// Input: s = "a"
// Output: [["a"]]

// Example 3:
// Input: s = "aabb"
// Output: [["a","a","b","b"],["a","a","bb"],["aa","b","b"],["aa","bb"]]


// Dry run image (in this folder): 11_PalindromePartitioning.jpg

using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the partitions

public class Solution
{
	// Algorithm: Make the first cut, then partition the rest the same way (recursion).
	// 1. If index == s.Length, the whole string is cut into palindromes. Save the parts.
	// 2. Else try every end i from 'index' to n - 1 for the next part s[index ... i]:
	//    -> only if that part is a palindrome: add it, partition the rest from i + 1, then backtrack.
	// Recursion tree for "aab":
	//                           "aab"
	//               "a" /                \ "aa"            ("aab" is not a palindrome, so no third branch)
	//                "ab"                  "b"
	//          "a" /                        | "b"
	//            "b"                        ""   -> [aa, b]
	//          "b" |
	//              ""   -> [a, a, b]
	// T.C: O(2^n * n);   a string of length n has 2^(n-1) ways to cut it, and each palindrome check / copy costs n
	// S.C: O(n);   recursion depth and 'current' (ignoring the answer)
	public IList<IList<string>> Partition_Simple(string s)
	{
		List<IList<string>> ans = new List<IList<string>>();
		Solve(0, s, new List<string>(), ans);
		return ans;
	}

	private void Solve(int index, string s, List<string> current, List<IList<string>> ans)
	{
		if (index == s.Length)
		{
			ans.Add(new List<string>(current));
			return;
		}

		for (int i = index; i < s.Length; i++)
		{
			if (!IsPalindrome(s, index, i)) continue;

			current.Add(s.Substring(index, i - index + 1));
			Solve(i + 1, s, current, ans);
			current.RemoveAt(current.Count - 1);   // backtrack
		}
	}

	private bool IsPalindrome(string s, int start, int end)
	{
		while (start < end)
		{
			if (s[start] != s[end]) return false;
			start++;
			end--;
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Pre-compute which substrings are palindromes, so each check is O(1).
	// Problem with the simple version: The same substring is checked again and again in different branches, each time O(n).
	// Solution: Fill a table isPal[i, j] = "is s[i ... j] a palindrome ?" once, before the recursion:
	//           s[i ... j] is a palindrome if the ends match (s[i] == s[j]) AND the inside s[i + 1 ... j - 1] is a
	//           palindrome. Strings of length 1 or 2 have no inside to check (j - i < 2).
	//           Fill i from the END, so isPal[i + 1, j - 1] (a later row) is already filled when we need it.
	//           (Using answers of smaller problems like this is called Dynamic Programming.)
	// T.C: O(n^2) for the table + O(2^n * n) for the recursion (copying the answers still costs n each)
	// S.C: O(n^2);   for the table
	public IList<IList<string>> Partition(string s)
	{
		int n = s.Length;
		bool[,] isPal = new bool[n, n];
		for (int i = n - 1; i >= 0; i--)
		{
			for (int j = i; j < n; j++)
			{
				isPal[i, j] = s[i] == s[j] && (j - i < 2 || isPal[i + 1, j - 1]);
			}
		}

		List<IList<string>> ans = new List<IList<string>>();
		SolveFast(0, s, isPal, new List<string>(), ans);
		return ans;
	}

	private void SolveFast(int index, string s, bool[,] isPal, List<string> current, List<IList<string>> ans)
	{
		if (index == s.Length)
		{
			ans.Add(new List<string>(current));
			return;
		}

		for (int i = index; i < s.Length; i++)
		{
			if (!isPal[index, i]) continue;

			current.Add(s.Substring(index, i - index + 1));
			SolveFast(i + 1, s, isPal, current, ans);
			current.RemoveAt(current.Count - 1);
		}
	}
}


class Program
{
	static void Main()
	{
		string s = "aab";

		var ans1 = new Solution().Partition_Simple(s);
		Console.WriteLine(string.Join(" ", ans1.Select(p => "[" + string.Join(",", p) + "]")));   // o/p: [a,a,b] [aa,b]

		var ans2 = new Solution().Partition(s);
		Console.WriteLine(string.Join(" ", ans2.Select(p => "[" + string.Join(",", p) + "]")));   // o/p: [a,a,b] [aa,b]
	}
}

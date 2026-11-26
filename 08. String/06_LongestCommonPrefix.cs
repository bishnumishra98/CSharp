// Leetcode: 14. Longest Common Prefix   --->   Write a function to find the longest common prefix string amongst an
// array of strings.
// If there is no common prefix, return an empty string "".

// Example 1:
// Input: strs = ["flower","flow","flight"]
// Output: "fl"

// Example 2:
// Input: strs = ["dog","racecar","car"]
// Output: ""
// Explanation: There is no common prefix among the input strings.


using System;

public class Solution
{
	// Horizontal scanning: Start with the whole first string as the prefix, and keep cutting it until it fits every string.
	// 1. prefix = strs[0].
	// 2. For every other string: while it doesn't start with 'prefix', remove the last character of 'prefix'.
	//    If 'prefix' becomes empty, there is no common prefix. Return "".
	// e.g., ["flower", "flow", "flight"]:  "flower" -> "flowe" -> "flow" (fits "flow") -> "flo" -> "fl" (fits "flight")
	// Note: StartsWith(string) compares using the computer's language/culture rules by default, which is slower and can
	//       behave oddly for some characters. StringComparison.Ordinal compares the plain character codes. Prefer it in DSA.
	// T.C: O(n * m);   where n = number of strings, m = length of strs[0] (each StartsWith check costs at most O(m))
	// S.C: O(m);   for the new prefix strings created by Substring
	public string LongestCommonPrefix_Horizontal(string[] strs)
	{
		string prefix = strs[0];

		for (int i = 1; i < strs.Length; i++)
		{
			while (!strs[i].StartsWith(prefix, StringComparison.Ordinal))
			{
				prefix = prefix.Substring(0, prefix.Length - 1);
				if (prefix == "") return "";
			}
		}

		return prefix;
	}

// ------------------------------------------------------------------------------

	// Vertical scanning: Compare all strings column by column (index 0 of every string, then index 1, ...).
	// 1. For every index i of the first string, check strs[j][i] for every other string j.
	// 2. Stop at the first index where a string ends (i == strs[j].Length) or the character differs.
	//    The prefix is strs[0][0 ... i - 1].
	// Better than horizontal: It stops at the shortest string / first mismatch, and doesn't build many temporary strings.
	// e.g., ["flower", "flow", "flight"]:  index 0: f f f,  index 1: l l l,  index 2: o o i (mismatch)  ->  "fl"
	// T.C: O(n * m) worst case, but often much less, as it stops at the first mismatch
	// S.C: O(1)
	public string LongestCommonPrefix(string[] strs)
	{
		for (int i = 0; i < strs[0].Length; i++)
		{
			char c = strs[0][i];

			for (int j = 1; j < strs.Length; j++)
			{
				if (i == strs[j].Length || strs[j][i] != c) return strs[0].Substring(0, i);
			}
		}

		return strs[0];   // the whole first string is common
	}
}


class Program
{
	static void Main()
	{
		string[] strs = { "flower", "flow", "flight" };

		Console.WriteLine(new Solution().LongestCommonPrefix_Horizontal(strs));   // o/p: fl
		Console.WriteLine(new Solution().LongestCommonPrefix(strs));              // o/p: fl
	}
}

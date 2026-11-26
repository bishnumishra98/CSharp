// Leetcode: 28. Find the Index of the First Occurrence in a String   --->   Given two strings needle and haystack, return
// the index of the first occurrence of needle in haystack, or -1 if needle is not part of haystack.

// Example 1:
// Input: haystack = "sadbutsad", needle = "sad"
// Output: 0
// Explanation: "sad" occurs at index 0 and 6. The first occurrence is at index 0, so we return 0.

// Example 2:
// Input: haystack = "leetcode", needle = "leeto"
// Output: -1
// Explanation: "leeto" did not occur in "leetcode", so we return -1.

// Also in this file: Leetcode 1392. Longest Happy Prefix   --->   A string is called a happy prefix if it is a non-empty
// prefix which is also a suffix (excluding itself). Given a string s, return the longest happy prefix of s. Return an
// empty string "" if no such prefix exists.
// Example: Input: s = "ababab",  Output: "abab"   ("abab" is both a prefix and a suffix. They can overlap.)
// (This is exactly the LPS array that KMP needs, so both problems are solved together here.)

// Video links: https://www.youtube.com/watch?v=6gQR8TaFXMw&t=2687s  (KMP)
//              https://www.youtube.com/watch?v=sODA1BzFvsE           (LPS array)


using System;

public class Solution
{
	// Built-in: haystack.IndexOf(needle, StringComparison.Ordinal)
	// Note: Always pass StringComparison.Ordinal. Without it, IndexOf(string) uses the computer's culture rules, which is
	//       slower and can even give surprising answers for some special characters.
	// T.C: O(n * m) worst case;   where n = haystack.Length, m = needle.Length
	// S.C: O(1)
	public int StrStr_BuiltIn(string haystack, string needle)
	{
		return haystack.IndexOf(needle, StringComparison.Ordinal);
	}

// ------------------------------------------------------------------------------

	// Brute force: Try every start index i in haystack, and compare needle character by character.
	// If all m characters match, return i. If a character doesn't match, try the next i.
	// Problem: After a mismatch, it starts again from i + 1, and re-checks characters it has already seen.
	//          e.g., haystack = "aaaaaaab", needle = "aaab": almost every i matches "aaa" and then fails at 'b'.
	// T.C: O(n * m)
	// S.C: O(1)
	public int StrStr_BruteForce(string haystack, string needle)
	{
		int n = haystack.Length, m = needle.Length;

		for (int i = 0; i <= n - m; i++)
		{
			int j = 0;
			while (j < m && haystack[i + j] == needle[j]) j++;
			if (j == m) return i;   // all m characters matched
		}

		return -1;
	}

// ------------------------------------------------------------------------------

	// LPS array (Longest Prefix which is also a Suffix):
	// lps[i] = length of the longest string that is BOTH a prefix of s[0 ... i] and a suffix of s[0 ... i]
	//          (not the whole s[0 ... i] itself).
	//   e.g., s     = a b a b a b
	//         index = 0 1 2 3 4 5
	//         lps   = 0 0 1 2 3 4
	//   lps[4] = 3, because in "ababa", "aba" is both the start and the end.
	//
	// How to build it: Use 2 pointers. 'prefix' = length of the prefix matched so far, 'suffix' = current index.
	// (A single character has no proper prefix, so lps[0] = 0, and we start with prefix = 0, suffix = 1.)
	// Case 1: s[prefix] == s[suffix]  ->  The match grows by 1: lps[suffix] = prefix + 1. Move both ahead.
	// Case 2: They don't match, and prefix == 0  ->  Nothing matches here: lps[suffix] = 0. Move suffix ahead.
	// Case 3: They don't match, and prefix > 0   ->  Try a SHORTER prefix that still matches: prefix = lps[prefix - 1].
	//         (Don't move suffix; compare again.)
	//   ● Why lps[prefix - 1] ? s[0 ... prefix - 1] matched so far. The longest shorter piece of it that is also its own
	//     suffix has length lps[prefix - 1]. That piece still matches the end of our current suffix, so we can continue
	//     from there instead of starting from 0.
	//   e.g., s = "ababac": at 'c', prefix = 4 ("abab"), mismatch. prefix = lps[3] = 2 ("ab"), mismatch.
	//         prefix = lps[1] = 0, mismatch. lps = 0.
	// T.C: O(m);   'suffix' moves m times, and 'prefix' can only go back as much as it went forward
	// S.C: O(m);   for the lps array
	private int[] BuildLps(string s)
	{
		int[] lps = new int[s.Length];
		int prefix = 0, suffix = 1;

		while (suffix < s.Length)
		{
			if (s[prefix] == s[suffix])
			{
				lps[suffix] = prefix + 1;
				prefix++;
				suffix++;
			}
			else if (prefix == 0)
			{
				lps[suffix] = 0;
				suffix++;
			}
			else
			{
				prefix = lps[prefix - 1];
			}
		}

		return lps;
	}

	// Leetcode 1392. Longest Happy Prefix: lps of the last index is the length of the longest prefix that is also a
	// suffix of the WHOLE string.
	// T.C: O(n)
	// S.C: O(n)
	public string LongestPrefix(string s)
	{
		int[] lps = BuildLps(s);
		return s.Substring(0, lps[s.Length - 1]);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: KMP (Knuth-Morris-Pratt).
	// Idea: When a mismatch happens after matching j characters of needle, we already KNOW those j characters (they are
	//       needle[0 ... j - 1]). So we never move i back in haystack. We just move j back to lps[j - 1], because that
	//       many characters at the start of needle are already matched (they are equal to the end of what we matched).
	// 1. i walks over haystack, j walks over needle.
	// 2. If haystack[i] == needle[j]: move both. If j reaches m, needle is found and it starts at i - m.
	// 3. Else if j > 0: j = lps[j - 1] (keep i, compare again).
	// 4. Else (j == 0): nothing matched, move i.
	// e.g., haystack = "ababcabcabababd", needle = "ababd" (lps = 0 0 1 2 0):
	//       "abab" matches, then 'c' != 'd'. j = lps[3] = 2, i.e., "ab" is still matched. Compare 'c' with needle[2] ...
	// T.C: O(n + m);   i never moves back, and building lps is O(m)
	// S.C: O(m);   for the lps array
	public int StrStr(string haystack, string needle)
	{
		int n = haystack.Length, m = needle.Length;
		int[] lps = BuildLps(needle);

		int i = 0, j = 0;
		while (i < n)
		{
			if (haystack[i] == needle[j])
			{
				i++;
				j++;
				if (j == m) return i - m;   // whole needle matched
			}
			else if (j > 0) j = lps[j - 1];
			else i++;
		}

		return -1;
	}
}


class Program
{
	static void Main()
	{
		string haystack = "sadbutsad", needle = "sad";

		Console.WriteLine(new Solution().StrStr_BuiltIn(haystack, needle));      // o/p: 0
		Console.WriteLine(new Solution().StrStr_BruteForce(haystack, needle));   // o/p: 0
		Console.WriteLine(new Solution().LongestPrefix("ababab"));               // o/p: abab
		Console.WriteLine(new Solution().StrStr(haystack, needle));              // o/p: 0
	}
}

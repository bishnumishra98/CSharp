// Leetcode: 890. Find and Replace Pattern   --->   Given a list of strings words and a string pattern, return a list of
// words[i] that match pattern. You may return the answer in any order.
// A word matches the pattern if there exists a permutation of letters p so that after replacing every letter x in the
// pattern with p(x), we get the desired word.
// Recall that a permutation of letters is a bijection from letters to letters: every letter maps to another letter, and
// no two letters map to the same letter.

// Example 1:
// Input: words = ["abc","deq","mee","aqq","dkd","ccc"], pattern = "abb"
// Output: ["mee","aqq"]
// Explanation: "mee" matches the pattern because there is a permutation {a -> m, b -> e, ...}.
// "ccc" does not match the pattern because {a -> c, b -> c, ...} is not a permutation, since a and b map to the same letter.

// Example 2:
// Input: words = ["a","b","c"], pattern = "a"
// Output: ["a","b","c"]


using System;
using System.Collections.Generic;

public class Solution
{
	// Idea: Convert every string into its "shape". Give the first new letter 'a', the next new letter 'b', and so on.
	//       A letter seen before gets the same code as before.
	//       e.g., "mee" -> "abb",  "aqq" -> "abb",  "dkd" -> "aba",  "ccc" -> "aaa",  pattern "abb" -> "abb"
	//       Two strings match if and only if their shapes are equal.
	// Why does this check both directions ? Same shape means: same letters in one are same in the other, AND different
	// letters in one are different in the other. So "ccc" ("aaa") can't match "abb", because a and b would both map to c.
	// (This "first appearance gets the next letter" table is also the whole idea of Leetcode 2325. Decode the Message.)
	// T.C: O(n * k);   where n = words.Length, k = length of each word
	// S.C: O(k);   for the shape strings (the map array always has 26 slots)
	public IList<string> FindAndReplacePattern(string[] words, string pattern)
	{
		string patternShape = GetShape(pattern);

		List<string> ans = new List<string>();
		foreach (string word in words)
		{
			if (GetShape(word) == patternShape) ans.Add(word);
		}
		return ans;
	}

	private string GetShape(string s)
	{
		char[] map = new char[26];   // map[letter] = its code. '\0' (the default char) means "not seen yet"
		char next = 'a';
		char[] shape = new char[s.Length];

		for (int i = 0; i < s.Length; i++)
		{
			int idx = s[i] - 'a';
			if (map[idx] == '\0') map[idx] = next++;   // first time we see this letter: give it the next code
			shape[i] = map[idx];
		}

		return new string(shape);
	}
}


class Program
{
	static void Main()
	{
		string[] words = { "abc", "deq", "mee", "aqq", "dkd", "ccc" };
		string pattern = "abb";

		Console.WriteLine(string.Join(" ", new Solution().FindAndReplacePattern(words, pattern)));   // o/p: mee aqq
	}
}

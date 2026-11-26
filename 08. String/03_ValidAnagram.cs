// Leetcode: 242. Valid Anagram   --->   Given two strings s and t, return true if t is an anagram of s, and false
// otherwise.
// Two strings s and t are anagrams if they contain the same characters with the same frequency.

// Example 1:
// Input: s = "anagram", t = "nagaram"
// Output: true

// Example 2:
// Input: s = "rat", t = "car"
// Output: false

// Constraints:
// • 1 <= s.length, t.length <= 5 * 10^4
// • s and t consist of lowercase English letters.


using System;

public class Solution
{
	// Brute force: Sort both strings. Anagrams become exactly the same string after sorting.
	// e.g., "anagram" -> "aaagmnr",  "nagaram" -> "aaagmnr"  ->  equal
	// In C#, a string can't be sorted directly (immutable), so sort its char array.
	// T.C: O(nlogn + mlogm);   where n = s.Length, m = t.Length
	// S.C: O(n + m);   for the char arrays
	public bool IsAnagram_Sorting(string s, string t)
	{
		if (s.Length != t.Length) return false;

		char[] a = s.ToCharArray();
		char[] b = t.ToCharArray();
		Array.Sort(a);
		Array.Sort(b);

		return new string(a) == new string(b);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Count characters with an int array of size 26.
	// 1. For every character of s, do freq[c - 'a']++. For every character of t, do freq[c - 'a']--.
	// 2. If both have the same characters with the same counts, everything cancels and all 26 counts become 0.
	// (c - 'a' gives the index: 'a' -> 0, 'b' -> 1, ..., 'z' -> 25.)
	// Follow-up: If the strings can have any Unicode characters (not only a-z), use a Dictionary<char, int> instead of
	//            the array, with the same ++ and -- idea.
	// T.C: O(n + m)
	// S.C: O(1);   the array always has 26 slots, no matter how long the strings are
	public bool IsAnagram(string s, string t)
	{
		if (s.Length != t.Length) return false;

		int[] freq = new int[26];
		foreach (char c in s) freq[c - 'a']++;
		foreach (char c in t) freq[c - 'a']--;

		foreach (int count in freq)
		{
			if (count != 0) return false;
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		string s = "anagram", t = "nagaram";

		Console.WriteLine(new Solution().IsAnagram_Sorting(s, t));   // o/p: True
		Console.WriteLine(new Solution().IsAnagram(s, t));           // o/p: True
	}
}

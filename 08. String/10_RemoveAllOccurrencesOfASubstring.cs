// Leetcode: 1910. Remove All Occurrences of a Substring   --->   Given two strings s and part, perform the following
// operation on s until all occurrences of the substring part are removed:
// • Find the leftmost occurrence of the substring part and remove it from s.
// Return s after removing all occurrences of part.
// A substring is a contiguous sequence of characters in a string.

// Example 1:
// Input: s = "daabcbaabcbc", part = "abc"
// Output: "dab"
// Explanation: The following operations are done:
// - s = "daabcbaabcbc", remove "abc" starting at index 2, so s = "dabaabcbc".
// - s = "dabaabcbc", remove "abc" starting at index 4, so s = "dababc".
// - s = "dababc", remove "abc" starting at index 3, so s = "dab".
// Now s has no occurrences of "abc".

// Example 2:
// Input: s = "axxxxyyyyb", part = "xy"
// Output: "ab"


using System;
using System.Text;

public class Solution
{
	// Brute force: Use built-in functions. Find the leftmost 'part', remove it, and repeat until it's not found.
	// • s.IndexOf(part, StringComparison.Ordinal): index of the first occurrence of 'part', or -1 if not found.
	//   (Ordinal = compare plain character codes. Without it, C# uses culture rules, which are slower.)
	// • s.Remove(index, count): a new string without 'count' characters starting at 'index'.
	// T.C: O((n / m) * n * m) = O(n^2) worst case;   up to n/m removals, and each IndexOf can take O(n * m)
	// S.C: O(n);   each Remove() creates a new string
	public string RemoveOccurrences_BuiltIn(string s, string part)
	{
		int index = s.IndexOf(part, StringComparison.Ordinal);

		while (index != -1)
		{
			s = s.Remove(index, part.Length);
			index = s.IndexOf(part, StringComparison.Ordinal);
		}

		return s;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Stack (like '07_RemoveAllAdjacentDuplicatesInString.cs').
	// Problem with brute force: After every removal, it searches the whole string again from the start.
	// Solution: Build the answer one character at a time. After adding a character, check only the END of the answer:
	//           if the last m characters are equal to 'part', remove them right away.
	//           A removal can join 2 halves into a new 'part' (e.g., "a" + "abc" + "bc"), but those halves are already
	//           in the answer, so the next characters' checks catch it.
	// e.g., s = "daabcbaabcbc", part = "abc":
	//       d, da, daa, daab, daabc -> ends with "abc", remove -> da, dab, daba, dabaa, dabaab, dabaabc -> remove -> daba,
	//       dabab, dababc -> remove -> dab
	// T.C: O(n * m);   for every character, compare at most m characters
	// S.C: O(n);   for the answer
	public string RemoveOccurrences(string s, string part)
	{
		StringBuilder sb = new StringBuilder();
		int m = part.Length;

		foreach (char c in s)
		{
			sb.Append(c);

			if (sb.Length >= m && EndsWith(sb, part)) sb.Length -= m;   // remove the last m characters
		}

		return sb.ToString();
	}

	// Do the last characters of sb match 'part' ?
	private bool EndsWith(StringBuilder sb, string part)
	{
		int start = sb.Length - part.Length;
		for (int i = 0; i < part.Length; i++)
		{
			if (sb[start + i] != part[i]) return false;
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		string s = "daabcbaabcbc", part = "abc";

		Console.WriteLine(new Solution().RemoveOccurrences_BuiltIn(s, part));   // o/p: dab
		Console.WriteLine(new Solution().RemoveOccurrences(s, part));           // o/p: dab
	}
}

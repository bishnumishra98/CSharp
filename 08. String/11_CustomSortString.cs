// Leetcode: 791. Custom Sort String   --->   You are given two strings order and s. All the characters of order are
// unique and were sorted in some custom order previously.
// Permute the characters of s so that they match the order that order was sorted. More specifically, if a character x
// occurs before a character y in order, then x should occur before y in the permuted string.
// Return any permutation of s that satisfies this property.

// Example 1:
// Input: order = "cba", s = "abcd"
// Output: "cbad"
// Explanation: "a", "b", "c" appear in order, so the order of "a", "b", "c" should be "c", "b", and "a".
// Since "d" does not appear in order, it can be at any position in the returned string. "dcba", "cdba", "cbda" are
// also valid outputs.

// Example 2:
// Input: order = "bcafg", s = "abcd"
// Output: "bcad"

// Constraints: order and s consist of lowercase English letters.


using System;
using System.Text;

public class Solution
{
	// Sorting with a custom rule: Give every letter a "rank" = its position in 'order'. Then sort s by rank.
	// Letters not in 'order' get rank 26, so they go to the end.
	// e.g., order = "cba": rank[c] = 0, rank[b] = 1, rank[a] = 2, rank[d] = 26  ->  "abcd" sorted by rank = "cbad"
	// Array.Sort(arr, (x, y) => ...) sorts using our own comparison. It must return a negative number if x should come
	// first, a positive number if y should come first, and 0 if they are equal.
	// T.C: O(m + nlogn);   where m = order.Length, n = s.Length
	// S.C: O(n);   for the char array
	public string CustomSortString_Sorting(string order, string s)
	{
		int[] rank = new int[26];
		Array.Fill(rank, 26);
		for (int i = 0; i < order.Length; i++) rank[order[i] - 'a'] = i;

		char[] arr = s.ToCharArray();
		Array.Sort(arr, (x, y) => rank[x - 'a'] - rank[y - 'a']);
		return new string(arr);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Counting (no sorting).
	// 1. Count every letter of s in an int array of size 26.
	// 2. Go through 'order'. For each letter, add it to the answer as many times as it appears in s, and set its count to 0.
	// 3. The letters left (not in 'order') can go anywhere, so add them at the end.
	// e.g., order = "cba", s = "abcd": counts a1 b1 c1 d1.  order -> "c", "cb", "cba".  Leftover d -> "cbad"
	// T.C: O(m + n)
	// S.C: O(1);   the count array always has 26 slots (ignoring the answer)
	public string CustomSortString(string order, string s)
	{
		int[] freq = new int[26];
		foreach (char c in s) freq[c - 'a']++;

		StringBuilder sb = new StringBuilder();
		foreach (char c in order)
		{
			sb.Append(c, freq[c - 'a']);   // Append(c, count) adds 'c' count times
			freq[c - 'a'] = 0;
		}

		for (int i = 0; i < 26; i++)
		{
			sb.Append((char)('a' + i), freq[i]);   // leftover letters
		}

		return sb.ToString();
	}
}


class Program
{
	static void Main()
	{
		string order = "cba", s = "abcd";

		Console.WriteLine(new Solution().CustomSortString_Sorting(order, s));   // o/p: cbad
		Console.WriteLine(new Solution().CustomSortString(order, s));           // o/p: cbad
	}
}

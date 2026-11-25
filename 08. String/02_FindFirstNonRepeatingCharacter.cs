// Problem: Find the first character in a string that does not repeat (appears only once).
//          If every character repeats, return '_'.
//          (Assume the string has only small letters a-z.)

// Example 1:
// Input: str = "leetcode"
// Output: 'l'          ('l' appears only once, and it comes first)

// Example 2:
// Input: str = "loveleetcode"
// Output: 'v'          ('l' and 'o' repeat, 'v' is the first one that appears only once)

// Example 3:
// Input: str = "aabb"
// Output: '_'          (every character repeats)

// NOTE: "First" means first in the string from left to right, not first in alphabetical order.
//       So after counting, we must go through the string again in its original order.


using System;
using System.Collections.Generic;   // for Dictionary<TKey, TValue> used in FirstUnique_Dictionary
using System.Linq;                  // for GroupBy(), FirstOrDefault() and Count() used in FirstUnique_LINQ

public class Solution
{
	// Naive approach: For every character, check the whole string to see if it appears anywhere else.
	// 1. Pick each character str[i].
	// 2. Run an inner loop over the string. If the same character is found at another index j, it repeats.
	// 3. The first character that doesn't repeat is the answer.
	// Shortcoming: For every character we scan the whole string again. So for a long string, this becomes slow.
	// T.C: O(n^2)
	// S.C: O(1)
	public char FirstUnique_Naive(string str)
	{
		for (int i = 0; i < str.Length; i++)
		{
			bool repeated = false;

			for (int j = 0; j < str.Length; j++)
			{
				if (i != j && str[i] == str[j])
				{
					repeated = true;
					break;
				}
			}

			if (!repeated)
				return str[i];
		}

		return '_';
	}

// ------------------------------------------------------------------------------

	// Better approach: Count every character first using a Dictionary, then find the first one with count 1.
	// 1. Loop 1: For each character, increase its count in the dictionary. e.g., "loveleetcode" -> l:2, o:2, v:1, e:4...
	// 2. Loop 2: Go through the string again (in original order) and return the first character with count 1.
	// T.C: O(n);   2 separate loops, not a loop inside a loop
	// S.C: O(k);   k = number of different characters in the string
	public char FirstUnique_Dictionary(string str)
	{
		Dictionary<char, int> count = new Dictionary<char, int>();

		foreach (char c in str)
		{
			if (count.ContainsKey(c))
				count[c]++;
			else
				count[c] = 1;
		}

		foreach (char c in str)
		{
			if (count[c] == 1)
				return c;
		}

		return '_';
	}

// ------------------------------------------------------------------------------

	// Best approach: Same idea, but use an int array of size 26 instead of a Dictionary.
	// Since there are only 26 small letters, count[0] is for 'a', count[1] is for 'b'... count[25] is for 'z'.
	// c - 'a' gives the index: 'a' - 'a' = 0, 'b' - 'a' = 1, 'z' - 'a' = 25 (characters are numbers inside).
	// An array is faster than a Dictionary because there is no hashing, just direct index access.
	// T.C: O(n)
	// S.C: O(1);   the array is always size 26, no matter how long the string is
	public char FirstUnique(string str)
	{
		int [] count = new int[26];

		foreach (char c in str)
		{
			count[c - 'a']++;
		}

		foreach (char c in str)
		{
			if (count[c - 'a'] == 1)
				return c;
		}

		return '_';
	}

// ------------------------------------------------------------------------------

	// LINQ way: A string is IEnumerable<char>, so LINQ methods work on it.
	// 1. GroupBy(c => c) puts same characters into one group. e.g., "loveleetcode" -> [l,l] [o,o] [v] [e,e,e,e]...
	//    Groups come out in the order the character first appears in the string, so "first" order is kept.
	// 2. FirstOrDefault(g => g.Count() == 1) picks the first group that has only 1 character.
	//    If no group has count 1, it gives null.
	// 3. group.Key is the character of that group.
	// Shortcoming: Shortest code, but slower than the array approach because it creates a group (a small list) for
	//              every different character.
	// T.C: O(n)
	// S.C: O(n)
	public char FirstUnique_LINQ(string str)
	{
		var group = str.GroupBy(c => c).FirstOrDefault(g => g.Count() == 1);

		return group == null ? '_' : group.Key;
	}
}


class Program
{
	static void Main()
	{
		string str = "loveleetcode";

		// char ans = new Solution().FirstUnique_Naive(str);
		// char ans = new Solution().FirstUnique_Dictionary(str);
		char ans = new Solution().FirstUnique(str);
		// char ans = new Solution().FirstUnique_LINQ(str);

		Console.WriteLine(ans);   // o/p: v
	}
}

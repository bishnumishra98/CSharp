// Leetcode: 49. Group Anagrams   --->   Given an array of strings strs, group the anagrams together. You can return the
// answer in any order.

// Example 1:
// Input: strs = ["eat","tea","tan","ate","nat","bat"]
// Output: [["bat"],["nat","tan"],["ate","eat","tea"]]
// Explanation:
// There is no string in strs that can be rearranged to form "bat".
// The strings "nat" and "tan" are anagrams as they can be rearranged to form each other.
// The strings "ate", "eat", and "tea" are anagrams as they can be rearranged to form each other.

// Example 2:
// Input: strs = [""]
// Output: [[""]]

// Example 3:
// Input: strs = ["a"]
// Output: [["a"]]

// Constraints: strs[i] consists of lowercase English letters.
// First understand '03_ValidAnagram.cs'.


using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the groups
using System.Text;

public class Solution
{
	// Same as IsAnagram() in '03_ValidAnagram.cs'.
	private bool IsAnagram(string s, string t)
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

	// Brute force: Compare every pair of strings.
	// 1. Keep a 'used' array, so a string is put in only one group.
	// 2. For every string i that is not used yet, start a new group with it. Then add every later string j that is an
	//    anagram of it, and mark j as used.
	// T.C: O(n^2 * k);   where n = strs.Length, k = length of the longest string
	// S.C: O(n);   for the 'used' array (ignoring the answer)
	public IList<IList<string>> GroupAnagrams_BruteForce(string[] strs)
	{
		int n = strs.Length;
		bool[] used = new bool[n];
		List<IList<string>> ans = new List<IList<string>>();

		for (int i = 0; i < n; i++)
		{
			if (used[i]) continue;

			List<string> group = new List<string> { strs[i] };
			for (int j = i + 1; j < n; j++)
			{
				if (!used[j] && IsAnagram(strs[i], strs[j]))
				{
					group.Add(strs[j]);
					used[j] = true;
				}
			}
			ans.Add(group);
		}

		return ans;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Hashing with a sorted key.
	// Idea: Anagrams look the same after sorting their letters: "eat", "tea", "ate" -> "aet". So use the sorted string as a
	//       KEY in a Dictionary, and keep a list of all strings with that key.
	//       Key     |  Value (group)
	//       ------------------------------
	//       "aet"   |  eat, tea, ate
	//       "ant"   |  tan, nat
	//       "abt"   |  bat
	// T.C: O(n * klogk);   sorting each string
	// S.C: O(n * k);   for the Dictionary
	public IList<IList<string>> GroupAnagrams_SortedKey(string[] strs)
	{
		Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();

		foreach (string s in strs)
		{
			char[] arr = s.ToCharArray();
			Array.Sort(arr);
			string key = new string(arr);

			if (!groups.ContainsKey(key)) groups[key] = new List<string>();
			groups[key].Add(s);
		}

		return new List<IList<string>>(groups.Values);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Hashing with a count key (no sorting).
	// Problem with the sorted key: Sorting each string costs O(klogk).
	// Solution: Anagrams have the same letter counts. So build the key from the 26 counts in O(k).
	//           e.g., "eat" -> counts a1 e1 t1 -> key "#1#0#0#0#1#0...#1#0..." (one "#count" for each of the 26 letters)
	// Why the '#' ? Without it, different counts can join into the same text. e.g., counts (1, 11) -> "111" and
	//               (11, 1) -> "111". With '#': "#1#11" and "#11#1", which are different.
	// T.C: O(n * k);   counting is O(k), and the key is always 26 numbers long
	// S.C: O(n * k);   for the Dictionary
	public IList<IList<string>> GroupAnagrams(string[] strs)
	{
		Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();

		foreach (string s in strs)
		{
			int[] freq = new int[26];
			foreach (char c in s) freq[c - 'a']++;

			StringBuilder sb = new StringBuilder();
			foreach (int count in freq) sb.Append('#').Append(count);
			string key = sb.ToString();

			if (!groups.ContainsKey(key)) groups[key] = new List<string>();
			groups[key].Add(s);
		}

		return new List<IList<string>>(groups.Values);
	}
}


class Program
{
	static void Main()
	{
		string[] strs = { "eat", "tea", "tan", "ate", "nat", "bat" };

		// Groups are separated by " | ". (Any order of groups is accepted by Leetcode.)
		var ans1 = new Solution().GroupAnagrams_BruteForce(strs);
		Console.WriteLine(string.Join(" | ", ans1.Select(g => string.Join(" ", g))));   // o/p: eat tea ate | tan nat | bat

		var ans2 = new Solution().GroupAnagrams_SortedKey(strs);
		Console.WriteLine(string.Join(" | ", ans2.Select(g => string.Join(" ", g))));   // o/p: eat tea ate | tan nat | bat

		var ans3 = new Solution().GroupAnagrams(strs);
		Console.WriteLine(string.Join(" | ", ans3.Select(g => string.Join(" ", g))));   // o/p: eat tea ate | tan nat | bat
	}
}

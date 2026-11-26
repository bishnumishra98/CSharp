// Leetcode 3. Longest Substring Without Repeating Characters   --->   Given a string s, find the length of the longest
// substring without duplicate characters.

// Example 1:
// Input: s = "abcabcbb"
// Output: 3
// Explanation: The answer is "abc", with the length of 3. Note that "bca" and "cab" are also correct answers.

// Example 2:
// Input: s = "bbbbb"
// Output: 1
// Explanation: The answer is "b", with the length of 1.

// Example 3:
// Input: s = "pwwkew"
// Output: 3
// Explanation: The answer is "wke", with the length of 3.
// Notice that the answer must be a substring, "pwke" is a subsequence and not a substring.

// Constraints: s consists of English letters, digits, symbols and spaces. All of these are ASCII characters (codes 0 to
// 127), so an array of size 128 can store one slot per character.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: Start a substring at every index i, and extend it to the right until a character repeats.
	// A bool array 'seen' remembers which characters are already in the current substring.
	// T.C: O(n^2)
	// S.C: O(128) = O(1)
	public int LengthOfLongestSubstring_BruteForce(string s)
	{
		int maxLen = 0;

		for (int i = 0; i < s.Length; i++)
		{
			bool[] seen = new bool[128];
			for (int j = i; j < s.Length; j++)
			{
				if (seen[s[j]]) break;   // s[j] repeats, so this substring can't grow any more
				seen[s[j]] = true;
				maxLen = Math.Max(maxLen, j - i + 1);
			}
		}

		return maxLen;
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Sliding window with a HashSet.
	// Problem with brute force: For every new start, it rebuilds the substring from scratch.
	// Solution: Keep a window [left, right] that has no repeats, and the set of its characters.
	// 1. Move 'right' one step at a time to add s[right].
	// 2. If s[right] is already in the set, shrink the window from the left (remove s[left], left++) until it is gone.
	// 3. Add s[right], and update the answer with the window size (right - left + 1).
	// e.g., "abcabcbb": [abc] -> 'a' repeats: remove 'a' -> [bca] -> 'b' repeats: remove 'b' -> [cab] -> ...  max = 3
	// T.C: O(2n) = O(n);   each character is added once and removed at most once
	// S.C: O(k);   where k = number of different characters (at most 128)
	public int LengthOfLongestSubstring_Better(string s)
	{
		HashSet<char> window = new HashSet<char>();
		int left = 0, maxLen = 0;

		for (int right = 0; right < s.Length; right++)
		{
			while (window.Contains(s[right]))
			{
				window.Remove(s[left]);
				left++;
			}

			window.Add(s[right]);
			maxLen = Math.Max(maxLen, right - left + 1);
		}

		return maxLen;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Sliding window that JUMPS.
	// Problem with the HashSet version: When a repeat comes, 'left' moves one step at a time.
	// Solution: Remember the last index where each character was seen. When s[right] repeats inside the window, jump
	//           'left' directly to (its last index + 1).
	// ● Why check 'lastIndex[c] >= left' ? If the last time we saw c was BEFORE the window started, it is not inside the
	//   window, so it is not a repeat, and 'left' must not move back.
	//   e.g., "abba": at the last 'a', lastIndex['a'] = 0, but left is already 2. Moving left to 1 would be wrong.
	// T.C: O(n);   one pass
	// S.C: O(128) = O(1)
	public int LengthOfLongestSubstring(string s)
	{
		int[] lastIndex = new int[128];
		Array.Fill(lastIndex, -1);   // -1 = not seen yet

		int left = 0, maxLen = 0;
		for (int right = 0; right < s.Length; right++)
		{
			char c = s[right];
			if (lastIndex[c] >= left) left = lastIndex[c] + 1;   // repeat inside the window: jump past it

			lastIndex[c] = right;
			maxLen = Math.Max(maxLen, right - left + 1);
		}

		return maxLen;
	}
}


class Program
{
	static void Main()
	{
		string s = "abcabcbb";

		Console.WriteLine(new Solution().LengthOfLongestSubstring_BruteForce(s));   // o/p: 3
		Console.WriteLine(new Solution().LengthOfLongestSubstring_Better(s));       // o/p: 3
		Console.WriteLine(new Solution().LengthOfLongestSubstring(s));              // o/p: 3
	}
}

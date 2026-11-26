// Leetcode: 5. Longest Palindromic Substring   --->   Given a string s, return the longest palindromic substring in s.

// Example 1:
// Input: s = "babad"
// Output: "bab"
// Explanation: "aba" is also a valid answer.

// Example 2:
// Input: s = "cbbd"
// Output: "bb"

// First understand '18_PalindromicSubstrings.cs'. This is the same "expand around the centre" idea, but instead of
// counting palindromes, we remember the longest one.


using System;

public class Solution
{
	private bool IsPalindrome(string s, int left, int right)
	{
		while (left < right)
		{
			if (s[left] != s[right]) return false;
			left++;
			right--;
		}
		return true;
	}

	// Brute force: Check every substring s[i ... j]. Keep the longest one that is a palindrome.
	// T.C: O(n^3)
	// S.C: O(1)
	public string LongestPalindrome_BruteForce(string s)
	{
		int start = 0, maxLen = 1;

		for (int i = 0; i < s.Length; i++)
		{
			for (int j = i + maxLen; j < s.Length; j++)   // only check substrings longer than the best so far
			{
				if (IsPalindrome(s, i, j))
				{
					start = i;
					maxLen = j - i + 1;
				}
			}
		}

		return s.Substring(start, maxLen);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Expand around the centre.
	// 1. For every index i, expand around an odd centre (i, i) and an even centre (i, i + 1). Each expansion returns the
	//    length of the longest palindrome with that centre.
	// 2. If it is longer than the best so far, save its length and its start index.
	//    Start index = i - (len - 1) / 2. Why ? The palindrome spreads about len/2 characters on each side of i.
	//    e.g., "babad", i = 1, odd length 3 ("bab"): start = 1 - (3 - 1) / 2 = 0
	//          "cbbd",  i = 1, even length 2 ("bb"): start = 1 - (2 - 1) / 2 = 1 - 0 = 1
	// T.C: O(n^2)
	// S.C: O(1)
	// (Manacher's algorithm does it in O(n), but it is rarely asked in interviews.)
	public string LongestPalindrome(string s)
	{
		int start = 0, maxLen = 1;

		for (int i = 0; i < s.Length; i++)
		{
			int len = Math.Max(ExpandAroundCentre(s, i, i), ExpandAroundCentre(s, i, i + 1));

			if (len > maxLen)
			{
				maxLen = len;
				start = i - (len - 1) / 2;
			}
		}

		return s.Substring(start, maxLen);
	}

	// Grow from the centre while both ends match. Returns the length of the palindrome found.
	// When the loop stops, left and right are one step OUTSIDE the palindrome, so its length is right - left - 1.
	private int ExpandAroundCentre(string s, int left, int right)
	{
		while (left >= 0 && right < s.Length && s[left] == s[right])
		{
			left--;
			right++;
		}
		return right - left - 1;
	}
}


class Program
{
	static void Main()
	{
		string s = "babad";

		Console.WriteLine(new Solution().LongestPalindrome_BruteForce(s));   // o/p: bab
		Console.WriteLine(new Solution().LongestPalindrome(s));              // o/p: bab
	}
}

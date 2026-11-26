// Leetcode: 647. Palindromic Substrings   --->   Given a string s, return the number of palindromic substrings in it.
// A string is a palindrome when it reads the same backward as forward.
// A substring is a contiguous sequence of characters within the string.

// Example 1:
// Input: s = "abc"
// Output: 3
// Explanation: Three palindromic strings: "a", "b", "c".

// Example 2:
// Input: s = "aaa"
// Output: 6
// Explanation: Six palindromic strings: "a", "a", "a", "aa", "aa", "aaa".


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

	// Brute force: Check every substring s[i ... j] and count the palindromes.
	// T.C: O(n^3);   n^2 substrings, and checking each one is O(n)
	// S.C: O(1)
	public int CountSubstrings_BruteForce(string s)
	{
		int count = 0;
		for (int i = 0; i < s.Length; i++)
		{
			for (int j = i; j < s.Length; j++)
			{
				if (IsPalindrome(s, i, j)) count++;
			}
		}
		return count;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Expand around the centre.
	// Idea: Every palindrome has a centre, and is the same on both sides of it. So instead of checking substrings, pick
	//       each centre and grow outwards while both ends match. Every step that matches is one more palindrome.
	// There are 2 kinds of centres:
	// • Odd length ("aba"):  the centre is 1 character  ->  start with left = right = i
	// • Even length ("abba"): the centre is between 2 characters  ->  start with left = i, right = i + 1
	// e.g., "aaa", centre at index 1 (odd): "a" (yes), "aaa" (yes)  ->  2 palindromes from this centre.
	// Why faster ? Once the ends don't match, no bigger palindrome can have this centre, so we stop early. And we never
	// re-check the inside, as it was already matched in the previous step.
	// T.C: O(n^2);   2n centres, each can expand up to n/2 steps
	// S.C: O(1)
	// (There is also Manacher's algorithm, which does this in O(n), but it is rarely asked in interviews.)
	public int CountSubstrings(string s)
	{
		int total = 0;
		for (int i = 0; i < s.Length; i++)
		{
			total += ExpandAroundCentre(s, i, i);       // odd length palindromes
			total += ExpandAroundCentre(s, i, i + 1);   // even length palindromes
		}
		return total;
	}

	// Grow from the centre while both ends match. Returns how many palindromes were found.
	private int ExpandAroundCentre(string s, int left, int right)
	{
		int count = 0;
		while (left >= 0 && right < s.Length && s[left] == s[right])
		{
			count++;
			left--;
			right++;
		}
		return count;
	}
}


class Program
{
	static void Main()
	{
		string s = "aaa";

		Console.WriteLine(new Solution().CountSubstrings_BruteForce(s));   // o/p: 6
		Console.WriteLine(new Solution().CountSubstrings(s));              // o/p: 6
	}
}

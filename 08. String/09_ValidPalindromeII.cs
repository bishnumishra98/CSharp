// Leetcode: 680. Valid Palindrome II   --->   Given a string s, return true if the s can be palindrome after deleting
// at most one character from it.

// Example 1:
// Input: s = "aba"
// Output: true

// Example 2:
// Input: s = "abca"
// Output: true
// Explanation: You could delete the character 'c'.

// Example 3:
// Input: s = "abc"
// Output: false


using System;

public class Solution
{
	// Is s[left ... right] a palindrome ? (Normal two pointer check.)
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

	// Brute force: Try deleting every character one by one, and check if the rest is a palindrome.
	// T.C: O(n^2);   n tries, and each try builds and checks a string of length n
	// S.C: O(n);   for the string without the deleted character
	public bool ValidPalindrome_BruteForce(string s)
	{
		if (IsPalindrome(s, 0, s.Length - 1)) return true;   // no deletion needed

		for (int i = 0; i < s.Length; i++)
		{
			string rest = s.Remove(i, 1);   // delete the character at index i
			if (IsPalindrome(rest, 0, rest.Length - 1)) return true;
		}
		return false;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Two pointers, and use the one deletion only at the first mismatch.
	// 1. Move left and right towards the middle while the characters match.
	// 2. At the first mismatch, one of these 2 characters must be deleted. So try both:
	//    -> delete s[left]:  check if s[left + 1 ... right] is a palindrome
	//    -> delete s[right]: check if s[left ... right - 1] is a palindrome
	//    If either works, return true. Else false. (We have used our one deletion, so no more chances.)
	// e.g., "abca": a == a, then b != c. Delete b -> "c" is a palindrome. True.
	// T.C: O(n);   one pass, plus at most 2 more checks
	// S.C: O(1)
	public bool ValidPalindrome(string s)
	{
		int left = 0, right = s.Length - 1;

		while (left < right)
		{
			if (s[left] != s[right]) return IsPalindrome(s, left + 1, right) || IsPalindrome(s, left, right - 1);
			left++;
			right--;
		}

		return true;
	}
}


class Program
{
	static void Main()
	{
		string s = "abca";

		Console.WriteLine(new Solution().ValidPalindrome_BruteForce(s));   // o/p: True
		Console.WriteLine(new Solution().ValidPalindrome(s));              // o/p: True
	}
}

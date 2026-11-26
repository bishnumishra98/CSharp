// Leetcode: 125. Valid Palindrome   --->   A phrase is a palindrome if, after converting all uppercase letters into
// lowercase letters and removing all non-alphanumeric characters, it reads the same forward and backward.
// Alphanumeric characters include letters and numbers.
// Given a string s, return true if it is a palindrome, or false otherwise.

// Example 1:
// Input: s = "A man, a plan, a canal: Panama"
// Output: true
// Explanation: "amanaplanacanalpanama" is a palindrome.

// Example 2:
// Input: s = "race a car"
// Output: false
// Explanation: "raceacar" is not a palindrome.

// Example 3:
// Input: s = " "
// Output: true
// Explanation: s is an empty string "" after removing non-alphanumeric characters.
// Since an empty string reads the same forward and backward, it is a palindrome.

// Useful char methods in C#:
// • char.IsLetterOrDigit(c): true for a-z, A-Z, 0-9 (and letters/digits of other languages).
// • char.ToLower(c): 'A' -> 'a'. Characters that are not capital letters stay the same.


using System;
using System.Text;

public class Solution
{
	// Brute force: Build a clean string (only lowercase letters and digits), then check it like a normal palindrome.
	// e.g., "A man, a plan" -> "amanaplan"
	// T.C: O(n)
	// S.C: O(n);   for the clean string
	public bool IsPalindrome_CleanCopy(string s)
	{
		StringBuilder sb = new StringBuilder();
		foreach (char c in s)
		{
			if (char.IsLetterOrDigit(c)) sb.Append(char.ToLower(c));
		}

		string clean = sb.ToString();
		int i = 0, j = clean.Length - 1;
		while (i < j)
		{
			if (clean[i] != clean[j]) return false;
			i++;
			j--;
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Two pointers on the original string, skipping the characters we don't care about.
	// 1. left = 0, right = n - 1.
	// 2. If s[left] is not a letter/digit, skip it (left++). If s[right] is not a letter/digit, skip it (right--).
	// 3. Else compare both in lowercase. If different, return false. Else move both towards the middle.
	// (Same two pointer pattern as '05_ReverseVowelsOfAString.cs', where we skip non-vowels.)
	// T.C: O(n)
	// S.C: O(1)
	public bool IsPalindrome(string s)
	{
		int left = 0, right = s.Length - 1;

		while (left < right)
		{
			if (!char.IsLetterOrDigit(s[left])) left++;
			else if (!char.IsLetterOrDigit(s[right])) right--;
			else
			{
				if (char.ToLower(s[left]) != char.ToLower(s[right])) return false;
				left++;
				right--;
			}
		}

		return true;
	}
}


class Program
{
	static void Main()
	{
		string s = "A man, a plan, a canal: Panama";

		Console.WriteLine(new Solution().IsPalindrome_CleanCopy(s));   // o/p: True
		Console.WriteLine(new Solution().IsPalindrome(s));             // o/p: True
	}
}

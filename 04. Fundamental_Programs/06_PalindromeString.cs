// Palindrome String   --->   Given a string str, check whether it is a palindrome, i.e., it reads the same from
// left to right and from right to left.

// Example 1:
// Input: str = "obkbo"
// Output: Palindrome

// Example 2:
// Input: str = "abc"
// Output: Not a palindrome

// (Leetcode 125. Valid Palindrome, which also ignores spaces, symbols and case, is in the String folder.)


using System;

public class Solution
{
	// Brute force: Make a reversed copy of the string and compare both.
	// In C#, a string can't be changed (it's immutable), so we copy it into a char array, reverse that, and make a new
	// string from it.
	// T.C: O(n)
	// S.C: O(n);   for the reversed copy
	public bool IsPalindrome_Reverse(string str)
	{
		char[] arr = str.ToCharArray();
		Array.Reverse(arr);
		return new string(arr) == str;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Two pointers (no copy needed).
	// 1. Put i on the first character and j on the last character.
	// 2. While i < j: if str[i] != str[j], it's not a palindrome. Else move both towards the middle (i++, j--).
	// 3. If the loop ends, all pairs matched. It's a palindrome.
	// e.g., "obkbo": (o, o) match, (b, b) match, now i == j (middle 'k'), stop. Palindrome.
	// T.C: O(n)
	// S.C: O(1)
	public bool IsPalindrome(string str)
	{
		int i = 0, j = str.Length - 1;

		while (i < j)
		{
			if (str[i] != str[j]) return false;
			i++;
			j--;
		}

		return true;
	}
}


class Program
{
	static void Main()
	{
		string str = "obkbo";

		Console.WriteLine(new Solution().IsPalindrome_Reverse(str) ? "Palindrome" : "Not a palindrome");   // o/p: Palindrome
		Console.WriteLine(new Solution().IsPalindrome(str) ? "Palindrome" : "Not a palindrome");           // o/p: Palindrome
	}
}

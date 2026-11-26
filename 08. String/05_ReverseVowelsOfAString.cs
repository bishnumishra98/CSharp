// Leetcode: 345. Reverse Vowels of a String   --->   Given a string s, reverse only all the vowels in the string and
// return it.
// The vowels are 'a', 'e', 'i', 'o', and 'u', and they can appear in both lower and upper cases, more than once.

// Example 1:
// Input: s = "IceCreAm"
// Output: "AceCreIm"
// Explanation: The vowels in s are ['I', 'e', 'e', 'A']. On reversing the vowels, s becomes "AceCreIm".

// Example 2:
// Input: s = "leetcode"
// Output: "leotcede"


using System;
using System.Collections.Generic;

public class Solution
{
	// "aeiouAEIOU".IndexOf(c) finds c in the vowel list. -1 means not found, i.e., c is not a vowel.
	private bool IsVowel(char c)
	{
		return "aeiouAEIOU".IndexOf(c) != -1;
	}

	// Brute force: Collect the vowels, then put them back in reverse order.
	// 1. Go left to right and push every vowel onto a stack.
	// 2. Go left to right again. At every vowel position, pop from the stack. A stack gives items back in reverse
	//    order (last in, first out), so the vowels get reversed.
	// T.C: O(n)
	// S.C: O(n);   for the stack (plus the char array)
	public string ReverseVowels_Stack(string s)
	{
		Stack<char> vowels = new Stack<char>();
		foreach (char c in s)
		{
			if (IsVowel(c)) vowels.Push(c);
		}

		char[] arr = s.ToCharArray();
		for (int i = 0; i < arr.Length; i++)
		{
			if (IsVowel(arr[i])) arr[i] = vowels.Pop();
		}

		return new string(arr);
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Two pointers (same as reversing a string, but swap only vowels).
	// 1. left = 0, right = n - 1.
	// 2. If arr[left] is not a vowel, left++. If arr[right] is not a vowel, right--.
	// 3. If both are vowels, swap them, and move both towards the middle.
	// T.C: O(n)
	// S.C: O(n);   for the char array (needed, as a C# string can't be changed). No other extra space.
	public string ReverseVowels(string s)
	{
		char[] arr = s.ToCharArray();
		int left = 0, right = arr.Length - 1;

		while (left < right)
		{
			if (!IsVowel(arr[left])) left++;
			else if (!IsVowel(arr[right])) right--;
			else
			{
				(arr[left], arr[right]) = (arr[right], arr[left]);
				left++;
				right--;
			}
		}

		return new string(arr);
	}
}


class Program
{
	static void Main()
	{
		string s = "IceCreAm";

		Console.WriteLine(new Solution().ReverseVowels_Stack(s));   // o/p: AceCreIm
		Console.WriteLine(new Solution().ReverseVowels(s));         // o/p: AceCreIm
	}
}

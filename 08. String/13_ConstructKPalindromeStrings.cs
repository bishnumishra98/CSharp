// Leetcode: 1400. Construct K Palindrome Strings   --->   Given a string s and an integer k, return true if you can use
// all the characters in s to construct non-empty k palindrome strings or false otherwise.

// Example 1:
// Input: s = "annabelle", k = 2
// Output: true
// Explanation: You can construct two palindromes using all characters in s.
// Some possible constructions "anna" + "elble", "anbna" + "elle", "anellena" + "b"

// Example 2:
// Input: s = "leetcode", k = 3
// Output: false
// Explanation: It is impossible to construct 3 palindromes using all the characters of s.

// Example 3:
// Input: s = "true", k = 4
// Output: true
// Explanation: The only possible solution is to put each character in a separate string.


using System;

public class Solution
{
	// Key fact: In a palindrome, every letter comes in pairs (one on each side), except at most ONE letter in the middle.
	//           So a palindrome can hold at most 1 letter with an odd count.
	// 1. Letters with an odd count: each one needs its own palindrome (as its middle). So we need at least 'odd'
	//    palindromes. If odd > k, it's impossible.
	// 2. We also need at least k letters, as each of the k palindromes must be non-empty. If s.Length < k, impossible.
	// 3. If both are fine, it's always possible: put the odd letters as middles, and spread the pairs around. If we
	//    need more palindromes, split a pair into 2 single-letter palindromes ("a" + "a").
	// e.g., "annabelle": a2 n2 b1 e2 l2. Only 'b' is odd, so odd = 1 <= k = 2, and 9 letters >= 2. True.
	// T.C: O(n)
	// S.C: O(1);   the count array always has 26 slots
	public bool CanConstruct(string s, int k)
	{
		if (s.Length < k) return false;

		int[] freq = new int[26];
		foreach (char c in s) freq[c - 'a']++;

		int odd = 0;
		foreach (int count in freq)
		{
			if (count % 2 == 1) odd++;
		}

		return odd <= k;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().CanConstruct("annabelle", 2));   // o/p: True
	}
}

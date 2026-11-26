// GFG: First non-repeating character in a stream   --->   Given a string s consisting of only lowercase alphabets, for
// each index i in the string (0 <= i < n), find the first non-repeating character in the prefix s[0..i]. If no such
// character exists, use '#'.

// Example 1:
// Input: s = "aabc"
// Output: "a#bb"
// Explanation: "a" -> 'a'.  "aa" -> none, '#'.  "aab" -> 'b'.  "aabc" -> 'b' and 'c' appear once, 'b' came first -> 'b'.

// Example 2:
// Input: s = "ababc"
// Output: "aab#c"

// Problem link: https://www.geeksforgeeks.org/problems/first-non-repeating-character-in-a-stream1216/1

// "Stream" means the characters come one at a time, and after EACH one we must answer right away.
// (For a single answer for the whole string, see '08. String\02_FindFirstNonRepeatingCharacter.cs'.)


// Dry run image (in this folder): 10_FirstNonRepeatingCharacterInAStream.jpg

using System;
using System.Text;
using System.Collections.Generic;

public class Solution
{
	// Brute force: After each new character, count the characters of the prefix and scan it from the start for the first
	// one with count 1.
	// T.C: O(n^2)
	// S.C: O(1);   the count array has 26 slots (ignoring the answer)
	public string FirstNonRepeating_BruteForce(string s)
	{
		StringBuilder ans = new StringBuilder();
		int[] freq = new int[26];

		for (int i = 0; i < s.Length; i++)
		{
			freq[s[i] - 'a']++;

			char first = '#';
			for (int j = 0; j <= i; j++)
			{
				if (freq[s[j] - 'a'] == 1)
				{
					first = s[j];
					break;
				}
			}
			ans.Append(first);
		}
		return ans.ToString();
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Count array + a queue of "candidates" in the order they first appeared.
	// 1. For every new character c: freq[c]++. If this is its FIRST appearance (freq becomes 1), enqueue it.
	// 2. While the front of the queue has freq > 1 (it repeated), it can never be the answer again: dequeue it.
	// 3. The front (if any) is the first non-repeating character so far. Else '#'.
	// e.g., "ababc":  a -> q [a] -> 'a'.  b -> q [a, b] -> 'a'.  a (freq 2) -> front a is repeated, remove -> 'b'.
	//                 b (freq 2) -> remove b -> empty -> '#'.  c -> q [c] -> 'c'.   Answer: "aab#c"
	// Note: The C++ code enqueued EVERY character, so the queue could grow to n. Enqueuing only on the first appearance
	//       keeps at most 26 letters in the queue.
	// T.C: O(n);   every letter is enqueued and dequeued at most once
	// S.C: O(26) = O(1);   ignoring the answer
	public string FirstNonRepeating(string s)
	{
		StringBuilder ans = new StringBuilder();
		int[] freq = new int[26];
		Queue<char> q = new Queue<char>();

		foreach (char c in s)
		{
			if (++freq[c - 'a'] == 1) q.Enqueue(c);   // first appearance

			while (q.Count > 0 && freq[q.Peek() - 'a'] > 1) q.Dequeue();

			ans.Append(q.Count > 0 ? q.Peek() : '#');
		}
		return ans.ToString();
	}
}


class Program
{
	static void Main()
	{
		string s = "ababc";

		Console.WriteLine(new Solution().FirstNonRepeating_BruteForce(s));   // o/p: aab#c
		Console.WriteLine(new Solution().FirstNonRepeating(s));              // o/p: aab#c
	}
}

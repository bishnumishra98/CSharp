// Leetcode: 1047. Remove All Adjacent Duplicates In String   --->   You are given a string s consisting of lowercase
// English letters. A duplicate removal consists of choosing two adjacent and equal letters and removing them.
// We repeatedly make duplicate removals on s until we no longer can.
// Return the final string after all such duplicate removals have been made. It can be proven that the answer is unique.

// Example 1:
// Input: s = "abbaca"
// Output: "ca"
// Explanation: For example, in "abbaca" we could remove "bb" since the letters are adjacent and equal, and this is the
// only possible move. The result of this move is that the string is "aaca", of which only "aa" is possible, so the
// final string is "ca".

// Example 2:
// Input: s = "azxxzy"
// Output: "ay"


using System;
using System.Text;

public class Solution
{
	// Brute force: Keep scanning the string, and remove the first equal pair you find. Repeat until no pair is left.
	// e.g., "abbaca" -> remove "bb" -> "aaca" -> remove "aa" -> "ca" -> no pair left
	// T.C: O(n^2);   up to n/2 removals, and each scan + Remove() is O(n)
	// S.C: O(n);   for the new strings
	public string RemoveDuplicates_BruteForce(string s)
	{
		bool removed = true;

		while (removed)
		{
			removed = false;
			for (int i = 0; i < s.Length - 1; i++)
			{
				if (s[i] == s[i + 1])
				{
					s = s.Remove(i, 2);   // remove the 2 equal characters
					removed = true;
					break;
				}
			}
		}

		return s;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Use the answer itself as a stack.
	// Go through s one character at a time:
	// -> If the answer's last character is the same as the current one, they form a pair, so remove that last character.
	// -> Else, add the current character to the answer.
	// A removal can create a new pair (e.g., "abba": after "bb" goes, "a" meets "a"). The stack handles this
	// automatically, as the next character is compared with the new last character.
	// e.g., "abbaca":  a -> ab -> a (b removed) -> "" (a removed) -> c -> ca
	// StringBuilder works as the stack: Append() = push, Length-- = pop, sb[sb.Length - 1] = top.
	// T.C: O(n)
	// S.C: O(n);   for the answer
	public string RemoveDuplicates(string s)
	{
		StringBuilder sb = new StringBuilder();

		foreach (char c in s)
		{
			if (sb.Length > 0 && sb[sb.Length - 1] == c) sb.Length--;   // pair found: remove the last character
			else sb.Append(c);
		}

		return sb.ToString();
	}
}


class Program
{
	static void Main()
	{
		string s = "abbaca";

		Console.WriteLine(new Solution().RemoveDuplicates_BruteForce(s));   // o/p: ca
		Console.WriteLine(new Solution().RemoveDuplicates(s));              // o/p: ca
	}
}

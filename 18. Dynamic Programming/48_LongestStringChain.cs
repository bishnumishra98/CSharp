// Leetcode: 1048. Longest String Chain   --->   wordA is a PREDECESSOR of wordB if inserting exactly ONE letter anywhere
// in wordA makes wordB (e.g., "abc" -> "abac"). A word chain is a sequence where each word is a predecessor of the
// next. Using words from the list in any order, return the length of the longest chain.

// Example 1:
// Input: words = ["a","b","ba","bca","bda","bdca"]
// Output: 4   ("a" -> "ba" -> "bda" -> "bdca")

// Example 2:
// Input: words = ["xbc","pcxbcf","xb","cxbc","pcxbc"]
// Output: 5   ("xb" -> "xbc" -> "cxbc" -> "pcxbc" -> "pcxbcf")

// Example 3:
// Input: words = ["abcd","dbqca"]
// Output: 1

// First understand FindLIS in '46_LongestIncreasingSubsequence.cs'.


using System;
using System.Collections.Generic;

public class Solution
{
	// Better (LIS pattern, from the C++ code): the same 2 changes as '47_LargestDivisibleSubset.cs':
	// Change 1: sort the words by LENGTH (any order is allowed, and a predecessor is always 1 letter shorter).
	// Change 2: in the LIS code, replace "nums[j] < nums[i]" with "words[j] is a predecessor of words[i]".
	// IsPredecessor(longer, shorter): the lengths must differ by exactly 1. Walk both words with 2 pointers: on a match
	// move both, on a mismatch move only the pointer in the LONGER word (that is the inserted letter). It works if the
	// whole shorter word was matched.
	// T.C: O(n^2 * L);   L = the word length (for each pair, the 2-pointer check)
	// S.C: O(n)
	public int LongestStrChain_LIS(string[] words)
	{
		string[] w = (string[])words.Clone();
		Array.Sort(w, (x, y) => x.Length.CompareTo(y.Length));   // change 1

		int n = w.Length, best = 1;
		int[] dp = new int[n];
		for (int i = 0; i < n; i++)
		{
			dp[i] = 1;
			for (int j = 0; j < i; j++)
			{
				if (IsPredecessor(w[i], w[j]) && 1 + dp[j] > dp[i]) dp[i] = 1 + dp[j];   // change 2
			}
			best = Math.Max(best, dp[i]);
		}
		return best;
	}

	private bool IsPredecessor(string longer, string shorter)
	{
		if (longer.Length != shorter.Length + 1) return false;

		int j = 0;
		for (int i = 0; i < longer.Length; i++)
		{
			if (j < shorter.Length && longer[i] == shorter[j]) j++;
		}
		return j == shorter.Length;
	}

// ------------------------------------------------------------------------------

	// Optimal (Dictionary): Instead of comparing every pair of words, go the other way round: for each word, REMOVE each
	// one of its letters. Every result is a possible predecessor; look it up in a dictionary {word -> longest chain
	// ending at that word}.
	// Process words from short to long, so a predecessor's value is always ready.
	//   chain[word] = 1 + max(chain[word without letter k]) over all k (only those that are in the dictionary)
	// e.g., "bdca": remove each letter -> "dca", "bca", "bda", "bdc". "bca" and "bda" have chain 3. So chain = 4.
	// T.C: O(n log n + n * L^2);   sort + for each word, L removals of cost L each. With up to 1000 words of length
	//      <= 16, that is far fewer steps than comparing all n^2 pairs.
	// S.C: O(n * L);   the dictionary
	public int LongestStrChain(string[] words)
	{
		string[] w = (string[])words.Clone();
		Array.Sort(w, (x, y) => x.Length.CompareTo(y.Length));

		Dictionary<string, int> chain = new Dictionary<string, int>();
		int best = 1;

		foreach (string word in w)
		{
			int length = 1;
			for (int k = 0; k < word.Length; k++)
			{
				string predecessor = word.Remove(k, 1);   // the word without its k-th letter
				if (chain.TryGetValue(predecessor, out int prevLength)) length = Math.Max(length, prevLength + 1);
			}
			chain[word] = length;
			best = Math.Max(best, length);
		}
		return best;
	}
}


class Program
{
	static void Main()
	{
		string[] words = { "a", "b", "ba", "bca", "bda", "bdca" };

		Console.WriteLine(new Solution().LongestStrChain_LIS(words));   // o/p: 4
		Console.WriteLine(new Solution().LongestStrChain(words));       // o/p: 4
	}
}

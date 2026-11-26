// Leetcode: 1653. Minimum Deletions to Make String Balanced   --->   s has only 'a' and 'b'. It is BALANCED if no 'b' comes
// before an 'a' (all a's first, then all b's). Return the fewest characters to delete to make it balanced.

// Example 1:
// Input: s = "aababbab"
// Output: 2   (delete positions 2 and 6 -> "aaabbb", or 3 and 6 -> "aabbbb")

// Example 2:
// Input: s = "bbaaaaabb"
// Output: 2   (delete the first two b's)


using System;

public class Solution
{
	// Algorithm (one pass): Read left to right, keeping:
	//   bCount    = the b's seen so far,
	//   deletions = the fewest deletions that make the part read so far balanced.
	// On 'b': a 'b' at the end never breaks the order. Only bCount++.
	// On 'a': this 'a' comes after bCount b's. Two ways to fix it:
	//   I.  delete this 'a':               deletions + 1
	//   II. delete ALL b's seen so far:    bCount           (then every 'a' so far is fine)
	//   Take the smaller one.
	// e.g., "bbaaaaabb": b, b (bCount 2). Each a: min(prev + 1, 2) -> 1, 2, 2, 2, 2. Final b's change nothing. Answer 2.
	// (This is a tiny DP: 'deletions' is the best answer for the prefix, built from the previous one.)
	// T.C: O(n)
	// S.C: O(1)
	public int MinimumDeletions(string s)
	{
		int bCount = 0, deletions = 0;
		foreach (char c in s)
		{
			if (c == 'b') bCount++;
			else deletions = Math.Min(deletions + 1, bCount);
		}
		return deletions;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().MinimumDeletions("aababbab"));   // o/p: 2
	}
}

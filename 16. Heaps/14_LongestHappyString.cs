// Leetcode: 1405. Longest Happy String   --->   A string s is "happy" if:
// ● it only has the letters 'a', 'b' and 'c',
// ● it does NOT contain "aaa", "bbb" or "ccc" (no letter 3 times in a row),
// ● it has at most a letters 'a', at most b letters 'b', and at most c letters 'c'.
// Given a, b and c, return the LONGEST possible happy string. If there are several, return any. If none, return "".

// Example 1:
// Input: a = 1, b = 1, c = 7
// Output: "ccaccbcc"   ("ccbccacc" is also correct)

// Example 2:
// Input: a = 7, b = 1, c = 0
// Output: "aabaa"   (only 1 'b' to break the a's, so only 2 + 2 a's can be used)

// Close to '13_ReorganizeString.cs'. There a letter could appear once in a row, here up to TWICE in a row, and we may
// leave letters out.


using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
	// Algorithm (Greedy + max heap, from the C++ code): Work in rounds with the 2 most frequent letters, like in
	// Reorganize String.
	// 1. Max heap of (letter, count), for the letters with count > 0.
	// 2. While there are at least 2 letters: take out the first (most frequent) and the second.
	//    ● First: write it TWICE if its count is >= 2, else once. (It has the most left, so use it as much as allowed.)
	//    ● Second: write it twice only if its count is >= 2 AND it now has at least as many left as the first.
	//      Otherwise write it ONCE: it is only there to break up the first letter, so save the rest of it for later
	//      breaks.
	//    Put back the letters that still have a count > 0.
	// 3. If 1 letter is left: write it up to 2 times (more would make 3 in a row). The last written letter was the
	//    second letter of the last round, which is a different letter, so this is safe.
	// e.g., a = 1, b = 1, c = 7: round 1: "cc" (c: 5) + "a" (a: 0). Round 2: "cc" (c: 3) + "b" (b: 0).
	//       Only c left (3): write "cc". Answer "ccaccbcc".
	//       (a and b both have count 1, and a heap gives equal priorities in no fixed order. Here C# picked 'a' first,
	//        the C++ code picked 'b'. Both answers are correct.)
	// T.C: O(a + b + c);   every round writes at least 2 letters, and the heap has at most 3 items
	// S.C: O(1);   not counting the answer
	public string LongestDiverseString(int a, int b, int c)
	{
		PriorityQueue<char, int> maxHeap = new PriorityQueue<char, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
		if (a > 0) maxHeap.Enqueue('a', a);
		if (b > 0) maxHeap.Enqueue('b', b);
		if (c > 0) maxHeap.Enqueue('c', c);

		StringBuilder ans = new StringBuilder();

		while (maxHeap.Count > 1)
		{
			maxHeap.TryDequeue(out char first, out int firstCount);
			maxHeap.TryDequeue(out char second, out int secondCount);

			int useFirst = Math.Min(2, firstCount);
			ans.Append(first, useFirst);
			firstCount -= useFirst;

			int useSecond = (secondCount >= 2 && secondCount >= firstCount) ? 2 : 1;
			ans.Append(second, useSecond);
			secondCount -= useSecond;

			if (firstCount > 0) maxHeap.Enqueue(first, firstCount);
			if (secondCount > 0) maxHeap.Enqueue(second, secondCount);
		}

		if (maxHeap.Count == 1)
		{
			maxHeap.TryDequeue(out char last, out int lastCount);
			ans.Append(last, Math.Min(2, lastCount));   // Append(char, repeatCount) writes it that many times
		}

		return ans.ToString();
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().LongestDiverseString(1, 1, 7));   // o/p: ccaccbcc
	}
}

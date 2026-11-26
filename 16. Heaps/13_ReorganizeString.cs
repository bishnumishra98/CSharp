// Leetcode: 767. Reorganize String   --->   Given a string s, rearrange its characters so that no two ADJACENT characters
// are the same. Return any valid rearrangement, or "" if it is not possible.
// s has only lowercase English letters.

// Example 1:
// Input: s = "aab"
// Output: "aba"

// Example 2:
// Input: s = "aaab"
// Output: ""   (3 a's need 2 other letters between them, there is only 1)

// When is it possible ? The most frequent letter must not need more gaps than there are other letters:
// maxCount <= (n + 1) / 2. e.g., "aaab": n = 4, (4 + 1) / 2 = 2 < 3, impossible.


using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
	// Better (Greedy + max heap), from the C++ code:
	// 1. Count every letter. Put (letter, count) in a max heap by count.
	// 2. While the heap has at least 2 letters: take out the 2 MOST frequent, write both (they are different letters,
	//    so they can sit side by side), lower both counts by 1, and put back the ones that still have a count > 0.
	//    Why 2 at a time ? If we took only the top one, it could still be the top after writing it (its count is still
	//    the biggest), and we would write it twice in a row.
	// 3. If 1 letter is left: it can be written once (it differs from the last written letter, which was the second
	//    letter of the last pair). If its count is still > 1, there is no way to place the rest: return "".
	// Using the most frequent letters first is the key: they are the hardest to place, so use them while there are
	// still other letters to put between them.
	// e.g., "aab": heap {a:2, b:1}. Take a, b -> "ab", a:1 goes back. One letter left: a:1 -> "aba".
	// T.C: O(n log 26) = O(n);   the heap has at most 26 letters
	// S.C: O(1);   26 counts and a heap of at most 26 (not counting the answer)
	public string ReorganizeString_Heap(string s)
	{
		int[] freq = new int[26];
		foreach (char c in s) freq[c - 'a']++;

		PriorityQueue<char, int> maxHeap = new PriorityQueue<char, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
		for (int i = 0; i < 26; i++)
		{
			if (freq[i] > 0) maxHeap.Enqueue((char)('a' + i), freq[i]);
		}

		StringBuilder ans = new StringBuilder();
		while (maxHeap.Count > 1)
		{
			maxHeap.TryDequeue(out char first, out int firstCount);
			maxHeap.TryDequeue(out char second, out int secondCount);

			ans.Append(first).Append(second);

			if (firstCount > 1) maxHeap.Enqueue(first, firstCount - 1);
			if (secondCount > 1) maxHeap.Enqueue(second, secondCount - 1);
		}

		if (maxHeap.Count == 1)
		{
			maxHeap.TryDequeue(out char last, out int lastCount);
			if (lastCount > 1) return "";   // it would have to sit next to itself
			ans.Append(last);
		}

		return ans.ToString();
	}

// ------------------------------------------------------------------------------

	// Optimal (no heap): Fill the EVEN indexes first (0, 2, 4, ...), then the odd ones (1, 3, 5, ...).
	// 1. If maxCount > (n + 1) / 2, return "".
	// 2. Put the most frequent letter at indexes 0, 2, 4, ... first (it fits, by step 1).
	// 3. Then place every other letter, continuing at the next even index, and jumping to index 1 when the even ones run
	//    out.
	// Why no two neighbours are equal: every letter fills positions 2 apart, so it never touches itself, EXCEPT maybe a
	// letter that "wraps" (some copies at the END of the evens, the rest at the START of the odds: 1, 3, ...). Its even
	// copies are at the far right and its odd copies at the far left, so they could only meet if that letter filled
	// about half of the string. Only the most frequent letter can be that big, and it is placed first, from index 0,
	// so it never wraps.
	// e.g., "aaabbc": a at 0, 2, 4 -> b at 1, 3 (evens are full, jump to 1) -> c at 5. "ababac".
	// T.C: O(n)
	// S.C: O(n);   the char array for the answer
	public string ReorganizeString(string s)
	{
		int n = s.Length;
		int[] freq = new int[26];
		foreach (char c in s) freq[c - 'a']++;

		int maxLetter = 0;
		for (int i = 1; i < 26; i++)
		{
			if (freq[i] > freq[maxLetter]) maxLetter = i;
		}
		if (freq[maxLetter] > (n + 1) / 2) return "";

		char[] ans = new char[n];
		int index = 0;

		// Most frequent letter first.
		while (freq[maxLetter] > 0)
		{
			ans[index] = (char)('a' + maxLetter);
			index += 2;
			freq[maxLetter]--;
		}

		// All the others.
		for (int i = 0; i < 26; i++)
		{
			while (freq[i] > 0)
			{
				if (index >= n) index = 1;   // evens are full, go on with the odds
				ans[index] = (char)('a' + i);
				index += 2;
				freq[i]--;
			}
		}

		return new string(ans);
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().ReorganizeString_Heap("aab"));   // o/p: aba
		Console.WriteLine(new Solution().ReorganizeString("aab"));        // o/p: aba
	}
}

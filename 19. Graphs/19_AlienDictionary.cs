// GFG: Alien Dictionary   --->   An alien language uses the first k letters of the English alphabet, in an UNKNOWN order.
// You get a list of words SORTED in that alien order. Return one possible order of the k letters. If the words can not
// be in sorted order for any letter order, return "".

// Example 1:
// Input: words = ["baa","abcd","abca","cab","cad"], k = 4
// Output: "bdac"   (one valid answer)

// Example 2:
// Input: words = ["caa","aaa","aab"], k = 3
// Output: "cab"

// Example 3:
// Input: words = ["abc","ab"], k = 3
// Output: ""   ("ab" is a prefix of "abc", so it must come first. The list is not sorted in ANY order.)

// Example 4:
// Input: words = ["abc","bcd","aef"], k = 3
// Output: ""   (a before b, then b before a: a cycle)

// First understand '15_TopologicalSort.cs'.


using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
	// Algorithm:
	// 1. Build a graph of letters. Compare each pair of NEIGHBOURING words. The first position where they differ tells
	//    the order of two letters: word1[j] comes before word2[j], so add the edge word1[j] -> word2[j]. Nothing after
	//    that position tells us anything (like "apple" < "apz..." says only l < z).
	//    e.g., "baa" vs "abcd": b -> a.   "abcd" vs "abca": d -> a.   "abca" vs "cab": a -> c.   "cab" vs "cad": b -> d.
	// 2. A topological sort (Kahn's algorithm) of the letters is a valid alphabet.
	// 3. Two cases have no valid order (Example 3 and 4):
	//    - A longer word comes before its own PREFIX ("abc" before "ab"): no letter order can fix that.
	//    - The letter graph has a CYCLE: Kahn's output is shorter than k.
	// Why only neighbouring pairs ? Sorted order is "chained": if w1 <= w2 and w2 <= w3, then w1 <= w3 follows.
	// T.C: O(N * L + k + E);   N words of length up to L, k letters, E edges (at most N - 1)
	// S.C: O(k + E)
	// (The C++ had 2 files: one without the "" checks, and a follow-up with them. They are merged here.)
	public string FindOrder(string[] words, int k)
	{
		List<int>[] adj = new List<int>[k];
		for (int i = 0; i < k; i++) adj[i] = new List<int>();
		int[] inDegree = new int[k];

		for (int i = 0; i + 1 < words.Length; i++)
		{
			string w1 = words[i], w2 = words[i + 1];
			int len = Math.Min(w1.Length, w2.Length);

			int j = 0;
			while (j < len && w1[j] == w2[j]) j++;

			if (j == len)
			{
				if (w1.Length > w2.Length) return "";   // a longer word before its own prefix
				continue;                               // one is a prefix of the other, in the right order: no info
			}

			adj[w1[j] - 'a'].Add(w2[j] - 'a');
			inDegree[w2[j] - 'a']++;
		}

		Queue<int> q = new Queue<int>();
		for (int c = 0; c < k; c++)
		{
			if (inDegree[c] == 0) q.Enqueue(c);
		}

		StringBuilder order = new StringBuilder();
		while (q.Count > 0)
		{
			int c = q.Dequeue();
			order.Append((char)('a' + c));
			foreach (int next in adj[c])
			{
				if (--inDegree[next] == 0) q.Enqueue(next);
			}
		}

		return order.Length == k ? order.ToString() : "";   // shorter: a cycle
	}
}


class Program
{
	static void Main()
	{
		string[] words = { "baa", "abcd", "abca", "cab", "cad" };

		Console.WriteLine(new Solution().FindOrder(words, 4));   // o/p: bdac
	}
}

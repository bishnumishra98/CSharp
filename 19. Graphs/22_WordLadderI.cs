// Leetcode: 127. Word Ladder (GFG: Word Ladder I)   --->   Given beginWord, endWord and a wordList, find the length of the
// SHORTEST sequence beginWord -> w1 -> w2 -> ... -> endWord where:
// ● each step changes exactly ONE letter,
// ● every word after beginWord is in wordList (beginWord does not need to be).
// The length counts all the words, including beginWord and endWord. Return 0 if there is no such sequence.

// Example 1:
// Input: beginWord = "der", endWord = "dfs", wordList = ["des","der","dfr","dgt","dfs"]
// Output: 3   (der -> dfr -> dfs)

// Example 2:
// Input: beginWord = "hit", endWord = "cog", wordList = ["hot","dot","dog","lot","log","cog"]
// Output: 5   (hit -> hot -> dot -> dog -> cog)

// Example 3:
// Input: beginWord = "hit", endWord = "cog", wordList = ["hot","dot","dog","lot","log"]
// Output: 0   ("cog" is not in the list)

// See it as a graph: every word is a node, and two words are connected if they differ in exactly one letter. The
// question is the shortest path, in an UNWEIGHTED graph, so BFS (see '21_ShortestPathInUndirectedGraphWithUnitWeights.cs').


using System;
using System.Collections.Generic;

public class Solution
{
	// Algorithm (BFS):
	// 1. Put all words in a HashSet (fast "is this a word ?" checks). Queue: (beginWord, 1).
	// 2. Take out (word, steps). If it is endWord, return steps.
	// 3. Find its neighbours WITHOUT comparing with every other word: change each position to each letter 'a'..'z'. Every
	//    result that is in the set is a neighbour: enqueue (it, steps + 1) and REMOVE it from the set. Removing works as
	//    the "visited" mark: a word reached later can only be reached by a longer path.
	// T.C: O(N * L * 26 * L);   N words, L letters each, 26 tries per letter, and building / hashing a word costs L
	// S.C: O(N * L);   the set and the queue
	public int LadderLength(string beginWord, string endWord, IList<string> wordList)
	{
		HashSet<string> words = new HashSet<string>(wordList);
		Queue<(string word, int steps)> q = new Queue<(string word, int steps)>();
		q.Enqueue((beginWord, 1));
		words.Remove(beginWord);

		while (q.Count > 0)
		{
			var (word, steps) = q.Dequeue();
			if (word == endWord) return steps;

			char[] chars = word.ToCharArray();
			for (int i = 0; i < chars.Length; i++)
			{
				char original = chars[i];
				for (char c = 'a'; c <= 'z'; c++)
				{
					chars[i] = c;
					string next = new string(chars);
					if (words.Remove(next)) q.Enqueue((next, steps + 1));   // Remove returns true if it was there
				}
				chars[i] = original;   // put the letter back before changing the next position
			}
		}
		return 0;
	}
}


class Program
{
	static void Main()
	{
		List<string> wordList = new List<string> { "hot", "dot", "dog", "lot", "log", "cog" };

		Console.WriteLine(new Solution().LadderLength("hit", "cog", wordList));   // o/p: 5
	}
}

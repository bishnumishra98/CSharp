// Leetcode: 126. Word Ladder II (GFG: Word Ladder II)   --->   Same rules as '22_WordLadderI.cs', but return ALL the
// shortest sequences from beginWord to endWord (as lists of words), or an empty list if there is none.

// Example 1:
// Input: beginWord = "der", endWord = "dfs", wordList = ["des","der","dfr","dgt","dfs"]
// Output: [["der","dfr","dfs"], ["der","des","dfs"]]

// Example 2:
// Input: beginWord = "hit", endWord = "cog", wordList = ["hot","dot","dog","lot","log","cog"]
// Output: [["hit","hot","dot","dog","cog"], ["hit","hot","lot","log","cog"]]

// First understand '22_WordLadderI.cs'.


using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	// Approach 1 (from the C++ code): BFS over whole SEQUENCES. The queue holds lists of words; a list grows by one word
	// per level.
	// The new difficulty: two different shortest sequences can share a word ON THE SAME LEVEL (e.g., both "hot,dot,dog"
	// and "hot,lot,log" end in "cog"). So a word can not be removed from the set the moment it is used, only when the
	// WHOLE level is finished. Keep the words used on the current level in 'usedOnLevel' and remove them when the next
	// level starts.
	// The first sequences that reach endWord are the shortest; keep every sequence of that same length.
	// T.C: exponential in the worst case (every shortest sequence is stored in the queue as a full list)
	// S.C: same
	// This is fine for GFG, but too slow for Leetcode 126 (Time Limit Exceeded). Approach 2 fixes that.
	public IList<IList<string>> FindLadders_BFS(string beginWord, string endWord, IList<string> wordList)
	{
		HashSet<string> words = new HashSet<string>(wordList);
		Queue<List<string>> q = new Queue<List<string>>();
		q.Enqueue(new List<string> { beginWord });

		List<string> usedOnLevel = new List<string> { beginWord };
		int level = 0;
		List<IList<string>> ans = new List<IList<string>>();

		while (q.Count > 0)
		{
			List<string> seq = q.Dequeue();

			if (seq.Count > level)   // a new level starts: now remove the words of the previous level
			{
				level++;
				foreach (string w in usedOnLevel) words.Remove(w);
				usedOnLevel.Clear();
			}

			string last = seq[seq.Count - 1];
			if (last == endWord)
			{
				if (ans.Count == 0 || ans[0].Count == seq.Count) ans.Add(seq);
				continue;
			}

			char[] chars = last.ToCharArray();
			for (int i = 0; i < chars.Length; i++)
			{
				char original = chars[i];
				for (char c = 'a'; c <= 'z'; c++)
				{
					chars[i] = c;
					string next = new string(chars);
					if (!words.Contains(next)) continue;

					q.Enqueue(new List<string>(seq) { next });   // a copy of seq, plus next
					usedOnLevel.Add(next);
				}
				chars[i] = original;
			}
		}
		return ans;
	}

// ------------------------------------------------------------------------------

	// Approach 2 (Optimal: BFS for levels + backtracking DFS):
	// 1. BFS from beginWord, like Word Ladder I, but only remember each word's LEVEL (its distance from beginWord) in a
	//    Dictionary. No sequences are stored. Stop after the level where endWord is reached.
	// 2. DFS BACKWARDS from endWord: from a word at level d, step only to a neighbour at level d - 1. Every such step is
	//    part of some shortest sequence, so the DFS never wanders into dead ends. When beginWord is reached, the path
	//    (reversed) is one answer.
	// Going backwards matters: from beginWord forwards, many branches reach level d without leading to endWord; from
	//    endWord backwards, every branch that follows the levels ends at beginWord.
	// T.C: O(N * L * 26 * L) for the BFS + the size of the output for the DFS
	// S.C: O(N * L)
	public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
	{
		HashSet<string> words = new HashSet<string>(wordList);
		Dictionary<string, int> level = new Dictionary<string, int> { [beginWord] = 0 };
		Queue<string> q = new Queue<string>();
		q.Enqueue(beginWord);
		words.Remove(beginWord);

		while (q.Count > 0 && !level.ContainsKey(endWord))
		{
			int size = q.Count;
			for (int k = 0; k < size; k++)   // one full level at a time
			{
				string word = q.Dequeue();
				foreach (string next in Neighbours(word, words))
				{
					if (level.ContainsKey(next)) continue;   // already reached on this same level from another word
					level[next] = level[word] + 1;
					q.Enqueue(next);
				}
			}
			foreach (string w in q) words.Remove(w);   // the whole new level is now used
		}

		List<IList<string>> ans = new List<IList<string>>();
		if (!level.ContainsKey(endWord)) return ans;

		List<string> path = new List<string> { endWord };
		Backtrack(endWord, beginWord, level, path, ans);
		return ans;
	}

	// All words in 'words' that differ from 'word' in exactly one letter.
	private List<string> Neighbours(string word, HashSet<string> words)
	{
		List<string> result = new List<string>();
		char[] chars = word.ToCharArray();
		for (int i = 0; i < chars.Length; i++)
		{
			char original = chars[i];
			for (char c = 'a'; c <= 'z'; c++)
			{
				if (c == original) continue;
				chars[i] = c;
				string next = new string(chars);
				if (words.Contains(next)) result.Add(next);
			}
			chars[i] = original;
		}
		return result;
	}

	private void Backtrack(string word, string beginWord, Dictionary<string, int> level,
	                       List<string> path, List<IList<string>> ans)
	{
		if (word == beginWord)
		{
			List<string> found = new List<string>(path);
			found.Reverse();
			ans.Add(found);
			return;
		}

		// Previous word: one letter different, one level lower. Every word with a level is a key of 'level'.
		char[] chars = word.ToCharArray();
		for (int i = 0; i < chars.Length; i++)
		{
			char original = chars[i];
			for (char c = 'a'; c <= 'z'; c++)
			{
				chars[i] = c;
				string prev = new string(chars);
				if (level.TryGetValue(prev, out int d) && d == level[word] - 1)
				{
					path.Add(prev);
					Backtrack(prev, beginWord, level, path, ans);
					path.RemoveAt(path.Count - 1);
				}
			}
			chars[i] = original;
		}
	}
}


class Program
{
	static void Main()
	{
		List<string> wordList = new List<string> { "hot", "dot", "dog", "lot", "log", "cog" };

		var bfs = new Solution().FindLadders_BFS("hit", "cog", wordList).Select(s => string.Join(" ", s)).OrderBy(s => s);
		var best = new Solution().FindLadders("hit", "cog", wordList).Select(s => string.Join(" ", s)).OrderBy(s => s);
		Console.WriteLine(string.Join(" | ", bfs));    // o/p: hit hot dot dog cog | hit hot lot log cog
		Console.WriteLine(string.Join(" | ", best));   // o/p: hit hot dot dog cog | hit hot lot log cog
	}
}

// All Words with a Given Prefix (Autocomplete)   --->   Given a set of words stored in a trie, and a prefix, return all
// the words that start with that prefix, in alphabetical order.

// Example:
// Words:  hell, hello, heat, ant, animal, sun, sum, summer
// Prefix: "he"
// Output: [heat, hell, hello]
//
//              (root)
//                |
//                h
//                |
//                e          <- the prefix "he" ends here. Every word below this node starts with "he".
//               / \
//              a   l
//              |   |
//              t*  l*
//                  |
//                  o*

// First understand '01_TrieIntroduction.cs'.


using System;
using System.Collections.Generic;
using System.Text;

public class TrieNode
{
	public TrieNode[] children = new TrieNode[26];
	public bool isEnd;
}

public class Solution
{
	private TrieNode root = new TrieNode();

	// Same as '01_TrieIntroduction.cs'.
	public void Insert(string word)
	{
		TrieNode node = root;
		foreach (char c in word)
		{
			int i = c - 'a';
			if (node.children[i] == null) node.children[i] = new TrieNode();
			node = node.children[i];
		}
		node.isEnd = true;
	}

// ------------------------------------------------------------------------------

	// Algorithm:
	// 1. Walk down the prefix from the root (like StartsWith). If a letter is missing, no word has this prefix: return
	//    an empty list.
	// 2. From the node where the prefix ends, do a DFS over everything below it. Keep the letters of the current path
	//    in a StringBuilder (it starts as the prefix). At every node marked isEnd, the StringBuilder holds a whole word:
	//    save it.
	// 3. Backtracking: add a letter before going down into a child, remove it after coming back, so the StringBuilder
	//    is right for the next child.
	// Visiting the children from 'a' to 'z' gives the words in alphabetical order, for free.
	// e.g., "he": walk h, e. DFS: 'a' -> "hea" -> 't' -> "heat" (end, save) -> back -> 'l' -> "hel" -> 'l' -> "hell"
	//       (end, save) -> 'o' -> "hello" (end, save).
	// T.C: O(P + M * 26 + total length of the answers);   M = nodes below the prefix node, each checks 26 slots
	// S.C: O(D);   D = the length of the longest word (recursion stack and StringBuilder), not counting the answer
	// Note: The C++ code passed searchString.substr(1) in every recursive call while walking the prefix (O(P^2)), and
	//       had both a 'prefix' and a 'searchString' parameter. One StringBuilder is enough.
	public List<string> WordsWithPrefix(string prefix)
	{
		List<string> ans = new List<string>();

		TrieNode node = root;
		foreach (char c in prefix)
		{
			node = node.children[c - 'a'];
			if (node == null) return ans;   // nothing starts with this prefix
		}

		Collect(node, new StringBuilder(prefix), ans);
		return ans;
	}

	private void Collect(TrieNode node, StringBuilder path, List<string> ans)
	{
		if (node.isEnd) ans.Add(path.ToString());

		for (int i = 0; i < 26; i++)
		{
			if (node.children[i] == null) continue;

			path.Append((char)('a' + i));                 // go down
			Collect(node.children[i], path, ans);
			path.Remove(path.Length - 1, 1);              // come back (backtrack)
		}
	}
}


class Program
{
	static void Main()
	{
		Solution sol = new Solution();
		foreach (string w in new[] { "hell", "hello", "heat", "ant", "animal", "sun", "sum", "summer" }) sol.Insert(w);

		Console.WriteLine(string.Join(" ", sol.WordsWithPrefix("he")));   // o/p: heat hell hello
	}
}

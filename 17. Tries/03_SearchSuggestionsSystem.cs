// Leetcode: 1268. Search Suggestions System   --->   You are given an array of strings products and a string searchWord.
// After each character of searchWord is typed, suggest at most THREE products that start with what has been typed so
// far. If more than three products match, suggest the three that come first alphabetically.
// Return a list of lists: the suggestions after each typed character.

// Example 1:
// Input: products = ["mobile","mouse","moneypot","monitor","mousepad"], searchWord = "mouse"
// Output: [["mobile","moneypot","monitor"],["mobile","moneypot","monitor"],["mouse","mousepad"],["mouse","mousepad"],
//          ["mouse","mousepad"]]
// Explanation: Sorted products: mobile, moneypot, monitor, mouse, mousepad.
//              "m" and "mo" match all 5, show the first 3. "mou", "mous" and "mouse" match only mouse and mousepad.

// Example 2:
// Input: products = ["havana"], searchWord = "havana"
// Output: [["havana"],["havana"],["havana"],["havana"],["havana"],["havana"]]

// Example 3 (a typed prefix that matches nothing):
// Input: products = ["bags","baggage","banner","box","cloths"], searchWord = "bags"
// Output: [["baggage","bags","banner"],["baggage","bags","banner"],["baggage","bags"],["bags"]]

// First understand '02_AllWordsWithAPrefix.cs'. This is the same, but stopped at 3 words, once per typed prefix.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TrieNode
{
	public TrieNode[] children = new TrieNode[26];
	public bool isEnd;
}

public class Solution
{
	// Approach 1 (Trie, from the C++ code):
	// 1. Insert every product into a trie.
	// 2. Walk down searchWord one letter at a time. After each letter, we stand on the node of the typed prefix.
	//    DFS below it in 'a' to 'z' order (alphabetical), and STOP as soon as 3 words are found.
	// 3. If a typed letter has no node, this prefix and every longer one match nothing: the rest of the answer is empty
	//    lists.
	// Why is stopping at 3 cheap ? Every branch of a trie ends at a word, so the DFS finds a word within at most L steps
	// down. 3 words cost about 3 * L nodes, not the whole subtree.
	// T.C: O(N * L) to build + O(S * L * 26) for the suggestions;   N = products, L = longest product, S = searchWord
	//      length (the 26 is the slot scan at each node)
	// S.C: O(N * L);   the trie
	public IList<IList<string>> SuggestedProducts_Trie(string[] products, string searchWord)
	{
		TrieNode root = new TrieNode();
		foreach (string p in products) Insert(root, p);

		IList<IList<string>> ans = new List<IList<string>>();
		TrieNode node = root;
		StringBuilder typed = new StringBuilder();

		foreach (char c in searchWord)
		{
			typed.Append(c);
			node = node?.children[c - 'a'];   // once node is null, it stays null

			List<string> suggestions = new List<string>();
			if (node != null) Collect(node, new StringBuilder(typed.ToString()), suggestions);
			ans.Add(suggestions);
		}
		return ans;
	}

	private void Insert(TrieNode root, string word)
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

	// DFS in alphabetical order, stopping once 3 words are found.
	private void Collect(TrieNode node, StringBuilder path, List<string> found)
	{
		if (found.Count == 3) return;
		if (node.isEnd) found.Add(path.ToString());

		for (int i = 0; i < 26 && found.Count < 3; i++)
		{
			if (node.children[i] == null) continue;

			path.Append((char)('a' + i));
			Collect(node.children[i], path, found);
			path.Remove(path.Length - 1, 1);
		}
	}

// ------------------------------------------------------------------------------

	// Approach 2 (Sort + Binary search), no trie:
	// 1. Sort the products. Now all products with the same prefix sit NEXT to each other, in alphabetical order.
	// 2. For each typed prefix, binary search for the FIRST product that is >= the prefix (the "lower bound"). If any
	//    product starts with the prefix, it is there, and the matching ones follow it. Take up to 3 of them while they
	//    still start with the prefix.
	// e.g., sorted [mobile, moneypot, monitor, mouse, mousepad], prefix "mou": the first product >= "mou" is "mouse".
	//       mouse, mousepad start with "mou", then the list ends.
	// ● Use ORDINAL comparison (plain character codes) everywhere. C#'s default string comparison follows the culture's
	//   language rules, which can order some strings differently from the simple 'a' < 'b' order the problem means.
	// T.C: O(N log N * L) to sort + O(S * (log N * L + 3 * L)) for the searches
	// S.C: O(log N);   the sort (it sorts a copy here, which is O(N) if you count that)
	// Simpler code, and no trie memory. The trie wins when the same products are searched many times while new
	// products keep being added (no re-sorting).
	public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
	{
		string[] sorted = (string[])products.Clone();
		Array.Sort(sorted, StringComparer.Ordinal);

		IList<IList<string>> ans = new List<IList<string>>();
		for (int len = 1; len <= searchWord.Length; len++)
		{
			string prefix = searchWord.Substring(0, len);
			int start = LowerBound(sorted, prefix);

			ans.Add(sorted.Skip(start).Take(3).Where(p => p.StartsWith(prefix, StringComparison.Ordinal)).ToList());
		}
		return ans;
	}

	// The first index whose string is >= target (sorted.Length if none).
	private int LowerBound(string[] sorted, string target)
	{
		int low = 0, high = sorted.Length;
		while (low < high)
		{
			int mid = low + (high - low) / 2;
			if (string.CompareOrdinal(sorted[mid], target) < 0) low = mid + 1;
			else high = mid;
		}
		return low;
	}
}


class Program
{
	static void Main()
	{
		string[] products = { "mobile", "mouse", "moneypot", "monitor", "mousepad" };

		var trie = new Solution().SuggestedProducts_Trie(products, "mouse").Select(list => string.Join(",", list));
		var sorted = new Solution().SuggestedProducts(products, "mouse").Select(list => string.Join(",", list));
		Console.WriteLine(string.Join(" | ", trie));     // o/p: mobile,moneypot,monitor | mobile,moneypot,monitor | mouse,mousepad | mouse,mousepad | mouse,mousepad
		Console.WriteLine(string.Join(" | ", sorted));   // o/p: mobile,moneypot,monitor | mobile,moneypot,monitor | mouse,mousepad | mouse,mousepad | mouse,mousepad
	}
}

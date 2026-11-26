// Trie (Prefix Tree, said "try")   --->   A tree that stores WORDS letter by letter, so that words with the same start
// (prefix) share the same path. Each edge is one letter. A path from the root spells a prefix. A mark on a node says
// "a word ENDS here".
//
// Words: hell, hello, heat, ant, animal, sun, sum, summer           (* = a word ends at this node)
//
//                  (root)
//               /    |    \
//              h     a     s
//              |     |     |
//              e     n     u
//             / \   / \   / \
//            a   l t*  i n*  m*
//            |   |     |     |
//            t*  l*    m     m
//                |     |     |
//                o*    a     e
//                      |     |
//                      l*    r*
//
// "hell" and "hello" share h-e-l-l. "sum" and "summer" share s-u-m. Each shared prefix is stored only ONCE.
// Note: "sum" ends at an INNER node (m has a child). That is why we need the end mark: the path s-u-m exists for
//       "summer" too, but only the mark tells us that "sum" itself was inserted.
// Note: The C++ picture showed a-n-t and a-n-i-m-a-l, but its word list said "bat" and "bamboo". The list here
//       matches the picture.

// ● How a node looks: an array of 26 children (one slot per letter 'a'..'z', null = no such letter), and a bool isEnd.
//   The letter is not stored in the node: slot 0 is 'a', slot 1 is 'b', ..., so the slot index tells the letter.
//   (The C++ node also stored the letter in a 'val' field. It is not needed.)
//   'c' - 'a' gives the slot: 'a' - 'a' = 0, 'n' - 'a' = 13, 't' - 'a' = 19.
//
// ● Why use a trie and not a HashSet<string> ?
//   A HashSet can tell if a WHOLE word exists, but not "which words START with 'he'" (it would have to check every
//   word). A trie answers prefix questions by walking just the prefix.
//
//   Operation                         |  Trie            |  HashSet<string>
//   -------------------------------------------------------------------------------
//   Insert / search a word            |  O(L)            |  O(L) on average (hashing reads the whole word)
//   Does any word start with p ?      |  O(P)            |  O(n * P), check every word
//   All words that start with p       |  O(P + output)   |  O(n * P)
//   (L = word length, P = prefix length, n = number of words)
//
// ● Used for: autocomplete, spell checkers, search suggestions, word games (Boggle), IP routing (longest prefix match).
// ● The cost: memory. Every node has 26 slots, most of them null. (A Dictionary<char, TrieNode> per node uses less
//   memory when letters are sparse, or when the alphabet is big, e.g., any Unicode character.)


// Dry run image (in this folder): 01_TrieIntroduction.jpg   (inserting "ant": the 'a' slot of the root points to a new
//                                                             node, its 'n' slot to another, then 't' is marked as end)

using System;

public class TrieNode
{
	public TrieNode[] children = new TrieNode[26];   // all null at first
	public bool isEnd;                               // false at first
}

// Leetcode: 208. Implement Trie (Prefix Tree)   --->   Implement the Trie class:
// ● Trie(): creates an empty trie.
// ● void Insert(string word): inserts the word.
// ● bool Search(string word): true if the word was inserted before.
// ● bool StartsWith(string prefix): true if some inserted word starts with prefix.
// Words have only lowercase English letters.
// Example:
// Input:  ["Trie", "insert", "search", "search", "startsWith", "insert", "search"]
//         [[], ["apple"], ["apple"], ["app"], ["app"], ["app"], ["app"]]
// Output: [null, null, true, false, true, null, true]
public class Trie
{
	private TrieNode root = new TrieNode();

	// Insert: Walk the word letter by letter from the root. If the slot for a letter is empty, make a new node there.
	// Move into it. After the last letter, mark that node as the end of a word.
	// e.g., insert "ant" (see the image): root['a'] is null -> new node. Its ['n'] -> new node. Its ['t'] -> new node.
	//       Mark the 't' node isEnd = true. Inserting "animal" later reuses the 'a' and 'n' nodes.
	// T.C: O(L)
	// S.C: O(L);   up to L new nodes (0 if the word's path already exists)
	// Note: The C++ code was recursive and passed word.substr(1) to every call. substr makes a NEW string each time,
	//       so a word of length L cost L + (L-1) + ... = O(L^2) time and memory, not O(L). A loop over the letters
	//       is truly O(L).
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

	// Walk the path of 's' from the root. Return the node where it ends, or null if a letter is missing.
	// Search and StartsWith both use it.
	private TrieNode FindNode(string s)
	{
		TrieNode node = root;
		foreach (char c in s)
		{
			node = node.children[c - 'a'];
			if (node == null) return null;
		}
		return node;
	}

	// Search: the path must exist AND end at a node marked as a word end.
	// e.g., "sum" with only "summer" inserted: the path s-u-m exists, but m is not marked. false.
	// T.C: O(L)
	// S.C: O(1)
	public bool Search(string word)
	{
		TrieNode node = FindNode(word);
		return node != null && node.isEnd;
	}

	// StartsWith: only the path must exist (some word continues through it).
	// T.C: O(P)
	// S.C: O(1)
	public bool StartsWith(string prefix)
	{
		return FindNode(prefix) != null;
	}

	// Delete a word (not part of Leetcode 208).
	// Easy version: just unmark the end node (isEnd = false). The word can no longer be found. The C++ code did this,
	// but its nodes stay in memory forever, even if no other word uses them.
	// Full version (here): unmark the end, then on the way BACK UP remove every node that is now useless: not the end
	// of another word and without children. Stop removing at the first node that is still needed.
	// e.g., words "sum" and "summer". Delete "summer": unmark r. Going up: r, e, m (the 2nd m) are useless, remove them.
	//       The 1st m is the end of "sum", keep it, and everything above it.
	// T.C: O(L * 26) = O(L);   checking "has no children" looks at 26 slots per node
	// S.C: O(L);   recursion stack
	public void Delete(string word)
	{
		Delete(root, word, 0);
	}

	// Returns true if 'node' became useless, so its parent should remove it.
	private bool Delete(TrieNode node, string word, int depth)
	{
		if (depth == word.Length)
		{
			if (!node.isEnd) return false;   // the word was never inserted, change nothing
			node.isEnd = false;
			return HasNoChildren(node);
		}

		int i = word[depth] - 'a';
		TrieNode child = node.children[i];
		if (child == null) return false;    // the word is not in the trie

		if (Delete(child, word, depth + 1)) node.children[i] = null;   // cut the useless child off

		return !node.isEnd && HasNoChildren(node);
	}

	private bool HasNoChildren(TrieNode node)
	{
		foreach (TrieNode child in node.children)
		{
			if (child != null) return false;
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		Trie trie = new Trie();
		trie.Insert("summer");
		trie.Insert("sun");

		Console.WriteLine(trie.Search("sun"));        // o/p: True
		Console.WriteLine(trie.Search("sum"));        // o/p: False   (only a prefix of "summer")
		Console.WriteLine(trie.StartsWith("sum"));    // o/p: True

		trie.Delete("sun");
		Console.WriteLine(trie.Search("sun"));        // o/p: False
	}
}

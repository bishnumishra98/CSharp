// Leetcode: 721. Accounts Merge   --->   accounts[i] = [name, email1, email2, ...]. Two accounts belong to the same person
// if they share at least ONE email (same name alone is not enough: different people can have the same name). Merge
// them. Return each person as [name, all their emails in sorted order]; the people may be in any order.

// Example 1:
// Input: accounts = [["John","johnsmith@mail.com","john_newyork@mail.com"],
//                    ["John","johnsmith@mail.com","john00@mail.com"],
//                    ["Mary","mary@mail.com"],
//                    ["John","johnnybravo@mail.com"]]
// Output: [["John","john00@mail.com","john_newyork@mail.com","johnsmith@mail.com"],
//          ["Mary","mary@mail.com"],
//          ["John","johnnybravo@mail.com"]]
// Explanation: the first two Johns share johnsmith@mail.com. The third John shares nothing, so he is someone else.

// First understand '35_DisjointSet.cs'.


using System;
using System.Collections.Generic;
using System.Linq;

public class DisjointSet
{
	private int[] parent, size;

	public DisjointSet(int n)
	{
		parent = new int[n + 1];
		size = new int[n + 1];
		for (int i = 0; i <= n; i++)
		{
			parent[i] = i;
			size[i] = 1;
		}
	}

	public int Find(int x) => parent[x] == x ? x : (parent[x] = Find(parent[x]));

	public void Union(int u, int v)
	{
		int ru = Find(u), rv = Find(v);
		if (ru == rv) return;
		if (size[ru] < size[rv]) (ru, rv) = (rv, ru);
		parent[rv] = ru;
		size[ru] += size[rv];
	}
}

public class Solution
{
	// Algorithm: Every ACCOUNT (index 0..n-1) is a node.
	// 1. Walk all emails, remembering in a Dictionary {email -> the first account it appeared in}. When an email appears
	//    AGAIN in another account, Union the two accounts: they are the same person.
	// 2. Group the emails: each email goes to the LEADER of the account it is mapped to.
	// 3. For every leader with emails: [the leader account's name, its emails sorted].
	// e.g., johnsmith@mail.com is first seen in account 0, then in account 1 -> Union(1, 0). Account 1's other email
	//       (john00@mail.com) is mapped to 1, whose leader is the same as 0's. So all 3 emails end up together.
	// T.C: O(N log N);   N = the total number of emails (sorting them dominates; unions are almost O(1))
	// S.C: O(N)
	// C# note: sort with StringComparer.Ordinal (plain character codes), like C++ and Leetcode do. The default string
	//          comparison follows language rules and can order characters such as '_' and '.' differently.
	public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
	{
		int n = accounts.Count;
		DisjointSet ds = new DisjointSet(n);
		Dictionary<string, int> emailOwner = new Dictionary<string, int>();

		for (int i = 0; i < n; i++)
		{
			for (int j = 1; j < accounts[i].Count; j++)
			{
				string email = accounts[i][j];
				if (emailOwner.TryGetValue(email, out int owner)) ds.Union(i, owner);
				else emailOwner[email] = i;
			}
		}

		Dictionary<int, List<string>> groups = new Dictionary<int, List<string>>();
		foreach (var (email, owner) in emailOwner)
		{
			int leader = ds.Find(owner);
			if (!groups.ContainsKey(leader)) groups[leader] = new List<string>();
			groups[leader].Add(email);
		}

		IList<IList<string>> result = new List<IList<string>>();
		foreach (var (leader, emails) in groups)
		{
			emails.Sort(StringComparer.Ordinal);
			List<string> merged = new List<string> { accounts[leader][0] };
			merged.AddRange(emails);
			result.Add(merged);
		}
		return result;
	}
}


class Program
{
	static void Main()
	{
		IList<IList<string>> accounts = new List<IList<string>>
		{
			new List<string> { "John", "johnsmith@mail.com", "john_newyork@mail.com" },
			new List<string> { "John", "johnsmith@mail.com", "john00@mail.com" },
			new List<string> { "Mary", "mary@mail.com" },
			new List<string> { "John", "johnnybravo@mail.com" }
		};

		foreach (var person in new Solution().AccountsMerge(accounts)) Console.WriteLine(string.Join(" ", person));
		// o/p: John john00@mail.com john_newyork@mail.com johnsmith@mail.com
		//      Mary mary@mail.com
		//      John johnnybravo@mail.com
	}
}

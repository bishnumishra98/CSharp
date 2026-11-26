// Greedy Algorithm   --->   Build the answer step by step, and at EVERY step take the choice that looks best RIGHT NOW,
// without ever going back. Simple and fast, but it only works when "best now" really leads to "best overall" (this must
// be argued for each problem). When it does not, DP is needed (e.g., '18. Dynamic Programming/26_CoinChange.cs': greedy
// fails for coins [1, 3, 4] and amount 6).
// Many greedy solutions start by SORTING, so that the "best next choice" is simply the next element.

// Leetcode: 455. Assign Cookies   --->   greed[i] is the smallest cookie that makes child i happy; cookie[j] is the size of
// cookie j. Each child gets at most one cookie. Return the maximum number of happy children.

// Example 1:
// Input: greed = [1, 10, 3], cookie = [1, 2, 3]
// Output: 2   (child 1 gets cookie 1, child 3 gets cookie 3; nothing is big enough for greed 10)

// Example 2:
// Input: greed = [10, 100], cookie = [1, 2]
// Output: 0


using System;

public class Solution
{
	// Algorithm (Greedy + two pointers): Sort both. Go through the cookies from small to big, and give each one to the
	// LEAST greedy child still waiting, if it is big enough; otherwise this cookie is too small for everyone left: skip it.
	// Why is this best ? A small cookie is useless for greedier children, so spending it on the least greedy child (if
	// possible) never wastes a bigger cookie that someone else needs.
	// e.g., greed [1, 3, 10], cookie [1, 2, 3]: cookie 1 -> child 1. cookie 2 < 3: skip. cookie 3 -> child 3. Answer 2.
	// T.C: O(n log n + m log m);   the sorts
	// S.C: O(1);   (sorting copies here, so the caller's arrays do not change: O(n + m) if you count that)
	public int FindContentChildren(int[] greed, int[] cookie)
	{
		int[] g = (int[])greed.Clone(), c = (int[])cookie.Clone();
		Array.Sort(g);
		Array.Sort(c);

		int child = 0;
		for (int j = 0; j < c.Length && child < g.Length; j++)
		{
			if (c[j] >= g[child]) child++;   // this cookie makes the next child happy
		}
		return child;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().FindContentChildren(new int[] { 1, 10, 3 }, new int[] { 1, 2, 3 }));   // o/p: 2
	}
}

// Leetcode: 1547. Minimum Cost to Cut a Stick   --->   A stick of length n is marked from 0 to n. cuts[] lists the
// positions where it must be cut (all of them). You may do the cuts in ANY order. A cut costs the length of the piece
// being cut at that moment. Return the minimum total cost.

// Example 1:
// Input: n = 7, cuts = [1,3,4,5]
// Output: 16
// Explanation: In the given order: 7 (cut at 1) + 6 (piece 1..7, cut at 3) + 4 (piece 3..7) + 3 (piece 4..7) = 20.
//              In the order [3, 5, 1, 4]: 7 + 4 + 3 + 2 = 16.

// Example 2:
// Input: n = 9, cuts = [5,6,1,4,2]
// Output: 22

// First understand '52_MatrixChainMultiplication.cs' (partition DP).


using System;

public class Solution
{
	// Setup (used by every method):
	// 1. SORT the cuts. Then a piece of the stick always holds a CONTINUOUS range of cuts (cuts i..j), so the two pieces
	//    after a cut are independent problems. (Unsorted, the cuts inside one piece would be scattered.)
	// 2. Add 0 at the front and n at the end. Then the piece that holds cuts i..j goes from c[i - 1] to c[j + 1], and its
	//    length is c[j + 1] - c[i - 1], for every range, with no special cases.
	// e.g., n = 7, cuts [1, 3, 4, 5] -> c = [0, 1, 3, 4, 5, 7]. The whole stick holds cuts 1..4: length c[5] - c[0] = 7.
	private int[] Prepare(int n, int[] cuts)
	{
		int[] c = new int[cuts.Length + 2];
		Array.Copy(cuts, 0, c, 1, cuts.Length);   // c[0] = 0 already
		c[c.Length - 1] = n;
		Array.Sort(c);
		return c;
	}

	// The recursion: f(i, j) = the cheapest way to make cuts i..j inside their piece.
	//   Choose which cut k (i <= k <= j) is made FIRST: it costs the whole piece, c[j + 1] - c[i - 1], and splits it into
	//   a left piece (cuts i..k - 1) and a right piece (cuts k + 1..j).
	//   f(i, j) = min over k of:  (c[j + 1] - c[i - 1]) + f(i, k - 1) + f(k + 1, j)
	//   f(i, j) = 0 if i > j   (no cuts left)
	// T.C: exponential
	// S.C: O(c);   c = number of cuts
	public int MinCost_Recursion(int n, int[] cuts)
	{
		int[] c = Prepare(n, cuts);
		return Cost(1, cuts.Length, c);
	}

	private int Cost(int i, int j, int[] c)
	{
		if (i > j) return 0;

		int best = int.MaxValue;
		for (int k = i; k <= j; k++) best = Math.Min(best, c[j + 1] - c[i - 1] + Cost(i, k - 1, c) + Cost(k + 1, j, c));
		return best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(c^3)
	// S.C: O(c^2)
	public int MinCost_Memoization(int n, int[] cuts)
	{
		int[] c = Prepare(n, cuts);
		int m = cuts.Length;
		int[,] dp = new int[m + 1, m + 1];
		for (int i = 0; i <= m; i++)
		{
			for (int j = 0; j <= m; j++) dp[i, j] = -1;
		}
		return Cost(1, m, c, dp);
	}

	private int Cost(int i, int j, int[] c, int[,] dp)
	{
		if (i > j) return 0;
		if (dp[i, j] != -1) return dp[i, j];

		int best = int.MaxValue;
		for (int k = i; k <= j; k++) best = Math.Min(best, c[j + 1] - c[i - 1] + Cost(i, k - 1, c, dp) + Cost(k + 1, j, c, dp));
		return dp[i, j] = best;
	}

// ------------------------------------------------------------------------------

	// i from m down to 1, j from i up to m. dp has 2 extra rows / columns so that dp[k + 1, j] and dp[i, k - 1] exist
	// (and are 0) at the edges.
	// T.C: O(c^3)
	// S.C: O(c^2)
	public int MinCost(int n, int[] cuts)
	{
		int[] c = Prepare(n, cuts);
		int m = cuts.Length;
		int[,] dp = new int[m + 2, m + 2];

		for (int i = m; i >= 1; i--)
		{
			for (int j = i; j <= m; j++)
			{
				int best = int.MaxValue;
				for (int k = i; k <= j; k++) best = Math.Min(best, c[j + 1] - c[i - 1] + dp[i, k - 1] + dp[k + 1, j]);
				dp[i, j] = best;
			}
		}
		return dp[1, m];
	}
}

// Note: The C++ code sorted the caller's 'cuts' and inserted 0 and n into it, which changed the caller's array (main
//       had to reset it before every call). Prepare() works on a copy.


class Program
{
	static void Main()
	{
		int[] cuts = { 1, 3, 4, 5 };

		Console.WriteLine(new Solution().MinCost_Recursion(7, cuts));     // o/p: 16
		Console.WriteLine(new Solution().MinCost_Memoization(7, cuts));   // o/p: 16
		Console.WriteLine(new Solution().MinCost(7, cuts));               // o/p: 16
	}
}

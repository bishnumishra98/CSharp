// GFG: Maximize The Cut Segments   --->   A line has length n. Every cut must make a piece of length exactly x, y or z.
// The WHOLE line must be used (no leftover). Return the MAXIMUM number of pieces, or 0 if it can not be done.

// Example 1:
// Input: n = 4, x = 2, y = 1, z = 1
// Output: 4   (four pieces of length 1)

// Example 2:
// Input: n = 5, x = 5, y = 3, z = 2
// Output: 2   (3 + 2)

// Example 3:
// Input: n = 7, x = 8, y = 9, z = 10
// Output: 0   (every piece is longer than the line)

// Problem link: https://www.geeksforgeeks.org/problems/cutted-segments1642/1

// The recursion: f(n) = the most pieces for a line of length n (or "impossible").
//   Cut off a piece of x, y or z, and solve the rest:  f(n) = 1 + max(f(n - x), f(n - y), f(n - z))
//   f(0) = 0 (nothing left, done),   f(negative) = impossible
// "Impossible" is a huge NEGATIVE number, so max never picks it, and 1 + it stays negative. At the very end, a negative
// answer means impossible: return 0.
// (Like '02_ClimbingStairs.cs' with 3 step sizes, but max instead of +.)


using System;

public class Solution
{
	private const int IMPOSSIBLE = -1_000_000_000;

	// T.C: O(3^n)
	// S.C: O(n)
	// Note: The C++ recursive and memoized versions returned the negative "impossible" value as the answer (e.g., for
	//       Example 3). The answer must be turned into 0 at the end.
	public int MaximizeTheCuts_Recursion(int n, int x, int y, int z)
	{
		return Math.Max(0, MaxPieces(n, x, y, z));
	}

	private int MaxPieces(int n, int x, int y, int z)
	{
		if (n == 0) return 0;
		if (n < 0) return IMPOSSIBLE;

		int best = Math.Max(MaxPieces(n - x, x, y, z), Math.Max(MaxPieces(n - y, x, y, z), MaxPieces(n - z, x, y, z)));
		return best == IMPOSSIBLE ? IMPOSSIBLE : 1 + best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	// Note: The C++ memoized solve() called the plain recursive function inside it, so dp was never used. Each call
	//       must go through the memoized function.
	public int MaximizeTheCuts_Memoization(int n, int x, int y, int z)
	{
		int[] dp = new int[n + 1];
		Array.Fill(dp, int.MinValue);   // not solved yet (IMPOSSIBLE is a real answer here, so it can not be the marker)
		return Math.Max(0, MaxPieces(n, x, y, z, dp));
	}

	private int MaxPieces(int n, int x, int y, int z, int[] dp)
	{
		if (n == 0) return 0;
		if (n < 0) return IMPOSSIBLE;
		if (dp[n] != int.MinValue) return dp[n];

		int best = Math.Max(MaxPieces(n - x, x, y, z, dp),
		           Math.Max(MaxPieces(n - y, x, y, z, dp), MaxPieces(n - z, x, y, z, dp)));
		return dp[n] = best == IMPOSSIBLE ? IMPOSSIBLE : 1 + best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public int MaximizeTheCuts(int n, int x, int y, int z)
	{
		int[] dp = new int[n + 1];
		Array.Fill(dp, IMPOSSIBLE);
		dp[0] = 0;

		for (int len = 1; len <= n; len++)
		{
			foreach (int piece in new[] { x, y, z })
			{
				if (piece <= len && dp[len - piece] != IMPOSSIBLE) dp[len] = Math.Max(dp[len], 1 + dp[len - piece]);
			}
		}
		return Math.Max(0, dp[n]);
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().MaximizeTheCuts_Recursion(5, 5, 3, 2));     // o/p: 2
		Console.WriteLine(new Solution().MaximizeTheCuts_Memoization(5, 5, 3, 2));   // o/p: 2
		Console.WriteLine(new Solution().MaximizeTheCuts(5, 5, 3, 2));               // o/p: 2
	}
}

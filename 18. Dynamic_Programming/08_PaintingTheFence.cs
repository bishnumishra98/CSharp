// GFG: Painting the Fence   --->   A fence has n posts, and there are k colours. Count the ways to paint the fence so that
// NO MORE THAN 2 neighbouring posts have the same colour (3 in a row of one colour is not allowed).

// Example 1:
// Input: n = 3, k = 2
// Output: 6   (colours R, B: RRB, RBR, RBB, BRR, BRB, BBR. Only RRR and BBB are not allowed.)

// Example 2:
// Input: n = 2, k = 4
// Output: 16  (any colour for each post: 4 * 4)

// Problem link: https://www.geeksforgeeks.org/problems/painting-the-fence3727/1

// The recursion: f(n) = the number of ways to paint n posts. Look at the LAST post:
//   I.  It has a DIFFERENT colour from post n - 1: paint the first n - 1 posts in any valid way (f(n - 1)), then pick
//       one of the other k - 1 colours for the last post:      f(n - 1) * (k - 1)
//   II. It has the SAME colour as post n - 1: then post n - 1 must differ from post n - 2 (else 3 in a row). So paint
//       the first n - 2 posts (f(n - 2)), and pick one of k - 1 colours for the PAIR of last posts:  f(n - 2) * (k - 1)
//   f(n) = (k - 1) * (f(n - 1) + f(n - 2))
//   f(1) = k,  f(2) = k * k
// e.g., n = 3, k = 2: f(3) = 1 * (f(2) + f(1)) = 4 + 2 = 6.

// Note: The answer grows very fast (k * k alone is 10^10 for k = 10^5), so 'long' is used. (The GFG version asks for
//       the answer modulo 10^9 + 7 for this reason: then take '% MOD' after every multiplication and addition.)


using System;

public class Solution
{
	// T.C: O(2^n)
	// S.C: O(n);   recursion stack
	public long CountWays_Recursion(int n, int k)
	{
		if (n == 1) return k;
		if (n == 2) return (long)k * k;
		return (k - 1) * (CountWays_Recursion(n - 1, k) + CountWays_Recursion(n - 2, k));
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	// Note: The C++ memoized version called the plain recursive solve() inside it (without dp), so nothing was ever read
	//       from dp, and it was still O(2^n). Each call must pass dp down.
	public long CountWays_Memoization(int n, int k)
	{
		long[] dp = new long[n + 1];
		Array.Fill(dp, -1);
		return Ways(n, k, dp);
	}

	private long Ways(int n, int k, long[] dp)
	{
		if (n == 1) return k;
		if (n == 2) return (long)k * k;
		if (dp[n] != -1) return dp[n];
		return dp[n] = (k - 1) * (Ways(n - 1, k, dp) + Ways(n - 2, k, dp));
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(n)
	public long CountWays_Tabulation(int n, int k)
	{
		if (n == 1) return k;

		long[] dp = new long[n + 1];
		dp[1] = k;
		dp[2] = (long)k * k;
		for (int i = 3; i <= n; i++) dp[i] = (k - 1) * (dp[i - 1] + dp[i - 2]);
		return dp[n];
	}

// ------------------------------------------------------------------------------

	// T.C: O(n)
	// S.C: O(1)
	public long CountWays_SpaceOptimized(int n, int k)
	{
		if (n == 1) return k;

		long prev2 = k, prev = (long)k * k;
		for (int i = 3; i <= n; i++)
		{
			long curr = (k - 1) * (prev + prev2);
			prev2 = prev;
			prev = curr;
		}
		return prev;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().CountWays_Recursion(3, 2));        // o/p: 6
		Console.WriteLine(new Solution().CountWays_Memoization(3, 2));      // o/p: 6
		Console.WriteLine(new Solution().CountWays_Tabulation(3, 2));       // o/p: 6
		Console.WriteLine(new Solution().CountWays_SpaceOptimized(3, 2));   // o/p: 6
	}
}

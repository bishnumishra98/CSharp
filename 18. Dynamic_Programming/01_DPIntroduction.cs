// Dynamic Programming (DP)   --->   "Those who do not remember the past are condemned to repeat it."
// DP = recursion + MEMORY. Break a problem into smaller subproblems (like recursion), but SAVE the answer of every
// subproblem the first time it is solved, so it is never solved again.
//
// ● DP works when a problem has both:
//   I.  Optimal substructure: the answer of the big problem can be built from the answers of smaller problems.
//       e.g., fib(5) = fib(4) + fib(3).
//   II. Overlapping subproblems: the SAME smaller problem is needed again and again.
//       e.g., the recursion tree of fib(5):
//
//                         fib(5)
//                      /          \
//                 fib(4)            fib(3)        <- fib(3) is solved here ...
//                /      \          /     \
//           fib(3)     fib(2)   fib(2)  fib(1)    <- ... and again here. fib(2) 3 times.
//           /    \
//       fib(2)  fib(1)
//
//       The tree doubles at every level: O(2^n). But there are only n + 1 DIFFERENT problems: fib(0) .. fib(n).
//       Solve each one once, and it becomes O(n).
//
// ● 4 steps, from slow to best (most problems in this folder are written in all 4):
//   I.   Recursion: write the plain recursive solution first. It shows the "state" (what changes between calls) and
//        the "transition" (how a problem uses smaller ones).
//   II.  Memoization (Top-Down): the same recursion + an array dp[] for the answers. Before solving a state, check
//        dp: if it is already solved, return it. After solving, store it.
//        Steps: (1) make dp, filled with "not solved yet" (-1),  (2) store every answer in dp,
//               (3) return dp[state] at once if it is already there.
//   III. Tabulation (Bottom-Up): no recursion. Fill dp with a loop, from the SMALLEST state up to the one we want.
//        Steps: (1) make dp,  (2) fill the base cases,  (3) loop over the states in order, using the SAME formula as
//               the recursion, reading dp instead of calling.
//   IV.  Space Optimization: very often dp[i] only needs the last 1 or 2 values (or the previous ROW of a 2D table).
//        Keep just those in a few variables, instead of the whole array.
//
//   Memoization and Tabulation have the same T.C. Tabulation avoids the recursion stack (no stack overflow for big n,
//   and no cost of function calls), and it is the one that can be space-optimized.
//
// ● How to write the recursion for a DP problem (the hard part):
//   1. Describe the problem with an INDEX (and other changing values): "f(i) = the answer for the first i items" or
//      "f(i, j) = the answer for cell (i, j)".
//   2. At that index, try EVERY choice (take / not take, go left / go up, jump 1 / jump 2, ...).
//   3. Combine the choices: min / max of them (optimization), or their sum (counting the ways).
//   4. Write the base case: the smallest index, where the answer is known directly.


using System;

public class Solution
{
	// I. Recursion: fib(n) = fib(n - 1) + fib(n - 2), fib(0) = 0, fib(1) = 1.
	// T.C: O(2^n);   the call tree doubles at every level
	// S.C: O(n);     the recursion stack holds only one path of the tree at a time (at most n calls deep)
	// Note: The C++ comment said S.C: O(2^n). Only the CALLS add up to 2^n, the stack is never deeper than n.
	public int Fib_Recursion(int n)
	{
		if (n <= 1) return n;
		return Fib_Recursion(n - 1) + Fib_Recursion(n - 2);
	}

// ------------------------------------------------------------------------------

	// II. Memoization: the same recursion, but every fib(i) is solved only once and kept in dp[i].
	// T.C: O(n);   n + 1 different states, each solved once
	// S.C: O(n);   dp array + O(n) recursion stack
	public int Fib_Memoization(int n)
	{
		int[] dp = new int[n + 1];
		Array.Fill(dp, -1);   // -1 = not solved yet
		return Fib(n, dp);
	}

	private int Fib(int n, int[] dp)
	{
		if (n <= 1) return n;
		if (dp[n] != -1) return dp[n];   // already solved

		dp[n] = Fib(n - 1, dp) + Fib(n - 2, dp);
		return dp[n];
	}

// ------------------------------------------------------------------------------

	// III. Tabulation: fill dp from the bottom: dp[0], dp[1], then dp[2] .. dp[n] with the same formula.
	// T.C: O(n)
	// S.C: O(n)
	public int Fib_Tabulation(int n)
	{
		if (n <= 1) return n;   // dp[1] would not exist for n = 0

		int[] dp = new int[n + 1];
		dp[0] = 0;
		dp[1] = 1;
		for (int i = 2; i <= n; i++) dp[i] = dp[i - 1] + dp[i - 2];

		return dp[n];
	}

// ------------------------------------------------------------------------------

	// IV. Space Optimization: dp[i] only needs dp[i - 1] and dp[i - 2]. Keep just those 2.
	//     prev2 = dp[i - 2], prev = dp[i - 1]. After computing curr, slide both one step forward.
	// T.C: O(n)
	// S.C: O(1)
	public int Fib_SpaceOptimized(int n)
	{
		if (n <= 1) return n;

		int prev2 = 0, prev = 1;
		for (int i = 2; i <= n; i++)
		{
			int curr = prev + prev2;
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
		// Fibonacci: 0, 1, 1, 2, 3, 5, 8, 13, 21, ...
		Console.WriteLine(new Solution().Fib_Recursion(6));        // o/p: 8
		Console.WriteLine(new Solution().Fib_Memoization(6));      // o/p: 8
		Console.WriteLine(new Solution().Fib_Tabulation(6));       // o/p: 8
		Console.WriteLine(new Solution().Fib_SpaceOptimized(6));   // o/p: 8
	}
}

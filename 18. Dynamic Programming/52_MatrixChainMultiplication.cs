// GFG: Matrix Chain Multiplication   --->   arr has n numbers that describe n - 1 matrices: matrix i (for i = 1..n - 1)
// has size arr[i - 1] x arr[i]. Multiply all of them in order (M1 * M2 * ... ), choosing where to put the brackets.
// Return the minimum number of scalar multiplications needed.

// Example 1:
// Input: arr = [2, 1, 3, 4]   (M1: 2x1, M2: 1x3, M3: 3x4)
// Output: 20
// Explanation: (M1 * M2) * M3 costs 2*1*3 + 2*3*4 = 6 + 24 = 30.
//              M1 * (M2 * M3) costs 1*3*4 + 2*1*4 = 12 + 8 = 20.   <- the minimum

// Example 2:
// Input: arr = [1, 2, 3, 4, 3]
// Output: 30   (((M1 * M2) * M3) * M4: 1*2*3 + 1*3*4 + 1*4*3)

// Example 3:
// Input: arr = [3, 4]
// Output: 0    (only one matrix, nothing to multiply)

// Problem link: https://www.geeksforgeeks.org/problems/matrix-chain-multiplication0303/1

// ● Matrix basics: an (x by y) matrix times a (y by z) matrix gives an (x by z) matrix, and costs x * y * z
//   multiplications. The middle sizes must match. The ORDER of the matrices can not change (A * B != B * A), but the
//   BRACKETS can: (A * B) * C == A * (B * C), with a different cost.

// PARTITION DP (also called Interval DP / MCM pattern): solve f(i, j) for a whole range, by trying every place k to
// SPLIT the range into two parts, solving each part, and combining. The 3 steps for every partition DP problem:
//   1. Start with the whole range (i, j).
//   2. Try every split point k in a loop.
//   3. Return the best split.

// The recursion: f(i, j) = the cheapest cost to multiply matrices i..j into one.
//   Split after matrix k (i <= k < j): (Mi .. Mk) * (Mk+1 .. Mj).
//     Left part costs f(i, k), and becomes an arr[i - 1] x arr[k] matrix.
//     Right part costs f(k + 1, j), and becomes an arr[k] x arr[j] matrix.
//     Multiplying those two costs arr[i - 1] * arr[k] * arr[j].
//   f(i, j) = min over k of:  f(i, k) + f(k + 1, j) + arr[i - 1] * arr[k] * arr[j]
//   f(i, i) = 0   (one matrix alone)
// Answer: f(1, n - 1).


using System;

public class Solution
{
	// T.C: exponential (every range tries every split, and the same ranges are solved again and again)
	// S.C: O(n)
	public int MatrixMultiplication_Recursion(int[] arr)
	{
		return MinCost(1, arr.Length - 1, arr);
	}

	private int MinCost(int i, int j, int[] arr)
	{
		if (i == j) return 0;

		int best = int.MaxValue;
		for (int k = i; k < j; k++)
		{
			best = Math.Min(best, MinCost(i, k, arr) + MinCost(k + 1, j, arr) + arr[i - 1] * arr[k] * arr[j]);
		}
		return best;
	}

// ------------------------------------------------------------------------------

	// T.C: O(n^3);   n^2 ranges, each tries up to n splits
	// S.C: O(n^2)
	public int MatrixMultiplication_Memoization(int[] arr)
	{
		int n = arr.Length;
		int[,] dp = new int[n, n];
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++) dp[i, j] = -1;
		}
		return MinCost(1, n - 1, arr, dp);
	}

	private int MinCost(int i, int j, int[] arr, int[,] dp)
	{
		if (i == j) return 0;
		if (dp[i, j] != -1) return dp[i, j];

		int best = int.MaxValue;
		for (int k = i; k < j; k++)
		{
			best = Math.Min(best, MinCost(i, k, arr, dp) + MinCost(k + 1, j, arr, dp) + arr[i - 1] * arr[k] * arr[j]);
		}
		return dp[i, j] = best;
	}

// ------------------------------------------------------------------------------

	// Fill order: f(i, j) needs f(i, k) (same i, smaller j) and f(k + 1, j) (bigger i). So go i from n - 1 DOWN to 1,
	// and j from i + 1 UP to n - 1. (Ranges of one matrix, i == j, stay 0.)
	// T.C: O(n^3)
	// S.C: O(n^2)
	public int MatrixMultiplication(int[] arr)
	{
		int n = arr.Length;
		int[,] dp = new int[n, n];

		for (int i = n - 1; i >= 1; i--)
		{
			for (int j = i + 1; j <= n - 1; j++)
			{
				int best = int.MaxValue;
				for (int k = i; k < j; k++)
				{
					best = Math.Min(best, dp[i, k] + dp[k + 1, j] + arr[i - 1] * arr[k] * arr[j]);
				}
				dp[i, j] = best;
			}
		}
		return dp[1, n - 1];
	}
}


class Program
{
	static void Main()
	{
		int[] arr = { 1, 2, 3, 4, 3 };

		Console.WriteLine(new Solution().MatrixMultiplication_Recursion(arr));     // o/p: 30
		Console.WriteLine(new Solution().MatrixMultiplication_Memoization(arr));   // o/p: 30
		Console.WriteLine(new Solution().MatrixMultiplication(arr));               // o/p: 30
	}
}

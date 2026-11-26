// Sieve of Eratosthenes   --->   Given an integer n, find all prime numbers from 2 to n.

// Example 1:
// Input: n = 23
// Output: 2 3 5 7 11 13 17 19 23

// Example 2:
// Input: n = 10
// Output: 2 3 5 7

// Problem with checking every number one by one with IsPrime() (from '04. Fundamental_Programs\08_PrimeNumber.cs'):
// It costs O(sqrt(n)) per number, so O(n * sqrt(n)) in total.
// Solution (Sieve): Instead of checking each number, CROSS OUT the multiples of every prime. Whatever is never crossed
// out is prime. (A "sieve" is a strainer that lets small things through and keeps the rest.)
// (Leetcode 204. Count Primes uses exactly this, see '04. Fundamental_Programs\09_CountPrimes.cs'.)


using System;
using System.Collections.Generic;

public class Solution
{
	// Basic sieve:
	// 1. Make a bool array 'isPrime' of size n + 1, and mark everything true. Then mark 0 and 1 false.
	// 2. For every i from 2 to n: if i is still marked prime, cross out its multiples 2i, 3i, 4i, ... (mark them false).
	// 3. The numbers still marked true are the primes.
	// e.g., n = 10:  i = 2: cross 4, 6, 8, 10.  i = 3: cross 6, 9.  i = 5: cross 10.  Left: 2, 3, 5, 7
	// T.C: O(nlog(logn));   n/2 + n/3 + n/5 + ... (only for primes) adds up to about n * log(logn)
	// S.C: O(n);   for the 'isPrime' array
	public List<int> Sieve_Basic(int n)
	{
		bool[] isPrime = new bool[n + 1];
		Array.Fill(isPrime, true);
		isPrime[0] = false;
		if (n >= 1) isPrime[1] = false;

		for (int i = 2; i <= n; i++)
		{
			if (!isPrime[i]) continue;

			for (int j = 2 * i; j <= n; j += i) isPrime[j] = false;
		}

		List<int> primes = new List<int>();
		for (int i = 2; i <= n; i++)
		{
			if (isPrime[i]) primes.Add(i);
		}
		return primes;
	}

// ------------------------------------------------------------------------------

	// Optimised sieve: Same answer, less work. 2 changes:
	// Optimisation 1: Start crossing from i * i, not from 2i.
	//                 The smaller multiples 2i, 3i, ..., (i - 1) * i have a smaller factor, so a smaller prime has
	//                 already crossed them out. e.g., for i = 5: 10 (by 2), 15 (by 3), 20 (by 2) are already crossed.
	//                 So start from 25.
	// Optimisation 2: Run the outer loop only while i * i <= n.
	//                 If i * i > n, then i's first multiple to cross (i * i) is already beyond n. Nothing to do.
	// e.g., n = 23: only i = 2 (cross 4, 6, ..., 22) and i = 3 (cross 9, 12, ..., 21) do any crossing.
	//               i = 4 is not prime, and i = 5 has 5 * 5 = 25 > 23, so the loop stops.
	// T.C: O(nlog(logn));   same big-O, but much fewer steps in practice
	// S.C: O(n)
	public List<int> Sieve(int n)
	{
		bool[] isPrime = new bool[n + 1];
		Array.Fill(isPrime, true);
		isPrime[0] = false;
		if (n >= 1) isPrime[1] = false;

		for (int i = 2; i * i <= n; i++)
		{
			if (!isPrime[i]) continue;

			for (int j = i * i; j <= n; j += i) isPrime[j] = false;
		}

		List<int> primes = new List<int>();
		for (int i = 2; i <= n; i++)
		{
			if (isPrime[i]) primes.Add(i);
		}
		return primes;
	}
}


class Program
{
	static void Main()
	{
		int n = 23;

		Console.WriteLine(string.Join(" ", new Solution().Sieve_Basic(n)));   // o/p: 2 3 5 7 11 13 17 19 23
		Console.WriteLine(string.Join(" ", new Solution().Sieve(n)));         // o/p: 2 3 5 7 11 13 17 19 23
	}
}

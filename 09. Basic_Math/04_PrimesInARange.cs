// Primes in a Range   --->   Given two integers left and right, return all prime numbers in the range [left, right].
// The range can be very large: left and right can be up to 10^9 (but right - left is small, up to about 10^6).

// Example 1:
// Input: left = 10, right = 20
// Output: [11, 13, 17, 19]

// Example 2:
// Input: left = 2, right = 17
// Output: [2, 3, 5, 7, 11, 13, 17]

// Example 3 (large range):
// Input: left = 110000000, right = 110000100
// Output: [110000017, 110000039, 110000057, 110000083]

// First understand '03_SieveOfEratosthenes.cs'.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: Check every number from left to right with IsPrime() (only up to sqrt(x), see
	// '04. Fundamental_Programs\08_PrimeNumber.cs').
	// T.C: O((right - left) * sqrt(right))
	// S.C: O(1);   ignoring the answer list
	public List<int> PrimesInRange_BruteForce(int left, int right)
	{
		List<int> primes = new List<int>();
		for (int x = Math.Max(left, 2); x <= right; x++)
		{
			if (IsPrime(x)) primes.Add(x);
			if (x == int.MaxValue) break;   // x++ would overflow
		}
		return primes;
	}

	private bool IsPrime(int n)
	{
		for (int i = 2; i <= n / i; i++)
		{
			if (n % i == 0) return false;
		}
		return n >= 2;
	}

// ------------------------------------------------------------------------------

	// Better algorithm (small range): Make a normal sieve from 0 to right, and keep only the primes >= left.
	// Problem: For right = 10^9, the sieve needs an array of 10^9 bools = about 1 GB of memory, and it does work for all
	//          numbers below 'left' too, which we don't even need.
	// T.C: O(right * log(log(right)))
	// S.C: O(right)
	public List<int> PrimesInRange_Sieve(int left, int right)
	{
		bool[] isPrime = new bool[right + 1];
		Array.Fill(isPrime, true);

		for (int i = 2; i <= right / i; i++)
		{
			if (!isPrime[i]) continue;
			for (int j = i * i; j <= right; j += i) isPrime[j] = false;
		}

		List<int> primes = new List<int>();
		for (int x = Math.Max(left, 2); x <= right; x++)
		{
			if (isPrime[x]) primes.Add(x);
		}
		return primes;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm (large range): Segmented Sieve. Make a sieve ONLY for the numbers from left to right.
	// Key fact: Every non-prime x has a prime factor <= sqrt(x). So to cross out all non-primes up to 'right', we only need
	//           the primes up to sqrt(right). These are called "base primes". For right = 10^9, sqrt is only about 31623.
	// 1. Find the base primes: a normal sieve from 0 to sqrt(right).
	// 2. Make a bool array 'isPrime' of size (right - left + 1). Index 0 means 'left', index 1 means 'left + 1', and so on.
	//    So number x is at index (x - left).
	// 3. For every base prime p, cross out its multiples inside [left, right]:
	//    • First multiple of p that is >= left: ceil(left / p) * p = ((left + p - 1) / p) * p
	//    • But never start below p * p (same reason as the optimised sieve: smaller multiples have a smaller factor,
	//      and it also stops p from crossing out itself when p is inside the range).
	//    • Cross out start, start + p, start + 2p, ... up to right.
	// 4. Numbers 0 and 1 are not prime, so if they are inside the range, mark them false. What is left is prime.
	// e.g., left = 110, right = 130. Base primes up to sqrt(130) = 11: 2, 3, 5, 7, 11.
	//       p = 3: first multiple >= 110 is 111. Cross 111, 114, 117, 120, 123, 126, 129.
	//       p = 7: first multiple >= 110 is 112. Cross 112, 119, 126.
	//       After all base primes, only 113 and 127 are left.
	// Why 'long' for the multiples ? If right is close to int.MaxValue, 'j + p' can go past it and overflow.
	// T.C: O(sqrt(right) * log(log(sqrt(right))) + (right - left + 1) * log(log(right)))
	// S.C: O(sqrt(right) + (right - left + 1))
	public List<int> PrimesInRange(int left, int right)
	{
		// Step 1: base primes up to sqrt(right)
		int limit = (int)Math.Sqrt(right);
		bool[] small = new bool[limit + 1];
		Array.Fill(small, true);
		List<int> basePrimes = new List<int>();
		for (int i = 2; i <= limit; i++)
		{
			if (!small[i]) continue;
			basePrimes.Add(i);
			for (int j = i * i; j <= limit; j += i) small[j] = false;
		}

		// Step 2 and 3: cross out multiples of base primes inside [left, right]
		bool[] isPrime = new bool[right - left + 1];
		Array.Fill(isPrime, true);

		foreach (int p in basePrimes)
		{
			long start = Math.Max((long)p * p, ((long)left + p - 1) / p * p);
			for (long j = start; j <= right; j += p) isPrime[j - left] = false;
		}

		// Step 4: 0 and 1 are not prime
		for (int x = left; x <= Math.Min(right, 1); x++) isPrime[x - left] = false;

		List<int> primes = new List<int>();
		for (int i = 0; i < isPrime.Length; i++)
		{
			if (isPrime[i]) primes.Add(left + i);
		}
		return primes;
	}
}


class Program
{
	static void Main()
	{
		int left = 10, right = 20;

		Console.WriteLine(string.Join(" ", new Solution().PrimesInRange_BruteForce(left, right)));   // o/p: 11 13 17 19
		Console.WriteLine(string.Join(" ", new Solution().PrimesInRange_Sieve(left, right)));        // o/p: 11 13 17 19
		Console.WriteLine(string.Join(" ", new Solution().PrimesInRange(left, right)));              // o/p: 11 13 17 19
	}
}

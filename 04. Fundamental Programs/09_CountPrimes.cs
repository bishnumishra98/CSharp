// Leetcode: 204. Count Primes   --->   Given an integer n, return the number of prime numbers that are strictly less
// than n.

// Example 1:
// Input: n = 10
// Output: 4
// Explanation: There are 4 prime numbers less than 10, they are 2, 3, 5, 7.

// Example 2:
// Input: n = 0
// Output: 0

// Example 3:
// Input: n = 1
// Output: 0

// Constraints: 0 <= n <= 5 * 10^6


using System;

public class Solution
{
	// Brute force: Check every number from 2 to n - 1 with IsPrime() (from '08_PrimeNumber.cs'), and count the primes.
	// Problem: For n = 5 * 10^6, this is too slow (Time Limit Exceeded).
	// T.C: O(n * sqrt(n))
	// S.C: O(1)
	public int CountPrimes_BruteForce(int n)
	{
		int count = 0;
		for (int i = 2; i < n; i++)
		{
			if (IsPrime(i)) count++;
		}
		return count;
	}

	private bool IsPrime(int n)
	{
		for (int i = 2; i <= n / i; i++)
		{
			if (n % i == 0) return false;
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Sieve of Eratosthenes.
	// Idea: Instead of checking each number, cross out the multiples of every prime. Whatever is left is prime.
	// 1. Make a bool array 'isPrime' of size n, and mark everything as true (prime). Then mark 0 and 1 as false.
	// 2. For every i from 2 while i * i < n: if i is still prime, cross out all its multiples (mark them false):
	//    i * i, i * i + i, i * i + 2i, ...
	//    ● Why start from i * i ? Smaller multiples like 2i, 3i, ... are already crossed out by smaller primes.
	//      e.g., for i = 5: 10 (by 2), 15 (by 3), 20 (by 2) are already crossed. So start from 25.
	//    ● Why stop at i * i < n ? If i * i >= n, all its multiples below n are already crossed by smaller primes.
	// 3. Count the numbers still marked true.
	// e.g., n = 10:  i = 2: cross 4, 6, 8.  i = 3: cross 9.  i = 4: 4 * 4 = 16 >= 10, stop.
	//       Left as prime: 2, 3, 5, 7  ->  4 primes.
	// T.C: O(n * log(logn))
	// S.C: O(n);   for the 'isPrime' array
	public int CountPrimes(int n)
	{
		if (n < 3) return 0;   // no prime is strictly less than 2

		bool[] isPrime = new bool[n];
		Array.Fill(isPrime, true);
		isPrime[0] = isPrime[1] = false;

		for (int i = 2; i * i < n; i++)
		{
			if (!isPrime[i]) continue;

			for (int j = i * i; j < n; j += i)
			{
				isPrime[j] = false;
			}
		}

		int count = 0;
		for (int i = 2; i < n; i++)
		{
			if (isPrime[i]) count++;
		}
		return count;
	}
}


class Program
{
	static void Main()
	{
		int n = 10;

		Console.WriteLine(new Solution().CountPrimes_BruteForce(n));   // o/p: 4
		Console.WriteLine(new Solution().CountPrimes(n));              // o/p: 4
	}
}

// GFG: LCM And GCD   --->   Given two positive integers a and b, find their GCD (Greatest Common Divisor) and LCM (Least
// Common Multiple).
// • GCD (also called HCF): the biggest number that divides both a and b.
// • LCM: the smallest number that both a and b divide.

// Example 1:
// Input: a = 14, b = 8
// Output: GCD = 2, LCM = 56
// Explanation: Divisors of 14: 1, 2, 7, 14. Divisors of 8: 1, 2, 4, 8. Biggest common one = 2.
//              Multiples of 14: 14, 28, 42, 56, ...  56 is the first one that 8 also divides.

// Example 2:
// Input: a = 5, b = 10
// Output: GCD = 5, LCM = 10

// Problem link: https://www.geeksforgeeks.org/problems/lcm-and-gcd4516/1
// (Leetcode 1979. Find Greatest Common Divisor of Array is the same: GCD of the smallest and largest element.)


using System;

public class Solution
{
	// Brute force: Try every number from min(a, b) down to 1. The first one that divides both is the GCD.
	// (Start from min(a, b), because no divisor of a number can be bigger than the number itself.)
	// T.C: O(min(a, b))
	// S.C: O(1)
	public int GCD_BruteForce(int a, int b)
	{
		for (int i = Math.Min(a, b); i > 1; i--)
		{
			if (a % i == 0 && b % i == 0) return i;
		}
		return 1;   // 1 divides every number
	}

// ------------------------------------------------------------------------------

	// Better algorithm: Euclid's algorithm with subtraction.
	// Key fact: gcd(a, b) = gcd(a - b, b), when a > b.
	// Why ? If a number divides both a and b, it also divides their difference a - b. (e.g., 2 divides 14 and 8, so it
	//       also divides 14 - 8 = 6.) So both pairs have exactly the same common divisors, and the same GCD.
	// 1. Keep replacing the bigger number with (bigger - smaller).
	// 2. When both become equal, that number is the GCD.
	// e.g., (14, 8) -> (6, 8) -> (6, 2) -> (4, 2) -> (2, 2)  ->  GCD = 2
	// Problem: For (1000000000, 1), it subtracts 1 almost a billion times.
	// T.C: O(max(a, b)) worst case
	// S.C: O(1)
	public int GCD_Subtraction(int a, int b)
	{
		while (a != b)
		{
			if (a > b) a = a - b;
			else b = b - a;
		}
		return a;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Euclid's algorithm with modulo.
	// Problem with subtraction: It subtracts the smaller number again and again, one at a time.
	// Solution: Subtracting b from a as many times as possible leaves exactly a % b. So do it in one step:
	//           gcd(a, b) = gcd(b, a % b). Stop when b becomes 0. Then gcd(a, 0) = a (every number divides 0).
	// e.g., (14, 8) -> (8, 14 % 8 = 6) -> (6, 8 % 6 = 2) -> (2, 6 % 2 = 0)  ->  GCD = 2
	// Note: If a < b at the start, the first step just swaps them: (8, 14) -> (14, 8 % 14 = 8) -> ...
	// T.C: O(log(min(a, b)));   the numbers at least halve every 2 steps
	// S.C: O(1)
	public int GCD(int a, int b)
	{
		while (b != 0)
		{
			int remainder = a % b;
			a = b;
			b = remainder;
		}
		return a;
	}

// ------------------------------------------------------------------------------

	// LCM using GCD: a * b = GCD(a, b) * LCM(a, b). So LCM = (a * b) / GCD.
	// e.g., 14 * 8 = 112,  112 / 2 = 56
	// Overflow trick: a * b can be too big even when the LCM fits. So divide FIRST: LCM = (a / GCD) * b.
	//                 a / GCD is always a whole number, as GCD divides a. Use long, as the LCM itself can be bigger than int.
	// T.C: O(log(min(a, b)))
	// S.C: O(1)
	public long LCM(int a, int b)
	{
		return (long)(a / GCD(a, b)) * b;
	}
}

// Built-in in C#: System.Numerics.BigInteger.GreatestCommonDivisor(a, b) (there is no Math.Gcd in C#).


class Program
{
	static void Main()
	{
		int a = 14, b = 8;

		Console.WriteLine(new Solution().GCD_BruteForce(a, b));    // o/p: 2
		Console.WriteLine(new Solution().GCD_Subtraction(a, b));   // o/p: 2
		Console.WriteLine(new Solution().GCD(a, b));               // o/p: 2
		Console.WriteLine(new Solution().LCM(a, b));               // o/p: 56
	}
}

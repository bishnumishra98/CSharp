// Recursion   --->   A function that calls itself is called recursion.
// It is useful when a big problem can be broken into a smaller version of the SAME problem.
// e.g., factorial(5) = 5 * factorial(4), and factorial(4) is the same problem, just smaller.

// ● Every recursive function has 3 parts:
//   I.   Base case (must have): The smallest input, where the function stops and returns directly without calling itself.
//        Without it, the function calls itself forever.
//   II.  Recursive call (must have): The function calls itself with a SMALLER input, moving towards the base case.
//   III. Processing (optional): The work done in the current call (printing, adding, multiplying ...). It can be done
//        BEFORE the recursive call or AFTER it (when the call returns). That changes the order of the output.

// ● Call stack: Every call waits on a "stack" until the call it made comes back. e.g., Factorial(3):
//      Factorial(3) waits for Factorial(2), which waits for Factorial(1)   <- base case, returns 1
//      then Factorial(2) returns 2 * 1 = 2, then Factorial(3) returns 3 * 2 = 6
//   So recursion uses O(depth) extra space for the stack.
//   In C#, the stack is about 1 MB. Very deep recursion (or a missing base case) causes a StackOverflowException, which
//   can't be caught: the program just crashes. For very deep problems, a loop is safer.


using System;

public class Solution
{
	// 1. Print 1 to n: Processing (printing) BEFORE the recursive call, so numbers come out in the order the calls go.
	// PrintOneToN(1, 3): print 1 -> PrintOneToN(2, 3): print 2 -> PrintOneToN(3, 3): print 3 -> PrintOneToN(4, 3): stop
	// T.C: O(n)
	// S.C: O(n);   n calls wait on the stack
	public void PrintOneToN(int i, int n)
	{
		if (i > n) return;           // base case

		Console.Write(i + " ");      // processing
		PrintOneToN(i + 1, n);       // recursive call
	}

// ------------------------------------------------------------------------------

	// 2. Print n to 1 using the SAME calls: Just move the printing AFTER the recursive call.
	// Now nothing is printed on the way down. Printing happens on the way back up, when the calls return, i.e., in
	// reverse order. (This "work after returning" is the idea behind backtracking.)
	// PrintNToOne(1, 3) -> PrintNToOne(2, 3) -> PrintNToOne(3, 3) -> PrintNToOne(4, 3): stop
	//                                           print 3 <- returns
	//                      print 2 <- returns
	// print 1 <- returns
	// T.C: O(n)
	// S.C: O(n)
	public void PrintNToOne(int i, int n)
	{
		if (i > n) return;

		PrintNToOne(i + 1, n);
		Console.Write(i + " ");
	}

// ------------------------------------------------------------------------------

	// 3. Factorial: n! = n * (n - 1)!, and the base case is 0! = 1! = 1.
	// T.C: O(n)
	// S.C: O(n);   recursion stack
	public long Factorial(int n)
	{
		if (n <= 1) return 1;
		return n * Factorial(n - 1);
	}

// ------------------------------------------------------------------------------

	// 4. GCD (Euclid): gcd(a, b) = gcd(b, a % b), and the base case is gcd(a, 0) = a.
	// (Loop version, brute force, and LCM are in '09. Basic Math\05_GCDAndLCM.cs'.)
	// T.C: O(log(min(a, b)))
	// S.C: O(log(min(a, b)));   recursion stack
	public int GCD(int a, int b)
	{
		if (b == 0) return a;
		return GCD(b, a % b);
	}

// ------------------------------------------------------------------------------

	// 5. Power x^n (n can be negative): Divide and conquer.
	// • x^n = x^(n/2) * x^(n/2)          if n is even
	// • x^n = x^(n/2) * x^(n/2) * x      if n is odd and positive
	// • x^n = x^(n/2) * x^(n/2) / x      if n is odd and negative   (e.g., x^-3 = x^-1 * x^-1 / x)
	// • x^0 = 1                          base case
	// Important: Calculate x^(n/2) ONCE and store it in 'half'. Calling MyPow(x, n / 2) twice would make 2 calls at every
	//            level, which adds up to O(n) calls instead of O(logn).
	// Note: No need to make n positive here. In C#, n / 2 moves towards 0 for negative n too (-5 / 2 = -2), and n % 2 is
	//       -1 for negative odd n, so the cases above still work, even for n = int.MinValue.
	// Note: double can't store some decimals exactly. e.g., MyPow(10, -2) prints 0.010000000000000002, not 0.01,
	//       because 0.1 has no exact binary form (like 1/3 = 0.333... has no exact decimal form). Leetcode accepts such
	//       tiny differences.
	// (Loop version: '09. Basic Math\02_PowXN.cs'.)
	// T.C: O(logn)
	// S.C: O(logn);   recursion stack
	public double MyPow(double x, int n)
	{
		if (n == 0) return 1;

		double half = MyPow(x, n / 2);

		if (n % 2 == 0) return half * half;
		if (n > 0) return half * half * x;
		return half * half / x;
	}
}


class Program
{
	static void Main()
	{
		new Solution().PrintOneToN(1, 5);
		Console.WriteLine();                                 // o/p: 1 2 3 4 5
		new Solution().PrintNToOne(1, 5);
		Console.WriteLine();                                 // o/p: 5 4 3 2 1
		Console.WriteLine(new Solution().Factorial(5));      // o/p: 120
		Console.WriteLine(new Solution().GCD(14, 8));        // o/p: 2
		Console.WriteLine(new Solution().MyPow(2, -2));      // o/p: 0.25
	}
}

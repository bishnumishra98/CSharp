// Factorial of a Number   --->   Given a non-negative integer n, find its factorial.
// n! = 1 * 2 * 3 * ... * n, and 0! = 1.

// Example 1:
// Input: n = 5
// Output: 120
// Explanation: 5! = 1 * 2 * 3 * 4 * 5 = 120

// Example 2:
// Input: n = 0
// Output: 1


using System;

public class Solution
{
	// Multiply all numbers from 1 to n.
	// Why 'long' ? Factorials grow very fast. int can hold only up to 12!, and long up to 20!.
	// For n > 20, even long overflows. C# doesn't warn you by default; it just gives a wrong number.
	// ● To get an error instead of a wrong answer, write the multiplication as 'ans = checked(ans * i);'.
	// ● For really big factorials (like 100!), see '05. Arrays\18_FactorialOfLargeNumbers.cs'.
	// T.C: O(n)
	// S.C: O(1)
	public long Factorial(int n)
	{
		if (n < 0) return 0;   // factorial is not defined for negative numbers

		long ans = 1;
		for (int i = 2; i <= n; i++)
		{
			ans = ans * i;
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().Factorial(5));   // o/p: 120
	}
}

// GFG: Factorials of large numbers   --->   Given an integer N, find its factorial. Return a list of integers denoting
// the digits that make up the factorial of N.

// Example 1:
// Input: N = 5
// Output: [1,2,0]
// Explanation : 5! = 1*2*3*4*5 = 120

// Example 2:
// Input: N = 10
// Output: [3,6,2,8,8,0,0]
// Explanation : 10! = 1*2*3*4*5*6*7*8*9*10 = 3628800

// Problem link: https://www.geeksforgeeks.org/problems/factorials-of-large-numbers2508/1

// Why is this a problem? Factorials grow very fast. 20! already fills a 'long', and 21! does not fit in any built-in
// number type. So we store the answer as a list of digits and multiply digit by digit, like on paper.
// (C# has System.Numerics.BigInteger for huge numbers, but interviewers want to see this digit-by-digit logic.)

// Algorithm: Understand 17_AddTwoNumbersRepresentedByTwoArrays.cs first to get the intuition.
// Multiplication works the same way as addition: digit by digit, with a carry.
// 1. Start with ans = [1] (1! = 1). The digits are stored in REVERSE order (unit's digit at index 0), because new
//    digits from the carry are added at the end, and adding at the end of a List is easy.
// 2. For every i from 2 to N, multiply the number stored in 'ans' by i:
//    -> For each digit: product = digit * i + carry. Keep (product % 10) as the new digit, and carry = product / 10.
//    -> After all digits, if carry is left, add its digits at the end one by one.
// 3. Reverse 'ans' to get the digits in the correct order and return.
// Dry run the algorithm for better understanding. e.g., ans = [0, 2, 1] (i.e., 120) * 6:
//    0 * 6 + 0 = 0  -> digit 0, carry 0
//    2 * 6 + 0 = 12 -> digit 2, carry 1
//    1 * 6 + 1 = 7  -> digit 7, carry 0
//    ans = [0, 2, 7] -> reversed: 720 (= 6!)


using System;
using System.Collections.Generic;

public class Solution
{
	// T.C: O(N^2 * logN);   the outer loop runs from 2 to N, i.e., O(N) iterations. In the inner loop, we multiply every
	//      digit of 'ans'. N! has roughly N * logN digits (by Stirling's approximation). Combining these:
	//      O(N * N * logN) = O(N^2 * logN)
	// S.C: O(N * logN);   for the digits stored in 'ans'
	public List<int> Factorial(int N)
	{
		// Initialize a list to store the digits of the factorial. Starting with 1 because factorial of 1 is 1.
		List<int> ans = new List<int>();
		ans.Add(1);

		int carry = 0;
		// Loop from 2 to N to calculate N!
		for (int i = 2; i <= N; i++)
		{
			// Multiply each digit in 'ans' by i
			for (int j = 0; j < ans.Count; j++)
			{
				int product = ans[j] * i + carry;   // multiply current digit by i and add carry
				ans[j] = product % 10;   // store the last digit of the product in 'ans'
				carry = product / 10;    // update carry with the rest of the product
			}
			// If carry remains after multiplying all digits, add it to 'ans' digit by digit
			while (carry > 0)
			{
				ans.Add(carry % 10);   // insert the last digit of the carry
				carry = carry / 10;    // update carry with the remaining part
			}
		}

		// The digits in 'ans' are stored in reverse order, so reverse the list to get the correct order
		ans.Reverse();
		return ans;
	}
}


class Program
{
	static void Main()
	{
		List<int> ans = new Solution().Factorial(10);
		Console.WriteLine(string.Join("", ans));   // o/p: 3628800
	}
}

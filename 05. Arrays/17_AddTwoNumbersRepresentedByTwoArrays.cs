// GFG: Add two numbers represented by two arrays   --->   Given two arrays arr1 and arr2, representing two numbers
// such that every element of arrays represents a digit. The task is to find the sum of both the numbers.

// Example 1:
// Input : arr1[] = [1, 2], arr2[] = [2, 1]
// Output : 33
// Explanation: Number represented by arr1 is 12, and by arr2 is 21. Sum = 12 + 21 = 33.

// Example 2:
// Input : arr1[] = [9, 5, 4, 9], arr2[] = [2, 1, 4]
// Output : 9763
// Explanation : sum of 9549 and 214 is 9763.

// Problem link: https://www.geeksforgeeks.org/problems/add-two-numbers-represented-by-two-arrays2408/1

// Why not just convert the arrays to int and add? Because the arrays can be very long (like 100 digits), and such a big
// number does not fit in int or long. So we add digit by digit, just like we do on paper.

// Algorithm: Same as addition on paper.
// 1. Initialize a 'carry' variable with 0, and keep 2 pointers i and j on the last element of arr1 and arr2 respectively,
//    as addition always starts from the unit's digit. Also create a StringBuilder 'ans' to build the answer.
// 2. Add arr1[i], arr2[j] and carry. Store (sum % 10) in 'ans' (the last digit), and update carry with sum / 10.
//    e.g., 9 + 4 + 0 = 13 -> store 3, carry = 1.
// 3. Keep doing the same, until i or j goes before the first element.
// 4. If some digits are left in arr1 or arr2, process them alone in the same way (with the carry).
// 5. If something is left in carry, add that too. Note that carry can only be 1 or 0.
// 6. The digits get stored in 'ans' in reverse order (unit's digit first), so reverse the string and return.


using System;
using System.Text;   // for StringBuilder

public class Solution
{
	// T.C: O(max(n, m));   where n = arr1.Length and m = arr2.Length respectively
	// S.C: O(max(n, m))
	public string CalcSum(int[] arr1, int[] arr2)
	{
		int i = arr1.Length - 1;
		int j = arr2.Length - 1;
		int carry = 0;
		StringBuilder ans = new StringBuilder();

		// Add both arrays till same length
		while (i >= 0 && j >= 0)
		{
			int sum = arr1[i] + arr2[j] + carry;
			ans.Append(sum % 10);
			carry = sum / 10;
			i--;
			j--;
		}

		// Add arr1 digits in 'ans' if they are still left to be processed
		while (i >= 0)
		{
			int sum = arr1[i] + carry;
			ans.Append(sum % 10);
			carry = sum / 10;
			i--;
		}

		// Add arr2 digits in 'ans' if they are still left to be processed
		while (j >= 0)
		{
			int sum = arr2[j] + carry;
			ans.Append(sum % 10);
			carry = sum / 10;
			j--;
		}

		// Add carry, if it's left. Carry can be either 0 or 1 only. So if a carry is present, it is for sure 1.
		if (carry > 0) ans.Append(1);

		// Reverse the string (see 08. String/01_ReverseString.cs)
		char[] digits = ans.ToString().ToCharArray();
		Array.Reverse(digits);
		return new string(digits);
	}
}


class Program
{
	static void Main()
	{
		int[] arr1 = { 9, 5, 4, 9 };
		int[] arr2 = { 2, 1, 4 };

		Console.WriteLine(new Solution().CalcSum(arr1, arr2));   // o/p: 9763
	}
}

// Leetcode 67. Add Binary   --->   Given two binary strings a and b, return their sum as a binary string.

// Example 1:
// Input: a = "11", b = "1"
// Output: "100"

// Example 2:
// Input: a = "1010", b = "1011"
// Output: "10101"

// Constraints: 1 <= a.length, b.length <= 10^4

// Why not convert both to numbers, add, and convert back ? The strings can be 10^4 digits long, which is far too big for
// int or long (long holds only 64 bits). So add digit by digit, just like on paper. (Same idea as
// '05. Arrays\17_AddTwoNumbersRepresentedByTwoArrays.cs', but in base 2.)


using System;
using System.Text;

public class Solution
{
	// Algorithm: Addition on paper, from the last digit.
	// 1. i and j start at the last characters of a and b. carry = 0.
	// 2. While there is a digit left in a OR in b OR a carry:
	//    sum = (digit of a, or 0) + (digit of b, or 0) + carry
	//    Write (sum % 2) as the answer digit, and carry = sum / 2.
	//    In binary: 0 + 0 = 0,  1 + 0 = 1,  1 + 1 = 2 -> write 0 carry 1,  1 + 1 + 1 = 3 -> write 1 carry 1.
	// 3. We wrote the digits from right to left, so reverse the answer at the end.
	// e.g., "11" + "1":  1 + 1 = 2 -> write 0, carry 1.  1 + 0 + 1 = 2 -> write 0, carry 1.  carry 1 -> write 1.
	//                    Written "001", reversed "100".
	// T.C: O(max(n1, n2));   where n1 = a.Length, n2 = b.Length
	// S.C: O(max(n1, n2));   for the answer
	public string AddBinary(string a, string b)
	{
		StringBuilder sb = new StringBuilder();
		int i = a.Length - 1, j = b.Length - 1, carry = 0;

		while (i >= 0 || j >= 0 || carry > 0)
		{
			int sum = carry;
			if (i >= 0) sum += a[i--] - '0';   // a[i] - '0' turns '0'/'1' into 0/1
			if (j >= 0) sum += b[j--] - '0';

			sb.Append(sum % 2);
			carry = sum / 2;
		}

		char[] arr = sb.ToString().ToCharArray();
		Array.Reverse(arr);
		return new string(arr);
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().AddBinary("1010", "1011"));   // o/p: 10101
	}
}

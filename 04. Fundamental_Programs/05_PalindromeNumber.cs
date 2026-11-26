// Leetcode: 9. Palindrome Number   --->   Given an integer x, return true if x is a palindrome, and false otherwise.
// (A palindrome reads the same from left to right and from right to left.)

// Example 1:
// Input: x = 121
// Output: true
// Explanation: 121 reads as 121 from left to right and from right to left.

// Example 2:
// Input: x = -121
// Output: false
// Explanation: From left to right, it reads -121. From right to left, it becomes 121-. Therefore it is not a palindrome.

// Example 3:
// Input: x = 10
// Output: false
// Explanation: Reads 01 from right to left. Therefore it is not a palindrome.


using System;

public class Solution
{
	// Brute force: Reverse the whole number, and compare it with the original.
	// 1. Negative numbers are never palindromes (because of the '-' sign). Return false.
	// 2. Reverse: Take the last digit (x % 10), add it to the end of 'rev' (rev * 10 + digit), remove it (x / 10).
	//    e.g., 121: rev = 1 -> 12 -> 121
	// 3. If rev == original, it's a palindrome.
	// Why 'long rev' ? Reversing a big int can go out of int range, e.g., 2147483647 -> 7463847412.
	// T.C: O(logn);   base 10, i.e., number of digits
	// S.C: O(1)
	public bool IsPalindrome_FullReverse(int x)
	{
		if (x < 0) return false;

		int original = x;   // keep a backup, as x becomes 0 in the loop
		long rev = 0;
		while (x != 0)
		{
			rev = rev * 10 + x % 10;
			x = x / 10;
		}

		return rev == original;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Reverse only HALF of the number.
	// Problem with brute force: It reverses the whole number, which needs 'long' to avoid overflow.
	// Solution: A palindrome's second half reversed is equal to its first half. So reverse digits only until the
	//           reversed part becomes >= the remaining part. Half a number can never overflow.
	// 1. Return false for negative numbers, and for numbers ending with 0 (except 0 itself), because a number can't
	//    start with 0. e.g., 10 -> "01" is not a number.
	// 2. While x > rev: move the last digit of x to rev.
	// 3. Even number of digits: x == rev.        e.g., 1221: x = 12, rev = 12
	//    Odd number of digits: x == rev / 10.    e.g., 12321: x = 12, rev = 123. The middle digit (3) doesn't
	//    matter, so drop it with rev / 10.
	// T.C: O(logn / 2) = O(logn)
	// S.C: O(1)
	public bool IsPalindrome(int x)
	{
		if (x < 0 || (x % 10 == 0 && x != 0)) return false;

		int rev = 0;
		while (x > rev)
		{
			rev = rev * 10 + x % 10;
			x = x / 10;
		}

		return x == rev || x == rev / 10;
	}
}


class Program
{
	static void Main()
	{
		int x = 121;

		Console.WriteLine(new Solution().IsPalindrome_FullReverse(x));   // o/p: True
		Console.WriteLine(new Solution().IsPalindrome(x));               // o/p: True
	}
}

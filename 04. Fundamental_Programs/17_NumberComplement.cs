// Leetcode: 476. Number Complement   --->   The complement of an integer is the integer you get when you flip all the
// 0's to 1's and all the 1's to 0's in its binary representation.
// For example, The integer 5 is "101" in binary and its complement is "010" which is the integer 2.
// Given an integer num, return its complement.

// Example 1:
// Input: num = 5
// Output: 2
// Explanation: The binary representation of 5 is 101 (no leading zero bits), and its complement is 010. So you need
// to output 2.

// Example 2:
// Input: num = 1
// Output: 0
// Explanation: The binary representation of 1 is 1 (no leading zero bits), and its complement is 0. So you need to
// output 0.

// Constraints: 1 <= num < 2^31

// Why not just use ~num ? Because ~ flips ALL 32 bits, including the leading zeros.
// e.g., 5 = 000...0101  ->  ~5 = 111...1010 = -6, but we want only the last 3 bits flipped: 010 = 2.


using System;

public class Solution
{
	// Bit by bit: Go through the bits of num from right to left. If a bit is 0, set that bit in the answer.
	// T.C: O(logn);   base 2, i.e., number of bits
	// S.C: O(1)
	public int FindComplement_BitByBit(int num)
	{
		int ans = 0, pos = 0;

		while (num != 0)
		{
			if ((num & 1) == 0) ans = ans | (1 << pos);   // bit is 0, so make it 1 in the answer
			num = num >> 1;
			pos++;
		}

		return ans;
	}

// ------------------------------------------------------------------------------

	// Mask approach: Make a mask with 1s on exactly as many bits as num has, then XOR. (x ^ 1 flips the bit.)
	// 1. Build the mask: for every bit of num, shift the mask left and add a 1.
	//    e.g., num = 5 (101, 3 bits)  ->  mask = 1 -> 11 -> 111
	// 2. Answer = num ^ mask.  e.g., 101 ^ 111 = 010 = 2
	// T.C: O(logn);   base 2, i.e., number of bits
	// S.C: O(1)
	public int FindComplement(int num)
	{
		int mask = 0;
		int temp = num;

		while (temp != 0)
		{
			mask = (mask << 1) | 1;
			temp = temp >> 1;
		}

		return num ^ mask;
	}
}

// Note: Leetcode 1009. Complement of Base 10 Integer is the same problem, but it also allows n = 0, whose answer is 1.
//       For n = 0, the mask stays 0, so add one line at the start: if (n == 0) return 1;


class Program
{
	static void Main()
	{
		int num = 5;

		Console.WriteLine(new Solution().FindComplement_BitByBit(num));   // o/p: 2
		Console.WriteLine(new Solution().FindComplement(num));            // o/p: 2
	}
}

// GFG: Set kth bit   --->   Given a number N and a value K. From the right, set the Kth bit in the binary
// representation of N. The position of Least Significant Bit (or last bit) is 0, the second last bit is 1 and so on.

// Example 1:
// Input: N = 10, K = 2
// Output: 14
// Explanation: Binary representation of the given number 10 is: 1 0 1 0, number of bits in the binary representation
// is 4. Thus 2nd bit from right is 0. The number after changing this bit to 1 is: 14 (1 1 1 0).

// Example 2:
// Input: N = 15, K = 3
// Output: 15
// Explanation: The binary representation of the given number 15 is: 1 1 1 1, number of bits in the binary
// representation is 4. Thus 3rd bit from the right is 1. The number after changing this bit to 1 is 15 (1 1 1 1).

// Extension: The 4 basic operations on the kth bit (all are used a lot in bit manipulation problems):
// Set (make it 1), Check (is it 1 ?), Clear (make it 0), Toggle (flip it).

// Key idea: A "mask" is a number with only the kth bit set: (1 << k).
//           e.g., k = 2  ->  1 << 2 = 100 (binary) = 4


using System;

public class Solution
{
	// Set kth bit: OR with the mask. x | 1 = 1 (bit becomes 1), x | 0 = x (other bits stay the same).
	// e.g., 10 | (1 << 2) = 1010 | 0100 = 1110 = 14
	// T.C: O(1)
	// S.C: O(1)
	public int SetKthBit(int n, int k)
	{
		return n | (1 << k);
	}

// ------------------------------------------------------------------------------

	// Check kth bit: AND with the mask. The result is non-zero only if the kth bit of n is 1.
	// e.g., 10 & (1 << 1) = 1010 & 0010 = 0010 (non-zero)  ->  bit 1 is set
	// T.C: O(1)
	// S.C: O(1)
	public bool CheckKthBit(int n, int k)
	{
		return (n & (1 << k)) != 0;
	}

// ------------------------------------------------------------------------------

	// Clear kth bit: AND with the inverted mask. ~(1 << k) has all bits 1 except the kth bit.
	// x & 0 = 0 (bit becomes 0), x & 1 = x (other bits stay the same).
	// e.g., 10 & ~(1 << 3) = 1010 & 0111 = 0010 = 2
	// T.C: O(1)
	// S.C: O(1)
	public int ClearKthBit(int n, int k)
	{
		return n & ~(1 << k);
	}

// ------------------------------------------------------------------------------

	// Toggle kth bit: XOR with the mask. x ^ 1 flips the bit, x ^ 0 = x (other bits stay the same).
	// e.g., 10 ^ (1 << 0) = 1010 ^ 0001 = 1011 = 11
	// T.C: O(1)
	// S.C: O(1)
	public int ToggleKthBit(int n, int k)
	{
		return n ^ (1 << k);
	}
}


class Program
{
	static void Main()
	{
		int n = 10;   // 1010

		Console.WriteLine(new Solution().SetKthBit(n, 2));      // o/p: 14
		Console.WriteLine(new Solution().CheckKthBit(n, 1));    // o/p: True
		Console.WriteLine(new Solution().ClearKthBit(n, 3));    // o/p: 2
		Console.WriteLine(new Solution().ToggleKthBit(n, 0));   // o/p: 11
	}
}

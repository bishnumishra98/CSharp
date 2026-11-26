// Leetcode: 678. Valid Parenthesis String   --->   s has '(', ')' and '*'. A '*' can be '(' or ')' or nothing. Return true
// if some choice for the stars makes s a valid bracket string (every '(' closed later by a ')', and no ')' without an
// earlier '(').

// Example 1:
// Input: s = "()"
// Output: true

// Example 2:
// Input: s = "(*)"
// Output: true

// Example 3:
// Input: s = "(*))"
// Output: true   ('*' as '(')

// Example 4:
// Input: s = ")*("
// Output: false

// Without stars ('12. Stack/04_ValidParentheses.cs' style), one counter is enough: open = the number of '(' not closed
// yet. It goes +1 on '(' and -1 on ')'; it must never go below 0 and must end at 0.


using System;

public class Solution
{
	// Brute force: try all 3 meanings for every '*' (recursion), keeping the 'open' counter.
	// T.C: O(3^k);   k = the number of stars
	// S.C: O(n);     recursion stack
	public bool CheckValidString_BruteForce(string s)
	{
		return Check(s, 0, 0);
	}

	private bool Check(string s, int i, int open)
	{
		if (open < 0) return false;          // a ')' with nothing to close
		if (i == s.Length) return open == 0;

		if (s[i] == '(') return Check(s, i + 1, open + 1);
		if (s[i] == ')') return Check(s, i + 1, open - 1);
		return Check(s, i + 1, open + 1) || Check(s, i + 1, open - 1) || Check(s, i + 1, open);   // '*'
	}

// ------------------------------------------------------------------------------

	// Optimal (range of counters): Instead of trying every choice, track the RANGE of possible 'open' values:
	//   minOpen = the smallest possible count (stars used as ')' when we can),
	//   maxOpen = the biggest possible count  (stars used as '(').
	//   '(':  both + 1.      ')':  both - 1.      '*':  minOpen - 1, maxOpen + 1.
	//   If maxOpen < 0: even using every star as '(' there are too many ')': false.
	//   If minOpen < 0: that choice is not allowed (count can not go below 0); the smallest VALID count is 0: minOpen = 0.
	//   (Every value between minOpen and maxOpen is possible, because each star can move the count by 1 in either way.)
	// At the end, the string is valid if 0 is in the range, i.e. minOpen == 0.
	// e.g., "(*))": ( -> [1,1]. * -> [0,2]. ) -> [-1,1] -> [0,1]. ) -> [-1,0] -> [0,0]. minOpen 0: true.
	// T.C: O(n)
	// S.C: O(1)
	public bool CheckValidString(string s)
	{
		int minOpen = 0, maxOpen = 0;
		foreach (char c in s)
		{
			if (c == '(')
			{
				minOpen++;
				maxOpen++;
			}
			else if (c == ')')
			{
				minOpen--;
				maxOpen--;
			}
			else
			{
				minOpen--;
				maxOpen++;
			}

			if (maxOpen < 0) return false;
			if (minOpen < 0) minOpen = 0;
		}
		return minOpen == 0;
	}
}

// Note: The C++ brute force looped over the characters inside each call and only recursed at a '*'. The version above
//       recurses on every character, which is shorter and does the same thing.


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().CheckValidString_BruteForce("(*))"));   // o/p: True
		Console.WriteLine(new Solution().CheckValidString("(*))"));              // o/p: True
	}
}

// Leetcode: 20. Valid Parentheses   --->   Given a string s containing just the characters '(', ')', '{', '}', '[' and
// ']', determine if the input string is valid.
// An input string is valid if:
// 1. Open brackets must be closed by the same type of brackets.
// 2. Open brackets must be closed in the correct order.
// 3. Every close bracket has a corresponding open bracket of the same type.

// Example 1:
// Input: s = "()"
// Output: true

// Example 2:
// Input: s = "()[]{}"
// Output: true

// Example 3:
// Input: s = "(]"
// Output: false

// Example 4:
// Input: s = "([])"
// Output: true


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: Keep removing pairs "()", "[]", "{}" that sit right next to each other, until nothing changes.
	// If the string becomes empty, it was valid.
	// e.g., "([])" -> remove "[]" -> "()" -> remove "()" -> ""  ->  valid
	// T.C: O(n^2);   up to n/2 rounds, each Replace() scans the string
	// S.C: O(n);   for the new strings
	public bool IsValid_Replace(string s)
	{
		string prev;
		do
		{
			prev = s;
			s = s.Replace("()", "").Replace("[]", "").Replace("{}", "");
		} while (s != prev);

		return s.Length == 0;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Stack.
	// The LAST bracket that was opened must be the FIRST one to close. That is exactly LIFO.
	// 1. Opening bracket: push it.
	// 2. Closing bracket: the top of the stack must be its matching opening bracket. If the stack is empty, or the top
	//    doesn't match, it's invalid. Else pop (this pair is done).
	// 3. At the end, the stack must be empty. (Leftover opening brackets were never closed, e.g., "((".)
	// e.g., "([])":  push (,  push [,  ']' matches '[' -> pop,  ')' matches '(' -> pop.  Empty -> valid
	//       "(]":    push (,  ']' doesn't match '(' -> invalid
	// T.C: O(n)
	// S.C: O(n);   for the stack
	public bool IsValid(string s)
	{
		Stack<char> stack = new Stack<char>();

		foreach (char c in s)
		{
			if (c == '(' || c == '[' || c == '{')
			{
				stack.Push(c);
			}
			else
			{
				if (stack.Count == 0) return false;   // closing bracket with nothing to close

				char open = stack.Pop();
				if ((c == ')' && open != '(') || (c == ']' && open != '[') || (c == '}' && open != '{')) return false;
			}
		}

		return stack.Count == 0;
	}
}


class Program
{
	static void Main()
	{
		string s = "([])";

		Console.WriteLine(new Solution().IsValid_Replace(s));   // o/p: True
		Console.WriteLine(new Solution().IsValid(s));           // o/p: True
	}
}

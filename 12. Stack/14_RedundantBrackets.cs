// GFG: Expression contains redundant bracket or not   --->   Given a string of balanced expression, find if it contains
// a redundant parenthesis or not. A set of parenthesis are redundant if the same sub-expression is surrounded by
// unnecessary or multiple brackets. Expression contains +, -, *, and / operators along with brackets. Given expression
// is valid and there are no white spaces present.

// Example 1:
// Input: exp = "((a+b))"
// Output: Yes
// Explanation: ((a+b)) can be reduced to (a+b).

// Example 2:
// Input: exp = "(a+b+(c+d))"
// Output: No
// Explanation: (a+b+(c+d)) doesn't have any redundant or multiple brackets.

// Example 3:
// Input: exp = "(a)"
// Output: Yes

// Problem link: https://www.geeksforgeeks.org/problems/expression-contains-redundant-bracket-or-not/1

// When is a pair of brackets useless ? When there is NO operator directly inside it.
//   (a+b)   -> has '+' inside: useful
//   (a)     -> no operator inside: redundant
//   ((a+b)) -> the inner pair has '+', but the outer pair has only (a+b) inside, no operator of its own: redundant


using System;
using System.Collections.Generic;

public class Solution
{
	// Stack algorithm:
	// 1. Push '(' and every operator (+ - * /). Letters don't matter, skip them.
	// 2. At ')': pop until the matching '('. If at least one operator was popped, this pair is useful. If NOTHING was
	//    popped before reaching '(', the pair has no operator inside: return true (redundant).
	//    (An inner pair that was already closed is completely popped, so its operators don't count for the outer pair.)
	// 3. If the whole string is fine, return false.
	// e.g., "((a+b))":  push (, (, +.  First ')': pop +, then ( -> useful.  Second ')': pops ( right away -> redundant.
	// T.C: O(n);   every character is pushed and popped at most once
	// S.C: O(n);   for the stack
	public bool CheckRedundancy(string s)
	{
		Stack<char> st = new Stack<char>();

		foreach (char c in s)
		{
			if (c == '(' || c == '+' || c == '-' || c == '*' || c == '/')
			{
				st.Push(c);
			}
			else if (c == ')')
			{
				bool hasOperator = false;
				while (st.Peek() != '(')   // only '(' and operators are in the stack, so anything else is an operator
				{
					st.Pop();
					hasOperator = true;
				}
				st.Pop();   // remove the '('

				if (!hasOperator) return true;
			}
		}

		return false;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().CheckRedundancy("((a+b))") ? "Yes" : "No");   // o/p: Yes
	}
}

// Stack   --->   A linear data structure that follows LIFO: Last In, First Out.
// Think of a stack of plates: you put a plate on the TOP, and you also take a plate from the TOP. The last plate you
// put is the first one you take.
//
//      | 30 |  <- top (pushed last, popped first)
//      | 20 |
//      | 10 |  <- bottom (pushed first, popped last)
//      +----+
//
// ● Main operations (all O(1)):
//   I.   Push(x): put x on the top.
//   II.  Pop():   remove the top item and return it.
//   III. Peek():  look at the top item without removing it. (C++ calls it top().)
//   IV.  Count:   number of items. Count == 0 means the stack is empty.
//
// ● C# built-in: Stack<T> in System.Collections.Generic.
//   - Pop() and Peek() on an EMPTY stack throw InvalidOperationException. So check Count > 0 first, or use
//     TryPop(out x) / TryPeek(out x), which return false instead of throwing.
//   - Contains(x): O(n), checks every item.
//   - foreach / ToArray() go from TOP to BOTTOM.
//   - Clear(): removes everything.
//
// ● Where stacks are used: Undo (Ctrl + Z), the browser Back button, matching brackets ('04_ValidParentheses.cs'),
//   function calls (the call stack in recursion), and the "next greater / smaller element" problems
//   ('07_NextAndPreviousSmallerOrGreaterElement.cs').


using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
	// Use of a stack: Reverse a string.
	// Push every character, then pop them all. LIFO gives them back in reverse order.
	// "Golu": push G, o, l, u  ->  pop u, l, o, G  ->  "uloG"
	// (Other ways to reverse a string are in '08. String\01_ReverseString.cs'.)
	// T.C: O(n)
	// S.C: O(n);   for the stack
	public string ReverseString(string str)
	{
		Stack<char> stack = new Stack<char>();
		foreach (char c in str) stack.Push(c);

		StringBuilder sb = new StringBuilder();
		while (stack.Count > 0) sb.Append(stack.Pop());
		return sb.ToString();
	}
}


class Program
{
	static void Main()
	{
		Stack<int> st = new Stack<int>();
		Console.WriteLine(st.Count == 0);                    // o/p: True   (empty)

		st.Push(10);
		st.Push(20);
		st.Push(30);
		Console.WriteLine(st.Count);                         // o/p: 3
		Console.WriteLine(st.Peek());                        // o/p: 30   (top, not removed)
		Console.WriteLine(string.Join(" ", st));             // o/p: 30 20 10   (top to bottom)

		Console.WriteLine(st.Pop());                         // o/p: 30   (removed)
		Console.WriteLine(st.Peek());                        // o/p: 20

		st.Pop();
		st.Pop();
		Console.WriteLine(st.TryPop(out int x));             // o/p: False   (empty, no exception)
		// st.Pop();   // Error: InvalidOperationException, as the stack is empty

		Console.WriteLine(new Solution().ReverseString("Golu"));   // o/p: uloG
	}
}

// Find the Middle Element of a Stack   --->   Given a stack, return its middle element. Count positions from the TOP.
// For an odd size, the middle is at position size / 2 + 1. For an even size there are 2 middles; return the one at
// position size / 2 from the top. The stack must be the same after the function returns.

// Example 1:
// Input: stack (bottom -> top) = 10 20 30 40 50
// Output: 30   (positions from top: 50 is 1st, 40 is 2nd, 30 is 3rd = 5 / 2 + 1)

// Example 2:
// Input: stack (bottom -> top) = 10 20 30 40
// Output: 30   (positions from top: 40 is 1st, 30 is 2nd = 4 / 2)

// A stack only lets us see the TOP. So to reach the middle, we must take items off, and put them back later.
// Both cases in one formula: the middle is at position (size + 1) / 2 from the top.
//   size 5: (5 + 1) / 2 = 3.   size 4: (4 + 1) / 2 = 2.


using System;
using System.Collections.Generic;

public class Solution
{
	// Approach 1: Copy the stack, and pop from the COPY until the middle is on top. The original is never touched.
	// new Stack<int>(original) would copy it upside down (it reads the original from top to bottom and pushes in that
	// order). Copying twice puts it back the right way up.
	// T.C: O(n);   copying
	// S.C: O(n);   for the copy
	public int FindMiddle_Copy(Stack<int> st)
	{
		if (st.Count == 0) return -1;

		Stack<int> copy = new Stack<int>(new Stack<int>(st));
		int pos = (st.Count + 1) / 2;
		for (int i = 1; i < pos; i++) copy.Pop();
		return copy.Peek();
	}

// ------------------------------------------------------------------------------

	// Approach 2: Recursion (pop, recurse, push back). No copy of the whole stack.
	// 1. Pop the top item and remember it in this call.
	// 2. If we have popped pos - 1 items, the middle is now on top: read it.
	// 3. When coming back from the recursion, push every popped item back (backtracking), so the stack is restored.
	// T.C: O(n / 2) = O(n)
	// S.C: O(n / 2) = O(n);   recursion stack
	public int FindMiddle(Stack<int> st)
	{
		if (st.Count == 0) return -1;
		return Solve(st, (st.Count + 1) / 2);
	}

	private int Solve(Stack<int> st, int pos)
	{
		if (pos == 1) return st.Peek();   // the middle is on top now

		int top = st.Pop();
		int middle = Solve(st, pos - 1);
		st.Push(top);   // put it back
		return middle;
	}
}


class Program
{
	static void Main()
	{
		Stack<int> st = new Stack<int>(new[] { 10, 20, 30, 40, 50 });   // pushes 10 first, so 50 is on top

		Console.WriteLine(new Solution().FindMiddle_Copy(st));   // o/p: 30
		Console.WriteLine(new Solution().FindMiddle(st));        // o/p: 30
		Console.WriteLine(string.Join(" ", st));                 // o/p: 50 40 30 20 10   (unchanged, top to bottom)
	}
}

// GFG: Sort a Stack   --->   Given a stack, sort it so that the LARGEST element is on the TOP.
// Use only stack operations (push, pop, peek, empty). No arrays or lists.

// Example 1:
// Input:  stack (top -> bottom) = 30 10 40 20 50
// Output: stack (top -> bottom) = 50 40 30 20 10

// Example 2:
// Input:  stack (top -> bottom) = 3 2 1
// Output: stack (top -> bottom) = 3 2 1

// Problem link: https://www.geeksforgeeks.org/problems/sort-a-stack/1

// Sub-problem: Insert an element into a SORTED stack (largest on top), so that it stays sorted.
// Example: stack (top -> bottom) = 50 40 30 20 10, insert 25  ->  50 40 30 25 20 10


using System;
using System.Collections.Generic;

public class Solution
{
	// Insert x into a stack that is already sorted (largest on top), using recursion.
	// 1. Base case: if the stack is empty, or x is bigger than the top, x belongs on top. Push it.
	// 2. Else the top is bigger than x, so x must go deeper. Pop the top, insert x into the rest, then push the top back.
	// e.g., insert 25 into 50 40 30 20 10:  pop 50, pop 40, pop 30.  Now top = 20 < 25: push 25.  Push 30, 40, 50 back.
	// T.C: O(n)
	// S.C: O(n);   recursion stack
	public void InsertSorted(Stack<int> st, int x)
	{
		if (st.Count == 0 || x > st.Peek())
		{
			st.Push(x);
			return;
		}

		int top = st.Pop();
		InsertSorted(st, x);
		st.Push(top);
	}

// ------------------------------------------------------------------------------

	// Sort with recursion (no extra stack):
	// 1. Base case: an empty stack is sorted.
	// 2. Pop the top, sort the rest recursively, then put the popped item back in its correct place with InsertSorted().
	// It is like insertion sort: the stack below is already sorted, and we insert one more item into it.
	// T.C: O(n^2);   n items, and each InsertSorted() can take O(n)
	// S.C: O(n);   recursion stack
	public void SortStack(Stack<int> st)
	{
		if (st.Count == 0) return;

		int top = st.Pop();
		SortStack(st);
		InsertSorted(st, top);
	}

// ------------------------------------------------------------------------------

	// Sort with a second stack (no recursion):
	// Keep 'sorted' with the SMALLEST on top while filling it. Take each item from the input:
	// while sorted's top is smaller than the item, move those smaller items back to the input. Then push the item.
	// At the end, 'sorted' has the smallest on top. Move everything back into the input, which flips it: largest on top.
	// T.C: O(n^2);   items can move back and forth
	// S.C: O(n);   for the second stack
	public void SortStack_TwoStacks(Stack<int> st)
	{
		Stack<int> sorted = new Stack<int>();   // smallest on top

		while (st.Count > 0)
		{
			int x = st.Pop();
			while (sorted.Count > 0 && sorted.Peek() < x) st.Push(sorted.Pop());
			sorted.Push(x);
		}

		while (sorted.Count > 0) st.Push(sorted.Pop());   // flip: largest on top
	}
}


class Program
{
	static Stack<int> Make(params int[] bottomToTop) => new Stack<int>(bottomToTop);

	static void Main()
	{
		Stack<int> st1 = Make(10, 20, 30, 40, 50);
		new Solution().InsertSorted(st1, 25);
		Console.WriteLine(string.Join(" ", st1));   // o/p: 50 40 30 25 20 10

		Stack<int> st2 = Make(50, 20, 40, 10, 30);   // top -> bottom: 30 10 40 20 50
		new Solution().SortStack(st2);
		Console.WriteLine(string.Join(" ", st2));   // o/p: 50 40 30 20 10

		Stack<int> st3 = Make(50, 20, 40, 10, 30);
		new Solution().SortStack_TwoStacks(st3);
		Console.WriteLine(string.Join(" ", st3));   // o/p: 50 40 30 20 10
	}
}

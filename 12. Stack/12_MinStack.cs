// Leetcode: 155. Min Stack   --->   Design a stack that supports push, pop, top, and retrieving the minimum element in
// constant time.
// Implement the MinStack class:
// • MinStack() initializes the stack object.
// • void push(int val) pushes the element val onto the stack.
// • void pop() removes the element on the top of the stack.
// • int top() gets the top element of the stack.
// • int getMin() retrieves the minimum element in the stack.
// You must implement a solution with O(1) time complexity for each function.

// Example:
// push(3), push(2), push(5), push(1)  ->  top() = 1, getMin() = 1
// pop()                               ->  top() = 5, getMin() = 2

// Constraints: -2^31 <= val <= 2^31 - 1. pop, top and getMin are always called on non-empty stacks.

// The difficulty: After a pop, the minimum can change back to an OLDER value. e.g., push 3, 2, 1 then pop -> min goes back
// from 1 to 2. So remembering just one "min" variable is not enough.


using System;
using System.Collections.Generic;

// Brute force: A normal stack, and getMin() scans all items.
// T.C: O(1) for push/pop/top, O(n) for getMin (not allowed by the problem)
// S.C: O(n)
public class MinStack_Scan
{
	private readonly Stack<int> st = new Stack<int>();

	public void Push(int val) => st.Push(val);
	public void Pop() => st.Pop();
	public int Top() => st.Peek();

	public int GetMin()
	{
		int min = int.MaxValue;
		foreach (int x in st) min = Math.Min(min, x);
		return min;
	}
}

// ------------------------------------------------------------------------------

// Better: Store pairs (value, minimum so far). Each item remembers what the minimum was when it was pushed (itself and
// everything below it). After a pop, the new top's pair already knows the old minimum.
//   push 3 -> (3, 3)   push 2 -> (2, 2)   push 5 -> (5, 2)   push 1 -> (1, 1)
//   pop    -> top pair (5, 2): top = 5, min = 2
// T.C: O(1) for every operation
// S.C: O(2n);   2 numbers per item
public class MinStack_Pairs
{
	private readonly Stack<(int val, int min)> st = new Stack<(int val, int min)>();

	public void Push(int val)
	{
		int min = st.Count == 0 ? val : Math.Min(val, st.Peek().min);
		st.Push((val, min));
	}

	public void Pop() => st.Pop();
	public int Top() => st.Peek().val;
	public int GetMin() => st.Peek().min;
}

// ------------------------------------------------------------------------------

// Optimal (space): One number per item + one 'min' variable. When a NEW minimum comes, store a special "encoded" value
// that tells us the OLD minimum later.
// • Push(val):
//   -> empty stack:   push val, min = val
//   -> val >= min:    push val (min doesn't change)
//   -> val < min:     push (2 * val - min), then min = val.
//                     The pushed value is always LESS than the new min (because val < oldMin, so 2 * val - oldMin < val).
//                     That is how we recognise an encoded value later.
// • Pop():
//   -> top >= min:    a normal value, just pop.
//   -> top < min:     an encoded value. The min is about to change back. Old min = 2 * min - top. Then pop.
// • Top():  top >= min ? top : min   (an encoded top means the real top value IS the current min)
// • GetMin(): min
// Why 'long' ? 2 * val - min can go beyond the int range (e.g., val = -2^31).
// e.g., push 3 (min 3), push 2 -> 2 < 3, store 2*2-3 = 1, min = 2.  pop -> top 1 < min 2, so old min = 2*2-1 = 3.
// T.C: O(1) for every operation
// S.C: O(n);   1 number per item
public class MinStack
{
	private readonly Stack<long> st = new Stack<long>();
	private long min;

	public void Push(int val)
	{
		if (st.Count == 0)
		{
			st.Push(val);
			min = val;
		}
		else if (val >= min)
		{
			st.Push(val);
		}
		else
		{
			st.Push(2L * val - min);   // encoded
			min = val;
		}
	}

	public void Pop()
	{
		long top = st.Pop();
		if (top < min) min = 2 * min - top;   // restore the old min
	}

	public int Top()
	{
		long top = st.Peek();
		return (int)(top >= min ? top : min);
	}

	public int GetMin() => (int)min;
}


class Program
{
	static void Main()
	{
		MinStack_Scan s1 = new MinStack_Scan();
		s1.Push(3); s1.Push(2); s1.Push(5); s1.Push(1);
		Console.Write(s1.Top() + " " + s1.GetMin() + " ");
		s1.Pop();
		Console.WriteLine(s1.Top() + " " + s1.GetMin());   // o/p: 1 1 5 2

		MinStack_Pairs s2 = new MinStack_Pairs();
		s2.Push(3); s2.Push(2); s2.Push(5); s2.Push(1);
		Console.Write(s2.Top() + " " + s2.GetMin() + " ");
		s2.Pop();
		Console.WriteLine(s2.Top() + " " + s2.GetMin());   // o/p: 1 1 5 2

		MinStack s3 = new MinStack();
		s3.Push(3); s3.Push(2); s3.Push(5); s3.Push(1);
		Console.Write(s3.Top() + " " + s3.GetMin() + " ");
		s3.Pop();
		Console.WriteLine(s3.Top() + " " + s3.GetMin());   // o/p: 1 1 5 2
	}
}

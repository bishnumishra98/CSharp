// GFG: Implement two stacks in an array   --->   Implement 2 stacks in ONE array. Both stacks should use the array space
// as well as possible: as long as the array has any free slot, either stack can push.

// Example:
// Array of size 5.  push1(10), push2(50), push1(20), push2(40), push1(30)  ->  array = [10, 20, 30, 40, 50]
// push1(35)  ->  "Stack overflow" (no free slot)
// pop2() -> 40, pop2() -> 50, pop2() -> -1 (stack 2 is empty)

// Problem link: https://www.geeksforgeeks.org/problems/implement-two-stacks-in-an-array/1

// Simple idea (and its problem): Give each stack half of the array. But then stack 1 can be full while stack 2's half is
// still empty, so space is wasted.
// Better idea: Stack 1 grows from the LEFT end, stack 2 grows from the RIGHT end. They grow towards each other, and the
// array is full only when they meet.
//
//     [10, 20, 30, _, _, 40, 50]
//              top1      top2
//      stack 1 -->       <-- stack 2


using System;

// T.C: O(1) for every operation
// S.C: O(n);   for the array
public class TwoStacks
{
	private readonly int[] arr;
	private int top1, top2;

	public TwoStacks(int size)
	{
		arr = new int[size];
		top1 = -1;     // stack 1 is empty: it will start at index 0
		top2 = size;   // stack 2 is empty: it will start at index size - 1
	}

	// Full when there is no slot between the 2 tops, i.e., they are next to each other.
	private bool IsFull() => top2 - top1 == 1;

	public void Push1(int x)
	{
		if (IsFull()) { Console.WriteLine("Stack overflow"); return; }
		arr[++top1] = x;   // stack 1 moves right
	}

	public void Push2(int x)
	{
		if (IsFull()) { Console.WriteLine("Stack overflow"); return; }
		arr[--top2] = x;   // stack 2 moves left
	}

	public int Pop1()
	{
		if (top1 == -1) return -1;   // stack 1 is empty
		return arr[top1--];
	}

	public int Pop2()
	{
		if (top2 == arr.Length) return -1;   // stack 2 is empty
		return arr[top2++];
	}

	public string Print() => string.Join(" ", arr);
}


class Program
{
	static void Main()
	{
		TwoStacks s = new TwoStacks(5);
		s.Push1(10);
		s.Push2(50);
		s.Push1(20);
		s.Push2(40);
		s.Push1(30);
		Console.WriteLine(s.Print());   // o/p: 10 20 30 40 50
		s.Push1(35);                    // o/p: Stack overflow

		Console.WriteLine(s.Pop2() + " " + s.Pop2() + " " + s.Pop2());   // o/p: 40 50 -1
		Console.WriteLine(s.Pop1());                                      // o/p: 30
	}
}

// Deque (Double-Ended Queue, said "deck")   --->   A queue where we can add and remove at BOTH ends, front and rear.
//
//   AddFirst / RemoveFirst  <-->  [10] [20] [30]  <-->  AddLast / RemoveLast
//
// It can work as a stack (use only one end) and as a queue (add at one end, remove at the other).
// Used in: Sliding Window Maximum ('11_SlidingWindowMaximum.cs'), palindrome checks, undo/redo with a size limit.

// ● C# has NO built-in Deque<T>. Two common choices:
//   I.  LinkedList<T> (a doubly linked list): AddFirst, AddLast, RemoveFirst, RemoveLast, First.Value, Last.Value.
//       All O(1). This is what we use in DSA problems.
//   II. Our own circular array (below), the same idea as the circular queue in '02_QueueImplementation.cs'.

// Leetcode 641. Design Circular Deque: Implement MyCircularDeque(k) with insertFront, insertLast, deleteFront,
// deleteLast, getFront, getRear, isEmpty, isFull. (Each insert/delete returns true if it worked.)

// Example:
// Capacity 5. InsertLast 10, 20, 30. InsertFront 40, 50  ->  50 40 10 20 30. InsertFront 60 -> false (full).
// DeleteFront -> 40 10 20 30.  DeleteLast -> 40 10 20.


using System;
using System.Collections.Generic;

// Circular array deque.
// Keep 'front' and 'count', like the circular queue:
//   rear index          = (front + count - 1) % capacity
//   InsertLast:  put x at (front + count) % capacity, count++
//   InsertFront: front moves one step BACK: front = (front - 1 + capacity) % capacity, put x there, count++
//                (+ capacity keeps the index from going negative: (0 - 1 + 5) % 5 = 4, the last slot)
//   DeleteFront: front = (front + 1) % capacity, count--
//   DeleteLast:  count--   (the rear index moves back by itself)
// T.C: O(1) for every operation
// S.C: O(capacity)
public class MyCircularDeque
{
	private readonly int[] arr;
	private int front = 0, count = 0;

	public MyCircularDeque(int k) { arr = new int[k]; }

	public bool InsertFront(int x)
	{
		if (IsFull()) return false;
		front = (front - 1 + arr.Length) % arr.Length;
		arr[front] = x;
		count++;
		return true;
	}

	public bool InsertLast(int x)
	{
		if (IsFull()) return false;
		arr[(front + count) % arr.Length] = x;
		count++;
		return true;
	}

	public bool DeleteFront()
	{
		if (IsEmpty()) return false;
		front = (front + 1) % arr.Length;
		count--;
		return true;
	}

	public bool DeleteLast()
	{
		if (IsEmpty()) return false;
		count--;
		return true;
	}

	public int GetFront() => IsEmpty() ? -1 : arr[front];
	public int GetRear() => IsEmpty() ? -1 : arr[(front + count - 1) % arr.Length];
	public bool IsEmpty() => count == 0;
	public bool IsFull() => count == arr.Length;

	public string Print()   // front to rear
	{
		List<int> items = new List<int>();
		for (int i = 0; i < count; i++) items.Add(arr[(front + i) % arr.Length]);
		return string.Join(" ", items);
	}
}


class Program
{
	static void Main()
	{
		// Built-in way: LinkedList<int> as a deque
		LinkedList<int> dq = new LinkedList<int>();
		dq.AddLast(10);
		dq.AddLast(20);
		dq.AddLast(30);
		dq.AddFirst(40);
		dq.AddFirst(50);
		Console.WriteLine(string.Join(" ", dq));                        // o/p: 50 40 10 20 30
		dq.RemoveFirst();
		dq.RemoveLast();
		Console.WriteLine(dq.First.Value + " " + dq.Last.Value);       // o/p: 40 20

		// Our own circular deque
		MyCircularDeque cd = new MyCircularDeque(5);
		cd.InsertLast(10);
		cd.InsertLast(20);
		cd.InsertLast(30);
		cd.InsertFront(40);
		cd.InsertFront(50);
		Console.WriteLine(cd.Print());                                 // o/p: 50 40 10 20 30
		Console.WriteLine(cd.InsertFront(60));                         // o/p: False   (full)
		cd.DeleteFront();
		cd.DeleteLast();
		Console.WriteLine(cd.Print());                                 // o/p: 40 10 20
		Console.WriteLine(cd.GetFront() + " " + cd.GetRear());         // o/p: 40 20
	}
}

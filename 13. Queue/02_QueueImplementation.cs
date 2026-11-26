// Queue Implementation   --->   Build our own queue (without Queue<T>), with Enqueue, Dequeue, Front, Rear, IsEmpty and
// Size.
// I.   Simple array queue: easy, but has a big problem (shown below).
// II.  Circular queue (Leetcode 622. Design Circular Queue): fixes that problem.
// III. Linked list queue: no size limit.

// Example (array of size 5):
// Enqueue 10, 20, 30, 40, 50 -> full. Dequeue twice -> 30 40 50. Enqueue 60, 70.
// Simple queue:   "Queue overflow" (it can't reuse the 2 freed slots at the front)
// Circular queue: 30 40 50 60 70   (60 and 70 go into the freed slots)


using System;
using System.Collections.Generic;

// I. Simple array queue.
// 'front' = index of the first item, 'rear' = index of the last item. Enqueue: rear++. Dequeue: front++.
// Problem: Both indexes only move FORWARD. Once rear reaches the end of the array, we can't enqueue any more, even if
//          the front slots were freed by dequeues. The array is "full" while having empty slots.
//          e.g., size 5: enqueue 5 items, dequeue 2  ->  [_, _, 30, 40, 50]. rear is at the end, so no more enqueues.
// T.C: O(1) for every operation
// S.C: O(capacity)
public class SimpleQueue
{
	private readonly int[] arr;
	private int front = 0, rear = -1;

	public SimpleQueue(int capacity) { arr = new int[capacity]; }

	public bool Enqueue(int x)
	{
		if (rear == arr.Length - 1) return false;   // overflow (even if front slots are free)
		arr[++rear] = x;
		return true;
	}

	public int Dequeue()
	{
		if (IsEmpty()) return -1;   // underflow
		return arr[front++];
	}

	public bool IsEmpty() => front > rear;
	public int Size() => rear - front + 1;
}

// ------------------------------------------------------------------------------

// II. Circular queue (also called a ring buffer).
// Solution: When an index reaches the end of the array, WRAP it around to index 0, so freed slots are reused:
//           next index = (index + 1) % capacity
//   e.g., capacity 5: indexes go 0, 1, 2, 3, 4, 0, 1, ...
// Keep 'front' (first item), and 'count' (number of items). Then:
//   rear (last item) = (front + count - 1) % capacity
//   Enqueue: put x at (front + count) % capacity, count++.  Full when count == capacity.
//   Dequeue: take arr[front], front = (front + 1) % capacity, count--.  Empty when count == 0.
// (Keeping 'count' avoids the classic confusion "front == rear: is it full or empty ?")
// T.C: O(1) for every operation
// S.C: O(capacity)
public class CircularQueue
{
	private readonly int[] arr;
	private int front = 0, count = 0;

	public CircularQueue(int capacity) { arr = new int[capacity]; }

	public bool Enqueue(int x)
	{
		if (IsFull()) return false;
		arr[(front + count) % arr.Length] = x;
		count++;
		return true;
	}

	public bool Dequeue()
	{
		if (IsEmpty()) return false;
		front = (front + 1) % arr.Length;
		count--;
		return true;
	}

	public int Front() => IsEmpty() ? -1 : arr[front];
	public int Rear() => IsEmpty() ? -1 : arr[(front + count - 1) % arr.Length];
	public bool IsEmpty() => count == 0;
	public bool IsFull() => count == arr.Length;
	public int Size() => count;

	public string Print()   // front to rear
	{
		List<int> items = new List<int>();
		for (int i = 0; i < count; i++) items.Add(arr[(front + i) % arr.Length]);
		return string.Join(" ", items);
	}
}

// ------------------------------------------------------------------------------

// III. Linked list queue.
// Keep references to both ends: 'head' (front, where we remove) and 'tail' (rear, where we add). Both are O(1).
// No capacity, so no overflow.
// T.C: O(1) for every operation
// S.C: O(n);   one node per item
public class LinkedListQueue
{
	private class Node
	{
		public int val;
		public Node next;
		public Node(int val) { this.val = val; }
	}

	private Node head, tail;
	private int count;

	public void Enqueue(int x)
	{
		Node node = new Node(x);
		if (tail == null) head = tail = node;   // first item
		else
		{
			tail.next = node;
			tail = node;
		}
		count++;
	}

	public int Dequeue()
	{
		if (head == null) return -1;
		int val = head.val;
		head = head.next;
		if (head == null) tail = null;   // the queue became empty
		count--;
		return val;
	}

	public int Front() => head == null ? -1 : head.val;
	public int Rear() => tail == null ? -1 : tail.val;
	public int Size() => count;
}


class Program
{
	static void Main()
	{
		SimpleQueue sq = new SimpleQueue(5);
		for (int x = 10; x <= 50; x += 10) sq.Enqueue(x);
		sq.Dequeue();
		sq.Dequeue();
		Console.WriteLine(sq.Enqueue(60) ? "Added" : "Queue overflow");   // o/p: Queue overflow   (2 slots are free, but can't be used)

		CircularQueue cq = new CircularQueue(5);
		for (int x = 10; x <= 50; x += 10) cq.Enqueue(x);
		cq.Dequeue();
		cq.Dequeue();
		cq.Enqueue(60);
		cq.Enqueue(70);
		Console.WriteLine(cq.Print());                                 // o/p: 30 40 50 60 70
		Console.WriteLine(cq.Front() + " " + cq.Rear() + " " + cq.IsFull());   // o/p: 30 70 True

		LinkedListQueue lq = new LinkedListQueue();
		lq.Enqueue(10);
		lq.Enqueue(20);
		lq.Enqueue(30);
		Console.WriteLine(lq.Dequeue() + " " + lq.Front() + " " + lq.Rear() + " " + lq.Size());   // o/p: 10 20 30 2
	}
}

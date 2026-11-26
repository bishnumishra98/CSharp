// PriorityQueue in C#   --->   The built-in heap (.NET 6 and later). In problems we use this, not our own heap class.
// A normal Queue gives items back in the order they came in. A PriorityQueue gives back the item with the SMALLEST
// priority first, whatever the order they came in.
//
// ● PriorityQueue<TElement, TPriority>: every item has 2 parts: the element (what you store) and its priority (what it
//   is sorted by). They can be different, e.g., store a word with its count as the priority.
//
//   Operation                          |  What it does                                       |  Time
//   -----------------------------------------------------------------------------------------------------
//   pq.Enqueue(element, priority)      |  add                                                |  O(log n)
//   pq.Dequeue()                       |  remove and return the element with the SMALLEST    |  O(log n)
//                                      |  priority                                           |
//   pq.Peek()                          |  look at it, without removing                       |  O(1)
//   pq.Count                           |  how many items                                     |  O(1)
//   pq.TryDequeue(out e, out p)        |  like Dequeue, but returns false if empty           |  O(log n)
//   pq.TryPeek(out e, out p)           |  like Peek, but returns false if empty, and also    |  O(1)
//                                      |  gives the priority                                 |
//   pq.EnqueueDequeue(e, p)            |  add, then remove the smallest (faster than doing   |  O(log n)
//                                      |  both)                                              |
//   new PriorityQueue<E, P>(items)     |  build from a list of (element, priority) pairs     |  O(n)
//   pq.UnorderedItems                  |  all items, in NO particular order                  |  O(n)
//
//   Peek / Dequeue on an empty queue throw an InvalidOperationException.
//
// ● It is a MIN heap by default. For a MAX heap, give it a comparer that compares the other way round:
//     new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)))
//   A common shortcut is to use -x as the priority. Careful: -int.MinValue does not fit in an int (it overflows), so
//   the comparer is the safe way.
//
// ● Tuple priorities are compared item by item: (2, "b") < (3, "a"), and (2, "a") < (2, "b"). Handy for "sort by
//   count, then by name".
//
// ● Things to know:
//   I.   Equal priorities come out in NO guaranteed order (it is not first-in-first-out for ties).
//   II.  You can not loop over it in sorted order. foreach over UnorderedItems is unordered. To get sorted order, keep
//        dequeuing.
//   III. It does not check for duplicates: the same element can be added many times.
//
// ● C++ vs C#:
//   C++: priority_queue<int> pq;                                 (MAX heap by default!)   pq.push(x), pq.top(), pq.pop()
//   C++: priority_queue<int, vector<int>, greater<int>> pq;      (min heap)
//   C#:  PriorityQueue<int, int> pq;                              (MIN heap by default)   pq.Enqueue(x, x), pq.Peek(),
//                                                                                         pq.Dequeue()
//   C++ pop() returns nothing. C# Dequeue() removes AND returns the element.


using System;
using System.Collections.Generic;

class Program
{
	static void Main()
	{
		// I. Min heap: the element is also its own priority.
		PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
		foreach (int x in new int[] { 10, 50, 30, 20, 40 }) minHeap.Enqueue(x, x);

		Console.WriteLine(minHeap.Peek());      // o/p: 10
		Console.WriteLine(minHeap.Dequeue());   // o/p: 10
		Console.WriteLine(minHeap.Peek());      // o/p: 20
		Console.WriteLine(minHeap.Count);       // o/p: 4

		// II. Max heap: reverse comparer.
		PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
		foreach (int x in new int[] { 10, 50, 30, 20, 40 }) maxHeap.Enqueue(x, x);

		Console.WriteLine(maxHeap.Dequeue());   // o/p: 50
		Console.WriteLine(maxHeap.Peek());      // o/p: 40

		// III. Element and priority are different things: tasks sorted by their deadline.
		PriorityQueue<string, int> tasks = new PriorityQueue<string, int>();
		tasks.Enqueue("homework", 3);
		tasks.Enqueue("dinner", 1);
		tasks.Enqueue("game", 5);

		List<string> order = new List<string>();
		while (tasks.Count > 0) order.Add(tasks.Dequeue());
		Console.WriteLine(string.Join(" ", order));   // o/p: dinner homework game

		// IV. Build in O(n) from (element, priority) pairs.
		PriorityQueue<char, int> letters = new PriorityQueue<char, int>(new (char, int)[] { ('c', 3), ('a', 1), ('b', 2) });
		Console.WriteLine(letters.Peek());            // o/p: a

		// V. TryDequeue: no exception when empty.
		PriorityQueue<int, int> empty = new PriorityQueue<int, int>();
		Console.WriteLine(empty.TryDequeue(out int item, out int priority));   // o/p: False
	}
}

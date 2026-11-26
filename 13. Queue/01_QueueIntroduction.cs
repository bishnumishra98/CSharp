// Queue   --->   A linear data structure that follows FIFO: First In, First Out.
// Think of a line at a ticket counter: people join at the BACK (rear), and the person at the FRONT is served first.
//
//   Dequeue (remove)                         Enqueue (add)
//        <--  [10] [20] [30] [40]  <--
//             front           rear
//
// ● Main operations (all O(1)):
//   I.   Enqueue(x): add x at the rear. (C++: push)
//   II.  Dequeue():  remove the front item and return it. (C++: pop)
//   III. Peek():     look at the front item without removing it. (C++: front)
//   IV.  Count:      number of items. Count == 0 means the queue is empty.
//
// ● C# built-in: Queue<T> in System.Collections.Generic.
//   - Dequeue() and Peek() on an EMPTY queue throw InvalidOperationException. Use TryDequeue(out x) / TryPeek(out x)
//     to get false instead.
//   - There is no Back()/Rear() method (C++ has q.back()). If you need the last item, keep it in a variable when you
//     enqueue it, or use queue.Last() from LINQ (O(n)).
//   - foreach / ToArray() go from FRONT to REAR.
//
// ● Stack vs Queue:
//   Stack: LIFO, add and remove at the SAME end (top).        e.g., Undo, matching brackets, recursion
//   Queue: FIFO, add at one end, remove at the OTHER end.     e.g., printer jobs, ticket lines, BFS in graphs/trees
//
// ● Types of queue:
//   I.   Simple queue (this file).
//   II.  Circular queue: the array wraps around, so freed slots at the front can be reused ('02_QueueImplementation.cs').
//   III. Deque (double-ended queue): add/remove at BOTH ends ('03_Deque.cs').
//   IV.  Priority queue: the item with the smallest (or largest) priority comes out first, not the oldest one.
//        C#: PriorityQueue<TElement, TPriority>. (More in the Heaps folder.)


using System;
using System.Collections.Generic;

class Program
{
	static void Main()
	{
		Queue<int> q = new Queue<int>();
		Console.WriteLine(q.Count == 0);                  // o/p: True   (empty)

		q.Enqueue(10);
		q.Enqueue(20);
		q.Enqueue(30);
		q.Enqueue(40);
		Console.WriteLine(q.Count);                       // o/p: 4
		Console.WriteLine(string.Join(" ", q));           // o/p: 10 20 30 40   (front to rear)

		Console.WriteLine(q.Dequeue());                   // o/p: 10   (first in, first out)
		Console.WriteLine(q.Peek());                      // o/p: 20   (new front, not removed)

		q.Clear();
		Console.WriteLine(q.TryDequeue(out int x));       // o/p: False   (empty, no exception)
		// q.Dequeue();   // Error: InvalidOperationException, as the queue is empty
	}
}

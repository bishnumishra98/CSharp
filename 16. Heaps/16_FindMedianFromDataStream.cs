// Leetcode: 295. Find Median from Data Stream   --->   The median is the middle value of a SORTED list. If the list has
// an even size, it is the average of the 2 middle values. e.g., [2,3,4] -> 3, and [2,3] -> (2 + 3) / 2 = 2.5.
// Implement the MedianFinder class:
// ● MedianFinder(): creates the object.
// ● void AddNum(int num): adds num from the data stream.
// ● double FindMedian(): returns the median of all numbers added so far.

// Example:
// Input:  ["MedianFinder", "addNum", "addNum", "findMedian", "addNum", "findMedian"]
//         [[], [1], [2], [], [3], []]
// Output: [null, null, null, 1.5, null, 2.0]

// Brute force (idea only): Keep a sorted List. AddNum inserts at the right place (binary search to find it, but the
// List must shift everything after it: O(n)). FindMedian reads the middle: O(1).
// T.C: AddNum O(n), FindMedian O(1).


using System;
using System.Collections.Generic;

// Optimal: Two heaps. Split the numbers into 2 halves:
// ● 'low':  the SMALLER half, in a MAX heap  (its top = the biggest of the small ones)
// ● 'high': the BIGGER half,  in a MIN heap  (its top = the smallest of the big ones)
// Keep 2 rules:
//   1. Every number in low <= every number in high.
//   2. The sizes differ by at most 1.
// Then the middle of the sorted list is always at the TOPS: the median is the top of the bigger heap, or the average
// of both tops if they have the same size.
//
//    low (max heap)  |  high (min heap)
//      1  2 [3]      |  [4]  5  6          -> median = (3 + 4) / 2 = 3.5
//
// AddNum (the C++ way):
//   a. Always add to low first.
//   b. If low's top is now bigger than high's top (rule 1 broken), move low's top to high.
//   c. If one heap has 2 more than the other (rule 2 broken), move a top across.
// e.g., add 1: low [1]. Add 2: low [2, 1] -> sizes 2 vs 0, move 2 to high: low [1], high [2]. Median (1 + 2) / 2 = 1.5.
//       Add 3: low [3, 1] -> 3 > high's top 2, move 3: low [1], high [2, 3]. Median = high's top = 2.
// T.C: AddNum O(log n);   a few heap operations.   FindMedian O(1).
// S.C: O(n);   all numbers are stored
public class MedianFinder
{
	private PriorityQueue<int, int> low = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
	private PriorityQueue<int, int> high = new PriorityQueue<int, int>();

	public void AddNum(int num)
	{
		low.Enqueue(num, num);

		if (high.Count > 0 && low.Peek() > high.Peek()) Move(low, high);   // rule 1

		if (low.Count > high.Count + 1) Move(low, high);                  // rule 2
		else if (high.Count > low.Count + 1) Move(high, low);
	}

	public double FindMedian()
	{
		if (low.Count > high.Count) return low.Peek();
		if (high.Count > low.Count) return high.Peek();
		return ((long)low.Peek() + high.Peek()) / 2.0;   // long: the sum of 2 big ints could overflow an int
	}

	private void Move(PriorityQueue<int, int> from, PriorityQueue<int, int> to)
	{
		int x = from.Dequeue();
		to.Enqueue(x, x);
	}
}


class Program
{
	static void Main()
	{
		MedianFinder mf = new MedianFinder();
		mf.AddNum(1);
		mf.AddNum(2);
		Console.WriteLine(mf.FindMedian());   // o/p: 1.5
		mf.AddNum(3);
		Console.WriteLine(mf.FindMedian());   // o/p: 2
	}
}

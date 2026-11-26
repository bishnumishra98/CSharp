// Leetcode: 621. Task Scheduler   --->   You are given an array of CPU tasks, each labeled with a letter from A to Z, and a
// cooling time n. Each CPU interval can do one task or be idle. Tasks can be done in any order, but two tasks with the
// SAME label must be at least n intervals apart. Return the minimum number of intervals needed to finish all tasks.

// Example 1:
// Input: tasks = ["A","A","A","B","B","B"], n = 2
// Output: 8
// Explanation: A -> B -> idle -> A -> B -> idle -> A -> B. After A, two other intervals must pass before the next A.

// Example 2:
// Input: tasks = ["A","C","A","B","D","B"], n = 1
// Output: 6   (A -> B -> C -> D -> A -> B, no idle needed)

// Example 3:
// Input: tasks = ["A","A","A","B","B","B"], n = 3
// Output: 10   (A -> B -> idle -> idle -> A -> B -> idle -> idle -> A -> B)

// Note: The C++ file for this problem was empty (only the title). This solution is new.


using System;
using System.Collections.Generic;

public class Solution
{
	// Better (Simulation with a max heap + a queue):
	// Greedy rule: at every interval, run the available task with the MOST copies left (it is the hardest to fit, so
	// start it early, like in '13_ReorganizeString.cs').
	// ● Max heap: the counts of tasks that are ready to run now.
	// ● Queue: tasks that are cooling down, as (count left, time when it is ready again). The queue is in time order,
	//   because every task enters it with "now + n", and "now" only grows.
	// Each interval: time++. If the heap has a task, run it (count - 1); if it still has copies, it cools until
	// time + n. Then move every task whose cooling ended from the queue back to the heap. (An empty heap = idle.)
	// T.C: O(T log 26) = O(T);   T = number of intervals in the answer (at most tasks.Length * (n + 1))
	// S.C: O(1);   at most 26 different tasks
	public int LeastInterval_Simulation(char[] tasks, int n)
	{
		int[] freq = new int[26];
		foreach (char t in tasks) freq[t - 'A']++;

		PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
		foreach (int f in freq)
		{
			if (f > 0) maxHeap.Enqueue(f, f);
		}

		Queue<(int count, int readyAt)> cooling = new Queue<(int count, int readyAt)>();
		int time = 0;

		while (maxHeap.Count > 0 || cooling.Count > 0)
		{
			time++;

			if (maxHeap.Count > 0)
			{
				int left = maxHeap.Dequeue() - 1;
				if (left > 0) cooling.Enqueue((left, time + n));
			}

			if (cooling.Count > 0 && cooling.Peek().readyAt == time)
			{
				int count = cooling.Dequeue().count;
				maxHeap.Enqueue(count, count);
			}
		}

		return time;
	}

// ------------------------------------------------------------------------------

	// Optimal (Math): Only the MOST frequent task really decides the answer.
	// Say A is the most frequent, with maxFreq copies. Its copies must be n apart, so they make (maxFreq - 1) "frames"
	// of (n + 1) slots each, plus the last copy:
	//     A _ _ | A _ _ | A          (maxFreq = 3, n = 2)
	// Other tasks go into the empty slots. Every task that ALSO has maxFreq copies needs a spot at the very end too:
	//     A B _ | A B _ | A B        -> (maxFreq - 1) * (n + 1) + countOfMax = 2 * 3 + 2 = 8
	// If there are so many different tasks that the slots overflow, no idle is needed at all: then the answer is just
	// tasks.Length (every interval runs a task). So the answer is the bigger of the two.
	// e.g., Example 2: maxFreq = 2 (A and B), n = 1: (2 - 1) * 2 + 2 = 4, but there are 6 tasks. Answer 6.
	// T.C: O(m);   m = tasks.Length, for counting
	// S.C: O(1)
	public int LeastInterval(char[] tasks, int n)
	{
		int[] freq = new int[26];
		foreach (char t in tasks) freq[t - 'A']++;

		int maxFreq = 0, countOfMax = 0;
		foreach (int f in freq)
		{
			if (f > maxFreq)
			{
				maxFreq = f;
				countOfMax = 1;
			}
			else if (f == maxFreq) countOfMax++;
		}

		return Math.Max(tasks.Length, (maxFreq - 1) * (n + 1) + countOfMax);
	}
}


class Program
{
	static void Main()
	{
		char[] tasks = { 'A', 'A', 'A', 'B', 'B', 'B' };

		Console.WriteLine(new Solution().LeastInterval_Simulation(tasks, 2));   // o/p: 8
		Console.WriteLine(new Solution().LeastInterval(tasks, 2));              // o/p: 8
	}
}

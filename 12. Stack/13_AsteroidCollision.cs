// Leetcode: 735. Asteroid Collision   --->   We are given an array asteroids of integers representing asteroids in a row.
// The indices of the asteroid in the array represent their relative position in space.
// For each asteroid, the absolute value represents its size, and the sign represents its direction (positive meaning
// right, negative meaning left). Each asteroid moves at the same speed.
// Find out the state of the asteroids after all collisions. If two asteroids meet, the smaller one will explode. If both
// are the same size, both will explode. Two asteroids moving in the same direction will never meet.

// Example 1:
// Input: asteroids = [5,10,-5]
// Output: [5,10]
// Explanation: The 10 and -5 collide resulting in 10. The 5 and 10 never collide.

// Example 2:
// Input: asteroids = [8,-8]
// Output: []

// Example 3:
// Input: asteroids = [10,2,-5]
// Output: [10]
// Explanation: The 2 and -5 collide resulting in -5. The 10 and -5 collide resulting in 10.

// Example 4:
// Input: asteroids = [3,5,-6,2,-1,4]
// Output: [-6,2,4]
// Explanation: -6 destroys 3 and 5, and keeps going left. 2 destroys -1. 4 never meets anything.

// When do 2 asteroids meet ? Only when the LEFT one moves right (+) and the RIGHT one moves left (-): "-> <-".
// "<- ->" move apart, "-> ->" and "<- <-" move together at the same speed. So they never meet.


using System;
using System.Collections.Generic;

public class Solution
{
	// Brute force: Simulate. Find the first pair "+ then -" next to each other, crash them, and repeat until there is no
	// such pair.
	// T.C: O(n^2)
	// S.C: O(n)
	public int[] AsteroidCollision_Simulate(int[] asteroids)
	{
		List<int> list = new List<int>(asteroids);

		bool crashed = true;
		while (crashed)
		{
			crashed = false;
			for (int i = 0; i + 1 < list.Count; i++)
			{
				int a = list[i], b = list[i + 1];
				if (a > 0 && b < 0)
				{
					if (a > -b) list.RemoveAt(i + 1);        // b explodes
					else if (a < -b) list.RemoveAt(i);       // a explodes
					else list.RemoveRange(i, 2);             // both explode
					crashed = true;
					break;
				}
			}
		}
		return list.ToArray();
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Stack of survivors (left to right).
	// • A right-moving asteroid (+): push it. It may crash with something that comes later.
	// • A left-moving asteroid (-x): it can only crash with right-moving asteroids on top of the stack.
	//   1. While the top is + and smaller than x: the top explodes (pop), and -x keeps going.
	//   2. Then: if the top is + and the same size: both explode (pop, and -x is gone).
	//            else if the stack is empty or the top is - : -x survives (push it).
	//            else (the top is + and bigger): -x explodes (do nothing).
	// A List<int> is used as the stack (Add = push, RemoveAt(Count - 1) = pop), so the answer is already in left to right
	// order. (A Stack<int> would give it back reversed.)
	// T.C: O(n);   every asteroid is pushed and popped at most once
	// S.C: O(n)
	public int[] AsteroidCollision(int[] asteroids)
	{
		List<int> st = new List<int>();

		foreach (int a in asteroids)
		{
			if (a > 0)
			{
				st.Add(a);
				continue;
			}

			while (st.Count > 0 && st[st.Count - 1] > 0 && st[st.Count - 1] < -a) st.RemoveAt(st.Count - 1);

			if (st.Count > 0 && st[st.Count - 1] == -a) st.RemoveAt(st.Count - 1);   // same size: both explode
			else if (st.Count == 0 || st[st.Count - 1] < 0) st.Add(a);              // nothing to crash into
		}

		return st.ToArray();
	}
}


class Program
{
	static void Main()
	{
		int[] asteroids = { 3, 5, -6, 2, -1, 4 };

		Console.WriteLine(string.Join(" ", new Solution().AsteroidCollision_Simulate(asteroids)));   // o/p: -6 2 4
		Console.WriteLine(string.Join(" ", new Solution().AsteroidCollision(asteroids)));            // o/p: -6 2 4
	}
}

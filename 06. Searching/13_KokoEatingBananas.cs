// Leetcode: 875. Koko Eating Bananas   --->   Koko loves to eat bananas. There are n piles of bananas, the ith pile has
// piles[i] bananas. The guards have gone and will come back in h hours.
// Koko can decide her bananas-per-hour eating speed of k. Each hour, she chooses some pile of bananas and eats k bananas
// from that pile. If the pile has less than k bananas, she eats all of them instead and will not eat any more bananas
// during this hour.
// Koko likes to eat slowly but still wants to finish eating all the bananas before the guards return.
// Return the minimum integer k such that she can eat all the bananas within h hours.

// Example 1:
// Input: piles = [3,6,7,11], h = 8
// Output: 4

// Example 2:
// Input: piles = [30,11,23,4,20], h = 5
// Output: 30

// Example 3:
// Input: piles = [30,11,23,4,20], h = 6
// Output: 23

// Understand the problem: Faster speed = less time. We need the SLOWEST speed for which total time <= h.
// Time to finish one pile = ceil(pile / speed), because even if a few bananas are left, a full hour is used.
// e.g., piles = [3, 6, 7, 11], h = 8
//       speed = 3: time = 1 + 2 + 3 + 4 = 10 hours > 8. Too slow.
//       speed = 4: time = 1 + 2 + 2 + 3 = 8 hours <= 8. So the answer is 4.
// Search space of speed: from 1 to max(piles). A speed more than max(piles) is useless, because she can't eat more than
// one pile in an hour anyway (time stays = piles.Length).


using System;

public class Solution
{
	// Total hours needed to eat all piles at the given speed.
	// (pile + speed - 1) / speed is the integer way to write ceil(pile / speed).
	// 'long', because with speed = 1 the total can be up to 10^4 piles * 10^9 bananas, which doesn't fit in int.
	private long HoursNeeded(int[] piles, int speed)
	{
		long hours = 0;
		for (int i = 0; i < piles.Length; i++) hours += (piles[i] + (long)speed - 1) / speed;
		return hours;
	}

	// Brute force: Linear search. Try every speed from 1 to max(piles). The first speed that works is the answer.
	// T.C: O(max * n);   where max = largest pile, n = piles.Length
	// S.C: O(1)
	public int MinEatingSpeed_BruteForce(int[] piles, int h)
	{
		int max = 0;
		for (int i = 0; i < piles.Length; i++) max = Math.Max(max, piles[i]);

		for (int speed = 1; speed <= max; speed++)
		{
			if (HoursNeeded(piles, speed) <= h) return speed;
		}

		return max;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search on answer (speed).
	// If speed 'x' works, then every speed bigger than x also works. This "works / doesn't work" pattern is sorted:
	// speed:  1  2  3  4  5  6 ...
	// works?  N  N  N  Y  Y  Y ...    ->  We need the first Y. Binary search can find it.
	// 1. start = 1, end = max(piles). Find mid (a speed):
	//    -> If HoursNeeded(mid) <= h: mid works. Store ans = mid, and try a slower speed: end = mid - 1.
	//    -> Else: mid is too slow. Try a faster speed: start = mid + 1.
	// T.C: O(log(max) * n);   where max = largest pile, n = piles.Length
	// S.C: O(1)
	public int MinEatingSpeed(int[] piles, int h)
	{
		int start = 1, end = 0;
		for (int i = 0; i < piles.Length; i++) end = Math.Max(end, piles[i]);

		int ans = end;
		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (HoursNeeded(piles, mid) <= h)
			{
				ans = mid;
				end = mid - 1;
			}
			else start = mid + 1;
		}

		return ans;
	}
}


class Program
{
	static void Main()
	{
		int[] piles = { 3, 6, 7, 11 };
		int h = 8;

		Console.WriteLine(new Solution().MinEatingSpeed_BruteForce(piles, h));   // o/p: 4
		Console.WriteLine(new Solution().MinEatingSpeed(piles, h));              // o/p: 4
	}
}

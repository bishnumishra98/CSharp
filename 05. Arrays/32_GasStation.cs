// Leetcode: 134. Gas Station   --->   There are n gas stations along a circular route, where the amount of gas at the
// ith station is gas[i].
// You have a car with an unlimited gas tank and it costs cost[i] of gas to travel from the ith station to its next
// (i + 1)th station. You begin the journey with an empty tank at one of the gas stations.
// Given two integer arrays gas and cost, return the starting gas station's index if you can travel around the circuit
// once in the clockwise direction, otherwise return -1. If there exists a solution, it is guaranteed to be unique.

// Example 1:
// Input: gas = [1,2,3,4,5], cost = [3,4,5,1,2]
// Output: 3
// Explanation:
// Start at station 3 (index 3) and fill up with 4 unit of gas. Your tank = 0 + 4 = 4
// Travel to station 4. Your tank = 4 - 1 + 5 = 8
// Travel to station 0. Your tank = 8 - 2 + 1 = 7
// Travel to station 1. Your tank = 7 - 3 + 2 = 6
// Travel to station 2. Your tank = 6 - 4 + 3 = 5
// Travel to station 3. The cost is 5. Your gas is just enough to travel back to station 3.
// Therefore, return 3 as the starting index.

// Example 2:
// Input: gas = [2,3,4], cost = [3,4,3]
// Output: -1
// Explanation:
// You can't start at station 0 or 1, as there is not enough gas to travel to the next station.
// Let's start at station 2 and fill up with 4 unit of gas. Your tank = 0 + 4 = 4
// Travel to station 0. Your tank = 4 - 3 + 2 = 3
// Travel to station 1. Your tank = 3 - 3 + 3 = 3
// You cannot travel back to station 2, as it requires 4 unit of gas but you only have 3.
// Therefore, you can't travel around the circuit once no matter where you start.

// Algorithm (Greedy):
// 1. Initialize 3 variables: start = 0, totalFuelBalance = 0 and currentFuelBalance = 0.
//    -> start: The index of the station from where we are trying to start the trip.
//    -> totalFuelBalance (tFB): Tracks the overall fuel balance of the entire trip, i.e.,
//       tFB = sum of all gas - sum of all cost.
//       If tFB >= 0 at the end, the trip is possible (from some station). If tFB < 0, there is not enough gas in total,
//       so the trip is not possible from any station.
//       So tFB only tells WHETHER the trip is possible. To find WHERE it should start, we use 'cFB' and 'start'.
//    -> currentFuelBalance (cFB): The fuel in the tank since we started from 'start'. If cFB becomes < 0 at the ith
//       station, we can't reach the next station. So we try to start from the next station: start = i + 1, and reset
//       cFB to 0.
//
//    EXTRA NOTE:
//    Q) Why not check starting the trip from each station, i.e., start = 0, 1, 2, ... one by one?
//       Why does 'start' directly jump to 'i + 1' if cFB becomes negative at the ith station?
//    A) Suppose we started at station 0 and reached station 3, but we can't reach station 4.
//       Reaching station 3 means the fuel was enough at stations 0, 1 and 2, i.e., cFB was >= 0 at each of them.
//       So each of those stations ADDED fuel (or at least nothing negative) to the tank before station 3.
//       If even with that extra fuel we fail at station 3, then starting from station 1 or 2 (with less fuel) will
//       surely fail at station 3 too. If the sum of (3 positives + 1 big negative) is < 0, the sum of
//       (2 positives + 1 big negative) can never be >= 0.
//       So without wasting time checking from each station, we directly jump to the (i + 1)th station.
//
// 2. At the end of the loop, if tFB >= 0, return 'start'; else return -1.
//    In this way, we visit every station only once and still find the station from which the trip can be completed.


using System;

public class Solution
{
	// T.C: O(n)
	// S.C: O(1)
	public int CanCompleteCircuit(int[] gas, int[] cost)
	{
		int start = 0;                // potential start index
		int totalFuelBalance = 0;     // total fuel balance across all stations
		int currentFuelBalance = 0;   // current fuel balance for the ongoing trip

		for (int i = 0; i < gas.Length; i++)
		{
			totalFuelBalance += gas[i] - cost[i];   // total fuel balance till station i

			// If we run out of fuel at the current station, reset the start index to the next station,
			// and currentFuelBalance to 0.
			currentFuelBalance += gas[i] - cost[i];
			if (currentFuelBalance < 0)
			{
				start = i + 1;   // the next station becomes the next potential start index
				currentFuelBalance = 0;
			}
		}

		// If the total fuel balance is negative, it means the trip is not possible. Otherwise, return the start index.
		return (totalFuelBalance < 0) ? -1 : start;
	}
}


class Program
{
	static void Main()
	{
		int[] gas = { 1, 2, 3, 4, 5 };
		int[] cost = { 3, 4, 5, 1, 2 };

		Console.WriteLine(new Solution().CanCompleteCircuit(gas, cost));   // o/p: 3
	}
}

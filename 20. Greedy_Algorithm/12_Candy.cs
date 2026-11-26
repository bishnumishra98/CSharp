// Leetcode: 135. Candy   --->   n children stand in a line, ratings[i] is child i's rating. Give candies so that:
// ● every child gets at least 1,
// ● a child with a HIGHER rating than a NEIGHBOUR gets MORE candies than that neighbour.
// Return the minimum total number of candies.

// Example 1:
// Input: ratings = [1,0,2]
// Output: 5   (2, 1, 2)

// Example 2:
// Input: ratings = [1,2,2]
// Output: 4   (1, 2, 1: the last child has the same rating as its neighbour, so it can have fewer)

// Example 3:
// Input: ratings = [0,2,4,7,6,5,4,3,2,1,1,1,2,3,4,2,1,1,1]
// Output: 50
// Candies:          [1,2,3,7,6,5,4,3,2,1,1,1,2,3,4,2,1,1,1]
// (The C++ comment's candy list had a typo: an empty entry "2,,3".)


// Dry run image (in this folder): 12_Candy.jpg

using System;

public class Solution
{
	// Better (two passes): Satisfy each side separately, then combine.
	// 1. Left pass:  left[i]  = left[i - 1] + 1  if ratings[i] > ratings[i - 1], else 1.   (only the LEFT neighbour rule)
	// 2. Right pass: right[i] = right[i + 1] + 1 if ratings[i] > ratings[i + 1], else 1.  (only the RIGHT neighbour rule)
	// 3. Child i needs max(left[i], right[i]): enough for both rules, and no more than needed.
	// T.C: O(n)
	// S.C: O(n)
	public int Candy_TwoPass(int[] ratings)
	{
		int n = ratings.Length;
		int[] left = new int[n], right = new int[n];

		left[0] = 1;
		for (int i = 1; i < n; i++) left[i] = ratings[i] > ratings[i - 1] ? left[i - 1] + 1 : 1;

		right[n - 1] = 1;
		for (int i = n - 2; i >= 0; i--) right[i] = ratings[i] > ratings[i + 1] ? right[i + 1] + 1 : 1;

		int sum = 0;
		for (int i = 0; i < n; i++) sum += Math.Max(left[i], right[i]);
		return sum;
	}

// ------------------------------------------------------------------------------

	// Optimal (slopes, O(1) space, see the image): Draw the ratings as a mountain range. It is made of flat parts, UP
	// slopes and DOWN slopes.
	// ● Flat (equal neighbours): each child can get 1.
	// ● Up slope: 2, 3, 4, ... candies (one more each step). 'peak' = the candies of the top child.
	// ● Down slope: we do not know yet how long it is, so count it from the bottom instead: 1, 2, 3, ... and add them.
	//   'down' = the length of the down slope. Its first child (next to the peak) would need 'down' candies, but the peak
	//   child was given only 'peak'. If down > peak, the peak needs to be raised: add (down - peak) more.
	// e.g., [1, 3, 5, 4, 2, 1]: up 1, 2, 3 (peak 3). Down part 4, 2, 1 gets 3, 2, 1 (counted as 1 + 2 + 3).
	//       The peak (5) must beat 4, which has 3: needs 4, but has 3 -> add 1. Total 1 + 2 + 3 + 6 + 1 = 13.
	// T.C: O(n)
	// S.C: O(1)
	public int Candy(int[] ratings)
	{
		int n = ratings.Length, sum = 1, i = 1;

		while (i < n)
		{
			if (ratings[i] == ratings[i - 1])   // flat
			{
				sum++;
				i++;
				continue;
			}

			int peak = 1;
			while (i < n && ratings[i] > ratings[i - 1])   // up slope
			{
				peak++;
				sum += peak;
				i++;
			}

			int down = 0;
			while (i < n && ratings[i] < ratings[i - 1])   // down slope
			{
				down++;
				sum += down;
				i++;
			}

			if (down >= peak) sum += down - peak + 1;   // raise the peak so it beats the down slope
		}
		return sum;
	}
}

// Note: The C++ slope code started 'down' at 1 and added before counting (sum += down; down++), so 'down' ended one too
//       big, and then compared "down > peak". The two off-by-ones cancel out, so it was correct; counting the down slope
//       directly (as here) is easier to follow.


class Program
{
	static void Main()
	{
		int[] ratings = { 0, 2, 4, 7, 6, 5, 4, 3, 2, 1, 1, 1, 2, 3, 4, 2, 1, 1, 1 };

		Console.WriteLine(new Solution().Candy_TwoPass(ratings));   // o/p: 50
		Console.WriteLine(new Solution().Candy(ratings));           // o/p: 50
	}
}

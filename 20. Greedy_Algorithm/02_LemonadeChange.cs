// Leetcode: 860. Lemonade Change   --->   A lemonade costs $5. Customers pay one at a time with a $5, $10 or $20 bill
// (bills[i]). You start with no money, and must give each customer the right change. Return true if you can.

// Example 1:
// Input: bills = [5, 5, 5, 10, 20]
// Output: true    (the $10 gets one $5 back; the $20 gets $10 + $5 back)

// Example 2:
// Input: bills = [5, 5, 10, 10, 20]
// Output: false   (for the $20 we have two $10's but no $5)


using System;

public class Solution
{
	// Algorithm: Only count the $5 and $10 bills we hold ($20s are never given back as change).
	//   $5:  keep it.
	//   $10: give back one $5 (none -> false).
	//   $20: give back $15. Two ways: $10 + $5, or $5 + $5 + $5. Greedy choice: prefer $10 + $5.
	// Why prefer the $10 ? A $5 can make change for BOTH $10 and $20 customers, a $10 only for $20 customers. So keep the
	// more useful $5 bills when we can.
	// T.C: O(n)
	// S.C: O(1)
	public bool LemonadeChange(int[] bills)
	{
		int five = 0, ten = 0;

		foreach (int bill in bills)
		{
			if (bill == 5)
			{
				five++;
			}
			else if (bill == 10)
			{
				if (five == 0) return false;
				five--;
				ten++;
			}
			else   // 20
			{
				if (ten > 0 && five > 0)
				{
					ten--;
					five--;
				}
				else if (five >= 3) five -= 3;
				else return false;
			}
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(new Solution().LemonadeChange(new int[] { 5, 5, 5, 10, 20 }));   // o/p: True
	}
}

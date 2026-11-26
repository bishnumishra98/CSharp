using System;

// Armstrong number: sum of (each digit ^ no.of digits) == the number itself
// Example: 153 -> 1^3 + 5^3 + 3^3 = 1 + 125 + 27 = 153

// LeetCode style: our logic lives inside class Solution
public class Solution
{
	// T.C: O(logn)
	// S.C: O(1)
	public bool IsArmstrong(int n)
	{
		int num = n;   // keeping a backup of original number inside 'num'
		int noOfDigits = (int)Math.Log10(n) + 1;   // counting no.of digits in number

		// Armstrong number logic
		int bit, sum = 0;
		while (n != 0)   // C# needs a real bool, so 'while(n)' becomes 'while(n != 0)'
		{
			bit = n % 10;
			sum = sum + (int)Math.Round(Math.Pow(bit, noOfDigits));
			// Math.Pow() takes and returns double, even if we pass ints.
			// C# won't put a double into an int without a cast, so we cast with (int).
			// Round first, because a double can come out like 124.9999 and (int) would chop it to 124.
			n = n / 10;
		}

		// comparing value in sum with original number
		return sum == num;
	}
}

// LeetCode does this part for us. To run it on our PC, we write it ourselves.
class Program
{
	static void Main()
	{
		Solution sol = new Solution();   // IsArmstrong() is not static, so we need an object first

		int n = 153;
		if (sol.IsArmstrong(n)) Console.WriteLine("Armstrong number");
		else Console.WriteLine("Not an Armstrong number");
		// o/p: "Armstrong number"
	}
}

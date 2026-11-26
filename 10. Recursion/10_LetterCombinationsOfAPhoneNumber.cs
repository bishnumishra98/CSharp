// Leetcode: 17. Letter Combinations of a Phone Number   --->   Given a string containing digits from 2-9 inclusive,
// return all possible letter combinations that the number could represent. Return the answer in any order.
// A mapping of digits to letters (just like on the telephone buttons) is given below. Note that 1 does not map to any
// letters.
//      2: abc   3: def   4: ghi   5: jkl   6: mno   7: pqrs   8: tuv   9: wxyz

// Example 1:
// Input: digits = "23"
// Output: ["ad","ae","af","bd","be","bf","cd","ce","cf"]

// Example 2:
// Input: digits = "2"
// Output: ["a","b","c"]

// Constraints: 1 <= digits.length <= 4


// Dry run image (in this folder): 10_LetterCombinationsOfAPhoneNumber.jpg

using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
	private readonly string[] mapping = { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };

	// Algorithm: One recursion level per digit.
	// 1. If index == digits.Length, every digit has a letter. Save the combination.
	// 2. Else, for every letter of the current digit: add it, recurse for the next digit, then backtrack (remove it).
	// Recursion tree for "23":
	//                       ""
	//           a /          b|          \ c          <- letters of '2'
	//          "a"           "b"           "c"
	//        d/ e| \f      d/ e| \f      d/ e| \f     <- letters of '3'
	//       ad  ae  af    bd  be  bf    cd  ce  cf
	// T.C: O(4^n * n);   where n = digits.Length. Each digit has at most 4 letters, so at most 4^n combinations,
	//                    and building each string costs n.
	// S.C: O(n);   recursion depth and the StringBuilder (ignoring the answer)
	public IList<string> LetterCombinations(string digits)
	{
		List<string> ans = new List<string>();
		if (digits.Length == 0) return ans;

		Build(0, digits, new StringBuilder(), ans);
		return ans;
	}

	private void Build(int index, string digits, StringBuilder current, List<string> ans)
	{
		if (index == digits.Length)
		{
			ans.Add(current.ToString());
			return;
		}

		string letters = mapping[digits[index] - '0'];   // e.g., digits[index] = '2'  ->  "abc"
		foreach (char c in letters)
		{
			current.Append(c);
			Build(index + 1, digits, current, ans);
			current.Length--;   // backtrack: remove the last letter
		}
	}
}


class Program
{
	static void Main()
	{
		Console.WriteLine(string.Join(" ", new Solution().LetterCombinations("23")));   // o/p: ad ae af bd be bf cd ce cf
	}
}

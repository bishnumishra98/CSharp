// Problem: Reverse a given string.

// Example 1:
// Input: str = "Hello"
// Output: "olleH"

// Example 2:
// Input: str = "racecar"
// Output: "racecar"

// NOTE: Strings in C# are immutable, i.e., we can't change a character inside a string (str[0] = 'x' gives an error).
//       Also there is no Push() on a string. So either we build a new string, or we convert it into a char [],
//       change the array, and convert it back to a string.


using System;
using System.Text;
using System.Linq;   // for Reverse() and ToArray() used in Reverse_LINQ

public class Solution
{
	// Naive approach: Build a new string by adding characters from the end.
	// 1. Start with an empty string 'ans'.
	// 2. Run a loop from the last index to the first index and add each character to 'ans'.
	// Shortcoming: Every 'ans += str[i]' creates a brand new string (strings are immutable), and copies the old one into it.
	//              So for a long string, this becomes slow.
	// T.C: O(n^2)
	// S.C: O(n)
	public string Reverse_Naive(string str)
	{
		string ans = "";

		for (int i = str.Length - 1; i >= 0; i--)
		{
			ans += str[i];
		}

		return ans;
	}

// ------------------------------------------------------------------------------

	// Better approach: Use StringBuilder.
	// StringBuilder is a "changeable" string. Append() adds to the same object, no new string is created each time.
	// T.C: O(n)
	// S.C: O(n)
	public string Reverse_StringBuilder(string str)
	{
		StringBuilder sb = new StringBuilder(str.Length);

		for (int i = str.Length - 1; i >= 0; i--)
		{
			sb.Append(str[i]);
		}

		return sb.ToString();
	}

// ------------------------------------------------------------------------------

	// Best approach: Two pointers on a char array.
	// 1. Convert the string into a char array (arrays can be changed).
	// 2. Keep pointer 'left' at the start and 'right' at the end.
	// 3. Swap arr[left] and arr[right], then move left ahead and right back. Stop when they meet.
	// 4. Convert the char array back to a string.
	// T.C: O(n)
	// S.C: O(n);   for the char array (needed because the string itself can't be changed)
	public string Reverse(string str)
	{
		char [] arr = str.ToCharArray();
		int left = 0;
		int right = arr.Length - 1;

		while (left < right)
		{
			char temp = arr[left];
			arr[left] = arr[right];
			arr[right] = temp;
			left++;
			right--;
		}

		return new string(arr);
	}

// ------------------------------------------------------------------------------

	// Built-in way: Array.Reverse() does the same two pointer swapping for us.
	// T.C: O(n)
	// S.C: O(n)
	public string Reverse_BuiltIn(string str)
	{
		char [] arr = str.ToCharArray();
		Array.Reverse(arr);
		return new string(arr);
	}

// ------------------------------------------------------------------------------

	// LINQ way: A string is IEnumerable<char> (a sequence of characters), so LINQ methods work on it.
	// 1. str.Reverse() gives the characters in reverse order: 'o', 'l', 'l', 'e', 'H' (as IEnumerable<char>, not a string).
	// 2. ToArray() converts them into a char array.
	// 3. new string(...) converts the char array back to a string.
	// Shortcoming: Shortest code, but a bit slower than the others because LINQ first copies all items into a buffer
	//              and then gives them back one by one.
	// T.C: O(n)
	// S.C: O(n)
	public string Reverse_LINQ(string str)
	{
		return new string(str.Reverse().ToArray());
	}
}


class Program
{
	static void Main()
	{
		string str = "Hello";

		// string ans = new Solution().Reverse_Naive(str);
		// string ans = new Solution().Reverse_StringBuilder(str);
		string ans = new Solution().Reverse(str);
		// string ans = new Solution().Reverse_BuiltIn(str);
		// string ans = new Solution().Reverse_LINQ(str);

		Console.WriteLine(ans);   // o/p: "olleH"
	}
}

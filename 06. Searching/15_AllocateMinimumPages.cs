// GFG: Allocate Minimum Pages   --->   You are given an array arr[] of integers, where each element arr[i] represents
// the number of pages in the ith book. You also have an integer k representing the number of students. The task is to
// allocate books to each student such that:
// • Each student receives atleast one book.
// • Each student is assigned a contiguous sequence of books.
// • No book is assigned to more than one student.
// The objective is to minimize the maximum number of pages assigned to any student. In other words, out of all possible
// allocations, find the arrangement where the student who receives the most pages still has the smallest possible maximum.
// Note: Return -1 if a valid assignment is not possible, and allotment should be in contiguous order.

// Example 1:
// Input: arr[] = [12, 34, 67, 90], k = 2
// Output: 113
// Explanation: Allocation can be done in following ways:
// {12} and {34, 67, 90} Maximum Pages = 191
// {12, 34} and {67, 90} Maximum Pages = 157
// {12, 34, 67} and {90} Maximum Pages = 113.
// Therefore, the minimum of these cases is 113, which is selected as the output.

// Example 2:
// Input: arr[] = [15, 17, 20], k = 5
// Output: -1
// Explanation: Allocation can not be done.

// Example 3:
// Input: arr[] = [22, 23, 67], k = 1
// Output: 112

// Problem link: https://www.geeksforgeeks.org/problems/allocate-minimum-number-of-pages0937/1

// Understand the problem: Let 'maxPages' = the most pages any one student is allowed to get. We need the SMALLEST
// maxPages for which all books can be given to k students.
// Search space of the answer:
// ● Smallest = max(arr), the thickest book. If the limit were smaller, nobody could take the thickest book.
//   e.g., [12, 34, 67, 90]: a limit of 67 can't work, because who takes the book with 90 pages ?
// ● Biggest  = sum(arr), i.e., one student takes all the books.
// Edge case: If k > number of books, some student gets no book. So return -1.


using System;

public class Solution
{
	// Can we give all books to at most 'k' students, if no student can get more than 'maxPages' pages ?
	// Greedy: Give books to the 1st student, one by one, as long as the total stays <= maxPages. When the next book
	// doesn't fit, give it to a new student. In the end, count the students we needed.
	// Smaller maxPages = more students needed.
	private bool IsPossible(int[] arr, int maxPages, int k)
	{
		int students = 1;
		long pages = 0;   // pages given to the current student

		for (int i = 0; i < arr.Length; i++)
		{
			if (pages + arr[i] <= maxPages) pages += arr[i];
			else
			{
				students++;      // new student
				pages = arr[i];  // gets the current book
			}
		}

		return students <= k;
	}

	// ● Why 'students <= k' and not 'students == k' ?
	//   If all books can be shared among fewer students, they can also be shared among exactly k students: just take
	//   any student with 2 or more books and give one of the books to a new student. Pages per student only go down.
	//   e.g., arr = [15, 10, 19, 10, 5, 18, 7], k = 5, maxPages = 25:
	//         Greedy uses 4 students: {15, 10}, {19}, {10, 5}, {18, 7}.
	//         5 students also work:   {15, 10}, {19}, {10, 5}, {18}, {7}.
	//   With '== k', we would wrongly say 25 is not possible.

	// Brute force: Linear search. Try every limit from max(arr) to sum(arr). The first one that works is the answer.
	// T.C: O((sum - max) * n);   where n = arr.Length
	// S.C: O(1)
	public int FindPages_BruteForce(int[] arr, int k)
	{
		if (k > arr.Length) return -1;

		int max = 0, sum = 0;
		for (int i = 0; i < arr.Length; i++)
		{
			max = Math.Max(max, arr[i]);
			sum += arr[i];
		}

		for (int maxPages = max; maxPages <= sum; maxPages++)
		{
			if (IsPossible(arr, maxPages, k)) return maxPages;
		}

		return sum;   // never reached, as maxPages = sum always works
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Binary search on answer (maxPages).
	// If a limit works, every bigger limit also works. So the pattern is sorted:
	// maxPages:  90 ... 112  113  114 ...
	// works?      N ...   N    Y    Y ...    ->  We need the first Y.
	// 1. start = max(arr), end = sum(arr). Find mid (a limit):
	//    -> If IsPossible(mid): mid works. Store ans = mid, and try a smaller limit: end = mid - 1.
	//    -> Else: mid is too small. Try a bigger limit: start = mid + 1.
	// T.C: O(log(sum - max) * n);   where n = arr.Length
	// S.C: O(1)
	public int FindPages(int[] arr, int k)
	{
		if (k > arr.Length) return -1;

		int start = 0, end = 0;
		for (int i = 0; i < arr.Length; i++)
		{
			start = Math.Max(start, arr[i]);
			end += arr[i];
		}

		int ans = end;
		while (start <= end)
		{
			int mid = start + (end - start) / 2;

			if (IsPossible(arr, mid, k))
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
		int[] arr = { 12, 34, 67, 90 };
		int k = 2;

		Console.WriteLine(new Solution().FindPages_BruteForce(arr, k));   // o/p: 113
		Console.WriteLine(new Solution().FindPages(arr, k));              // o/p: 113
	}
}

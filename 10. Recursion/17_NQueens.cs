// Leetcode: 51. N-Queens   --->   The n-queens puzzle is the problem of placing n queens on an n x n chessboard such
// that no two queens attack each other.
// Given an integer n, return all distinct solutions to the n-queens puzzle. You may return the answer in any order.
// Each solution contains a distinct board configuration of the n-queens' placement, where 'Q' and '.' both indicate a
// queen and an empty space, respectively.
// (A queen attacks every cell in its row, its column, and both its diagonals.)

// Example 1:
// Input: n = 4
// Output: [[".Q..","...Q","Q...","..Q."],["..Q.","Q...","...Q",".Q.."]]
// Explanation: There exist two distinct solutions to the 4-queens puzzle:
//              . Q . .        . . Q .
//              . . . Q        Q . . .
//              Q . . .        . . . Q
//              . . Q .        . Q . .

// Example 2:
// Input: n = 1
// Output: [["Q"]]

// Problem link: https://leetcode.com/problems/n-queens/description/

// Algorithm: Backtracking, one COLUMN at a time.
// Every column must have exactly one queen. So place a queen in column 0, then column 1, and so on.
// 1. For the current column, try every row. Place the queen only if it is safe (no other queen attacks that cell).
// 2. Then solve the next column. When coming back, remove the queen (backtrack) and try the next row.
// 3. If all n columns have a queen (col == n), save the board.
// Which directions to check ? We fill columns from left to right, so all existing queens are on the LEFT. So only check
// 3 directions towards the left: same row, upper-left diagonal, and lower-left diagonal.


// Dry run images (in this folder): 17_NQueens_bruteForce.jpg, 17_NQueens_optimised.jpg

using System;
using System.Collections.Generic;
using System.Linq;   // for Select() in Main, to print the boards

public class Solution
{
	// Brute force: Check safety by walking from the cell towards the left in all 3 directions.
	// T.C: O(n! * n);   about n! placements are tried, and each safety check walks up to n cells
	// S.C: O(n^2);   the board (ignoring the answer)
	public IList<IList<string>> SolveNQueens_BruteForce(int n)
	{
		char[][] board = NewBoard(n);
		List<IList<string>> ans = new List<IList<string>>();
		Solve(0, board, n, ans);
		return ans;
	}

	private void Solve(int col, char[][] board, int n, List<IList<string>> ans)
	{
		if (col == n)
		{
			ans.Add(board.Select(row => new string(row)).ToList());
			return;
		}

		for (int row = 0; row < n; row++)
		{
			if (!IsSafe(row, col, board, n)) continue;

			board[row][col] = 'Q';
			Solve(col + 1, board, n, ans);
			board[row][col] = '.';   // backtrack
		}
	}

	private bool IsSafe(int row, int col, char[][] board, int n)
	{
		for (int r = row, c = col; r >= 0 && c >= 0; r--, c--)   // upper-left diagonal
		{
			if (board[r][c] == 'Q') return false;
		}
		for (int c = col; c >= 0; c--)                           // same row, to the left
		{
			if (board[row][c] == 'Q') return false;
		}
		for (int r = row, c = col; r < n && c >= 0; r++, c--)    // lower-left diagonal
		{
			if (board[r][c] == 'Q') return false;
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: Make the safety check O(1) with 3 bool arrays (hashing).
	// • rowUsed[row]: is there a queen in this row ?
	// • Every cell on the same "/" diagonal (lower-left to upper-right) has the same (row + col).
	//   e.g., (2, 0), (1, 1), (0, 2) all have row + col = 2.  So diag1Used[row + col].  Values: 0 to 2n - 2.
	// • Every cell on the same "\" diagonal (upper-left to lower-right) has the same (col - row).
	//   e.g., (0, 0), (1, 1), (2, 2) all have col - row = 0. It can be negative (from -(n - 1) to n - 1), so add n - 1
	//   to make it a valid index: diag2Used[n - 1 + col - row].  Values: 0 to 2n - 2.
	//                                                         row + col (for n = 4):     n - 1 + col - row:
	//                                                           0 1 2 3                    3 4 5 6
	//                                                           1 2 3 4                    2 3 4 5
	//                                                           2 3 4 5                    1 2 3 4
	//                                                           3 4 5 6                    0 1 2 3
	// Place a queen: mark all 3. Remove it (backtrack): unmark all 3.
	// T.C: O(n!);   the same placements are tried, but each check is O(1)
	// S.C: O(n^2);   the board, plus O(n) for the 3 arrays
	public IList<IList<string>> SolveNQueens(int n)
	{
		char[][] board = NewBoard(n);
		List<IList<string>> ans = new List<IList<string>>();
		SolveFast(0, board, n, new bool[n], new bool[2 * n - 1], new bool[2 * n - 1], ans);
		return ans;
	}

	private void SolveFast(int col, char[][] board, int n, bool[] rowUsed, bool[] diag1Used, bool[] diag2Used,
	                       List<IList<string>> ans)
	{
		if (col == n)
		{
			ans.Add(board.Select(row => new string(row)).ToList());
			return;
		}

		for (int row = 0; row < n; row++)
		{
			if (rowUsed[row] || diag1Used[row + col] || diag2Used[n - 1 + col - row]) continue;

			board[row][col] = 'Q';
			rowUsed[row] = diag1Used[row + col] = diag2Used[n - 1 + col - row] = true;

			SolveFast(col + 1, board, n, rowUsed, diag1Used, diag2Used, ans);

			board[row][col] = '.';   // backtrack
			rowUsed[row] = diag1Used[row + col] = diag2Used[n - 1 + col - row] = false;
		}
	}

	// An n x n board filled with '.'
	private char[][] NewBoard(int n)
	{
		char[][] board = new char[n][];
		for (int i = 0; i < n; i++) board[i] = new string('.', n).ToCharArray();
		return board;
	}
}


class Program
{
	static void Main()
	{
		int n = 4;

		var ans1 = new Solution().SolveNQueens_BruteForce(n);
		Console.WriteLine(string.Join(" | ", ans1.Select(b => string.Join(" ", b))));   // o/p: ..Q. Q... ...Q .Q.. | .Q.. ...Q Q... ..Q.

		var ans2 = new Solution().SolveNQueens(n);
		Console.WriteLine(string.Join(" | ", ans2.Select(b => string.Join(" ", b))));   // o/p: ..Q. Q... ...Q .Q.. | .Q.. ...Q Q... ..Q.
	}
}

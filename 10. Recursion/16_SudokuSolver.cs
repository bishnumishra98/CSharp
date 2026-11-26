// Leetcode: 37. Sudoku Solver   --->   Write a program to solve a Sudoku puzzle by filling the empty cells.
// A sudoku solution must satisfy all of the following rules:
// 1. Each of the digits 1-9 must occur exactly once in each row.
// 2. Each of the digits 1-9 must occur exactly once in each column.
// 3. Each of the digits 1-9 must occur exactly once in each of the 9 3x3 sub-boxes of the grid.
// The '.' character indicates empty cells.
// The input board has exactly one solution.

// Example 1:
// Input:                          Output:
// 5 3 . | . 7 . | . . .           5 3 4 | 6 7 8 | 9 1 2
// 6 . . | 1 9 5 | . . .           6 7 2 | 1 9 5 | 3 4 8
// . 9 8 | . . . | . 6 .           1 9 8 | 3 4 2 | 5 6 7
// ------+-------+------           ------+-------+------
// 8 . . | . 6 . | . . 3           8 5 9 | 7 6 1 | 4 2 3
// 4 . . | 8 . 3 | . . 1           4 2 6 | 8 5 3 | 7 9 1
// 7 . . | . 2 . | . . 6           7 1 3 | 9 2 4 | 8 5 6
// ------+-------+------           ------+-------+------
// . 6 . | . . . | 2 8 .           9 6 1 | 5 3 7 | 2 8 4
// . . . | 4 1 9 | . . 5           2 8 7 | 4 1 9 | 6 3 5
// . . . | . 8 . | . 7 9           3 4 5 | 2 8 6 | 1 7 9

// Problem link: https://leetcode.com/problems/sudoku-solver/description/
// Video link: https://www.youtube.com/watch?v=FWAIf_EVUKE&t=939s
// First understand '15_ValidSudoku.cs' (how to find a cell's 3 x 3 box).

// Algorithm: Backtracking.
// 1. Find the first empty cell.
// 2. Try digits '1' to '9' in it. For each digit that doesn't break the rules (not in the same row, column or box):
//    put it there, and recursively solve the rest of the board.
//    -> If the rest gets solved, return true (done, keep this digit).
//    -> Else this digit leads to a dead end. Remove it (backtrack) and try the next digit.
// 3. If no digit works in this cell, return false, so that the PREVIOUS cell changes its digit.
// 4. If there is no empty cell left, the board is solved. Return true.


using System;

public class Solution
{
	// Simple version: Check row, column and box by scanning them every time.
	// T.C: O(9^b * 9 * 81);   where b = number of empty cells. Each empty cell tries up to 9 digits, each check costs 9,
	//                         and finding the next empty cell scans the board.
	//                         (Very fast in practice, as wrong digits are cut off early.)
	// S.C: O(b);   recursion depth
	public void SolveSudoku_Simple(char[][] board)
	{
		Solve(board);
	}

	private bool Solve(char[][] board)
	{
		for (int r = 0; r < 9; r++)
		{
			for (int c = 0; c < 9; c++)
			{
				if (board[r][c] != '.') continue;

				for (char d = '1'; d <= '9'; d++)
				{
					if (!CanPlace(board, r, c, d)) continue;

					board[r][c] = d;
					if (Solve(board)) return true;
					board[r][c] = '.';   // backtrack
				}
				return false;   // nothing fits here: go back and change an earlier cell
			}
		}
		return true;   // no empty cell left
	}

	private bool CanPlace(char[][] board, int row, int col, char d)
	{
		for (int i = 0; i < 9; i++)
		{
			if (board[i][col] == d) return false;                                       // column
			if (board[row][i] == d) return false;                                       // row
			if (board[3 * (row / 3) + i / 3][3 * (col / 3) + i % 3] == d) return false;   // box
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// Faster version: Remember what each row, column and box has (like IsValidSudoku() in '15_ValidSudoku.cs'), so every
	// "can I place d here ?" check is O(1) instead of scanning 27 cells. Also continue the search from the NEXT cell
	// (pos + 1), instead of scanning the board from the start every time.
	// Every time a digit is placed, mark it in its row, column and box. When it is removed (backtrack), unmark it.
	// T.C: O(9^b);   same worst case, but every step is O(1)
	// S.C: O(b + 3 * 81)
	public void SolveSudoku(char[][] board)
	{
		bool[,] rowHas = new bool[9, 10], colHas = new bool[9, 10], boxHas = new bool[9, 10];

		for (int r = 0; r < 9; r++)
		{
			for (int c = 0; c < 9; c++)
			{
				if (board[r][c] == '.') continue;
				int d = board[r][c] - '0';
				rowHas[r, d] = colHas[c, d] = boxHas[(r / 3) * 3 + c / 3, d] = true;
			}
		}

		SolveFrom(0, board, rowHas, colHas, boxHas);
	}

	// pos = 0 to 80 numbers the cells row by row: row = pos / 9, col = pos % 9.
	private bool SolveFrom(int pos, char[][] board, bool[,] rowHas, bool[,] colHas, bool[,] boxHas)
	{
		if (pos == 81) return true;   // every cell is filled

		int r = pos / 9, c = pos % 9, b = (r / 3) * 3 + c / 3;
		if (board[r][c] != '.') return SolveFrom(pos + 1, board, rowHas, colHas, boxHas);   // already filled, skip

		for (int d = 1; d <= 9; d++)
		{
			if (rowHas[r, d] || colHas[c, d] || boxHas[b, d]) continue;

			board[r][c] = (char)('0' + d);
			rowHas[r, d] = colHas[c, d] = boxHas[b, d] = true;

			if (SolveFrom(pos + 1, board, rowHas, colHas, boxHas)) return true;

			board[r][c] = '.';   // backtrack
			rowHas[r, d] = colHas[c, d] = boxHas[b, d] = false;
		}
		return false;
	}
}


class Program
{
	static char[][] NewBoard()
	{
		return new char[][]
		{
			"53..7....".ToCharArray(),
			"6..195...".ToCharArray(),
			".98....6.".ToCharArray(),
			"8...6...3".ToCharArray(),
			"4..8.3..1".ToCharArray(),
			"7...2...6".ToCharArray(),
			".6....28.".ToCharArray(),
			"...419..5".ToCharArray(),
			"....8..79".ToCharArray()
		};
	}

	static void Main()
	{
		char[][] board1 = NewBoard();   // each method gets its own board, as solving changes it
		new Solution().SolveSudoku_Simple(board1);
		Console.WriteLine(new string(board1[0]) + " ... " + new string(board1[8]));   // o/p: 534678912 ... 345286179

		char[][] board2 = NewBoard();
		new Solution().SolveSudoku(board2);
		Console.WriteLine(new string(board2[0]) + " ... " + new string(board2[8]));   // o/p: 534678912 ... 345286179
	}
}

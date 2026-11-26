// Leetcode 36. Valid Sudoku   --->   Determine if a 9 x 9 Sudoku board is valid. Only the filled cells need to be
// validated according to the following rules:
// 1. Each row must contain the digits 1-9 without repetition.
// 2. Each column must contain the digits 1-9 without repetition.
// 3. Each of the nine 3 x 3 sub-boxes of the grid must contain the digits 1-9 without repetition.
// Note:
// • A Sudoku board (partially filled) could be valid but is not necessarily solvable.
// • Only the filled cells need to be validated according to the mentioned rules.

// Example 1:
// Input: board =
// [["5","3",".",".","7",".",".",".","."]
// ,["6",".",".","1","9","5",".",".","."]
// ,[".","9","8",".",".",".",".","6","."]
// ,["8",".",".",".","6",".",".",".","3"]
// ,["4",".",".","8",".","3",".",".","1"]
// ,["7",".",".",".","2",".",".",".","6"]
// ,[".","6",".",".",".",".","2","8","."]
// ,[".",".",".","4","1","9",".",".","5"]
// ,[".",".",".",".","8",".",".","7","9"]]
// Output: true

// Example 2: Same board, but the top left 5 is changed to 8.
// Output: false
// Explanation: There are two 8's in the top left 3x3 sub-box, so it is invalid.

// (Not really a recursion problem, but it is the first step of '16_SudokuSolver.cs'.)

// ● Which 3 x 3 box is a cell in ?
//   The top-left cell of the box of (row, col) is (3 * (row / 3), 3 * (col / 3)). e.g., (4, 7) -> box starts at (3, 6).
//   The 9 cells of that box, for i = 0 to 8:  (3 * (row / 3) + i / 3,  3 * (col / 3) + i % 3)
//   Box number (0 to 8, left to right, top to bottom): (row / 3) * 3 + col / 3. e.g., (4, 7) -> 1 * 3 + 2 = 5


using System;

public class Solution
{
	// Brute force: For every filled cell, check its row, its column and its box for the same digit in ANOTHER cell.
	// T.C: O(81 * 27) = O(1);   the board is always 9 x 9
	// S.C: O(1)
	public bool IsValidSudoku_BruteForce(char[][] board)
	{
		for (int r = 0; r < 9; r++)
		{
			for (int c = 0; c < 9; c++)
			{
				if (board[r][c] != '.' && !IsValidCell(board, r, c)) return false;
			}
		}
		return true;
	}

	private bool IsValidCell(char[][] board, int row, int col)
	{
		char ch = board[row][col];
		for (int i = 0; i < 9; i++)
		{
			if (i != row && board[i][col] == ch) return false;   // same column, other cell
			if (i != col && board[row][i] == ch) return false;   // same row, other cell

			int br = 3 * (row / 3) + i / 3, bc = 3 * (col / 3) + i % 3;
			if ((br != row || bc != col) && board[br][bc] == ch) return false;   // same box, other cell
		}
		return true;
	}

// ------------------------------------------------------------------------------

	// Optimal algorithm: One pass, remembering what each row, column and box already has.
	// rowHas[r, d] = true if row r already has digit d. Same for colHas[c, d] and boxHas[b, d].
	// For every filled cell: if its digit is already in its row, column or box, it's a repeat -> false.
	//                        Else mark it in all 3.
	// T.C: O(81) = O(1);   each cell is visited once
	// S.C: O(3 * 81) = O(1)
	public bool IsValidSudoku(char[][] board)
	{
		bool[,] rowHas = new bool[9, 10], colHas = new bool[9, 10], boxHas = new bool[9, 10];

		for (int r = 0; r < 9; r++)
		{
			for (int c = 0; c < 9; c++)
			{
				if (board[r][c] == '.') continue;

				int d = board[r][c] - '0';
				int b = (r / 3) * 3 + c / 3;

				if (rowHas[r, d] || colHas[c, d] || boxHas[b, d]) return false;
				rowHas[r, d] = colHas[c, d] = boxHas[b, d] = true;
			}
		}
		return true;
	}
}


class Program
{
	static void Main()
	{
		char[][] board =
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

		Console.WriteLine(new Solution().IsValidSudoku_BruteForce(board));   // o/p: True
		Console.WriteLine(new Solution().IsValidSudoku(board));              // o/p: True
	}
}

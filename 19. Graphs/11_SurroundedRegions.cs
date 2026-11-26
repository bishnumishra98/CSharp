// GFG: Replace O's with X's   --->   A matrix of 'O' and 'X'. Replace every group of 'O's (connected up / down / left /
// right) that is completely SURROUNDED by 'X' with 'X'. A group that touches the BORDER of the matrix is not surrounded.
// (Leetcode: 130. Surrounded Regions is the same problem.)

// Example 1:
// Input:  X X X X          Output: X X X X
//         X O X X                  X X X X
//         X O O X                  X X X X
//         X O X X                  X X X X
//         X X O O                  X X O O     (these two O's are on the border: they stay)

// Example 2:
// Input:  X O X X          Output: the same (every O group reaches the border)
//         X O X X
//         X O O X
//         X O X X
//         X X O O


using System;
using System.Linq;

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };

	// Algorithm (think in reverse): It is hard to check "is this group surrounded ?". It is easy to find the groups that
	// are NOT: they touch the border. So:
	// 1. Walk along the 4 borders. From every border 'O', DFS and mark its whole group as "safe".
	// 2. Every 'O' that is NOT safe can not reach the border, so it is surrounded: turn it into 'X'.
	// T.C: O(n * m)
	// S.C: O(n * m);   safe + recursion stack
	public char[][] Fill(char[][] mat)
	{
		int n = mat.Length, m = mat[0].Length;
		char[][] result = mat.Select(row => (char[])row.Clone()).ToArray();   // do not change the caller's matrix
		bool[,] safe = new bool[n, m];

		for (int c = 0; c < m; c++)
		{
			if (result[0][c] == 'O' && !safe[0, c]) Dfs(0, c, result, safe);              // top row
			if (result[n - 1][c] == 'O' && !safe[n - 1, c]) Dfs(n - 1, c, result, safe);  // bottom row
		}
		for (int r = 0; r < n; r++)
		{
			if (result[r][0] == 'O' && !safe[r, 0]) Dfs(r, 0, result, safe);              // left column
			if (result[r][m - 1] == 'O' && !safe[r, m - 1]) Dfs(r, m - 1, result, safe);  // right column
		}

		for (int r = 0; r < n; r++)
		{
			for (int c = 0; c < m; c++)
			{
				if (result[r][c] == 'O' && !safe[r, c]) result[r][c] = 'X';
			}
		}
		return result;
	}

	private void Dfs(int r, int c, char[][] mat, bool[,] safe)
	{
		safe[r, c] = true;
		for (int k = 0; k < 4; k++)
		{
			int nr = r + dr[k], nc = c + dc[k];
			if (nr >= 0 && nr < mat.Length && nc >= 0 && nc < mat[0].Length && mat[nr][nc] == 'O' && !safe[nr, nc])
			{
				Dfs(nr, nc, mat, safe);
			}
		}
	}
}


class Program
{
	static void Main()
	{
		char[][] mat =
		{
			"XXXX".ToCharArray(), "XOXX".ToCharArray(), "XOOX".ToCharArray(), "XOXX".ToCharArray(), "XXOO".ToCharArray()
		};

		var rows = new Solution().Fill(mat).Select(row => new string(row));
		Console.WriteLine(string.Join(" | ", rows));   // o/p: XXXX | XXXX | XXXX | XXXX | XXOO
	}
}

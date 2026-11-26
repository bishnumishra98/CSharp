// GFG: Rat in a Maze   --->   Consider a rat placed at position (0, 0) in an n x n square matrix maze[][]. The rat's
// goal is to reach the destination at position (n-1, n-1). The rat can move in four possible directions: 'U' (up),
// 'D' (down), 'L' (left), 'R' (right).
// The matrix contains only two possible values:
// • 0: A blocked cell through which the rat cannot travel.
// • 1: A free cell that the rat can pass through.
// Your task is to find all possible paths the rat can take to reach the destination, starting from (0, 0) and ending at
// (n-1, n-1), under the condition that the rat cannot revisit any cell along the same path. Furthermore, the rat can only
// move to adjacent cells that are within the bounds of the matrix and not blocked.
// If no path exists, return an empty list.
// Note: Return the final result vector in lexicographically smallest order.

// Example 1:
// Input: maze[][] = [[1, 0, 0, 0], [1, 1, 0, 1], [1, 1, 0, 0], [0, 1, 1, 1]]
// Output: ["DDRDRR", "DRDDRR"]
// Explanation: The rat can reach the destination at (3, 3) from (0, 0) by two paths - DRDDRR and DDRDRR, when printed
// in sorted order we get DDRDRR DRDDRR.

// Example 2:
// Input: maze[][] = [[1, 0], [1, 0]]
// Output: []
// Explanation: No path exists as the destination cell (1, 1) is blocked.

// Problem link: https://www.geeksforgeeks.org/problems/rat-in-a-maze-problem/1

// Algorithm: Backtracking.
// 1. From the current cell, try all 4 moves. Move only into a cell that is inside the maze, free (1), and not already
//    on the current path (visited).
// 2. Mark the current cell as visited before moving on, and unmark it after coming back (backtrack), so OTHER paths
//    can still use it.
// 3. When the rat reaches (n - 1, n - 1), save the path.
// Sorted order for free: Try the moves in alphabetical order D, L, R, U. Then paths are found in sorted order, and no
// sorting is needed at the end.


// Dry run image (in this folder): 14_RatInAMaze.jpg

using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
	// Version 1: Write the 4 moves one by one (easy to read).
	// T.C: O(3^(n^2));   at every cell, at most 3 new directions (it can't go back to the cell it came from)
	// S.C: O(n^2);   visited array + recursion depth (a path can visit every cell)
	public List<string> FindPath_Expanded(int[][] maze)
	{
		int n = maze.Length;
		List<string> ans = new List<string>();
		if (maze[0][0] == 0 || maze[n - 1][n - 1] == 0) return ans;

		SolveExpanded(0, 0, maze, new bool[n, n], new StringBuilder(), ans);
		return ans;
	}

	private void SolveExpanded(int i, int j, int[][] maze, bool[,] visited, StringBuilder path, List<string> ans)
	{
		int n = maze.Length;
		if (i == n - 1 && j == n - 1)
		{
			ans.Add(path.ToString());
			return;
		}

		visited[i, j] = true;

		if (i + 1 < n && maze[i + 1][j] == 1 && !visited[i + 1, j])   // Down
		{
			path.Append('D');
			SolveExpanded(i + 1, j, maze, visited, path, ans);
			path.Length--;
		}
		if (j - 1 >= 0 && maze[i][j - 1] == 1 && !visited[i, j - 1])  // Left
		{
			path.Append('L');
			SolveExpanded(i, j - 1, maze, visited, path, ans);
			path.Length--;
		}
		if (j + 1 < n && maze[i][j + 1] == 1 && !visited[i, j + 1])   // Right
		{
			path.Append('R');
			SolveExpanded(i, j + 1, maze, visited, path, ans);
			path.Length--;
		}
		if (i - 1 >= 0 && maze[i - 1][j] == 1 && !visited[i - 1, j])  // Up
		{
			path.Append('U');
			SolveExpanded(i - 1, j, maze, visited, path, ans);
			path.Length--;
		}

		visited[i, j] = false;   // backtrack
	}

// ------------------------------------------------------------------------------

	// Version 2: Same logic, but the 4 moves come from small arrays, so one loop handles all of them.
	//   Move:          D    L    R    U
	//   Row change:   +1    0    0   -1      (di)
	//   Col change:    0   -1   +1    0      (dj)
	// This "direction arrays" trick is used a lot in grid and graph problems.
	// T.C: O(3^(n^2))
	// S.C: O(n^2)
	public List<string> FindPath(int[][] maze)
	{
		int n = maze.Length;
		List<string> ans = new List<string>();
		if (maze[0][0] == 0 || maze[n - 1][n - 1] == 0) return ans;

		Solve(0, 0, maze, new bool[n, n], new StringBuilder(), ans);
		return ans;
	}

	private readonly int[] di = { 1, 0, 0, -1 };
	private readonly int[] dj = { 0, -1, 1, 0 };
	private const string dirs = "DLRU";

	private void Solve(int i, int j, int[][] maze, bool[,] visited, StringBuilder path, List<string> ans)
	{
		int n = maze.Length;
		if (i == n - 1 && j == n - 1)
		{
			ans.Add(path.ToString());
			return;
		}

		visited[i, j] = true;

		for (int d = 0; d < 4; d++)
		{
			int ni = i + di[d], nj = j + dj[d];
			if (ni >= 0 && nj >= 0 && ni < n && nj < n && maze[ni][nj] == 1 && !visited[ni, nj])
			{
				path.Append(dirs[d]);
				Solve(ni, nj, maze, visited, path, ans);
				path.Length--;
			}
		}

		visited[i, j] = false;   // backtrack
	}
}


class Program
{
	static void Main()
	{
		int[][] maze =
		{
			new int[] { 1, 0, 0, 0 },
			new int[] { 1, 1, 0, 1 },
			new int[] { 1, 1, 0, 0 },
			new int[] { 0, 1, 1, 1 }
		};

		Console.WriteLine(string.Join(" ", new Solution().FindPath_Expanded(maze)));   // o/p: DDRDRR DRDDRR
		Console.WriteLine(string.Join(" ", new Solution().FindPath(maze)));            // o/p: DDRDRR DRDDRR
	}
}

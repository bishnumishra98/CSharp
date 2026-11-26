// Leetcode: 733. Flood Fill   --->   image[i][j] is the colour of a pixel. Starting from pixel (sr, sc), recolour it and
// every pixel connected to it (4 directions: up, down, left, right) that has the SAME original colour, with 'color'.
// (Like the paint bucket tool in a drawing program.) Return the image.

// Example 1:
// Input: image = [[1,1,1],
//                 [1,1,0],
//                 [1,0,1]], sr = 1, sc = 1, color = 2
// Output: [[2,2,2],
//          [2,2,0],
//          [2,0,1]]
// Explanation: the bottom-right 1 is not connected (only diagonally), so it stays 1.

// Example 2:
// Input: image = [[0,0,0],[0,0,0]], sr = 0, sc = 0, color = 0
// Output: [[0,0,0],[0,0,0]]   (already the new colour)


using System;
using System.Linq;

public class Solution
{
	private static readonly int[] dr = { -1, 0, 1, 0 }, dc = { 0, 1, 0, -1 };   // up, right, down, left

	// Algorithm (DFS): Remember the starting colour. Paint the start pixel, then DFS into each of its 4 neighbours that
	// still has the starting colour. Painting a pixel also works as "visited": it no longer has the starting colour, so
	// it is never entered again. No visited array is needed.
	// Careful: if the new colour EQUALS the starting colour, painting does not change anything, so the DFS would go back
	// and forth forever. Return at once in that case.
	// T.C: O(n * m)
	// S.C: O(n * m);   recursion stack in the worst case (one long snake of pixels)
	public int[][] FloodFill(int[][] image, int sr, int sc, int color)
	{
		int start = image[sr][sc];
		if (start == color) return image;

		Dfs(sr, sc, start, color, image);
		return image;
	}

	private void Dfs(int r, int c, int start, int color, int[][] image)
	{
		image[r][c] = color;

		for (int d = 0; d < 4; d++)
		{
			int nr = r + dr[d], nc = c + dc[d];
			if (nr >= 0 && nr < image.Length && nc >= 0 && nc < image[0].Length && image[nr][nc] == start)
			{
				Dfs(nr, nc, start, color, image);
			}
		}
	}
}


class Program
{
	static void Main()
	{
		int[][] image = { new[] { 1, 1, 1 }, new[] { 1, 1, 0 }, new[] { 1, 0, 1 } };

		var rows = new Solution().FloodFill(image, 1, 1, 2).Select(row => string.Join(" ", row));
		Console.WriteLine(string.Join(" | ", rows));   // o/p: 2 2 2 | 2 2 0 | 2 0 1
	}
}

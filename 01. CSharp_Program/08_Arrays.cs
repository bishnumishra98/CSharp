// ● Arrays: An array is a data structure that stores a fixed-size sequential collection of elements of the same type.
//           In C#, arrays are zero-indexed, meaning the first element is at index 0, the second element is at index 1, and so on.
//           The common ways to declare and initialize arrays in C# are:
//           - Single-dimensional array: A single row of elements.
//             Example: int[] numbers = new int[5]; // Declares an array of 5 integers.
//           - Multi-dimensional array: An array with more than one dimension (e.g., a matrix).
//             Example: int[,] matrix = new int[3, 4]; // Declares a 2D array with 3 rows and 4 columns.
//           - Jagged array: An array of arrays, where each "row" can have a different length.
//	           Example: int[][] jaggedArray = new int[3][]; // Declares a jagged array with 3 rows, where each row can have a
//             different number of columns.

// Single-dimensional array example:
int[] numbers = new int[5];   // declares an array of 5 integers.

for(int i = 0; i < numbers.Length; i++)
{
	numbers[i] = i + 1;   // initializes the array with values 1 to 5.
}

for(int i = 0; i < numbers.Length; i++)
{
	Console.WriteLine("Element at index " + i + ": " + numbers[i]);   // prints the elements of the array.
}


// Multi-dimensional array example:
int[,] matrix = new int[3, 4];   // declares a 2D array with 3 rows and 4 columns.

for(int i = 0; i < 3; i++)
{
	for(int j = 0; j < 4; j++)
	{
		matrix[i, j] = (i + 1) * (j + 1);   // initializes the matrix with multiplication table values.
	}
}

for(int i = 0; i < 3; i++)
{
	for(int j = 0; j < 4; j++)
	{
		Console.WriteLine("Element at [" + i + ", " + j + "]: " + matrix[i, j]);   // prints the elements of the matrix.
	}
}


// Jagged array example:
int[][] jaggedArray = new int[3][];   // declares a jagged array with 3 rows.
jaggedArray[0] = new int[2];   // first row has 2 columns.
jaggedArray[1] = new int[3];   // second row has 3 columns.
jaggedArray[2] = new int[4];   // third row has 4 columns.

for(int i = 0; i < jaggedArray.Length; i++)
{
	for(int j = 0; j < jaggedArray[i].Length; j++)
	{
		jaggedArray[i][j] = (i + 1) * (j + 1);   // initializes the jagged array with multiplication table values.
	}
}

for(int i = 0; i < jaggedArray.Length; i++)
{
	for(int j = 0; j < jaggedArray[i].Length; j++)
	{
		Console.WriteLine("Element at [" + i + "][" + j + "]: " + jaggedArray[i][j]);   // prints the elements of the jagged array.
	}
}

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

// ● Single-dimensional array example:
int[] numbers = new int[5];   // declares an array of 5 integers.
int[] nums = {1, 2, 3, 4, 5};   // declaring and initializing array

for(int i = 0; i < numbers.Length; i++)
{
	numbers[i] = i + 1;   // initializes the array with values 1 to 5.
}

for(int i = 0; i < numbers.Length; i++)
{
	Console.WriteLine("Element at index " + i + ": " + numbers[i]);   // prints the elements of the array.
}


// ● Multi-dimensional array example:
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


// ● Jagged array example:
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


// ● Utility functions and properties of Array:
//
//   Note: Unlike List, most Array functions are static. So we write Array.Sort(arr), not arr.Sort().
//
//   I.   Core Properties:
//        - Length: Total number of elements in the array. (For a 2D array, it's rows * columns.)
//        - Rank: Number of dimensions. 1 for a normal array, 2 for a 2D array.
//        - GetLength(dimension): Size of one dimension. GetLength(0) = rows, GetLength(1) = columns.
//
//   II.  Searching & Checking elements:
//        - Array.IndexOf(arr, item): Index of the first occurrence of item. Returns -1 if not found.
//        - arr.Contains(item): True if item is in the array, else False.
//        - Array.Exists(arr, predicate): True if any element matches the condition.
//        - Array.Find(arr, predicate): Returns the first element that matches the condition.
//        - Array.FindIndex(arr, predicate): Returns the index of the first element that matches the condition.
//        - Array.FindAll(arr, predicate): Returns a new array of all elements that match the condition.
//        - Array.BinarySearch(arr, item): Fast search, but works only on a SORTED array. Returns the index.
//
//   III. Sorting & Ordering elements:
//        - Array.Sort(arr): Sorts the array in ascending order.
//        - Array.Sort(arr, comparison): Sorts using our own rule. e.g., descending order.
//        - Array.Reverse(arr): Reverses the order of the elements.
//
//   IV.  Max, Min, Sum, Average (from LINQ):
//        - arr.Max(), arr.Min(), arr.Sum(), arr.Average()
//
//   V.   Copying & Resizing:
//        - arr.Clone(): Makes a new copy of the array. Returns 'object', so cast it back: (int[])arr.Clone()
//        - Array.Copy(source, destination, length): Copies the first 'length' elements into another array.
//        - Array.Resize(ref arr, newSize): Array size is fixed, so this makes a NEW array of newSize, copies the
//          old elements into it, and makes 'arr' point to it. New empty places get the default value (0 for int).
//
//   VI.  Filling & Clearing:
//        - Array.Fill(arr, value): Puts the same value in every place of the array.
//        - Array.Clear(arr): Sets every element to its default value (0 for int). Length stays the same.
//
//   VII. Converting & Printing:
//        - arr.ToList(): Converts the array to a List<T>.
//        - string.Join(separator, arr): Joins all elements into one string, with separator in between.


int[] arr = { 5, 2, 8, 1, 9 };

// I. Core Properties
Console.WriteLine("Length of arr: " + arr.Length);   // o/p: "Length of arr: 5"
Console.WriteLine("Rank of arr: " + arr.Rank);   // o/p: "Rank of arr: 1"
Console.WriteLine("Rank of matrix: " + matrix.Rank);   // o/p: "Rank of matrix: 2"
Console.WriteLine("Rows in matrix: " + matrix.GetLength(0));   // o/p: "Rows in matrix: 3"
Console.WriteLine("Columns in matrix: " + matrix.GetLength(1));   // o/p: "Columns in matrix: 4"
Console.WriteLine("Length of matrix: " + matrix.Length);   // o/p: "Length of matrix: 12"   (3 * 4)

// II. Searching & Checking elements
Console.WriteLine("Index of 8: " + Array.IndexOf(arr, 8));   // o/p: "Index of 8: 2"
Console.WriteLine("Index of 100: " + Array.IndexOf(arr, 100));   // o/p: "Index of 100: -1"   (not found)
Console.WriteLine("Does arr contain 8 ? " + arr.Contains(8));   // o/p: "Does arr contain 8 ? True"
Console.WriteLine("Any number greater than 8 ? " + Array.Exists(arr, n => n > 8));   // o/p: "Any number greater than 8 ? True"
Console.WriteLine("First even number: " + Array.Find(arr, n => n % 2 == 0));   // o/p: "First even number: 2"
Console.WriteLine("Index of first number greater than 5: " + Array.FindIndex(arr, n => n > 5));   // o/p: "Index of first number greater than 5: 2"
Console.WriteLine("All numbers greater than 4: " + string.Join(", ", Array.FindAll(arr, n => n > 4)));   // o/p: "All numbers greater than 4: 5, 8, 9"

// III. Sorting & Ordering elements
Array.Sort(arr);
Console.WriteLine("After sorting: " + string.Join(", ", arr));   // o/p: "After sorting: 1, 2, 5, 8, 9"

Console.WriteLine("Index of 8 using BinarySearch: " + Array.BinarySearch(arr, 8));   // o/p: "Index of 8 using BinarySearch: 3"   (arr is sorted now)

Array.Sort(arr, (a, b) => b.CompareTo(a));   // (a, b) => b.CompareTo(a) is a lambda that sorts in descending order
Console.WriteLine("After sorting in descending order: " + string.Join(", ", arr));   // o/p: "After sorting in descending order: 9, 8, 5, 2, 1"

Array.Reverse(arr);
Console.WriteLine("After reversing: " + string.Join(", ", arr));   // o/p: "After reversing: 1, 2, 5, 8, 9"

// IV. Max, Min, Sum, Average
Console.WriteLine("Max: " + arr.Max());   // o/p: "Max: 9"
Console.WriteLine("Min: " + arr.Min());   // o/p: "Min: 1"
Console.WriteLine("Sum: " + arr.Sum());   // o/p: "Sum: 25"
Console.WriteLine("Average: " + arr.Average());   // o/p: "Average: 5"

// V. Copying & Resizing
int[] arrCopy = (int[])arr.Clone();
arrCopy[0] = 100;
Console.WriteLine("Original after changing the copy: " + string.Join(", ", arr));   // o/p: "Original after changing the copy: 1, 2, 5, 8, 9"   (original didn't change)
Console.WriteLine("Copy: " + string.Join(", ", arrCopy));   // o/p: "Copy: 100, 2, 5, 8, 9"

int[] firstThree = new int[3];
Array.Copy(arr, firstThree, 3);
Console.WriteLine("First three elements: " + string.Join(", ", firstThree));   // o/p: "First three elements: 1, 2, 5"

Array.Resize(ref arr, 7);
Console.WriteLine("After resizing to 7: " + string.Join(", ", arr));   // o/p: "After resizing to 7: 1, 2, 5, 8, 9, 0, 0"

// VI. Filling & Clearing
int[] sevens = new int[4];
Array.Fill(sevens, 7);
Console.WriteLine("After filling with 7: " + string.Join(", ", sevens));   // o/p: "After filling with 7: 7, 7, 7, 7"

// VII. Converting & Printing
List<int> arrList = arr.ToList();
arrList.Add(10);   // now we can add, because it's a List
Console.WriteLine("List made from arr: " + string.Join(", ", arrList));   // o/p: "List made from arr: 1, 2, 5, 8, 9, 0, 0, 10"

Array.Clear(arr);
Console.WriteLine("After clearing: " + string.Join(", ", arr));   // o/p: "After clearing: 0, 0, 0, 0, 0, 0, 0"   (Length is still 7)

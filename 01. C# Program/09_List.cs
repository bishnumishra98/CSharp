// ● List: A List is a collection that can dynamically resize itself and provides more functionality than arrays. In C#, the
//         List<T> class is part of the System.Collections.Generic namespace and allows you to store a list of objects of type T.
//         Lists are zero-indexed, similar to arrays.

// ● Ways to declare and initialize a List<T> in C#:
//   - Using the default constructor:
//	   List<int> numbers = new List<int>();   // declares an empty list of integers
//   - Using collection initializer:
//	   List<string> names = new List<string> { "Alice", "Bob", "Charlie" };   // declares and initializes a list of strings

// ● Utility functions and properties of List<T>:
//
//   I.   Core Properties:
//        - Count: Gets the number of elements contained in the List<T>.
//        - Capacity: Gets or sets the total number of elements the internal data structure can hold without resizing.
//                    Capacity of an empty list is 0.
//                    When one item is added to a list, the capacity is increased to 4.
//                    When the number of items exceeds the current capacity, the capacity is doubled.
//        - Item[index]: Gets or sets the element at the specified index in the List<T>.
//
//   II.  Adding & Inserting elements:
//        - Add(item): Adds an object to the end of the List<T>.
//        - AddRange(collection): Adds the elements of the specified collection to the end of the List<T>.
//		  - Insert(index, item): Inserts an element into the List<T> at the specified index.
//
//   III. Removing elements:
//        - Remove(item): Removes the first occurrence of a specific object from the List<T>.
//		  - RemoveAt(index): Removes the element at the specified index of the List<T>.
//        - RemoveAll(predicate): Removes all the elements that match the conditions defined by the specified predicate.
//
//   IV.  Searching & Checking elements:
//        - Contains(item): Determines whether an element is in the List<T>.
//        - IndexOf(item): Returns the zero-based index of the first occurrence of a specific object in the List<T>.
//        - Find(predicate): Searches for an element that matches the conditions defined by the specified predicate, and
//                           returns the first occurrence within the entire List<T>.
//        
//   V.   Sorting & Ordering elements:
//        - Sort(): Sorts the elements in the entire List<T> using the default comparer.
//        - Sort(comparison): Sorts the elements in the entire List<T> using the specified Comparison<T>.
//        - Reverse(): Reverses the order of the elements in the entire List<T>.
//
//   VI.  Iterating through the list:
//        - Using foreach loop: Iterates through each element in the List<T>.
//        - Using for loop: Iterates through each element in the List<T> using an index.
//
//   VIII. Converting to Array & List:
//        - ToArray(): Copies the elements of the List<T> to a new array.
//        - ToList(): Converts the array to a List<T>.
//
//   IX.   Clearing the list:
//        - Clear(): Removes all elements from the List<T>.
//
//   X.    String representation:
//        - string.Join(separator, list): Concatenates the elements of the List<T>, using the specified separator between
//          each element, and returns a single string.


List<int> numbers = new List<int>();

numbers.Add(1);
numbers.Add(2);
numbers.Add(3);

Console.WriteLine("Count of elements in the list: " + numbers.Count);   // o/p: "Count of elements in the list: 3"

Console.WriteLine("List after adding elements: " + string.Join(", ", numbers));   // o/p: "List after adding elements: 1, 2, 3"

numbers.Remove(2);

Console.WriteLine("List after removing an element: " + string.Join(", ", numbers));   // o/p: "List after removing an element: 1, 3"
Console.WriteLine("Item at index 0: " + numbers[0]);   // o/p: "Item at index 0: 1"
Console.WriteLine("Capacity of the list: " + numbers.Capacity);   // o/p: "Capacity of the list: 4"

numbers.AddRange(new List<int> { 4, 5, 6 });

Console.WriteLine("List after adding a range of elements: " + string.Join(", ", numbers));   // o/p: "List after adding a range of elements: 1, 3, 4, 5, 6"

numbers.Insert(1, 2);

Console.WriteLine("List after inserting an element at index 1: " + string.Join(", ", numbers));   // o/p: "List after inserting an element at index 1: 1, 2, 3, 4, 5, 6"

numbers.RemoveAt(3);

Console.WriteLine("List after removing an element at index 3: " + string.Join(", ", numbers));   // o/p: "List after removing an element at index 3: 1, 2, 3, 5, 6"

numbers.Remove(1);

Console.WriteLine("List after removing the first occurrence of 1: " + string.Join(", ", numbers));   // o/p: "List after removing the first occurrence of 1: 2, 3, 5, 6"

numbers.RemoveAll(n => n > 4);   // n => n > 4 is a lambda expression that defines the condition for removal (removes all elements greater than 4)

Console.WriteLine("List after removing all elements greater than 4: " + string.Join(", ", numbers));   // o/p: "List after removing all elements greater than 4: 2, 3"

Console.WriteLine("Does the list contain 2 ? " + numbers.Contains(2));   // o/p: "Does the list contain 2 ? True"

Console.WriteLine("Index of 3 in the list: " + numbers.IndexOf(3));   // o/p: "Index of 3 in the list: 1"

Console.WriteLine("First even number in the list: " + numbers.Find(n => n % 2 == 0));   // o/p: "First even number in the list: 2"

numbers.Reverse();

Console.WriteLine("List after reversing: " + string.Join(", ", numbers));   // o/p: "List after reversing: 3, 2"

numbers.Sort();

Console.WriteLine("List after sorting: " + string.Join(", ", numbers));   // o/p: "List after sorting: 2, 3"

foreach (var number in numbers)
{
	Console.WriteLine("Iterating through the list using foreach: " + number);   // o/p: "Iterating through the list using foreach: 2" and "Iterating through the list using foreach: 3"
}

for (int i = 0; i < numbers.Count; i++)
{
	Console.WriteLine("Iterating through the list using for loop: " + numbers[i]);   // o/p: "Iterating through the list using for loop: 2" and "Iterating through the list using for loop: 3"
}

List<int> numbers_copy = numbers.ToArray().ToList();   // converts the list to an array and then back to a list

Console.WriteLine("String representation of the list: " + string.Join(", ", numbers_copy));   // o/p: "String representation of the list: 2, 3"

numbers.Clear();

Console.WriteLine("List after clearing all elements: " + string.Join(", ", numbers));   // o/p: "List after clearing all elements: "

// ● List: A List is a collection that can dynamically resize itself and provides more functionality than arrays. In C#, the
//         List<T> class is part of the System.Collections.Generic namespace and allows you to store a list of objects of type T.
//         Lists are zero-indexed, similar to arrays.

// ● Common operations on List<T> include adding, removing, and accessing elements, as well as iterating through the list.

// ● Ways to declare and initialize a List<T> in C#:
//   - Using the default constructor:
//	   List<int> numbers = new List<int>();   // declares an empty list of integers
//   - Using collection initializer:
//	   List<string> names = new List<string> { "Alice", "Bob", "Charlie" };   // declares and initializes a list of strings

// ● Helpful functions and properties of List<T>:
//
//   I.   Core Properties:
//        - Count: Gets the number of elements contained in the List<T>.
//        - Capacity: Gets or sets the total number of elements the internal data structure can hold without resizing.
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
//   VIII. Converting to Array:
//        - ToArray(): Copies the elements of the List<T> to a new array.
//
//   IX.   Clearing the list:
//        - Clear(): Removes all elements from the List<T>.

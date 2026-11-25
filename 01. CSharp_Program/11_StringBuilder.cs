// ● Problem with string: Regular string objects in C# are immutable, meaning once created, their value
//                        cannot be changed. Whenever you concatenate or alter a string (using + or +=),
//                        the runtime allocates a brand-new string in memory and discards the old one.
//                        Doing this inside a large loop creates massive memory overhead and slows down
//                        your application due to frequent garbage collection.
//
//   Example:
//        string s = "";
//        for (int i = 0; i < 5; i++)
//        {
//            s += i;     // creates a new string every time: "0" -> "01" -> "012" -> "0123" -> "01234"
//        }
//
//   For 5 times it is fine. But for 1,00,000 times, lakhs of strings get created and copied, which is very slow
//   and wastes memory.

// ● Solution - StringBuilder: A StringBuilder is a "changeable" (mutable) string. It keeps the characters in one place
//                             and changes them directly. No new object is created on every change.
//   - It lives in the System.Text namespace.
//   - When we are done, we call ToString() to get the normal string back.

// ● When to use what:
//   - string: when the text does not change much (names, messages, small joins).
//   - StringBuilder: when we change/build text many times, specially inside a loop.

// ● Ways to create a StringBuilder:
//   - StringBuilder sb1 = new StringBuilder();            // empty
//   - StringBuilder sb2 = new StringBuilder("Hello");     // starts with "Hello"
//   - StringBuilder sb3 = new StringBuilder(50);          // empty, but has room for 50 characters (capacity)

// ● Utility functions and properties of StringBuilder:
//
//   I.   Core Properties:
//        - Length: Number of characters currently in it. (Can also be set, e.g., sb.Length = 0 empties it.)
//        - Capacity: How many characters it can hold before it needs to grow. It grows by itself when needed.
//        - sb[index]: Gets or sets the character at the given index. (This is NOT allowed in a string.)
//
//   II.  Adding:
//        - Append(value): Adds the value at the end. Works with string, char, int, bool, etc.
//        - AppendLine(value): Adds the value at the end, followed by a new line.
//        - AppendFormat(format, args): Adds a formatted string at the end, like string.Format().
//
//   III. Changing:
//        - Insert(index, value): Inserts the value at the given index.
//        - Remove(startIndex, count): Removes 'count' characters starting from 'startIndex'.
//        - Replace(oldValue, newValue): Replaces all occurrences of oldValue with newValue.
//        - Clear(): Removes all characters.
//
//   IV.  Converting back:
//        - ToString(): Returns the final normal string.
//
//   NOTE: Append, Insert, Remove, Replace return the same StringBuilder, so we can chain them:
//         sb.Append("a").Append("b").Append("c");

using System.Text;

StringBuilder sb = new StringBuilder();

sb.Append("Hello");
sb.Append(' ');
sb.Append("World");
sb.Append(2026);

Console.WriteLine(sb.ToString());   // o/p: "Hello World2026"

Console.WriteLine($"Length: {sb.Length}");   // o/p: "Length: 15"

sb[0] = 'J';   // changing a character directly (not possible in a string)

Console.WriteLine(sb.ToString());   // o/p: "Jello World2026"

sb.Insert(5, ",");   // inserts "," at index 5

Console.WriteLine(sb.ToString());   // o/p: "Jello, World2026"

sb.Remove(12, 4);   // removes 4 characters starting from index 12

Console.WriteLine(sb.ToString());   // o/p: "Jello, World"

sb.Replace("World", "C#");   // replaces "World" with "C#"

Console.WriteLine(sb.ToString());   // o/p: "Jello, C#"

sb.Clear();   // empties it

Console.WriteLine($"After Clear, Length: {sb.Length}");   // o/p: "After Clear, Length: 0"

sb.Append("a").Append("b").Append("c");   // chaining

Console.WriteLine(sb.ToString());   // o/p: "abc"

StringBuilder lines = new StringBuilder();

lines.AppendLine("Line 1");
lines.AppendLine("Line 2");
lines.AppendFormat("Name: {0}, Age: {1}", "Vishnu", 25);

Console.WriteLine(lines.ToString());
// o/p:
// Line 1
// Line 2
// Name: Vishnu, Age: 25

// Building a string in a loop: the main use of StringBuilder
StringBuilder numbers = new StringBuilder();

for (int i = 0; i < 5; i++)
{
	numbers.Append(i);   // no new string is created here
}

string result = numbers.ToString();   // convert to a normal string only once, at the end

Console.WriteLine(result);   // o/p: "01234"

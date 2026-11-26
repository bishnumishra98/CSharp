// ● String: A sequence of characters enclosed in double quotes. Strings are immutable. Once a string object is created in memory,
//           its value cannot be changed.

// ● Simplest way to declare a string in C# is using string literal:
//   Example: string greeting = "Hello, World!";   // declares and initializes a string variable

// ● Ways to create a string:
//   I.   Empty string (length 0):
//        - string s1 = "";              // empty double quotes
//        - string s2 = string.Empty;    // same as "", just more readable
//   II.  null string (no string at all):
//        - string? s3 = null;           // variable points to nothing. s3.Length will crash (NullReferenceException).
//        - NOTE: "" is an empty box, null means there is no box.
//   III. String of length 'x' filled with one character:
//        - new string(ch, x)            // repeats character 'ch', 'x' times
//        - string spaces = new string(' ', 5);   // "     " (5 spaces)
//        - string stars  = new string('*', 3);   // "***"
//        - NOTE: A string can't have "nothing" inside it at a position, so a string of length x must be filled with
//                some character. Use ' ' (space) or '\0' (null character) if you just want a blank string of length x.
//   IV.  From a char array:
//        - new string(charArray)        // joins all characters of the array into a string
//        - char [] letters = { 'C', 'a', 't' };  string word = new string(letters);   // "Cat"
//        - Useful when we want to change characters: string -> char [] -> change -> back to string.

// ● Concatenation (joining strings):
//   I.   Using + operator: "Hello" + " " + "World"   gives "Hello World"
//   II.  Using += operator: s += "World";   same as s = s + "World";
//   III. Using string.Concat(s1, s2): "Hello" + "World" in function form.
//   IV.  Using interpolation: $"{s1} {s2}"   (easiest to read when mixing many values)
//   NOTE: + also works with numbers: "Age: " + 25 gives "Age: 25". But be careful: 1 + 2 + "a" gives "3a"
//         (numbers are added first), while "a" + 1 + 2 gives "a12" (once a string comes, everything is joined).

// ● Adding a character to a string:
//   I.   At the end: s + 'c'  or  s += 'c';
//   II.  At the start: 'c' + s
//   III. At an index: s.Insert(index, "c")   (Insert takes a string, so use "c" or ch.ToString())
//   NOTE: Strings are immutable, so all of these create a NEW string. The old one is not changed.
//         Doing this many times in a loop is slow. Use StringBuilder for that (see 11_StringBuilder.cs).
//   CAREFUL: 'a' + 'b' is NOT "ab". A char is a number inside (its code), so 'a' + 'b' = 97 + 98 = 195.
//            Use "" + 'a' + 'b' or string.Concat('a', 'b') to get "ab".

// ● Utility functions and properties of string:
//
//   I.   Core Properties:
//		  - Length: Gets the number of characters in the string.
//        - string.IsNullOrEmpty(str): Checks if the string is null or empty("") and returns a boolean value.
//        - string.IsNullOrWhiteSpace(str): Checks if the string is null, empty(""), or consists only of white-space characters and returns a boolean.
//
//   II.  Searching and Comparison:
//        - Contains(substring): Checks if the string contains the specified substring and returns a boolean value.
//        - StartsWith(prefix): Checks if the string starts with the specified prefix and returns a boolean value.
//        - EndsWith(suffix): Checks if the string ends with the specified suffix and returns a boolean value.
//        - IndexOf(substring): Returns the zero-based index of the first occurrence of the specified substring within the string.
//        - LastIndexOf(substring): Returns the zero-based index of the last occurrence of the specified substring within the string.
//        - string.Equals(str1, str2): Compares two strings for equality and returns a boolean value.
//
//   III. Extraction and Manipulation:
//        - Substring(startIndex, length): Returns a substring that starts at the specified index and has the specified length.
//        - Replace(oldValue, newValue): Returns a new string in which all occurrences of a specified string in the current instance
//                                       are replaced with another specified string.
//        - Split(separator): Splits the string into an array of substrings based on the specified separator.
//        - ToCharArray(): Converts the string into an array of characters (char []).
//        - ToString(): Returns the string representation of the current instance.
//        - Parse(str): Converts the string representation of a number to its numeric equivalent (e.g., int.Parse, double.Parse).
//                      Throws an exception if the string is not a valid number, or if the number doesn't fit (like C++ stoi()).
//        - TryParse(str, out result): Same as Parse, but never throws. Returns true/false, and puts the number in 'result'
//                                     (0 if it failed). Safer, so use it for user input.
//          Note: Both need the WHOLE string to be a number (spaces around it are fine). So "42abc" fails. C++ atoi()/stoi()
//                are different: they stop at 'a' and give 42.
//
//   IV.  Modification and Formatting:
//        - ToUpper(): Converts the string to uppercase.
//        - ToLower(): Converts the string to lowercase.
//        - Trim(): Removes all leading and trailing white-space characters from the string.
//        - TrimStart(): Removes all leading white-space characters from the string.
//        - TrimEnd(): Removes all trailing white-space characters from the string.
//        - Insert(startIndex, value): Inserts a string into the current string at the specified index.
//        - Remove(startIndex, count): Removes a specified number of characters from the current string, starting at the specified index.
//        - string.Join(separator, values): Concatenates the elements of a specified array or collection, using the specified separator
//                                          between each element, and returns a single string.
//        - string.Format(format, args): Replaces the format items in a specified string with the string representation of specified objects
//                                       and returns a new string.
//        - string.Concat(str1, str2): Concatenates two strings and returns a new string.
//        - string.Compare(str1, str2): Compares two strings and returns an integer that indicates their relative position in the sort order.
//                                      Returns 0 if they are equal, a negative number if str1 is less than str2, and a positive number if
//                                      str1 is greater than str2.
//
//   V.   Iterating through the string:
//        - Using foreach loop: Iterates through each character in the string.
//        - Using for loop: Iterates through each character in the string using an index.
//
//   VI.  String Interpolation:
//        - $"Hello, {name}!" : Embeds expressions inside string literals, allowing for easier and more readable string formatting.

string empty1 = "";
string empty2 = string.Empty;

Console.WriteLine($"Length of empty1: {empty1.Length}, Length of empty2: {empty2.Length}");   // o/p: "Length of empty1: 0, Length of empty2: 0"
Console.WriteLine($"Are \"\" and string.Empty equal? {empty1 == empty2}");   // o/p: "Are "" and string.Empty equal? True"

string? nothing = null;

Console.WriteLine($"Is null string null or empty? {string.IsNullOrEmpty(nothing)}");   // o/p: "Is null string null or empty? True"

string spaces = new string(' ', 5);   // string of length 5, filled with spaces
string stars = new string('*', 3);   // string of length 3, filled with '*'

Console.WriteLine($"spaces: '{spaces}', Length: {spaces.Length}");   // o/p: "spaces: '     ', Length: 5"
Console.WriteLine($"stars: '{stars}', Length: {stars.Length}");   // o/p: "stars: '***', Length: 3"

char [] letters = { 'C', 'a', 't' };
string word = new string(letters);   // char array -> string

Console.WriteLine($"String made from char array: '{word}'");   // o/p: "String made from char array: 'Cat'"

string first = "Hello";
string second = "World";

string joined1 = first + " " + second;   // using +
Console.WriteLine(joined1);   // o/p: "Hello World"

string joined2 = "Hello";
joined2 += " World";   // using +=
Console.WriteLine(joined2);   // o/p: "Hello World"

string joined3 = string.Concat(first, " ", second);   // using string.Concat
Console.WriteLine(joined3);   // o/p: "Hello World"

string joined4 = $"{first} {second}";   // using interpolation
Console.WriteLine(joined4);   // o/p: "Hello World"

Console.WriteLine("Age: " + 25);   // o/p: "Age: 25"
Console.WriteLine(1 + 2 + "a");   // o/p: "3a"
Console.WriteLine("a" + 1 + 2);   // o/p: "a12"

string pet = "Ca";

pet += 't';   // add character at the end
Console.WriteLine(pet);   // o/p: "Cat"

pet = 'A' + pet;   // add character at the start
Console.WriteLine(pet);   // o/p: "ACat"

pet = pet.Insert(1, "-");   // add character at index 1
Console.WriteLine(pet);   // o/p: "A-Cat"

Console.WriteLine('a' + 'b');   // o/p: "195"   (char + char = number, not a string)
Console.WriteLine("" + 'a' + 'b');   // o/p: "ab"

string name = "Vishnu";

Console.WriteLine($"Hello, {name}!");   // o/p: "Hello, Vishnu!"
Console.WriteLine("Hello, " + name + "!");  // o/p: "Hello, Vishnu!"

Console.WriteLine($"Length of the string '{name}' is: {name.Length}");   // o/p: "Length of the string 'Vishnu' is: 6"

Console.WriteLine($"Is the string '{name}' null or empty? {string.IsNullOrEmpty(name)}");   // o/p: "Is the string 'Vishnu' null or empty? False"

Console.WriteLine($"Is the string '{name}' null, empty, or whitespace? {string.IsNullOrWhiteSpace(name)}");   // o/p: "Is the string 'Vishnu' null, empty, or whitespace? False"

Console.WriteLine($"Does the string '{name}' contain 'Vish'? {name.Contains("Vish")}");   // o/p: "Does the string 'Vishnu' contain 'Vish'? True"

Console.WriteLine($"Does the string '{name}' start with 'Vi'? {name.StartsWith("Vi")}");   // o/p: "Does the string 'Vishnu' start with 'Vi'? True"

Console.WriteLine($"Does the string '{name}' end with 'nu'? {name.EndsWith("nu")}");   // o/p: "Does the string 'Vishnu' end with 'nu'? True"

Console.WriteLine($"Index of 'sh' in the string '{name}' is: {name.IndexOf("sh")}");   // o/p: "Index of 'sh' in the string 'Vishnu' is: 2"

Console.WriteLine($"Last index of 'n' in the string '{name}' is: {name.LastIndexOf("n")}");   // o/p: "Last index of 'n' in the string 'Vishnu' is: 4"

string name2 = "Vishnu";

Console.WriteLine($"Are the strings '{name}' and '{name2}' equal? {string.Equals(name, name2)}");   // o/p: "Are the strings 'Vishnu' and 'Vishnu' equal? True"

string substring = name.Substring(1, 3);   // extracts a substring starting at index 1 with length 3

Console.WriteLine($"Substring of '{name}' starting at index 1 with length 3 is: '{substring}'");   // o/p: "Substring of 'Vishnu' starting at index 1 with length 3 is: 'ish'"

string replacedString = name.Replace("Vish", "Nish");   // replaces "Vish" with "Nish"

Console.WriteLine($"String after replacing 'Vish' with 'Nish' in '{name}' is: '{replacedString}'");   // o/p: "String after replacing 'Vish' with 'Nish' in 'Vishnu' is: 'Nishnu'"

string [] splitString = name.Split('s');   // splits the string at 's'

Console.WriteLine($"String '{name}' after splitting at 's' is: [{string.Join(", ", splitString)}]");   // o/p: "String 'Vishnu' after splitting at 's' is: [Vi, hnu]"

char [] charArray = name.ToCharArray();   // converts the string to a character array

Console.WriteLine($"Character array of the string '{name}' is: [{string.Join(", ", charArray)}]");   // o/p: "Character array of the string 'Vishnu' is: [V, i, s, h, n, u]"

int num = 12345;
string numString = num.ToString();   // converts the integer to a string

Console.WriteLine($"String representation of the integer {num} is: '{numString}'");   // o/p: "String representation of the integer 12345 is: '12345'"

int parsedNum = int.Parse(numString);   // converts the string back to an integer

Console.WriteLine($"Parsed integer from the string '{numString}' is: {parsedNum}");   // o/p: "Parsed integer from the string '12345' is: 12345"

bool isNumber = int.TryParse("42abc", out int tryParsedNum);   // "42abc" is not a whole number, so it fails (no exception)
// int.Parse("42abc");   // Error: FormatException

Console.WriteLine($"TryParse of '42abc' worked? {isNumber}, value: {tryParsedNum}");   // o/p: "TryParse of '42abc' worked? False, value: 0"

Console.WriteLine($"String '{name}' in uppercase is: '{name.ToUpper()}'");   // o/p: "String 'Vishnu' in uppercase is: 'VISHNU'"

Console.WriteLine($"String '{name}' in lowercase is: '{name.ToLower()}'");   // o/p: "String 'Vishnu' in lowercase is: 'vishnu'"

string str = "   Hello, World!   ";

Console.WriteLine($"String '{str}' after trimming whitespace is: '{str.Trim()}'");   // o/p: "String '   Hello, World!   ' after trimming whitespace is: 'Hello, World!'"

Console.WriteLine($"String '{str}' after trimming leading whitespace is: '{str.TrimStart()}'");   // o/p: "String '   Hello, World!   ' after trimming leading whitespace is: 'Hello, World!   '"

Console.WriteLine($"String '{str}' after trimming trailing whitespace is: '{str.TrimEnd()}'");   // o/p: "String '   Hello, World!   ' after trimming trailing whitespace is: '   Hello, World!'"

string insertedString = name.Insert(3, "XYZ");   // inserts "XYZ" at index 3

Console.WriteLine($"String after inserting 'XYZ' at index 3 in '{name}' is: '{insertedString}'");   // o/p: "String after inserting 'XYZ' at index 3 in 'Vishnu' is: 'VisXYZhnu'"

string removedString = name.Remove(2, 3);   // removes 3 characters starting from index 2

Console.WriteLine($"String after removing 3 characters starting from index 2 in '{name}' is: '{removedString}'");   // o/p: "String after removing 3 characters starting from index 2 in 'Vishnu' is: 'Vinu'"

string joinedString = string.Join("-", "Hello", "World", "C#");   // joins the strings with a hyphen

Console.WriteLine($"Joined string with hyphen is: '{joinedString}'");   // o/p: "Joined string with hyphen is: 'Hello-World-C#'"

string formattedString = string.Format("Name: {0}, Age: {1}", "Vishnu", 25);   // formats the string with placeholders

Console.WriteLine($"Formatted string is: '{formattedString}'");   // o/p: "Formatted string is: 'Name: Vishnu, Age: 25'"

string concatenatedString = string.Concat("Hello", " ", "World");   // concatenates the strings

Console.WriteLine($"Concatenated string is: '{concatenatedString}'");   // o/p: "Concatenated string is: 'Hello World'"

int comparisonResult1 = string.Compare("appleeee", "apple");   // compares two strings
int comparisonResult2 = string.Compare("apple", "apple");   // compares two strings
int comparisonResult3 = string.Compare("apple", "appleeee");   // compares two strings

Console.WriteLine($"Comparison result between 'appleeee' and 'apple' is: {comparisonResult1}");   // o/p: "Comparison result between 'appleeee' and 'apple' is: 1"
Console.WriteLine($"Comparison result between 'apple' and 'apple' is: {comparisonResult2}");   // o/p: "Comparison result between 'apple' and 'apple' is: 0"
Console.WriteLine($"Comparison result between 'apple' and 'appleeee' is: {comparisonResult3}");   // o/p: "Comparison result between 'apple' and 'appleeee' is: -1"

for (int i = 0; i < name.Length; i++)
{
	Console.WriteLine($"Character at index {i} in '{name}' is: '{name[i]}'");   // o/p: "Character at index 0 in 'Vishnu' is: 'V'" and so on
}

foreach (char c in name)
{
	Console.WriteLine($"Iterating through the string '{name}' using foreach: '{c}'");   // o/p: "Iterating through the string 'Vishnu' using foreach: 'V'" and so on
}

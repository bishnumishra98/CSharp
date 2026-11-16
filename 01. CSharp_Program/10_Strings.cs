// ● String: A sequence of characters enclosed in double quotes.

// ● Simplest way to declare a string in C# is using string literal:
//   Example: string greeting = "Hello, World!";   // declares and initializes a string variable

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


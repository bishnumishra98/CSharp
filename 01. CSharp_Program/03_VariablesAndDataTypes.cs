// ● Variables are used to store data values.

// ● Data types specify the type of data that a variable can hold. C# is a statically-typed language, which means that the data type of a
//   variable must be declared at the time of its creation.

// ● C# provides several built-in data types, including:
//   - int: Represents a 32-bit signed integer. Size = 4 bytes. Range: -2,147,483,648 to 2,147,483,647.
//   - double: Represents a double-precision floating-point number. Size = 8 bytes. Range: ±5.0 × 10^−324 to ±1.7 × 10^308.
//   - char: Represents a single 16-bit Unicode character. Size = 2 bytes. Range: U+0000 to U+FFFF.
//   - string: Represents a sequence of characters. Size = 2 bytes per character. Range: U+0000 to U+FFFF for each character.
//   - bool: Represents a Boolean value (true or false). Size = 1 byte. Range: true or false.

// ● C# also provides other data types like float, decimal, long, short, byte, sbyte, uint, ulong, ushort, object, dynamic, etc.

string name = "Vishnu";
Console.WriteLine("Hello, " + name + "!");   // o/p: "Hello, Vishnu!"

int x = 10, y = 15;
int sum = x + y;
Console.WriteLine("Sum of " + x + " and " + y + " is: " + sum);   // o/p: "Sum of 10 and 15 is: 25"

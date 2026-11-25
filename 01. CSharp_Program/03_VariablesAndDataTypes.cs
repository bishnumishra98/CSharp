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

// ● var: Lets the compiler find the data type by looking at the value on the right side.
//   - var age = 25;          // compiler makes it: int age = 25;
//   - var city = "Delhi";    // compiler makes it: string city = "Delhi";
//   - It is still statically-typed. The type is fixed at compile time and can't change later.
//        var count = 10;
//        count = "ten";      // Error: count is an int, a string can't be stored in it
//   - Rules:
//     I.   Must be given a value in the same line:   var a;        // Error (compiler can't guess the type)
//     II.  Can't be given null alone:                var b = null; // Error (null has no type)
//     III. Only for local variables (inside methods). Can't be used for class fields, method parameters or return types.
//   - When to use: when the type is already clear from the right side, or the type name is very long.
//        var numbers = new List<int>();   // better than: List<int> numbers = new List<int>();
//        var rich = employees.Where(e => e.Salary > 1000000);   // LINQ results have long types (see 04. LINQ)
//   - When NOT to use: when the type is not clear by reading, e.g. var result = GetData();   // what type is result?

// ● dynamic: A variable whose type is decided while the program is RUNNING, not at compile time.
//   - The compiler does NOT check it. It trusts us and skips all type checking for that variable.
//   - It can hold any value, and the value can change to a different type later:
//        dynamic d = 10;     // now d is an int
//        d = "ten";          // now d is a string (allowed)
//   - It can be set to null, even at the start (var can't do this):
//        dynamic n = null;   // allowed. n holds nothing for now.
//        n = 50;             // later it can get any value
//        NOTE: Using a member on it while it's null crashes: n = null; n.Length;   // RuntimeBinderException
//   - We can call any method or property on it. The compiler won't complain, even if it doesn't exist:
//        dynamic s = "Hello";
//        Console.WriteLine(s.Length);   // works: 5
//        s.Fly();                       // compiles fine, but CRASHES at run time (string has no Fly method)
//   - No IntelliSense (auto-complete) for it in the editor, because the type is not known yet.
//   - It is a bit slower, because the type has to be checked while running.
//   - Used in rare cases: reading JSON whose shape we don't know, working with COM objects (Excel, Word),
//     or talking to dynamic languages like Python.

// ● var vs dynamic:
//   - var: type is decided at COMPILE time, and fixed forever. Mistakes are caught before running.
//   - dynamic: type is decided at RUN time, and can change. Mistakes are found only while running (program crashes).
//   - Prefer var. Use dynamic only when the type truly can't be known before running.

string name = "Vishnu";
Console.WriteLine("Hello, " + name + "!");   // o/p: "Hello, Vishnu!"

int x = 10, y = 15;
int sum = x + y;
Console.WriteLine("Sum of " + x + " and " + y + " is: " + sum);   // o/p: "Sum of 10 and 15 is: 25"

var age = 25;
var city = "Delhi";
var price = 99.5;
var grade = 'A';
var isPassed = true;

// GetType() tells the real type that the compiler chose
Console.WriteLine(age.GetType());        // o/p: "System.Int32"    (int)
Console.WriteLine(city.GetType());       // o/p: "System.String"   (string)
Console.WriteLine(price.GetType());      // o/p: "System.Double"   (double)
Console.WriteLine(grade.GetType());      // o/p: "System.Char"     (char)
Console.WriteLine(isPassed.GetType());   // o/p: "System.Boolean"  (bool)

age = 30;          // fine: still an int
Console.WriteLine(age);   // o/p: 30
// age = "thirty"; // Error: can't store a string in an int

dynamic d = 10;
Console.WriteLine(d.GetType());   // o/p: "System.Int32"
d = "ten";                        // allowed with dynamic
Console.WriteLine(d.GetType());   // o/p: "System.String"
Console.WriteLine(d.Length);      // o/p: 3

try
{
	d.Fly();   // compiler allowed it, but string has no Fly() method
}
catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
{
	Console.WriteLine(ex.Message);   // o/p: "'string' does not contain a definition for 'Fly'"
}

dynamic n = null;                 // allowed with dynamic (var n = null; gives an error)
Console.WriteLine(n == null);     // o/p: True
n = 50;                           // later it can hold any value
Console.WriteLine(n);             // o/p: 50

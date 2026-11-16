// ● null means "no value".
//   Reference types (string, arrays, classes) can store null.
//   Value types (int, double, bool, etc.) can NOT store null.
//
// ● Nullable Type: Add '?' after a value type to allow null. e.g., int?, double?, bool?
//   Use it when "no value" is possible. e.g., marks of an exam not taken yet (0 is wrong, because 0 is also a valid mark).

// int marks = null;   // ❌ Error: int cannot be null
int? marks = null;     // ✅ int? can store any int value + null


// ● HasValue: true if it has a value, false if it is null.
// ● Value: Gives the actual value. Crashes if it is null, so check HasValue first.
int? age = 25;

if (age.HasValue)
	Console.WriteLine("Age: " + age.Value);   // o/p: "Age: 25"


// ● ?? (Null-Coalescing): "If the left side is null, use the right side."
int finalMarks = marks ?? 0;
Console.WriteLine(finalMarks);   // o/p: 0


// ● ?. (Null-Conditional): "If the object is null, return null instead of crashing."
string? name = null;
// int len = name.Length;          // ❌ Crash: NullReferenceException
int len = name?.Length ?? 0;       // ✅ name is null, so name?.Length is null, and ?? gives 0
Console.WriteLine(len);            // o/p: 0


// ● string? : Tells the compiler this string can be null. The compiler then warns you if you use it without
//   checking for null. This helps you avoid crashes before even running the program.

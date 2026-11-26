// ● Exception: An error that happens while the program is RUNNING (not a compile error). If nobody handles it,
//              the program crashes, and the lines after it never run.
//
//   Common exceptions:
//   Exception                     | When it happens
//   DivideByZeroException         | 10 / 0   (only for int. 10.0 / 0 gives Infinity, no error)
//   NullReferenceException        | Using something that is null, e.g. name.Length when name is null
//   IndexOutOfRangeException      | arr[5] when the array has only 3 items
//   ArgumentOutOfRangeException   | list[5] when the List has only 3 items
//   FormatException               | int.Parse("abc")
//   OverflowException             | Value too big for the type (inside 'checked'), e.g. int.MaxValue + 1
//   InvalidCastException          | Wrong type conversion, e.g. (string)obj when obj is an int
//   KeyNotFoundException          | dict["x"] when key "x" is not in the Dictionary
//   InvalidOperationException     | Wrong time to call, e.g. First() on an empty list
//   ArgumentException             | Wrong value given to a method (ArgumentNullException if null was given)

// ● Exception Handling: Catching the error and handling it, so the program does not crash.
//   I.   try     : Code that may throw an error.
//   II.  catch   : Runs only if an error happens in try. 'ex' has the details:
//                  ex.Message (what went wrong), ex.GetType().Name (which exception), ex.StackTrace (where it happened).
//   III. finally : ALWAYS runs, error or no error (even after 'return'). Used for cleanup (close file, DB connection).
//   IV.  throw   : Throw an error ourselves when something is wrong, e.g. age is negative.
//
//   try
//   {
//       // risky code
//   }
//   catch (FormatException ex)     // specific error first
//   {
//   }
//   catch (Exception ex)           // catches everything, so keep it LAST
//   {
//   }
//   finally
//   {
//       // cleanup
//   }
//
//   - Once an error happens in try, the rest of try is skipped, and C# jumps to the matching catch.
//   - Only ONE catch runs: the first one that matches, from top to bottom.
//   - Putting catch (Exception) before a specific catch is a compile error, because the specific one could never run.
//   - try must have at least one catch or a finally.

// ● How an exception travels (propagation):
//   If a method does not catch an error, the error goes back to the method that called it, then to its caller,
//   and so on. The first try-catch on the way catches it. If no one catches it, the program crashes.
//        Main() -> PlaceOrder() -> Pay()  [error here]
//        Pay has no catch -> goes to PlaceOrder -> no catch -> goes to Main -> Main's catch handles it.

// ● Exception filter (when): Catch only if a condition is true.
//   catch (ArgumentException ex) when (ex.ParamName == "age") { ... }

// ● Rethrowing: Sometimes we catch an error just to log it, and then pass it on.
//   - throw;      : Rethrows the SAME error. Keeps the original stack trace (where it really happened). Use this.
//   - throw ex;   : Resets the stack trace to this line. We lose the real place of the error. Avoid it.

// ● Throw shortcuts:
//   - throw expression : string city = input ?? throw new ArgumentNullException(nameof(input));
//   - ThrowIfNull      : ArgumentNullException.ThrowIfNull(input);   // throws if input is null (.NET 6+)
//   - nameof(x)        : gives the variable's name as a string ("x"). Safer than typing "x" by hand.

// ● using: Automatic cleanup. Works like try-finally that calls Dispose() for us. (Dispose = "close and free it")
//   using (StreamWriter writer = new StreamWriter("log.txt"))
//   {
//       writer.WriteLine("Hi");
//   }   // file is closed here, even if an error happened inside
//
//   Short form (C# 8+): using StreamWriter writer = new StreamWriter("log.txt");   // closed at the end of the block

// ● Custom exceptions (our own exception classes): see 02. Object Oriented Programming/17_ExceptionHandling.cs

// ● Best practices:
//   1. Catch specific exceptions. Use catch (Exception) only at the top level, e.g. to log and show "Something went wrong".
//   2. Don't use exceptions for normal checks. They are slow. Prefer:
//        int.TryParse(input, out int n)   instead of   try { int.Parse(input) } catch { }
//        dict.TryGetValue(key, out var v) instead of   try { dict[key] } catch { }
//        if (name != null)                instead of   catching NullReferenceException
//   3. Never leave catch empty: catch { }. The error gets hidden and bugs become very hard to find.
//   4. Use 'throw;' (not 'throw ex;') to rethrow.
//   5. Use finally or using for cleanup.

// ---------------- 1. try-catch-finally ----------------

try
{
	int a = 10, b = 0;
	Console.WriteLine(a / b);
	Console.WriteLine("This line will not run");   // skipped, because the line above threw
}
catch (DivideByZeroException ex)
{
	Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");   // o/p: "DivideByZeroException: Attempted to divide by zero."
}
finally
{
	Console.WriteLine("finally always runs");   // o/p: "finally always runs"
}

// ---------------- 2. Multiple catch blocks (only the first match runs) ----------------

string[] inputs = { "abc", "99999999999", "25" };

foreach (string input in inputs)
{
	try
	{
		int number = int.Parse(input);
		Console.WriteLine($"Parsed: {number}");
	}
	catch (FormatException)
	{
		Console.WriteLine($"'{input}' is not a number");
	}
	catch (OverflowException)
	{
		Console.WriteLine($"'{input}' is too big for int");
	}
	catch (Exception ex)   // anything else. Always last.
	{
		Console.WriteLine($"Something went wrong: {ex.Message}");
	}
}
// o/p:
// 'abc' is not a number
// '99999999999' is too big for int
// Parsed: 25

// ---------------- 3. Common exceptions and their messages ----------------

try { int[] arr = { 1, 2, 3 }; Console.WriteLine(arr[5]); }
catch (IndexOutOfRangeException ex) { Console.WriteLine(ex.Message); }   // o/p: "Index was outside the bounds of the array."

try { string? name = null; Console.WriteLine(name!.Length); }
catch (NullReferenceException ex) { Console.WriteLine(ex.Message); }   // o/p: "Object reference not set to an instance of an object."

try { var ages = new Dictionary<string, int>(); Console.WriteLine(ages["Amit"]); }
catch (KeyNotFoundException ex) { Console.WriteLine(ex.Message); }   // o/p: "The given key 'Amit' was not present in the dictionary."

try { object obj = 10; string s = (string)obj; }
catch (InvalidCastException ex) { Console.WriteLine(ex.GetType().Name); }   // o/p: "InvalidCastException"

try { int big = int.MaxValue; int result = checked(big + 1); }
catch (OverflowException ex) { Console.WriteLine(ex.Message); }   // o/p: "Arithmetic operation resulted in an overflow."

// ---------------- 4. throw (throwing our own error) ----------------

void SetAge(int age)
{
	if (age < 0)
		throw new ArgumentException("Age can't be negative", nameof(age));

	Console.WriteLine($"Age set to {age}");
}

try
{
	SetAge(20);    // o/p: "Age set to 20"
	SetAge(-5);
}
catch (ArgumentException ex)
{
	Console.WriteLine(ex.Message);     // o/p: "Age can't be negative (Parameter 'age')"
	Console.WriteLine(ex.ParamName);   // o/p: "age"
}

// ---------------- 5. Propagation (error travels up to the caller) ----------------

void Pay() => throw new InvalidOperationException("Payment failed");
void PlaceOrder() { Pay(); Console.WriteLine("Order placed"); }   // "Order placed" never prints

try
{
	PlaceOrder();
}
catch (InvalidOperationException ex)
{
	Console.WriteLine($"Caught in top level: {ex.Message}");   // o/p: "Caught in top level: Payment failed"
}

// ---------------- 6. finally runs even after return ----------------

int Divide(int a, int b)
{
	try
	{
		return a / b;
	}
	finally
	{
		Console.WriteLine("Cleanup done");   // runs before the value is returned
	}
}

Console.WriteLine(Divide(10, 2));
// o/p:
// Cleanup done
// 5

// ---------------- 7. Exception filter (when) ----------------

try
{
	SetAge(-1);
}
catch (ArgumentException ex) when (ex.ParamName == "age")
{
	Console.WriteLine("Filter matched: problem is with age");   // o/p: "Filter matched: problem is with age"
}

// ---------------- 8. throw; vs throw ex; ----------------

void Inner() => throw new InvalidOperationException("Oops");

void RethrowGood()
{
	try { Inner(); }
	catch (Exception) { Console.WriteLine("Logged the error"); throw; }   // same error, same stack trace
}

void RethrowBad()
{
	try { Inner(); }
	catch (Exception ex) { throw ex; }   // stack trace starts from here now (compiler even warns: CA2200)
}

try { RethrowGood(); }
catch (Exception ex) { Console.WriteLine($"throw;    -> shows Inner? {ex.StackTrace!.Contains("Inner")}"); }
// o/p:
// Logged the error
// throw;    -> shows Inner? True

try { RethrowBad(); }
catch (Exception ex) { Console.WriteLine($"throw ex; -> shows Inner? {ex.StackTrace!.Contains("Inner")}"); }
// o/p: "throw ex; -> shows Inner? False"   (we lost where the error really happened)

// ---------------- 9. Throw shortcuts ----------------

string GetCity(string? input) => input ?? throw new ArgumentNullException(nameof(input));

try
{
	Console.WriteLine(GetCity("Delhi"));   // o/p: "Delhi"
	GetCity(null);
}
catch (ArgumentNullException ex)
{
	Console.WriteLine(ex.Message);   // o/p: "Value cannot be null. (Parameter 'input')"
}

// ---------------- 10. Avoid exceptions for normal checks: TryParse ----------------

if (int.TryParse("abc", out int value))
	Console.WriteLine(value);
else
	Console.WriteLine("Not a number, and no exception was thrown");   // o/p: "Not a number, and no exception was thrown"

Console.WriteLine("Program is still running");   // o/p: "Program is still running"

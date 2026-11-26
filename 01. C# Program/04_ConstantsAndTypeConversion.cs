// ● Constants: C# allows you to define constants using the `const` keyword. Constants are immutable values that
//              cannot be changed after they are defined. They must be initialized at the time of declaration.

const double Pi = 3.14159;

Console.WriteLine("The value of Pi is: " + Pi);   // o/p: "The value of Pi is: 3.14159"


// ● Type Conversion: C# provides two types of type conversion: implicit and explicit.

//   - Implicit Conversion: This occurs when a smaller data type is converted to a larger data type automatically by the compiler.
//                          char -> int -> long -> float -> double

int num = 65;
double convertedNum = num;   // Implicit conversion from int to double
Console.WriteLine("The value of convertedNum is: " + convertedNum);   // o/p: "The value of convertedNum is: 65"

//   - Explicit Conversion: This occurs when a larger data type is converted to a smaller data type, and it requires a cast operator.
//                          double -> float -> long -> int -> char

double d = 9.78;
int i = (int)d;   // Explicit conversion from double to int
Console.WriteLine("The value of i is: " + i);   // o/p: "The value of i is: 9"

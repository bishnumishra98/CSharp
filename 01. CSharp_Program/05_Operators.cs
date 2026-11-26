// ● Operators: C# provides various operators for performing operations on variables and values.
//              There are several categories of operators in C#, like Arithmetic, Assignment, Comparison, Logical and Bitwise operators.

// ● Arithmetic Operators: These operators are used to perform mathematical operations on numeric values. The common arithmetic operators in C# are:
//   - Addition (+): Adds two operands.
//   - Subtraction (-): Subtracts the second operand from the first.
//   - Multiplication (*): Multiplies two operands.
//   - Division (/): Divides the first operand by the second.
//   - Modulus (%): Returns the remainder of the division of the first operand by the second.
//   - Increment (++): Increases the value of the operand by 1.
//   - Decrement (--): Decreases the value of the operand by 1.

int a = 10, b = 3;

Console.WriteLine(a + b);   // o/p: "13"
Console.WriteLine(a - b);   // o/p: "7"
Console.WriteLine(a * b);   // o/p: "30"
Console.WriteLine(a / b);   // o/p: "3"    (int / int = int: the decimal part is thrown away, not rounded)
Console.WriteLine(a % b);   // o/p: "1"    (10 = 3 * 3 + 1)

// To get the decimal answer, make at least one side a double:
Console.WriteLine((double)a / b);   // o/p: "3.3333333333333335"

// % takes the sign of the LEFT number:
Console.WriteLine(-7 % 3);   // o/p: "-1"
Console.WriteLine(7 % -3);   // o/p: "1"

// ++ and -- : the position matters when the value is USED in the same line.
//   x++ (postfix): use the old value first, then add 1.
//   ++x (prefix):  add 1 first, then use the new value.
int x = 5;
Console.WriteLine(x++);   // o/p: "5"    (prints 5, then x becomes 6)
Console.WriteLine(x);     // o/p: "6"
Console.WriteLine(++x);   // o/p: "7"    (x becomes 7, then prints 7)
Console.WriteLine(x--);   // o/p: "7"    (prints 7, then x becomes 6)
Console.WriteLine(--x);   // o/p: "5"    (x becomes 5, then prints 5)


// ● Assignment Operators: These operators are used to assign values to variables. The common assignment operators in C# are:
//   - Assignment (=): Assigns the value of the right operand to the left operand.
//   - Addition Assignment (+=): Adds the right operand to the left operand and assigns the result to the left operand.
//   - Subtraction Assignment (-=): Subtracts the right operand from the left operand and assigns the result to the left operand.
//   - Multiplication Assignment (*=): Multiplies the left operand by the right operand and assigns the result to the left operand.
//   - Division Assignment (/=): Divides the left operand by the right operand and assigns the result to the left operand.
//   - Modulus Assignment (%=): Takes the modulus using the left and right operands and assigns the result to the left operand.

int n = 10;
n += 5;   Console.WriteLine(n);   // o/p: "15"   (same as n = n + 5)
n -= 3;   Console.WriteLine(n);   // o/p: "12"   (n = n - 3)
n *= 2;   Console.WriteLine(n);   // o/p: "24"   (n = n * 2)
n /= 5;   Console.WriteLine(n);   // o/p: "4"    (n = n / 5, integer division: 4.8 -> 4)
n %= 3;   Console.WriteLine(n);   // o/p: "1"    (n = n % 3)

string greeting = "Hi";
greeting += " there";              // += also joins strings
Console.WriteLine(greeting);       // o/p: "Hi there"


// ● Comparison Operators: These operators are used to compare two values and return a Boolean result (true or false).
//                         The common comparison operators in C# are:
//   - Equal to (==): Returns true if the left operand is equal to the right operand.
//   - Not equal to (!=): Returns true if the left operand is not equal to the right operand.
//   - Greater than (>): Returns true if the left operand is greater than the right operand.
//   - Less than (<): Returns true if the left operand is less than the right operand.
//   - Greater than or equal to (>=): Returns true if the left operand is greater than or equal to the right operand.
//   - Less than or equal to (<=): Returns true if the left operand is less than or equal to the right operand.

int p = 5, q = 8;

Console.WriteLine(p == q);   // o/p: "False"
Console.WriteLine(p != q);   // o/p: "True"
Console.WriteLine(p > q);    // o/p: "False"
Console.WriteLine(p < q);    // o/p: "True"
Console.WriteLine(p >= 5);   // o/p: "True"
Console.WriteLine(q <= 7);   // o/p: "False"

// Careful: '=' assigns, '==' compares.  if (p = 5) is a compile error in C# (an int is not a bool), which saves you.

// == on strings compares the TEXT (unlike Java):
Console.WriteLine("cat" == "cat");   // o/p: "True"


// ● Logical Operators: These operators are used to perform logical operations on Boolean values. The common logical operators in C# are:
//   - Logical AND (&&): Returns true if both operands are true.
//   - Logical OR (||): Returns true if at least one of the operands is true.
//   - Logical NOT (!): Returns true if the operand is false, and false if the operand is true.

int age = 20;
bool hasTicket = false;

Console.WriteLine(age >= 18 && hasTicket);   // o/p: "False"   (both must be true)
Console.WriteLine(age >= 18 || hasTicket);   // o/p: "True"    (one true is enough)
Console.WriteLine(!hasTicket);               // o/p: "True"    (flips it)

// Short-circuit: && stops at the first false, || stops at the first true. The right side is then NOT run.
// This is often used to avoid errors:
int[] arr = { };
if (arr.Length > 0 && arr[0] == 1)   // arr[0] is never read, because arr.Length > 0 is already false
{
	Console.WriteLine("starts with 1");
}
Console.WriteLine("No crash");               // o/p: "No crash"

int count = 0;
bool result = false && ++count > 0;   // ++count never runs
Console.WriteLine(count);                    // o/p: "0"


// ● Bitwise Operators: These operators are used to perform bit-level operations on integer values. The common bitwise operators in C# are:
//   - Bitwise AND (&): Performs a bitwise AND operation on two operands.
//   - Bitwise OR (|): Performs a bitwise OR operation on two operands.
//   - Bitwise XOR (^): Performs a bitwise exclusive OR operation on two operands.
//   - Bitwise NOT (~): Performs a bitwise NOT operation on the operand.
//   - Left shift (<<) and Right shift (>>): Move all bits left or right. x << 1 doubles x, x >> 1 halves it (rounding down).
//   They work bit by bit, on the binary form of the numbers:
//     5 = 0101
//     3 = 0011
//     5 & 3 = 0001 = 1   (1 only where BOTH bits are 1)
//     5 | 3 = 0111 = 7   (1 where AT LEAST ONE bit is 1)
//     5 ^ 3 = 0110 = 6   (1 where the bits are DIFFERENT)

int m = 5, k = 3;

Console.WriteLine(m & k);    // o/p: "1"
Console.WriteLine(m | k);    // o/p: "7"
Console.WriteLine(m ^ k);    // o/p: "6"
Console.WriteLine(~m);       // o/p: "-6"   (flips ALL 32 bits; for any int, ~x == -x - 1)
Console.WriteLine(m << 1);   // o/p: "10"   (0101 -> 1010)
Console.WriteLine(m >> 1);   // o/p: "2"    (0101 -> 0010)

// Common uses:
Console.WriteLine((m & 1) == 1);                // o/p: "True"   (odd number check: the last bit is 1)
Console.WriteLine(Convert.ToString(m, 2));      // o/p: "101"    (see the binary form)

// & and | also work on bools, but they do NOT short-circuit: both sides always run. Use && and || for conditions.

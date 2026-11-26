// ● Conditional Statements: C# provides several conditional statements to control the flow of execution based on certain conditions.
//   The common conditional statements in C# are:
//   - if statement: Executes a block of code if a specified condition is true.
//   - if-else statement: Executes one block of code if a specified condition is true, and another block of code if the condition is false.
//   - if-else if-else statement: Executes one block of code if a specified condition is true, another block of code if another condition
//                                is true, and a final block of code if none of the conditions are true.
//   - switch statement: Executes one block of code from multiple options based on the value of a specified expression.
//   - ternary operator (? :): A one-line if-else that gives back a value.   condition ? valueIfTrue : valueIfFalse
//   - switch expression (C# 8+): A short form of switch that gives back a value.

// ---------------- if ----------------

int age = 20;

if (age >= 18)
{
	Console.WriteLine("Adult");   // o/p: "Adult"
}

// ---------------- if-else ----------------

int number = 7;

if (number % 2 == 0)
{
	Console.WriteLine("Even");
}
else
{
	Console.WriteLine("Odd");   // o/p: "Odd"
}

// ---------------- if-else if-else ----------------
// Checks from top to bottom. The first true condition runs, the rest are skipped.

int marks = 72;

if (marks >= 90)
{
	Console.WriteLine("Grade A");
}
else if (marks >= 70)
{
	Console.WriteLine("Grade B");   // o/p: "Grade B"
}
else if (marks >= 50)
{
	Console.WriteLine("Grade C");
}
else
{
	Console.WriteLine("Fail");
}

// ---------------- Nested if & multiple conditions ----------------

bool hasTicket = true;

if (age >= 18)
{
	if (hasTicket)
		Console.WriteLine("Can enter");   // o/p: "Can enter"   ({ } are optional for a single line)
}

if (age >= 18 && hasTicket)   // same thing using && (and)
	Console.WriteLine("Can enter");   // o/p: "Can enter"

// ---------------- Ternary operator ----------------

string result = marks >= 50 ? "Pass" : "Fail";
Console.WriteLine(result);   // o/p: "Pass"

// ---------------- switch statement ----------------
// - Each case must end with 'break' (C# does not allow falling into the next case).
// - default runs when no case matches.
// - Many cases can share one block by writing them one after another.

int day = 3;

switch (day)
{
	case 1:
		Console.WriteLine("Monday");
		break;
	case 2:
		Console.WriteLine("Tuesday");
		break;
	case 3:
		Console.WriteLine("Wednesday");   // o/p: "Wednesday"
		break;
	case 6:
	case 7:
		Console.WriteLine("Weekend");     // runs for both 6 and 7
		break;
	default:
		Console.WriteLine("Other day");
		break;
}

// ---------------- switch expression (C# 8+) ----------------
// '_' means "anything else" (like default).

string dayName = day switch
{
	1 => "Monday",
	2 => "Tuesday",
	3 => "Wednesday",
	6 or 7 => "Weekend",
	_ => "Other day"
};
Console.WriteLine(dayName);   // o/p: "Wednesday"

// Works with conditions too (relational patterns)
string grade = marks switch
{
	>= 90 => "A",
	>= 70 => "B",
	>= 50 => "C",
	_ => "Fail"
};
Console.WriteLine(grade);   // o/p: "B"

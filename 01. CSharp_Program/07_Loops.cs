// ● Loops: C# provides several loop statements to execute a block of code multiple times.
//   The common loop statements in C# are:
//   - for loop: Executes a block of code a specified number of times.
//   - while loop: Executes a block of code as long as a specified condition is true.
//   - do-while loop: Executes a block of code at least once, and then continues executing it as long as a specified condition is true.
//   - foreach loop: Goes through each item of a collection (array, List, string...). No index needed.
//
// ● Loop control:
//   - break: Stops the loop completely.
//   - continue: Skips the rest of the current round and jumps to the next round.
//
// ● Which loop to use?
//   - for: when you know how many times (e.g., 1 to 10), or you need the index.
//   - while: when you don't know how many times, only "keep going while this is true".
//   - do-while: when the code must run at least once (e.g., show a menu first, then ask again).
//   - foreach: when you just want every item of a collection. You can't change the collection inside foreach.

// ---------------- for ----------------
// for (start; condition; update)

for (int i = 1; i <= 5; i++)
{
	Console.Write(i + " ");
}
Console.WriteLine();   // o/p: "1 2 3 4 5 "

for (int i = 10; i > 0; i -= 3)   // counting down, by 3
{
	Console.Write(i + " ");
}
Console.WriteLine();   // o/p: "10 7 4 1 "

// ---------------- while ----------------
// Checks the condition first. If false at the start, the loop never runs.

int n = 1234, digits = 0;

while (n > 0)
{
	n /= 10;      // remove the last digit
	digits++;
}
Console.WriteLine($"Digits: {digits}");   // o/p: "Digits: 4"

// ---------------- do-while ----------------
// Runs first, checks later. So it runs at least once, even if the condition is false.

int count = 10;

do
{
	Console.WriteLine($"count = {count}");   // o/p: "count = 10"   (runs once even though 10 < 5 is false)
	count++;
} while (count < 5);

// ---------------- foreach ----------------

string[] fruits = { "Apple", "Mango", "Banana" };

foreach (string fruit in fruits)
{
	Console.Write(fruit + " ");
}
Console.WriteLine();   // o/p: "Apple Mango Banana "

foreach (char c in "Hi!")   // a string is a collection of chars
{
	Console.Write(c + "-");
}
Console.WriteLine();   // o/p: "H-i-!-"

// ---------------- break ----------------

for (int i = 1; i <= 10; i++)
{
	if (i == 4)
		break;   // stop the loop at 4
	Console.Write(i + " ");
}
Console.WriteLine();   // o/p: "1 2 3 "

// ---------------- continue ----------------

for (int i = 1; i <= 6; i++)
{
	if (i % 2 == 0)
		continue;   // skip even numbers
	Console.Write(i + " ");
}
Console.WriteLine();   // o/p: "1 3 5 "

// ---------------- Nested loop ----------------
// Inner loop runs fully for every single round of the outer loop.

for (int row = 1; row <= 3; row++)
{
	for (int col = 1; col <= row; col++)
	{
		Console.Write("* ");
	}
	Console.WriteLine();
}
// o/p:
// *
// * *
// * * *

// ---------------- Infinite loop ----------------
// A loop whose condition never becomes false. Use break to get out of it.

int tries = 0;

while (true)
{
	tries++;
	if (tries == 3)
		break;
}
Console.WriteLine($"Stopped after {tries} tries");   // o/p: "Stopped after 3 tries"

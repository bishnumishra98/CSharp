// ● Aggregation: Turning many items into ONE value (a count, a total, an average...).
//   I.   Count(): Number of items. Count(condition): Number of items that match the condition.
//   II.  Sum(selector): Total.
//   III. Average(selector): Average (mean).
//   IV.  Min(selector) / Max(selector): Smallest / biggest VALUE.
//   V.   MinBy(selector) / MaxBy(selector): The ITEM which has the smallest / biggest value. (.NET 6+)
//        - Max(e => e.Salary)   gives 1800000   (just the number)
//        - MaxBy(e => e.Salary) gives Karan     (the whole Employee)
//   VI.  Aggregate(func): Our own custom aggregation (when Sum/Min/Max are not enough).

// ● Count vs Count():
//   - list.Count   : a property of List. Already stored, so it is instant.
//   - list.Count() : a LINQ method. Works on any IEnumerable. With a condition, it has to check every item.
//   For a List or array, prefer .Count / .Length when there is no condition.

// ● Aggregate: Goes through the items one by one and keeps a running result (called accumulator).
//   I.   Aggregate((acc, item) => ...)
//        - acc starts as the 1st item, then for every next item, acc = func(acc, item).
//        - Product of { 2, 3, 4 }: acc = 2 -> 2 * 3 = 6 -> 6 * 4 = 24.
//   II.  Aggregate(seed, (acc, item) => ...)
//        - acc starts as 'seed'. Useful when the result type is different from the item type.
//        - Example: total salary = employees.Aggregate(0m, (total, e) => total + e.Salary)

// ● NOTE (Empty collection):
//   - Count() and Sum() of an empty collection give 0.
//   - Average(), Min(), Max() of an empty collection THROW InvalidOperationException.
//     Fix: check with Any() first, or use DefaultIfEmpty(0) to give a default value.

using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
	public int EmployeeId { get; set; }
	public string EmployeeName { get; set; } = "";
	public decimal Salary { get; set; }
	public int DepartmentId { get; set; }
	public int? ManagerId { get; set; }
}

class Program
{
	static void Main()
	{
		List<Employee> employees = new List<Employee>
		{
			new Employee { EmployeeId = 1, EmployeeName = "Bishnu", Salary = 1200000, DepartmentId = 1, ManagerId = 5 },
			new Employee { EmployeeId = 2, EmployeeName = "Rahul", Salary = 800000, DepartmentId = 2, ManagerId = 5 },
			new Employee { EmployeeId = 3, EmployeeName = "Amit", Salary = 1500000, DepartmentId = 1, ManagerId = 6 },
			new Employee { EmployeeId = 4, EmployeeName = "Priya", Salary = 600000, DepartmentId = 3, ManagerId = 6 },
			new Employee { EmployeeId = 5, EmployeeName = "Karan", Salary = 1800000, DepartmentId = 2, ManagerId = null },
			new Employee { EmployeeId = 6, EmployeeName = "Sneha", Salary = 1400000, DepartmentId = 1, ManagerId = null },
			new Employee { EmployeeId = 7, EmployeeName = "Neha", Salary = 900000, DepartmentId = 3, ManagerId = 5 },
			new Employee { EmployeeId = 8, EmployeeName = "Arjun", Salary = 1200000, DepartmentId = 2, ManagerId = 6 }
		};

		// ---------------- On simple numbers ----------------

		int[] nums = { 5, 3, 8, 1, 4 };

		Console.WriteLine(nums.Count());     // o/p: 5
		Console.WriteLine(nums.Sum());       // o/p: 21
		Console.WriteLine(nums.Average());   // o/p: 4.2   (Average of ints gives a double)
		Console.WriteLine(nums.Min());       // o/p: 1
		Console.WriteLine(nums.Max());       // o/p: 8

		// ---------------- Count ----------------

		Console.WriteLine(employees.Count);                               // o/p: 8   (List property)
		Console.WriteLine(employees.Count(e => e.Salary > 1000000));      // o/p: 5
		Console.WriteLine(employees.Count(e => e.DepartmentId == 2));     // o/p: 3

		// ---------------- Sum / Average ----------------

		Console.WriteLine(employees.Sum(e => e.Salary));                                  // o/p: 9400000
		Console.WriteLine(employees.Where(e => e.DepartmentId == 1).Sum(e => e.Salary));  // o/p: 4100000   (total salary of IT)
		Console.WriteLine(employees.Average(e => e.Salary));                              // o/p: 1175000

		// ---------------- Min / Max vs MinBy / MaxBy ----------------

		Console.WriteLine(employees.Max(e => e.Salary));   // o/p: 1800000
		Console.WriteLine(employees.Min(e => e.Salary));   // o/p: 600000

		Employee? highestPaid = employees.MaxBy(e => e.Salary);
		Employee? lowestPaid = employees.MinBy(e => e.Salary);
		Console.WriteLine($"{highestPaid?.EmployeeName}, {lowestPaid?.EmployeeName}");   // o/p: "Karan, Priya"

		// Before .NET 6, the same thing was done like this:
		Employee topOld = employees.OrderByDescending(e => e.Salary).First();
		Console.WriteLine(topOld.EmployeeName);   // o/p: "Karan"

		// ---------------- Aggregate ----------------

		// I. Product of all numbers (there is no built-in Product(), so Aggregate does it).
		//    nums = { 5, 3, 8, 1, 4 }. No seed, so acc starts as the 1st item (5), and the loop starts from the 2nd item:
		//      acc = 5
		//      n = 3:  acc = 5 * 3   = 15
		//      n = 8:  acc = 15 * 8  = 120
		//      n = 1:  acc = 120 * 1 = 120
		//      n = 4:  acc = 120 * 4 = 480   <- the last acc is the answer
		int product = nums.Aggregate((acc, n) => acc * n);   // 5 -> 15 -> 120 -> 120 -> 480
		Console.WriteLine(product);   // o/p: 480

		// II. Biggest number, written by hand (our own Max()).
		//     The accumulator can have any name. Here it is called 'max' because it holds the biggest number seen so far.
		//     For every n: if n is bigger than max, n becomes the new max, otherwise max stays.
		//       max = 5  ->  3 > 5 ? no (5)  ->  8 > 5 ? yes (8)  ->  1 > 8 ? no (8)  ->  4 > 8 ? no (8)
		int biggest = nums.Aggregate((max, n) => n > max ? n : max);   // our own Max()
		Console.WriteLine(biggest);   // o/p: 8

		// III. Factorial of 5 = 1 * 2 * 3 * 4 * 5.
		//      Enumerable.Range(start, count) makes the numbers to multiply: 'count' numbers starting from 'start'.
		//      Then it is the same product as I:  1 -> 2 -> 6 -> 24 -> 120
		int factorial = Enumerable.Range(1, 5).Aggregate((acc, n) => acc * n);   // Range(1, 5) gives 1, 2, 3, 4, 5
		Console.WriteLine(factorial);   // o/p: 120

		// IV. Join all names into one string, with " | " between them.
		//     Why Select first ? Without a seed, acc has the SAME type as the items. The items must be strings to build a
		//     string, so Select turns each Employee into its name first.
		//       acc = "Bishnu"  ->  "Bishnu | Rahul"  ->  "Bishnu | Rahul | Amit"  ->  ...
		//     Note: In real code, string.Join(" | ", names) does this more simply and faster (no new string at every step).
		string allNames = employees.Select(e => e.EmployeeName).Aggregate((acc, name) => acc + " | " + name);
		Console.WriteLine(allNames);   // o/p: "Bishnu | Rahul | Amit | Priya | Karan | Sneha | Neha | Arjun"

		// V. Total salary, with a SEED.
		//    The items are Employees, but the answer is a decimal. Without a seed, acc would start as the 1st Employee, and
		//    'Employee + decimal' makes no sense. So we give a starting value (seed) of the answer's type: 0m.
		//      acc = 0  ->  0 + 1200000 (Bishnu) = 1200000  ->  1200000 + 800000 (Rahul) = 2000000  ->  ...  -> 9400000
		//    - Why 0m and not 0 ? The seed decides acc's type. 0 is an int, and 'int + decimal' gives a decimal, which can
		//      not be stored back into an int acc: compile error. 'm' makes it a decimal.
		//    - With a seed, the loop starts from the 1st item (not the 2nd), because acc already has a value.
		//    - Bonus: with a seed, an EMPTY list just returns the seed. Without a seed, an empty list THROWS
		//      InvalidOperationException (there is no 1st item to start acc with).
		decimal total = employees.Aggregate(0m, (acc, e) => acc + e.Salary);
		Console.WriteLine(total);   // o/p: 9400000   (same as employees.Sum(e => e.Salary))

		// ---------------- Empty collection ----------------

		List<int> empty = new List<int>();

		Console.WriteLine(empty.Count());   // o/p: 0
		Console.WriteLine(empty.Sum());     // o/p: 0

		try
		{
			Console.WriteLine(empty.Max());
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine(ex.Message);   // o/p: "Sequence contains no elements"
		}

		// Two ways to avoid the crash:

		// I. DefaultIfEmpty(value): If the list is empty, it gives back a list with just ONE item: 'value'.
		//    If the list is NOT empty, it gives back the list as it is (the value is NOT added).
		//      empty         -> DefaultIfEmpty(0) -> { 0 }        -> Max() = 0
		//      { 5, 3, 8 }   -> DefaultIfEmpty(0) -> { 5, 3, 8 }  -> Max() = 8
		//    Careful: the answer 0 can mean "empty list" OR "the biggest number really is 0". You can not tell them apart.
		Console.WriteLine(empty.DefaultIfEmpty(0).Max());   // o/p: 0

		// II. Any(): true if the list has at least one item.
		//     condition ? a : b  (ternary operator): if the condition is true, the answer is a, otherwise b.
		//     So: if the list has items, take its Max(), otherwise use -1. Max() is only called when it is safe.
		//     Here we choose the value to use for "no items" (-1), so pick one that can not be a real answer.
		Console.WriteLine(empty.Any() ? empty.Max() : -1);  // o/p: -1
	}
}

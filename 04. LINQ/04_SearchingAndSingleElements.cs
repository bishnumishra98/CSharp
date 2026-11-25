// ● Getting ONE item:
//   I.   First(condition): The first matching item. THROWS if nothing matches.
//   II.  FirstOrDefault(condition): The first matching item, or default if nothing matches.
//        - default is null for classes (Employee, string), 0 for int, false for bool.
//        - FirstOrDefault(condition, myDefault) lets us give our own default. (.NET 6+)
//   III. Last(condition) / LastOrDefault(condition): Same as above, but from the end.
//   IV.  Single(condition): The ONLY matching item. THROWS if 0 match or more than 1 match.
//   V.   SingleOrDefault(condition): default if 0 match, the item if 1 match, THROWS if more than 1 match.
//   VI.  ElementAt(index): Item at the given position.
//   - All of these also work without a condition, e.g. employees.First() gives the first employee.
//
//   Method            | 0 match    | 1 match | 2+ matches
//   First             | Exception  | item    | first item
//   FirstOrDefault    | default    | item    | first item
//   Single            | Exception  | item    | Exception
//   SingleOrDefault   | default    | item    | Exception
//
//   - Use First / FirstOrDefault when you just want the first one.
//   - Use Single / SingleOrDefault when there MUST be only one (like searching by Id). If there are 2, it's a bug,
//     and Single catches that bug for us.
//   - Use the 'OrDefault' version when "not found" is normal, and then check for null.

// ● Find(condition): A method of List<T> (not LINQ). Works like FirstOrDefault.
//   FindAll(condition): A method of List<T>. Works like Where, but gives a List<T> directly.

// ● Checking (these return bool):
//   I.   Any(): Is there at least one item?  Any(condition): Does at least one item match?
//   II.  All(condition): Do ALL items match?   NOTE: All() on an empty collection gives true.
//   III. Contains(value): Is this value present?
//        - Works well on simple values (int, string).
//        - On objects, it compares references. A new Employee with same values is a different object.
//          So for objects use Any(e => e.EmployeeName == "Neha") instead.
//   - Prefer Any() over Count() > 0. Any() stops at the first match, Count() checks everything.

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

		// ---------------- First / FirstOrDefault ----------------

		Console.WriteLine(employees.First().EmployeeName);                             // o/p: "Bishnu"
		Console.WriteLine(employees.First(e => e.Salary > 1300000).EmployeeName);      // o/p: "Amit"
		Console.WriteLine(employees.First(e => e.ManagerId == null).EmployeeName);     // o/p: "Karan"

		try
		{
			Employee emp = employees.First(e => e.Salary > 5000000);   // nobody earns 50 lakh
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine(ex.Message);   // o/p: "Sequence contains no matching element"
		}

		Employee? notFound = employees.FirstOrDefault(e => e.Salary > 5000000);
		Console.WriteLine(notFound == null);                         // o/p: True
		Console.WriteLine(notFound?.EmployeeName ?? "Not found");    // o/p: "Not found"

		int[] nums = { 4, 8, 15 };
		Console.WriteLine(nums.FirstOrDefault(n => n > 100));        // o/p: 0    (default of int)
		Console.WriteLine(nums.FirstOrDefault(n => n > 100, -1));    // o/p: -1   (our own default)

		// ---------------- Last / LastOrDefault / ElementAt ----------------

		Console.WriteLine(employees.Last().EmployeeName);                           // o/p: "Arjun"
		Console.WriteLine(employees.Last(e => e.DepartmentId == 1).EmployeeName);   // o/p: "Sneha"
		Console.WriteLine(employees.ElementAt(2).EmployeeName);                     // o/p: "Amit"

		// ---------------- Single / SingleOrDefault ----------------

		Console.WriteLine(employees.Single(e => e.EmployeeId == 3).EmployeeName);   // o/p: "Amit"

		try
		{
			Employee emp = employees.Single(e => e.DepartmentId == 1);   // IT has 3 employees
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine(ex.Message);   // o/p: "Sequence contains more than one matching element"
		}

		Employee? noId = employees.SingleOrDefault(e => e.EmployeeId == 99);
		Console.WriteLine(noId?.EmployeeName ?? "No employee with Id 99");   // o/p: "No employee with Id 99"

		// ---------------- Find / FindAll (List<T> methods) ----------------

		Employee? neha = employees.Find(e => e.EmployeeName == "Neha");
		Console.WriteLine(neha?.Salary);   // o/p: 900000

		List<Employee> lowPaid = employees.FindAll(e => e.Salary < 1000000);
		Console.WriteLine(string.Join(", ", lowPaid.Select(e => e.EmployeeName)));   // o/p: "Rahul, Priya, Neha"

		// ---------------- Any / All ----------------

		Console.WriteLine(employees.Any());                              // o/p: True   (list is not empty)
		Console.WriteLine(employees.Any(e => e.Salary > 1700000));       // o/p: True   (Karan)
		Console.WriteLine(employees.Any(e => e.DepartmentId == 4));      // o/p: False  (no one in Marketing)

		Console.WriteLine(employees.All(e => e.Salary > 500000));        // o/p: True
		Console.WriteLine(employees.All(e => e.Salary > 1000000));       // o/p: False

		Console.WriteLine(new List<int>().All(n => n > 100));            // o/p: True   (empty: nothing breaks the rule)

		// ---------------- Contains ----------------

		List<string> names = employees.Select(e => e.EmployeeName).ToList();
		Console.WriteLine(names.Contains("Priya"));   // o/p: True
		Console.WriteLine(names.Contains("Rohit"));   // o/p: False

		// Problem: Contains on objects compares references
		Employee copyOfNeha = new Employee { EmployeeId = 7, EmployeeName = "Neha", Salary = 900000, DepartmentId = 3, ManagerId = 5 };
		Console.WriteLine(employees.Contains(copyOfNeha));   // o/p: False   (same values, but a different object)

		// Solution: check by a value using Any
		Console.WriteLine(employees.Any(e => e.EmployeeId == copyOfNeha.EmployeeId));   // o/p: True
	}
}

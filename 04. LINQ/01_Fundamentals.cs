// ● LINQ (Language Integrated Query): A way to ask questions to data (filter, sort, group, count...) directly in C#.
//   - It works on any collection: array, List, Dictionary... and even on a database (with EF Core, see 08_LINQWithEFCore.cs).
//   - Think of it as writing SQL inside C#: "give me names of all employees whose salary is more than 10 lakh".
//   - Namespace: using System.Linq;
//   - It works on anything that is IEnumerable<T> (i.e., anything you can run a foreach loop on).

// ● Why LINQ? (Problem -> Solution)
//   Problem: Without LINQ, to get the names of employees earning more than 10 lakh:
//        List<string> names = new List<string>();
//        foreach (Employee e in employees)
//        {
//            if (e.Salary > 1000000)
//                names.Add(e.EmployeeName);
//        }
//   Solution: With LINQ, the same thing in 1 line:
//        var names = employees.Where(e => e.Salary > 1000000).Select(e => e.EmployeeName);
//   We only say WHAT we want, not HOW to loop. Less code, easier to read.

// ● Lambda Expression: A short function with no name, written using '=>' (read as "goes to").
//   Syntax: (parameters) => expression
//   - x => x * x                                // takes x, returns x * x
//   - (a, b) => a + b                           // takes a and b, returns a + b
//   - () => "Hi"                                // takes nothing, returns "Hi"
//   - x => { int y = x * 2; return y + 1; }     // more than 1 line needs { } and 'return'
//
//   A lambda can be stored in Func<> or Action<>. Both are built-in delegates (see 01. CSharp_Program/14_Delegates.cs).
//   - Func<...>   : returns a value. The LAST type is the return type.
//                   Func<int, int> square = x => x * x;           // takes int, returns int
//                   Func<int, bool> isEven = n => n % 2 == 0;     // takes int, returns bool
//   - Action<...> : returns nothing (void).
//                   Action<string> greet = name => Console.WriteLine("Hi " + name);   // takes string, returns nothing
//
//   Every LINQ method takes a lambda. Where(e => e.Salary > 1000000) means: "for each employee e, keep it if salary > 10 lakh".

// ● Where(condition): Filter. Keeps only those items for which the condition is true.
// ● Select(transform): Projection. Converts each item into something else (e.g., Employee -> its name).
//   - Where only decides keep or drop. It never changes an item. (Count can go down)
//   - Select only changes the shape of an item. It never drops an item. (Count stays the same)

// ● Query syntax vs Method syntax: 2 ways to write the same LINQ.
//   I.  Method syntax (methods + lambdas):
//        employees.Where(e => e.Salary > 1000000).Select(e => e.EmployeeName)
//   II. Query syntax (looks like SQL):
//        from e in employees
//        where e.Salary > 1000000
//        select e.EmployeeName
//   - The compiler converts query syntax into method syntax. So both have the same speed.
//   - Query syntax starts with 'from' and ends with 'select' or 'group'.
//   - Many methods (Count, Sum, First, Take, Distinct...) exist only in method syntax.
//     That's why method syntax is used more in real projects. Query syntax is easier to read for joins (see 06_Joins.cs).

// ● NOTE:
//   - LINQ never changes the original collection. It always gives back a new result.
//   - The result is usually IEnumerable<T>. Use ToList() or ToArray() to store it as a List or array.
//   - Result types can get long (IEnumerable<string>, IOrderedEnumerable<Employee>...), so 'var' is used a lot with LINQ.

// ● Data used in all LINQ files (1 lakh = 1,00,000):
//   Id | Name   | Salary  | Department | ManagerId
//   1  | Bishnu | 12 lakh | 1 (IT)     | 5
//   2  | Rahul  |  8 lakh | 2 (HR)     | 5
//   3  | Amit   | 15 lakh | 1 (IT)     | 6
//   4  | Priya  |  6 lakh | 3 (Finance)| 6
//   5  | Karan  | 18 lakh | 2 (HR)     | null (no manager)
//   6  | Sneha  | 14 lakh | 1 (IT)     | null (no manager)
//   7  | Neha   |  9 lakh | 3 (Finance)| 5
//   8  | Arjun  | 12 lakh | 2 (HR)     | 6
//   Departments: 1 IT, 2 HR, 3 Finance, 4 Marketing (Marketing has no employee)

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

class Department
{
	public int DepartmentId { get; set; }
	public string DepartmentName { get; set; } = "";
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

		// ---------------- Lambda expressions ----------------

		Func<int, int> square = x => x * x;
		Func<int, int, int> add = (a, b) => a + b;
		Func<int, bool> isEven = n => n % 2 == 0;
		Action<string> greet = name => Console.WriteLine($"Hi {name}");

		Console.WriteLine(square(5));   // o/p: 25
		Console.WriteLine(add(2, 3));   // o/p: 5
		Console.WriteLine(isEven(7));   // o/p: False
		greet("Bishnu");   // o/p: "Hi Bishnu"

		// ---------------- Where (filter) ----------------

		int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

		IEnumerable<int> evens = nums.Where(n => n % 2 == 0);
		Console.WriteLine(string.Join(", ", evens));   // o/p: "2, 4, 6, 8, 10"

		IEnumerable<Employee> rich = employees.Where(e => e.Salary > 1000000);
		foreach (Employee e in rich)
		{
			Console.Write(e.EmployeeName + " ");
		}
		Console.WriteLine();   // o/p: "Bishnu Amit Karan Sneha Arjun "

		// ---------------- Select (transform) ----------------

		IEnumerable<int> squares = nums.Select(n => n * n);
		Console.WriteLine(string.Join(", ", squares));   // o/p: "1, 4, 9, 16, 25, 36, 49, 64, 81, 100"

		IEnumerable<string> names = employees.Select(e => e.EmployeeName);   // Employee -> string
		Console.WriteLine(string.Join(", ", names));   // o/p: "Bishnu, Rahul, Amit, Priya, Karan, Sneha, Neha, Arjun"

		IEnumerable<string> inLakhs = employees.Select(e => $"{e.EmployeeName}: {e.Salary / 100000}L");
		Console.WriteLine(string.Join(", ", inLakhs));   // o/p: "Bishnu: 12L, Rahul: 8L, Amit: 15L, Priya: 6L, Karan: 18L, Sneha: 14L, Neha: 9L, Arjun: 12L"

		// Select can also give the index (position) of each item as the 2nd parameter
		IEnumerable<string> numbered = employees.Select((e, i) => $"{i + 1}.{e.EmployeeName}");
		Console.WriteLine(string.Join(" ", numbered));   // o/p: "1.Bishnu 2.Rahul 3.Amit 4.Priya 5.Karan 6.Sneha 7.Neha 8.Arjun"

		// ---------------- Chaining (Where + Select) ----------------
		// Read it from left to right: employees -> keep salary > 10 lakh -> take their names

		var richNames = employees.Where(e => e.Salary > 1000000).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", richNames));   // o/p: "Bishnu, Amit, Karan, Sneha, Arjun"

		// NOTE: Order matters. If we Select names first, we get strings, and a string has no 'Salary' to check.
		// employees.Select(e => e.EmployeeName).Where(e => e.Salary > 1000000);   // Error

		// ---------------- Query syntax vs Method syntax ----------------

		var methodSyntax = employees.Where(e => e.DepartmentId == 1).Select(e => e.EmployeeName);

		var querySyntax = from e in employees
						  where e.DepartmentId == 1
						  select e.EmployeeName;

		Console.WriteLine(string.Join(", ", methodSyntax));   // o/p: "Bishnu, Amit, Sneha"
		Console.WriteLine(string.Join(", ", querySyntax));    // o/p: "Bishnu, Amit, Sneha"

		// Query syntax has no Count(). So wrap the query in ( ) and call the method on it.
		int itCount = (from e in employees where e.DepartmentId == 1 select e).Count();
		Console.WriteLine(itCount);   // o/p: 3

		// ---------------- Original list is not changed ----------------

		List<string> richList = richNames.ToList();   // store result as a List
		Console.WriteLine($"{richList.Count} rich, {employees.Count} total");   // o/p: "5 rich, 8 total"
	}
}

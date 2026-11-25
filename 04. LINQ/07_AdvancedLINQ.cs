// ● I. Anonymous objects: An object made on the spot, without writing a class for it.
//   - Syntax: new { Name = e.EmployeeName, e.Salary }   // 'e.Salary' alone means: property name = Salary
//   - Must be stored in 'var' (the type has no name we can write).
//   - Properties are read-only.
//   - Two anonymous objects with the same values are Equal (they compare by value, unlike normal classes).
//   - Used a lot in Select, to pick only the needed fields.
//   - Limit: Can't be easily returned from a method. For that, use a class, a record, or a tuple:
//     Select(e => (Name: e.EmployeeName, e.Salary))   // tuple: can be returned from a method

// ● II. SelectMany: Flattens a "list of lists" into ONE list.
//   - Select    : { {1, 2}, {3} }  ->  { {1, 2}, {3} }   (still a list of lists)
//   - SelectMany: { {1, 2}, {3} }  ->  { 1, 2, 3 }       (one flat list)
//   - Example: sentences -> all words: sentences.SelectMany(s => s.Split(' '))

// ● III. ToDictionary(key, value): Makes a Dictionary for fast lookup by key (O(1)).
//   - THROWS if two items have the same key. So the key must be unique (like an Id).

// ● IV. ToLookup(key): Like a Dictionary, but ONE key can have MANY values.
//   - lookup[key] gives all items of that key.
//   - Missing key gives an empty list (no exception). A Dictionary would throw.
//   - Difference from GroupBy: GroupBy runs later, when you loop on it (deferred). ToLookup runs immediately
//     and stores the result (see 08_LINQWithEFCore.cs for deferred vs immediate).

// ● V. Zip: Joins 2 collections by POSITION (1st with 1st, 2nd with 2nd...).
//   - Stops when the shorter collection ends.
//   - a.Zip(b) gives tuples (First, Second). a.Zip(b, (x, y) => ...) gives our own result.

// ● VI. DistinctBy(key): Keeps only the first item for each key. (.NET 6+)
//   - employees.DistinctBy(e => e.DepartmentId) gives 1 employee from each department.
//   - Same family: MinBy, MaxBy, UnionBy, IntersectBy, ExceptBy.

// ● VII. Set operations (all of them remove duplicates):
//   Let a = {1, 2, 3, 4}, b = {3, 4, 5, 6}
//   - a.Union(b)     : all items of both               -> 1, 2, 3, 4, 5, 6
//   - a.Intersect(b) : items present in both           -> 3, 4
//   - a.Except(b)    : items in a but NOT in b         -> 1, 2
//   - a.Concat(b)    : just joins both (keeps duplicates, not a set operation) -> 1, 2, 3, 4, 3, 4, 5, 6
//   - On objects, they compare references. Use UnionBy / IntersectBy / ExceptBy to compare by a key.

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

		List<Department> departments = new List<Department>
		{
			new Department { DepartmentId = 1, DepartmentName = "IT" },
			new Department { DepartmentId = 2, DepartmentName = "HR" },
			new Department { DepartmentId = 3, DepartmentName = "Finance" },
			new Department { DepartmentId = 4, DepartmentName = "Marketing" }
		};

		// ---------------- I. Anonymous objects ----------------

		var slim = employees.Select(e => new { Name = e.EmployeeName, Lakhs = e.Salary / 100000 });

		Console.WriteLine(slim.First());         // o/p: "{ Name = Bishnu, Lakhs = 12 }"
		Console.WriteLine(slim.First().Name);    // o/p: "Bishnu"

		var p1 = new { Name = "Amit", Age = 25 };
		var p2 = new { Name = "Amit", Age = 25 };
		Console.WriteLine(p1.Equals(p2));        // o/p: True   (compared by value)

		// Very common: anonymous object inside GroupBy
		var deptSummary = employees.GroupBy(e => e.DepartmentId)
								   .Select(g => new { DeptId = g.Key, Count = g.Count(), Total = g.Sum(e => e.Salary) });

		foreach (var d in deptSummary)
		{
			Console.WriteLine($"Dept {d.DeptId}: {d.Count} employees, {d.Total}");
		}
		// o/p:
		// Dept 1: 3 employees, 4100000
		// Dept 2: 3 employees, 3800000
		// Dept 3: 2 employees, 1500000

		// ---------------- II. SelectMany ----------------

		List<List<int>> nested = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4, 5 }, new List<int> { 6 } };

		var flat = nested.SelectMany(list => list);
		Console.WriteLine(string.Join(", ", flat));   // o/p: "1, 2, 3, 4, 5, 6"

		string[] sentences = { "I love CSharp", "LINQ is fun" };

		var wordsSelect = sentences.Select(s => s.Split(' '));       // IEnumerable<string[]>  (2 arrays)
		var wordsMany = sentences.SelectMany(s => s.Split(' '));     // IEnumerable<string>    (6 words)
		Console.WriteLine(wordsSelect.Count());                      // o/p: 2
		Console.WriteLine(string.Join(", ", wordsMany));             // o/p: "I, love, CSharp, LINQ, is, fun"

		// Every pair of (department, salary band) - SelectMany also makes combinations
		string[] bands = { "Low", "High" };
		var combos = departments.Take(2).SelectMany(d => bands, (d, b) => $"{d.DepartmentName}-{b}");
		Console.WriteLine(string.Join(", ", combos));   // o/p: "IT-Low, IT-High, HR-Low, HR-High"

		// ---------------- III. ToDictionary ----------------

		Dictionary<int, Employee> byId = employees.ToDictionary(e => e.EmployeeId);
		Console.WriteLine(byId[3].EmployeeName);   // o/p: "Amit"

		Dictionary<int, string> deptNames = departments.ToDictionary(d => d.DepartmentId, d => d.DepartmentName);
		Console.WriteLine(deptNames[2]);           // o/p: "HR"

		try
		{
			var bad = employees.ToDictionary(e => e.DepartmentId);   // DepartmentId 1 comes 3 times
		}
		catch (ArgumentException ex)
		{
			Console.WriteLine(ex.Message);   // o/p: "An item with the same key has already been added. Key: 1"
		}

		// ---------------- IV. ToLookup ----------------

		ILookup<int, string> namesByDept = employees.ToLookup(e => e.DepartmentId, e => e.EmployeeName);

		Console.WriteLine(string.Join(", ", namesByDept[1]));   // o/p: "Bishnu, Amit, Sneha"
		Console.WriteLine(namesByDept[4].Count());              // o/p: 0   (Marketing: empty, no exception)

		// ---------------- V. Zip ----------------

		string[] days = { "Mon", "Tue", "Wed" };
		int[] temps = { 30, 32, 31, 29 };   // 4 items, but only 3 days

		var weather = days.Zip(temps, (day, t) => $"{day}: {t}");
		Console.WriteLine(string.Join(", ", weather));   // o/p: "Mon: 30, Tue: 32, Wed: 31"   (29 is ignored)

		foreach (var (day, t) in days.Zip(temps))   // without selector, gives tuples
		{
			Console.Write($"{day}={t} ");
		}
		Console.WriteLine();   // o/p: "Mon=30 Tue=32 Wed=31 "

		// Trick: difference between consecutive numbers (zip the list with itself, shifted by 1)
		int[] marks = { 10, 15, 25, 30 };
		var diffs = marks.Zip(marks.Skip(1), (a, b) => b - a);
		Console.WriteLine(string.Join(", ", diffs));   // o/p: "5, 10, 5"

		// ---------------- VI. DistinctBy ----------------

		var onePerDept = employees.DistinctBy(e => e.DepartmentId).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", onePerDept));   // o/p: "Bishnu, Rahul, Priya"

		// ---------------- VII. Union / Intersect / Except ----------------

		int[] a = { 1, 2, 3, 4 };
		int[] b = { 3, 4, 5, 6 };

		Console.WriteLine(string.Join(", ", a.Union(b)));       // o/p: "1, 2, 3, 4, 5, 6"
		Console.WriteLine(string.Join(", ", a.Intersect(b)));   // o/p: "3, 4"
		Console.WriteLine(string.Join(", ", a.Except(b)));      // o/p: "1, 2"
		Console.WriteLine(string.Join(", ", b.Except(a)));      // o/p: "5, 6"
		Console.WriteLine(string.Join(", ", a.Concat(b)));      // o/p: "1, 2, 3, 4, 3, 4, 5, 6"

		// ExceptBy: departments whose Id is NOT in the employees' DepartmentIds
		var emptyDepts = departments.ExceptBy(employees.Select(e => e.DepartmentId), d => d.DepartmentId).Select(d => d.DepartmentName);
		Console.WriteLine(string.Join(", ", emptyDepts));   // o/p: "Marketing"

		// IntersectBy: employees whose Id is present in the list of ManagerIds (i.e., who are managers)
		var managers = employees.IntersectBy(employees.Select(e => e.ManagerId), e => (int?)e.EmployeeId).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", managers));   // o/p: "Karan, Sneha"
	}
}

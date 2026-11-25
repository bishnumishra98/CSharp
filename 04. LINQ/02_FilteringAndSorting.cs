// ● Filtering: Where(condition)
//   - Combine conditions with && (and), || (or), ! (not).
//   - Where((item, index) => ...) also gives the index of each item.

// ● Sorting:
//   I.   OrderBy(key): Sorts in ascending order (small -> big, A -> Z) by the given key.
//   II.  OrderByDescending(key): Sorts in descending order (big -> small, Z -> A).
//   III. ThenBy(key) / ThenByDescending(key): Tie breaker. Used AFTER OrderBy, to sort items whose first key is equal.
//        Example: sort by salary, and if salary is same, then by name.
//   IV.  Reverse(): Reverses the order.
//   - OrderBy is "stable": if 2 items have the same key, they keep their original order.
//
//   Problem: Calling OrderBy two times does NOT work as a tie breaker.
//        employees.OrderByDescending(e => e.Salary).OrderBy(e => e.EmployeeName)
//        The 2nd OrderBy sorts everything again by name. The salary sorting is lost.
//   Solution: Use ThenBy for the 2nd key.
//        employees.OrderByDescending(e => e.Salary).ThenBy(e => e.EmployeeName)

// ● Distinct(): Removes duplicates.
//   - Works well on simple values (int, string...).
//   - On objects (like Employee), it compares references, not values. So select the value first:
//     employees.Select(e => e.DepartmentId).Distinct()   or use DistinctBy (see 07_AdvancedLINQ.cs).

// ● Take & Skip:
//   I.   Take(n): Takes the first n items.
//   II.  Skip(n): Skips the first n items and gives the rest.
//   III. TakeLast(n) / SkipLast(n): Same thing, but from the end.
//   IV.  TakeWhile(condition): Takes items from the start WHILE the condition is true. Stops at the first false.
//   V.   SkipWhile(condition): Skips items from the start WHILE the condition is true, then gives everything after.
//   - Pagination (showing data page by page): Skip((pageNumber - 1) * pageSize).Take(pageSize)
//   - NOTE: TakeWhile is not the same as Where. Where checks every item. TakeWhile stops at the first false.

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

		// ---------------- Where with multiple conditions ----------------

		var itRich = employees.Where(e => e.DepartmentId == 1 && e.Salary > 1200000).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", itRich));   // o/p: "Amit, Sneha"

		var financeOrTop = employees.Where(e => e.DepartmentId == 3 || e.Salary >= 1500000).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", financeOrTop));   // o/p: "Amit, Priya, Karan, Neha"

		var evenPositions = employees.Where((e, i) => i % 2 == 0).Select(e => e.EmployeeName);   // items at index 0, 2, 4, 6
		Console.WriteLine(string.Join(", ", evenPositions));   // o/p: "Bishnu, Amit, Karan, Neha"

		// ---------------- OrderBy / OrderByDescending ----------------

		var bySalary = employees.OrderBy(e => e.Salary).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", bySalary));   // o/p: "Priya, Rahul, Neha, Bishnu, Arjun, Sneha, Amit, Karan"
		// Bishnu and Arjun both have 12 lakh. Bishnu stays before Arjun (stable sort).

		var bySalaryDesc = employees.OrderByDescending(e => e.Salary).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", bySalaryDesc));   // o/p: "Karan, Amit, Sneha, Bishnu, Arjun, Neha, Rahul, Priya"

		var byName = employees.OrderBy(e => e.EmployeeName).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", byName));   // o/p: "Amit, Arjun, Bishnu, Karan, Neha, Priya, Rahul, Sneha"

		// ---------------- ThenBy / ThenByDescending ----------------

		// Wrong: 2nd OrderBy throws away the 1st sorting
		var wrong = employees.OrderByDescending(e => e.Salary).OrderBy(e => e.EmployeeName).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", wrong));   // o/p: "Amit, Arjun, Bishnu, Karan, Neha, Priya, Rahul, Sneha"

		// Right: salary high to low, and if salary is same, name A to Z
		var right = employees.OrderByDescending(e => e.Salary).ThenBy(e => e.EmployeeName).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", right));   // o/p: "Karan, Amit, Sneha, Arjun, Bishnu, Neha, Rahul, Priya"

		// Department 1, 2, 3 and inside each department, salary high to low
		var byDeptThenSalary = employees.OrderBy(e => e.DepartmentId)
										.ThenByDescending(e => e.Salary)
										.Select(e => $"{e.DepartmentId}-{e.EmployeeName}");
		Console.WriteLine(string.Join(", ", byDeptThenSalary));
		// o/p: "1-Amit, 1-Sneha, 1-Bishnu, 2-Karan, 2-Arjun, 2-Rahul, 3-Neha, 3-Priya"

		// ---------------- Reverse ----------------

		int[] nums = { 1, 2, 3, 4, 5 };
		Console.WriteLine(string.Join(", ", nums.Reverse()));   // o/p: "5, 4, 3, 2, 1"

		// ---------------- Distinct ----------------

		int[] withDuplicates = { 1, 2, 2, 3, 3, 3 };
		Console.WriteLine(string.Join(", ", withDuplicates.Distinct()));   // o/p: "1, 2, 3"

		var deptIds = employees.Select(e => e.DepartmentId).Distinct();
		Console.WriteLine(string.Join(", ", deptIds));   // o/p: "1, 2, 3"

		var uniqueSalaries = employees.Select(e => e.Salary).Distinct().OrderByDescending(s => s);
		Console.WriteLine(string.Join(", ", uniqueSalaries));   // o/p: "1800000, 1500000, 1400000, 1200000, 900000, 800000, 600000"

		// ---------------- Take / Skip ----------------

		var top3 = employees.OrderByDescending(e => e.Salary).Take(3).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", top3));   // o/p: "Karan, Amit, Sneha"

		var exceptTop3 = employees.OrderByDescending(e => e.Salary).Skip(3).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", exceptTop3));   // o/p: "Bishnu, Arjun, Neha, Rahul, Priya"

		var last2 = employees.TakeLast(2).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", last2));   // o/p: "Neha, Arjun"

		// Pagination: page 2, with 3 employees per page
		int pageNumber = 2, pageSize = 3;
		var page2 = employees.OrderBy(e => e.EmployeeId)
							 .Skip((pageNumber - 1) * pageSize)
							 .Take(pageSize)
							 .Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", page2));   // o/p: "Priya, Karan, Sneha"

		// ---------------- TakeWhile / SkipWhile vs Where ----------------

		int[] values = { 1, 2, 3, 10, 4, 5 };

		Console.WriteLine(string.Join(", ", values.Where(n => n < 5)));       // o/p: "1, 2, 3, 4"   (checks every item)
		Console.WriteLine(string.Join(", ", values.TakeWhile(n => n < 5)));   // o/p: "1, 2, 3"      (stops at 10)
		Console.WriteLine(string.Join(", ", values.SkipWhile(n => n < 5)));   // o/p: "10, 4, 5"     (skips until 10, then gives all)
	}
}
